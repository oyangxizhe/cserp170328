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
    public class CPRIMARY_COLORS:IGETID 
    {
        basec bc = new basec();
        private string _USID;
        public string USID
        {
            set { _USID = value; }
            get { return _USID; }

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
        private string _PRIMARY_COLORS;
        public string PRIMARY_COLORS
        {
            set { _PRIMARY_COLORS = value; }
            get { return _PRIMARY_COLORS; }

        }
        DataTable dt = new DataTable();
      
        public CPRIMARY_COLORS()
        {
         
        }
        public string GETID()
        {
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM PRIMARY_COLORS", "PRID", "PR");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
    
    }
}
