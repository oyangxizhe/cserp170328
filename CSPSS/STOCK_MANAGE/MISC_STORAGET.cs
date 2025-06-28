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


namespace CSPSS.STOCK_MANAGE
{
    public partial class MISC_STORAGET : Form
    {

        private string _ORKEY;
        public string ORKEY
        {
            set { _ORKEY = value; }
            get { return _ORKEY; }
        }
        private int _SELECT;
        public int SELECT
        {
            set { _SELECT = value; }
            get { return _SELECT; }
        }
        private static string _MATERIAL;
        public static string MATERIAL
        {
            set { _MATERIAL = value; }
            get { return _MATERIAL; }
        }
        private string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }
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
        private string _BARCODE;
        public string BARCODE
        {
            set { _BARCODE = value; }
            get { return _BARCODE; }
        }
        private string _IDO;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }
        }
        private string _ADD_OR_UPDATE;
        public string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }
        }

        basec bc = new basec();
        CCUSTOMER_INFO ccustomer_info = new CCUSTOMER_INFO();
        CORDER corder = new CORDER();
        CMISC_STORAGE cMISC_STORAGE = new CMISC_STORAGE();
        DataTable dt = new DataTable();
        DataTable dtx = new DataTable();
        string varDate = DateTime.Now.ToString("yyy/MM/dd").Replace("-", "/");
        MISC_STORAGE F1= new MISC_STORAGE();
        protected int i, j;
        public MISC_STORAGET()
        {
            InitializeComponent();
        }
        public MISC_STORAGET(MISC_STORAGE FRM)
        {
            InitializeComponent();
            F1 = FRM;
        }
        private void MISC_STORAGET_Load(object sender, EventArgs e)
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
            DataGridViewTextBoxColumn d8 = new DataGridViewTextBoxColumn();
            d8.Name = "识别码";
            d8.HeaderText = "识别码/Mark";
            dataGridView1.Columns.Add(d8);
       
            label9.Text = "（说明：批号有销货的不允许整笔单据删除，批号没有销货记录，则可以鼠标右击选中该批号单项删除）";
            label9.ForeColor = CCOLOR.lylf1;
            this.Icon =  Resource1.xz_200X200;
         

            textBox3.Font = new Font("黑体", 45, FontStyle.Regular);
            textBox3.BackColor = CCOLOR.lylfnp;
            textBox3.ForeColor = Color.White;
            textBox3.Focus();
            textBox2.Text = LOGIN.SUPPLIER;
            
            
            try
            {
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
            dtx = basec.getdts(cMISC_STORAGE.sql + " where 入库单号='" + IDO  + "' ORDER BY  入库单号 ASC ");
            if (dtx.Rows.Count > 0)
            {
              
                dateTimePicker1.Text = dtx.Rows[0]["入库日期"].ToString();
                textBox2.Text = dtx.Rows[0]["供应商ID"].ToString();
                textBox4.Text = dtx.Rows[0]["提货单号"].ToString();
                dt = dtx;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   
                    DataGridViewRow dar = new DataGridViewRow();
                    dataGridView1.Rows.Add(dar);
                    dataGridView1["项次", i].Value = dt.Rows[i]["项次"].ToString();
                    dataGridView1["产品分类", i].Value = dt.Rows[i]["产品分类"].ToString();
                    dataGridView1["品名", i].Value = dt.Rows[i]["品名"].ToString();
                    dataGridView1["型号", i].Value = dt.Rows[i]["型号"].ToString();
                    dataGridView1["单价", i].Value = dt.Rows[i]["单价"].ToString();
                    dataGridView1["数量", i].Value = dt.Rows[i]["数量"].ToString();
                    dataGridView1["识别码", i].Value = dt.Rows[i]["识别码"].ToString();
                
                }
             
            }
            else
            {
            
                DataGridViewRow dar = new DataGridViewRow();
                dataGridView1.Rows.Add(dar);
          
                dataGridView1["项次", 0].Value ="1";
              
            }

           
            dgvStateControl();
        }
        #endregion
        #region total1
        private DataTable total()
        {
            DataTable dtt2 = cMISC_STORAGE.GetTableInfo();
            for (i = 1; i <= 6; i++)
            {
                DataRow dr = dtt2.NewRow();
                dr["项次"] = i;
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
            dataGridView1.Columns["识别码"].Width = 100;
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
              !((System.Windows.Forms.TextBox)ActiveControl).AcceptsReturn && ActiveControl.TabIndex != 5)
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
                    dataGridView1["产品分类",rows ].Value  = FRM.TYPE_VALUE;
                    dataGridView1.CurrentCell = dataGridView1["型号", rows];
                }
            }
            else if (dataGridView1.Columns[columns].Name.ToString() == "型号")
            {

                BASE_INFO.PRODUCT FRM = new BASE_INFO.PRODUCT();
                FRM.PRODUCT_TYPE = dataGridView1["产品分类", rows].FormattedValue .ToString();
                FRM.SELECT = 1;
                FRM.ShowDialog();
                if (FRM.WAREID != "")
                {
                 
                   dataGridView1["品名",rows ].Value = FRM.WNAME;
                   dataGridView1["型号",rows ].Value = FRM.MODEL;
                   dataGridView1["单价",rows ].Value = FRM.BUYING_PRICE;
             
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
                   
                    if (dataGridView1.Rows.Count >= 1)
                    {
                      
                      
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

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
            IDO = cMISC_STORAGE.GETID();
            bind();
            hint.Text = "";
          
        }
        public void ClearText()
        {
            
            dateTimePicker1.Text = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
            //textBox2.Text = "";不用清空供应商名称
            textBox3.Text = "";
            textBox4.Text = "";
         
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            add();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
  

        #region juage
        private bool juage()
        {
            bool b = false;
            string GET_MGID = bc.getOnlyString("SELECT MGID FROM MISC_GODE_MST WHERE PICKID='" + textBox4.Text + "'");
            if (IDO == "")
            {
                hint.Text = "编号不能为空！";
                b = true;
            }
            else if (textBox4.Text == "")
            {
                hint.Text = string.Format("提货单号不能为空");
                b = true;
            }
            else if (textBox4.Text != "" &&
                bc.exists("SELECT * FROM MISC_GODE_MST WHERE PICKID='" + textBox4.Text + "'") && IDO != GET_MGID)
            {
                hint.Text = string.Format("提货单号:{0} 已经存在", textBox4.Text);
                b = true;
            }
            else if (textBox2.Text == "")
            {
                hint.Text = string.Format("供应商ID不能为空");
                b = true;
            }
            else if (JUAGE_WNAME_IF_ABOVE_ONE(dataGridView1, "型号") == false)
            {
                hint.Text = string.Format("至少有一项型号才能保存");
                b = true;
            }
            else if (cMISC_STORAGE.JUAGE_CURRENT_STORAGECOUNT_IF_LESSTHAN_DELETE_COUNT(IDO))
            {
                b = true;
                hint.Text = cMISC_STORAGE.ErrowInfo;
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
                else   if (dataGridView1["单价", i].FormattedValue.ToString() == "")
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
                else   if (dataGridView1["数量", i].FormattedValue.ToString() == "")
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
                else if (dataGridView1["识别码", i].FormattedValue.ToString()!="" && 
                    juage3(dataGridView1["识别码", i].FormattedValue.ToString()))
                {

                    b = true;
                    break;
                }
            }
            return b;
        }
        #endregion
        #region juage3()
        private bool juage3(string MARK)
        {
            bool b = false;
            string a = MARK;
            string[] b1 = a.Split(',');
            for (int i = 0; i < b1.Length; i++)
            {
                if(bc.exists ("SELECT * FROM MARK WHERE MARK='"+b1[i]+"' AND MGID<>'"+IDO +"'"))
                {
                    hint.Text = string.Format("识别码 {0} 已经存在系统中",b1[i]);
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

           
            IFExecution_SUCCESS = cMISC_STORAGE.IFExecution_SUCCESS;
            hint.Text = cMISC_STORAGE.ErrowInfo;
            cMISC_STORAGE.MGID = IDO;
            cMISC_STORAGE.GODE_DATE = dateTimePicker1.Text;
            cMISC_STORAGE.GODE_MAKERID = "";
            cMISC_STORAGE.MAKERID = LOGIN.USID ;
            cMISC_STORAGE.REMARK = "";
            cMISC_STORAGE.SUPPLIER_ID = LOGIN.USID;
            cMISC_STORAGE.PICKID = textBox4.Text;
            cMISC_STORAGE.save(dataGridView1 , true);
            IFExecution_SUCCESS = cMISC_STORAGE.IFExecution_SUCCESS;
            F1.bind();
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
              
          
            }
        }
        #endregion
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除该条凭证吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (cMISC_STORAGE.JUAGE_CURRENT_STORAGECOUNT_IF_LESSTHAN_DELETE_COUNT(IDO))
                {
                    hint.Text = cMISC_STORAGE.ErrowInfo;
                }
                else
                {
                    basec.getcoms("DELETE MISC_GODE_MST WHERE MGID='" + IDO + "'");
                    basec.getcoms("DELETE MISC_GODE_DET WHERE MGID='" + IDO + "'");
                    basec.getcoms("DELETE GODE WHERE GODEID='" + IDO + "'");
                    basec.getcoms("DELETE MARK WHERE MGID='" + IDO + "'");//删除识别码表
                    bind();
                    ClearText();
                    IDO = "";
                    F1.bind();
                }

            }
            try
            {
               
         
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

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
       
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                actioin();
            }
        }
        private int yesno(string vars)
        {
            int k = 1;
            int i;
            for (i = 0; i < vars.Length; i++)
            {
                int p = Convert.ToInt32(vars[i]);
                if (p >= 48 && p <= 57 || p >= 65 && p <= 90 || p >= 97 && p <= 122)
                {
                    k = 1;
                }
                else
                {
                    k = 0; break;
                }

            }
            return k;
        }
        private void actioin()
        {
            
            ORKEY = "";
            DataTable dtt = bc.getdt("SELECT * FROM ORDER_BARCODE WHERE BARCODE='" + textBox3.Text.Trim() + "'");
            if (dtt.Rows.Count > 0)
            {

                ORKEY = dtt.Rows[0]["ORKEY"].ToString();
            }
            else
            {
                MessageBox.Show("条码："+textBox3.Text.Trim() + " 不存在系统", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Text = "";
            }
            if (yesno(textBox3.Text.Trim()) == 0)
            {
                MessageBox.Show("条码："+textBox3.Text.Trim()+" 输入的字符不合法", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Text = "";
            }
            else  if (bc.exists("SELECT * FROM GODE WHERE BATCHID='" + textBox3 .Text .Trim () + "'"))
            {
                MessageBox.Show(string.Format("条码：{0} 已经存在入库记录", textBox3.Text.Trim ()), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Text = "";

            }
            else
            {
                
                dtx = bc.getdt(corder.sql + string.Format(" WHERE A.ORKEY='{0}'", ORKEY));
                if (dtx.Rows.Count > 0)
                {
                    textBox2.Text = dtx.Rows[0]["客户名称"].ToString();
              
                    cMISC_STORAGE.MGID = IDO;
                    cMISC_STORAGE.WAREID = dtx.Rows[0]["型号"].ToString();
                    cMISC_STORAGE.MGCOUNT = dtx.Rows[0]["数量"].ToString();
                    cMISC_STORAGE.SKU = dtx.Rows[0]["单位"].ToString();
                    cMISC_STORAGE.BARCODE = textBox3.Text.Trim();
                    cMISC_STORAGE.GODE_DATE = dateTimePicker1.Text;
                    cMISC_STORAGE.GODE_MAKERID = "";
                    cMISC_STORAGE.MAKERID = "";
                    cMISC_STORAGE.REMARK = "";
                    cMISC_STORAGE.ORKEY = ORKEY;
                    cMISC_STORAGE.save_BARCODE();
                    textBox3.Text = "";
                    bind();
                }
            }
            try
            {
              

     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void TSMI_Click(object sender, EventArgs e)
        {
         
            int i = dataGridView1.CurrentCell.RowIndex;
            if (bc.exists("SELECT * FROM MATERE WHERE BATCHID='" + dt.Rows[i]["批号"].ToString() + "'"))
            {
                hint.Text = string.Format("此批号：{0} 已经有销货记录，不允许删除", dt.Rows[i]["批号"].ToString());
            }
            else
            {
                if (bc.juageOne("SELECT * FROM MISC_GODE_DET WHERE MGID='" + IDO + "'"))
                {
                    basec.getcoms("DELETE MISC_GODE_MST WHERE MGID='" + IDO + "'");
                    basec.getcoms("DELETE MISC_GODE_DET WHERE MGID='" + IDO + "'");
                    basec.getcoms("DELETE GODE WHERE GODEID='" + IDO + "'");
                    bind();
                }
                else
                {
                    basec.getcoms("DELETE MISC_GODE_DET WHERE MGKEY=(SELECT GEKEY FROM Gode WHERE BatchID='" + dt.Rows[i]["批号"].ToString() + "')");
                    basec.getcoms("DELETE GODE WHERE BATCHID='" + dt.Rows[i]["批号"].ToString() + "'");
                    bind();
                }
            }
        }
    }
}
