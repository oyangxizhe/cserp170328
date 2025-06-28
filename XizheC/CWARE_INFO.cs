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
    public class CWARE_INFO
    {
        basec bc = new basec();
    
        #region nature
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
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }

        }
        public string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }

        }

        private string _CO_WAREID;
        public string CO_WAREID
        {
            set { _CO_WAREID = value; }
            get { return _CO_WAREID; }
        }
        private string _WNAME;
        public string WNAME
        {
            set { _WNAME = value; }
            get { return _WNAME; }
        }
        private string _SPEC;
        public string SPEC
        {
            set { _SPEC = value; }
            get { return _SPEC; }
        }
        private string _UNIT;
        public string UNIT
        {
            set { _UNIT = value; }
            get { return _UNIT; }
        }
        private string _sql;
        public string sql
        {
            set { _sql = value; }
            get { return _sql; }
        }
        private string _BUYING_PRICE;
        public string BUYING_PRICE
        {
            set { _BUYING_PRICE = value; }
            get { return _BUYING_PRICE; }
        }
        private string _TRADE_PRICE;
        public string TRADE_PRICE
        {
            set { _TRADE_PRICE = value; }
            get { return _TRADE_PRICE; }
        }
        private string _RETAIL_PRICE;
        public string RETAIL_PRICE
        {
            set { _RETAIL_PRICE = value; }
            get { return _RETAIL_PRICE; }
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
        private string _PRODUCT_TYPE;
        public string PRODUCT_TYPE
        {
            set { _PRODUCT_TYPE = value; }
            get { return _PRODUCT_TYPE; }
        }
        private string _MODEL;
        public string MODEL
        {
            set { _MODEL = value; }
            get { return _MODEL; }
        }
        private string _SIMPLE_CODE;
        public string SIMPLE_CODE
        {
            set { _SIMPLE_CODE = value; }
            get { return _SIMPLE_CODE; }
        }
        private string _sqlfi;
        public string sqlfi
        {
            set { _sqlfi = value; }
            get { return _sqlfi; }

        }
        private  bool _IFExecutionSUCCESS;
        public  bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
  
        public string _SEMI_FINISHED;
        public string SEMI_FINISHED
        {
            set { _SEMI_FINISHED = value; }
            get { return _SEMI_FINISHED; }

        }
        public string _ACTIVE;
        public string ACTIVE
        {
            set { _ACTIVE = value; }
            get { return _ACTIVE; }
        }
        public string _MATERIALS;
        public string MATERIALS
        {
            set { _MATERIALS = value; }
            get { return _MATERIALS; }

        }
        #endregion
        #region setsql
        string setsql = @"

SELECT 
(SELECT UName  FROM UserInfo  WHERE USID=A.MAKERID) AS 供应商ID,
A.WAREID AS 物料编号,
A.PRODUCT_TYPE AS 产品分类,
A.WNAME AS 品名,
A.MODEL AS 型号,
A.CO_WAREID AS 产品编号,
A.SIMPLE_CODE AS 搜索简码,
A.BUYING_PRICE AS 进货价,
A.TRADE_PRICE AS 批发价,
A.RETAIL_PRICE AS 零售价
FROM  WAREINFO A
LEFT JOIN USERINFO B ON A.MAKERID=B.USID

";
      
        string setsqlo = @"
SELECT 
(SELECT UName  FROM UserInfo  WHERE USID=A.MAKERID) AS 供应商ID,
A.WAREID AS 物料编号,
A.PRODUCT_TYPE AS 产品分类,
A.WNAME AS 品名,
A.MODEL AS 型号,
A.CO_WAREID AS 产品编号,
A.SIMPLE_CODE AS 搜索简码,
A.TRADE_PRICE AS 批发价,
A.RETAIL_PRICE AS 零售价,
A.DATE AS 制单日期
FROM  WAREINFO A
LEFT JOIN USERINFO B ON A.MAKERID=B.USID
";
      string setsqlt = @"
INSERT INTO WAREINFO(
WAREID,
CO_WAREID,
WNAME,
PRODUCT_TYPE,
MODEL,
SIMPLE_CODE,
BUYING_PRICE,
TRADE_PRICE,
RETAIL_PRICE,
DATE,
MAKERID,
YEAR,
MONTH
)
VALUES 
(
@WAREID,
@CO_WAREID,
@WNAME,
@PRODUCT_TYPE,
@MODEL,
@SIMPLE_CODE,
@BUYING_PRICE,
@TRADE_PRICE,
@RETAIL_PRICE,
@DATE,
@MAKERID,
@YEAR,
@MONTH
)
";
      string setsqlth = @"
UPDATE WAREINFO SET 
CO_WAREID=@CO_WAREID,
WNAME=@WNAME,
PRODUCT_TYPE=@PRODUCT_TYPE,
MODEL=@MODEL,
SIMPLE_CODE=@SIMPLE_CODE,
BUYING_PRICE=@BUYING_PRICE,
TRADE_PRICE=@TRADE_PRICE,
RETAIL_PRICE=@RETAIL_PRICE,
DATE=@DATE,
YEAR=@YEAR,
MONTH=@MONTH
"; 
        #endregion

         public CWARE_INFO()
        {
            string year, month, day;
            year = DateTime.Now.ToString("yy");
            month = DateTime.Now.ToString("MM");
            day = DateTime.Now.ToString("dd");
            //GETID =bc.numYM(10, 4, "0001", "SELECT * FROM WORKORDER_SCRAP_MST", "WSID", "WS");
            sql = setsql; /*WAREINFO*/
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth; 
        }
 
              public string GETID(string  KEY)
            {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            string v1 = bc.numYMFREE(9, 4, "0001", "SELECT * FROM WareINFO WHERE SUBSTRING(WAREID,1,1)='"+KEY +"' AND YEAR='" + year +
                         "' AND MONTH='" + month + "'", "WAREID", KEY );
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
              

                  string GET_CO_WAREID = bc.getOnlyString("SELECT CO_WAREID FROM WAREINFO WHERE WAREID='" + WAREID + "'");
                  string GET_MODEL = bc.getOnlyString("SELECT MODEL FROM WAREINFO WHERE WAREID='" + WAREID + "'");
                  if (!bc.exists("SELECT WAREID FROM WAREINFO WHERE WAREID='" + WAREID + "'"))
                  {
                      if (CO_WAREID != "" && bc.exists("SELECT * FROM WAREINFO where CO_WAREID='" + CO_WAREID + "'"))
                      {

                          ErrowInfo = "该料号已经存在了！";
                          IFExecution_SUCCESS = false;
                      }
                      else if (MODEL  != "" && bc.exists("SELECT * FROM WAREINFO where MODEL='" + MODEL + "'"))
                      {

                          ErrowInfo = "该型号已经存在了！";
                          IFExecution_SUCCESS = false;
                      }
                      else
                      {
                        
                          SQlcommandE_MST(sqlt);
                      
                          IFExecution_SUCCESS = true;

                      }
                  }
                  else if (CO_WAREID != "" && GET_CO_WAREID != CO_WAREID)
                  {


                      if (bc.exists("SELECT * FROM WAREINFO where CO_WAREID='" + CO_WAREID + "'"))
                      {

                          ErrowInfo = "该料号已经存在了！";
                          IFExecution_SUCCESS = false;

                      }
                      else
                      {
                        
                          SQlcommandE_MST(sqlth + " WHERE WAREID='" + WAREID + "'");
                          IFExecution_SUCCESS = true;
                      }


                  }
                  else if (MODEL  != "" && GET_MODEL != MODEL )
                  {


                      if (bc.exists("SELECT * FROM WAREINFO where MODEL='" + MODEL  + "'"))
                      {

                          ErrowInfo = "该型号已经存在了！";
                          IFExecution_SUCCESS = false;

                      }
                      else
                      {

                          SQlcommandE_MST(sqlth + " WHERE WAREID='" + WAREID + "'");
                          IFExecution_SUCCESS = true;
                      }


                  }
                  else
                  {
                      
                      SQlcommandE_MST(sqlth + " WHERE WAREID='" + WAREID + "'");
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
                  sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = WAREID;
                  sqlcom.Parameters.Add("@WNAME", SqlDbType.VarChar, 20).Value = WNAME;
                  sqlcom.Parameters.Add("@CO_WAREID", SqlDbType.VarChar, 20).Value = CO_WAREID;
                  sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                  sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
                  sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
                  sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
                  sqlcom.Parameters.Add("@PRODUCT_TYPE", SqlDbType.VarChar, 20).Value = PRODUCT_TYPE;
                  sqlcom.Parameters.Add("@MODEL", SqlDbType.VarChar, 20).Value = MODEL;
                  sqlcom.Parameters.Add("@SIMPLE_CODE", SqlDbType.VarChar, 20).Value = SIMPLE_CODE;
                  sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
                  sqlcom.Parameters.Add("@BUYING_PRICE", SqlDbType.VarChar, 20).Value = BUYING_PRICE  ;
                  sqlcom.Parameters.Add("@TRADE_PRICE", SqlDbType.VarChar, 20).Value = TRADE_PRICE ;
                  sqlcom.Parameters.Add("@RETAIL_PRICE", SqlDbType.VarChar, 20).Value = RETAIL_PRICE ;
                  sqlcom.ExecuteNonQuery();
                  sqlcon.Close();
              }
              #endregion
     #region GET_MAX_STORAGECOUNT()
     public DataTable GET_MAX_STORAGECOUNT(string SOURCE)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("WAREID", typeof(string));
            dtt.Columns.Add("CO_WAREID", typeof(string));
            dtt.Columns.Add("WNAME", typeof(string));
            dtt.Columns.Add("CWAREID", typeof(string));
            dtt.Columns.Add("SPEC", typeof(string));
            dtt.Columns.Add("CUID", typeof(string));
            dtt.Columns.Add("CNAME", typeof(string));
            /*dtt.Columns.Add("SUID", typeof(string));
            dtt.Columns.Add("SNAME", typeof(string));*/
            dtt.Columns.Add("REMARK", typeof(string));
            dtt.Columns.Add("SELLUNITPRICE", typeof(string));
            dtt.Columns.Add("PURCHASEUNITPRICE", typeof(string));
            dtt.Columns.Add("CURRENCY", typeof(string));
            dtt.Columns.Add("DATE", typeof(string));
            dtt.Columns.Add("MAKER", typeof(string));
            dtt.Columns.Add("ACTIVE", typeof(string));
            dtt.Columns.Add("BRAND", typeof(string));
            dtt.Columns.Add("MPA_UNIT", typeof(string));
            dtt.Columns.Add("SKU", typeof(string));
            dtt.Columns.Add("BOM_UNIT", typeof(string));
            dtt.Columns.Add("STORAGENAME", typeof(string));
            dtt.Columns.Add("STORAGE_LOCATION", typeof(string));
            dtt.Columns.Add("BATCHID", typeof(string));
            dtt.Columns.Add("MAX_STORAGE_COUNT", typeof(string));
            DataTable dtx = new DataTable();
            if (SOURCE == "O")
            {
                dtx = bc.getdt(sqlo);
            }
            else if (SOURCE == "P")
            {
                dtx = bc.getdt(sqlt);
               
            }
            else
            {
                dtx = bc.getdt(sql);
            
            }

            if (dtx.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtx.Rows)
                {

                    DataRow dr = dtt.NewRow();
                    dr["WAREID"] = dr1["WAREID"].ToString();
                    dr["CO_WAREID"] = dr1["CO_WAREID"].ToString();
                    dr["WNAME"] = dr1["WNAME"].ToString();
                    dr["CWAREID"] = dr1["CWAREID"].ToString();
                    dr["SPEC"] = dr1["SPEC"].ToString();

                    if (SOURCE == "O")
                    {
                        dr["SELLUNITPRICE"] = dr1["SELLUNITPRICE"].ToString();
                        dr["CURRENCY"] = dr1["CURRENCY"].ToString();
                    }
                    if (SOURCE == "P")
                    {
                        dr["PURCHASEUNITPRICE"] = dr1["PURCHASEUNITPRICE"].ToString();
                        dr["CURRENCY"] = dr1["CURRENCY"].ToString();
                       
                    }
                    dr["CUID"] = dr1["CUID"].ToString();
                    dr["CNAME"] = dr1["CNAME"].ToString();
                    dr["DATE"] = dr1["DATE"].ToString();
                    dr["MAKER"] = dr1["MAKER"].ToString();
                    dr["ACTIVE"] = dr1["ACTIVE"].ToString();
                    dr["BRAND"] = dr1["BRAND"].ToString();
                    dr["MPA_UNIT"] = dr1["MPA_UNIT"].ToString();
                    dr["SKU"] = dr1["SKU"].ToString();
                    dr["BOM_UNIT"] = dr1["BOM_UNIT"].ToString();

                    /*DataTable dtx1 = bc.getmaxstoragecount(dr1["WAREID"].ToString(), dr1["SKU"].ToString());
                    if (dtx1.Rows.Count > 0)
                    {
                        dr["STORAGENAME"] = dtx1.Rows[0]["仓库"].ToString();
                        dr["STORAGE_LOCATION"] = dtx1.Rows[0]["库位"].ToString();
                        dr["BATCHID"] = dtx1.Rows[0]["批号"].ToString();
                        dr["MAX_STORAGE_COUNT"] = dtx1.Rows[0]["库存数量"].ToString();
                    }*/
                    dtt.Rows.Add(dr);
                }
            }
           
            return dtt;
        }
        #endregion
    }
}
