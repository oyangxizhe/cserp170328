using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;
using XizheC;

namespace XizheC
{
    public class CMISC_STORAGE
    {
        basec bc = new basec();
        DataTable dt = new DataTable();
        #region nature
        private string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }
        }
        private string _PICKID;
        public string PICKID
        {
            set { _PICKID = value; }
            get { return _PICKID; }
        }
        private  string _SUPPLIER_ID;
        public  string SUPPLIER_ID
        {
            set { _SUPPLIER_ID = value; }
            get { return _SUPPLIER_ID; }

        }
        private string _PURCHASE_PRICE;
        public string PURCHASE_PRICE
        {
            set { _PURCHASE_PRICE = value; }
            get { return _PURCHASE_PRICE; }
        }
        private string _ORKEY;
        public string ORKEY
        {
            set { _ORKEY = value; }
            get { return _ORKEY; }
        }
        private string _GODE_MAKERID;
        public string GODE_MAKERID
        {
            set { _GODE_MAKERID = value; }
            get { return _GODE_MAKERID; }
        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }
        }
        private string _GODE_DATE;
        public string GODE_DATE
        {
            set { _GODE_DATE = value; }
            get { return _GODE_DATE; }
        }
        private string _BARCODE;
        public string BARCODE
        {
            set { _BARCODE = value; }
            get { return _BARCODE; }
        }
        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }
        }
        private string _MGKEY;
        public string MGKEY
        {
            set { _MGKEY = value; }
            get { return _MGKEY; }
        }
        private string _MGCOUNT;
        public string MGCOUNT
        {
            set { _MGCOUNT = value; }
            get { return _MGCOUNT; }
        }
        private string _MARK;
        public string MARK
        {
            set { _MARK = value; }
            get { return _MARK; }
        }
        private string _SKU;
        public string SKU
        {
            set { _SKU = value; }
            get { return _SKU; }

        }
        private string _sql;
        public string sql
        {
            set { _sql = value; }
            get { return _sql; }

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
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

        }
        private  bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _WP_COUNT;
        public string WP_COUNT
        {
            set { _WP_COUNT = value; }
            get { return _WP_COUNT; }
        }
        private string _MGID;
        public string MGID
        {
            set { _MGID = value; }
            get { return _MGID; }
        }
        #endregion
        #region sql
        string setsql = @"
WITH ds1 as (
SELECT
ROW_NUMBER() OVER (ORDER BY A.MGKEY ASC)  AS 序号, 
F.GODE_DATE AS 入库日期,
A.MGID AS 入库单号, 
D.WAREID AS 物料编号,
D.WName AS 品名,
d.MODEL as 型号,
d.product_type as 产品分类,
d.co_wareid as 产品编号,
A.SN AS 项次,
a.PURCHASE_PRICE as 单价,
c.GECount as 数量,
a.mark as 识别码,
F.SUPPLIER_ID AS 供应商编号,
G.UNAME AS 供应商ID,
F.PICKID AS 提货单号,
(SELECT EMPLOYEE_ID FROM EMPLOYEEINFO WHERE EMID=F.GODE_MAKERID )  AS 入库员工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.GODE_MAKERID )  AS 入库员,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=F.MAKERID )  AS 制单人,
F.DATE AS 制单日期,
(SELECT TOP 1 APID FROM ADVANCE_PAYMENT A1 WHERE A1.MGID=A.MGID ) AS 预付单号,
(SELECT TOP 1 B1.ADVANCE_PAYMENT FROM ADVANCE_PAYMENT A1 
LEFT JOIN GODE B1 ON A1.APKEY=B1.GEKEY WHERE A1.MGID=A.MGID ) AS 预付金额,
RTRIM(CONVERT(DECIMAL(18,2),(CAST(A.PURCHASE_PRICE AS FLOAT)*CAST(C.GECOUNT AS FLOAT)))) AS 金额,
(SELECT SUM(cast(CAST(B2.GECOUNT AS FLOAT)*CAST(B1.PURCHASE_PRICE AS FLOAT) as decimal(18,2))) FROM 
MISC_GODE_DET B1  LEFT JOIN GODE B2 ON B1.MGKEY=B2.GEKEY WHERE B1.MGID =A.MGID  GROUP BY B1.MGID ) AS 总计,
CASE WHEN  H.CUTPAYMENT_AMOUNT='' then 0
ELSE H.CUTPAYMENT_AMOUNT
END 
AS 扣款金额,
(SELECT 
case when sum(PAYMENT_ORDER_AMOUNT) IS null then 0
else 
SUM(PAYMENT_ORDER_AMOUNT) 
end  FROM PAYMENT_ORDER A1 WHERE A1.RMID =H.RMID GROUP BY A1.RMID) AS 累计付款金额1,
(SELECT
  DISTINCT  A1.PAYMENT_ORDER_DATE  +' '+A1.PAYMENT+' '+CONVERT(varchar(20),A1.PAYMENT_ORDER_AMOUNT,111) +';'
FROM 
  PAYMENT_ORDER A1 WHERE A1.RMID =H.RMID 
FOR XML PATH('')) AS 付款记录
FROM MISC_GODE_DET A 
LEFT JOIN Gode  C ON A.MGKEY=C.GEKEY
LEFT JOIN ORDER_BARCODE B ON B.BARCODE =C.BatchID 
LEFT JOIN MISC_GODE_MST F ON A.MGID=F.MGID
LEFT JOIN WareInfo D ON C.WareID =D.WAREID
LEFT JOIN USERINFO G ON F.SUPPLIER_ID=G.USID
LEFT JOIN REQUEST_MONEY_DET E ON A.MGKEY=E.PRKEY 
LEFT JOIN REQUEST_MONEY_MST H ON E.RMID  =H.RMID 
 ),
ds2  as (select 总计-预付金额-扣款金额 as 实际应付金额,
case when 累计付款金额1 IS null then 0 else 累计付款金额1 end as 累计付款金额,*
 from ds1),
ds3 as (select 实际应付金额-累计付款金额 as 未付金额,* from ds2)
select * from ds3



";
        string setsqlo = @"
INSERT INTO 
MISC_GODE_DET
(
MGKEY,
MGID,
SN,
PURCHASE_PRICE,
MARK,
REMARK,
YEAR,
MONTH,
DAY
)
VALUES
(
@MGKEY,
@MGID,
@SN,
@PURCHASE_PRICE,
@MARK,
@REMARK,
@YEAR,
@MONTH,
@DAY

)
";
        string setsqlt = @"
INSERT INTO 
MISC_GODE_MST
(
MGID,
GODE_DATE,
GODE_MAKERID,
SUPPLIER_ID,
PICKID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@MGID,
@GODE_DATE,
@GODE_MAKERID,
@SUPPLIER_ID,
@PICKID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlth = @"
UPDATE MISC_GODE_MST SET 
GODE_DATE=@GODE_DATE,
GODE_MAKERID=@GODE_MAKERID,
SUPPLIER_ID=@SUPPLIER_ID,
PICKID=@PICKID,
DATE=@DATE,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY

";
        string setsqlf = @"
INSERT INTO GODE
(
GEKEY,
GODEID,
SN,
WAREID,
GECOUNT,
SKU,
STORAGEID,
SLID,
ORKEY,
BATCHID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@GEKEY,
@GODEID,
@SN,
@WAREID,
@GECOUNT,
@SKU,
@STORAGEID,
@SLID,
@ORKEY,
@BATCHID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlfi = @"


";
        #endregion
        int i;
        public CMISC_STORAGE()
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
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM MISC_GODE_MST", "MGID", "MG");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
        #region GetTableInfo
        public DataTable GetTableInfo()
        {
            dt = new DataTable();
            dt.Columns.Add("项次", typeof(string));
            dt.Columns.Add("产品分类", typeof(string));
            dt.Columns.Add("产品编号", typeof(string));
            dt.Columns.Add("品名", typeof(string));
            dt.Columns.Add("型号", typeof(string));
            dt.Columns.Add("单价", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            dt.Columns.Add("识别码", typeof(string));
         
            return dt;
        }
        #endregion
        #region ask
        public DataTable ask(string MGID)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sqlfi + " WHERE A.MGID='" + MGID + "' ORDER BY A.MGKEY ASC");
            return dtt;
        }
        #endregion
        #region  JUAGE_CURRENT_STORAGECOUNT_IF_LESSTHAN_DELETE_COUNT
        public bool JUAGE_CURRENT_STORAGECOUNT_IF_LESSTHAN_DELETE_COUNT(string MGID)
        {
            bool b = false;
            DataTable dt = bc.getdt(sql+ " WHERE A.MGID='" + MGID + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    WAREID  = dr["型号"].ToString();
        
                    decimal d= decimal.Parse(dr["数量"].ToString());
                    decimal d1 = 0;
                    DataView dv = new DataView(bc.getstoragecount_no_batchid());
                    dv.RowFilter = "型号='" + WAREID + "'";
                   
                    DataTable dtx = dv.ToTable();
                    if (dtx.Rows.Count > 0)
                    {
                        d1 = decimal.Parse(dtx.Rows[0]["库存数量"].ToString());
                        if (d1 < d)
                        {
                            b = true;
                            ErrowInfo = "型号：" + WAREID + " 库存数量：" + d1.ToString("#0.00")
                                + "小于该型号要删除的入库数量：" + d.ToString("0.00") + "，不允许编辑或删除该单据";
                            break;
                        }
                        
                    }
                    else
                    {

                        b = true;
                        ErrowInfo = "型号：" + WAREID + " 库存数量为0：" + "不允许编辑或删除该单据";
                        break;
                    }
                }
            }
            return b;
        }
        #endregion
        #region save_BARCODE
        public void save_BARCODE()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
        
                int s1, s2;
                DataTable dty = bc.getdt("SELECT * FROM MISC_GODE_DET WHERE MGID='" + MGID + "'");
                if (dty.Rows.Count > 0)
                {
                    s1 = Convert.ToInt32(dty.Rows[dty.Rows.Count - 1]["SN"].ToString());
                    s2 = Convert.ToInt32(s1) + 1;
                }
                else
                {
                    s2 = 1;
                }
                SN = Convert.ToString(s2);
            
            
            if (!bc.exists("SELECT MGID FROM MISC_GODE_DET WHERE MGID='" + MGID + "'"))
            {
               
                    SQlcommandE_DET(sqlo);
                    SQlcommandE_GODE(sqlf);
                
                SQlcommandE_MST(sqlt);
                IFExecution_SUCCESS = true;
            }
            else
            {

               
                    SQlcommandE_DET(sqlo);
                    SQlcommandE_GODE(sqlf);
                
                SQlcommandE_MST(sqlth + " WHERE MGID='" + MGID + "'");
                IFExecution_SUCCESS = true;
            }
            
        }
        #endregion
        #region save
        public void save(DataGridView dgv, bool COME_FROM_DGV_OR_BARCODE)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            if (COME_FROM_DGV_OR_BARCODE)//来自入库单DGV输入数据
            {
                basec.getcoms("DELETE MISC_GODE_DET WHERE MGID='" + MGID + "'");
                basec.getcoms("DELETE GODE WHERE GODEID='" + MGID + "'");
                basec.getcoms("DELETE MARK WHERE MGID='" + MGID + "'");//删除识别码表
                for (i = 0; i < dgv.Rows.Count-1; i++)
                {
                    if (dgv["型号",i].FormattedValue .ToString() == "")
                    {

                    }
                    else
                    {
                        int s1, s2;
                        DataTable dty = bc.getdt("SELECT * FROM MISC_GODE_DET WHERE MGID='" + MGID + "'");
                        if (dty.Rows.Count > 0)
                        {
                            s1 = Convert.ToInt32(dty.Rows[dty.Rows.Count - 1]["SN"].ToString());
                            s2 = Convert.ToInt32(s1) + 1;
                        }
                        else
                        {
                            s2 = 1;
                        }
                        SN = Convert.ToString(s2);
                        WAREID = bc.getOnlyString("SELECT WAREID FROM WAREINFO WHERE MODEL='" + dgv["型号", i].Value.ToString()+"'");
                        MGCOUNT = dgv["数量",i].Value .ToString();
                        PURCHASE_PRICE = dgv["单价",i].Value .ToString();
                        MARK = dgv["识别码",i].FormattedValue .ToString();
                        SKU = "";
                        BARCODE = "";
                        ORKEY = bc.getOnlyString("SELECT ORKEY FROM ORDER_BARCODE WHERE BARCODE='" + BARCODE + "'");
                        SQlcommandE_DET(sqlo);
                        SQlcommandE_GODE(sqlf);
                    
                        string[] a = MARK.Split(',');//将识别码写入表中，用于录入时判断是否有重复的识别码
                        for (int j = 0; j < a.Length; j++)
                        {
                            if (a[j] != "")
                            {
                                basec.getcoms("INSERT INTO MARK(MGID,MARK) VALUES ('" + MGID + "','" + a[j] + "')");
                            }
                        }
                    }
                }
                if (!bc.exists("SELECT MGID FROM MISC_GODE_MST WHERE MGID='" + MGID + "'"))
                {
                    SQlcommandE_MST(sqlt);
                    IFExecution_SUCCESS = true;
                }
                else
                {
                    SQlcommandE_MST(sqlth + " WHERE MGID='" + MGID + "'");
                    IFExecution_SUCCESS = true;
                }
            }
            else//来自条码扫入时保存161031
            {
                int s1, s2;
                DataTable dty = bc.getdt("SELECT * FROM MISC_GODE_DET WHERE MGID='" + MGID + "'");
                if (dty.Rows.Count > 0)
                {
                    s1 = Convert.ToInt32(dty.Rows[dty.Rows.Count - 1]["SN"].ToString());
                    s2 = Convert.ToInt32(s1) + 1;
                }
                else
                {
                    s2 = 1;
                }
                SN = Convert.ToString(s2);
            }

            if (!bc.exists("SELECT MGID FROM MISC_GODE_DET WHERE MGID='" + MGID + "'"))
            {
                if (COME_FROM_DGV_OR_BARCODE == false)
                {
                    SQlcommandE_DET(sqlo);
                    SQlcommandE_GODE(sqlf);
                }
                SQlcommandE_MST(sqlt);
                IFExecution_SUCCESS = true;
            }
            else
            {

                if (COME_FROM_DGV_OR_BARCODE == false)
                {
                    SQlcommandE_DET(sqlo);
                    SQlcommandE_GODE(sqlf);
                }
                SQlcommandE_MST(sqlth + " WHERE MGID='" + MGID + "'");
                IFExecution_SUCCESS = true;
            }

        }
        #endregion
        #region SQlcommandE_DET
        protected void SQlcommandE_DET(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            SqlConnection sqlcon = bc.getcon();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            MGKEY = bc.numYMD(20, 12, "000000000001", "select * from MISC_GODE_DET", "MGKEY", "MG");
            sqlcom.Parameters.Add("@MGKEY", SqlDbType.VarChar, 20).Value = MGKEY;
            sqlcom.Parameters.Add("@MGID", SqlDbType.VarChar, 20).Value = MGID;
            sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = SN;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 20).Value = REMARK;
            sqlcom.Parameters.Add("@PURCHASE_PRICE", SqlDbType.VarChar, 20).Value = PURCHASE_PRICE;
            sqlcom.Parameters.Add("@MARK", SqlDbType.VarChar, 50).Value = MARK;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
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
            sqlcom.Parameters.Add("@MGID", SqlDbType.VarChar, 20).Value = MGID;
            sqlcom.Parameters.Add("@GODE_DATE", SqlDbType.VarChar, 20).Value = GODE_DATE;
            sqlcom.Parameters.Add("@GODE_MAKERID", SqlDbType.VarChar, 20).Value = GODE_MAKERID;
            sqlcom.Parameters.Add("@SUPPLIER_ID", SqlDbType.VarChar, 50).Value = SUPPLIER_ID;
            sqlcom.Parameters.Add("@PICKID", SqlDbType.VarChar, 20).Value = PICKID ; 
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        #region SQlcommandE_GODE
        protected void SQlcommandE_GODE(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcon.Open();
            sqlcom.Parameters.Add("@GEKEY", SqlDbType.VarChar, 20).Value = MGKEY ;
            sqlcom.Parameters.Add("@GODEID", SqlDbType.VarChar, 20).Value = MGID;
            sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = SN;
            sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = WAREID;
            sqlcom.Parameters.Add("@GECOUNT", SqlDbType.VarChar, 20).Value = MGCOUNT;
            sqlcom.Parameters.Add("@SKU", SqlDbType.VarChar, 20).Value = SKU;
            sqlcom.Parameters.Add("@STORAGEID", SqlDbType.VarChar, 20).Value = SUPPLIER_ID;
            sqlcom.Parameters.Add("@SLID", SqlDbType.VarChar, 20).Value = "SLID";
            sqlcom.Parameters.Add("@ORKEY", SqlDbType.VarChar, 20).Value = ORKEY;
            sqlcom.Parameters.Add("@BATCHID", SqlDbType.VarChar, 20).Value = MARK;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
    }
}
