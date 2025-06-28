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
    public partial class CUSTOMER_INFOT : Form
    {
        DataTable dt = new DataTable();
        basec bc=new basec ();
        private string _IDO;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

        }
        private static string _MATERIAL;
        public static string MATERIAL
        {
            set { _MATERIAL = value; }
            get { return _MATERIAL; }
        }
        private static string _RETURN_DATA;
        public static string RETURN_DATA
        {
            set { _RETURN_DATA = value; }
            get { return _RETURN_DATA; }
        }
        private string _ADD_OR_UPDATE;
        public string ADD_OR_UPDATE
        {
            set { _ADD_OR_UPDATE = value; }
            get { return _ADD_OR_UPDATE; }
        }
        private static string _EMPLOYEE_ID;
        public static string EMPLOYEE_ID
        {
            set { _EMPLOYEE_ID = value; }
            get { return _EMPLOYEE_ID; }

        }
        private static string _ENAME;
        public static string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }

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
        CUSTOMER_INFO F1 = new CUSTOMER_INFO();
        protected int M_int_judge, i;
        protected int select;
        CCUSTOMER_INFO ccustomer_info = new CCUSTOMER_INFO();
        public CUSTOMER_INFOT()
        {
            InitializeComponent();
        }
        public CUSTOMER_INFOT(CUSTOMER_INFO FRM)
        {
            InitializeComponent();
            F1 = FRM;

        }
        private void CUSTOMER_INFOT_Load(object sender, EventArgs e)
        {

            this.Icon =  Resource1.xz_200X200;
            textBox1.Text = IDO;
            bind();
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        #region total1
        private DataTable total1()
        {
            DataTable dtt2 = ccustomer_info.GetTableInfo();
            for (i = 1; i <= 6; i++)
            {
                DataRow dr = dtt2.NewRow();
                dr["项次"] = i;
                dtt2.Rows.Add(dr);
            }
            return dtt2;
        }
        #endregion
    
        public void a1()
        {
            dataGridView1.ReadOnly = true;
            select = 0;
        }
        public void a2()
        {
            dataGridView1.ReadOnly = true;
            select = 1;
        }
        public void ClearText()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2 .Text ="";
            comboBox3.Text = "";
        }

        #region bind
        private void bind()
        {
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            textBox3.Focus();
            hint.Location = new Point(256, 136);
            hint.ForeColor = Color.Red;
            textBox3.BackColor = Color.Yellow;
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {

                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Text = "";
            }

            DataTable dtx = basec.getdts(ccustomer_info.sql + " where A.CUID='" + textBox1.Text + "' ORDER BY  B.CUID ASC ");
            if (dtx.Rows.Count > 0)
            {
               
                dt = ccustomer_info.GetTableInfo();
                textBox2.Text = dtx.Rows[0]["客户代码"].ToString();
                textBox3.Text = dtx.Rows[0]["客户名称"].ToString();
                comboBox1.Text =dtx.Rows[0]["付款方式"].ToString();
                comboBox2.Text = dtx.Rows[0]["付款条件"].ToString();
                foreach (DataRow dr1 in dtx.Rows)
                {
           
                    DataRow dr = dt.NewRow();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["联系人"] = dr1["联系人"].ToString();
                    dr["材料"] = dr1["材料"].ToString();
                    dr["基数"] = dr1["基数"].ToString();
                    dr["联系电话"] = dr1["联系电话"].ToString();
                    dr["传真号码"] = dr1["传真号码"].ToString();
                    dr["邮政编码"] = dr1["邮政编码"].ToString();
                    dr["EMAIL"] = dr1["EMAIL"].ToString();
                    dr["公司地址"] = dr1["公司地址"].ToString();
                    dr["品牌"] = dr1["品牌"].ToString();
                    dr["客户类别"] = dr1["客户类别"].ToString();
                    dr["部门"] = dr1["部门"].ToString();
                    dr["QQ号"] = dr1["QQ号"].ToString();
                    dr["备注"] = dr1["备注"].ToString();
                    dr["手机号码"] = dr1["手机号码"].ToString();
                    if (dr1["默认联系人"].ToString() == "是")
                    {
                        dr["默认联系人"] = "True";
                    }
                    else
                    {
                        dr["默认联系人"] = "False";
                    }
                    dt.Rows.Add(dr);
                 
                }

                if (dt.Rows.Count > 0 && dt.Rows.Count < 6)
                {
                    int n = 6 - dt.Rows.Count;
                    for (int i = 0; i < n; i++)
                    {

                        DataRow dr = dt.NewRow();
                        int b1 = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["项次"].ToString());
                        dr["项次"] = Convert.ToString(b1 + 1);
                        dt.Rows.Add(dr);
                    }
                }
                
            }
            else
            {
                dt = total1();
            }
            dataGridView1.DataSource = dt;
            dgvStateControl();
        }
        #endregion
        private void btnAdd_Click(object sender, EventArgs e)
        {
            add();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            M_int_judge = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
         
            try
            {

                btnSave.Focus();
                if (juage())
                {
                    IFExecution_SUCCESS = false;
                }
                else
                {

                    save();
                    if (IFExecution_SUCCESS == true && ADD_OR_UPDATE == "ADD")
                    {
                        add();
                    }

                    F1.load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);


            }
        }
        private void add()
        {
            ClearText();
            textBox1.Text = ccustomer_info.GETID();
            bind();
            ADD_OR_UPDATE = "ADD";
        }
        private void save()
        {

            btnSave.Focus();
            //dgvfoucs();
            if (dt.Rows.Count > 0)
            {
                DataTable dtx = bc.GET_NOEXISTS_EMPTY_ROW_DT(dt, "", "材料 IS NOT NULL");
                if (dtx.Rows.Count > 0)
                {

                    ccustomer_info.EMID = LOGIN.EMID;
                    ccustomer_info.CUID = textBox1.Text;
                    ccustomer_info.CUSTOMER_ID = textBox2.Text;
                    ccustomer_info.CNAME = textBox3.Text;
                    ccustomer_info.PAYMENT = comboBox1.Text;
                    ccustomer_info.PAYMENT_CLAUSE = comboBox2.Text;
                    ccustomer_info.PROVINCE = comboBox3.Text;
                
                    ccustomer_info.save(dtx);
                    IFExecution_SUCCESS = ccustomer_info.IFExecution_SUCCESS;
                    hint.Text = ccustomer_info.ErrowInfo;
                    if (IFExecution_SUCCESS)
                    {
                      
                        bind();
                    }
                    /*F1.Bind();
                    F1.search();*/

                }
                else
                {
                
                    hint.Text = "至少有一项材料才能保存！";

                }
            }
           
        }
        private bool juage()
        {
            bool b = false;
           if (textBox3 .Text  == "")
            {
                hint.Text = "客户名称不能为空！";
                b = true;
            }
           else if(juage2())
           {
            
               b = true;
            }
          /*else if (juage3()==0)
           {
               hint.Text = "需点选一个默认联系人！";
               b = true;
           }*/
           else if (juage3()>1)
           {
               hint.Text = "默认联系人只能选择一个！";
               b = true;
           }
            return b;
        }
        #region juage2()
        private bool juage2()
        {
            bool b = false;
            DataTable dtx = bc.GET_NOEXISTS_EMPTY_ROW_DT(dt, "", "材料 IS NOT NULL");
            foreach (DataRow dr in dtx.Rows)
            {
                string v1 = dr["联系电话"].ToString();
                string v2 = dr["传真号码"].ToString();
                string v3 = dr["邮政编码"].ToString();
                string v4 = dr["公司地址"].ToString();
                string v5 = dr["QQ号"].ToString();
                string v6 = dr["品牌"].ToString();
                string v7 = dr["客户类别"].ToString();
                string v8= dr["基数"].ToString();
                if (bc.checkphone(v1) == false)
                {
                    b = true;
                    hint.Text = "项次" + dr["项次"].ToString() + " 电话号码只能输入数字！";

                }
                else if (v8 =="")
                {
                    b = true;
                    hint.Text = "项次" + dr["项次"].ToString() + " 基数不能为空！";
                }
                else if (bc.checkphone(v8) == false)
                {
                    b = true;
                    hint.Text = "项次" + dr["项次"].ToString() + " 基数只能输入数字！";

                }
                else if (bc.checkphone(v5) == false)
                {
                    b = true;
                    hint.Text = "项次" + dr["项次"].ToString() + " QQ号只能输入数字！";

                }
               /* else if (v5!="" && bc.exists("SELECT * FROM CUSTOMERINFO_DET WHERE QQ='" + v5 + "' AND CUID!='"+ textBox1 .Text +"'"))
                {
                    b = true;
                    hint.Text = "项次" + dr["项次"].ToString() + " QQ号码已经存在！";

                }*/
         
                else if (bc.checkphone(v2) == false)
                {
                    b = true;
                    hint.Text = "项次" + dr["项次"].ToString() + " 传真号码只能输入数字！";

                }
                else if (bc.checkphone(v3) == false)
                {
                    b = true;
                    hint.Text ="项次" + dr["项次"].ToString() + " 邮编只能输入数字！";

                }
            }
        
            return b;
        }
        #endregion
        #region juage3()
        private int juage3()
        {
            DataTable dtx = bc.GET_NOEXISTS_EMPTY_ROW_DT(dt, "", "联系人 IS NOT NULL");
            int n = 0;
            foreach (DataRow dr in dtx.Rows)
            {
                string v1 = dr["默认联系人"].ToString();
                if (v1=="True")
                {
                    n = n + 1;

                }
            }
            return n;
        }
        #endregion
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                 if (MessageBox.Show("确定要删除该条凭证吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (bc.exists("SELECT * FROM ORDER_MST WHERE CUID='"+textBox1 .Text +"'"))
                    {
                        hint.Text = "此客户信息在项目信息作业中存在不允许删除";
                    }
                    else if (bc.exists("SELECT * FROM MOLD_BASE WHERE CUID='" + textBox1.Text + "'"))
                    {
                        hint.Text = "此客户信息在模具库作业中存在不允许删除";
                    }
                    else
                    {
                        basec.getcoms("DELETE CUSTOMERINFO_MST WHERE CUID='" + textBox1.Text + "'");
                        basec.getcoms("DELETE CUSTOMERINFO_DET WHERE CUID='" + textBox1.Text + "'");

                        bind();
                        ClearText();
                        textBox1.Text = "";
                        F1.load();
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
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            dataGridView1.ClearSelection();//加载不选中第一列
            int numCols1 = dataGridView1.Columns.Count;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;/*自动调整DATAGRIDVIEW的列宽*/
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
            //dataGridView1.Columns["联系人"].DefaultCellStyle.BackColor = Color.Yellow;
            //dataGridView1.Columns["公司地址"].DefaultCellStyle.BackColor = Color.Yellow;
            dataGridView1.Columns["材料"].DefaultCellStyle.BackColor = Color.Yellow;
            dataGridView1.Columns["基数"].DefaultCellStyle.BackColor = Color.Yellow;
            dataGridView1.Columns["项次"].ReadOnly = true;
            dataGridView1.Columns["联系人"].ReadOnly = false;
            dataGridView1.Columns["联系电话"].ReadOnly = false;
            dataGridView1.Columns["传真号码"].ReadOnly = false;
            dataGridView1.Columns["邮政编码"].ReadOnly = false;
            dataGridView1.Columns["EMAIL"].ReadOnly = false;
            dataGridView1.Columns["公司地址"].ReadOnly = false;
            dataGridView1.Columns["客户类别"].ReadOnly = true;
            dataGridView1.Columns["项次"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

   

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int a = dataGridView1.CurrentCell.ColumnIndex;
                int b = dataGridView1.CurrentCell.RowIndex;
                int c = dataGridView1.Columns.Count - 1;
                int d = dataGridView1.Rows.Count - 1;
                if (a == c && b == d)
                {
                    if (dt.Rows.Count >= 6)
                    {

                        DataRow dr = dt.NewRow();
                        int b1 = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["项次"].ToString());
                        dr["项次"] = Convert.ToString(b1 + 1);
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
   
            //dgvfoucs();

        }

        private void 删除此项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
    
            try
            {

                string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
                string sql2 = "DELETE FROM CUSTOMERINFO_DET WHERE CUID='" + textBox1.Text + "' AND SN='" + v1 + "'";
                if (dt.Rows.Count > 0)
                {

                    if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        if (!bc.exists("SELECT * FROM CUSTOMERINFO_DET WHERE CUID='" + textBox1.Text + "' AND SN='" + v1 + "'"))
                        {
                            hint.Text = "此条记录还未写入数据库";
                        }
                        else if (bc.juageOne("SELECT * FROM CUSTOMERINFO_DET WHERE CUID='" + textBox1.Text + "'"))
                        {

                            basec.getcoms(sql2);
                            string sql3 = "DELETE CUSTOMERINFO_MST WHERE CUID='" + textBox1.Text + "'";
                            basec.getcoms(sql3);
                            basec.getcoms("DELETE REMARK WHERE CUID='" + textBox1.Text + "'");
                            IFExecution_SUCCESS = false;
                            bind();
                        }
                        else
                        {

                            basec.getcoms(sql2);

                            IFExecution_SUCCESS = false;
                            bind();
                        }
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
             
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            IF_DOUBLE_CLICK = false;
            int rows = dataGridView1.CurrentCell.RowIndex;
            int columns = dataGridView1.CurrentCell.ColumnIndex;
    if (dataGridView1.Columns[columns].DataPropertyName.ToString() == "材料")
            {
                BASE_INFO.MATERIAL FRM = new MATERIAL();
                FRM.SELECT = 1;
                FRM.ShowDialog();
                if (FRM.MATERIAL_VALUE !="")
                {
                    dt.Rows[rows]["材料"] = FRM.MATERIAL_VALUE;
                }
                dataGridView1.CurrentCell = dataGridView1["基数", rows];
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
 
    }
}
