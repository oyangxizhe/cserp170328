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


namespace CSPSS.BASE_INFO
{
    public partial class ACCOUNTANT_COURSE : Form
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        protected int M_int_judge, t;
        basec bc = new basec();
        ExcelToCSHARP etc = new ExcelToCSHARP();
        Color c = System.Drawing.ColorTranslator.FromHtml("#efdaec");
        CACCOUNTANT_COURSE caccountant_course = new CACCOUNTANT_COURSE();
        private string _IDO;
        protected int select;
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
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        private string _ACCODE;
        public string ACCODE
        {
            set { _ACCODE = value; }
            get { return _ACCODE; }
        }
        private int _SELECT;
        public int SELECT
        {
            set { _SELECT = value; }
            get { return _SELECT; }
        }
        Color c2 = System.Drawing.ColorTranslator.FromHtml("#990033");
        public ACCOUNTANT_COURSE()
        {
            InitializeComponent();
        }
        private void ACCOUNTANT_COURSE_Load(object sender, EventArgs e)
        {
            textBox2.BackColor = CCOLOR.YELLOW;
            bind();
        }
        private void currency()
        {
            DataTable dtx = bc.getdt("SELECT * FROM CURRENCY_MST WHERE CYCODE='RMB'");
        }
        #region override enter
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && ((!(ActiveControl is System.Windows.Forms.TextBox) ||
                !((System.Windows.Forms.TextBox)ActiveControl).AcceptsReturn)))
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
        #region bind
        private void bind()
        {
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
            if (ADD_OR_UPDATE == "UPDATE")
            {
               
            }
            else
            {
                textBox1.Text = caccountant_course.GETID();
            }
            textBox3.Focus();
            think();
        }
        #endregion
        #region think
        private void think()
        {
            dt = bc.getdt(@"SELECT [ACID] AS 账户编号
      ,[ACCODE] AS 账户名
      ,[ACNAME] AS 开户行
      ,[COURSE_TYPE] AS 账号
  FROM [ACCOUNTANT_COURSE]");
            dt = bc.GET_DT_TO_DV_TO_DT(dt, "", "账户名 LIKE '%" + textBox5.Text + "%' AND 账号 LIKE '%" + textBox6.Text + "%'");
            dataGridView1.DataSource = dt;
            dgvStateControl();
        }
        #endregion
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.AllowUserToAddRows = false;
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
        }
        #endregion
        #region show_treeview_O
        private void SHOW_TREEVIEW_O(string ACID,TreeNode trd)
        {

                    dt2 = bc.getdt("SELECT * FROM ACCOUNTANT_COURSE WHERE PARENT_NODEID='" + ACID + "'");
                    if (dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt2.Rows)
                        {

                            TreeNode TRC = new TreeNode();
                            TRC.Text =dr1["ACCODE"].ToString()+" "+dr1["ACNAME"].ToString ();
                            trd.Nodes.Add(TRC);
                            if (TRC.Text == textBox2.Text+" "+textBox3 .Text )
                            {

                                TRC.BackColor = c;
                              
                            }
                            SHOW_TREEVIEW_O(dr1["ACID"].ToString(),TRC);
                          
                        }
                   }
        }
        #endregion
        #region bind1
        private void bind(DataTable dt)
        {

            try
            {
                if (dt.Rows.Count > 0)
                {
                    textBox1.Text = dt.Rows[0]["ACID"].ToString();
                    textBox2.Text = dt.Rows[0]["ACCODE"].ToString();
                    textBox3.Text = dt.Rows[0]["ACNAME"].ToString();
                    bind2(dt.Rows[0]["ACCODE"].ToString());
                  
                }
                think();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region bind2
        private void bind2(string ACCODE)
        {
       
        }
        #endregion
       
        #region save
        protected void save()
        {
            etc.EMID = LOGIN.EMID;
            etc.save(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, "", "", "");
            ADD_OR_UPDATE = etc.ADD_OR_UPDATE;
        }
        private void COURSE_TYPE_LOAD()
        {
            if (textBox2.Text.Length > 0)
            {
                int k = Convert.ToInt32(textBox2.Text.Substring(0, 1));
                dt = etc.GetCOURSE_TypeData(k);
            }

            if (dt.Rows.Count > 0)
            {

                //bind(dt);
            }
            else
            {
                textBox1.Text = "";
                ClearText();
               
            }
            think();
            textBox2.Focus();
            LoadAgain();
            //
        }
        #endregion
        


        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
        
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
        #region excelprint
        private void btnExcelPrint_Click(object sender, EventArgs e)
        {

        }
        #endregion
        #region btnadd

        #endregion
        #region loadagain
        private void LoadAgain()
        {
            ClearText();
            string a1 = bc.numYM(10, 4, "0001", "select * from Accountant_Course", "ACID", "AC");
            if (a1 == "Exceed Limited")
            {
                MessageBox.Show("编码超出限制！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                textBox1.Text = a1;
            }
            //dataGridView1.DataSource = total1();
        }
        #endregion
        private void ClearText()
        {
            textBox2.Text = "";
            textBox3.Text = "";
       
          
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            save1();

        }
        private void save1()
        {
            try
            {
                if (textBox2.Text == "")
                {
                    
                    hint.Text = "账户名不能为空！";
                }
                else
                {
                    save();
                    hint.Text = etc.hint;
                    IFExecution_SUCCESS = etc.IFExecution_SUCCESS;
                    if (etc.IFExecution_SUCCESS && etc.ADD_OR_UPDATE =="ADD")
                    {
                        ClearText();
                        bind();
                    }
                    else if(etc.IFExecution_SUCCESS && etc.ADD_OR_UPDATE =="UPDATE")
                    {
                       
                        bind();

                    }
                }
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
        #region btndel
        private void btnDel_Click(object sender, EventArgs e)
        {


            try
            {
                if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    string v = textBox1.Text;
                    string v1 = bc.getOnlyStringO("ACCOUNTANT_COURSE", "ACNAME", "ACID", v);
                    string v2 = bc.getOnlyStringO("ACCOUNTANT_COURSE", "ACCODE", "ACID", v);

                   if (bc.exists("VOUCHER_DET", "ACID", v, "账户 " +textBox2.Text  + " " + "已经有银行收支记录不允许删除！"))
                    {

                    }

                    else
                    {
                        basec.getcoms("DELETE Accountant_Course WHERE ACID='" + v + "'");
                        bind();
                    }
                    //ClearText();
                    //textBox1.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
            
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            dt = etc.GetCOURSE_TypeData(1);
            bind(dt);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt = etc.GetCOURSE_TypeData(2);
            bind(dt);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            dt = etc.GetCOURSE_TypeData(3);
            bind(dt);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            dt = etc.GetCOURSE_TypeData(4);
            bind(dt);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dt = etc.GetCOURSE_TypeData(5);
            bind(dt);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dt = etc.GetCOURSE_TypeData(6);
            bind(dt);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            bind();
         
      
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
           
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {

            }
            else if (textBox2.Text.Length > 4)
            {

                bind2(textBox2.Text.Substring(0, 4));
            }

        }
        public void a5()
        {
            select = 1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LoadAgain();
            textBox2.Focus();
            currency();
        
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==13)
            {
                save1();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string v1 = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            if (v1 != "")
            {
                if(SELECT !=0)
                {
                    ACCODE = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    FINANCIAL_MANAGE.VOUCHERT.IF_DOUBLE_CLICK = true;
                    this.Close();
                }
                else 
                {
                    textBox1.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    textBox2.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    textBox3.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    textBox4.Text = Convert.ToString(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                }
            
               
            }
        }

      
    }
}
