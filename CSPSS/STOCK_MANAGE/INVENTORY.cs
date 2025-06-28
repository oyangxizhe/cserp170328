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
using FzBozc;
using FzBozc.Common;
using System.IO;
namespace CSPSS.STOCK_MANAGE
{
    public partial class INVENTORY : Form
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        CINVENTORY cinventory = new CINVENTORY();
        #region nature
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }
        }
        private string _SUPPLIER_ID;
        public string SUPPLIER_ID
        {
            set { _SUPPLIER_ID = value; }
            get { return _SUPPLIER_ID; }
        }
        private static bool _IF_DOUBLE_CLICK;
        public static bool IF_DOUBLE_CLICK
        {
            set { _IF_DOUBLE_CLICK = value; }
            get { return _IF_DOUBLE_CLICK; }
        }
        int select=0;
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
        private string _BATCHID;
        public string BATCHID
        {
            set { _BATCHID = value; }
            get { return _BATCHID; }
        }
        private int _SELECT;
        public int SELECT
        {
            set { _SELECT = value; }
            get { return _SELECT; }
        }
        private string _MODEL;
        public string MODEL
        {
            set { _MODEL = value; }
            get { return _MODEL; }
        }
        private string _WNAME;
        public string WNAME
        {
            set { _WNAME = value; }
            get { return _WNAME; }
        }
        private string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }
        }
        private string _MARK;
        public string MARK
        {
            set { _MARK = value; }
            get { return _MARK; }
        }
        private string _STORAGE_COUNT;
        public string STORAGE_COUNT
        {
            set { _STORAGE_COUNT = value; }
            get { return _STORAGE_COUNT; }
        }
        #endregion
        public INVENTORY()
        {
            InitializeComponent();
        }
        #region init
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(INVENTORY));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.PictureBox();
            this.hint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(0, 241);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(943, 375);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_DataSourceChanged);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(3, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(936, 84);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(355, 20);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(141, 21);
            this.textBox2.TabIndex = 130;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 129;
            this.label2.Text = "供应商ID";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(114, 50);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(141, 21);
            this.textBox3.TabIndex = 128;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 127;
            this.label1.Text = "识别码";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(115, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(141, 21);
            this.textBox1.TabIndex = 126;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "型号";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(862, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 29;
            this.label11.Text = "退出";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(771, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 28;
            this.label12.Text = "搜索";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.pictureBox5);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(936, 121);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单栏";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(684, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 114;
            this.label5.Text = "导出";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.InitialImage = null;
            this.pictureBox5.Location = new System.Drawing.Point(667, 20);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(60, 60);
            this.pictureBox5.TabIndex = 113;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Click += new System.EventHandler(this.pictureBox5_Click);
            // 
            // btnExit
            // 
            this.btnExit.ErrorImage = null;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.InitialImage = null;
            this.btnExit.Location = new System.Drawing.Point(847, 20);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 60);
            this.btnExit.TabIndex = 19;
            this.btnExit.TabStop = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.ErrorImage = null;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.InitialImage = null;
            this.btnSearch.Location = new System.Drawing.Point(757, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 60);
            this.btnSearch.TabIndex = 18;
            this.btnSearch.TabStop = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // hint
            // 
            this.hint.AutoSize = true;
            this.hint.Location = new System.Drawing.Point(204, 136);
            this.hint.Name = "hint";
            this.hint.Size = new System.Drawing.Size(29, 12);
            this.hint.TabIndex = 105;
            this.hint.Text = "hint";
            // 
            // INVENTORY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(942, 616);
            this.Controls.Add(this.hint);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "INVENTORY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "库存查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmWorkGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
  
        private void FrmWorkGroup_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn d1 = new DataGridViewTextBoxColumn();
            d1.Name = "序号";
            d1.HeaderText = "序号";
            dataGridView1.Columns.Add(d1);
            DataGridViewTextBoxColumn d2 = new DataGridViewTextBoxColumn();
            d2.Name = "供应商ID";
            d2.HeaderText = "供应商ID";
            dataGridView1.Columns.Add(d2);
            DataGridViewTextBoxColumn d4 = new DataGridViewTextBoxColumn();
            d4.Name = "品名";
            d4.HeaderText = "产品名称/Name";
            dataGridView1.Columns.Add(d4);
            DataGridViewTextBoxColumn d5 = new DataGridViewTextBoxColumn();
            d5.Name = "型号";
            d5.HeaderText = "型号/Model";
            dataGridView1.Columns.Add(d5);
            DataGridViewTextBoxColumn d8 = new DataGridViewTextBoxColumn();
            d8.Name = "识别码";
            d8.HeaderText = "识别码/Mark";
            dataGridView1.Columns.Add(d8);
            DataGridViewTextBoxColumn d6 = new DataGridViewTextBoxColumn();
            d6.Name = "单价";
            d6.HeaderText = "单价/Price";
            dataGridView1.Columns.Add(d6);
            DataGridViewTextBoxColumn d7 = new DataGridViewTextBoxColumn();
            d7.Name = "数量";
            d7.HeaderText = "数量/Qty";
            dataGridView1.Columns.Add(d7);
            DataGridViewImageColumn d9 = new DataGridViewImageColumn();
            d9.Name = "缩略图";
            d9.HeaderText = "缩略图/Picture";
            dataGridView1.Columns.Add(d9);
            try
            {
                this.Icon =  Resource1.xz_200X200;
                hint.Location = new Point(400, 100);
                hint.ForeColor = Color.Red;
                hint.Text = "";
                bind();
              
            }
            catch (Exception)
            {
                MessageBox.Show("网络连接中断");
            }
        }
        #region bind
        public void bind()
        {
            dataGridView1.Rows.Clear();
            hint.Text = "";
            if (SUPPLIER_ID != null && SUPPLIER_ID!="")
            {
              
                textBox2.Text = SUPPLIER_ID;
            }
            StringBuilder stb = new StringBuilder();
            dt = bc.getstoragecount();
            dt = bc.GET_DT_TO_DV_TO_DT(dt, "", "型号 LIKE'%" + textBox1.Text + "%' AND 供应商ID like'%" + textBox2.Text + 
                "%' AND 识别码 like'%" + textBox3.Text + "%'");
            dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.ContextMenuStrip = contextMenuStrip1;
            string v7 = bc.getOnlyString("SELECT SCOPE FROM SCOPE_OF_AUTHORIZATION WHERE USID='" + LOGIN.USID + "'");

            if (v7 == "Y")
            {

              
            }
            else
            {
                
                dt = bc.GET_DT_TO_DV_TO_DT(dt, "", "仓库编号='"+LOGIN .USID +"'");

            }
            hint.Location = new Point(400, 100);
            hint.ForeColor = Color.Red;

            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {

                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Text = "";
            }
            dt = cinventory.RETURN_HAVE_ID_DT(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataGridViewRow dar = new DataGridViewRow();
                dataGridView1.Rows.Add(dar);
                dataGridView1["序号", i].Value = dt.Rows[i]["序号"].ToString();
                dataGridView1["供应商ID", i].Value = dt.Rows[i]["供应商ID"].ToString();
                dataGridView1["品名", i].Value = dt.Rows[i]["品名"].ToString();
                dataGridView1["型号", i].Value = dt.Rows[i]["型号"].ToString();
                dataGridView1["识别码", i].Value = dt.Rows[i]["识别码"].ToString();
                dataGridView1["数量", i].Value = dt.Rows[i]["库存数量"].ToString();
                string path=bc.getOnlyString (@"
SELECT PATH FROM WAREFILE A LEFT JOIN WAREINFO B ON A.WAREID=B.WAREID 
WHERE B.MODEL='" + dt.Rows[i]["型号"].ToString() + "'  AND INITIAL_OR_OTHER='80X80'");

              
                if(path!="")
                {
                dataGridView1["缩略图", i].Value = Image.FromStream(System.Net.WebRequest.Create(path ).GetResponse().GetResponseStream());
                }
            }
            dgvStateControl();
            try
            {
        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            SUPPLIER_ID = "";
        }
        #endregion
        #region search_o()
        public void search_o(string sql)
        {
       
            //string v7 = bc.getOnlyString("SELECT SCOPE FROM SCOPE_OF_AUTHORIZATION WHERE USID='" + LOGIN.USID + "'");
            string v7 = "Y";
           if (v7 == "Y")
            {

                dt = bc.getdt(sql+" ORDER  BY A.MGKEY ASC");

            }
            else if (v7 == "GROUP")
            {

                dt = bc.getdt(sql + @" AND A.MAKERID IN (SELECT EMID FROM USERINFO A WHERE UGID IN 
 (SELECT UGID FROM USERINFO WHERE USID='" + LOGIN.USID + "'))" );
            }
            else
            {
                dt = bc.getdt(sql + " AND A.MAKERID='" + LOGIN.EMID + "'" );

            }
            //dt = cINVENTORY.RETURN_DT(dt);
            if (dt.Rows.Count > 0)
            {
             
                dataGridView1.DataSource = dt;
                dgvStateControl();
             
            }
            else
            {
                hint.Text = "找不到所要搜索项！";
                dataGridView1.DataSource = null;

            }
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
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[i].ReadOnly = true;
            }
            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Height = 80;
            }
            for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = CCOLOR.GLS;
                dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = CCOLOR.YG;
                i = i + 1;
            }
        }
        #endregion
        #region add

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
        #region doubleclick
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (select != 0)
            {
                int intCurrentRowNumber = this.dataGridView1.CurrentCell.RowIndex;
                string s1 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[0].Value.ToString().Trim();
                string s2 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[1].Value.ToString().Trim();
                string s3 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[2].Value.ToString().Trim();
                string s4 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[3].Value.ToString().Trim();
                this.Close();
            }
            else
            {
               
                string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex]["入库单号"].ToString();
              
            }
        }
        #endregion
        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                bc.dgvtoExcel(dataGridView1, "订单信息");
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            int i;
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Columns[i].ValueType.ToString() == "System.Decimal")
                {
                    dataGridView1.Columns[i].DefaultCellStyle.Format = "N";
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                }

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
         
            try
            {
                bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //判断是不是右键
            {
                Control control = new Control();
                Point ClickPoint = new Point(e.X, e.Y);
                control.GetChildAtPoint(ClickPoint);
                if (dataGridView1.HitTest(e.X, e.Y).RowIndex >= 0 && dataGridView1.HitTest(e.X, e.Y).ColumnIndex >= 0)//判断你点的是不是一个信息行里
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.HitTest(e.X, e.Y).RowIndex].Cells[dataGridView1.HitTest(e.X, e.Y).ColumnIndex];
                    ContextMenu con = new ContextMenu();
                    MenuItem menuDeleteknowledge = new MenuItem("复制");
                    menuDeleteknowledge.Click += new EventHandler(btndgvInfoCopy_Click);
                    con.MenuItems.Add(menuDeleteknowledge);
                    this.dataGridView1.ContextMenu = con;
                    con.Show(dataGridView1, new Point(e.X + 10, e.Y));
                }
            }
        }
        private void btndgvInfoCopy_Click(object sender, EventArgs e)
        {

            dgvCopy(ref dataGridView1);
        }
        private void dgvCopy(ref DataGridView  dgv)
        {
            if (dgv.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    Clipboard.SetDataObject(dgv.GetClipboardContent());
                }
                catch (Exception MyEx)
                {
                    MessageBox.Show(MyEx.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                bc.dgvtoExcel(dataGridView1, this.Text );
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string v1 = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            if (v1 != "")
            {
                if (SELECT != 0)
                {
                    //ACCODE = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    WNAME = Convert.ToString(dataGridView1["品名", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    MODEL = Convert.ToString(dataGridView1["型号", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    MARK = Convert.ToString(dataGridView1["识别码", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    STORAGE_COUNT = Convert.ToString(dataGridView1["数量", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    STOCK_MANAGE.SCRAPT.IF_DOUBLE_CLICK = true;
                    this.Close();
                }
                else
                {
                  
                }


            }
        }
    }
}
