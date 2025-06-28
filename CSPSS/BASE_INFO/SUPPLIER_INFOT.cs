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
    public partial class SUPPLIER_INFOT : Form
    {
        DataTable dt = new DataTable();
        basec bc=new basec ();
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
        SUPPLIER_INFO F1 = new SUPPLIER_INFO();
        protected int M_int_judge, i;
        protected int select;
        CSUPPLIER_INFO CSUPPLIER_INFO = new CSUPPLIER_INFO();
        public SUPPLIER_INFOT()
        {
            InitializeComponent();
        }
        public SUPPLIER_INFOT(SUPPLIER_INFO FRM)
        {
            InitializeComponent();
            F1 = FRM;

        }
        private void SUPPLIER_INFOT_Load(object sender, EventArgs e)
        {
            textBox1.Text = IDO;
            bind();
        }
        #region total1
        private DataTable total1()
        {
            DataTable dtt2 = CSUPPLIER_INFO.GetTableInfo();
            for (i = 1; i <= 6; i++)
            {
                DataRow dr = dtt2.NewRow();
                dr["项次"] = i;
                dtt2.Rows.Add(dr);
            }
            return dtt2;
        }
        #endregion
        private void dgvClientInfo_DoubleClick(object sender, EventArgs e)
        {
            /*int intCurrentRowNumber = this.dataGridView1.CurrentCell.RowIndex;
            string s1 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[0].Value.ToString().Trim();
            string s2 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[1].Value.ToString().Trim();
            string s3 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[2].Value.ToString().Trim();
            string s4 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[5].Value.ToString().Trim();
            if (select == 0)
            {

              

            }
            if (select == 1)
            {

             

            }
            this.Close();*/
        }
      
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

 
        private void dgvClientInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
         
     
          
        }

        public void ClearText()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2 .Text ="";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox15.Text = "";

  
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            bind();
            try
            {
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #region bind
        private void bind()
        {

          
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            textBox3.Focus();
            hint.Location = new Point(400, 100);
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


            DataTable dtx = basec.getdts(CSUPPLIER_INFO.sql + " where A.SUID='" + textBox1.Text + "' ORDER BY  B.SUID ASC ");
            if (dtx.Rows.Count > 0)
            {
               
                dt = CSUPPLIER_INFO.GetTableInfo();
                textBox2.Text = dtx.Rows[0]["供应商代码"].ToString();
                textBox3.Text = dtx.Rows[0]["供应商名称"].ToString();
                comboBox1.Text =dtx.Rows[0]["收款方式"].ToString();
                comboBox2.Text = dtx.Rows[0]["收款条件"].ToString();

                textBox4.Text = dtx.Rows[0]["定义1"].ToString();
                textBox5.Text = dtx.Rows[0]["定义2"].ToString();
                textBox6.Text = dtx.Rows[0]["定义3"].ToString();
                textBox7.Text = dtx.Rows[0]["定义4"].ToString();
                textBox8.Text = dtx.Rows[0]["定义5"].ToString();
                textBox9.Text = dtx.Rows[0]["定义6"].ToString();
                textBox10.Text = dtx.Rows[0]["定义7"].ToString();
                textBox11.Text = dtx.Rows[0]["定义8"].ToString();
                textBox12.Text = dtx.Rows[0]["定义9"].ToString();
                textBox13.Text = dtx.Rows[0]["定义10"].ToString();
                textBox14.Text = dtx.Rows[0]["水印内容"].ToString();
                textBox15.Text = dtx.Rows[0]["备注"].ToString();

                if (dtx.Rows[0]["是否需业务审核"].ToString() == "是")
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
                if (dtx.Rows[0]["是否需财务审核"].ToString() == "是")
                {
                    checkBox2.Checked = true;
                }
                else
                {
                    checkBox2.Checked = false;

                }
                if (dtx.Rows[0]["是否需文员审核"].ToString() == "是")
                {
                    checkBox3.Checked = true;
                   
                }
                else
                {
                    checkBox3.Checked = false;

                }
                foreach (DataRow dr1 in dtx.Rows)
                {
           
                    DataRow dr = dt.NewRow();
                    dr["项次"] = dr1["项次"].ToString();
                    dr["联系人"] = dr1["联系人"].ToString();
                    dr["联系电话"] = dr1["联系电话"].ToString();
                    dr["传真号码"] = dr1["传真号码"].ToString();
                    dr["邮政编码"] = dr1["邮政编码"].ToString();
                    dr["EMAIL"] = dr1["EMAIL"].ToString();
                    dr["公司地址"] = dr1["公司地址"].ToString();
                    dr["部门"] = dr1["部门"].ToString();
                    dr["QQ号"] = dr1["QQ号"].ToString();
                    dr["旺旺号"] = dr1["旺旺号"].ToString();
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
            textBox1.Text = CSUPPLIER_INFO.GETID();
            bind();
         
            ADD_OR_UPDATE = "ADD";
           

        }
        private void save()
        {

            btnSave.Focus();
            //dgvfoucs();
            if (dt.Rows.Count > 0)
            {
                DataTable dtx = bc.GET_NOEXISTS_EMPTY_ROW_DT(dt, "", "联系人 IS NOT NULL");
                if (dtx.Rows.Count > 0)
                {

                    CSUPPLIER_INFO.EMID = LOGIN.EMID;
                    CSUPPLIER_INFO.SUID = textBox1.Text;
                    CSUPPLIER_INFO.SUPPLIER_ID = textBox2.Text;
                    CSUPPLIER_INFO.SNAME = textBox3.Text;
                    CSUPPLIER_INFO.PAYMENT = comboBox1.Text;
                    CSUPPLIER_INFO.PAYMENT_CLAUSE = comboBox2.Text;

                    CSUPPLIER_INFO.USER_DEFINED_ONE = textBox4.Text;
                    CSUPPLIER_INFO.USER_DEFINED_TWO = textBox5.Text;
                    CSUPPLIER_INFO.USER_DEFINED_THREE = textBox6.Text;
                    CSUPPLIER_INFO.USER_DEFINED_FOUR = textBox7.Text;
                    CSUPPLIER_INFO.USER_DEFINED_FIVE = textBox8.Text;
                    CSUPPLIER_INFO.USER_DEFINED_SIX = textBox9.Text;
                    CSUPPLIER_INFO.USER_DEFINED_SEVEN = textBox10.Text;
                    CSUPPLIER_INFO.USER_DEFINED_EIGHT = textBox11.Text;
                    CSUPPLIER_INFO.USER_DEFINED_NINE = textBox12.Text;
                    CSUPPLIER_INFO.USER_DEFINED_TEN = textBox13.Text;
                    CSUPPLIER_INFO.WATER_MARK_CONTENT = textBox14.Text;
                    CSUPPLIER_INFO.REMARK = textBox15.Text;

                    string v1="",v2 ="",v3 = "";
                    if (checkBox1.Checked)
                    {
                        CSUPPLIER_INFO.SALE_AUDIT = "Y";
                        v1 = "Y";
                    }
                    else
                    {
                        CSUPPLIER_INFO.SALE_AUDIT = "N";
                        v1 = "N";
                    }
                    if (checkBox2.Checked)
                    {
                        v2 = "Y";
                        CSUPPLIER_INFO.FINANCIAL_AUDIT = "Y";
                    }
                    else
                    {
                        CSUPPLIER_INFO.FINANCIAL_AUDIT = "N";
                        v2= "N";
                    }
                    if (checkBox3.Checked)
                    {
                        CSUPPLIER_INFO.OFFICE_AUDIT = "Y";
                        v3= "Y";
                    }
                    else
                    {
                        CSUPPLIER_INFO.OFFICE_AUDIT = "N";
                        v3= "N";
                    }


                    if (v1 == "N" && v2 == "N" && v3 == "N")
                    {
                     
                        CSUPPLIER_INFO.AUDIT_STYLE = "NNN";
                    }
                    else if (v1 == "Y" && v2 == "N" && v3 == "N")
                    {

                        CSUPPLIER_INFO.AUDIT_STYLE = "YNN";
                    }
                    else if (v1 == "N" && v2 == "Y" && v3 == "N")
                    {

                        CSUPPLIER_INFO.AUDIT_STYLE = "NYN";
                    }
                    else if (v1 == "N" && v2 == "N" && v3 != "Y")
                    {
                      
                        CSUPPLIER_INFO.AUDIT_STYLE = "NNY";
                    }
                    else if (v1 == "Y" && v2 == "Y" && v3 == "N")
                    {
                    
                        CSUPPLIER_INFO.AUDIT_STYLE = "YYN";
                    }
                    else if (v1 == "Y" && v2 == "N" && v3 == "Y")
                    {

                        CSUPPLIER_INFO.AUDIT_STYLE = "YNY";
                    }
                    else if (v1 == "N" && v2 == "Y" && v3 == "Y")
                    {

                        CSUPPLIER_INFO.AUDIT_STYLE = "NYY";
                    }
                    else if (v1 == "Y" && v2 == "Y" && v3 == "Y")
                    {

                        CSUPPLIER_INFO.AUDIT_STYLE = "YYY";
                    }
                   // MessageBox.Show(v1 + "," + v2 + "," + v3);
                    
                    CSUPPLIER_INFO.save(dtx);
                    IFExecution_SUCCESS = CSUPPLIER_INFO.IFExecution_SUCCESS;
                    hint.Text = CSUPPLIER_INFO.ErrowInfo;
                    if (IFExecution_SUCCESS)
                    {
                      
                        bind();
                    }
                  

                }
                else
                {
                
                    hint.Text = "至少有一项联系人及地址才能保存！";

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
        private bool juage()
        {
            bool b = false;
           if (textBox3 .Text  == "")
            {
                hint.Text = "供应商名称不能为空！";
                b = true;
            }
           else if(juage2())
           {
            
               b = true;
            }
           else if (juage3()==0)
           {
               hint.Text = "需点选一个默认联系人！";
               b = true;
           }
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
            DataTable dtx = bc.GET_NOEXISTS_EMPTY_ROW_DT(dt, "", "联系人 IS NOT NULL");
            foreach (DataRow dr in dtx.Rows)
            {
                
                string v1 = dr["联系电话"].ToString();
                string v2 = dr["传真号码"].ToString();
                string v3 = dr["邮政编码"].ToString();
                string v4 = dr["公司地址"].ToString();
                string v5 = dr["QQ号"].ToString();

                if (bc.checkphone(v1) == false)
                {
                    b = true;
                    hint.Text = "项次" + dr["项次"].ToString() + " 电话号码只能输入数字！";

                }
                else if (bc.checkphone(v5) == false)
                {
                    b = true;
                    hint.Text = "项次" + dr["项次"].ToString() + " QQ号只能输入数字！";

                }
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
                if (v4 == "")
                {
                 
                    hint.Text = "项次" + dr["项次"].ToString() + " 公司地址不能为空";
                    b = true;
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
                    basec.getcoms("DELETE SUPPLIERINFO_MST WHERE SUID='" + textBox1.Text + "'");
                    basec.getcoms("DELETE SUPPLIERINFO_DET WHERE SUID='" + textBox1.Text + "'");
                    bind();
                    ClearText();
                    textBox1.Text = "";
                    F1.load();
                  
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


       
            dataGridView1.Columns["联系人"].DefaultCellStyle.BackColor = Color.Yellow;
            dataGridView1.Columns["公司地址"].DefaultCellStyle.BackColor = Color.Yellow;
            dataGridView1.Columns["项次"].ReadOnly = true;
            dataGridView1.Columns["联系人"].ReadOnly = false;
            dataGridView1.Columns["联系电话"].ReadOnly = false;
            dataGridView1.Columns["传真号码"].ReadOnly = false;
            dataGridView1.Columns["邮政编码"].ReadOnly = false;
            dataGridView1.Columns["EMAIL"].ReadOnly = false;
            dataGridView1.Columns["公司地址"].ReadOnly = false;
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
           

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
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
            //dgvfoucs();

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
       
        }

        private void 删除此项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string v1 = dt.Rows[dataGridView1.CurrentCell.RowIndex][0].ToString();
            string sql2 = "DELETE FROM SUPPLIERINFO_DET WHERE SUID='" + textBox1.Text + "' AND SN='" + v1 + "'";
            if (dt.Rows.Count > 0)
            {

                if (MessageBox.Show("确定要删除该条信息吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (!bc.exists("SELECT * FROM SUPPLIERINFO_DET WHERE SUID='" + textBox1.Text + "' AND SN='"+v1+"'"))
                    {
                        hint.Text = "此条记录还未写入数据库";
                    }
                    else  if (bc.juageOne("SELECT * FROM SUPPLIERINFO_DET WHERE SUID='" + textBox1.Text + "'"))
                    {

                        basec.getcoms(sql2);
                        string sql3 = "DELETE SUPPLIERINFO_MST WHERE SUID='" + textBox1.Text + "'";
                        basec.getcoms(sql3);
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
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox151_TextChanged(object sender, EventArgs e)
        {

        }

     
   
    }
}
