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
    public partial class SCRAPT : Form
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
        CSCRAP cSCRAP = new CSCRAP();
        DataTable dt = new DataTable();
        DataTable dtx = new DataTable();
        string varDate = DateTime.Now.ToString("yyy/MM/dd").Replace("-", "/");
        SCRAP F1= new SCRAP();
        protected int i, j;
        public SCRAPT()
        {
            InitializeComponent();
        }
        public SCRAPT(SCRAP FRM)
        {
            InitializeComponent();
            F1 = FRM;
        }
        private void SCRAPT_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            this.Icon =  Resource1.xz_200X200;
           comboBox1 .Text  = LOGIN.SUPPLIER;
            
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
        private void bind()
        {
         
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
            dtx = basec.getdts(cSCRAP.sql + " where A.SCID='"+IDO +"' ORDER BY  A.SCKEY ASC ");
            if (dtx.Rows.Count > 0)
            {
               comboBox1 .Text  = dtx.Rows[0]["供应商ID"].ToString();
                dt = cSCRAP.GetTableInfo();
                foreach (DataRow dr in dtx.Rows)
                {
                    DataRow dr1 = dt.NewRow();
                    dr1["项次"] = dr["项次"].ToString();
                    dr1["日期"] = dr["日期"].ToString();
                    dr1["品名"] = dr["品名"].ToString();
                    dr1["型号"] = dr["型号"].ToString();
                    dr1["识别码"] = dr["识别码"].ToString();
                    dr1["数量"] = dr["数量"].ToString();
                    dr1["报废原因"] = dr["报废原因"].ToString();
                    dt.Rows.Add(dr1);
                   
                }
                if (dt.Rows.Count > 0 && dt.Rows.Count < 6)
                {
                    int n = 6 - dt.Rows.Count;
                    for (int i = 0; i < n; i++)
                    {
                        DataRow dr = dt.NewRow();
                        int b1 = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["项次"].ToString());
                        dr["项次"] = Convert.ToString(b1 + 1);
                        dr["日期"] = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
                        dt.Rows.Add(dr);
                    }
                }
            }
            else
            {
                dt = total();
            }
            dataGridView1.DataSource = dt;
            dgvStateControl();
        }
        #endregion
        #region total1
        private DataTable total()
        {
            DataTable dtt2 = cSCRAP.GetTableInfo();
            for (i = 1; i <= 6; i++)
            {
                DataRow dr = dtt2.NewRow();
                dr["项次"] = i;
                dr["日期"] = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
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
            dataGridView1.Columns["报废原因"].Width = 300;
            dataGridView1.Columns["项次"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["数量"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["数量"].DefaultCellStyle.BackColor = CCOLOR.CUSTOMER_YELLOW;
            dataGridView1.Columns["日期"].HeaderText = "日期/Date";
            dataGridView1.Columns["品名"].HeaderText = "品名/Product";
            dataGridView1.Columns["型号"].HeaderText = "型号/Model";
            dataGridView1.Columns["识别码"].HeaderText = "识别号/Mark";
            dataGridView1.Columns["数量"].HeaderText = "数量/Qty";
            dataGridView1.Columns["报废原因"].HeaderText = "报废原因,损耗说明/Discard,consume";
           
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

                STOCK_MANAGE.INVENTORY FRM = new INVENTORY();
                FRM.SUPPLIER_ID = comboBox1.Text;
                FRM.SELECT = 1;
                FRM.ShowDialog();
                if (FRM.WAREID != "" && IF_DOUBLE_CLICK ==true  )
                {
                   dataGridView1["品名",rows ].Value = FRM.WNAME;
                   dataGridView1["型号",rows ].Value = FRM.MODEL;
                   dataGridView1["识别码", rows].Value = FRM.MARK;
                   dataGridView1["数量", rows].Value = FRM.STORAGE_COUNT;
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
                        DataRow dr = dt.NewRow();
                        int b1 = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["项次"].ToString());
                        dr["项次"] = Convert.ToString(b1 + 1);
                        dr["日期"] = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
                        dt.Rows.Add(dr);
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
        
            IDO = cSCRAP.GETID();
            bind();
            hint.Text = "";
          
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
            if (IDO == "")
            {
                hint.Text = "编号不能为空！";
                b = true;
            }
            else if (comboBox1.Text == "")
            {
                hint.Text = string.Format("供应商ID不能为空");
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
            IFExecution_SUCCESS = cSCRAP.IFExecution_SUCCESS;
            hint.Text = cSCRAP.ErrowInfo;
            cSCRAP.SCID = IDO;
            cSCRAP.MAKERID = LOGIN.USID ;
            cSCRAP.REMARK = "";
            cSCRAP.SUPPLIER_ID = bc.getOnlyString("SELECT USID FROM USERINFO WHERE UNAME='"+comboBox1 .Text +"'");
            cSCRAP.PICKID = "";
            cSCRAP.save(dataGridView1 , true);
            IFExecution_SUCCESS = cSCRAP.IFExecution_SUCCESS;
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
            try
            {
               
                if (MessageBox.Show("确定要删除该条凭证吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    basec.getcoms("DELETE SCRAP_MST WHERE SCID='" + IDO + "'");
                    basec.getcoms("DELETE SCRAP_DET WHERE SCID='" + IDO + "'");
                    basec.getcoms("DELETE MATERE WHERE MATEREID='" + IDO + "'");
                    bind();
                    IDO = "";
                    F1.bind();
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
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
         
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
                if (bc.juageOne("SELECT * FROM SCRAPT_DET WHERE SCID='" + IDO + "'"))
                {
                    basec.getcoms("DELETE SCRAPT_MST WHERE SCID='" + IDO + "'");
                    basec.getcoms("DELETE SCRAPT_DET WHERE SCID='" + IDO + "'");
                    basec.getcoms("DELETE GODE WHERE GODEID='" + IDO + "'");
                    bind();
                }
                else
                {
                    basec.getcoms("DELETE SCRAPT_DET WHERE SCKEY=(SELECT GEKEY FROM Gode WHERE BatchID='" + dt.Rows[i]["批号"].ToString() + "')");
                    basec.getcoms("DELETE GODE WHERE BATCHID='" + dt.Rows[i]["批号"].ToString() + "'");
                    bind();
                }
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            CSPSS.USER_MANAGE.USER_INFO FRM = new USER_MANAGE.USER_INFO();
            FRM.GET_DATA_INT = 1;
            FRM.EditRight();
            FRM.ShowDialog();
            this.comboBox1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.comboBox1.DroppedDown = false;//使组合框不显示其下拉部分
            this.comboBox1.IntegralHeight = true;//恢复默认值
            if (IF_DOUBLE_CLICK)
            {
                comboBox1.Text = FRM.UNAME;
         
            }
        }
    }
}
