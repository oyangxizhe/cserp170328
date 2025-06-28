using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using XizheC;


namespace CSPSS.SELL_MANAGE
{
    public partial class SELLRETURNT : Form
    {

        basec bc = new basec();
        CCUSTOMER_INFO ccustomer_info = new CCUSTOMER_INFO();
        CMISC_STORAGE cmisc_storage = new CMISC_STORAGE();
        CSELLRETURN cSELLRETURN = new CSELLRETURN();
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dtx = new DataTable();
        string varDate = DateTime.Now.ToString("yyy/MM/dd").Replace("-", "/");
        SELLRETURN F1= new SELLRETURN();
        CORDER corder = new CORDER();
        protected int i, j;
        #region nature
        private static string _CUID;
        public static string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }
        }
        private static string _MATERIAL;
        public static string MATERIAL
        {
            set { _MATERIAL = value; }
            get { return _MATERIAL; }
        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }
        }
        private static bool _IF_DOUBLE_CLICK;
        public static bool IF_DOUBLE_CLICK
        {
            set { _IF_DOUBLE_CLICK = value; }
            get { return _IF_DOUBLE_CLICK; }
        }
        private string _SKU;
        public string SKU
        {
            set { _SKU = value; }
            get { return _SKU; }
        }
        private string _IDO;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }
        }
        private string _MGKEY;
        public string MGKEY
        {
            set { _MGKEY = value; }
            get { return _MGKEY; }
        }
        private string _COUNT;
        public string COUNT
        {
            set { _COUNT = value; }
            get { return _COUNT; }
        }
        private string _ADD_OR_UPDATE;
        public string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }
        }
        private string _WEIGHT;
        public string WEIGHT
        {
            set { _WEIGHT = value; }
            get { return _WEIGHT; }
        }
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
        private string _NOSECOUNT;
        public string NOSECOUNT
        {
            set { _NOSECOUNT = value; }
            get { return _NOSECOUNT; }
        }
#endregion
        DataTable dtx1= new DataTable();
        DataTable dtx2 = new DataTable();
        CMOLD_BASE cmold_base = new CMOLD_BASE();
        public SELLRETURNT()
        {
            InitializeComponent();
        }
        public SELLRETURNT(SELLRETURN FRM)
        {
            InitializeComponent();
            F1 = FRM;
        }
        private void SELLRETURNT_Load(object sender, EventArgs e)
        {
            
            textBox50.BackColor = CCOLOR.lylfnp;
            this.Icon =  Resource1.xz_200X200;
            textBox1.Text = IDO;
            comboBox2.Text = ORID;
            bind();
            try
            {
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        #region bind
        protected void bind()
        {
           
            comboBox2.BackColor = CCOLOR.YELLOW;
            hint.ForeColor = Color.Red;
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Text = "";
            }
          
           dt = ask(textBox1.Text, comboBox2.Text);

           DataTable dtx4 = basec.getdts(@"
SELECT 
A.SRID,
A.ORID,
SUM(C.GECount*B.PRICE)
FROM SELLRETURN_DET A 
LEFT JOIN ORDER_DET B ON A.ORID=B.ORID AND A.SN=B.SN
LEFT JOIN Gode  C ON A.SRKEY=C.GEKEY 
WHERE A.SRID='" + textBox1 .Text + "' AND A.ORID='" + comboBox2.Text + "' GROUP BY A.ORID,A.SRID ");

            if (dtx4.Rows.Count > 0)
            {
                string v8 = dtx4.Rows[0][2].ToString();
                textBox50.Text= string.Format("{0:F2}", Convert.ToDouble(v8));
           
            }
            else
            {
                textBox50.Text = "";
             
            }
  
            if (dt.Rows.Count > 0)
            {
              
                dataGridView1.DataSource = dt;
                dgvStateControl();
            }
            else
            {
                dataGridView1.DataSource = null;
            }
            dt1 = bc.getdt(cSELLRETURN.sql + " WHERE 销退单号='" + IDO + "'");
            if (dt1.Rows.Count > 0)
            {
                dateTimePicker1.Text = dt1.Rows[0]["销退日期"].ToString();
                textBox3.Text = dt1.Rows[0]["备注"].ToString();
            }
            dataGridView2.DataSource = dt1;
            dgvStateControl_2();
        }
        #endregion
     
        #region ask
        private DataTable ask(string SRID, string ORID)
        {
           
            DataTable dtt = new DataTable();
            dtt.Columns.Add("订单号", typeof(string));
            dtt.Columns.Add("项次", typeof(string));
            dtt.Columns.Add("型号", typeof(string));
            dtt.Columns.Add("品名", typeof(string));
            dtt.Columns.Add("下单日期", typeof(string));
            dtt.Columns.Add("单价", typeof(decimal));
            dtt.Columns.Add("订单数量", typeof(decimal));
            dtt.Columns.Add("累计销货数量", typeof(decimal));
            dtt.Columns.Add("累计销退数量", typeof(decimal));
            dtt.Columns.Add("可销退数量", typeof(decimal), "累计销货数量-累计销退数量");
            dtt.Columns.Add("识别码", typeof(string));
   
            dtt.Columns.Add("销退数量", typeof(decimal));
            dtt.Columns.Add("本销退单累计销退数量", typeof(decimal));
            //dtt.Columns.Add("金额", typeof(decimal), "单价*基数*销货数量");
            dtt.Columns.Add("金额", typeof(decimal));
            dtx1 = bc.getdt(corder.sql +" WHERE A.ORID='" + ORID + "'");
          
            if (dtx1.Rows.Count > 0)
            {
                comboBox1.Text = dtx1.Rows[0]["供应商ID"].ToString();
                textBox2.Text = dtx1.Rows[0]["客户名称"].ToString();
               
                for (i = 0; i < dtx1.Rows.Count; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dr["订单号"] = dtx1.Rows[i]["订单号"].ToString();
                    dr["项次"] = dtx1.Rows[i]["项次"].ToString();
                    dr["型号"] = dtx1.Rows[i]["型号"].ToString();
                    dr["识别码"] = dtx1.Rows[i]["识别码"].ToString();
                    if (!string.IsNullOrEmpty(dtx1.Rows[i]["单价"].ToString()))
                    {
                        dr["单价"] = dtx1.Rows[i]["单价"].ToString();
                    }
                    else
                    {
                        dr["单价"] = DBNull.Value;
                    }
                    dr["订单数量"] = dtx1.Rows[i]["数量"].ToString();
                    dr["型号"] = dtx1.Rows[i]["型号"].ToString();
                    dr["品名"] = dtx1.Rows[i]["品名"].ToString();
                    dr["下单日期"] = dtx1.Rows[i]["下单日期"].ToString();
                    dr["累计销货数量"] = 0;
                    dr["累计销退数量"] = 0;
                    dr["本销退单累计销退数量"] = 0;
                    dtt.Rows.Add(dr);
                    SKU = dtx1.Rows[i]["单位"].ToString();
                
                }
            }
         
            DataTable dtx4 = bc.getdt(@"
SELECT
A.ORID AS ORID,
A.SN AS SN,
CAST(ROUND(SUM(B.MRCOUNT),2) AS DECIMAL(18,2)) AS MRCOUNT 
FROM SELLTABLE_DET A 
LEFT JOIN MATERE 
B ON A.SEKEY=B.MRKEY  
WHERE  A.ORID='" + ORID  + "' GROUP BY A.ORID,A.SN");
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
            DataTable dtx5 = bc.getdt(@"
SELECT 
A.ORID AS ORID,
A.SRID AS SRID,
A.SN AS SN,
CAST(ROUND(SUM(B.GECOUNT),2) AS DECIMAL(18,2)) AS GECOUNT
FROM SELLRETURN_DET A 
LEFT JOIN  GODE B ON A.SRKEY=B.GEKEY 
WHERE  A.ORID='" + ORID  + "' AND A.SRID='" + SRID  + "' GROUP BY A.ORID,A.SRID,A.SN");
            if (dtx5.Rows.Count > 0)
            {
                for (i = 0; i < dtx5.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["订单号"].ToString() == dtx5.Rows[i]["ORID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx5.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["本销退单累计销退数量"] = dtx5.Rows[i]["GECOUNT"].ToString();
                            break;
                        }

                    }
                }

            }
            DataTable dtx7 = bc.getdt(@"
SELECT 
A.ORID AS ORID,
A.SN AS SN,
SUM(B.GECOUNT) AS GECOUNT
FROM SELLRETURN_DET A 
LEFT JOIN GODE B ON A.SRKEY=B.GEKEY  
GROUP BY 
A.ORID,
A.SN
");
            if (dtx7.Rows.Count > 0)
            {
                for (i = 0; i < dtx7.Rows.Count; i++)
                {
                    for (j = 0; j < dtt.Rows.Count; j++)
                    {
                        if (dtt.Rows[j]["订单号"].ToString() == dtx7.Rows[i]["ORID"].ToString() && dtt.Rows[j]["项次"].ToString() == dtx7.Rows[i]["SN"].ToString())
                        {
                            dtt.Rows[j]["累计销退数量"] = dtx7.Rows[i]["GECOUNT"].ToString();
                            break;
                        }

                    }
                }

            }
            foreach (DataRow dr in dtt.Rows )
            {
                decimal d1 = 0,d4=0;
                if (!string.IsNullOrEmpty(dr["单价"].ToString()))
                {
                    dr["单价"] = dr["单价"].ToString();
                    d1 = decimal.Parse(dr["单价"].ToString());
                }
                else
                {
                    dr["单价"] = DBNull.Value;
                }

              
                dr["销退数量"] = dr["可销退数量"].ToString();
                if (!string.IsNullOrEmpty(dr["销退数量"].ToString()))
                {
                    dr["销退数量"] = dr["销退数量"].ToString();
                    d4 = decimal.Parse(dr["销退数量"].ToString());
                }
                else
                {
                    dr["销货数量"] = DBNull.Value;
                }
              
                dr["金额"] = (d1 * d4).ToString("0.00");
        
            }
            return dtt;
        }
        #endregion
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            int numCols1 = dataGridView1.Columns.Count;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;/*自动调整DATAGRIDVIEW的列宽*/
            for (i = 0; i < numCols1; i++)
            {
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                //this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.Lavender;
                dataGridView1.Columns[i].ReadOnly = true;
            }

            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.OldLace;
              
                i = i + 1;

            }
   
            dataGridView1.Columns["项次"].ReadOnly = true;
            dataGridView1.Columns["单价"].ReadOnly = false;
            dataGridView1.Columns["识别码"].ReadOnly = false;
            dataGridView1.Columns["销退数量"].ReadOnly = false;
            dataGridView1.Columns["项次"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["销退数量"].DefaultCellStyle.BackColor = CCOLOR.YELLOW;
            dataGridView1.Columns["销退数量"].DefaultCellStyle.BackColor = CCOLOR.YELLOW;
            dataGridView1.Columns["单价"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["订单数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["销退数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["累计销货数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["累计销退数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["可销退数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["本销退单累计销退数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
 
        }
        #endregion
        #region dgvStateControl_2
        private void dgvStateControl_2()
        {
            int i;
            dataGridView2.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            int numCols1 = dataGridView2.Columns.Count;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;/*自动调整DATAGRIDVIEW的列宽*/
            for (i = 0; i < numCols1; i++)
            {
                dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                //this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.EnableHeadersVisualStyles = false;
                dataGridView2.Columns[i].HeaderCell.Style.BackColor = Color.Lavender;
                dataGridView2.Columns[i].ReadOnly = true;
            }
            for (i = 0; i < dataGridView2.Columns.Count; i++)
            {
                dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView2.Columns[i].DefaultCellStyle.BackColor = Color.OldLace;
                i = i + 1;
            }
            dataGridView2.Columns["序号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns["单价"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["订单数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["销退数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
          
            dataGridView2.Columns["订单销退金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["累计销货"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["累计销退"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns["可销退数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
           
        }
        #endregion
        #region save

        #endregion
        #region override enter
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter &&
             (
             (
              !(ActiveControl is System.Windows.Forms.TextBox) ||
              !((System.Windows.Forms.TextBox)ActiveControl).AcceptsReturn)
             )
             )
            {
                SendKeys.SendWait("{Tab}");
                return true;
            }
            if (keyData == (Keys.Enter | Keys.Shift))
            {
                SendKeys.SendWait("+{Tab}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
        #region dgvcellclick
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }
        #endregion

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            string varDate = DateTime.Now.ToString("yyy/MM/dd").Replace("-", "/");
            try
            {
                int a = dataGridView1.CurrentCell.ColumnIndex;
                int b = dataGridView1.CurrentCell.RowIndex;
                int c = dataGridView1.Columns.Count - 1;
                int d = dataGridView1.Rows.Count - 1;
                if (a == c && b == d)
                {
                    if (dt.Rows.Count >= 1)
                    {

                        DataRow dr = dt.NewRow();
                        int b1 = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["项次"].ToString());
                        dr["项次"] = Convert.ToString(b1 + 1);
                        dr["交货日期"] = varDate;
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }

        }
        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            MessageBox.Show("数值型数据只能输入数字", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

        private void add()
        {
            ClearText();
            IDO = cSELLRETURN.GETID();
            textBox1.Text = IDO;
           
            bind();
            dataGridView1.DataSource = null;
            comboBox1.Focus();
        }
        public void ClearText()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            add();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
  
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            IF_DOUBLE_CLICK = false;
            USER_MANAGE.USER_INFO FRM = new USER_MANAGE.USER_INFO();
            FRM.GET_DATA_INT = 1;
            FRM.ShowDialog();
            this.comboBox1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.comboBox1.DroppedDown = false;//使组合框不显示其下拉部分
            this.comboBox1.IntegralHeight = true;//恢复默认值
            if (FRM.UNAME != "")
            {
                comboBox1.Text = FRM.UNAME;
            }
            comboBox2.Focus();
        }
        #region juage
        private bool juage()
        {
            bool b = false;
            if (IDO == "")
            {
                hint.Text = "编号不能为空！";
                b = true;
            }
            else if (ac0(IDO, comboBox2.Text))
            {
                b = true;
            }
            else if (!bc.exists("SELECT * FROM ORDER_MST WHERE ORID='" + comboBox2.Text + "'"))
            {
                hint.Text = "订单号为空或不存在于系统中！";
                b = true;
            }
            else if (bc.exists("SELECT * FROM RECEIVABLES_MST WHERE ORID='" + comboBox2.Text + "'"))
            {
                hint.Text = string.Format("订单号: {0} 已经存在应收款记录,不允许新增修改或是删除！", comboBox2.Text);
                b = true;
            }
            /*if (cSELLRETURN.JUAGE_RESIDUE_SECOUNT_IF_LESSTHAN_SR_COUNT(comboBox2.Text ))
            {

                hint.Text  = cSELLRETURN.ErrowInfo;
                b = true;
            }*/
       
            else if (juage2())
            {
                b = true;
            }
            return b;
        }
        #endregion
        #region juage2()
        private bool juage2()
        {
            bool b = false;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["销退数量"].ToString() == "0" || dr["销退数量"].ToString() == "0.00")//销退数量为0不做处理，即此项不用销退
                {

                }
                else
                {
                
                    string SECOUNT = dr["销退数量"].ToString();
                    string BATCHID = dr["识别码"].ToString();
                    string NOSECOUNT = "";
                   // MessageBox.Show(dr["销退数量"].ToString() + "," + (decimal.Parse(SECOUNT)).ToString());
                    /*避免单据未保存前，此入库单在别的电脑修改过库存数量,销过货，引起库存数量，未销退数量不对,再次更新数据为最新 start 161120*/


             
                    //MessageBox.Show(dr["型号"].ToString() + "," + STORAGECOUNT);
                    DataTable dtx4 = bc.getdt(@"
SELECT
A.ORID AS ORID,
A.SN AS SN,
B.WAREID AS WAREID,
CAST(ROUND(SUM(B.GECOUNT),2) AS DECIMAL(18,2)) AS GECOUNT 
FROM SELLRETURN_DET A 
LEFT JOIN GODE 
B ON A.SRKEY=B.GEKEY  
WHERE  A.ORID='" + dr["订单号"].ToString() + "' AND A.SN='" + dr["项次"].ToString() + "' GROUP BY A.ORID,A.SN,B.WAREID");
                    if (dtx4.Rows.Count > 0)
                    {
                        dr["累计销退数量"] = dtx4.Rows[0]["GECOUNT"].ToString();

                    }
                    else
                    {
                        dr["累计销退数量"] = "0";

                    }
                    NOSECOUNT = dr["可销退数量"].ToString();
                    /*避免单据未保存前，此入库单在别的电脑修改过库存数量,销过货，引起库存数量，未销退数量不对,再次更新数据为最新 end 161120*/
                    string GET_ORKEY = bc.getOnlyString("SELECT ORKEY FROM ORDER_BARCODE WHERE BARCODE='" + BATCHID + "'");

                    if (SECOUNT == "")
                    {
                        hint.Text = string.Format("订单号：{0} 与项次：{1} 销退数量不能为空！",
                         dr["订单号"].ToString(), dr["项次"].ToString());
                        b = true;
                        break;
                    }
                    else if (bc.yesno(SECOUNT) == 0)
                    {
                        hint.Text = "数量只能输入数字！";
                        b = true;
                        break;
                    }
                    else if (decimal.Parse(SECOUNT) == 0)
                    {
                        hint.Text = "销退数量不能为0！";
                        b = true;
                        break;
                    }
                    else if (decimal.Parse(SECOUNT) > decimal.Parse(NOSECOUNT))
                    {
                        hint.Text = string.Format("订单号：{0} 与项次：{1} 销退数量不能大于可销退数量！",
                            dr["订单号"].ToString(), dr["项次"].ToString());
                        b = true;
                        break;
                    }
                   /* else if (decimal.Parse(SECOUNT) != decimal.Parse(NOSECOUNT))
                    {
                        hint.Text = string.Format("订单号：{0} 与项次：{1} 销退数量需等于未销退数量！",
                            dr["订单号"].ToString(), dr["项次"].ToString());
                        b = true;
                        break;
                    }*/
                }
            }
            return b;
        }
        #endregion
        private bool ac0(string SRID, string ORID)
        {
            bool c = false;
            if (bc.exists("SELECT * FROM SELLRETURN_DET WHERE SRID='" + SRID + "'"))
            {
                string s3 = bc.getOnlyString("SELECT ORID FROM SELLRETURN_DET WHERE SRID='" + SRID + "'");
                if (s3 != ORID)
                {
                    hint.Text  = "同一个发货单下面只能出现一个订单号!";
                    c = true;
                }
            }
            return c;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            IFExecution_SUCCESS = false;
            hint.Text = "";
            btnSave.Focus();
            
            if (juage())
            {

            }
            else
            {
                save();
            }
            try
            {
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        #region save
        private void save()
        {

            btnSave.Focus();
            cSELLRETURN.MAKERID = LOGIN.USID;
            cSELLRETURN.ORID = comboBox2.Text;
            cSELLRETURN.STORAGEID = comboBox1.Text;
            cSELLRETURN.SRID = IDO;
            cSELLRETURN.CNAME = comboBox1.Text;
            cSELLRETURN.SELLRETURN_DATE = dateTimePicker1.Text;
            cSELLRETURN.REMARK = textBox3.Text;
            cSELLRETURN.SEND_ADDRESS = "";
            cSELLRETURN.CONTACT = "";
            cSELLRETURN.PHONE = "";
            dt=cSELLRETURN.save(dt);
            IFExecution_SUCCESS = cSELLRETURN.IFExecution_SUCCESS;
            hint.Text = cSELLRETURN.ErrowInfo;
            if (IFExecution_SUCCESS)
            {
                if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
                {

                    hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
                }
                else
                {
                    hint.Text = "";
                }
                bind();
                F1.bind();
            }
        }
        #endregion
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要删除该条凭证吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (bc.exists("SELECT * FROM RECEIVABLES_MST WHERE ORID='" + comboBox2.Text + "'"))
                    {
                        hint.Text = string.Format("订单号: {0} 已经存在应收款记录,不允许新增修改或是删除！", comboBox2.Text);
                   
                    }
                    else
                    {
                        basec.getcoms("DELETE SELLRETURN_MST WHERE SRID='" + textBox1.Text + "'");
                        basec.getcoms("DELETE SELLRETURN_DET WHERE SRID='" + textBox1.Text + "'");
                        basec.getcoms("DELETE GODE WHERE GODEID='" + textBox1.Text + "'");
                        corder.UPDATE_ORDER_STATUS(comboBox2.Text);
                        ClearText();
                        bind();
                        F1.bind();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
       
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        { 
            int i=dataGridView1 .CurrentCell .RowIndex ;
            if (dataGridView1["单价", i].FormattedValue.ToString() != "" && 
               
                 dataGridView1["销退数量", i].FormattedValue.ToString() != "")
            {
                dataGridView1["金额", i].Value = 
                    (decimal.Parse(dataGridView1["单价", i].FormattedValue.ToString()) *
                   
                    decimal.Parse(dataGridView1["销退数量", i].FormattedValue.ToString())).ToString ("0.00");
               
            }
            textBox50.Text = dt.Compute("SUM(金额)", "").ToString();
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            
     
            try
            {
                IF_DOUBLE_CLICK = false;
                SELL_MANAGE.ORDER FRM = new ORDER();
                FRM.UNAME = comboBox1.Text;
                FRM.SELECT = 1;
                FRM.ShowDialog();
                this.comboBox2.IntegralHeight = false;//使组合框不调整大小以显示其所有项
                this.comboBox2.DroppedDown = false;//使组合框不显示其下拉部分
                this.comboBox2.IntegralHeight = true;//恢复默认值
                if (FRM.ORID != "" && IF_DOUBLE_CLICK==true )
                {
                    comboBox2.Text = FRM.ORID;

                    textBox2.Text = FRM.CNAME;
                    this.ActiveControl.TabIndex = 5;

                }
                bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
 
        }
     

        private void comboBox4_DropDown(object sender, EventArgs e)
        {
          
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            dtx = bc.getdt(corder.sql + " WHERE A.ORID='" + comboBox2.Text + "'");
            if (dtx.Rows.Count > 0)
            {
               
                comboBox1.Text = dtx.Rows[0]["客户名称"].ToString();
            }
            else
            {
           
                comboBox1.Text = "";
            }
            bind();
            try
            {
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            cSELLRETURN.MAKERID = "";
           
            //cSELLRETURN.ExcelPrint(dt1, "销货单", System.IO.Path.GetFullPath("销货单.xls"));
            //corder.ExcelPrint_40X30(dataGridView1, "订单", System.IO.Path.GetFullPath("订单40X30.xlsx"));
            hint.Text = cSELLRETURN.ErrowInfo;
            try
            {
               
            }
            catch (Exception MyEx)
            {
                MessageBox.Show(MyEx.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
