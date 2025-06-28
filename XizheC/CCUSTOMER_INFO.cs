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
    public class CCUSTOMER_INFO
    {
        basec bc = new basec();

        #region nature
        private string _EMID;
        public string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private string _BRAND;
        public string BRAND
        {
            set { _BRAND = value; }
            get { return _BRAND; }

        }
        private string _CUSTOMER_TYPE;
        public string CUSTOMER_TYPE
        {
            set { _CUSTOMER_TYPE = value; }
            get { return _CUSTOMER_TYPE; }

        }
        private string _TEL;
        public string TEL
        {
            set { _TEL = value; }
            get { return _TEL; }

        }
        private string _RMKEY;
        public string RMKEY
        {
            set { _RMKEY = value; }
            get { return _RMKEY; }

        }
        private string _ADDRESS;
        public string ADDRESS
        {
            set { _ADDRESS = value; }
            get { return _ADDRESS; }
        }
        private string _CONTACT;
        public string CONTACT
        {
            set { _CONTACT = value; }
            get { return _CONTACT; }
        }
        private string _PHONE;
        public string PHONE
        {
            set { _PHONE = value; }
            get { return _PHONE; }
        }
        private string _MAID;
        public string MAID
        {
            set { _MAID = value; }
            get { return _MAID; }
        }
        private string _BASE;
        public string BASE
        {
            set { _BASE = value; }
            get { return _BASE; }
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
        private string _CUID;
        public string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }
        }
        private string _PAYMENT_CLAUSE;
        public string PAYMENT_CLAUSE
        {
            set { _PAYMENT_CLAUSE = value; }
            get { return _PAYMENT_CLAUSE; }

        }
        private string _CUSTOMER_ID;
        public string CUSTOMER_ID
        {
            set { _CUSTOMER_ID = value; }
            get { return _CUSTOMER_ID; }
        }
        private string _CNAME;
        public string CNAME
        {
            set { _CNAME = value; }
            get { return _CNAME; }
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
        private string _CUKEY;
        public string CUKEY
        {
            set { _CUKEY = value; }
            get { return _CUKEY; }

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
        private string _PROVINCE;
        public string PROVINCE
        {
            set { _PROVINCE = value; }
            get { return _PROVINCE; }

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
CAST(0   as   bit)   as   复选框,
B.CUID AS 客户编号,
B.CUSTOMER_ID AS 客户代码,
B.CNAME AS 客户名称,
B.PAYMENT AS 付款方式,
B.PAYMENT_CLAUSE AS 付款条件,
A.CUSTOMER_TYPE AS 客户类别,
A.SN AS 项次,
CASE WHEN A.THE_DEFAULT='Y' THEN '是'
ELSE ''
END 
AS 默认联系人,
A.CONTACT AS 联系人,
C.MATERIAL AS 材料,
A.BASE AS 基数,
A.PHONE AS 联系电话,
A.QQ AS QQ号,
A.TEL AS 手机号码,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=B.MAKERID) AS 制单人,
B.DATE AS 制单日期,
A.FAX AS 传真号码,
A.POSTCODE AS 邮政编码,
A.EMAIL AS EMAIL,
A.ADDRESS AS 公司地址,
A.BRAND AS 品牌,
A.CUSTOMER_TYPE AS 客户类别,
A.DEPART AS 部门,
B.PROVINCE AS 省份,
A.REMARK AS 备注
FROM CUSTOMERINFO_DET A 
LEFT JOIN CUSTOMERINFO_MST B ON A.CUID=B.CUID
LEFT JOIN MATERIAL C ON A.MAID=C.MAID

";


        string setsqlo = @"
INSERT INTO CUSTOMERINFO_DET
(
CUKEY,
CUID,
SN,
CONTACT,
MAID,
THE_DEFAULT,
PHONE,
TEL,
QQ,
FAX,
POSTCODE,
EMAIL,
ADDRESS,
BRAND,
CUSTOMER_TYPE,
DEPART,
BASE,
REMARK,
MAKERID,
DATE,
YEAR,
MONTH,
DAY
)
VALUES
(
@CUKEY,
@CUID,
@SN,
@CONTACT,
@MAID,
@THE_DEFAULT,
@PHONE,
@TEL,
@QQ,
@FAX,
@POSTCODE,
@EMAIL,
@ADDRESS,
@BRAND,
@CUSTOMER_TYPE,
@DEPART,
@BASE,
@REMARK,
@MAKERID,
@DATE,
@YEAR,
@MONTH,
@DAY

)


";

        string setsqlt = @"

INSERT INTO CUSTOMERINFO_MST
(
CUID,
CNAME,
CUKEY,
DATE,
MAKERID,
YEAR,
MONTH,
DAY,
PAYMENT,
PAYMENT_CLAUSE,
CUSTOMER_ID,
PROVINCE
)
VALUES
(
@CUID,
@CNAME,
@CUKEY,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY,
@PAYMENT,
@PAYMENT_CLAUSE,
@CUSTOMER_ID,
@PROVINCE
)
";
        string setsqlth = @"
UPDATE CUSTOMERINFO_MST SET 
CNAME=@CNAME,
CUKEY=@CUKEY,
DATE=@DATE,
YEAR=@YEAR,
MONTH=@MONTH,
DAY=@DAY,
PAYMENT=@PAYMENT,
PAYMENT_CLAUSE=@PAYMENT_CLAUSE,
CUSTOMER_ID=@CUSTOMER_ID,
PROVINCE=@PROVINCE,
REMARK=@REMARK
";

        string setsqlf = @"

";
        string setsqlfi = @"

";
        string setsqlsi = @"
SELECT 
B.CUID AS 客户编号,
B.CNAME AS 客户名称,
A.DEPART AS 部门,
A.CONTACT AS 联系人,
C.MATERIAL AS 材料,
A.BASE AS 基数,
A.PHONE AS 联系电话,
A.TEL AS 手机号码,
A.FAX AS 传真,
A.QQ AS QQ号码,
A.EMAIL AS EMAIL,
A.BRAND AS 品牌,
A.CUSTOMER_TYPE AS 客户类别,
B.PROVINCE AS 省份,
B.CUSTOMER_ID AS 客户代码,
A.REMARK AS 备注,
(SELECT ENAME FROM EMPLOYEEINFO WHERE EMID=B.MAKERID) AS 制单人,
B.DATE AS 制单日期
FROM CUSTOMERINFO_DET A 
LEFT JOIN CUSTOMERINFO_MST B ON A.CUID=B.CUID
LEFT JOIN MATERIAL C ON A.MAID=C.MAID

";
        #endregion
        public CCUSTOMER_INFO()
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
            dt.Columns.Add("材料", typeof(string));
            dt.Columns.Add("基数", typeof(string));
            dt.Columns.Add("联系电话", typeof(string));
            dt.Columns.Add("手机号码", typeof(string));
            dt.Columns.Add("QQ号", typeof(string));
            dt.Columns.Add("传真号码", typeof(string));
            dt.Columns.Add("邮政编码", typeof(string));
            dt.Columns.Add("EMAIL", typeof(string));
            dt.Columns.Add("公司地址", typeof(string));
            dt.Columns.Add("品牌", typeof(string));
            dt.Columns.Add("客户类别", typeof(string));
            dt.Columns.Add("部门", typeof(string));
            dt.Columns.Add("备注", typeof(string));
            return dt;
        }
        #endregion
        public string GETID()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            string v1 = bc.numYM(10, 4, "0001", "select * from CUSTOMERINFO_MST", "CUID", "CU");
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
            string GET_CNAME = bc.getOnlyString("SELECT CNAME FROM CUSTOMERINFO_MST WHERE  CUID='" + CUID + "'");
            string GET_CUKEY = bc.getOnlyString("SELECT CUKEY FROM CUSTOMERINFO_MST WHERE CUID='" + CUID + "'");
            string GET_CUSTOMER_ID = bc.getOnlyString("SELECT CUSTOMER_ID FROM CUSTOMERINFO_MST WHERE CUID='" + CUID + "'");
          
            if (!bc.exists("SELECT CUID FROM CUSTOMERINFO_DET WHERE CUID='" + CUID + "'"))
            {
                if (CUSTOMER_ID != "" && bc.exists("SELECT * FROM CUSTOMERINFO_MST where CUSTOMER_ID='" + CUSTOMER_ID + "'"))
                {

                    ErrowInfo = "该客户代码已经存在了！";
                    IFExecution_SUCCESS = false;

                }
                else if (bc.exists("SELECT * FROM CUSTOMERINFO_MST WHERE CNAME='" + CNAME + "'"))
                {

                    ErrowInfo = "该客户名称已经存在了！";
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
            else if (CUSTOMER_ID != "" && GET_CUSTOMER_ID != CUSTOMER_ID)
            {
               
              
                if (bc.exists("SELECT * FROM CUSTOMERINFO_MST where CUSTOMER_ID='" + CUSTOMER_ID + "'"))
                {
                   
                    ErrowInfo = "该客户代码已经存在了！";
                    IFExecution_SUCCESS = false;

                }
                else
                {
                    ACTION_DET(dt);
                    SQlcommandE_MST(sqlth + " WHERE CUID='" + CUID + "'");
                    UPDATE_THE_DEFAULT();
                    IFExecution_SUCCESS = true;
                }


            }
            else if (GET_CNAME != CNAME)
            {
                if (CNAME != "" && bc.exists("SELECT * FROM CUSTOMERINFO_MST WHERE CNAME='" + CNAME + "'"))
                {

                    ErrowInfo = "该客户名称已经存在了！";
                    IFExecution_SUCCESS = false;
                }
                else
                {
                    ACTION_DET(dt);
                    SQlcommandE_MST(sqlth + " WHERE CUID='" + CUID + "'");
                    UPDATE_THE_DEFAULT();
                    IFExecution_SUCCESS = true;
                }

            }
            else
            {
                ACTION_DET(dt);
                SQlcommandE_MST(sqlth + " WHERE CUID='" + CUID + "'");
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
            sqlcom.Parameters.Add("@CUKEY", SqlDbType.VarChar, 20).Value = CUKEY;
            sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = SN;
            sqlcom.Parameters.Add("@CUID", SqlDbType.VarChar, 20).Value = CUID;
            sqlcom.Parameters.Add("@CONTACT", SqlDbType.VarChar, 20).Value = CONTACT;
            sqlcom.Parameters.Add("@MAID", SqlDbType.VarChar, 20).Value = MAID;
            sqlcom.Parameters.Add("@BASE", SqlDbType.VarChar, 20).Value =BASE;
            sqlcom.Parameters.Add("@THE_DEFAULT", SqlDbType.VarChar, 20).Value = THE_DEFAULT;
            sqlcom.Parameters.Add("@PHONE", SqlDbType.VarChar, 20).Value = PHONE;
            sqlcom.Parameters.Add("@TEL", SqlDbType.VarChar, 20).Value = TEL;
            sqlcom.Parameters.Add("@FAX", SqlDbType.VarChar, 20).Value = FAX;
            sqlcom.Parameters.Add("@POSTCODE", SqlDbType.VarChar, 20).Value = POSTCODE;
            sqlcom.Parameters.Add("@EMAIL", SqlDbType.VarChar, 20).Value = EMAIL;
            sqlcom.Parameters.Add("@ADDRESS", SqlDbType.VarChar, 100).Value = ADDRESS;
            sqlcom.Parameters.Add("@BRAND", SqlDbType.VarChar, 20).Value = BRAND ;
            sqlcom.Parameters.Add("@CUSTOMER_TYPE", SqlDbType.VarChar, 20).Value = CUSTOMER_TYPE;
            sqlcom.Parameters.Add("@DEPART", SqlDbType.VarChar, 20).Value = DEPART;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 20).Value = REMARK;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = EMID;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.Parameters.Add("@QQ", SqlDbType.VarChar, 20).Value = QQ;
      
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
            sqlcom.Parameters.Add("@CUID", SqlDbType.VarChar, 20).Value = CUID;
            sqlcom.Parameters.Add("@CNAME", SqlDbType.VarChar, 20).Value = CNAME;
            sqlcom.Parameters.Add("@CUKEY", SqlDbType.VarChar, 20).Value = CUKEY;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = EMID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.Parameters.Add("@PAYMENT", SqlDbType.VarChar, 20).Value = PAYMENT;
            sqlcom.Parameters.Add("@PAYMENT_CLAUSE", SqlDbType.VarChar, 20).Value = PAYMENT_CLAUSE;
            sqlcom.Parameters.Add("@CUSTOMER_ID", SqlDbType.VarChar, 20).Value = CUSTOMER_ID;
            sqlcom.Parameters.Add("@PROVINCE", SqlDbType.VarChar, 20).Value = PROVINCE;
            sqlcom.Parameters.Add("@REMARK", SqlDbType.VarChar, 1000).Value = REMARK;

            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        private void ACTION_DET(DataTable dt)
        {
           
            basec.getcoms("DELETE CUSTOMERINFO_DET WHERE CUID='" + CUID + "'");
            foreach (DataRow dr in dt.Rows)
            {
               
                CUKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM CUSTOMERINFO_DET", "CUKEY", "CU");
                CONTACT = dr["联系人"].ToString();
                MAID = bc.getOnlyString("SELECT MAID FROM MATERIAL WHERE MATERIAL='" + dr["材料"].ToString() + "'");
                BASE = dr["基数"].ToString();
                PHONE = dr["联系电话"].ToString();
                FAX = dr["传真号码"].ToString();
                POSTCODE = dr["邮政编码"].ToString();
                EMAIL = dr["EMAIL"].ToString();
                ADDRESS = dr["公司地址"].ToString();
                BRAND  = dr["品牌"].ToString();
                CUSTOMER_TYPE  = dr["客户类别"].ToString();
                DEPART = dr["部门"].ToString();
                SN = dr["项次"].ToString();
                QQ = dr["QQ号"].ToString();
                REMARK = dr["备注"].ToString();
                TEL = dr["手机号码"].ToString();
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
            basec.getcoms("UPDATE CUSTOMERINFO_MST SET CUKEY=(SELECT CUKEY FROM CUSTOMERINFO_DET WHERE THE_DEFAULT='Y' AND CUID='"+CUID +"') WHERE CUID='"+CUID +"'");


        }
     
    }
}
