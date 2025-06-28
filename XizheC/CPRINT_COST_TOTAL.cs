using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using XizheC;
using System.Windows.Forms;

namespace XizheC
{
    public class CPRINT_COST_TOTAL:IGETID 
    {
        basec bc = new basec();
        #region nature
        private string _EMID;
        public string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private string _OFFER_ID;
        public string OFFER_ID
        {
            set { _OFFER_ID = value; }
            get { return _OFFER_ID; }

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

        private string _sqlsi;
        public string sqlsi
        {
            set { _sqlsi = value; }
            get { return _sqlsi; }

        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

        }

        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }

        private string _PRINT_COST_TOTAL;
        public string PRINT_COST_TOTAL
        {
            set { _PRINT_COST_TOTAL = value; }
            get { return _PRINT_COST_TOTAL; }

        }
        private string _DPID;
        public string DPID
        {
            set { _DPID = value; }
            get { return _DPID; }

        }
        private string _STARTING_BATCH_TOTAL;
        public string STARTING_BATCH_TOTAL
        {
            set { _STARTING_BATCH_TOTAL = value; }
            get { return _STARTING_BATCH_TOTAL; }

        }
        private string _STARTING_BATCH_TOTAL_UNIT;
        public string STARTING_BATCH_TOTAL_UNIT
        {
            set { _STARTING_BATCH_TOTAL_UNIT = value; }
            get { return _STARTING_BATCH_TOTAL_UNIT; }

        }
        private string _UNIT_BATCH_TOTAL_UNIT;
        public string UNIT_BATCH_TOTAL_UNIT
        {
            set { _UNIT_BATCH_TOTAL_UNIT = value; }
            get { return _UNIT_BATCH_TOTAL_UNIT; }

        }
        private string _MAX_BATCH_TOTAL;
        public string MAX_BATCH_TOTAL
        {
            set { _MAX_BATCH_TOTAL = value; }
            get { return _MAX_BATCH_TOTAL; }

        }
        private string _PFID;
        public string PFID
        {
            set { _PFID = value; }
            get { return _PFID; }

        }
        private string _UNIT_BATCH_TOTAL;
        public string UNIT_BATCH_TOTAL
        {

            set { _UNIT_BATCH_TOTAL = value; }
            get { return _UNIT_BATCH_TOTAL; }

        }
        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }

        }

        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
  
        #endregion
        #region sql
        string setsql = @"
SELECT 
A.PFID AS 编号,
A.PROJECT_NAME AS 项目,
A.YUAN_SET AS 元套,
A.BATCH_TOTAL AS 批量小计,
A.MAIN_DOSAGE AS 主件用量,
B.ENAME AS 制单人,
A.DATE AS 制单日期
FROM PRINT_COST_TOTAL A
LEFT JOIN EMPLOYEEINFO B ON A.MAKERID=B.EMID
LEFT JOIN PRINTING_OFFER_MST C ON A.PFID=C.PFID


";


        string setsqlo = @"



";

        string setsqlt = @"

INSERT INTO PRINT_COST_TOTAL
(
PCID,
PFID,
PROJECT_NAME,
YUAN_SET,
BATCH_TOTAL,
MAIN_DOSAGE,
MakerID,
Date,
Year,
Month,
DAY
)
VALUES
(
@PCID,
@PFID,
@PROJECT_NAME,
@YUAN_SET,
@BATCH_TOTAL,
@MAIN_DOSAGE,
@MakerID,
@Date,
@Year,
@Month,
@DAY

)
";
        string setsqlth = @"




";

        string setsqlf = @"

";
        string setsqlfi = @"

";
        string setsqlsi = @"


";
        #endregion
        DataTable dt = new DataTable();
   
        public CPRINT_COST_TOTAL()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
            sqlsi = setsqlsi;
        }
        public string GETID()
        {
            string v1 = bc.numYMD(12, 4, "0001", "SELECT * FROM PRINT_COST_TOTAL", "PCID", "PC");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
        #region save
        public void save(DataTable dt)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            basec.getcoms("DELETE PRINT_COST_TOTAL WHERE PFID='" + PFID + "'");
            SQlcommandE(sqlt, dt);
            IFExecution_SUCCESS = true;
        }
        #endregion
        #region SQlcommandE
        protected void SQlcommandE(string sql,DataTable dt)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //MessageBox.Show( dt.Rows [i]["项目"].ToString()+","+dt.Rows[i]["主件用量"].ToString());
                SqlConnection sqlcon = bc.getcon();
                SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
                sqlcon.Open();
                sqlcom.Parameters.Add("PCID", SqlDbType.VarChar, 20).Value = GETID();
                sqlcom.Parameters.Add("PFID", SqlDbType.VarChar, 20).Value = PFID;
                sqlcom.Parameters.Add("PROJECT_NAME", SqlDbType.VarChar, 20).Value = dt.Rows [i]["项目"].ToString();
                sqlcom.Parameters.Add("YUAN_SET", SqlDbType.VarChar, 20).Value = dt.Rows [i]["元套"].ToString();
                sqlcom.Parameters.Add("BATCH_TOTAL", SqlDbType.VarChar,20).Value =dt.Rows [i]["批量小计"].ToString();
                sqlcom.Parameters.Add("MAIN_DOSAGE", SqlDbType.VarChar, 20).Value = dt.Rows [i]["主件用量"].ToString();
                sqlcom.Parameters.Add("MakerID", SqlDbType.VarChar, 20).Value = MAKERID;
                sqlcom.Parameters.Add("Date", SqlDbType.VarChar, 20).Value = varDate;
                sqlcom.Parameters.Add("YEAR", SqlDbType.VarChar, 20).Value = year;
                sqlcom.Parameters.Add("MONTH", SqlDbType.VarChar, 20).Value = month;
                sqlcom.Parameters.Add("DAY", SqlDbType.VarChar, 20).Value = day;
                sqlcom.ExecuteNonQuery();
                sqlcon.Close(); 
            }
          
        }
        #endregion
      
        #region GetTableInfo
        public DataTable GetTableInfo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(string));
            dt.Columns.Add("项目", typeof(string));
            dt.Columns.Add("元套", typeof(string));
            dt.Columns.Add("批量小计", typeof(string));
            dt.Columns.Add("主件用量", typeof(string));
            return dt;
        }
        #endregion
        #region RETURN_DT
        public DataTable RETURN_DT(DataTable dtt)
        {
            int i = 1;
            DataTable dt = GetTableInfo();
            foreach (DataRow dr1 in dtt.Rows)
            {
                DataRow dr = dt.NewRow();
                if (i == dtt.Rows.Count - 1 || i == dtt.Rows.Count - 2)
                {
                }
                else
                {
                    dr["序号"] = i.ToString();
                }
                dr["项目"] = dr1["项目"].ToString();
                dr["元套"] = dr1["元套"].ToString();
                dr["批量小计"] = dr1["批量小计"].ToString();
                dr["主件用量"] = dr1["主件用量"].ToString();
                i = i + 1;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion
    }
}
