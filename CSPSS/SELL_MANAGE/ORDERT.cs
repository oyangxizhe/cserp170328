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
    public partial class ORDERT : Form
    {

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
        basec bc = new basec();
        CCUSTOMER_INFO ccustomer_info = new CCUSTOMER_INFO();
        CMISC_STORAGE cmisc_storage = new CMISC_STORAGE();
        CORDER corder = new CORDER();
        DataTable dt = new DataTable();
        DataTable dtx = new DataTable();
        string varDate = DateTime.Now.ToString("yyy/MM/dd").Replace("-", "/");
        ORDER F1= new ORDER();
        protected int i, j;
        public ORDERT()
        {
            InitializeComponent();
        }
        public ORDERT(ORDER FRM)
        {
            InitializeComponent();
            F1 = FRM;
        }
        private void ORDERT_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn d1 = new DataGridViewTextBoxColumn();
            d1.Name = "项次";
            d1.HeaderText = "项次";
            dataGridView1.Columns.Add(d1);
            DataGridViewTextBoxColumn d2 = new DataGridViewTextBoxColumn();
            d2.Name = "产品分类";
            d2.HeaderText = "产品分类";
            dataGridView1.Columns.Add(d2);

            DataGridViewTextBoxColumn d4 = new DataGridViewTextBoxColumn();
            d4.Name = "品名";
            d4.HeaderText = "品名/Name";
            dataGridView1.Columns.Add(d4);
            DataGridViewTextBoxColumn d5 = new DataGridViewTextBoxColumn();
            d5.Name = "型号";
            d5.HeaderText = "型号/Model";
            dataGridView1.Columns.Add(d5);
            DataGridViewTextBoxColumn d6 = new DataGridViewTextBoxColumn();
            d6.Name = "单价";
            d6.HeaderText = "单价/Price";
            dataGridView1.Columns.Add(d6);
            DataGridViewTextBoxColumn d7 = new DataGridViewTextBoxColumn();
            d7.Name = "数量";
            d7.HeaderText = "数量/Qty";
            dataGridView1.Columns.Add(d7);
     
            try
            {
                this.Icon =  Resource1.xz_200X200;
                textBox1.Text = IDO;
               
                bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        #region bind
        private void bind()
        {
            dataGridView1.Rows.Clear();
            textBox2.Focus();
            comboBox2.BackColor = CCOLOR.YELLOW;
            textBox2.BackColor = CCOLOR.YELLOW;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            hint.Location = new Point(256, 136);
            hint.ForeColor = Color.Red;
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Text = "";
            }
            dt = basec.getdts(corder.sql + " where A.ORID='" + IDO + "' ORDER BY  A.ORID ASC ");
            if (dt.Rows.Count > 0)
            {
             
                textBox2.Text = dt.Rows[0]["客户名称"].ToString();
                dateTimePicker1.Text = dt.Rows[0]["下单日期"].ToString();
               comboBox2.Text  = dt.Rows[0]["供应商ID"].ToString();
            
             
             for(int i=0;i<dt.Rows.Count ;i++)
                {
                    DataGridViewRow dar = new DataGridViewRow();
                    dataGridView1.Rows.Add(dar);
                    dataGridView1["项次", i].Value = dt.Rows[i]["项次"].ToString();
                    dataGridView1["产品分类", i].Value = dt.Rows[i]["产品分类"].ToString();
                    dataGridView1["品名", i].Value = dt.Rows[i]["品名"].ToString();
                    dataGridView1["型号", i].Value = dt.Rows[i]["型号"].ToString();
                    dataGridView1["单价", i].Value = dt.Rows[i]["单价"].ToString();
                    dataGridView1["数量", i].Value = dt.Rows[i]["数量"].ToString();
                   
                }

              
            }
            else
            {
                
                DataGridViewRow dar = new DataGridViewRow();
                dataGridView1.Rows.Add(dar);
                dataGridView1["项次", 0].Value = "1";
            }
          
            dgvStateControl();
        }
        #endregion
        #region total1
        private DataTable total()
        {
            DataTable dtt2 = corder.GetTableInfo();
            for (i = 1; i <= 6; i++)
            {
                DataRow dr = dtt2.NewRow();
                dr["项次"] = i;
                dr["订单交期"] = varDate;
                dtt2.Rows.Add(dr);
            }
            return dtt2;
        }
        #endregion
        #region dgvStateControl
        private void dgvStateControl()
        {

            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            int numCols1 = dataGridView1.Columns.Count;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;/*自动调整DATAGRIDVIEW的列宽*/
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                //this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.Lavender;

            }

            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.OldLace;

                i = i + 1;

            }
            dataGridView1.Columns["项次"].ReadOnly = true;
            dataGridView1.Columns["项次"].Width = 40;
            dataGridView1.Columns["项次"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["单价"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["数量"].DefaultCellStyle.BackColor = CCOLOR.CUSTOMER_YELLOW;

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

            IF_DOUBLE_CLICK = false;
            int rows = dataGridView1.CurrentCell.RowIndex;
            int columns = dataGridView1.CurrentCell.ColumnIndex;
            if (dataGridView1.Columns[columns].Name.ToString() == "产品分类")
            {

                BASE_INFO.TYPE FRM = new BASE_INFO.TYPE();
                FRM.SELECT = 1;
                FRM.ShowDialog();
                if (FRM.TYPE_VALUE != "")
                {
                    dataGridView1["产品分类", rows].Value = FRM.TYPE_VALUE;
                    dataGridView1.CurrentCell = dataGridView1["型号", rows];
                }
            }
            else if (dataGridView1.Columns[columns].Name.ToString() == "型号")
            {

                BASE_INFO.PRODUCT FRM = new BASE_INFO.PRODUCT();
                FRM.PRODUCT_TYPE = dataGridView1["产品分类", rows].FormattedValue.ToString();
                FRM.SELECT = 1;
                FRM.SNAME = comboBox2.Text;
                FRM.ShowDialog();
                if (FRM.WAREID != "")
                {

                    dataGridView1["品名", rows].Value = FRM.WNAME;
                    dataGridView1["型号", rows].Value = FRM.MODEL;
                    dataGridView1["单价", rows].Value = FRM.RETAIL_PRICE;

                    dataGridView1.CurrentCell = dataGridView1["数量", rows];
                }

            }

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
                        dr["订单交期"] = varDate;
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
            dataGridView1.Rows.Clear();
            ClearText();
            IDO = corder.GETID();
            textBox1.Text = IDO;
            bind();
            hint.Text = "";
        }
        public void ClearText()
        {
            comboBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
        
            comboBox2.Text = "";
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
            BASE_INFO.CUSTOMER_INFO FRM = new CSPSS.BASE_INFO.CUSTOMER_INFO();
            FRM.ORDER_USE();
            FRM.ShowDialog();
            this.comboBox1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.comboBox1.DroppedDown = false;//使组合框不显示其下拉部分
            this.comboBox1.IntegralHeight = true;//恢复默认值
            if (IF_DOUBLE_CLICK)
            {
                dtx = bc.getdt(ccustomer_info.sql + " WHERE A.CUID='"+CUID +"'");
                if (dtx.Rows.Count > 0)
                {
                    comboBox1.Text = dtx.Rows[0]["客户编号"].ToString();
                    textBox2.Text = dtx.Rows[0]["客户名称"].ToString();
                
                }
            }
            //textBox6.Focus();
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
           /* else if(bc.exists (cmisc_storage .sql +string .Format (" WHERE D.ORID='{0}'",IDO )))
            {
                hint.Text = string.Format("订单号 {0} 已经有入库记录不允许修改", IDO);
                b = true;
            }
            /*else if (comboBox1.Text == "")
            {
                hint.Text = "客户编号不能为空！";
                b = true;
            }
            else if (!bc.exists(ccustomer_info .sql  + " WHERE A.CUID='" + comboBox1.Text + "'"))
            {
                hint.Text = "客户编号不存在系统！";
                b = true;
            }*/
             else if (textBox2 .Text  == "")
            {
                hint.Text = "客户名称不能为空！";
                b = true;
            }
           else if (comboBox2.Text == "")
          {
              hint.Text = "供应商ID不能为空！";
              b = true;
          }
          else if (!bc.exists(" SELECT * FROM USERINFO  WHERE UNAME='" + comboBox2.Text + "'"))
          {
              hint.Text = "供应商ID不存在系统！";
              b = true;
          }
            else if (JUAGE_WNAME_IF_ABOVE_ONE(dataGridView1, "型号") == false)
            {
                hint.Text = string.Format("至少有一项型号才能保存");
                b = true;
            }
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
            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1["型号", i].FormattedValue.ToString() == "")
                {

                }
                else if (!bc.exists(@"
SELECT * FROM WAREINFO
WHERE MODEL='" + dataGridView1["型号", i].FormattedValue.ToString() + "'"))
                {
                    hint.Text = string.Format("项次 {0} 型号不存在系统", dataGridView1["项次", i].FormattedValue.ToString());
                    b = true;
                    break;
                }
                else if (dataGridView1["单价", i].FormattedValue.ToString() == "")
                {
                    hint.Text = string.Format("项次 {0} 单价不能为空", dataGridView1["项次", i].FormattedValue.ToString());
                    b = true;
                    break;
                }
                else if (bc.yesno(dataGridView1["单价", i].FormattedValue.ToString()) == 0)
                {
                    hint.Text = string.Format("项次 {0} 单价只能输入数字", dataGridView1["项次", i].FormattedValue.ToString());
                    b = true;
                    break;
                }
                else if (dataGridView1["数量", i].FormattedValue.ToString() == "")
                {
                    hint.Text = string.Format("项次 {0} 数量不能为空", dataGridView1["项次", i].FormattedValue.ToString());
                    b = true;
                    break;
                }


                else if (bc.yesno(dataGridView1["数量", i].FormattedValue.ToString()) == 0)
                {
                    hint.Text = string.Format("项次 {0} 数量只能输入数字", dataGridView1["项次", i].FormattedValue.ToString());
                    b = true;
                    break;
                }
            
            }
            return b;
        }
        #endregion
    
        #region JUAGE_WNAME_IF_ABOVE_ONE
        private bool JUAGE_WNAME_IF_ABOVE_ONE(DataGridView dgv, string COLUMN_NAME)
        {
            bool b = false;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv[COLUMN_NAME, i].FormattedValue.ToString() != "")
                {
                    b = true;
                }
            }
            return b;
        }
        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
         
            try
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
            corder.MAKERID = LOGIN.USID;
            corder.ORID = IDO;
            corder.CUID = textBox2.Text;
            corder.PUID = bc.getOnlyString("select usid from userinfo where uname='"+comboBox2.Text +"'");
            corder.ORDER_DATE = dateTimePicker1.Text;
            //corder.CUSTOMER_ORID = textBox6.Text;
            corder.save(dataGridView1);
            IFExecution_SUCCESS = corder.IFExecution_SUCCESS;
            hint.Text = corder.ErrowInfo;
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
                    if (bc.exists("SELECT * FROM SELLTABLE_DET WHERE ORID='" + textBox1 .Text + "'"))
                    {
                        hint.Text = string.Format("订单号: {0} 已经存在销货记录,不允许新增修改或是删除！", textBox1 .Text );

                    }
                    else
                    {
                        basec.getcoms("DELETE ORDER_MST WHERE ORID='" + textBox1.Text + "'");
                        basec.getcoms("DELETE ORDER_DET WHERE ORID='" + textBox1.Text + "'");
                        add();
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
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                dataGridView1[0, i].Value = i + 1;

            }
            for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = CCOLOR.GLS;
                dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = CCOLOR.YG;
                i = i + 1;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            CSPSS.USER_MANAGE.USER_INFO FRM = new USER_MANAGE.USER_INFO();
            FRM.GET_DATA_INT = 1;
            FRM.ShowDialog();
            this.comboBox2.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.comboBox2.DroppedDown = false;//使组合框不显示其下拉部分
            this.comboBox2.IntegralHeight = true;//恢复默认值
            if (FRM.IF_DOUBLE_CLICK==true )
            {
                comboBox2.Text = FRM.UNAME;
            }
          
        }

    }
}
