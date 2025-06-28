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
    public class CORDER
    {
        /*预收单号与预收金额取订单号一笔带出信息*/
        basec bc = new basec();
        #region nature
        private string _ORID;
        public string ORID
        {
            set { _ORID = value; }
            get { return _ORID; }
        }
        private string _PUID;
        public string PUID
        {
            set { _PUID = value; }
            get { return _PUID; }
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
        private string _ORKEY;
        public string ORKEY
        {
            set { _ORKEY = value; }
            get { return _ORKEY; }
        }
        private string _MAKERID;
        public string MAKERID
        {
            set { _MAKERID = value; }
            get { return _MAKERID; }
        }
        private string _ORDER_DATE;
        public string ORDER_DATE
        {
            set { _ORDER_DATE = value; }
            get { return _ORDER_DATE; }
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
SELECT 
A.ORKEY AS 索引,
A.ORID AS 订单号,
D.CUID AS 客户名称,
C.UNAME AS 供应商ID,
A.LEADDAYS AS 前置天数,
A.SN AS 项次,
B.MODEL AS 型号,
B.WNAME AS 品名,
A.OCOUNT AS 数量 ,
A.PRICE AS 单价 ,
A.CURRENCY AS 币别,
A.TAXRATE AS 税率,
A.PRICE*A.OCOUNT AS 未税金额,
(
SELECT 
RTRIM(CONVERT(DECIMAL(18,2),SUM(A3.PRICE*A3.OCOUNT))) FROM ORDER_DET A3 WHERE  A.ORID =A3.ORID   GROUP BY A3.ORID
) AS 订单合计金额,
A.TAXRATE/100*A.PRICE*OCOUNT AS 税额,
A.PRICE*(1+(A.TAXRATE)/100)*OCOUNT AS 含税金额,
A.PRICE AS 单价,
CASE WHEN D.ORDERSTATUS_MST ='OPEN' THEN '未发货'
WHEN D.ORDERSTATUS_MST='PROGRESS' THEN '部分发货'
ELSE '已发货'
END AS 状态,
A.SKU AS 单位,
A.WEIGHT AS 重量,   
D.ORDER_DATE AS 下单日期,
A.DELIVERY_DATE AS  订单交期,
A.LEADDAYS AS 前置天数,
A.NEEDDATE AS 需求日期 ,
A.STOCK_PREPOSITION AS 备料前置,
A.REMARK AS 备注,
B.PRODUCT_TYPE AS 产品分类,
D.DATE,
(SELECT TOP 1 BATCHID FROM MATERE A1 WHERE A1.ORKEY=A.ORKEY) AS 识别码,
(SELECT TOP 1 ARID FROM ADVANCE_RECEIVABLES A1 WHERE A1.ORID=A.ORID ) AS 预收单号,
(SELECT TOP 1 B1.ADVANCE_RECEIVABLES FROM ADVANCE_RECEIVABLES A1 
LEFT JOIN GODE B1 ON A1.ARKEY=B1.GEKEY WHERE A1.ORID=A.ORID ) AS 预收金额,
(SELECT TOP 1 B1.SELLDATE FROM SELLTABLE_DET A1 LEFT JOIN SELLTABLE_MST B1 ON A1.SEID=B1.SEID WHERE A1.ORID=A.ORID) AS 销货日期
FROM ORDER_DET A 
LEFT JOIN ORDER_MST D ON A.ORID=D.ORID
LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID
LEFT JOIN USERINFO C ON D.PUID=C.USID






";
        string setsqlo = @"
INSERT INTO ORDER_DET
(
ORKEY,
ORID,
SN,
WAREID,
WNAME,
PRICE,
OCOUNT,
YEAR,
MONTH,
DAY
)
VALUES
(
@ORKEY,
@ORID,
@SN,
@WAREID,
@WNAME,
@PRICE,
@OCOUNT,
@YEAR,
@MONTH,
@DAY

)

";

        string setsqlt = @"

INSERT INTO ORDER_MST
(
ORID,
CUID,
PUID,
ORDER_DATE,
OrderStatus_MST,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@ORID,
@CUID,
@PUID,
@ORDER_DATE,
@OrderStatus_MST,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlth = @"
UPDATE ORDER_MST SET 
CUID=@CUID,
PUID=@PUID,
ORDER_DATE=@ORDER_DATE,
ORDERSTATUS_MST=@ORDERSTATUS_MST,
DATE=@DATE
";
        string setsqlf = @"
INSERT INTO ORDER_BARCODE
(
BARCODE,
ORKEY,
MAKERID,
DATE,
YEAR,
MONTH,
DAY
)
VALUES
(
@BARCODE,
@ORKEY,
@MAKERID,
@DATE,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlfi = @"

";
        #endregion
        DataTable dtx2 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt = new DataTable();
        CFileInfo cfileinfo = new CFileInfo();
        int i,j;
        public CORDER()
        {
            sql = setsql;
            sqlo = setsqlo;
            sqlt = setsqlt;
            sqlth = setsqlth;
            sqlf = setsqlf;
            sqlfi = setsqlfi;
        }
        public string GETID()
        {
            string v1 = bc.numYYYYMD(10, 2, "01", "SELECT * FROM ORDER_MST", "ORID", "");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
        public bool IFNOALLOW_DELETE_ORID(string ORID)
        {
            bool b = false;
            if (bc.exists("SELECT * FROM CO_ORDER WHERE ORID='" + ORID + "'"))
            {
                b = true;
                ErrowInfo = "该订单号已经存在厂内订单中，不允许修改与删除！";
            }
            return b;
        }
        #region GET_TOTAL_ORDER
        public  DataTable GET_TOTAL_ORDER()
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("索引", typeof(string));
            dtt.Columns.Add("订单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("ID", typeof(string));
            dtt.Columns.Add("料号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("规格", typeof(string));
            dtt.Columns.Add("客户料号", typeof(string));
            dtt.Columns.Add("订单数量", typeof(decimal));
            dtt.Columns.Add("累计销货数量", typeof(decimal));
            dtt.Columns.Add("累计销退数量", typeof(decimal));
            dtt.Columns.Add("实际累计销货数量", typeof(decimal), "累计销货数量-累计销退数量");
            dtt.Columns.Add("订单未结数量", typeof(decimal), "订单数量-累计销货数量+累计销退数量");
            dtt.Columns.Add("状态", typeof(string));
            dtt.Columns.Add("订单交期", typeof(string));

            DataTable dtx1 = bc.getdt("SELECT * FROM ORDER_DET");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["索引"] = dtx1.Rows[i]["ORKEY"].ToString();
                    dr["订单号"] = dtx1.Rows[i]["ORID"].ToString();
                    dr["项次"] = dtx1.Rows[i]["SN"].ToString();
                    dr["ID"] = dtx1.Rows[i]["WAREID"].ToString();
                    dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["wareid"].ToString() + "'");
                    dr["料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["规格"] = dtx2.Rows[0]["SPEC"].ToString();
                    dr["客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["订单数量"] = dtx1.Rows[i]["OCOUNT"].ToString();
                    dr["累计销货数量"] = 0;
                    dr["累计销退数量"] = 0;
                    //dr["订单交期"] = dtx1.Rows[i]["DELIVERYDATE"].ToString();
                    if (dtx1.Rows[i]["ORDERSTATUS_DET"].ToString() == "OPEN")
                    {
                        dr["状态"] = "OPEN";
                    }
                    else if (dtx1.Rows[i]["ORDERSTATUS_DET"].ToString() == "PROGRESS")
                    {
                        dr["状态"] = "部分出货";
                    }
                    /*else if (dtx1.Rows[i]["ORDERSTATUS_DET"].ToString() == "DELAY")
                    {
                        dr["状态"] = "DELAY";
                    }*/
                    else
                    {
                        dr["状态"] = "已出货";
                    }

                    dtt.Rows.Add(dr);
                }

            }

            DataTable dtx4 = bc.getdt(@"
SELECT
A.ORID AS ORID,
A.SN AS SN,
B.WAREID AS WAREID,
SUM(B.MRCOUNT) AS MRCOUNT 
FROM SELLTABLE_DET A 
LEFT JOIN MATERE B ON A.SEKEY=B.MRKEY 
GROUP BY A.ORID,A.SN,B.WAREID
");
            if (dtx4.Rows.Count > 0)
            {
                for (i = 0; i < dtx4.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["订单号"].ToString() == dtx4.Rows[i]["ORID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx4.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["累计销货数量"] = dtx4.Rows[i]["MRCOUNT"].ToString();
                            break;
                        }

                    }
                }

            }
            DataTable dtx6 = bc.getdt(@"
SELECT 
A.ORID AS ORID,
A.SN AS SN,
B.WAREID AS WAREID,
SUM(B.GECOUNT) AS GECOUNT
FROM SELLRETURN_DET A 
LEFT JOIN GODE B ON A.SRKEY=B.GEKEY  
GROUP BY 
A.ORID,
A.SN,
B.WAREID

");
            if (dtx6.Rows.Count > 0)
            {
                for (i = 0; i < dtx6.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["订单号"].ToString() == dtx6.Rows[i]["ORID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx6.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["累计销退数量"] = dtx6.Rows[i]["GECOUNT"].ToString();
                            break;
                        }

                    }
                }

            }

            return dtt;
        }
        #endregion
        #region GET_ORDER_PROGRESS_COUNT
        public string GET_ORDER_PROGRESS_COUNT(string WAREID,string ORKEY)
        {
            string v = "0";
            DataView dv = new DataView(GET_TOTAL_ORDER());
            dv.RowFilter = "状态 NOT IN ('已出货') AND ID='" + WAREID + "' AND 索引 NOT IN ('"+ORKEY +"')";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

                v = dt.Compute("SUM(订单未结数量)", "").ToString();

            }
            return v;
        }
        #endregion
        #region UPDATE_ORDER_STATUS
        public void UPDATE_ORDER_STATUS(string ORID)
        {
            DataView dv = new DataView(GET_TOTAL_ORDER());
            dv.RowFilter = "订单号='" + ORID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    decimal d0 = decimal.Parse(dr["订单数量"].ToString());
                    decimal d1 = decimal.Parse(dr["累计销货数量"].ToString());
                    decimal d2 = decimal.Parse(dr["累计销退数量"].ToString());

                   if (decimal.Parse (dr["订单未结数量"].ToString()) ==0)
                    {
                        basec.getcoms("UPDATE ORDER_DET SET ORDERSTATUS_DET='CLOSE' WHERE ORID='" + ORID + "' AND SN='" +dr["项次"].ToString () + "'");
                    }
                    /*else if (bc.JuageCurrentDateIFAboveDeliveryDate(DateTime.Now.ToString(), dr["订单交期"].ToString()))
                    {
                        basec.getcoms("UPDATE ORDER_DET SET ORDERSTATUS_DET='DELAY' WHERE ORID='" + ORID + "' AND SN='" + dr["项次"].ToString() + "'");
                    }*/
                    else if (d1 - d2 > 0)
                    {
                        basec.getcoms("UPDATE ORDER_DET SET ORDERSTATUS_DET='PROGRESS' WHERE ORID='" + ORID + "' AND SN='" + dr["项次"].ToString() + "'");
                    }
                    else
                    {
                        basec.getcoms("UPDATE ORDER_DET SET ORDERSTATUS_DET='OPEN' WHERE ORID='" + ORID + "' AND SN='" + dr["项次"].ToString() + "'");
                    }
                }
                if (bc.JuageOrderOrPurchaseStatus(ORID, 0))
                {
                    basec.getcoms("UPDATE ORDER_MST SET ORDERSTATUS_MST='CLOSE' WHERE ORID='" + ORID + "'");

                }
                /*else if (bc.JuageCurrentDateIFAboveDeliveryDate(ORID, 0))
                {
                    basec.getcoms("UPDATE ORDER_MST SET ORDERSTATUS_MST='DELAY' WHERE ORID='" + ORID + "'");
                }*/
                else if (JUAGE_REALTY_IFHAVE_SELLCOUNT(ORID))
                {

                    basec.getcoms("UPDATE ORDER_MST SET ORDERSTATUS_MST='PROGRESS' WHERE ORID='" + ORID + "'");
                }
                else
                {
                    basec.getcoms("UPDATE ORDER_MST SET ORDERSTATUS_MST='OPEN' WHERE ORID='" + ORID + "'");

                }
            }
        }
        #endregion
        #region JUAGE_REALTY_IFHAVE_SELLCOUNT
        public bool  JUAGE_REALTY_IFHAVE_SELLCOUNT(string ORID)
        {
            bool b = false;
            DataView dv = new DataView(GET_TOTAL_ORDER());
            dv.RowFilter = "订单号='" + ORID + "'";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    decimal d1 = decimal.Parse(dr["累计销货数量"].ToString());
                    decimal d2 = decimal.Parse(dr["累计销退数量"].ToString());
                    if (d1 - d2 > 0)
                    {
                        b = true;
                        break;
                    }

                }
            }
            return b;
        }
        #endregion
        #region JUAGE_ORDER_IF_HAVE_NO_AUDIT
        public bool JUAGE_ORDER_IF_HAVE_NO_AUDIT(string ORID)
        {
            bool b = false;
            string s2 = bc.getOnlyString("SELECT IF_AUDIT FROM ORDER_MST WHERE ORID='" +ORID  + "'");
            if (s2 != "Y")
            {
                b = true;
                ErrowInfo = "此订单未审核，不能进行相关操作！";
            }
            return b;
        }
        #endregion
  
        #region save
        public void save(DataGridView dgv)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            if (!bc.exists("SELECT ORID FROM ORDER_DET WHERE ORID='" + ORID + "'"))
            {
               
                SQlcommandE_DET(dgv, sqlo);
                SQlcommandE_MST(sqlt);
                IFExecution_SUCCESS = true;
            }
            else
            {
                SQlcommandE_DET(dgv, sqlo);
                SQlcommandE_MST(sqlth+" WHERE ORID='"+ORID+"'");
                IFExecution_SUCCESS = true;
            }
        }
        #endregion
        #region SQlcommandE_DET
        protected void SQlcommandE_DET(DataGridView dgv, string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            basec.getcoms("DELETE ORDER_DET WHERE ORID='" + ORID + "'");
            for (i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv["型号", i].FormattedValue.ToString() == "")
                {

                }
                else
                {
                    SqlConnection sqlcon = bc.getcon();
                    sqlcon.Open();
                    SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
                  
                    ORKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM ORDER_DET", "ORKEY", "OR");
                    sqlcom.Parameters.Add("@ORKEY", SqlDbType.VarChar, 20).Value = ORKEY;
                    sqlcom.Parameters.Add("@ORID", SqlDbType.VarChar, 20).Value = ORID;
                    sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = (i + 1).ToString();
                    sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = bc.getOnlyString("SELECT WAREID FROM WAREINFO WHERE MODEL='" + dgv["型号", i].FormattedValue .ToString() + "'");
                    sqlcom.Parameters.Add("@WNAME", SqlDbType.VarChar, 50).Value = dgv["品名", i].FormattedValue.ToString();
                    sqlcom.Parameters.Add("@PRICE", SqlDbType.VarChar, 20).Value = dgv["单价", i].FormattedValue.ToString();
                    sqlcom.Parameters.Add("@OCOUNT", SqlDbType.VarChar, 20).Value = dgv["数量", i].FormattedValue.ToString();
          

                    sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
                    sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                    sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
                    sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
                    sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
                    sqlcom.ExecuteNonQuery();
                    sqlcon.Close();
                }
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
            sqlcom.Parameters.Add("ORID", SqlDbType.VarChar, 20).Value = ORID;
            sqlcom.Parameters.Add("CUID", SqlDbType.VarChar, 20).Value = CUID;
            sqlcom.Parameters.Add("PUID", SqlDbType.VarChar, 20).Value = PUID;
            sqlcom.Parameters.Add("ORDER_DATE", SqlDbType.VarChar, 20).Value = ORDER_DATE;
            sqlcom.Parameters.Add("ORDERSTATUS_MST", SqlDbType.VarChar, 20).Value = "OPEN";
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
        #region GetTableInfo
        public DataTable GetTableInfo()
        {
            dt = new DataTable();
            dt.Columns.Add("项次", typeof(string));
            dt.Columns.Add("产品分类", typeof(string));
            dt.Columns.Add("品名", typeof(string));
            dt.Columns.Add("型号", typeof(string));
            dt.Columns.Add("单价", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            dt.Columns.Add("识别码", typeof(string));
            return dt;
        }
        #endregion
        #region GetTableInfo_SEARCH
        public DataTable GetTableInfo_SEARCH()
        {
            dt = new DataTable();
            dt.Columns.Add("序号", typeof(string));
            dt.Columns.Add("订单号", typeof(string));
            dt.Columns.Add("项次", typeof(string));
            dt.Columns.Add("客户名称", typeof(string));
            dt.Columns.Add("下单日期", typeof(string));
            dt.Columns.Add("供应商ID", typeof(string));
            dt.Columns.Add("型号", typeof(string));
            dt.Columns.Add("品名", typeof(string));
            dt.Columns.Add("单价", typeof(decimal ));
            dt.Columns.Add("数量", typeof(decimal ));
            dt.Columns.Add("金额", typeof(decimal ),"单价*数量");
            dt.Columns.Add("总计", typeof(decimal));
            dt.Columns.Add("状态", typeof(string));
           
            return dt;
        }
        #endregion
        #region RETURN_DT
        public DataTable RETURN_DT(DataTable dtt)
        {
            int i = 1;
            DataTable dt = GetTableInfo_SEARCH();
            foreach (DataRow dr1 in dtt.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["序号"] = i.ToString();
                dr["订单号"] = dr1["订单号"].ToString();
                dr["项次"] = dr1["项次"].ToString();
                dr["客户名称"] = dr1["客户名称"].ToString();
                dr["供应商ID"] = dr1["供应商ID"].ToString();
                dr["下单日期"] = dr1["下单日期"].ToString();
              
                dr["型号"] = dr1["型号"].ToString();
                dr["品名"] = dr1["品名"].ToString();
                dr["单价"] = dr1["单价"].ToString();
                dr["数量"] = dr1["数量"].ToString();
             
                dr["状态"] = dr1["状态"].ToString();
                dr["总计"] = dr1["订单合计金额"].ToString();
                dt.Rows.Add(dr);
                i = i + 1;
            }
            return dt;
        }
        #endregion
        #region ExcelPrint
        public void ExcelPrint(DataGridView dv, string BillName, string Printpath)
        {
            if (dv.Rows.Count > 0)
            {
                if (bc.JUAGE_IF_EXISTS_SELECT_DV(dv))
                {
                    SaveFileDialog sfdg = new SaveFileDialog();
                    //sfdg.DefaultExt = @"D:\xls";
                    sfdg.Filter = "Excel(*.xls)|*.xls";
                    sfdg.RestoreDirectory = true;
                    sfdg.FileName = Printpath;
                    sfdg.CreatePrompt = true;
                    Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                    Excel.Workbook workbook;
                    Excel.Worksheet worksheet;
                    workbook = application.Workbooks._Open(sfdg.FileName, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing);
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                    application.Visible = false;
                    application.ExtendList = false;
                    application.DisplayAlerts = false;
                    application.AlertBeforeOverwriting = false;
                    int j = 0;
                    for (int i = 0; i < dv.Rows.Count; i++)
                    {
                        if (dv["序号", i].Selected == true)
                        {
                            BARCODE = bc.numYMD(20, 12, "000000000001", "select * from ORDER_BARCODE", "BARCODE", "BA");
                            ORKEY = bc.getOnlyString(string.Format ("SELECT ORKEY FROM ORDER_DET WHERE ORID='{0}' AND SN='{1}'",
                                dt.Rows[i]["订单号"].ToString(),dt.Rows[i]["项次"].ToString()));
                            SQlcommandE();
                            var writer = new BarcodeWriter
                            {
                                Format = BarcodeFormat.QR_CODE,
                            };
                            PictureBox pic = new PictureBox();
                            pic.Image = writer.Write(BARCODE);
                            pic.Image.Save("d:\\" + BARCODE);
                            worksheet.Shapes.AddPicture("d:\\" +
                            BARCODE, Microsoft.Office.Core.MsoTriState.msoFalse,
                            Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1+j * 146, 60, 60);
                            /*删除本地的临时图片文件 start*/
                            if (File.Exists("d:\\" + BARCODE))
                            {
                                File.Delete("d:\\" + BARCODE);
                            }
                            /*删除本地的临时图片文件 end*/
                            /*worksheet.get_Range("A2", "C7").Copy(worksheet.get_Range(worksheet.Cells[2 + j * 7, "A"], worksheet.Cells[7 + j * 7, "C"]));*/
                            
                            worksheet.get_Range(worksheet.Cells[1 + j * 7, "B"], worksheet.Cells[1 + j * 7, "C"]).MergeCells = true;
                         
                            worksheet.get_Range(worksheet.Cells[2 + j * 7, "B"], worksheet.Cells[2 + j * 7, "C"]).MergeCells = true;
                            worksheet.get_Range(worksheet.Cells[3 + j * 7, "B"], worksheet.Cells[3 + j * 7, "C"]).MergeCells = true;
                            worksheet.get_Range(worksheet.Cells[4 + j * 7, "B"], worksheet.Cells[4 + j * 7, "C"]).MergeCells = true;
                            worksheet.get_Range(worksheet.Cells[5 + j * 7, "B"], worksheet.Cells[5 + j * 7, "C"]).MergeCells = true;
                            worksheet.get_Range(worksheet.Cells[6 + j * 7, "B"], worksheet.Cells[6 + j * 7, "C"]).MergeCells = true;
                            worksheet.get_Range(worksheet.Cells[7 + j * 7, "B"], worksheet.Cells[7 + j * 7, "C"]).MergeCells = true;
               

                            worksheet.get_Range(worksheet.Cells[1 + j * 7, "A"], worksheet.Cells[1 + j * 7, "C"]).RowHeight = 64.75;
                            worksheet.get_Range(worksheet.Cells[2 + j * 7, "A"], worksheet.Cells[7 + j * 7, "C"]).Font.Size = 12;
                            worksheet.get_Range(worksheet.Cells[2 + j * 7, "B"], worksheet.Cells[7 + j * 7, "C"]).HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlLeft;
                            worksheet.Cells[1 + j * 7, "B"] = BARCODE;
                            worksheet.Cells[2 + j * 7, "A"] = "客户名称";
                            worksheet.Cells[3 + j * 7, "A"] = "型号";
                            worksheet.Cells[4 + j * 7, "A"] = "品名";
                            worksheet.Cells[5 + j * 7, "A"] = "单价";
                            worksheet.Cells[6 + j * 7, "A"] = "数量";
                            worksheet.Cells[7 + j * 7, "A"] = "下单日期";

                          
                            worksheet.Cells[2 + j * 7, "B"] = dt.Rows[i]["客户名称"].ToString();
                            worksheet.Cells[3 + j * 7, "B"] = dt.Rows[i]["型号"].ToString();
                            worksheet.Cells[4 + j * 7, "B"] = dt.Rows[i]["品名"].ToString();
                            worksheet.Cells[5 + j * 7, "B"] = dt.Rows[i]["单价"].ToString();
                            worksheet.Cells[6 + j * 7, "B"] = dt.Rows[i]["数量"].ToString();
                            worksheet.Cells[7 + j * 7, "B"] = dt.Rows[i]["下单日期"].ToString();
                            j = j + 1;
                    
                        }
                    }
                    worksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    /*workbook.SaveAs(System.IO.Path.GetFullPath("PRINT_TEMP/"+BARCODE+".xlsx"), Excel.XlFileFormat.xlExcel7, Type.Missing, 
                        Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, 
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                 
                    application.Quit();
                    worksheet = null;
                    workbook = null;
                    application = null;
                    GC.Collect();
                    Excel.Application application1 = new Microsoft.Office.Interop.Excel.Application();
                    Excel.Workbook workbook1;
                    Excel.Worksheet worksheet1=null ;
                    workbook1 = application1.Workbooks._Open(System.IO.Path.GetFullPath("PRINT_TEMP/" + BARCODE + ".xls"), Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing); ;
                    worksheet1 = (Excel.Worksheet)workbook1.Worksheets[1];
                    worksheet1.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    application1.Quit();
                    worksheet1 = null;
                    workbook1 = null;
                    application1 = null;
                    GC.Collect();*/
                    ErrowInfo = "打印数据已发送至打印机";
                }
                else
                {
                    ErrowInfo = "没有选中要打印的项";
                    return;
                }
            }
            else
            {
                ErrowInfo = "没有数据可打印";
                return;
            }
     
         
          
        }
        #endregion
        #region ExcelPrint_40X30
        public void ExcelPrint_40X30(DataGridView dv, string BillName, string Printpath)
        {
            if (dv.Rows.Count > 0)
            {
                if (bc.JUAGE_IF_EXISTS_SELECT_DV(dv))
                {
                    SaveFileDialog sfdg = new SaveFileDialog();
                    //sfdg.DefaultExt = @"D:\xls";
                    sfdg.Filter = "Excel(*.xls)|*.xls";
                    sfdg.RestoreDirectory = true;
                    sfdg.FileName = Printpath;
                    sfdg.CreatePrompt = true;
                    Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
                    Excel.Workbook workbook;
                    Excel.Worksheet worksheet;
                    workbook = application.Workbooks._Open(sfdg.FileName, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing);
                    worksheet = (Excel.Worksheet)workbook.Worksheets[1];
                    application.Visible = false;
                    application.ExtendList = false;
                    application.DisplayAlerts = false;
                    application.AlertBeforeOverwriting = false;
                    int j = 0;
                    List<CFileInfo> list1 = cfileinfo.FindFile(System.IO.Path.GetFullPath("PRINT_TEMP/"));
                    foreach (CFileInfo file in list1)
                    {
                        try
                        {
                            File.Delete(file.FileNameAndPath);
                        }
                        catch (Exception)
                        {
                           
                        }
                    }
                    for (int i = 0; i < dv.Rows.Count; i++)
                    {
                        if (dv["序号", i].Selected == true)
                        {
                            BARCODE = bc.numYMD(20, 12, "000000000001", "select * from ORDER_BARCODE", "BARCODE", "BA");
                            ORKEY = bc.getOnlyString(string.Format("SELECT ORKEY FROM ORDER_DET WHERE ORID='{0}' AND SN='{1}'",
                                dt.Rows[i]["订单号"].ToString(), dt.Rows[i]["项次"].ToString()));
                            SQlcommandE();
                            var writer = new BarcodeWriter
                            {
                                Format = BarcodeFormat.QR_CODE,
                            };
                            PictureBox pic = new PictureBox();
                            pic.Image = writer.Write(BARCODE);
                            pic.Image.Save("d:\\" + BARCODE);
                            worksheet.Shapes.AddPicture("d:\\" +
                            BARCODE, Microsoft.Office.Core.MsoTriState.msoFalse,
                            Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1 + j * 67, 50, 50);
                            /*删除本地的临时图片文件 start*/
                            if (File.Exists("d:\\" + BARCODE))
                            {
                                File.Delete("d:\\" + BARCODE);
                            }
                            /*删除本地的临时图片文件 end*/
                            worksheet.get_Range("A5", "B5").Copy(worksheet.get_Range(worksheet.Cells[5 + j * 5, "A"], worksheet.Cells[5 + j * 5, "B"]));
                            worksheet.get_Range(worksheet.Cells[1 + j * 5, "A"], worksheet.Cells[1 + j * 5, "B"]).MergeCells = true;
                            //worksheet.get_Range(worksheet.Cells[1 + j * 1, "A"], worksheet.Cells[1 + j * 1, "B"]).RowHeight = 54;
                            worksheet.Cells[5 + j * 5, "A"] = dt.Rows[i]["订单号"].ToString() + "-" + dt.Rows[i]["项次"].ToString();
                            j = j + 1;

                        }
                    }
                    workbook.SaveAs(System.IO.Path.GetFullPath("PRINT_TEMP/" + BARCODE), Excel.XlFileFormat.xlExcel7, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    application.Quit();
                    worksheet = null;
                    workbook = null;
                    application = null;
                    GC.Collect();
                    Excel.Application application1 = new Microsoft.Office.Interop.Excel.Application();
                    Excel.Workbook workbook1;
                    Excel.Worksheet worksheet1 = null;
                    workbook1 = application1.Workbooks._Open(System.IO.Path.GetFullPath("PRINT_TEMP/" + BARCODE + ".xls"), Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing); ;
                    worksheet1 = (Excel.Worksheet)workbook1.Worksheets[1];
                    worksheet1.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    application1.Quit();
                    worksheet1 = null;
                    workbook1 = null;
                    application1 = null;
                    GC.Collect();
                    ErrowInfo = "打印数据已发出";
                }
                else
                {
                    ErrowInfo = "没有选中要打印的项";
                    return;
                }
            }
            else
            {
                ErrowInfo = "没有数据可打印";
                return;
            }



        }
        #endregion
        #region SQlcommandE
        public void SQlcommandE()
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            SqlConnection sqlcon = bc.getcon();
            SqlCommand sqlcom = new SqlCommand(sqlf, sqlcon);
            sqlcon.Open();
            sqlcom.Parameters.Add("BARCODE", SqlDbType.VarChar, 20).Value = BARCODE;
            sqlcom.Parameters.Add("ORKEY", SqlDbType.VarChar, 20).Value = ORKEY;
            sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
            sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
            sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
            sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
            sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
        }
        #endregion
    }
}
