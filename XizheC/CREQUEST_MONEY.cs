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
    public class CREQUEST_MONEY
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
        private string _RMID;
        public string RMID
        {
            set { _RMID = value; }
            get { return _RMID; }
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
        private string _REQUEST_MONEY_DATE;
        public string REQUEST_MONEY_DATE
        {
            set { _REQUEST_MONEY_DATE = value; }
            get { return _REQUEST_MONEY_DATE; }
        }

        private string _APID;
        public string APID
        {
            set { _APID = value; }
            get { return _APID; }
        }
        private string _MGID;
        public string MGID
        {
            set { _MGID = value; }
            get { return _MGID; }
        }
        private string _MGKEY;
        public string MGKEY
        {
            set { _MGKEY = value; }
            get { return _MGKEY; }
        }
        private string _RMKEY;
        public string RMKEY
        {
            set { _RMKEY = value; }
            get { return _RMKEY; }
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
ROW_NUMBER() OVER (ORDER BY B.RMKEY ASC)  AS  序号,
B.RMKEY AS 应付索引,
A.RMID AS 应付单号,
A.INVOICE_NOTAX_AMOUNT AS 应付金额,
G.SN AS 项次,
H.WareID AS ID,
(SELECT TOP 1 BATCHID FROM Gode  A1 WHERE A1.GEKEY=C.MGKEY ) AS 识别码,
H.CO_WAREID AS 料号,
H.WName AS 品名,
H.SPEC AS 规格,
H.MODEL AS 型号,
G.SKU AS 采购单位,
E.UNAME AS 供应商ID,
D.PICKID AS 提货单号,
B.PRKEY AS 索引,
C.MGID AS 入库单号,
C.PURCHASE_PRICE  AS 单价,
G.GECount AS 入库退货数量,
B.COUNT as 实际入库数量,
A.APID AS 预付单号,
CASE WHEN I.ADVANCE_PAYMENT IS NULL THEN 0 
ELSE I.ADVANCE_PAYMENT   
END AS 预付金额,
RTRIM(CONVERT(DECIMAL(18,2),(CAST(C.PURCHASE_PRICE AS FLOAT)*CAST(B.COUNT AS FLOAT)))) AS 金额,
(SELECT SUM(cast(CAST(A1.COUNT AS FLOAT)*CAST(B1.PURCHASE_PRICE AS FLOAT) as decimal(18,2))) FROM REQUEST_MONEY_DET  A1  
LEFT JOIN MISC_GODE_DET B1 ON A1.PRKEY =B1.MGKEY WHERE B1.MGID =C.MGID GROUP BY B1.MGID ) AS 总计,
A.CUTPAYMENT_PROJECT AS 扣款项目,
CASE WHEN  A.CUTPAYMENT_AMOUNT='' then 0
ELSE A.CUTPAYMENT_AMOUNT
END 
AS 扣款金额,
(SELECT 
case when sum(PAYMENT_ORDER_AMOUNT) IS null then 0
else 
SUM(PAYMENT_ORDER_AMOUNT) 
end  FROM PAYMENT_ORDER A1 WHERE A1.RMID =A.RMID GROUP BY A1.RMID) AS 累计付款金额1,
A.REQUEST_MONEY_MAKERID 应付人工号,
(SELECT ENAME FROM EmployeeInfo WHERE EMID=A.REQUEST_MONEY_MAKERID ) AS 应付人,
A.REQUEST_MONEY_DATE AS 应付日期,
(SELECT ENAME FROM EmployeeInfo WHERE EMID=A.MakerID ) AS 制单人,
A.Date AS 制单日期
FROM REQUEST_MONEY_MST A 
LEFT JOIN REQUEST_MONEY_DET B ON A.RMID=B.RMID
LEFT JOIN MISC_GODE_DET C ON B.PRKEY =C.MGKEY  
LEFT JOIN MISC_GODE_MST D ON C.MGID=D.MGID
LEFT JOIN USERINFO E ON D.SUPPLIER_ID=E.USID
LEFT JOIN ADVANCE_PAYMENT F ON A.APID=F.APID 
LEFT JOIN Gode G ON C.MGKEY =G.GEKEY 
LEFT JOIN WareInfo H ON G.WareID =H.WareID
LEFT JOIN Gode I ON F.APID =I.GodeID 
 ),
ds2  as (select 总计-预付金额-扣款金额 as 实际应付金额,
case when 累计付款金额1 IS null then 0 else 累计付款金额1 end as 累计付款金额,*
 from ds1),
ds3 as (select 实际应付金额-累计付款金额 as 未付金额,* from ds2)
select * from ds3
";
        #endregion
        #region setsqlo
        string setsqlo = @"
INSERT INTO REQUEST_MONEY_DET
(
RMKEY,
RMID,
PRKEY,
COUNT,
YEAR,
MONTH,
DAY
)
VALUES
(
@RMKEY,
@RMID,
@PRKEY,
@COUNT,
@YEAR,
@MONTH,
@DAY

)
";
        #endregion
        #region setsqlt
        string setsqlt = @"
INSERT INTO REQUEST_MONEY_MST
(
RMID,
MGID,
APID,
INVOICE_NO,
INVOICE_NOTAX_AMOUNT,
INVOICE_TAX_AMOUNT,
INVOICE_HAVETAX_AMOUNT,
CUTPAYMENT_PROJECT,
CUTPAYMENT_AMOUNT,
REQUEST_MONEY_DATE,
REQUEST_MONEY_MAKERID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@RMID,
@MGID,
@APID,
@INVOICE_NO,
@INVOICE_NOTAX_AMOUNT,
@INVOICE_TAX_AMOUNT,
@INVOICE_HAVETAX_AMOUNT,
@CUTPAYMENT_PROJECT,
@CUTPAYMENT_AMOUNT,
@REQUEST_MONEY_DATE,
@REQUEST_MONEY_MAKERID,
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

UPDATE  REQUEST_MONEY_MST SET
MGID=@MGID,
APID=@APID,
INVOICE_NOTAX_AMOUNT=@INVOICE_NOTAX_AMOUNT,
CUTPAYMENT_PROJECT=@CUTPAYMENT_PROJECT,
CUTPAYMENT_AMOUNT=@CUTPAYMENT_AMOUNT,
REQUEST_MONEY_DATE=REQUEST_MONEY_DATE
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
        CMISC_STORAGE cmisc_storage = new CMISC_STORAGE();
        public CREQUEST_MONEY()
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
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM REQUEST_MONEY_MST", "RMID", "RM");
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
            basec.getcoms("DELETE REQUEST_MONEY_DET WHERE RMID='"+RMID +"'");
            if (!bc.exists("SELECT RMID FROM REQUEST_MONEY_MST WHERE RMID='" + RMID + "'"))
            {
                SQlcommandE_DET(dt,sqlo);
                SQlcommandE_MST(sqlt);
                IFExecution_SUCCESS = true;
               
            }
            else
            {
                SQlcommandE_DET(dt, sqlo);
                SQlcommandE_MST(sqlth + " WHERE RMID='" + RMID + "'");
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
                    RMKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM REQUEST_MONEY_DET", "RMKEY", "RM");
                    MGKEY = bc.getOnlyString(string.Format (@"SELECT MGKEY FROM MISC_GODE_DET WHERE MGID='{0}' 
                AND SN='{1}'",dt.Rows [i]["入库单号"].ToString (),dt.Rows [i]["项次"].ToString ()));
                    sqlcom.Parameters.Add("@RMKEY", SqlDbType.VarChar, 20).Value = RMKEY ;
                    sqlcom.Parameters.Add("@RMID", SqlDbType.VarChar, 20).Value = RMID;
                    sqlcom.Parameters.Add("@PRKEY", SqlDbType.VarChar, 20).Value = MGKEY;
                    sqlcom.Parameters.Add("@COUNT", SqlDbType.VarChar, 20).Value = dt.Rows[i]["实际入库数量"].ToString();
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
            sqlcom.Parameters.Add("@RMID", SqlDbType.VarChar, 20).Value = RMID;
            sqlcom.Parameters.Add("@MGID", SqlDbType.VarChar, 20).Value = MGID;
            sqlcom.Parameters.Add("@APID", SqlDbType.VarChar, 20).Value = APID;
            sqlcom.Parameters.Add("@INVOICE_NO", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@INVOICE_NOTAX_AMOUNT", SqlDbType.VarChar, 20).Value = INVOICE_NOTAX_AMOUNT;
            sqlcom.Parameters.Add("@INVOICE_TAX_AMOUNT", SqlDbType.VarChar, 20).Value = "0";
            sqlcom.Parameters.Add("@INVOICE_HAVETAX_AMOUNT", SqlDbType.VarChar, 20).Value = "0";
            sqlcom.Parameters.Add("@REQUEST_MONEY_MAKERID", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@REQUEST_MONEY_DATE", SqlDbType.VarChar, 20).Value = REQUEST_MONEY_DATE;
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
            dtt.Columns.Add("应付单号", typeof(string));
            dtt.Columns.Add("入库单号", typeof(string));
            dtt.Columns.Add("入库单号", typeof(string));
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
            dtt.Columns.Add("应付索引", typeof(string));
            dtt.Columns.Add("入库(销退)单号", typeof(string));
            dtt.Columns.Add("入库(销退)数量", typeof(string));
            dtt.Columns.Add("累计入库数量", typeof(decimal));
            dtt.Columns.Add("应付人工号", typeof(string));
            dtt.Columns.Add("应付人", typeof(string));
            dtt.Columns.Add("应付日期", typeof(string));
            dtt.Columns.Add("制单日期", typeof(string));
            dtt.Columns.Add("制单人", typeof(string));
            dtt.Columns.Add("合计未税金额", typeof(decimal));
            dtt.Columns.Add("合计税额", typeof(decimal));
            dtt.Columns.Add("合计含税金额", typeof(decimal));
            dtt.Columns.Add("预付款单号", typeof(string));
            dtt.Columns.Add("预付款金额", typeof(string));
            dtt.Columns.Add("扣款项目", typeof(string));
            dtt.Columns.Add("扣款金额", typeof(string));
            dtt.Columns.Add("实际应付金额", typeof(decimal));
            dtt.Columns.Add("累计付款金额", typeof(string));
            dtt.Columns.Add("未付款金额", typeof(decimal));
            dtt.Columns.Add("付款单号", typeof(string));
            dtt.Columns.Add("付款金额", typeof(decimal));
            dtt.Columns.Add("付款人工号", typeof(string));
            dtt.Columns.Add("付款人", typeof(string));
            dtt.Columns.Add("付款日期", typeof(string));
            dtt.Columns.Add("付款制单人", typeof(string));
            dtt.Columns.Add("付款制单日期", typeof(string));
            dtt.Columns.Add("备注", typeof(string));
     
            return dtt;
        }
   

        #region RETURN_PG_AND_RETURN_DT
        public DataTable RETURN_PG_AND_RETURN_DT()
        {

            DataTable dtt = DT_EMPTY();
            DataTable dt4 = basec.getdts(sqlth + " WHERE SUBSTRING (B.PRKEY,1,2)='SE' ");
            int i = 0;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    dr["索引"] = dr1["索引"].ToString();
                    dr["入库单号"] = dr1["入库单号"].ToString();
                    dr["入库(销退)单号"] = dr1["入库销退单号"].ToString();
                    dr["入库(销退)数量"] = dr1["入库销退数量"].ToString();
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
                    dr["应付索引"] = dr1["应付索引"].ToString();
                    dr["应付人工号"] = dr1["应付人工号"].ToString();
                    dr["应付人"] = dr1["应付人"].ToString();
                    dr["制单人"] = dr1["制单人"].ToString();
                    dr["应付日期"] = dr1["应付日期"].ToString();
                    dr["发票号码"] = dr1["发票号码"].ToString();
                    dr["发票未税金额"] = dr1["发票未税金额"].ToString();
                    dr["发票税额"] = dr1["发票税额"].ToString();
                    dr["发票含税金额"] = dr1["发票含税金额"].ToString();
                    dr["应付单号"] = dr1["应付单号"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }
            dt4 = basec.getdts(sqlfi + " WHERE SUBSTRING (B.PRKEY,1,2)='SR' ");
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
                    dr["入库单号"] = dr1["入库单号"].ToString();
                    dr["入库(销退)单号"] = dr1["入库销退单号"].ToString();
                    dr["入库(销退)数量"] = dr1["入库销退数量"].ToString();
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
                    dr["应付索引"] = dr1["应付索引"].ToString();
                    dr["应付人工号"] = dr1["应付人工号"].ToString();
                    dr["应付人"] = dr1["应付人"].ToString();
                    dr["制单人"] = dr1["制单人"].ToString();
                    dr["应付日期"] = dr1["应付日期"].ToString();
                    dr["发票号码"] = dr1["发票号码"].ToString();
                    dr["发票未税金额"] = dr1["发票未税金额"].ToString();
                    dr["发票税额"] = dr1["发票税额"].ToString();
                    dr["发票含税金额"] = dr1["发票含税金额"].ToString();
                    dr["应付单号"] = dr1["应付单号"].ToString();
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
        public DataTable RETURN_PG_AND_RETURN_DT(string RMID)
        {

            DataTable dtt = DT_EMPTY();
            DataTable dt4 = basec.getdts(sqlth + " WHERE SUBSTRING (B.PRKEY,1,2)='SE' AND A.RMID='" + RMID + "'");
            int i = 0;
            if (dt4.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt4.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    dr["索引"] = dr1["索引"].ToString();
                    dr["入库单号"] = dr1["入库单号"].ToString();
                    dr["入库(销退)单号"] = dr1["入库销退单号"].ToString();
                    dr["入库(销退)数量"] = dr1["入库销退数量"].ToString();
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
                    dr["应付索引"] = dr1["应付索引"].ToString();
                    dr["应付人工号"] = dr1["应付人工号"].ToString();
                    dr["应付人"] = dr1["应付人"].ToString();
                    dr["制单人"] = dr1["制单人"].ToString();
                    dr["应付日期"] = dr1["应付日期"].ToString();
                    dr["发票号码"] = dr1["发票号码"].ToString();
                    dr["发票未税金额"] = dr1["发票未税金额"].ToString();
                    dr["发票税额"] = dr1["发票税额"].ToString();
                    dr["发票含税金额"] = dr1["发票含税金额"].ToString();
                    dr["应付单号"] = dr1["应付单号"].ToString();
                    dtt.Rows.Add(dr);
                    i = i + 1;
                }

            }
            dt4 = basec.getdts(sqlfi + " WHERE SUBSTRING (B.PRKEY,1,2)='SR' AND A.RMID='" + RMID + "'");
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
                    dr["入库单号"] = dr1["入库单号"].ToString();
                    dr["入库(销退)单号"] = dr1["入库销退单号"].ToString();
                    dr["入库(销退)数量"] = dr1["入库销退数量"].ToString();
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
                    dr["应付索引"] = dr1["应付索引"].ToString();
                    dr["应付人工号"] = dr1["应付人工号"].ToString();
                    dr["应付人"] = dr1["应付人"].ToString();
                    dr["制单人"] = dr1["制单人"].ToString();
                    dr["应付日期"] = dr1["应付日期"].ToString();
                    dr["发票号码"] = dr1["发票号码"].ToString();
                    dr["发票未税金额"] = dr1["发票未税金额"].ToString();
                    dr["发票税额"] = dr1["发票税额"].ToString();
                    dr["发票含税金额"] = dr1["发票含税金额"].ToString();
                    dr["应付单号"] = dr1["应付单号"].ToString();
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
        public DataTable TOTAL_RETURN_PG_AND_RETURN_DT(string RMID)
        {
            DataTable dtt = DT_EMPTY();
            decimal d1 = 0, d2 = 0, d3 = 0;
            DataTable dt = RETURN_PG_AND_RETURN_DT(RMID);
            DataRow dr = dtt.NewRow();
            dr["合计未税金额"] = dt.Compute("SUM(未税金额)", "");
            dr["合计税额"] = dt.Compute("SUM(税额)", "");
            dr["合计含税金额"] = dt.Compute("SUM(含税金额)", "");
            dr["客户名称"] = dt.Rows[0]["客户名称"].ToString();
            dr["应付人"] = dt.Rows[0]["应付人"].ToString();
            dr["应付日期"] = dt.Rows[0]["应付日期"].ToString();
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
A.APID AS APID,
C.ADVANCE_REQUEST_MONEY AS ADVANCE_REQUEST_MONEY
FROM REQUEST_MONEY_MST A 
LEFT JOIN ADVANCE_REQUEST_MONEY  B ON A.APID=B.APID 
LEFT JOIN GODE C ON B.ARKEY=C.GEKEY 
WHERE RMID='" + RMID + "'";

                dt = bc.getdt(sqlx);
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["ADVANCE_REQUEST_MONEY"].ToString()))
                    {
                        d2 = decimal.Parse(dt.Rows[0]["ADVANCE_REQUEST_MONEY"].ToString());
                    }
                   

                }
          
                dtt.Rows[0]["预付款单号"] = dt.Rows[0]["APID"].ToString();
                dtt.Rows[0]["预付款金额"] = dt.Rows[0]["ADVANCE_REQUEST_MONEY"].ToString();
                dtt.Rows[0]["实际应付金额"] = d1 - d2 - d3;
 
            return dtt;
        }
        #endregion
        #region RETURN_DT
        public DataTable RETURN_DT()
        {
            DataTable dtt = DT_EMPTY();
            string sqlx = "SELECT * FROM REQUEST_MONEY_MST ";
            DataTable  dt = bc.getdt(sqlx);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt.Rows)
                {

                    DataRow dr = dtt.NewRow();
                    dr["应付单号"] = dr1["RMID"].ToString();
                    dr["发票号码"] = dr1["INVOICE_NO"].ToString();
                    dr["发票未税金额"] = dr1["INVOICE_NOTAX_AMOUNT"].ToString();
                    dr["发票税额"] = dr1["INVOICE_TAX_AMOUNT"].ToString();
                    dr["发票含税金额"] = dr1["INVOICE_HAVETAX_AMOUNT"].ToString();
                    dr["制单日期"] = dr1["DATE"].ToString();
                    dr["扣款项目"] = dr1["CUTPAYMENT_PROJECT"].ToString();
                    dr["扣款金额"] = dr1["CUTPAYMENT_AMOUNT"].ToString();
                    DataTable dtx = TOTAL_RETURN_PG_AND_RETURN_DT(dr1["RMID"].ToString());
                    dr["客户名称"] = dtx.Rows[0]["客户名称"].ToString();
                    dr["合计未税金额"] = dtx.Rows[0]["合计未税金额"].ToString();
                    dr["合计税额"] = dtx.Rows[0]["合计税额"].ToString();
                    dr["预付款单号"] = dtx.Rows[0]["预付款单号"].ToString();
                    dr["预付款金额"] = dtx.Rows[0]["预付款金额"].ToString();
                    dr["合计含税金额"] = dtx.Rows[0]["合计含税金额"].ToString();
                    dr["实际应付金额"] = dtx.Rows[0]["实际应付金额"].ToString();
                    dr["应付人"] = dtx.Rows[0]["应付人"].ToString();
                    dr["应付日期"] = dtx.Rows[0]["应付日期"].ToString();
                    dr["制单人"] = dtx.Rows[0]["制单人"].ToString();
                    DataTable dtx1 = bc.getdt(@"
SELECT 
RMID AS RMID,
SUM(REQUEST_MONEY_MISC_GODE_AMOUNT) AS REQUEST_MONEY_MISC_GODE_AMOUNT 
FROM REQUEST_MONEY_MISC_GODE 
WHERE
RMID='" + dr1["RMID"].ToString()+"' GROUP BY RMID");
                    if (dtx1.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtx1.Rows[0]["REQUEST_MONEY_MISC_GODE_AMOUNT"].ToString()))
                        {
                            dr["累计付款金额"] = dtx1.Rows[0]["REQUEST_MONEY_MISC_GODE_AMOUNT"].ToString();
                            dr["未付款金额"] = decimal.Parse(dtx.Rows[0]["实际应付金额"].ToString()) - decimal.Parse(dtx1.Rows[0]["REQUEST_MONEY_MISC_GODE_AMOUNT"].ToString());
                        }

                    }
                    else
                    {
                        dr["累计付款金额"] = "0.00";
                        dr["未付款金额"] = decimal.Parse(dtx.Rows[0]["实际应付金额"].ToString());
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
                    if (dr["入库(销退)单号"].ToString() == SEID_OR_REID)
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
