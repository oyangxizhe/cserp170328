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
namespace CSPSS.FINANCIAL_MANAGE
{
    public partial class RECEIVABLES_ORDER : Form
    {
        DataTable dt = new DataTable();
        basec bc = new basec();
        int select=0;
        CRECEIVABLES_ORDER cRECEIVABLES_ORDER = new CRECEIVABLES_ORDER();
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

        public RECEIVABLES_ORDER()
        {
            InitializeComponent();
        }
        #region init
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RECEIVABLES_ORDER));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnToExcel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.PictureBox();
            this.hint = new System.Windows.Forms.Label();
            this.textBox50 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new dgvInfo();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.btnToExcel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 151);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(936, 84);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(115, 20);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(141, 21);
            this.textBox3.TabIndex = 129;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(35, 54);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 126;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(296, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 125;
            this.label4.Text = "客户名称";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(355, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(141, 21);
            this.textBox1.TabIndex = 124;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateTimePicker2.Location = new System.Drawing.Point(355, 50);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker2.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateTimePicker1.Location = new System.Drawing.Point(114, 50);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(141, 21);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // btnToExcel
            // 
            this.btnToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToExcel.Font = new System.Drawing.Font("宋体", 9F);
            this.btnToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnToExcel.Image")));
            this.btnToExcel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnToExcel.Location = new System.Drawing.Point(847, 14);
            this.btnToExcel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnToExcel.Name = "btnToExcel";
            this.btnToExcel.Size = new System.Drawing.Size(50, 64);
            this.btnToExcel.TabIndex = 5;
            this.btnToExcel.Text = "导出";
            this.btnToExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnToExcel.UseVisualStyleBackColor = false;
            this.btnToExcel.Click += new System.EventHandler(this.btnToExcel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "订单号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "~";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "日期期间";
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
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(936, 121);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单栏";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(28, 95);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 24;
            this.label17.Text = "新增";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.InitialImage = null;
            this.btnAdd.Location = new System.Drawing.Point(12, 20);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 60);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.TabStop = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExit
            // 
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
            // textBox50
            // 
            this.textBox50.Location = new System.Drawing.Point(685, 587);
            this.textBox50.Name = "textBox50";
            this.textBox50.ReadOnly = true;
            this.textBox50.Size = new System.Drawing.Size(100, 21);
            this.textBox50.TabIndex = 109;
            this.textBox50.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(620, 590);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 110;
            this.label5.Text = "合计金额:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 241);
            this.dataGridView1.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dataGridView1.MergeColumnNames")));
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(943, 337);
            this.dataGridView1.TabIndex = 111;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // RECEIVABLES_ORDER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(942, 616);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox50);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.hint);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "RECEIVABLES_ORDER";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "收款查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmWorkGroup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
  
        private void FrmWorkGroup_Load(object sender, EventArgs e)
        {
            try
            {
                textBox50.BackColor = CCOLOR.lylfnp;
                this.Icon =  Resource1.xz_200X200;
                hint.Location = new Point(400, 100);
                hint.ForeColor = Color.Red;
                dateTimePicker1.CustomFormat = "yyyy/MM/dd";
                dateTimePicker2.CustomFormat = "yyyy/MM/dd";
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
                hint.Text = "";
                textBox3.Focus();
                hint.Text = "";
            
              
            }
            catch (Exception)
            {
                MessageBox.Show("网络连接中断");
            }
        }
        #region bind
        public void bind()
        {
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn d1 = new DataGridViewTextBoxColumn();
            d1.Name = "序号";
            d1.HeaderText = "序号";
            dataGridView1.Columns.Add(d1);
            DataGridViewTextBoxColumn d2 = new DataGridViewTextBoxColumn();
            d2.Name = "收款日期";
            d2.HeaderText = "日期/Date";
            dataGridView1.Columns.Add(d2);
            DataGridViewTextBoxColumn d9 = new DataGridViewTextBoxColumn();
            d9.Name = "金额";
            d9.HeaderText = "金额/Amt";
            dataGridView1.Columns.Add(d9);
            DataGridViewTextBoxColumn d5 = new DataGridViewTextBoxColumn();
            d5.Name = "收款方式";
            d5.HeaderText = "现钞cash/现汇Bank";
            dataGridView1.Columns.Add(d5);
            DataGridViewTextBoxColumn d6 = new DataGridViewTextBoxColumn();
            d6.Name = "销货日期";
            d6.HeaderText = "入帐到/Pay to";
            dataGridView1.Columns.Add(d6);
            DataGridViewTextBoxColumn d7 = new DataGridViewTextBoxColumn();
            d7.Name = "客户名称";
            d7.HeaderText = "客户/Customer";
            dataGridView1.Columns.Add(d7);
           
            try
            {
                dataGridView1.Rows.Clear();
                hint.Text = "";
                StringBuilder stb = new StringBuilder();
                stb.Append(cRECEIVABLES_ORDER.sql);
                stb.Append("  WHERE  客户名称 LIKE '%" + textBox1.Text + "%'");
                stb.Append("  AND 订单号 LIKE '%" + textBox3.Text + "%'");
                string v1 = dateTimePicker1.Text + " 0:00:00";
                string v2 = dateTimePicker2.Text + " 23:59:59";
                if (checkBox1.Checked)
                {
                    stb.Append(" AND 制单日期 BETWEEN  '" + v1 + "' AND '" + v2 + "'");
                    //MessageBox.Show(" AND B.DATE  '" + v1 + "' AND '" + v2 + "'");
                }
            
                dataGridView1.AllowUserToAddRows = false;
                //dataGridView1.ContextMenuStrip = contextMenuStrip1;

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
                search_o(stb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion
        #region search_o()
        public void search_o(string sql)
        {
       
            //string v7 = bc.getOnlyString("SELECT SCOPE FROM SCOPE_OF_AUTHORIZATION WHERE USID='" + LOGIN.USID + "'");
            string v7 = "Y";
            /*if (textBox3.Text  == "" && textBox1.Text == "" && checkBox1.Checked == false)
            {
                //hint.Text = "未选择查询内容或是查询日期期间";
                dataGridView1.DataSource = null;
                return;
            }
            else*/ if (v7 == "Y")
            {

                dt = bc.getdt(sql+" ORDER BY 收款单号 ASC");

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
  
            if (dt.Rows.Count > 0)
            {
                decimal j = 0;
                DataTable dtt = new DataTable();
                dtt.Columns.Add("收款单号", typeof(string));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow dar = new DataGridViewRow();
                    dataGridView1.Rows.Add(dar);
                    dataGridView1["序号", i].Value = dt.Rows[i]["序号"].ToString();
                    dataGridView1["收款日期", i].Value = dt.Rows[i]["收款日期"].ToString();
                    dataGridView1["客户名称", i].Value = dt.Rows[i]["客户名称"].ToString();
                    dataGridView1["收款方式", i].Value = dt.Rows[i]["收款方式"].ToString();
                    dataGridView1["销货日期", i].Value = dt.Rows[i]["销货日期"].ToString();
                    dataGridView1["金额", i].Value = dt.Rows[i]["金额"].ToString();
                
                    DataTable dtx1 = bc.GET_DT_TO_DV_TO_DT(dtt, "", "收款单号='"+dt.Rows[i]["收款单号"].ToString()+"'");
                    if (dtx1.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        DataRow dr = dtt.NewRow();
                        dr["收款单号"] = dt.Rows[i]["收款单号"].ToString();
                        dtt.Rows.Add(dr);
                        j = j + decimal.Parse(dt.Rows[i]["金额"].ToString());
                    }
                }
                textBox50.Text = j.ToString("0.00");
                dgvStateControl();
            }
            else
            {
                textBox50.Text = "";
                hint.Text = "找不到所要搜索项！";
                dataGridView1.DataSource = null;
            }
        }
        #endregion
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            this.dataGridView1.MergeColumnNames.Add("收款日期");
            this.dataGridView1.MergeColumnNames.Add("客户名称");
            //this.dataGridView1.MergeColumnNames.Add("总计");
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
                dataGridView1.Rows[i].Height = 18;
            }
            for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = CCOLOR.GLS;
                dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = CCOLOR.YG;
                i = i + 1;
            }
         
            dataGridView1.Columns["金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
   
        private void btnToExcel_Click(object sender, EventArgs e)
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
            CSPSS.FINANCIAL_MANAGE.RECEIVABLES_ORDERT FRM = new FINANCIAL_MANAGE.RECEIVABLES_ORDERT(this);
            FRM.IDO = cRECEIVABLES_ORDER.GETID();
            FRM.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bind();
            try
            {
                
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

 
        private void btndgvInfoCopy_Click(object sender, EventArgs e)
        {

            dgvCopy(ref dataGridView1 );
        }
        private void dgvCopy(ref dgvInfo   dgv)
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


        }

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
                RECEIVABLES_ORDERT FRM = new RECEIVABLES_ORDERT(this);
                string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex]["收款单号"].ToString();
                FRM.IDO = v1;
                FRM.ORID = dt.Rows[dataGridView1.CurrentCell.RowIndex]["订单号"].ToString();
                FRM.ADD_OR_UPDATE = "UPDATE";
                FRM.Show();
            }
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
