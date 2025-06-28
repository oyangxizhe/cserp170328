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
using System.IO;
using System.Collections.Generic;
namespace XizheC
{
    public class COTHER_COST:IGETID 
    {
        basec bc = new basec();
      
        #region nature
        private string _EMID;
        public string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private string _CUSTOMER_PERCENT;
        public string CUSTOMER_PERCENT
        {
            set { _CUSTOMER_PERCENT = value; }
            get { return _CUSTOMER_PERCENT; }
        }
        private string _CUID;
        public string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }
        }
        private string _BRAND;
        public string BRAND
        {
            set { _BRAND = value; }
            get { return _BRAND; }
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
        private string _PERCENT;
        public string PERCENT
        {
            set { _PERCENT = value; }
            get { return _PERCENT; }

        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }

        private string _PROJECT_NAME;
        public string PROJECT_NAME
        {
            set { _PROJECT_NAME = value; }
            get { return _PROJECT_NAME; }

        }
        private string _OCID;
        public string OCID
        {
            set { _OCID = value; }
            get { return _OCID; }

        }
        private string _TAX_UNIT_PRICE;
        public string TAX_UNIT_PRICE
        {
            set { _TAX_UNIT_PRICE = value; }
            get { return _TAX_UNIT_PRICE; }

        }
        private string _UNIT;
        public string UNIT
        {
            set { _UNIT = value; }
            get { return _UNIT; }

        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }

        }
        private string _TAX_MACHINE_COST;
        public string TAX_MACHINE_COST
        {
            set { _TAX_MACHINE_COST = value; }
            get { return _TAX_MACHINE_COST; }

        }
        private string _TAX_RATE;
        public string TAX_RATE
        {
            set { _TAX_RATE = value; }
            get { return _TAX_RATE; }

        }
        private string _UNIT_PRICE;
        public string UNIT_PRICE
        {

            set { _UNIT_PRICE = value; }
            get { return _UNIT_PRICE; }

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
A.OCID AS 编号,
C.CNAME AS 客户,
A.BRAND AS 品牌,
A.PROJECT_NAME  AS 项目,
RTRIM(CONVERT(DECIMAL(18,1),A.CUSTOMER_PERCENT))+'%' AS 客户比例,
A.REMARK AS 说明,
B.ENAME AS 制单人,
A.DATE AS 制单日期
FROM OTHER_COST A
LEFT JOIN EMPLOYEEINFO B ON A.MAKERID=B.EMID
LEFT  JOIN CUSTOMERINFO_MST C ON A.CUID=C.CUID
";


        string setsqlo = @"



";

        string setsqlt = @"

INSERT INTO OTHER_COST
(
OCID,
PROJECT_NAME,
CUID,
BRAND,
CUSTOMER_PERCENT,
REMARK,
MakerID,
Date,
Year,
Month
)
VALUES
(
@OCID,
@PROJECT_NAME,
@CUID,
@BRAND,
@CUSTOMER_PERCENT,
@REMARK,
@MakerID,
@Date,
@Year,
@Month
)
";
        string setsqlth = @"
UPDATE OTHER_COST SET 
OCID=@OCID,
PROJECT_NAME=@PROJECT_NAME,
CUID=@CUID,
BRAND=@BRAND,
CUSTOMER_PERCENT=@CUSTOMER_PERCENT,
REMARK=@REMARK,
MakerID=@MakerID,
Date=@Date,
Year=@Year,
Month=@Month

";

        string setsqlf = @"

";
        string setsqlfi = @"

";
        string setsqlsi = @"


";
        #endregion
        DataTable dt = new DataTable();
      
        public COTHER_COST()
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
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM OTHER_COST", "OCID", "OC");
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
            string GET_PROJECT_NAME = bc.getOnlyString("SELECT PROJECT_NAME FROM OTHER_COST WHERE  OCID='" + OCID + "'");
            string GET_BRAND = bc.getOnlyString("SELECT BRAND FROM OTHER_COST WHERE  OCID='" + OCID + "'");
            string GET_CUID = bc.getOnlyString("SELECT CUID FROM OTHER_COST WHERE  OCID='" + OCID + "'");
            if (!bc.exists("SELECT OCID FROM OTHER_COST WHERE OCID='" + OCID + "'"))
            {
                if (bc.exists("SELECT * FROM OTHER_COST where PROJECT_NAME='" + PROJECT_NAME + "' AND CUID='"+CUID +"' AND BRAND='" + BRAND + "'"))
                {
                    ErrowInfo = string.Format("项目：{0} + 客户：{1} + 品牌：{2} 组合" + "已经存在系统", PROJECT_NAME,bc.RETURN_CUID_TO_CNAME (CUID ), BRAND);
                    IFExecution_SUCCESS = false;
                }
                else
                {
                    SQlcommandE(sqlt);
                    IFExecution_SUCCESS = true;
                }
            }
            else if (GET_PROJECT_NAME != PROJECT_NAME || CUID!=GET_CUID || GET_BRAND != BRAND)
            {
                if (bc.exists("SELECT * FROM OTHER_COST where PROJECT_NAME='" + PROJECT_NAME + "' AND CUID='" + CUID + "' AND BRAND='" + BRAND + "'"))
                {
                    ErrowInfo = string.Format("项目：{0} + 客户：{1} + 品牌：{2} 组合" + "已经存在系统", PROJECT_NAME, bc.RETURN_CUID_TO_CNAME(CUID), BRAND);
                    IFExecution_SUCCESS = false;
                }
                else
                {
                    SQlcommandE(sqlth + " WHERE OCID='" + OCID + "'");
                    IFExecution_SUCCESS = true;
                }
            }
            else
            {
                SQlcommandE(sqlth + " WHERE OCID='" + OCID + "'");
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
            sqlcon.Open();
            sqlcom.Parameters.Add("OCID", SqlDbType.VarChar, 20).Value = OCID;
            sqlcom.Parameters.Add("PROJECT_NAME", SqlDbType.VarChar, 20).Value = PROJECT_NAME;
            sqlcom.Parameters.Add("CUID", SqlDbType.VarChar, 20).Value = CUID;
            sqlcom.Parameters.Add("BRAND", SqlDbType.VarChar, 1000).Value = BRAND;
            sqlcom.Parameters.Add("CUSTOMER_PERCENT", SqlDbType.VarChar, 20).Value = CUSTOMER_PERCENT;
            sqlcom.Parameters.Add("REMARK", SqlDbType.VarChar, 1000).Value = REMARK;
            sqlcom.Parameters.Add("MakerID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("Date", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        #region emptydatatable_T
        public DataTable emptydatatable_T()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("编号", typeof(string));
            dt.Columns.Add("序号", typeof(string));
            dt.Columns.Add("客户", typeof(string));
            dt.Columns.Add("品牌", typeof(string));
            dt.Columns.Add("项目", typeof(string));
            dt.Columns.Add("客户比例", typeof(string));
            dt.Columns.Add("说明", typeof(string));
            dt.Columns.Add("制单人", typeof(string));
            dt.Columns.Add("制单日期", typeof(string));
            return dt;
        }
        #endregion
        #region RETURN_HAVE_ID_DT
        public DataTable RETURN_HAVE_ID_DT(DataTable dtx)
        {
            DataTable dt = emptydatatable_T();
            int i = 1;
            foreach (DataRow dr1 in dtx.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["编号"] = dr1["编号"].ToString();
                dr["序号"] = i.ToString();
                dr["客户"] = dr1["客户"].ToString();
                dr["品牌"] = dr1["品牌"].ToString();
                dr["项目"] = dr1["项目"].ToString();
                dr["客户比例"] = dr1["客户比例"].ToString();
                dr["说明"] = dr1["说明"].ToString();
                dr["制单人"] = dr1["制单人"].ToString();
                dr["制单日期"] = dr1["制单日期"].ToString();
                dt.Rows.Add(dr);
                i = i + 1;
            }
            return dt;
        }
        #endregion
    
    }
}
