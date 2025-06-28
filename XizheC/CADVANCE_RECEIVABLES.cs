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
    public class CADVANCE_RECEIVABLES
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
        private string _ARID;
        public string ARID
        {
            set { _ARID = value; }
            get { return _ARID; }

        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }
        }
        private string _ORID;
        public string ORID
        {
            set { _ORID = value; }
            get { return _ORID; }
        }
        private string  _ADVANCE_RECEIVABLES;
        public  string  ADVANCE_RECEIVABLES
        {
            set { _ADVANCE_RECEIVABLES = value; }
            get { return _ADVANCE_RECEIVABLES; }

        }
        private string _CUID;
        public string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }

        }
        private string _ARKEY;
        public string ARKEY
        {
            set { _ARKEY = value; }
            get { return _ARKEY; }
        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }
        }
        private string _ADVANCE_RECEIVABLES_DATE;
        public string ADVANCE_RECEIVABLES_DATE
        {
            set { _ADVANCE_RECEIVABLES_DATE = value; }
            get { return _ADVANCE_RECEIVABLES_DATE; }

        }
        #endregion
        #region setsql
        string setsql = @"
SELECT 
ROW_NUMBER() OVER (ORDER BY A.ARID ASC)  AS  序号,
A.ARID AS 预收单号,
B.CUID  AS 客户名称,
A.ORID AS 订单号,
E.MODEL AS 型号,
D.PRICE  AS 单价,
D.OCOUNT AS 订单数量,
(SELECT TOP 1 BATCHID FROM MATERE A1 WHERE A1.ORKEY=D.ORKEY) AS 识别码,
A.ADVANCE_RECEIVABLES_MAKERID AS 经手人工号,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.ADVANCE_RECEIVABLES_MAKERID) AS 经手人,
A.ADVANCE_RECEIVABLES_DATE AS 预收日期,
CASE WHEN A.IF_ALREADY_USE='Y' THEN '已冲'
ELSE '未冲'
END  AS 冲减否,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=A.MAKERID) AS 制单人,
A.DATE AS 制单日期,
A.REMARK AS 备注,
C.ADVANCE_RECEIVABLES AS 预收金额,
(SELECT SUM(CAST(B1.ADVANCE_RECEIVABLES AS FLOAT)) FROM ADVANCE_RECEIVABLES A1 
LEFT JOIN Gode B1 ON A1.ARKEY=B1.GEKEY 
 WHERE A1.ORID=A.ORID GROUP BY A1.ORID) AS 总计
FROM ADVANCE_RECEIVABLES A 
LEFT JOIN Order_MST B ON A.ORID=B.ORID 
LEFT JOIN GODE C ON A.ARKEY=C.GEKEY
LEFT JOIN ORDER_DET D ON B.ORID=D.ORID
LEFT JOIN WAREINFO E ON D.WAREID=E.WAREID
";


        #endregion
        #region setsqlo
        string setsqlo = @"
INSERT INTO ADVANCE_RECEIVABLES
(
ARKEY,
ARID,
CUID,
ORID,
ADVANCE_RECEIVABLES_MAKERID,
ADVANCE_RECEIVABLES_DATE,
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
@ARKEY,
@ARID,
@CUID,
@ORID,
@ADVANCE_RECEIVABLES_MAKERID,
@ADVANCE_RECEIVABLES_DATE,
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
ADVANCE_RECEIVABLES,
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
@ADVANCE_RECEIVABLES,
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
ADVANCE_RECEIVABLES,
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
@ADVANCE_RECEIVABLES,
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
UPDATE ADVANCE_RECEIVABLES SET 
CUID=@CUID,
ORID=@ORID,
ADVANCE_RECEIVABLES_MAKERID=@ADVANCE_RECEIVABLES_MAKERID,
ADVANCE_RECEIVABLES_DATE=@ADVANCE_RECEIVABLES_DATE,
REMARK=@REMARK,
MAKERID=@MAKERID,
DATE=@DATE

";
        #endregion
        #region setsqlfi
        string setsqlfi = @"
UPDATE GODE SET 
ADVANCE_RECEIVABLES=@ADVANCE_RECEIVABLES,
MAKERID=@MAKERID,
DATE=@DATE
";
        #endregion
        DataTable dtx2 = new DataTable();
        DataTable dt4 = new DataTable();

        public CADVANCE_RECEIVABLES()
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
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM ADVANCE_RECEIVABLES", "ARID", "AR");
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
  
            if (!bc.exists("SELECT * FROM ADVANCE_RECEIVABLES WHERE ARID='" + ARID + "'"))
            {
                SQlcommandE(sqlo);
                SQlcommandE_GODE(sqlt);
                IFExecution_SUCCESS = true;
            
            }
            else
            {
                SQlcommandE(sqlf + " WHERE ARID='" + ARID + "'");
                SQlcommandE_GODE(sqlfi + " WHERE GODEID='" +ARID + "'");
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
            ARKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM ADVANCE_RECEIVABLES", "ARKEY", "AR");
            sqlcom.Parameters.Add("@ARKEY", SqlDbType.VarChar, 20).Value = ARKEY;
            sqlcom.Parameters.Add("@ARID", SqlDbType.VarChar, 20).Value = ARID;
            sqlcom.Parameters.Add("@CUID", SqlDbType.VarChar, 20).Value = CUID;
            sqlcom.Parameters.Add("@ORID", SqlDbType.VarChar, 20).Value = ORID;
            sqlcom.Parameters.Add("@ADVANCE_RECEIVABLES_MAKERID", SqlDbType.VarChar, 20).Value = "";
            sqlcom.Parameters.Add("@ADVANCE_RECEIVABLES_DATE", SqlDbType.VarChar, 20).Value = ADVANCE_RECEIVABLES_DATE;
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
            sqlcom.Parameters.Add("@GEKEY", SqlDbType.VarChar, 20).Value = ARKEY;
            sqlcom.Parameters.Add("@GODEID", SqlDbType.VarChar, 20).Value = ARID;
            sqlcom.Parameters.Add("@ADVANCE_RECEIVABLES", SqlDbType.VarChar, 20).Value = ADVANCE_RECEIVABLES;
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
