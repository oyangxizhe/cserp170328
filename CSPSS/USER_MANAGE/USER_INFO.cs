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

namespace CSPSS.USER_MANAGE
{
    public partial class USER_INFO : Form
    {
        DataTable dt = new DataTable();
        CEMPLOYEE_INFO cemplyee_info = new CEMPLOYEE_INFO();
        private string _IDO;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

        }
        private int _GET_DATA_INT;
        public int GET_DATA_INT
        {
            set { _GET_DATA_INT = value; }
            get { return _GET_DATA_INT; }

        }
        private string _ADD_OR_UPDATE;
        public string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }
        }
        private static string _EMID;
        public static string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }
        }
        private string _UNAME;
        public string UNAME
        {
            set { _UNAME = value; }
            get { return _UNAME; }
        }
        private static string _EMPLOYEE_ID;
        public static string EMPLOYEE_ID
        {
            set { _EMPLOYEE_ID = value; }
            get { return _EMPLOYEE_ID; }
        }
        private static string _ENAME;
        public  static string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }
        }
        private static string _DEPART;
        public static string DEPART
        {
            set { _DEPART = value; }
            get { return _DEPART; }
        }
        private bool _IFExecutionSUCCESS;
        public  bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }
        }
        private  bool _IF_DOUBLE_CLICK;
        public  bool IF_DOUBLE_CLICK
        {
            set { _IF_DOUBLE_CLICK = value; }
            get { return _IF_DOUBLE_CLICK; }
        }
        basec bc = new basec();
        CEMPLOYEE_INFO cemployee_info = new CEMPLOYEE_INFO();
        CUSER cuser = new CUSER();
      

        protected int M_int_judge, i;
        protected int select;
        public USER_INFO()
        {
            InitializeComponent();
        }
        private void FrmUSER_INFO_Load(object sender, EventArgs e)
        {
         
            this.Icon =  Resource1.xz_200X200;
            Bind();
        }
        private void Bind()
        {
          
            dt = basec.getdts(cuser .sql );
            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            textBox1.Text = cuser.GETID();
          
            textBox2.BackColor = Color.Yellow;
     
            textBox3.BackColor = Color.Yellow;
            dgvStateControl();
            hint.ForeColor= Color.Red;
            hint.Location = new Point(400,100);
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {
              
                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Text  = "";
            }
          
            textBox2.Focus();
            textBox3.PasswordChar = '*';
            IF_DOUBLE_CLICK = false;
       

        }
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
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
        }
        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
      
            try
            {
                save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
   
        }
        #region save
        protected void save()
        {
            if (juage())
            {

            }
            else
            {
                hint.Text = "";
                string year = DateTime.Now.ToString("yy");
                string month = DateTime.Now.ToString("MM");
                string day = DateTime.Now.ToString("dd");
                string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss");
                string varMakerID = LOGIN.EMID;
                string v2 = bc.getOnlyString("SELECT UNAME FROM USERINFO WHERE  USID='" + textBox1.Text + "'");
                Byte[] B = bc.GetMD5(textBox3.Text);
                if (juage())
                {

                }
                else
                {
                    cuser.PWD = textBox3.Text;
                    cuser.EMID = "";
                    cuser.MAKERID = LOGIN.EMID;
                    cuser.USER_GROUP = "";
                    cuser.USER_TYPE = "";
                    cuser.UGID = "";
                    cuser.SNAME = textBox7.Text;
                    cuser.DEPART = comboBox1.Text;
                    cuser.save("USERINFO", "USID", "UNAME", textBox1.Text, textBox2.Text, "用户ID", "用户名","","",""  ,"","工号");
                    if (cuser.IFExecution_SUCCESS)
                    {
                        IFExecution_SUCCESS = cuser.IFExecution_SUCCESS;
                        add();
                        Bind();
                      
                    }
                    else
                    {
                        hint.Text = cuser.ErrowInfo;
                    }

                }

            }
          
        }
        #endregion
    
        #region juage()
        private bool juage()
        {

            string pwd = textBox3.Text;
            bool b = false;
            if (textBox2.Text  == "")
            {
                b = true;
                hint.Text = "用户名不能为空！";
            

            }
       
            else if (pwd == "")
            {
                b = true;
                hint.Text = "密码不能为空！";
       

            }
            else if (bc.checkEmail(pwd) == false)
            {
                b = true;
                hint.Text = "密码只能输入数字字母的组合";


            }
            else if (pwd.Length < 6)
            {
                b = true;
                hint.Text = "密码长度需大于6位！";
              

            }
            else if (!bc.checkNumber(pwd))
            {
                b = true;
                hint.Text = "密码需是数字与字母的组合！";

            }
            else if (!bc.checkLetter(pwd))
            {
                b = true;
                hint.Text = "密码需是数字与字母的组合！";

            }
           
        
            return b;

        }
        #endregion
        public void ClearText()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            hint.Text = "";
    
        
        }
        public void EditRight()
        {
            dataGridView1.Enabled = true;

        }
  

        private void btnAdd_Click(object sender, EventArgs e)
        {

            add();
        }
        private void add()
        {

            textBox1.Text = cuser.GETID();
            ClearText();
          

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {


                dt = bc.getdt(cuser.sql+@" WHERE A.USID LIKE '%"+textBox4.Text +"%' AND A.UNAME LIKE '%"+textBox5 .Text +
                    "%' ");
                if (dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = dt;
                    dgvStateControl();
                }
                else
                {

                    MessageBox.Show("没有找到相关信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                if (MessageBox.Show("确定要删除该条用户信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {

                    string v1 = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    if (bc.exists("SELECT * FROM WAREINFO WHERE MAKERID='"+v1+"'"))
                    {
                        //hint.Text = string.Format("用户名：{0} 已经在产品信息中存在， 不允许删除", textBox2.Text);
                        MessageBox.Show(string.Format("用户名：{0} 已经在产品信息中存在， 不允许删除", textBox2.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        bc.getcom("delete from USERINFO where USID='" + v1 + "'");
                        basec.getcoms("delete RightList where USID='" + v1 + "'");
                        basec.getcoms("delete SCOPE_OF_AUTHORIZATION where USID='" + v1 + "'");
                        Bind();
                    }
                }
                else
                {

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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

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
            if (keyData == (Keys.F7))
            {

                //double_info();

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string v1 = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            if (GET_DATA_INT ==0)
            {
                textBox1.Text = Convert.ToString(dataGridView1["用户编号", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                textBox2.Text = Convert.ToString(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                textBox7.Text = Convert.ToString(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value).Trim();
                comboBox1.Text = Convert.ToString(dataGridView1["部门", dataGridView1.CurrentCell.RowIndex].Value).Trim();
            }
            else
            {
                int indexNumber = dataGridView1.CurrentCell.RowIndex;
                string sendUserID;
                sendUserID = dataGridView1.Rows[indexNumber].Cells[1].Value.ToString().Trim();
                string sname = dataGridView1.Rows[indexNumber].Cells[2].Value.ToString().Trim();

            
                string[] inputarry = new string[] { sendUserID };
                CSPSS.USER_MANAGE.EDIT_RIGHT.UNAME = inputarry[0];
                CSPSS.USER_MANAGE.EDIT_RIGHT.ENAME = sname;
                IF_DOUBLE_CLICK = true;
                UNAME = sendUserID;
                EDIT_RIGHT.IF_DOUBLE_CLICK = true;
                STOCK_MANAGE.SCRAPT.IF_DOUBLE_CLICK = true;
                this.Close();


            }
         
        }
        
   

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

    
  

     

   

    
    }
}
