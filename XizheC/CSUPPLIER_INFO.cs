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
    public class CSUPPLIER_INFO
    {
        basec bc = new basec();

        #region nature
        private string _EMID;
        public string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private string _CONTACT;
        public string CONTACT
        {
            set { _CONTACT = value; }
            get { return _CONTACT; }

        }
        private string _WATER_MARK_CONTENT;
        public string WATER_MARK_CONTENT
        {
            set { _WATER_MARK_CONTENT = value; }
            get { return _WATER_MARK_CONTENT; }

        }
        private string _PHONE;
        public string PHONE
        {
            set { _PHONE = value; }
            get { return _PHONE; }

        }
        private string _SALE_AUDIT;
        public string SALE_AUDIT
        {
            set { _SALE_AUDIT = value; }
            get { return _SALE_AUDIT; }

        }
        private string _FINANCIAL_AUDIT;
        public string FINANCIAL_AUDIT
        {
            set { _FINANCIAL_AUDIT = value; }
            get { return _FINANCIAL_AUDIT; }

        }
        private string _OFFICE_AUDIT;
        public string OFFICE_AUDIT
        {
            set { _OFFICE_AUDIT = value; }
            get { return _OFFICE_AUDIT; }

        }
        private string _FAX;
        public string FAX
        {
            set { _FAX = value; }
            get { return _FAX; }

        }
        private string _QQ;
        public string QQ
        {
            set { _QQ = value; }
            get { return _QQ; }

        }
        private string _ALWW;
        public string ALWW
        {
            set { _ALWW = value; }
            get { return _ALWW; }

        }
        private string _EMAIL;
        public string EMAIL
        {
            set { _EMAIL = value; }
            get { return _EMAIL; }

        }
        private string _DEPART;
        public string DEPART
        {
            set { _DEPART = value; }
            get { return _DEPART; }

        }
        private string _SUID;
        public string SUID
        {
            set { _SUID = value; }
            get { return _SUID; }

        }
        private string _PAYMENT_CLAUSE;
        public string PAYMENT_CLAUSE
        {
            set { _PAYMENT_CLAUSE = value; }
            get { return _PAYMENT_CLAUSE; }

        }
        private string _SUPPLIER_ID;
        public string SUPPLIER_ID
        {
            set { _SUPPLIER_ID = value; }
            get { return _SUPPLIER_ID; }

        }
        private string _SNAME;
        public string SNAME
        {
            set { _SNAME = value; }
            get { return _SNAME; }

        }
        private string _AUDIT_STYLE;
        public string AUDIT_STYLE
        {
            set { _AUDIT_STYLE = value; }
            get { return _AUDIT_STYLE; }

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
        private string _POSTCODE;
        public string POSTCODE
        {
            set { _POSTCODE = value; }
            get { return _POSTCODE; }

        }
        private string _ADDRESS;
        public string ADDRESS
        {
            set { _ADDRESS = value; }
            get { return _ADDRESS; }

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
        private string _SUKEY;
        public string SUKEY
        {
            set { _SUKEY = value; }
            get { return _SUKEY; }

        }
        private  bool _IFExecutionSUCCESS;
        public  bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private string _PAYMENT;
        public string PAYMENT
        {
            set { _PAYMENT = value; }
            get { return _PAYMENT; }

        }

        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }

        }
        private string _THE_DEFAULT;
        public string THE_DEFAULT
        {
            set { _THE_DEFAULT = value; }
            get { return _THE_DEFAULT; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _USER_DEFINED_ONE;
        public string USER_DEFINED_ONE
        {
            set { _USER_DEFINED_ONE = value; }
            get { return _USER_DEFINED_ONE; }

        }
        private string _USER_DEFINED_TWO;
        public string USER_DEFINED_TWO
        {
            set { _USER_DEFINED_TWO = value; }
            get { return _USER_DEFINED_TWO; }

        }
        private string _USER_DEFINED_THREE;
        public string USER_DEFINED_THREE
        {
            set { _USER_DEFINED_THREE = value; }
            get { return _USER_DEFINED_THREE; }

        }
        private string _USER_DEFINED_FOUR;
        public string USER_DEFINED_FOUR
        {
            set { _USER_DEFINED_FOUR = value; }
            get { return _USER_DEFINED_FOUR; }

        }
        private string _USER_DEFINED_FIVE;
        public string USER_DEFINED_FIVE
        {
            set { _USER_DEFINED_FIVE = value; }
            get { return _USER_DEFINED_FIVE; }

        }
        private string _USER_DEFINED_SIX;
        public string USER_DEFINED_SIX
        {
            set { _USER_DEFINED_SIX = value; }
            get { return _USER_DEFINED_SIX; }

        }
        private string _USER_DEFINED_SEVEN;
        public string USER_DEFINED_SEVEN
        {
            set { _USER_DEFINED_SEVEN = value; }
            get { return _USER_DEFINED_SEVEN; }

        }
        private string _USER_DEFINED_EIGHT;
        public string USER_DEFINED_EIGHT
        {
            set { _USER_DEFINED_EIGHT = value; }
            get { return _USER_DEFINED_EIGHT; }

        }
        private string _USER_DEFINED_NINE;
        public string USER_DEFINED_NINE
        {
            set { _USER_DEFINED_NINE = value; }
            get { return _USER_DEFINED_NINE; }

        }
        private string _USER_DEFINED_TEN;
        public string USER_DEFINED_TEN
        {
            set { _USER_DEFINED_TEN = value; }
            get { return _USER_DEFINED_TEN; }

        }
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }

        }
        #endregion
        DataTable dt = new DataTable();
        #region sql
        string setsql = @"
SELECT 
B.SUID AS 供应商编号,
B.SUPPLIER_ID AS 供应商代码,
B.SNAME AS 供应商名称,
B.PAYMENT AS 收款方式,
B.PAYMENT_CLAUSE AS 收款条件,
A.SN AS 项次,
CASE WHEN A.THE_DEFAULT='Y' THEN '是'
ELSE ''
END 
AS 默认联系人,
A.CONTACT AS 联系人,
A.PHONE AS 联系电话,
A.QQ AS QQ号,
A.ALWW AS 旺旺号,
A.FAX AS 传真号码,
A.POSTCODE AS 邮政编码,
A.EMAIL AS EMAIL,
A.ADDRESS AS 公司地址,
A.DEPART AS 部门,
B.USER_DEFINED_ONE AS 定义1,
B.USER_DEFINED_TWO AS 定义2,
B.USER_DEFINED_THREE AS 定义3,
B.USER_DEFINED_FOUR AS 定义4,
B.USER_DEFINED_FIVE AS 定义5,
B.USER_DEFINED_SIX AS 定义6,
B.USER_DEFINED_SEVEN AS 定义7,
B.USER_DEFINED_EIGHT AS 定义8,
B.USER_DEFINED_NINE AS 定义9,
B.USER_DEFINED_TEN AS 定义10,
B.WATER_MARK_CONTENT AS 水印内容,
B.REMARK AS 备注,
CASE WHEN B.SALE_AUDIT='Y' THEN '是'
ELSE ''
END AS 是否需业务审核,
CASE WHEN B.FINANCIAL_AUDIT='Y' THEN '是'
ELSE ''
END AS 是否需财务审核,
CASE WHEN B.OFFICE_AUDIT='Y' THEN '是'
ELSE ''
END AS 是否需文员审核
FROM SUPPLIERINFO_DET A 
LEFT JOIN SUPPLIERINFO_MST B ON A.SUID=B.SUID

";

        string setsqlo = @"
INSERT INTO SUPPLIERINFO_DET
(
SUKEY,
SUID,
SN,
CONTACT,
THE_DEFAULT,
PHONE,
QQ,
ALWW,
FAX,
POSTCODE,
EMAIL,
ADDRESS,
DEPART,
MAKERID,
DATE,
YEAR,
MONTH,
DAY
)
VALUES
(
@SUKEY,
@SUID,
@SN,
@CONTACT,
@THE_DEFAULT,
@PHONE,
@QQ,
@ALWW,
@FAX,
@POSTCODE,
@EMAIL,
@ADDRESS,
@DEPART,
@MAKERID,
@DATE,
@YEAR,
@MONTH,
@DAY

)


";

        string setsqlt = @"

INSERT INTO SUPPLIERINFO_MST
(
SUID,
SNAME,
AUDIT_STYLE,
SUKEY,
DATE,
MAKERID,
YEAR,
MONTH,
DAY,
PAYMENT,
PAYMENT_CLAUSE,
SUPPLIER_ID,
USER_DEFINED_ONE,
USER_DEFINED_TWO,
USER_DEFINED_THREE,
USER_DEFINED_FOUR,
USER_DEFINED_FIVE,
USER_DEFINED_SIX,
USER_DEFINED_SEVEN,
USER_DEFINED_EIGHT,
USER_DEFINED_NINE,
USER_DEFINED_TEN,
WATER_MARK_CONTENT,
REMARK,
SALE_AUDIT,
FINANCIAL_AUDIT,
OFFICE_AUDIT

)
VALUES
(
@SUID,
@SNAME,
@AUDIT_STYLE,
@SUKEY,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY,
@PAYMENT,
@PAYMENT_CLAUSE,
@SUPPLIER_ID,
@USER_DEFINED_ONE,
@USER_DEFINED_TWO,
@USER_DEFINED_THREE,
@USER_DEFINED_FOUR,
@USER_DEFINED_FIVE,
@USER_DEFINED_SIX,
@USER_DEFINED_SEVEN,
@USER_DEFINED_EIGHT,
@USER_DEFINED_NINE,
@USER_DEFINED_TEN,
@WATER_MARK_CONTENT,
@REMARK,
@SALE_AUDIT,
@FINANCIAL_AUDIT,
@OFFICE_AUDIT
)
";
        string setsqlth = @"
UPDATE SUPPLIERINFO_MST SET 
SNAME=@SNAME,
AUDIT_STYLE=@AUDIT_STYLE,
SUKEY=@SUKEY,
DATE=@DATE,
MAKERID=@MAKERID,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY,
PAYMENT=@PAYMENT,
PAYMENT_CLAUSE=@PAYMENT_CLAUSE,
SUPPLIER_ID=@SUPPLIER_ID,
USER_DEFINED_ONE=@USER_DEFINED_ONE,
USER_DEFINED_TWO=@USER_DEFINED_TWO,
USER_DEFINED_THREE=@USER_DEFINED_THREE,
USER_DEFINED_FOUR=@USER_DEFINED_FOUR,
USER_DEFINED_FIVE=@USER_DEFINED_FIVE,
USER_DEFINED_SIX=@USER_DEFINED_SIX,
USER_DEFINED_SEVEN=@USER_DEFINED_SEVEN,
USER_DEFINED_EIGHT=@USER_DEFINED_EIGHT,
USER_DEFINED_NINE=@USER_DEFINED_NINE,
USER_DEFINED_TEN=@USER_DEFINED_TEN,
WATER_MARK_CONTENT=@WATER_MARK_CONTENT,
REMARK=@REMARK,
SALE_AUDIT=@SALE_AUDIT,
FINANCIAL_AUDIT=@FINANCIAL_AUDIT,
OFFICE_AUDIT=@OFFICE_AUDIT
";

        string setsqlf = @"


)
";
        string setsqlfi = @"

";
        string setsqlsi = @"

)
";
        #endregion
        public CSUPPLIER_INFO()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            //GETID =bc.numYM(10, 4, "0001", "SELECT * FROM WORKORDER_PICKING_MST", "WPID", "WP");

            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
            sqlsi = setsqlsi;
        }
        #region GetTableInfo
        public DataTable GetTableInfo()
        {
            dt = new DataTable();
            dt.Columns.Add("项次", typeof(string));
            dt.Columns.Add("默认联系人",typeof (bool ));
            dt.Columns.Add("联系人", typeof(string));
            dt.Columns.Add("联系电话", typeof(string));
            dt.Columns.Add("QQ号", typeof(string));
            dt.Columns.Add("旺旺号", typeof(string));
            dt.Columns.Add("传真号码", typeof(string));
            dt.Columns.Add("邮政编码", typeof(string));
            dt.Columns.Add("EMAIL", typeof(string));
            dt.Columns.Add("公司地址", typeof(string));
            dt.Columns.Add("部门", typeof(string));
            return dt;
        }
        #endregion
        public string GETID()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            string v1 = bc.numYM(10, 4, "0001", "select * from SUPPLIERINFO_MST", "SUID", "SU");
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
            string GET_SNAME = bc.getOnlyString("SELECT SNAME FROM SUPPLIERINFO_MST WHERE  SUID='" + SUID + "'");
            string GET_SUKEY = bc.getOnlyString("SELECT SUKEY FROM SUPPLIERINFO_MST WHERE SUID='" + SUID + "'");
            string GET_SUPPLIER_ID = bc.getOnlyString("SELECT SUPPLIER_ID FROM SUPPLIERINFO_MST WHERE SUID='" + SUID + "'");
          
            if (!bc.exists("SELECT SUID FROM SUPPLIERINFO_DET WHERE SUID='" + SUID + "'"))
            {
                if (SUPPLIER_ID != "" && bc.exists("SELECT * FROM SUPPLIERINFO_MST where SUPPLIER_ID='" + SUPPLIER_ID + "'"))
                {

                    ErrowInfo = "该供应商代码已经存在了！";
                    IFExecution_SUCCESS = false;

                }
                else if (bc.exists("SELECT * FROM SUPPLIERINFO_MST WHERE SNAME='" + SNAME + "'"))
                {

                    ErrowInfo = "该供应商名称已经存在了！";
                    IFExecution_SUCCESS = false;
                }
                else
                {
                    ACTION_DET(dt);
                    SQlcommandE_MST(sqlt);
                    UPDATE_THE_DEFAULT();
                    IFExecution_SUCCESS = true;

                }
            }
            else if (SUPPLIER_ID != "" && GET_SUPPLIER_ID != SUPPLIER_ID)
            {
               
              
                if (bc.exists("SELECT * FROM SUPPLIERINFO_MST where SUPPLIER_ID='" + SUPPLIER_ID + "'"))
                {
                   
                    ErrowInfo = "该供应商代码已经存在了！";
                    IFExecution_SUCCESS = false;

                }
                else
                {
                    ACTION_DET(dt);
                    SQlcommandE_MST(sqlth + " WHERE SUID='" + SUID + "'");
                    UPDATE_THE_DEFAULT();
                    IFExecution_SUCCESS = true;
                }


            }
            else if (GET_SNAME != SNAME)
            {
                if (SNAME != "" && bc.exists("SELECT * FROM SUPPLIERINFO_MST WHERE SNAME='" + SNAME + "'"))
                {

                    ErrowInfo = "该供应商名称已经存在了！";
                    IFExecution_SUCCESS = false;
                }
                else
                {
                    ACTION_DET(dt);
                    SQlcommandE_MST(sqlth + " WHERE SUID='" + SUID + "'");
                    UPDATE_THE_DEFAULT();
                    IFExecution_SUCCESS = true;
                }

            }
            else
            {
                ACTION_DET(dt);
                SQlcommandE_MST(sqlth + " WHERE SUID='" + SUID + "'");
                UPDATE_THE_DEFAULT();
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
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace ("-","/");
        
            SqlConnection sqlcon = bc.getcon();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
            sqlcom.Parameters.Add("@SUKEY", SqlDbType.VarChar, 20).Value = SUKEY;
            sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = SN;
            sqlcom.Parameters.Add("@SUID", SqlDbType.VarChar, 20).Value = SUID;
            sqlcom.Parameters.Add("@CONTACT", SqlDbType.VarChar, 20).Value = CONTACT;
            sqlcom.Parameters.Add("@THE_DEFAULT", SqlDbType.VarChar, 20).Value = THE_DEFAULT;
            sqlcom.Parameters.Add("@PHONE", SqlDbType.VarChar, 20).Value = PHONE;
            sqlcom.Parameters.Add("@FAX", SqlDbType.VarChar, 20).Value = FAX;
            sqlcom.Parameters.Add("@POSTCODE", SqlDbType.VarChar, 20).Value = POSTCODE;
            sqlcom.Parameters.Add("@EMAIL", SqlDbType.VarChar, 20).Value = EMAIL;
            sqlcom.Parameters.Add("@ADDRESS", SqlDbType.VarChar, 20).Value = ADDRESS;
            sqlcom.Parameters.Add("@DEPART", SqlDbType.VarChar, 20).Value = DEPART;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = EMID;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.Parameters.Add("@QQ", SqlDbType.VarChar, 20).Value = QQ;
            sqlcom.Parameters.Add("@ALWW", SqlDbType.VarChar, 20).Value = ALWW;
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
            sqlcom.Parameters.Add("@SUID", SqlDbType.VarChar, 20).Value = SUID;
            sqlcom.Parameters.Add("@SNAME", SqlDbType.VarChar, 20).Value = SNAME;
            sqlcom.Parameters.Add("@AUDIT_STYLE", SqlDbType.VarChar, 20).Value = AUDIT_STYLE;
            sqlcom.Parameters.Add("@SUKEY", SqlDbType.VarChar, 20).Value = SUKEY;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = EMID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.Parameters.Add("@PAYMENT", SqlDbType.VarChar, 20).Value = PAYMENT;
            sqlcom.Parameters.Add("@PAYMENT_CLAUSE", SqlDbType.VarChar, 20).Value = PAYMENT_CLAUSE;
            sqlcom.Parameters.Add("@SUPPLIER_ID", SqlDbType.VarChar, 20).Value = SUPPLIER_ID;
            sqlcom.Parameters.Add("@USER_DEFINED_ONE", SqlDbType.VarChar, 20).Value = USER_DEFINED_ONE;
            sqlcom.Parameters.Add("@USER_DEFINED_TWO", SqlDbType.VarChar, 20).Value = USER_DEFINED_TWO;
            sqlcom.Parameters.Add("@USER_DEFINED_THREE", SqlDbType.VarChar, 20).Value = USER_DEFINED_THREE;
            sqlcom.Parameters.Add("@USER_DEFINED_FOUR", SqlDbType.VarChar, 20).Value = USER_DEFINED_FOUR;
            sqlcom.Parameters.Add("@USER_DEFINED_FIVE", SqlDbType.VarChar, 20).Value = USER_DEFINED_FIVE;
            sqlcom.Parameters.Add("@USER_DEFINED_SIX", SqlDbType.VarChar, 20).Value = USER_DEFINED_SIX;
            sqlcom.Parameters.Add("@USER_DEFINED_SEVEN", SqlDbType.VarChar, 20).Value = USER_DEFINED_SEVEN;
            sqlcom.Parameters.Add("@USER_DEFINED_EIGHT", SqlDbType.VarChar, 20).Value = USER_DEFINED_EIGHT;
            sqlcom.Parameters.Add("@USER_DEFINED_NINE", SqlDbType.VarChar, 20).Value = USER_DEFINED_NINE;
            sqlcom.Parameters.Add("@USER_DEFINED_TEN", SqlDbType.VarChar, 20).Value = USER_DEFINED_TEN;
            sqlcom.Parameters.Add("@WATER_MARK_CONTENT", SqlDbType.VarChar, 1000).Value = WATER_MARK_CONTENT;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 1000).Value = REMARK;
            sqlcom.Parameters.Add("@SALE_AUDIT", SqlDbType.VarChar, 20).Value = SALE_AUDIT;
            sqlcom.Parameters.Add("@FINANCIAL_AUDIT", SqlDbType.VarChar, 20).Value = FINANCIAL_AUDIT;
            sqlcom.Parameters.Add("@OFFICE_AUDIT", SqlDbType.VarChar, 20).Value = OFFICE_AUDIT;
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        private void ACTION_DET(DataTable dt)
        {
           
            basec.getcoms("DELETE SUPPLIERINFO_DET WHERE SUID='" + SUID + "'");
            foreach (DataRow dr in dt.Rows)
            {

                SUKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM SUPPLIERINFO_DET", "SUKEY", "CU");
                CONTACT = dr["联系人"].ToString();
                PHONE = dr["联系电话"].ToString();
                FAX = dr["传真号码"].ToString();
                POSTCODE = dr["邮政编码"].ToString();
                EMAIL = dr["EMAIL"].ToString();
                ADDRESS = dr["公司地址"].ToString();
                DEPART = dr["部门"].ToString();
                SN = dr["项次"].ToString();
                QQ = dr["QQ号"].ToString();
                ALWW  = dr["旺旺号"].ToString();
                if (dr["默认联系人"].ToString() == "True")
                {
                    THE_DEFAULT = "Y";
                }
                else
                {
                    THE_DEFAULT = "N";
                }
                SQlcommandE_DET(sqlo);
            }


        }
        private void UPDATE_THE_DEFAULT()
        {
            basec.getcoms("UPDATE SUPPLIERINFO_MST SET SUKEY=(SELECT SUKEY FROM SUPPLIERINFO_DET WHERE THE_DEFAULT='Y' AND SUID='"+SUID 
                +"') WHERE SUID='"+SUID +"'");


        }
    }
}
