using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using XizheC;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using FzBozc;
using FzBozc.Common;
namespace XizheC
{
    public class CINVENTORY
    {
        basec bc = new basec();
        #region nature
        private string _ORID;
        public string ORID
        {
            set { _ORID = value; }
            get { return _ORID; }
        }
        private string _ORKEY;
        public string ORKEY
        {
            set { _ORKEY = value; }
            get { return _ORKEY; }
        }
        private string _SN;
        public string SN
        {
            set { _SN = value; }
            get { return _SN; }
        }
        private string _SEID;
        public string SEID
        {
            set { _SEID = value; }
            get { return _SEID; }
        }
        private string _CUID;
        public string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }
        }
        private string _BARCODE;
        public string BARCODE
        {
            set { _BARCODE = value; }
            get { return _BARCODE; }
        }
        private string _SEKEY;
        public string SEKEY
        {
            set { _SEKEY = value; }
            get { return _SEKEY; }
        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }
        }
        private string _INVENTORY_DATE;
        public string INVENTORY_DATE
        {
            set { _INVENTORY_DATE = value; }
            get { return _INVENTORY_DATE; }
        }
        private string _CUSTOMER_ORID;
        public string CUSTOMER_ORID
        {
            set { _CUSTOMER_ORID = value; }
            get { return _CUSTOMER_ORID; }
        }
        private string _CO_COUNT;
        public string CO_COUNT
        {
            set { _CO_COUNT = value; }
            get { return _CO_COUNT; }

        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }
        }
        private string _SELLDATE;
        public string SELLDATE
        {
            set { _SELLDATE = value; }
            get { return _SELLDATE; }
        }
        private string _SELLERID;
        public string SELLERID
        {
            set { _SELLERID = value; }
            get { return _SELLERID; }
        }
        private string _WO_COUNT;
        public string WO_COUNT
        {
            set { _WO_COUNT = value; }
            get { return _WO_COUNT; }
        }
        private string _STORAGE_COUNT;
        public string STORAGE_COUNT
        {
            set { _STORAGE_COUNT = value; }
            get { return _STORAGE_COUNT; }
        }
        private string _PHONE;
        public string PHONE
        {
            set { _PHONE = value; }
            get { return _PHONE; }
        }
        private string _CONTACT;
        public string CONTACT
        {
            set { _CONTACT = value; }
            get { return _CONTACT; }

        }
        private string _SEND_ADDRESS;
        public string SEND_ADDRESS
        {
            set { _SEND_ADDRESS = value; }
            get { return _SEND_ADDRESS; }

        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }
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
        #endregion
        #region sql
        string setsql = @"


";
        string setsqlo = @"


";

        string setsqlt = @"


";
        string setsqlth = @"

";
        string setsqlf = @"

";
        string setsqlfi = @"

";
#endregion
      
        DataTable dtx2 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt = new DataTable();
        CFileInfo cfileinfo = new CFileInfo();
        CORDER corder = new CORDER();
        public CINVENTORY()
        {
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
        }
        #region GetTableInfo_SEARCH
        public DataTable GetTableInfo_SEARCH()
        {
            dt = new DataTable();
            dt.Columns.Add("序号", typeof(string));
            dt.Columns.Add("供应商ID", typeof(string));
            dt.Columns.Add("型号", typeof(string));
            dt.Columns.Add("品名", typeof(string));
            dt.Columns.Add("识别码", typeof(string));
            dt.Columns.Add("库存数量", typeof(string));
            return dt;
        }
        #endregion
        #region RETURN_HAVE_ID_DT
        public DataTable RETURN_HAVE_ID_DT(DataTable dtx)
        {
            DataTable dt = GetTableInfo_SEARCH();
            int i = 1;
            foreach (DataRow dr1 in dtx.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["序号"] = i.ToString();
                dr["供应商ID"] = dr1["供应商ID"].ToString();
                dr["型号"] = dr1["型号"].ToString();
                dr["品名"] = dr1["品名"].ToString();
                dr["识别码"] = dr1["识别码"].ToString();
                dr["库存数量"] = dr1["库存数量"].ToString();
                dt.Rows.Add(dr);
                i = i + 1;
            }
            return dt;
        }
        #endregion
    
    }
}
