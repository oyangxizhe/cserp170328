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

namespace CSPSS.FINANCIAL_MANAGE
{
    public partial class VOUCHER : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtx = new DataTable();
        basec bc = new basec();
        protected int M_int_judge, i, look;
        protected int getdata;
        StringBuilder sqb = new StringBuilder();
        Color c2 = System.Drawing.ColorTranslator.FromHtml("#990033");
        CVOUCHER vou = new CVOUCHER();
        private static DataTable _GETDT_INFO;
        public  static DataTable GETDT_INFO
        {
            set { _GETDT_INFO = value; }
            get { return _GETDT_INFO; }

        }
        private static bool _IF_DOUBLE_CLICK;
        public static bool IF_DOUBLE_CLICK
        {
            set { _IF_DOUBLE_CLICK = value; }
            get { return _IF_DOUBLE_CLICK; }

        }
        public VOUCHER()
        {
            InitializeComponent();
        }
        #region init
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VOUCHER));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnToExcel = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.hint = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
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
            this.dataGridView1.Location = new System.Drawing.Point(0, 266);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(943, 312);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_DataSourceChanged);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnToExcel);
            this.groupBox1.Location = new System.Drawing.Point(3, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(936, 133);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(115, 20);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 21);
            this.textBox1.TabIndex = 52;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(30, 56);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 51;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.chk2_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 49;
            this.label2.Text = "~";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy/MM/dd";
            this.dateTimePicker2.Enabled = false;
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(355, 50);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(121, 21);
            this.dateTimePicker2.TabIndex = 48;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy/MM/dd";
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(114, 50);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 21);
            this.dateTimePicker1.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 46;
            this.label1.Text = "凭证期间";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 44;
            this.label6.Text = "账户";
            // 
            // btnToExcel
            // 
            this.btnToExcel.FlatAppearance.BorderSize = 0;
            this.btnToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToExcel.Font = new System.Drawing.Font("宋体", 9F);
            this.btnToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnToExcel.Image")));
            this.btnToExcel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnToExcel.Location = new System.Drawing.Point(841, 13);
            this.btnToExcel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnToExcel.Name = "btnToExcel";
            this.btnToExcel.Size = new System.Drawing.Size(50, 64);
            this.btnToExcel.TabIndex = 11;
            this.btnToExcel.Text = "导出";
            this.btnToExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnToExcel.UseVisualStyleBackColor = false;
            this.btnToExcel.Click += new System.EventHandler(this.btnToExcel_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(857, 95);
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
            this.groupBox2.Controls.Add(this.hint);
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
            // 
            // hint
            // 
            this.hint.AutoSize = true;
            this.hint.Location = new System.Drawing.Point(196, 59);
            this.hint.Name = "hint";
            this.hint.Size = new System.Drawing.Size(29, 12);
            this.hint.TabIndex = 402;
            this.hint.Text = "hint";
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
            this.btnExit.Location = new System.Drawing.Point(843, 20);
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
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(92, 591);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 57;
            this.label13.Text = "合计支出";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox3.Location = new System.Drawing.Point(151, 583);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(115, 21);
            this.textBox3.TabIndex = 56;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(362, 590);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 59;
            this.label14.Text = "合计收入";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox4.Location = new System.Drawing.Point(421, 582);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(112, 21);
            this.textBox4.TabIndex = 58;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(634, 592);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 61;
            this.label15.Text = "合计余额";
            // 
            // textBox6
            // 
            this.textBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox6.Location = new System.Drawing.Point(693, 584);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(113, 21);
            this.textBox6.TabIndex = 60;
            // 
            // VOUCHER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(942, 616);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VOUCHER";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "银行账户查询";
            this.Load += new System.EventHandler(this.VOUCHER_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
  
    
        private void VOUCHER_Load(object sender, EventArgs e)
        {

            Bind();
        }
        #region Bind
        public void Bind()
        {
            textBox3.BackColor = CCOLOR.YELLOW;
            textBox4.BackColor = CCOLOR.YELLOW;
            textBox6.BackColor = CCOLOR.YELLOW;
            textBox3.TextAlign = HorizontalAlignment.Right;
            textBox4.TextAlign = HorizontalAlignment.Right;
            textBox6.TextAlign = HorizontalAlignment.Right;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox6.ReadOnly = true;
            dataGridView1.Columns.Clear();
            DataGridViewTextBoxColumn d1 = new DataGridViewTextBoxColumn();
            d1.Name = "序号";
            d1.HeaderText = "序号";
            dataGridView1.Columns.Add(d1);
            DataGridViewTextBoxColumn d2 = new DataGridViewTextBoxColumn();
            d2.Name = "凭证日期";
            d2.HeaderText = "日期/Date";
            dataGridView1.Columns.Add(d2);
            DataGridViewTextBoxColumn d9 = new DataGridViewTextBoxColumn();
            d9.Name = "收入金额";
            d9.HeaderText = "收入/Recd";
            dataGridView1.Columns.Add(d9);
            DataGridViewTextBoxColumn d5 = new DataGridViewTextBoxColumn();
            d5.Name = "摘要";
            d5.HeaderText = "说明/Description";
            dataGridView1.Columns.Add(d5);
            DataGridViewTextBoxColumn d6 = new DataGridViewTextBoxColumn();
            d6.Name = "支出金额";
            d6.HeaderText = "支出/Outging";
            dataGridView1.Columns.Add(d6);
            DataGridViewTextBoxColumn d7 = new DataGridViewTextBoxColumn();
            d7.Name = "余额";
            d7.HeaderText = "结余/Balance";
            dataGridView1.Columns.Add(d7);

            this.WindowState = FormWindowState.Maximized;
            //dt = basec.getdts(vou.getsqlX + " WHERE B.STATUS!='INITIAL' ORDER BY A.VOKEY");
            //dataGridView1.DataSource = dt;
            dt1 = bc.getdt("SELECT VOID FROM VOUCHER_MST");
            AutoCompleteStringCollection inputInfoSource = new AutoCompleteStringCollection();
            //dgvStateControl();
 
            hint.Text = "";
            hint.ForeColor = Color.Red;
            try
            {
            
              
            }
            catch (Exception)
            {


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
                dataGridView1.Rows[i].Height = 18;
            }
            for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = CCOLOR.GLS;
                dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = CCOLOR.YG;
                i = i + 1;
            }
            dataGridView1.Columns["收入金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["支出金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["余额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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

            if (getdata != 0)
            {
                if (getdata == 1)
                {
                    int intCurrentRowNumber = this.dataGridView1.CurrentCell.RowIndex;
                    string s1 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[0].Value.ToString().Trim();
                    string s2 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[18].Value.ToString().Trim();
                    string s3 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[19].Value.ToString().Trim();
                    string s4 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[20].Value.ToString().Trim();
                    string s5 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[21].Value.ToString().Trim();
                    /*CSPSS.FINANCIAL_MANAGE.FrmSellTableT.data4[0] = "doubleclick";
                    CSPSS.FINANCIAL_MANAGE.FrmSellTableT.data1[0] = s1;
                    CSPSS.FINANCIAL_MANAGE.FrmSellTableT.data1[1] = s2;
                    CSPSS.FINANCIAL_MANAGE.FrmSellTableT.data1[2] = s3;
                    CSPSS.FINANCIAL_MANAGE.FrmSellTableT.data1[3] = s4;
                    CSPSS.FINANCIAL_MANAGE.FrmSellTableT.data1[4] = s5;*/

                    this.Close();
                }

            }
            else
            {
                VOUCHERT frm = new VOUCHERT(this);
                frm.IDO  = dt.Rows[dataGridView1.CurrentCell.RowIndex]["凭证号"].ToString();
                frm.ADD_OR_UPDATE = "UPDATE";
                frm.ShowDialog();
            }
        }
        #endregion
        public void a2()
        {

            getdata = 1;

        }
        public void a3()
        {

            getdata = 2;

        }
   
        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
               
                bc.dgvtoExcel(dataGridView1, "凭证明细");
                
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
        
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string a1 = bc.numYMD(12, 4, "0001", "select * from VOUCHER_MST", "VOID", "VO");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

               
                VOUCHERT frm = new VOUCHERT(this);
                frm.IDO = a1;
                frm.ADD_OR_UPDATE = "ADD";
                frm.ShowDialog();

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            search();
            try
            {
             
            }
            catch (Exception)
            {
                MessageBox.Show("不能出现(单引号+变量+单引号)+变量的格式");

            }

        }
        #region search()
        public void search()
        {
            string v1 = dateTimePicker1.Value.ToString("yyyy/MM/dd 00:00:00").Replace("-", "/");
            string v2 = dateTimePicker2.Value.ToString("yyyy/MM/dd 23:59:59").Replace("-", "/");
            string v3 = dateTimePicker1.Value.ToString("yyyy/MM/dd").Replace("-", "/");
            string v4 = dateTimePicker2.Value.ToString("yyyy/MM/dd").Replace("-", "/");
            hint.Text = "";
            sqb = new StringBuilder();
            sqb.AppendFormat(vou.getsqlX);
            sqb.AppendFormat(" WHERE  C.ACCODE LIKE '%" + textBox1.Text + "%'");
            if (checkBox1.Checked)
            {
                sqb.Append(" AND B.DATE  BETWEEN  '" + v1 + "' AND '" + v2 + "'");
            }
            search_o(sqb.ToString ());
        }
        #endregion

        #region search_o()
        public void search_o(string sql)
        {
            string sqlo =" ORDER BY A.VOID ASC";
            string v7 = "Y";
            if (v7 == "Y")
            {
                dt = bc.getdt(sql+sqlo); 
            }
            dt = vou.GET_CALCULATE(dt);
            dataGridView1.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataGridViewRow dar = new DataGridViewRow();
                    dataGridView1.Rows.Add(dar);
                    dataGridView1["序号", i].Value = (i + 1).ToString();
                    dataGridView1["收入金额", i].Value = dt.Rows[i]["收入金额"].ToString();
                    dataGridView1["支出金额", i].Value = dt.Rows[i]["支出金额"].ToString();
                    dataGridView1["凭证日期", i].Value = dt.Rows[i]["凭证日期"].ToString();
                    dataGridView1["摘要", i].Value = dt.Rows[i]["摘要"].ToString();
                    dataGridView1["余额", i].Value = dt.Rows[i]["公司余额"].ToString();
                }
                decimal d1 = 0;
                decimal d2 = 0;

                string v1 = dt.Compute("SUM(收入金额)","").ToString();
               
                if (!string.IsNullOrEmpty(v1))
                {
                    d1 = decimal.Parse(v1);
                }
                string v2 = dt.Compute("SUM(支出金额)","").ToString();
                if (!string.IsNullOrEmpty(v2))
                {
                    d2 = decimal.Parse(v2);
                }
                textBox3.Text = (d2).ToString();
                textBox4.Text = (d1).ToString();
                textBox6.Text = (d1 - d2).ToString();
                dgvStateControl();
            }
            else
            {
                hint.Text = "找不到所要搜索项！";
               
                textBox3.Text = "";
                textBox4.Text = "";
                textBox6.Text = "";
            }
        }
        #endregion
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;

            }
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
      
        }

        private void comboBox5_DropDown(object sender, EventArgs e)
        {
     
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定要删除选中凭证吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                        {
                            basec.getcoms("DELETE VOUCHER_MST WHERE VOID='" + dt.Rows[i]["凭证号"].ToString() + "'");
                            basec.getcoms("DELETE VOUCHER_DET WHERE VOID='" + dt.Rows[i]["凭证号"].ToString() + "'");
                         
                        }

                    }
                    search();
                  

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dt.Rows[dataGridView1.CurrentCell.RowIndex][1].ToString() + "," + dt.Rows[dataGridView1.CurrentCell.RowIndex][3].ToString());
        }

        private void lkgeneral_manage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
            try
            {

            }
            catch (Exception)
            {


            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {

                    if (vou.RETURN_GENERAL_AUDIT_STATUS(dt.Rows[i]["凭证号"].ToString()) == "N")
                    {
                        basec.getcoms(@"
UPDATE
VOUCHER_MST 
SET 
GENERAL_MANAGE_AUDIT_STATUS='Y',
GENERAL_MANAGE_AUDIT_DATE='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +
                         "' WHERE VOID='" + dt.Rows[i]["凭证号"].ToString() + "'");
                    
                     
                    }

                }
            }
        }
    }
}
