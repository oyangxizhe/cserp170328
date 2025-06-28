using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;
using XizheC;

namespace XizheC
{
    public class CMOLD_BASE:IGETID 
    {
        basec bc = new basec();
        #region nature
        private string _MBID;
        public string MBID
        {
            set { _MBID = value; }
            get { return _MBID; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {
            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }
        }
        private string _EMID;
        public string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private string _ENAME;
        public string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }

        }
        private string _MAID;
        public string MAID
        {
            set { _MAID = value; }
            get { return _MAID; }
        }
        private string _CUID;
        public string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }
        }
        private string _WEIGHT;
        public string WEIGHT
        {
            set { _WEIGHT = value; }
            get { return _WEIGHT; }
        }
        private string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }

        }
        private string _CNAME;
        public string CNAME
        {
            set { _CNAME = value; }
            get { return _CNAME; }

        }
        private string _MATERIAL;
        public string MATERIAL
        {
            set { _MATERIAL = value; }
            get { return _MATERIAL; }

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
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
       #endregion
        DataTable dt = new DataTable();

        string setsql = @"
SELECT 
A.MBID AS 编号,
B.CNAME AS 客户名称,
A.WAREID AS 型号,
C.MATERIAL AS 材料,
A.WEIGHT AS 重量
FROM MOLD_BASE A 
LEFT JOIN CUSTOMERINFO_MST B ON A.CUID=B.CUID
LEFT JOIN MATERIAL C ON A.MAID=C.MAID

";
        string setsqlo = @"
INSERT INTO MOLD_BASE
(
MBID,
CUID,
WAREID,
MAID,
WEIGHT,
MAKERID,
DATE,
YEAR,
MONTH
)
VALUES
(
@MBID,
@CUID,
@WAREID,
@MAID,
@WEIGHT,
@MAKERID,
@DATE,
@YEAR,
@MONTH
)
";



        string setsqlt = @"
UPDATE MOLD_BASE SET
CUID=@CUID,
WAREID=@WAREID,
MAID=@MAID,
WEIGHT=@WEIGHT,
MAKERID=@MAKERID,
DATE=@DATE,
YEAR=@YEAR

";
        string setsqlth = @"

";

        public CMOLD_BASE()
        {
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
       
        }
        public string GETID()
        {
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM MOLD_BASE", "MBID", "MB");
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
            CUID = bc.getOnlyString("SELECT CUID FROM CUSTOMERINFO_MST WHERE CNAME='" + CNAME  + "'");
            MAID = bc.getOnlyString("SELECT MAID FROM MATERIAL WHERE MATERIAL='" + MATERIAL  + "'");
            string get_CUID = bc.getOnlyString("SELECT CUID FROM MOLD_BASE WHERE MBID='" +MBID  + "'");
            string get_MAID = bc.getOnlyString("SELECT MAID FROM MOLD_BASE WHERE MBID='" + MBID  + "'");
            if (!bc.exists("SELECT MBID FROM MOLD_BASE WHERE MBID='" + MBID  + "'"))
            {
                if (bc.exists("SELECT MBID FROM MOLD_BASE WHERE CUID='"+CUID +"' AND MAID='"+MAID +"'"))
                {
                    ErrowInfo = string.Format("客户名称：{0} + 材料：{1} 已经存在系统中了1",CNAME ,MATERIAL);
                    IFExecution_SUCCESS = false;
                }
                else
                {
                    SQlcommandE_MST(sqlo);
                    IFExecution_SUCCESS = true;
                }
            }
            else if (CUID != get_CUID || MAID != get_MAID)
            {
             if (bc.exists("SELECT MBID FROM MOLD_BASE WHERE CUID='" + CUID + "' AND MAID='" + MAID + "'"))
              {

                ErrowInfo = string.Format("客户名称：{0} + 材料：{1} 已经存在系统中了2", CNAME, MATERIAL);
                IFExecution_SUCCESS = false;

               }
               else
               {
                SQlcommandE_MST(sqlt + " WHERE MBID='" + MBID + "'");
                IFExecution_SUCCESS = true;
                }

            }
 
            else
            {
                SQlcommandE_MST(sqlt + " WHERE MBID='" + MBID + "'");
                IFExecution_SUCCESS = true;
            }
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
            sqlcom.Parameters.Add("@MBID", SqlDbType.VarChar, 20).Value = MBID;
            sqlcom.Parameters.Add("@CUID", SqlDbType.VarChar, 20).Value = CUID;
            sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = WAREID;
            sqlcom.Parameters.Add("@MAID", SqlDbType.VarChar, 20).Value = MAID;
            sqlcom.Parameters.Add("@WEIGHT", SqlDbType.VarChar, 20).Value =WEIGHT;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = EMID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
    }
}
