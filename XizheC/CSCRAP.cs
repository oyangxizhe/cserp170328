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
    public class CSCRAP
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
        private string _SCRAP_DATE;
        public string SCRAP_DATE
        {
            set { _SCRAP_DATE = value; }
            get { return _SCRAP_DATE; }
        }
        private string _ORKEY;
        public string ORKEY
        {
            set { _ORKEY = value; }
            get { return _ORKEY; }
        }
        private string _MATERE_MAKERID;
        public string MATERE_MAKERID
        {
            set { _MATERE_MAKERID = value; }
            get { return _MATERE_MAKERID; }
        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }
        }
        private string _MATERE_DATE;
        public string MATERE_DATE
        {
            set { _MATERE_DATE = value; }
            get { return _MATERE_DATE; }
        }
        private string _BECASUE;
        public string BECASUE
        {
            set { _BECASUE = value; }
            get { return _BECASUE; }
        }
        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }
        }
        private string _SCKEY;
        public string SCKEY
        {
            set { _SCKEY = value; }
            get { return _SCKEY; }
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
        private string _SCID;
        public string SCID
        {
            set { _SCID = value; }
            get { return _SCID; }
        }
        #endregion
        #region sql
        string setsql = @"
SELECT
ROW_NUMBER() OVER (ORDER BY A.SCKEY ASC)  AS 序号, 
A.SCRAP_DATE AS 日期,
A.SCID AS 报废单号, 
D.WAREID AS 物料编号,
D.WName AS 品名,
d.MODEL as 型号,
d.product_type as 产品分类,
d.co_wareid as 产品编号,
A.SN AS 项次,
c.MRCount as 数量,
F.SUPPLIER_ID AS 供应商编号,
G.UNAME AS 供应商ID,
c.batchid as 识别码,
F.DATE AS 制单日期,
A.BECAUSE as 报废原因
FROM SCRAP_DET A 
LEFT JOIN MateRe   C ON A.SCKEY=C.MRKEY
LEFT JOIN SCRAP_MST F ON A.SCID=F.SCID
LEFT JOIN WareInfo D ON C.WareID =D.WAREID
LEFT JOIN USERINFO G ON F.SUPPLIER_ID=G.USID

";
        string setsqlo = @"
INSERT INTO 
SCRAP_DET
(
SCKEY,
SCID,
SN,
SCRAP_DATE,
BECAUSE,
REMARK,
YEAR,
MONTH,
DAY
)
VALUES
(
@SCKEY,
@SCID,
@SN,
@SCRAP_DATE,
@BECAUSE,
@REMARK,
@YEAR,
@MONTH,
@DAY

)
";
        string setsqlt = @"
INSERT INTO 
SCRAP_MST
(
SCID,
SUPPLIER_ID,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@SCID,
@SUPPLIER_ID,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlth = @"
UPDATE SCRAP_MST SET 
SUPPLIER_ID=@SUPPLIER_ID,
DATE=@DATE,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY

";
        string setsqlf = @"
INSERT INTO 
MATERE
(
MRKEY,
MATEREID,
SN,
MRCOUNT,
WAREID,
STORAGEID,
BATCHID,
ORKEY,
Date,
MakerID,
Year,
Month,
Day
)
VALUES
(
@MRKEY,
@MATEREID,
@SN,
@MRCOUNT,
@WAREID,
@STORAGEID,
@BATCHID,
@ORKEY,
@Date,
@MakerID,
@Year,
@Month,
@Day
)";
        string setsqlfi = @"


";
        #endregion
        int i;
        public CSCRAP()
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
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM SCRAP_MST", "SCID", "SC");
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
            dt.Columns.Add("日期", typeof(string));
            dt.Columns.Add("品名", typeof(string));
            dt.Columns.Add("型号", typeof(string));
            dt.Columns.Add("识别码", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            dt.Columns.Add("报废原因", typeof(string));
            return dt;
        }
        #endregion
        #region ask
        public DataTable ask(string SCID)
        {
            string sql1 = sqlo;
            DataTable dtt = bc.getdt(sqlfi + " WHERE A.SCID='" + SCID + "' ORDER BY A.SCKEY ASC");
            return dtt;
        }
        #endregion

        #region save
        public void save(DataGridView dgv, bool COME_FROM_DGV_OR_BECASUE)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            if (COME_FROM_DGV_OR_BECASUE)//来自入库单DGV输入数据
            {
                basec.getcoms("DELETE SCRAP_DET WHERE SCID='" + SCID + "'");
                basec.getcoms("DELETE MATERE WHERE MATEREID='" + SCID + "'");
                for (i = 0; i < dgv.Rows.Count-1; i++)
                {
                    if (dgv["型号",i].FormattedValue .ToString() == "")
                    {

                    }
                    else
                    {
                        int s1, s2;
                        DataTable dty = bc.getdt("SELECT * FROM SCRAP_DET WHERE SCID='" + SCID + "'");
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
                        SCRAP_DATE = "";
                        MARK = dgv["识别码",i].FormattedValue .ToString();
                        SKU = "";
                        BECASUE = dgv["报废原因", i].FormattedValue.ToString();
                        SCRAP_DATE = dgv["日期", i].FormattedValue.ToString();
                        SQlcommandE_DET(sqlo);
                        SQlcommandE_MATERE(sqlf);
                    
                
                    }
                }
                if (!bc.exists("SELECT SCID FROM SCRAP_MST WHERE SCID='" + SCID + "'"))
                {
                    SQlcommandE_MST(sqlt);
                    IFExecution_SUCCESS = true;
                }
                else
                {
                    SQlcommandE_MST(sqlth + " WHERE SCID='" + SCID + "'");
                    IFExecution_SUCCESS = true;
                }
            }
            else//来自条码扫入时保存161031
            {
                int s1, s2;
                DataTable dty = bc.getdt("SELECT * FROM SCRAP_DET WHERE SCID='" + SCID + "'");
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

            if (!bc.exists("SELECT SCID FROM SCRAP_DET WHERE SCID='" + SCID + "'"))
            {
                if (COME_FROM_DGV_OR_BECASUE == false)
                {
                    SQlcommandE_DET(sqlo);
                    SQlcommandE_MATERE(sqlf);
                }
                SQlcommandE_MST(sqlt);
                IFExecution_SUCCESS = true;
            }
            else
            {

                if (COME_FROM_DGV_OR_BECASUE == false)
                {
                    SQlcommandE_DET(sqlo);
                    SQlcommandE_MATERE(sqlf);
                }
                SQlcommandE_MST(sqlth + " WHERE SCID='" + SCID + "'");
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
            SCKEY = bc.numYMD(20, 12, "000000000001", "select * from SCRAP_DET", "SCKEY", "SC");
            sqlcom.Parameters.Add("@SCKEY", SqlDbType.VarChar, 20).Value = SCKEY;
            sqlcom.Parameters.Add("@SCID", SqlDbType.VarChar, 20).Value = SCID;
            sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = SN;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 20).Value = REMARK;
            sqlcom.Parameters.Add("@SCRAP_DATE", SqlDbType.VarChar, 20).Value = SCRAP_DATE;
            sqlcom.Parameters.Add("@BECAUSE", SqlDbType.VarChar, 50).Value = BECASUE;
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
            sqlcom.Parameters.Add("@SCID", SqlDbType.VarChar, 20).Value = SCID;
            sqlcom.Parameters.Add("@SUPPLIER_ID", SqlDbType.VarChar, 50).Value = SUPPLIER_ID;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        #region SQlcommandE_MATERE
        protected void SQlcommandE_MATERE(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcon.Open();
            sqlcom.Parameters.Add("@MRKEY", SqlDbType.VarChar, 20).Value = SCKEY ;
            sqlcom.Parameters.Add("@MATEREID", SqlDbType.VarChar, 20).Value = SCID;
            sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = dt.Rows[i]["项次"].ToString();
            sqlcom.Parameters.Add("@MRCOUNT", SqlDbType.VarChar, 20).Value = dt.Rows[i]["数量"].ToString();
            sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = bc.getOnlyString("SELECT WAREID FROM WAREINFO WHERE MODEL='" + dt.Rows[i]["型号"].ToString() + "'");
            sqlcom.Parameters.Add("@STORAGEID", SqlDbType.VarChar, 20).Value = bc.getOnlyString("SELECT USID FROM USERINFO WHERE UNAME='" +SUPPLIER_ID  + "'");
            sqlcom.Parameters.Add("@BATCHID", SqlDbType.VarChar, 20).Value = dt.Rows[i]["识别码"].ToString();
            sqlcom.Parameters.Add("@ORKEY", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;


            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
    }
}
