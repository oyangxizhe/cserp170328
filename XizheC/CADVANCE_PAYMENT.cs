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
    public class CADVANCE_PAYMENT
    {
        basec bc = new basec();
        #region nature
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
        private string _IDO;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

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
        private string _APID;
        public string APID
        {
            set { _APID = value; }
            get { return _APID; }

        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }
        }
        private string _MGID;
        public string MGID
        {
            set { _MGID = value; }
            get { return _MGID; }
        }
        private string  _ADVANCE_PAYMENT;
        public  string  ADVANCE_PAYMENT
        {
            set { _ADVANCE_PAYMENT = value; }
            get { return _ADVANCE_PAYMENT; }

        }
        private string _SUID;
        public string SUID
        {
            set { _SUID = value; }
            get { return _SUID; }

        }
        private string _APKEY;
        public string APKEY
        {
            set { _APKEY = value; }
            get { return _APKEY; }
        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }
        }
        private string _ADVANCE_PAYMENT_DATE;
        public string ADVANCE_PAYMENT_DATE
        {
            set { _ADVANCE_PAYMENT_DATE = value; }
            get { return _ADVANCE_PAYMENT_DATE; }

        }
        #endregion
        #region setsql
        string setsql = @"
SELECT 
ROW_NUMBER() OVER (ORDER BY A.APID ASC)  AS  序号,
A.APID AS 预付单号,
F.UName   AS 供应商ID,
B.PICKID  AS 提货单号,
E.MODEL AS 型号,
D.PURCHASE_PRICE  AS 单价,
C.GECount  AS 入库数量,
D.MARK  AS 识别码,
A.ADVANCE_PAYMENT_MAKERID AS 经手人工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.ADVANCE_PAYMENT_MAKERID) AS 经手人,
A.ADVANCE_PAYMENT_DATE AS 预付日期,
CASE WHEN A.IF_ALREADY_USE='Y' THEN '已冲'
ELSE '未冲'
END  AS 冲减否,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS 制单人,
A.DATE AS 制单日期,
A.REMARK AS 备注,
G.ADVANCE_PAYMENT AS 预付金额,
(SELECT SUM(CAST(B1.ADVANCE_PAYMENT AS FLOAT)) FROM ADVANCE_PAYMENT A1 
LEFT JOIN Gode B1 ON A1.APKEY=B1.GEKEY 
 WHERE A1.MGID=A.MGID GROUP BY A1.MGID) AS 总计
FROM ADVANCE_PAYMENT A 
LEFT JOIN MISC_GODE_MST B ON A.MGID=B.MGID  
LEFT JOIN MISC_GODE_DET D ON B.MGID=D.MGID
LEFT JOIN GODE C ON D.MGKEY =C.GEKEY
LEFT JOIN WAREINFO E ON C.WAREID=E.WAREID
LEFT JOIN UserInfo F ON F.USID=B.SUPPLIER_ID 
LEFT JOIN Gode G ON A.APKEY =G.GEKEY 
";


        #endregion
        #region setsqlo
        string setsqlo = @"
INSERT INTO ADVANCE_PAYMENT
(
APKEY,
APID,
SUID,
MGID,
ADVANCE_PAYMENT_MAKERID,
ADVANCE_PAYMENT_DATE,
IF_ALREADY_USE,
REMARK,
MAKERID,
DATE,
YEAR,
MONTH,
DAY
)
VALUES
(
@APKEY,
@APID,
@SUID,
@MGID,
@ADVANCE_PAYMENT_MAKERID,
@ADVANCE_PAYMENT_DATE,
@IF_ALREADY_USE,
@REMARK,
@MAKERID,
@DATE,
@YEAR,
@MONTH,
@DAY


)
";
        #endregion
        #region setsqlt
        string setsqlt = @"
INSERT INTO GODE
(
GEKEY,
GODEID,
ADVANCE_PAYMENT,
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
@ADVANCE_PAYMENT,
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
INSERT INTO MATERRE
(
MRKEY,
MATEREID,
ADVANCE_PAYMENT,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@MRKEY,
@MATEREID,
@ADVANCE_PAYMENT,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";


        #endregion
        #region setsqlf
        string setsqlf = @"
UPDATE ADVANCE_PAYMENT SET 
SUID=@SUID,
MGID=@MGID,
ADVANCE_PAYMENT_MAKERID=@ADVANCE_PAYMENT_MAKERID,
ADVANCE_PAYMENT_DATE=@ADVANCE_PAYMENT_DATE,
REMARK=@REMARK,
MAKERID=@MAKERID,
DATE=@DATE

";
        #endregion
        #region setsqlfi
        string setsqlfi = @"
UPDATE GODE SET 
ADVANCE_PAYMENT=@ADVANCE_PAYMENT,
MAKERID=@MAKERID,
DATE=@DATE
";
        #endregion
        DataTable dtx2 = new DataTable();
        DataTable dt4 = new DataTable();

        public CADVANCE_PAYMENT()
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
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM ADVANCE_PAYMENT", "APID", "AP");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
        #region save
        public void save()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
  
            if (!bc.exists("SELECT * FROM ADVANCE_PAYMENT WHERE APID='" + APID + "'"))
            {
                SQlcommandE(sqlo);
                SQlcommandE_GODE(sqlt);
                IFExecution_SUCCESS = true;
            
            }
            else
            {
                SQlcommandE(sqlf + " WHERE APID='" + APID + "'");
                SQlcommandE_GODE(sqlfi + " WHERE GODEID='" +APID + "'");
                IFExecution_SUCCESS = true;

            }

        }
        #endregion
        #region SQlcommandE
        protected void SQlcommandE(string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            APKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM ADVANCE_PAYMENT", "APKEY", "AP");
            sqlcom.Parameters.Add("@APKEY", SqlDbType.VarChar, 20).Value = APKEY;
            sqlcom.Parameters.Add("@APID", SqlDbType.VarChar, 20).Value = APID;
            sqlcom.Parameters.Add("@SUID", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@MGID", SqlDbType.VarChar, 20).Value = MGID;
            sqlcom.Parameters.Add("@ADVANCE_PAYMENT_MAKERID", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@ADVANCE_PAYMENT_DATE", SqlDbType.VarChar, 20).Value = ADVANCE_PAYMENT_DATE;
            sqlcom.Parameters.Add("@IF_ALREADY_USE", SqlDbType.VarChar, 20).Value = "N";
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 1000).Value = REMARK;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = 
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
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
            sqlcom.Parameters.Add("@GEKEY", SqlDbType.VarChar, 20).Value = APKEY;
            sqlcom.Parameters.Add("@GODEID", SqlDbType.VarChar, 20).Value = APID;
            sqlcom.Parameters.Add("@ADVANCE_PAYMENT", SqlDbType.VarChar, 20).Value = ADVANCE_PAYMENT;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcon.Open();
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
    }
}
