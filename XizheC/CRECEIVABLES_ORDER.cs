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
    public class CRECEIVABLES_ORDER
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
        private string _RCID;
        public string RCID
        {
            set { _RCID = value; }
            get { return _RCID; }
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
        private string _RECEIVABLES_ORDER_DATE;
        public string RECEIVABLES_ORDER_DATE
        {
            set { _RECEIVABLES_ORDER_DATE = value; }
            get { return _RECEIVABLES_ORDER_DATE; }
        }
        private string _ROID;
        public string ROID
        {
            set { _ROID = value; }
            get { return _ROID; }
        }
        private string _ORID;
        public string ORID
        {
            set { _ORID = value; }
            get { return _ORID; }
        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }
        }
        private string _ROKEY;
        public string ROKEY
        {
            set { _ROKEY = value; }
            get { return _ROKEY; }
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
ROW_NUMBER() OVER (ORDER BY K.ROKEY ASC)  AS  序号,
K.ROID AS 收款单号,
K.RECEIVABLES_ORDER_DATE AS 收款日期,
k.RECEIVABLES_ORDER_AMOUNT as 金额,
K.REMARK AS 收款备注,
K.PAYMENT AS 收款方式,
B.CUID AS 客户名称,
B.ORID AS 订单号,
(SELECT TOP 1 B1.SELLDATE FROM SELLTABLE_DET A1 
LEFT JOIN SELLTABLE_MST B1 ON A1.SEID=B1.SEID WHERE A1.ORID=A.ORID) AS 销货日期,
K.DATE AS 制单日期
FROM   RECEIVABLES_ORDER K 
LEFT JOIN RECEIVABLES_MST A ON A.RCID =K.RCID 
LEFT JOIN Order_MST B ON A.ORID =B.ORID )
select * from x1

";


        #endregion
        #region setsqlo
        string setsqlo = @"
INSERT INTO RECEIVABLES_ORDER
(
ROKEY,
ROID,
RCID,
RECEIVABLES_ORDER_AMOUNT,
RECEIVABLES_ORDER_MAKERID,
RECEIVABLES_ORDER_DATE,
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
@ROKEY,
@ROID,
@RCID,
@RECEIVABLES_ORDER_AMOUNT,
@RECEIVABLES_ORDER_MAKERID,
@RECEIVABLES_ORDER_DATE,
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
UPDATE RECEIVABLES_ORDER SET 
RCID=@RCID,
RECEIVABLES_ORDER_MAKERID=@RECEIVABLES_ORDER_MAKERID,
RECEIVABLES_ORDER_DATE=@RECEIVABLES_ORDER_DATE,
RECEIVABLES_ORDER_AMOUNT=@RECEIVABLES_ORDER_AMOUNT,
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
        CRECEIVABLES creceivables = new CRECEIVABLES();
        public CRECEIVABLES_ORDER()
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
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM RECEIVABLES_ORDER", "ROID", "RO");
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

            if (!bc.exists("SELECT * FROM RECEIVABLES_ORDER WHERE ROID='" + ROID + "'"))
            {
                SQlcommandE(sqlo);
          
                IFExecution_SUCCESS = true;

            }
            else
            {
                SQlcommandE(sqlf + " WHERE ROID='" + ROID  + "'");
           
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
            ROKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM RECEIVABLES_ORDER", "ROKEY", "RO");
            sqlcom.Parameters.Add("@ROKEY", SqlDbType.VarChar, 20).Value = ROKEY;
            sqlcom.Parameters.Add("@ROID", SqlDbType.VarChar, 20).Value = ROID;
            sqlcom.Parameters.Add("@RCID", SqlDbType.VarChar, 20).Value = RCID;
            sqlcom.Parameters.Add("@RECEIVABLES_ORDER_MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@RECEIVABLES_ORDER_DATE", SqlDbType.VarChar, 20).Value = RECEIVABLES_ORDER_DATE;
            sqlcom.Parameters.Add("@RECEIVABLES_ORDER_AMOUNT", SqlDbType.VarChar, 20).Value = AMOUNT;
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
        #region RETURN_DT
        public DataTable RETURN_DT()
        {
            DataTable dtt = creceivables.DT_EMPTY();
            string sqlx = "SELECT * FROM RECEIVABLES_ORDER ";
            DataTable dt = bc.getdt(sqlx);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt.Rows)
                {
                    DataRow dr = dtt.NewRow();
                    dr["收款单号"] = dr1["ROID"].ToString();
                    dr["收款金额"] = dr1["RECEIVABLES_ORDER_AMOUNT"].ToString();
                    dr["收款人工号"] = dr1["RECEIVABLES_ORDER_MAKERID"].ToString();
                    dr["收款人"] = bc.getOnlyString("SELECT ENAME FROM EMPLOYEEINFO WHERE EMID='"+dr1["RECEIVABLES_ORDER_MAKERID"].ToString()+"'");
                    dr["收款日期"] = dr1["RECEIVABLES_ORDER_DATE"].ToString();
                    dr["备注"] = dr1["REMARK"].ToString();
                    dr["应收单号"] = dr1["RCID"].ToString();
                    DataTable dtx = bc.GET_DT_TO_DV_TO_DT(creceivables.RETURN_DT(), "", "应收单号='" + dr1["RCID"].ToString() + "'");
                    if (dtx.Rows.Count > 0)
                    {
                        dr["发票号码"] = dtx.Rows[0]["发票号码"].ToString();
                        dr["客户名称"] = dtx.Rows[0]["客户名称"].ToString();
                        dr["累计收款金额"] = dtx.Rows[0]["累计收款金额"].ToString();
                        dr["未收款金额"] = dtx.Rows[0]["未收款金额"].ToString();
                        dr["实际应收金额"] = dtx.Rows[0]["实际应收金额"].ToString();
                    }
                    dr["制单人"] = bc.getOnlyString("SELECT ENAME FROM EMPLOYEEINFO WHERE EMID='" + dr1["MAKERID"].ToString() + "'");
                    dr["制单日期"] = dr1["DATE"].ToString();
                    dtt.Rows.Add(dr);
                }

            }

            return dtt;
        }
        #endregion
   


    }
}
