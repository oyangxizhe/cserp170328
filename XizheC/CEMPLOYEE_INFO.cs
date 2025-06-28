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
    public class CEMPLOYEE_INFO
    {
        basec bc = new basec();
        #region nature
        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }

        }
        private string _EMID;
        public string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private string _EMPLOYEE_ID;
        public string EMPLOYEE_ID
        {
            set { _EMPLOYEE_ID = value; }
            get { return _EMPLOYEE_ID; }

        }
        private string _ENAME;
        public  string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }

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


        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }

        #endregion
        DataTable dt = new DataTable();
        string setsql = @"

SELECT 
A.EMPLOYEE_ID AS 员工工号,
A.ENAME AS 员工姓名,
A.DEPART AS 部门,
A.POSITION AS 职务,
A.PHONE AS 电话,
A.SAMPLE_CODE 简码,
(SELECT ENAME FROM EMPLOYEEINFO 
WHERE EMID=A.MAKERID ) AS 制单人,
A.DATE AS 制单日期
FROM
EMPLOYEEINFO A 

";
        public CEMPLOYEE_INFO()
        {
            sql = setsql;
    
        }

        public string  GETID()
        {
            string v1 = bc.numYM(7, 3, "001", "SELECT * FROM EMPLOYEEINFO", "EMID", "");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
        #region emptydatatable_T
        public DataTable emptydatatable_T()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", typeof(string));
            dt.Columns.Add("员工工号", typeof(string));
            dt.Columns.Add("员工姓名", typeof(string));
            dt.Columns.Add("部门", typeof(string));
            dt.Columns.Add("职务", typeof(string));
            dt.Columns.Add("电话", typeof(string));
            dt.Columns.Add("简码", typeof(string));
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
                dr["序号"] = i.ToString();
                dr["员工工号"] = dr1["员工工号"].ToString();
                dr["员工姓名"] = dr1["员工姓名"].ToString();
                dr["部门"] = dr1["部门"].ToString();
                dr["职务"] = dr1["职务"].ToString();
                dr["电话"] = dr1["电话"].ToString();
                dr["简码"] = dr1["简码"].ToString();
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
