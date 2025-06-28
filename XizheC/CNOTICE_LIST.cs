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
    public class CNOTICE_LIST
    {
        basec bc = new basec();
        #region nature
        private string _USID;
        public string USID
        {
            set { _USID = value; }
            get { return _USID; }

        }
        private string _sql;
        public string sql
        {
            set { _sql = value; }
            get { return _sql; }

        }
        private string _UNAME;
        public string UNAME
        {
            set { _UNAME = value; }
            get { return _UNAME; }

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
        private string _NOTICE_LIST;
        public string NOTICE_LIST
        {
            set { _NOTICE_LIST = value; }
            get { return _NOTICE_LIST; }

        }
        #endregion
        DataTable dt = new DataTable();
        string setsql = @"

SELECT 
A.NLID AS 编号,
B.EMPLOYEE_ID AS 员工工号,
B.ENAME AS 员工姓名,
(SELECT ENAME FROM EMPLOYEEINFO 
WHERE EMID=A.MAKERID ) AS 制单人,
A.DATE AS 制单日期
FROM
NOTICE_LIST A 
LEFT JOIN EMPLOYEEINFO B ON A.EMID=B.EMID

";
        public CNOTICE_LIST()
        {
            sql = setsql;
        }
        public string GETID()
        {
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM NOTICE_LIST", "NLID", "NL");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
    
    }
}
