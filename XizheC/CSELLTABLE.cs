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
    public class CSELLTABLE
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
        private string _CNAME;
        public string CNAME
        {
            set { _CNAME = value; }
            get { return _CNAME; }
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
        private string _SELLTABLE_DATE;
        public string SELLTABLE_DATE
        {
            set { _SELLTABLE_DATE = value; }
            get { return _SELLTABLE_DATE; }
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
        private string _STORAGEID;
        public string STORAGEID
        {
            set { _STORAGEID = value; }
            get { return _STORAGEID; }
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
        private string _REMARK;
        public string REMARK
        {
            set { _REMARK = value; }
            get { return _REMARK; }
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
WITH ds1 as (SELECT
ROW_NUMBER() OVER (ORDER BY A.SEKEY ASC)  AS  序号,
A.SEID AS 销货单号,
A.ORID as 订单号, 
A.SN as 项次,
B.MODEL as 型号,
B.WNAME AS 品名,
C.OCOUNT AS 订单数量,
E.BATCHID AS 识别码,
C.PRICE AS 单价,
CAST(ROUND(E.MRCount,2) AS DECIMAL(18,2)) as 销货数量 ,
CAST(ROUND(E.MRCOUNT*C.PRICE,4) AS DECIMAL(18,2)) AS 金额,
(
SELECT 
RTRIM(CONVERT(DECIMAL(18,2),SUM(A2.MRCOUNT*A3.PRICE))) FROM SELLTABLE_DET A1 
LEFT JOIN MateRe A2 ON A1.SEKEY=A2.MRKEY 
LEFT JOIN Order_DET A3 ON A1.ORID =A3.ORID AND A1.SN =A3.SN 
WHERE A1.SEID=A.SEID GROUP BY A1.SEID
) AS 销货单销货金额,
(
SELECT 
RTRIM(CONVERT(DECIMAL(18,2),SUM(A2.MRCOUNT*A3.PRICE))) FROM SELLTABLE_DET A1 
LEFT JOIN MateRe A2 ON A1.SEKEY=A2.MRKEY 
LEFT JOIN Order_DET A3 ON A1.ORID =A3.ORID AND A1.SN =A3.SN 
WHERE A1.ORID=A.ORID GROUP BY A1.ORID
) AS 订单销货金额,
(
SELECT 
RTRIM(CONVERT(DECIMAL(18,2),SUM(A2.MRCOUNT))) FROM SELLTABLE_DET A1 
LEFT JOIN MateRe A2 ON A1.SEKEY=A2.MRKEY WHERE A1.ORID=A.ORID AND A1.SN=A.SN  GROUP BY A1.ORID,A1.SN 
) AS 累计销货,
isnull((
SELECT 
RTRIM(CONVERT(DECIMAL(18,2),SUM(A2.GECOUNT))) FROM SELLRETURN_DET A1 
LEFT JOIN GODE A2 ON A1.SRKEY=A2.GEKEY WHERE A1.ORID=A.ORID AND A1.SN=A.SN  GROUP BY A1.ORID,A1.SN ),0)
 AS 累计销退,
C.OCOUNT-
(
SELECT 
RTRIM(CONVERT(DECIMAL(18,0),SUM(A2.MRCOUNT))) FROM SELLTABLE_DET A1 
LEFT JOIN MateRe A2 ON A1.SEKEY=A2.MRKEY WHERE A1.ORID=A.ORID AND A1.SN=A.SN  GROUP BY A1.ORID,A1.SN 
) 
+
CASE WHEN(
SELECT 
RTRIM(CONVERT(DECIMAL(18,0),SUM(A2.GECOUNT))) FROM SELLRETURN_DET A1 
LEFT JOIN GODE A2 ON A1.SRKEY=A2.GEKEY WHERE A1.ORID=A.ORID AND A1.SN=A.SN  GROUP BY A1.ORID,A1.SN 
) IS NOT NULL THEN
(
SELECT 
RTRIM(CONVERT(DECIMAL(18,0),SUM(A2.GECOUNT))) FROM SELLRETURN_DET A1 
LEFT JOIN GODE A2 ON A1.SRKEY=A2.GEKEY WHERE A1.ORID=A.ORID AND A1.SN=A.SN  GROUP BY A1.ORID,A1.SN 
) 
ELSE 0
END  
AS 未销数量,
H.CUID  as 客户名称 ,
F.SELLDATE AS 销货日期,
F.DATE AS  日期,
(SELECT SUM(cast(A1.COUNT*B1.PRICE as decimal(18,2))) FROM RECEIVABLES_DET  A1  
LEFT JOIN Order_DET B1 ON A1.SSKEY =B1.ORKEY WHERE B1.ORID =H.ORID GROUP BY B1.ORID ) AS 总计,
CASE WHEN J.ADVANCE_RECEIVABLES IS NULL THEN 0 
ELSE J.ADVANCE_RECEIVABLES  
END AS 预收金额,
G.CUTPAYMENT_PROJECT AS 扣款项目,
CASE WHEN  G.CUTPAYMENT_AMOUNT='' then 0
ELSE G.CUTPAYMENT_AMOUNT
END 
AS 扣款金额,
(SELECT 
case when sum(RECEIVABLES_ORDER_AMOUNT) IS null then 0
else 
SUM(RECEIVABLES_ORDER_AMOUNT) 
end  FROM RECEIVABLES_ORDER A1 WHERE A1.RCID =G.RCID GROUP BY A1.RCID) AS 累计收款金额1,
(SELECT
  DISTINCT  A1.RECEIVABLES_ORDER_DATE  +' '+A1.PAYMENT+' '+CONVERT(varchar(20),A1.RECEIVABLES_ORDER_AMOUNT,111) +';'
FROM 
  RECEIVABLES_ORDER A1 WHERE A1.RCID =G.RCID 
FOR XML PATH('')
) AS 收款记录,
F.REMARK AS 备注
from SELLTABLE_DET A 
LEFT JOIN ORDER_DET C ON A.ORID=C.ORID AND A.SN=C.SN
LEFT JOIN MATERE E ON A.SEKEY=E.MRKEY
LEFT JOIN WAREINFO B ON E.WAREID=B.WAREID
LEFT JOIN SELLTABLE_MST F ON A.SEID=F.SEID
LEFT JOIN Order_MST H ON C.ORID =H.ORID 
LEFT JOIN CUSTOMERINFO_MST D ON H.CUID=D.CUID
LEFT JOIN RECEIVABLES_MST G ON G.ORID=H.ORID 
LEFT JOIN ADVANCE_RECEIVABLES I ON G.ARID=I.ARID 
LEFT JOIN Gode J ON I.ARID=J.GodeID 
),
ds2  as (select *,总计-预收金额-扣款金额 as 实际应收金额,
case when 累计收款金额1 IS null then 0 else 累计收款金额1 end as 累计收款金额
 from ds1),
ds3 as (select * ,实际应收金额-累计收款金额 as 未收金额 from ds2)
select * from ds3




";
        string setsqlo = @"
INSERT INTO SELLTABLE_DET
(
SEKEY,
SEID,
ORID,
SN,
YEAR,
MONTH,
DAY
)
VALUES
(
@SEKEY,
@SEID,
@ORID,
@SN,
@YEAR,
@MONTH,
@DAY

)

";

        string setsqlt = @"

INSERT INTO SELLTABLE_MST
(
SEID,
SELLDATE,
SELLERID,
SEND_ADDRESS,
CONTACT,
PHONE,
REMARK,
DATE,
MAKERID,
YEAR,
MONTH,
DAY
)
VALUES
(
@SEID,
@SELLDATE,
@SELLERID,
@SEND_ADDRESS,
@CONTACT,
@REMARK,
@PHONE,
@DATE,
@MAKERID,
@YEAR,
@MONTH,
@DAY
)
";
        string setsqlth = @"
UPDATE SELLTABLE_MST SET 
SELLDATE=@SELLDATE,
SELLERID=@SELLERID,
SEND_ADDRESS=@SEND_ADDRESS,
CONTACT=@CONTACT,
PHONE=@PHONE,
REMARK=@REMARK,
DATE=@DATE
";
        string setsqlf = @"
INSERT INTO 
MATERE
(
MRKEY,
MATEREID,
SN,
MRCOUNT,
WAREID,
STORAGEID,
BATCHID,
ORKEY,
Date,
MakerID,
Year,
Month,
Day
)
VALUES
(
@MRKEY,
@MATEREID,
@SN,
@MRCOUNT,
@WAREID,
@STORAGEID,
@BATCHID,
@ORKEY,
@Date,
@MakerID,
@Year,
@Month,
@Day
)
";
        string setsqlfi = @"

";
        #endregion
        DataTable dtx2 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtx = new DataTable();
        CFileInfo cfileinfo = new CFileInfo();
        CORDER corder = new CORDER();
        CMOLD_BASE cmold_base = new CMOLD_BASE();
        int i,j;
        public CSELLTABLE()
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
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM SELLTABLE_MST", "SEID", "SE");
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
            if (bc.exists("SELECT * FROM CO_SELLTABLE WHERE ORID='" + ORID + "'"))
            {
                b = true;
                ErrowInfo = "该订单号已经存在厂内订单中，不允许修改与删除！";
            }
            return b;
        }
        #region GET_TOTAL_SELLTABLE
        public  DataTable GET_TOTAL_SELLTABLE()
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
            dtt.Columns.Add("订单未结数量", typeof(decimal), "订单数量-累计销货数量+累计销退数量");
            dtt.Columns.Add("状态", typeof(string));
            dtt.Columns.Add("交货日期", typeof(string));

            DataTable dtx1 = bc.getdt("SELECT * FROM SELLTABLE_DET ");
            if (dtx1.Rows.Count > 0)
            {
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["索引"] = dtx1.Rows[i]["SEKEY"].ToString();
                    dr["订单号"] = dtx1.Rows[i]["ORID"].ToString();
                    dr["项次"] = dtx1.Rows[i]["SN"].ToString();
                    dr["ID"] = dtx1.Rows[i]["WAREID"].ToString();
                    dtx2 = bc.getdt("select * from wareinfo where wareid='" + dtx1.Rows[i]["WAREID"].ToString() + "'");
                    dr["料号"] = dtx2.Rows[0]["CO_WAREID"].ToString();
                    dr["品名"] = dtx2.Rows[0]["WNAME"].ToString();
                    dr["规格"] = dtx2.Rows[0]["SPEC"].ToString();
                    dr["客户料号"] = dtx2.Rows[0]["CWAREID"].ToString();
                    dr["订单数量"] = dtx1.Rows[i]["OCOUNT"].ToString();
                    dr["累计销货数量"] = 0;
                    dr["累计销退数量"] = 0;
                    dr["交货日期"] = dtx1.Rows[i]["DELIVERYDATE"].ToString();
                    if (dtx1.Rows[i]["SELLTABLESTATUS_DET"].ToString() == "OPEN")
                    {
                        dr["状态"] = "OPEN";
                    }
                    else if (dtx1.Rows[i]["SELLTABLESTATUS_DET"].ToString() == "PROGRESS")
                    {
                        dr["状态"] = "部分出货";
                    }
                    else if (dtx1.Rows[i]["SELLTABLESTATUS_DET"].ToString() == "DELAY")
                    {
                        dr["状态"] = "DELAY";
                    }
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
        #region GET_SELLTABLE_PROGRESS_COUNT
        public string GET_SELLTABLE_PROGRESS_COUNT(string WAREID,string SEKEY)
        {
            string v = "0";
            DataView dv = new DataView(GET_TOTAL_SELLTABLE());
            dv.RowFilter = "状态 NOT IN ('已出货') AND ID='" + WAREID + "' AND 索引 NOT IN ('"+SEKEY +"')";
            DataTable dt = dv.ToTable();
            if (dt.Rows.Count > 0)
            {

                v = dt.Compute("SUM(订单未结数量)", "").ToString();

            }
            return v;
        }
        #endregion
    
        #region JUAGE_REALTY_IFHAVE_SELLCOUNT
        public bool  JUAGE_REALTY_IFHAVE_SELLCOUNT(string ORID)
        {
            bool b = false;
            DataView dv = new DataView(GET_TOTAL_SELLTABLE());
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
        #region JUAGE_SELLTABLE_IF_HAVE_NO_AUDIT
        public bool JUAGE_SELLTABLE_IF_HAVE_NO_AUDIT(string ORID)
        {
            bool b = false;
            string s2 = bc.getOnlyString("SELECT IF_AUDIT FROM SELLTABLE_MST WHERE ORID='" +ORID  + "'");
            if (s2 != "Y")
            {
                b = true;
                ErrowInfo = "此订单未审核，不能进行相关操作！";
            }
            return b;
        }
        #endregion
        #region  JUAGE_RESIDUE_SECOUNT_IF_LESSTHAN_SR_COUNT
        public bool JUAGE_RESIDUE_SECOUNT_IF_LESSTHAN_SR_COUNT(string SEID)
        {
            bool b = false;
            DataTable dt = bc.getdt(sqlo + " WHERE A.SEID='" + SEID + "'");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ORID = dr["订单号"].ToString();
                    SN = dr["项次"].ToString();
                    decimal d1 = decimal.Parse(dr["销货数量"].ToString());
                    decimal d = 0;
                    decimal d2 = 0;
                    DataView dv = new DataView(corder.GET_TOTAL_ORDER());
                    dv.RowFilter = "订单号='" + ORID + "' AND 项次='" + SN + "'";
                    DataTable dtx = dv.ToTable();
                    if (dtx.Rows.Count > 0)
                    {

                        d = decimal.Parse(dtx.Rows[0]["累计销货数量"].ToString());
                        d2 = decimal.Parse(dtx.Rows[0]["累计销退数量"].ToString());
                        if (d - d1 < d2)
                        {
                            b = true;
                            ErrowInfo = "项次:" + SN + " 累计销货数量：" + d.ToString("#0.00") +
                                "与删除的销货数量：" + d1.ToString("#0.00") + "差值：" + (d - d1).ToString("#0.00") +
                                "小于该项次的累计销退数量：" + d2.ToString("0.00") + "，不允许编辑或删除该单据";
                            break;
                        }
                    }
                }
            }
            return b;
        }
        #endregion
        #region save
        public DataTable save(DataTable dt)//返回第一次执行后订单的项次最新库存数量与累计销货数量值
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss").Replace("-", "/");
            if (!bc.exists("SELECT SEID FROM SELLTABLE_DET WHERE SEID='" + SEID + "'"))
            {
                SQlcommandE_DET(dt, sqlo);
                SQlcommandE_MST(sqlt);
                IFExecution_SUCCESS = true;
                corder.UPDATE_ORDER_STATUS(ORID);
            }
            else
            {
                SQlcommandE_DET(dt, sqlo);
                SQlcommandE_MST(sqlth+" WHERE SEID='"+SEID+"'");
                IFExecution_SUCCESS = true;
                corder.UPDATE_ORDER_STATUS(ORID);
            }
            return dt;
        }
        #endregion
        #region SQlcommandE_DET
        protected void SQlcommandE_DET(DataTable dt, string sql)
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss").Replace("-", "/");
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["销货数量"].ToString() == "0" || dt.Rows[i]["销货数量"].ToString() == "0.00")
                {

                }
                else
                {
                    SqlConnection sqlcon = bc.getcon();
                    sqlcon.Open();
                    SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
                    SEKEY = bc.numYMD(20, 12, "000000000001", "SELECT * FROM SELLTABLE_DET", "SEKEY", "SE");
                    sqlcom.Parameters.Add("@SEKEY", SqlDbType.VarChar, 20).Value = SEKEY;
                    sqlcom.Parameters.Add("@SEID", SqlDbType.VarChar, 20).Value = SEID;
                    sqlcom.Parameters.Add("@ORID", SqlDbType.VarChar, 20).Value = ORID;
                    sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = dt.Rows[i]["项次"].ToString();
                    sqlcom.Parameters.Add("@MAKERID", SqlDbType.VarChar, 20).Value = MAKERID;
                    sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                    sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
                    sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
                    sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
                    sqlcom.ExecuteNonQuery();

                    ORKEY = bc.getOnlyString("SELECT ORKEY FROM ORDER_DET WHERE ORID='" + ORID  +
                    "' AND SN='" + dt.Rows[i]["项次"].ToString() + "'");
                    DataTable dtx6 = bc.getmaxstoragecount(dt.Rows[i]["型号"].ToString());
                    if (dtx6.Rows.Count > 0)
                    {
                        dt.Rows[i]["识别码"] = dtx6.Rows[0]["识别码"].ToString();
                        dt.Rows[i]["库存数量"] = dtx6.Rows[0]["库存数量"].ToString();
                    }
                    sqlcom = new SqlCommand(sqlf, sqlcon);
                    sqlcom.Parameters.Add("@MRKEY", SqlDbType.VarChar, 20).Value = SEKEY;
                    sqlcom.Parameters.Add("@MATEREID", SqlDbType.VarChar, 20).Value = SEID;
                    sqlcom.Parameters.Add("@SN", SqlDbType.VarChar, 20).Value = dt.Rows[i]["项次"].ToString();
                    sqlcom.Parameters.Add("@MRCOUNT", SqlDbType.VarChar, 20).Value = dt.Rows[i]["销货数量"].ToString();
                    sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = bc.getOnlyString("SELECT WAREID FROM WAREINFO WHERE MODEL='" + dt.Rows[i]["型号"].ToString() + "'");
                    sqlcom.Parameters.Add("@STORAGEID", SqlDbType.VarChar, 20).Value = bc.getOnlyString("SELECT PUID FROM ORDER_MST WHERE ORID='" + dt.Rows[i]["订单号"].ToString() + "'");
                    sqlcom.Parameters.Add("@BATCHID", SqlDbType.VarChar, 20).Value = dt.Rows[i]["识别码"].ToString();
                    sqlcom.Parameters.Add("@ORKEY", SqlDbType.VarChar, 20).Value = ORKEY;
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
            sqlcom.Parameters.Add("SEID", SqlDbType.VarChar, 20).Value = SEID;
            sqlcom.Parameters.Add("SELLDATE", SqlDbType.VarChar, 20).Value =SELLDATE ;
            sqlcom.Parameters.Add("SELLERID", SqlDbType.VarChar, 20).Value = SELLERID;
            sqlcom.Parameters.Add("SEND_ADDRESS", SqlDbType.VarChar, 100).Value = SEND_ADDRESS;
            sqlcom.Parameters.Add("CONTACT", SqlDbType.VarChar, 20).Value = CONTACT;
            sqlcom.Parameters.Add("PHONE", SqlDbType.VarChar, 20).Value =PHONE;
            sqlcom.Parameters.Add("REMARK", SqlDbType.VarChar, 1000).Value = REMARK;
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
            dt.Columns.Add("型号", typeof(string));
            dt.Columns.Add("品名", typeof(string));
            dt.Columns.Add("材料", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("交货日期", typeof(string));
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
            dt.Columns.Add("订单日期", typeof(string));
            dt.Columns.Add("客户订单号", typeof(string));
            dt.Columns.Add("型号", typeof(string));
            dt.Columns.Add("品名", typeof(string));
            dt.Columns.Add("材料", typeof(string));
            dt.Columns.Add("数量", typeof(string));
            dt.Columns.Add("单位", typeof(string));
            dt.Columns.Add("交货日期", typeof(string));
            return dt;
        }
        #endregion
    
        #region ExcelPrint
        public void ExcelPrint(DataTable dt, string BillName, string Printpath)
        {
            if (dt.Rows.Count > 0)
            {
                //根据要打印的行数求出一共要几张A4纸，每张A4纸打印5个项
                    decimal totalcount = Math.Ceiling(decimal.Parse(dt.Rows.Count.ToString()) / 5);
                    int i = 0;
                    int i1=0;
                    for (int z = 0; z < totalcount; z++)
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
                        application.Visible = true;
                        application.ExtendList = false;
                        application.DisplayAlerts = false;
                        application.AlertBeforeOverwriting = false;
                        int count = 0;
                        int j = 0;
                        for (i =i1 ; i < dt.Rows.Count; i++)
                        {
                          
                            if (count == 5)
                            {
                                i1 = count;
                                break;
                            }
                            else
                            {
                                worksheet.Cells[6, "C"] = dt.Rows[i]["客户名称"].ToString ();
                                worksheet.Cells[6, "J"] = dt.Rows[i]["销货单号"].ToString ();
                                worksheet.Cells[7, "C"] = dt.Rows[i]["送货地址"].ToString ();
                                worksheet.Cells[7, "J"] = dt.Rows[i]["联系人"].ToString ();
                                worksheet.Cells[8, "C"] = dt.Rows[i]["联系电话"].ToString ();
                                worksheet.Cells[8, "J"] = dt.Rows[i]["销货日期"].ToString ();
                                worksheet.Cells[9, "C"] = dt.Rows[i]["订单号"].ToString();
                            
                                worksheet.Cells[12 + 2 * j, "A"] = dt.Rows[i]["序号"].ToString ();
                                worksheet.Cells[12 + 2 * j, "B"] = dt.Rows[i]["型号"].ToString();
                                worksheet.Cells[12 + 2 * j, "D"] = dt.Rows[i]["品名"].ToString();
                                worksheet.Cells[12 + 2 * j, "E"] = dt.Rows[i]["材料"].ToString();
                                worksheet.Cells[12 + 2 * j, "G"] = dt.Rows[i]["销货数量"].ToString ();
                                worksheet.Cells[12 + 2 * j, "I"] = dt.Rows[i]["单位"].ToString();
                                worksheet.Cells[12 + 2 * j, "J"] = dt.Rows[i]["识别码"].ToString();
                                worksheet.Cells[30, "C"] = dt.Rows[i]["销货员"].ToString();
                                j = j + 1;
                                count = count + 1;
                            }

                        }
                        //worksheet.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
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
                        GC.Collect();
                        ErrowInfo = "打印数据已发出";*/
                      
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
            sqlcom.Parameters.Add("SEKEY", SqlDbType.VarChar, 20).Value = SEKEY;
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
