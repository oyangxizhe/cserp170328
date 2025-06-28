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

namespace CSPSS.BASE_INFO
{
    public partial class TYPE : Form
    {
        DataTable dt = new DataTable();
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
        private int _SELECT;
        public int SELECT
        {
            set { _SELECT = value; }
            get { return _SELECT; }
        }
        private string _TYPE_VALUE;
        public  string TYPE_VALUE
        {
            set { _TYPE_VALUE = value; }
            get { return _TYPE_VALUE; }
        }
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        basec bc = new basec();
        protected string sql = @"
SELECT 
A.TYPE AS 类别,
(SELECT ENAME FROM EMPLOYEEINFO 
WHERE EMID=A.MAKERID ) AS 制单人,
A.DATE AS 制单日期
FROM
TYPE A ";
   
        protected int M_int_judge, i;
        protected int select;
        public TYPE()
        {
            InitializeComponent();
        }
        #region double_click
        private void dgvEmployeeInfo_DoubleClick(object sender, EventArgs e)
        {
            
        }
        #endregion
        public string GETID()
        {
            string v1 = bc.numYM(10, 4, "0001", "SELECT * FROM TYPE", "TYID", "TY");
            string GETID = "";
            if (v1 != "Exceed Limited")
            {
                GETID = v1;
            }
            return GETID;
        }
        private void DEPAET_Load(object sender, EventArgs e)
        {
            TYPE_VALUE = "";
            this.Icon =  Resource1.xz_200X200;

           
            groupBox1.Text = "类别信息";
            this.Text = "类别信息";
            label14.Text = "类别信息";
            label1.Text = "类别信息";
            bind();
        }
        public void CUSTOMERINFO_USE()
        {
            dataGridView1.Enabled = true;
            select = 1;
        }
        public void ORDER_USE()
        {
            dataGridView1.Enabled = true;
            select = 2;
        }
        private void bind()
        {
           
            dt = basec.getdts(sql);
            dataGridView1.DataSource = dt;
            textBox1.Focus();
            textBox1.BackColor = CCOLOR.CUSTOMER_YELLOW;
            dgvStateControl();
            hint.Location = new Point(256, 136);
            hint.ForeColor = Color.Red;
            dataGridView1.AllowUserToAddRows = false;
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Text = "";
            }
        }
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {

                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;/*自动调整DATAGRIDVIEW的列宽*/
            
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.Columns[i].HeaderCell.Style.BackColor = Color.Lavender;

            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].DefaultCellStyle.BackColor = Color.OldLace;
                i = i + 1;
            }
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].ReadOnly = true;

            }
            dataGridView1.Columns["制单人"].Width = 70;
        }
        #endregion
    
        #region save
        private void save()
        {

            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss");
            string varMakerID = LOGIN.EMID;
            if (!bc.exists("SELECT TYID FROM TYPE WHERE TYID='" + IDO  + "'"))
            {

                if (bc.exists("SELECT * FROM TYPE WHERE TYPE='"+textBox1 .Text  +"'"))
                {
                    hint.Text = "此类别已经存在";
                    IFExecution_SUCCESS = false;
                }
                else
                {
                    basec.getcoms(@"INSERT INTO TYPE(TYID,TYPE,MAKERID,DATE,YEAR,
                                   MONTH) VALUES ('" + IDO  + "','" + textBox1.Text +
                     "','" + varMakerID + "','" + varDate +
                     "','" + year + "','" + month + "')");
                    IFExecution_SUCCESS = true;
                    bind();
                }

            }
            else
            {
                if (bc.exists("SELECT * FROM TYPE WHERE TYPE='" + textBox1 .Text  + "'"))
                {
                    hint.Text = "此类别已经存在";
                    IFExecution_SUCCESS = false;
                }
                else
                {
                    basec.getcoms(@"UPDATE TYPE SET TYPE='" + textBox1.Text + "',MAKERID='" + varMakerID +
                          "',DATE='" + varDate + "' WHERE TYID='" +IDO  + "'");
                    IFExecution_SUCCESS = true;
                    bind();
                }
            }
           
        }
        #endregion
        #region juage()
        private bool juage()
        {
            bool b = false;
            if (IDO ==null)
            {
                b = true;
                hint.Text = "编号不能为空！";
            }
            else if (textBox1.Text == "")
            {
                b = true;
                hint.Text = "类别不能为空！";
            }
            return b;

        }
        #endregion
        public void ClearText()
        {
            textBox1.Text = "";
        }

        private void dgvEmployeeInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            hint.Text = "";
            string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex]["类别"].ToString();
            if (select != 0 || SELECT==1)
            {
                TYPE_VALUE = v1;
                this.Close();
            }
            else
            {
                if (v1 != "")
                {
                    IDO = bc.getOnlyString("SELECT TYID FROM TYPE WHERE TYPE='" + v1 + "'");
                    textBox1.Text = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
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
        private void add()
        {
            ClearText();
            IDO = GETID();
            textBox1.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (juage())
            {

            }
            else
            {
                save();
                if (IFExecution_SUCCESS)
                {
                    add();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dt = bc.getdt(sql+" WHERE  A.TYPE LIKE '%"+textBox5 .Text +"%'");
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dgvStateControl();
                }
                else
                {
                    hint.Text = "没有找到相关信息！";
                    dataGridView1.DataSource = null;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
          
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (MessageBox.Show("确定要删除该条凭证吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        /*if (bc.exists("SELECT * FROM CUSTOMERINFO_DET WHERE TYID='" + textBox1.Text + "'"))
                        {
                            hint.Text = "此类别信息在客户信息作业中存在不允许删除";
                        }
                        else if (bc.exists("SELECT * FROM MOLD_BASE WHERE TYID='" + textBox1.Text + "'"))
                        {
                            hint.Text = "此类别信息在模具库作业中存在不允许删除";
                        }
                        else
                        {
                           
                        }*/
                        string id = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                        IFExecution_SUCCESS = false;
                        string strSql = "DELETE FROM TYPE WHERE TYPE='" + id + "'";
                        basec.getcoms(strSql);
                        bind();
                        ClearText();
                    }
                }
                else
                {
                   
                    MessageBox.Show("无数据可以删除", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {


            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #region override enter
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && ((!(ActiveControl is System.Windows.Forms.TextBox) ||
                !((System.Windows.Forms.TextBox)ActiveControl).AcceptsReturn)))
            {

                if (dataGridView1.CurrentCell.ColumnIndex == 7 &&
                    dataGridView1["借方原币金额", dataGridView1.CurrentCell.RowIndex].Value.ToString() != null)
                {

                    SendKeys.SendWait("{Tab}");
                    SendKeys.SendWait("{Tab}");
                }
                else if (dataGridView1.CurrentCell.ColumnIndex == 9)
                {
                    SendKeys.SendWait("{Tab}");
                    SendKeys.SendWait("{Tab}");
                    SendKeys.SendWait("{Tab}");
                }
                else
                {

                    SendKeys.SendWait("{Tab}");
                }
                return true;
            }
            if (keyData == (Keys.Enter | Keys.Shift))
            {
                SendKeys.SendWait("+{Tab}");

                return true;
            }
            if (keyData == (Keys.F7))
            {

                dataGridView1.Focus();

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

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
        private void dgvCopy(ref DataGridView dgv)
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

        private void TYPE_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            add();
        }
  
    }
}
