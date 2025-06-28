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
    public partial class SUPPLIER_INFO : Form
    {
        DataTable dt = new DataTable();
        basec bc=new basec ();
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
        protected int M_int_judge, i;
        protected int select;
        CSUPPLIER_INFO cSUPPLIER_info = new CSUPPLIER_INFO();
        public SUPPLIER_INFO()
        {
            InitializeComponent();
        }
        private void SUPPLIER_INFO_Load(object sender, EventArgs e)
        {
          
            bind();
        }
        public void VOUCHER_USE()
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
            if (bc.getOnlyString("SELECT UNAME FROM USERINFO WHERE USID='" + LOGIN.USID + "'") == "admin")
            {
                btnToExcel.Visible = true;
            }
            else
            {
                btnToExcel.Visible = false;
            }
            string sqlx = " WHERE  A.SUID LIKE '%" + textBox1.Text + "%' AND B.SUPPLIER_ID LIKE '%" + textBox2.Text +
               "%' AND  B.SNAME LIKE '%" + textBox3.Text + "%' ORDER  BY A.SUID ASC";

           dt=bc.getdt(cSUPPLIER_info.sql+sqlx );
        
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            textBox2.Focus();
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            IDO = cSUPPLIER_info.GETID();
            SUPPLIER_INFOT FRM = new SUPPLIER_INFOT(this);
            FRM.IDO = cSUPPLIER_info.GETID();
            FRM.ADD_OR_UPDATE = "ADD";
            FRM.ShowDialog();
          
        }
        public void load()
        {
            bind();
        }

        #region search_o()
        public void search_o(string sql)
        {
            string sqlo = " ORDER BY A.VOID ASC";
            string v7 = bc.getOnlyString("SELECT SCOPE FROM SCOPE_OF_AUTHORIZATION WHERE USID='" + LOGIN.USID + "'");
            //string v7 = "Y";
            if (v7 == "Y")
            {

                dt = bc.getdt(sql + sqlo);

            }
            else if (v7 == "GROUP")
            {

                dt = bc.getdt(sql + @" AND B.MAKERID IN (SELECT EMID FROM USERINFO A WHERE USER_GROUP IN 
 (SELECT USER_GROUP FROM USERINFO WHERE USID='" + LOGIN.USID + "'))" + sqlo);
            }
            else
            {
                dt = bc.getdt(sql + " AND B.MAKERID='" + LOGIN.EMID + "'" + sqlo);

            }

        }
        #endregion
     
   

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
                dataGridView1.Columns[i].ReadOnly = true;
                i = i + 1;
               
            }

            dataGridView1.Columns["收款方式"].ReadOnly = true;
            dataGridView1.Columns["供应商代码"].ReadOnly = true;
            dataGridView1.Columns["项次"].ReadOnly = true;
            dataGridView1.Columns["联系人"].ReadOnly = true;
            dataGridView1.Columns["联系电话"].ReadOnly = true;
            dataGridView1.Columns["传真号码"].ReadOnly = true;
            dataGridView1.Columns["邮政编码"].ReadOnly = true;
            dataGridView1.Columns["EMAIL"].ReadOnly = true;
            dataGridView1.Columns["公司地址"].ReadOnly = true;
            dataGridView1.Columns["默认联系人"].ReadOnly = true;
            dataGridView1.Columns["供应商名称"].ReadOnly = true;
            dataGridView1.Columns["项次"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
           

            if (GET_DATA_INT != 0)
            {
                if (GET_DATA_INT  == 1)
                {
                    int intCurrentRowNumber = this.dataGridView1.CurrentCell.RowIndex;
                    string s1 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[0].Value.ToString().Trim();
                    string s2 = this.dataGridView1.Rows[intCurrentRowNumber].Cells[2].Value.ToString().Trim();
                    /*CSPSS.VOUCHER_MANAGE.VOUCHERT.IF_DOUBLE_CLICK = true;
                    CSPSS.VOUCHER_MANAGE.VOUCHERT.SUID = s1;
                    CSPSS.VOUCHER_MANAGE.VOUCHERT .SNAME  = s2;*;*/
        

                    this.Close();
                }

            }
            else
            {
                SUPPLIER_INFOT FRM = new SUPPLIER_INFOT(this);
                FRM.IDO = dt.Rows[dataGridView1.CurrentCell.RowIndex]["供应商编号"].ToString();
                FRM.ADD_OR_UPDATE = "UPDATE";
                FRM.Show();
            }
        }

        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {

                bc.dgvtoExcel(dataGridView1, "供应商信息");

            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
   
    }
}
