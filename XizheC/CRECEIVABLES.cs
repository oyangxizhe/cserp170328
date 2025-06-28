using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop;
using System.Security.Cryptography;

namespace XizheC
{
    public class CRECEIVABLES
    {
        basec bc = new basec();
        #region nature
        private string _sql;
        public string sql
        {
            set { _sql = value; }
            get { return _sql; }

        }
        private string _CUTPAYMENT_AMOUNT;
        public string CUTPAYMENT_AMOUNT
        {
            set { _CUTPAYMENT_AMOUNT = value; }
            get { return _CUTPAYMENT_AMOUNT; }
        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }
        }
        private string _sqlo;
        public string sqlo
        {
            set { _sqlo = value; }
            get { return _sqlo; }

        }
        private string _sqlt;
        public string sqlt
        {
            set { _sqlt = value; }
            get { return _sqlt; }

        }
        private string _sqlth;
        public string sqlth
        {
            set { _sqlth = value; }
            get { return _sqlth; }

        }
        private string _sqlf;
        public string sqlf
        {
            set { _sqlf = value; }
            get { return _sqlf; }

        }
        private string _sqlfi;
        public string sqlfi
        {
            set { _sqlfi = value; }
            get { return _sqlfi; }

        }
        private string _CUTPAYMENT_PROJECT;
        public string CUTPAYMENT_PROJECT
        {
            set { _CUTPAYMENT_PROJECT = value; }
            get { return _CUTPAYMENT_PROJECT; }
        }
        private string _IDO;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }
        }
        private string _RCID;
        public string RCID
        {
            set { _RCID = value; }
            get { return _RCID; }
        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }
        }
        private string _RECEIVABLES_DATE;
        public string RECEIVABLES_DATE
        {
            set { _RECEIVABLES_DATE = value; }
            get { return _RECEIVABLES_DATE; }
        }

        private string _ARID;
        public string ARID
        {
            set { _ARID = value; }
            get { return _ARID; }
        }
        private string _ORID;
        public string ORID
        {
            set { _ORID = value; }
            get { return _ORID; }
        }
        private string _ORKEY;
        public string ORKEY
        {
            set { _ORKEY = value; }
            get { return _ORKEY; }
        }
        private string _RCKEY;
        public string RCKEY
        {
            set { _RCKEY = value; }
            get { return _RCKEY; }
        }
        private string _INVOICE_NOTAX_AMOUNT;
        public string INVOICE_NOTAX_AMOUNT
        {
            set { _INVOICE_NOTAX_AMOUNT = value; }
            get { return _INVOICE_NOTAX_AMOUNT; }
        }
        #endregion
        #region setsql
        string setsql = @"

with ds1 as (
SELECT 
ROW_NUMBER() OVER (ORDER BY A.RCID ASC)  AS  序号,
B.RCKEY AS 应收索引,
A.RCID AS 应收单号,
A.INVOICE_NOTAX_AMOUNT AS 应收金额,
A.ARID AS 预收单号,
CASE WHEN J.ADVANCE_RECEIVABLES IS NULL THEN 0 
ELSE J.ADVANCE_RECEIVABLES  
END AS 预收金额,
D.ORID AS 订单号,
E.CUID AS 客户名称,
H.MODEL AS 型号,
(SELECT TOP 1 BATCHID FROM MATERE A1 WHERE A1.ORKEY=D.ORKEY) AS 识别码,
D.PRICE  AS 单价,
D.OCOUNT AS 订单数量,
B.COUNT as 实际销货数量,
RTRIM(CONVERT(DECIMAL(18,2),(D.PRICE*B.COUNT))) AS 金额,
(SELECT SUM(cast(A1.COUNT*B1.PRICE as decimal(18,2))) FROM RECEIVABLES_DET  A1  
LEFT JOIN Order_DET B1 ON A1.SSKEY =B1.ORKEY WHERE B1.ORID =D.ORID GROUP BY B1.ORID ) AS 总计,
E.ORDER_DATE AS 订单日期,
B.SSKEY AS 索引,
A.CUTPAYMENT_PROJECT AS 扣款项目,
CASE WHEN  A.CUTPAYMENT_AMOUNT='' then 0
ELSE A.CUTPAYMENT_AMOUNT
END 
AS 扣款金额,
(SELECT 
case when sum(RECEIVABLES_ORDER_AMOUNT) IS null then 0
else 
SUM(RECEIVABLES_ORDER_AMOUNT) 
end  FROM RECEIVABLES_ORDER A1 WHERE A1.RCID =A.RCID GROUP BY A1.RCID) AS 累计收款金额1,
A.RECEIVABLES_DATE AS 应收日期,
A.Date AS 制单日期,
(SELECT
  DISTINCT  A1.RECEIVABLES_ORDER_DATE  +' '+A1.PAYMENT+' '+CONVERT(varchar(20),A1.RECEIVABLES_ORDER_AMOUNT,111) +';'
FROM 
  RECEIVABLES_ORDER A1 WHERE A1.RCID =A.RCID 
FOR XML PATH('')
) AS 收款记录
FROM RECEIVABLES_MST A 
LEFT JOIN RECEIVABLES_DET B ON A.RCID=B.RCID
LEFT JOIN ORDER_DET D ON B.SSKEY =D.ORKEY 
LEFT JOIN ORDER_MST E ON D.ORID=E.ORID 
LEFT JOIN WareInfo H ON D.WareID =H.WareID 
LEFT JOIN ADVANCE_RECEIVABLES I ON A.ARID=I.ARID
LEFT JOIN GODE J ON I.ARID=J.GODEID

),
ds2  as (select 总计-预收金额-扣款金额 as 实际应收金额,
case when 累计收款金额1 IS null then 0 else 累计收款金额1 end as 累计收款金额,*
 from ds1),
ds3 as (select 实际应收金额-累计收款金额 as 未收金额,* from ds2)
select * from ds3


";
        #endregion
        #region setsqlo
        string setsqlo = @"
INSERT INTO RECEIVABLES_DET
(
RCKEY,
RCID,
SSKEY,
COUNT,
YEAR,
MONTH,
DAY
)
VALUES
(
@RCKEY,
@RCID,
@SSKEY,
@COUNT,
@YEAR,
@MONTH,
@DAY

)
";
        #endregion
        #region setsqlt
        string setsqlt = @"
INSERT INTO RECEIVABLES_MST
(
RCID,
ORID,
ARID,
INVOICE_NO,
INVOICE_NOTAX_AMOUNT,
INVOICE_TAX_AMOUNT,
INVOICE_HAVETAX_AMOUNT,
CUTPAYMENT_PROJECT,
CUTPAYMENT_AMOUNT,
RECEIVABLES_DATE,
RECEIVABLES_MAKERID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@RCID,
@ORID,
@ARID,
@INVOICE_NO,
@INVOICE_NOTAX_AMOUNT,
@INVOICE_TAX_AMOUNT,
@INVOICE_HAVETAX_AMOUNT,
@CUTPAYMENT_PROJECT,
@CUTPAYMENT_AMOUNT,
@RECEIVABLES_DATE,
@RECEIVABLES_MAKERID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY

)
";
        #endregion
        #region setsqlth
        string setsqlth = @"

UPDATE  RECEIVABLES_MST SET
ORID=@ORID,
ARID=@ARID,
INVOICE_NOTAX_AMOUNT=@INVOICE_NOTAX_AMOUNT,
CUTPAYMENT_PROJECT=@CUTPAYMENT_PROJECT,
CUTPAYMENT_AMOUNT=@CUTPAYMENT_AMOUNT,
RECEIVABLES_DATE=RECEIVABLES_DATE
";
        #endregion
        #region setsqlf
        string setsqlf = @"


";
        #endregion
        #region setsqlfi
        string setsqlfi = @"


";
        #endregion
        DataTable dtx2 = new DataTable();
        DataTable dt4 = new DataTable();
        CORDER corder = new CORDER();
        public CRECEIVABLES()
        {
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
        }
        public string GETID()
        {
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM RECEIVABLES_MST", "RCID", "RC");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
        #region save
        public void  save(DataTable dt)//返回第一次执行后订单的项次最新库存数量与累计销退数量值
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            basec.getcoms("DELETE RECEIVABLES_DET WHERE RCID='"+RCID +"'");
            if (!bc.exists("SELECT RCID FROM RECEIVABLES_MST WHERE RCID='" + RCID + "'"))
            {
                SQlcommandE_DET(dt,sqlo);
                SQlcommandE_MST(sqlt);
                IFExecution_SUCCESS = true;
               
            }
            else
            {
                SQlcommandE_DET(dt, sqlo);
                SQlcommandE_MST(sqlth + " WHERE RCID='" + RCID + "'");
                IFExecution_SUCCESS = true;
                
            }
          
        }
        #endregion
        #region SQlcommandE_DET
        protected void SQlcommandE_DET(DataTable dt, string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
          
                    SqlConnection sqlcon = bc.getcon();
                    sqlcon.Open();
                    SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
                    RCKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM RECEIVABLES_DET", "RCKEY", "RC");
                    ORKEY = bc.getOnlyString(string.Format (@"SELECT ORKEY FROM ORDER_DET WHERE ORID='{0}' 
                AND SN='{1}'",dt.Rows [i]["订单号"].ToString (),dt.Rows [i]["项次"].ToString ()));
                    sqlcom.Parameters.Add("@RCKEY", SqlDbType.VarChar, 20).Value = RCKEY ;
                    sqlcom.Parameters.Add("@RCID", SqlDbType.VarChar, 20).Value = RCID;
                    sqlcom.Parameters.Add("@SSKEY", SqlDbType.VarChar, 20).Value = ORKEY;
                    sqlcom.Parameters.Add("@COUNT", SqlDbType.VarChar, 20).Value = dt.Rows[i]["实际销货数量"].ToString();
                    sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
                    sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                    sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
                    sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
                    sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
                    sqlcom.ExecuteNonQuery();
                    sqlcon.Close();
                
            }
        }
        #endregion
        #region SQlcommandE_MST
        protected void SQlcommandE_MST(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcon.Open();
            sqlcom.Parameters.Add("@RCID", SqlDbType.VarChar, 20).Value = RCID;
            sqlcom.Parameters.Add("@ORID", SqlDbType.VarChar, 20).Value = ORID;
            sqlcom.Parameters.Add("@ARID", SqlDbType.VarChar, 20).Value = ARID;
            sqlcom.Parameters.Add("@INVOICE_NO", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@INVOICE_NOTAX_AMOUNT", SqlDbType.VarChar, 20).Value = INVOICE_NOTAX_AMOUNT;
            sqlcom.Parameters.Add("@INVOICE_TAX_AMOUNT", SqlDbType.VarChar, 20).Value = "0";
            sqlcom.Parameters.Add("@INVOICE_HAVETAX_AMOUNT", SqlDbType.VarChar, 20).Value = "0";
            sqlcom.Parameters.Add("@RECEIVABLES_MAKERID", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@RECEIVABLES_DATE", SqlDbType.VarChar, 20).Value = RECEIVABLES_DATE;
            sqlcom.Parameters.Add("@CUTPAYMENT_PROJECT", SqlDbType.VarChar, 20).Value = CUTPAYMENT_PROJECT;
            sqlcom.Parameters.Add("@CUTPAYMENT_AMOUNT", SqlDbType.VarChar, 20).Value = CUTPAYMENT_AMOUNT;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        public DataTable DT_EMPTY()
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("选择", typeof(bool));
            dtt.Columns.Add("索引", typeof(string));
            dtt.Columns.Add("应收单号", typeof(string));
            dtt.Columns.Add("订单号", typeof(string));
            dtt.Columns.Add("销货单号", typeof(string));
            dtt.Columns.Add("目录项次", typeof(Int32));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("ID", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("销售单位", typeof(string));
            dtt.Columns.Add("销售单价", typeof(decimal));
            dtt.Columns.Add("订单数量", typeof(decimal));
            dtt.Columns.Add("税率", typeof(decimal));
            dtt.Columns.Add("未税金额", typeof(decimal));
            dtt.Columns.Add("税额", typeof(decimal));
            dtt.Columns.Add("含税金额", typeof(decimal));
            dtt.Columns.Add("客户代码", typeof(string));
            dtt.Columns.Add("客户名称", typeof(string));
            dtt.Columns.Add("订单日期", typeof(string));
            dtt.Columns.Add("发票号码", typeof(string));
            dtt.Columns.Add("发票未税金额", typeof(decimal));
            dtt.Columns.Add("发票税额", typeof(decimal));
            dtt.Columns.Add("发票含税金额", typeof(decimal));
            dtt.Columns.Add("应收索引", typeof(string));
            dtt.Columns.Add("销货(销退)单号", typeof(string));
            dtt.Columns.Add("销货(销退)数量", typeof(string));
            dtt.Columns.Add("累计销货数量", typeof(decimal));
            dtt.Columns.Add("应收人工号", typeof(string));
            dtt.Columns.Add("应收人", typeof(string));
            dtt.Columns.Add("应收日期", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("合计未税金额", typeof(decimal));
            dtt.Columns.Add("合计税额", typeof(decimal));
            dtt.Columns.Add("合计含税金额", typeof(decimal));
            dtt.Columns.Add("预收款单号", typeof(string));
            dtt.Columns.Add("预收款金额", typeof(string));
            dtt.Columns.Add("扣款项目", typeof(string));
            dtt.Columns.Add("扣款金额", typeof(string));
            dtt.Columns.Add("实际应收金额", typeof(decimal));
            dtt.Columns.Add("累计收款金额", typeof(string));
            dtt.Columns.Add("未收款金额", typeof(decimal));
            dtt.Columns.Add("收款单号", typeof(string));
            dtt.Columns.Add("收款金额", typeof(decimal));
            dtt.Columns.Add("收款人工号", typeof(string));
            dtt.Columns.Add("收款人", typeof(string));
            dtt.Columns.Add("收款日期", typeof(string));
            dtt.Columns.Add("收款制单人", typeof(string));
            dtt.Columns.Add("收款制单日期", typeof(string));
            dtt.Columns.Add("备注", typeof(string));
     
            return dtt;
        }
   
        #region dtx
        public  DataTable dtx()
        {

            DataTable dtt = DT_EMPTY();
            DataTable dt4 = basec.getdts(corder.sqlo+" WHERE C.IF_HAVE_INVOICE='N' AND F.ORDERSTATUS_MST='CLOSE' "+corder.sqlt);
            int i = 0;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {   
                    DataRow dr = dtt.NewRow();
                    dr["选择"] = false;
                   
                    dr["订单号"] = dr1["订单号"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["规格"] = dr1["规格"].ToString();
                    dr["销售单位"] = dr1["销售单位"].ToString();
                    dr["销售单价"] = dr1["销售单价"].ToString();
                    dr["订单数量"] = dr1["订单数量"].ToString();
                    dr["累计销货数量"] = dr1["累计销货数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = dr1["未税金额"].ToString();
                    dr["税额"] = dr1["税额"].ToString();
                    dr["含税金额"] = dr1["含税金额"].ToString();
                    dr["客户代码"] = dr1["客户代码"].ToString();
                    dr["客户名称"] = dr1["客户名称"].ToString();
                    dr["订单日期"] = dr1["订单日期"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }
         
            }
       
            return dtt;
        }
        #endregion
        #region RETURN_PG_AND_RETURN_DT
        public DataTable RETURN_PG_AND_RETURN_DT()
        {

            DataTable dtt = DT_EMPTY();
            DataTable dt4 = basec.getdts(sqlth + " WHERE SUBSTRING (B.SSKEY,1,2)='SE' ");
            int i = 0;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    dr["索引"] = dr1["索引"].ToString();
                    dr["订单号"] = dr1["订单号"].ToString();
                    dr["销货(销退)单号"] = dr1["销货销退单号"].ToString();
                    dr["销货(销退)数量"] = dr1["销货销退数量"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["规格"] = dr1["规格"].ToString();
                    dr["销售单位"] = dr1["销售单位"].ToString();
                    dr["销售单价"] = dr1["销售单价"].ToString();
                    dr["订单数量"] = dr1["订单数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = dr1["未税金额"].ToString();
                    dr["税额"] = dr1["税额"].ToString();
                    dr["含税金额"] = dr1["含税金额"].ToString();
                    dr["扣款项目"] = dr1["扣款项目"].ToString();
                    dr["扣款金额"] = dr1["扣款金额"].ToString();
                    dr["客户代码"] = dr1["客户代码"].ToString();
                    dr["客户名称"] = dr1["客户名称"].ToString();
                    dr["订单日期"] = dr1["订单日期"].ToString();
                    dr["应收索引"] = dr1["应收索引"].ToString();
                    dr["应收人工号"] = dr1["应收人工号"].ToString();
                    dr["应收人"] = dr1["应收人"].ToString();
                    dr["制单人"] = dr1["制单人"].ToString();
                    dr["应收日期"] = dr1["应收日期"].ToString();
                    dr["发票号码"] = dr1["发票号码"].ToString();
                    dr["发票未税金额"] = dr1["发票未税金额"].ToString();
                    dr["发票税额"] = dr1["发票税额"].ToString();
                    dr["发票含税金额"] = dr1["发票含税金额"].ToString();
                    dr["应收单号"] = dr1["应收单号"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }
            dt4 = basec.getdts(sqlfi + " WHERE SUBSTRING (B.SSKEY,1,2)='SR' ");
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    decimal d1 = decimal.Parse(dr1["未税金额"].ToString());
                    decimal d2 = -d1;
                    decimal d3 = decimal.Parse(dr1["税额"].ToString());
                    decimal d4 = -d3;
                    decimal d5 = decimal.Parse(dr1["含税金额"].ToString());
                    decimal d6 = -d5;
                    dr["选择"] = false;
                    dr["索引"] = dr1["索引"].ToString();
                    dr["订单号"] = dr1["订单号"].ToString();
                    dr["销货(销退)单号"] = dr1["销货销退单号"].ToString();
                    dr["销货(销退)数量"] = dr1["销货销退数量"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["规格"] = dr1["规格"].ToString();
                    dr["销售单位"] = dr1["销售单位"].ToString();
                    dr["销售单价"] = dr1["销售单价"].ToString();
                    dr["订单数量"] = dr1["订单数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = d2;
                    dr["税额"] = d4;
                    dr["含税金额"] = d6;
                    dr["客户代码"] = dr1["客户代码"].ToString();
                    dr["客户名称"] = dr1["客户名称"].ToString();
                    dr["扣款项目"] = dr1["扣款项目"].ToString();
                    dr["扣款金额"] = dr1["扣款金额"].ToString();
                    dr["订单日期"] = dr1["订单日期"].ToString();
                    dr["应收索引"] = dr1["应收索引"].ToString();
                    dr["应收人工号"] = dr1["应收人工号"].ToString();
                    dr["应收人"] = dr1["应收人"].ToString();
                    dr["制单人"] = dr1["制单人"].ToString();
                    dr["应收日期"] = dr1["应收日期"].ToString();
                    dr["发票号码"] = dr1["发票号码"].ToString();
                    dr["发票未税金额"] = dr1["发票未税金额"].ToString();
                    dr["发票税额"] = dr1["发票税额"].ToString();
                    dr["发票含税金额"] = dr1["发票含税金额"].ToString();
                    dr["应收单号"] = dr1["应收单号"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }
            foreach (DataRow dr in dtt.Rows)
            {
                dr["合计未税金额"] = dtt.Compute("SUM(未税金额)", "");
                dr["合计税额"] = dtt.Compute("SUM(税额)", "");
                dr["合计含税金额"] = dtt.Compute("SUM(含税金额)", "");

            }
            return dtt;
        }
        #endregion
        #region RETURN_PG_AND_RETURN_DT
        public DataTable RETURN_PG_AND_RETURN_DT(string RCID)
        {

            DataTable dtt = DT_EMPTY();
            DataTable dt4 = basec.getdts(sqlth + " WHERE SUBSTRING (B.SSKEY,1,2)='SE' AND A.RCID='" + RCID + "'");
            int i = 0;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    dr["索引"] = dr1["索引"].ToString();
                    dr["订单号"] = dr1["订单号"].ToString();
                    dr["销货(销退)单号"] = dr1["销货销退单号"].ToString();
                    dr["销货(销退)数量"] = dr1["销货销退数量"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["规格"] = dr1["规格"].ToString();
                    dr["销售单位"] = dr1["销售单位"].ToString();
                    dr["销售单价"] = dr1["销售单价"].ToString();
                    dr["订单数量"] = dr1["订单数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = dr1["未税金额"].ToString();
                    dr["税额"] = dr1["税额"].ToString();
                    dr["含税金额"] = dr1["含税金额"].ToString();
                    dr["扣款项目"] = dr1["扣款项目"].ToString();
                    dr["扣款金额"] = dr1["扣款金额"].ToString();
                    dr["客户代码"] = dr1["客户代码"].ToString();
                    dr["客户名称"] = dr1["客户名称"].ToString();
                    dr["订单日期"] = dr1["订单日期"].ToString();
                    dr["应收索引"] = dr1["应收索引"].ToString();
                    dr["应收人工号"] = dr1["应收人工号"].ToString();
                    dr["应收人"] = dr1["应收人"].ToString();
                    dr["制单人"] = dr1["制单人"].ToString();
                    dr["应收日期"] = dr1["应收日期"].ToString();
                    dr["发票号码"] = dr1["发票号码"].ToString();
                    dr["发票未税金额"] = dr1["发票未税金额"].ToString();
                    dr["发票税额"] = dr1["发票税额"].ToString();
                    dr["发票含税金额"] = dr1["发票含税金额"].ToString();
                    dr["应收单号"] = dr1["应收单号"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }
            dt4 = basec.getdts(sqlfi + " WHERE SUBSTRING (B.SSKEY,1,2)='SR' AND A.RCID='" + RCID + "'");
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    decimal d1 = decimal.Parse(dr1["未税金额"].ToString());
                    decimal d2 = -d1;
                    decimal d3 = decimal.Parse(dr1["税额"].ToString());
                    decimal d4 = -d3;
                    decimal d5 = decimal.Parse(dr1["含税金额"].ToString());
                    decimal d6 = -d5;
                    dr["选择"] = false;
                    dr["索引"] = dr1["索引"].ToString();
                    dr["订单号"] = dr1["订单号"].ToString();
                    dr["销货(销退)单号"] = dr1["销货销退单号"].ToString();
                    dr["销货(销退)数量"] = dr1["销货销退数量"].ToString();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["目录项次"] = i + 1;
                    dr["ID"] = dr1["ID"].ToString();
                    dr["料号"] = dr1["料号"].ToString();
                    dr["品名"] = dr1["品名"].ToString();
                    dr["规格"] = dr1["规格"].ToString();
                    dr["销售单位"] = dr1["销售单位"].ToString();
                    dr["销售单价"] = dr1["销售单价"].ToString();
                    dr["订单数量"] = dr1["订单数量"].ToString();
                    dr["税率"] = dr1["税率"].ToString();
                    dr["未税金额"] = d2;
                    dr["税额"] = d4;
                    dr["含税金额"] = d6;
                    dr["客户代码"] = dr1["客户代码"].ToString();
                    dr["客户名称"] = dr1["客户名称"].ToString();
                    dr["扣款项目"] = dr1["扣款项目"].ToString();
                    dr["扣款金额"] = dr1["扣款金额"].ToString();
                    dr["订单日期"] = dr1["订单日期"].ToString();
                    dr["应收索引"] = dr1["应收索引"].ToString();
                    dr["应收人工号"] = dr1["应收人工号"].ToString();
                    dr["应收人"] = dr1["应收人"].ToString();
                    dr["制单人"] = dr1["制单人"].ToString();
                    dr["应收日期"] = dr1["应收日期"].ToString();
                    dr["发票号码"] = dr1["发票号码"].ToString();
                    dr["发票未税金额"] = dr1["发票未税金额"].ToString();
                    dr["发票税额"] = dr1["发票税额"].ToString();
                    dr["发票含税金额"] = dr1["发票含税金额"].ToString();
                    dr["应收单号"] = dr1["应收单号"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }
            foreach (DataRow dr in dtt.Rows)
            {
                dr["合计未税金额"] = dtt.Compute("SUM(未税金额)", "");
                dr["合计税额"] = dtt.Compute("SUM(税额)", "");
                dr["合计含税金额"] = dtt.Compute("SUM(含税金额)", "");
            
            }
            return dtt;
        }
        #endregion

        #region TOTAL_RETURN_PG_AND return_DT
        public DataTable TOTAL_RETURN_PG_AND_RETURN_DT(string RCID)
        {
            DataTable dtt = DT_EMPTY();
            decimal d1 = 0, d2 = 0, d3 = 0;
            DataTable dt = RETURN_PG_AND_RETURN_DT(RCID);
            DataRow dr = dtt.NewRow();
            dr["合计未税金额"] = dt.Compute("SUM(未税金额)", "");
            dr["合计税额"] = dt.Compute("SUM(税额)", "");
            dr["合计含税金额"] = dt.Compute("SUM(含税金额)", "");
            dr["客户名称"] = dt.Rows[0]["客户名称"].ToString();
            dr["应收人"] = dt.Rows[0]["应收人"].ToString();
            dr["应收日期"] = dt.Rows[0]["应收日期"].ToString();
            dr["制单人"] = dt.Rows[0]["制单人"].ToString();
            dr["扣款项目"] = dt.Rows[0]["扣款项目"].ToString();
            dr["扣款金额"] = dt.Rows[0]["扣款金额"].ToString();
            if (!string.IsNullOrEmpty(dt.Rows[0]["扣款金额"].ToString()))
            {
                d3 = decimal.Parse(dt.Rows[0]["扣款金额"].ToString());
            }

            dtt.Rows.Add(dr);
         
            d1 = decimal.Parse(dtt.Rows[0]["合计含税金额"].ToString());
            string sqlx = @"
SELECT 
A.ARID AS ARID,
C.ADVANCE_RECEIVABLES AS ADVANCE_RECEIVABLES
FROM RECEIVABLES_MST A 
LEFT JOIN ADVANCE_RECEIVABLES  B ON A.ARID=B.ARID 
LEFT JOIN GODE C ON B.ARKEY=C.GEKEY 
WHERE RCID='" + RCID + "'";

                dt = bc.getdt(sqlx);
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ADVANCE_RECEIVABLES"].ToString()))
                    {
                        d2 = decimal.Parse(dt.Rows[0]["ADVANCE_RECEIVABLES"].ToString());
                    }
                   

                }
          
                dtt.Rows[0]["预收款单号"] = dt.Rows[0]["ARID"].ToString();
                dtt.Rows[0]["预收款金额"] = dt.Rows[0]["ADVANCE_RECEIVABLES"].ToString();
                dtt.Rows[0]["实际应收金额"] = d1 - d2 - d3;
 
            return dtt;
        }
        #endregion
        #region RETURN_DT
        public DataTable RETURN_DT()
        {
            DataTable dtt = DT_EMPTY();
            string sqlx = "SELECT * FROM RECEIVABLES_MST ";
            DataTable  dt = bc.getdt(sqlx);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt.Rows)
                {

                    DataRow dr = dtt.NewRow();
                    dr["应收单号"] = dr1["RCID"].ToString();
                    dr["发票号码"] = dr1["INVOICE_NO"].ToString();
                    dr["发票未税金额"] = dr1["INVOICE_NOTAX_AMOUNT"].ToString();
                    dr["发票税额"] = dr1["INVOICE_TAX_AMOUNT"].ToString();
                    dr["发票含税金额"] = dr1["INVOICE_HAVETAX_AMOUNT"].ToString();
                    dr["制单日期"] = dr1["DATE"].ToString();
                    dr["扣款项目"] = dr1["CUTPAYMENT_PROJECT"].ToString();
                    dr["扣款金额"] = dr1["CUTPAYMENT_AMOUNT"].ToString();
                    DataTable dtx = TOTAL_RETURN_PG_AND_RETURN_DT(dr1["RCID"].ToString());
                    dr["客户名称"] = dtx.Rows[0]["客户名称"].ToString();
                    dr["合计未税金额"] = dtx.Rows[0]["合计未税金额"].ToString();
                    dr["合计税额"] = dtx.Rows[0]["合计税额"].ToString();
                    dr["预收款单号"] = dtx.Rows[0]["预收款单号"].ToString();
                    dr["预收款金额"] = dtx.Rows[0]["预收款金额"].ToString();
                    dr["合计含税金额"] = dtx.Rows[0]["合计含税金额"].ToString();
                    dr["实际应收金额"] = dtx.Rows[0]["实际应收金额"].ToString();
                    dr["应收人"] = dtx.Rows[0]["应收人"].ToString();
                    dr["应收日期"] = dtx.Rows[0]["应收日期"].ToString();
                    dr["制单人"] = dtx.Rows[0]["制单人"].ToString();
                    DataTable dtx1 = bc.getdt(@"
SELECT 
RCID AS RCID,
SUM(RECEIVABLES_ORDER_AMOUNT) AS RECEIVABLES_ORDER_AMOUNT 
FROM RECEIVABLES_ORDER 
WHERE
RCID='" + dr1["RCID"].ToString()+"' GROUP BY RCID");
                    if (dtx1.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtx1.Rows[0]["RECEIVABLES_ORDER_AMOUNT"].ToString()))
                        {
                            dr["累计收款金额"] = dtx1.Rows[0]["RECEIVABLES_ORDER_AMOUNT"].ToString();
                            dr["未收款金额"] = decimal.Parse(dtx.Rows[0]["实际应收金额"].ToString()) - decimal.Parse(dtx1.Rows[0]["RECEIVABLES_ORDER_AMOUNT"].ToString());
                        }

                    }
                    else
                    {
                        dr["累计收款金额"] = "0.00";
                        dr["未收款金额"] = decimal.Parse(dtx.Rows[0]["实际应收金额"].ToString());
                    }
                    dtt.Rows.Add(dr);
                }

            }

            return dtt;
        }
        #endregion
        #region JUAGE_IF_EXISTS_SE_SERETURN()
        public bool  JUAGE_IF_EXISTS_SE_SERETURN(string SEID_OR_REID,string SEKEY_OR_SRKEY)
        {
         
            bool b = false;
            DataTable dt = RETURN_PG_AND_RETURN_DT();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["销货(销退)单号"].ToString() == SEID_OR_REID)
                    {
                        b = true;
                        break;
                    }
                    if (dr["索引"].ToString() == SEKEY_OR_SRKEY)
                    {
                        b = true;
                        break;
                    }
                }

            }
        
            return b;
        }
        #endregion
    }
}
