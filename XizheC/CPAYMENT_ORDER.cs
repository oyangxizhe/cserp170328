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
    public class CPAYMENT_ORDER
    {
        basec bc = new basec();
        #region nature
        private string _sql;
        public string sql
        {
            set { _sql = value; }
            get { return _sql; }
        }
        private string _PAYMENT;
        public string PAYMENT
        {
            set { _PAYMENT = value; }
            get { return _PAYMENT; }
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
        private string _RMID;
        public string RMID
        {
            set { _RMID = value; }
            get { return _RMID; }
        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }
        }
        private string _AMOUNT;
        public string AMOUNT
        {
            set { _AMOUNT = value; }
            get { return _AMOUNT; }
        }
        private string _PAYMENT_ORDER_DATE;
        public string PAYMENT_ORDER_DATE
        {
            set { _PAYMENT_ORDER_DATE = value; }
            get { return _PAYMENT_ORDER_DATE; }
        }
        private string _POID;
        public string POID
        {
            set { _POID = value; }
            get { return _POID; }
        }
        private string _MGID;
        public string MGID
        {
            set { _MGID = value; }
            get { return _MGID; }
        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }
        }
        private string _POKEY;
        public string POKEY
        {
            set { _POKEY = value; }
            get { return _POKEY; }
        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }
        }
        #endregion
        #region setsql
        string setsql = @"
with x1 as (
SELECT 
ROW_NUMBER() OVER (ORDER BY K.POKEY ASC)  AS  序号,
K.POID AS 付款单号,
K.PAYMENT_ORDER_DATE AS 付款日期,
k.PAYMENT_ORDER_AMOUNT as 金额,
K.REMARK AS 付款备注,
K.PAYMENT AS 付款方式,
B.MGID AS 入库单号,
B.PICKID AS 提货单号,
C.UNAME AS 供应商ID,
(SELECT TOP 1 B1.GODE_DATE FROM MISC_GODE_DET A1 
LEFT JOIN MISC_GODE_MST B1 ON A1.MGID=B1.MGID WHERE A1.MGID=A.MGID) AS 入库日期,
K.DATE AS 制单日期
FROM   PAYMENT_ORDER K 
LEFT JOIN REQUEST_MONEY_MST A ON A.RMID =K.RMID 
LEFT JOIN MISC_GODE_MST B ON A.MGID =B.MGID
LEFT JOIN USERINFO C ON B.SUPPLIER_ID=C.USID)
select * from x1

";


        #endregion
        #region setsqlo
        string setsqlo = @"
INSERT INTO PAYMENT_ORDER
(
POKEY,
POID,
RMID,
PAYMENT_ORDER_AMOUNT,
PAYMENT_ORDER_MAKERID,
PAYMENT_ORDER_DATE,
REMARK,
PAYMENT,
MAKERID,
DATE,
YEAR,
MONTH,
DAY
)
VALUES
(
@POKEY,
@POID,
@RMID,
@PAYMENT_ORDER_AMOUNT,
@PAYMENT_ORDER_MAKERID,
@PAYMENT_ORDER_DATE,
@REMARK,
@PAYMENT,
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

";
        #endregion
        #region setsqlth
        string setsqlth = @"

";


        #endregion
        #region setsqlf
        string setsqlf = @"
UPDATE PAYMENT_ORDER SET 
RMID=@RMID,
PAYMENT_ORDER_MAKERID=@PAYMENT_ORDER_MAKERID,
PAYMENT_ORDER_DATE=@PAYMENT_ORDER_DATE,
PAYMENT_ORDER_AMOUNT=@PAYMENT_ORDER_AMOUNT,
REMARK=@REMARK,
PAYMENT=@PAYMENT,
MAKERID=@MAKERID,
DATE=@DATE

";
        #endregion
        #region setsqlfi
        string setsqlfi = @"

";
        #endregion
        DataTable dtx2 = new DataTable();
        DataTable dt4 = new DataTable();
  
        public CPAYMENT_ORDER()
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
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM PAYMENT_ORDER", "POID", "PO");
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

            if (!bc.exists("SELECT * FROM PAYMENT_ORDER WHERE POID='" + POID + "'"))
            {
                SQlcommandE(sqlo);
          
                IFExecution_SUCCESS = true;

            }
            else
            {
                SQlcommandE(sqlf + " WHERE POID='" + POID  + "'");
           
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
            POKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM PAYMENT_ORDER", "POKEY", "RO");
            sqlcom.Parameters.Add("@POKEY", SqlDbType.VarChar, 20).Value = POKEY;
            sqlcom.Parameters.Add("@POID", SqlDbType.VarChar, 20).Value = POID;
            sqlcom.Parameters.Add("@RMID", SqlDbType.VarChar, 20).Value = RMID;
            sqlcom.Parameters.Add("@PAYMENT_ORDER_MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@PAYMENT_ORDER_DATE", SqlDbType.VarChar, 20).Value = PAYMENT_ORDER_DATE;
            sqlcom.Parameters.Add("@PAYMENT_ORDER_AMOUNT", SqlDbType.VarChar, 20).Value = AMOUNT;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 1000).Value = REMARK;
            sqlcom.Parameters.Add("@PAYMENT", SqlDbType.VarChar, 20).Value = PAYMENT;
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
 
   


    }
}
