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
    public partial class CUSTOMER_INFO : Form
    {
        DataTable dt = new DataTable();
        basec bc=new basec ();
        private string _IDO;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

        }
        private static string _EMID;
        public static string EMID
        {
            set { _EMID = value; }
            get { return _EMID; }

        }
        private static string _ENAME;
        public static string ENAME
        {
            set { _ENAME = value; }
            get { return _ENAME; }

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
        private string _CNAME;
        public string CNAME
        {
            set { _CNAME = value; }
            get { return _CNAME; }
        }
        private int _SELECT;
        public int SELECT
        {
            set { _SELECT = value; }
            get { return _SELECT; }
        }
        private  bool _IF_DOUBLE_CLICK;
        public  bool IF_DOUBLE_CLICK
        {
            set { _IF_DOUBLE_CLICK = value; }
            get { return _IF_DOUBLE_CLICK; }

        }
        protected int M_int_judge, i;
        protected int select;
        CCUSTOMER_INFO ccustomer_info = new CCUSTOMER_INFO();
        public CUSTOMER_INFO()
        {
            InitializeComponent();
        }
        private void CUSTOMER_INFO_Load(object sender, EventArgs e)
        {
            try
            {
                CNAME = "";
                IF_DOUBLE_CLICK = false;
                this.Icon =  Resource1.xz_200X200;
                textBox6.Focus();
                bind();
            }
            catch (Exception)
            {

            }
        }
        public void ORDER_USE()
        {
            dataGridView1.ReadOnly = true;
            select = 1;
        }
        public void a2()
        {
            dataGridView1.ReadOnly = true;
            select = 2;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                bind();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #region bind
        private void bind()
        {
      
            StringBuilder sqb = new StringBuilder();
            sqb.Append(" WHERE B.PROVINCE LIKE '%" +comboBox2 .Text  + "%' ");
            sqb.Append(" AND B.CNAME LIKE '%" + textBox6.Text + "%'");
            dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.ContextMenuStrip = contextMenuStrip1;

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
            search_o(ccustomer_info.sqlsi + sqb.ToString ());
       
        }
        #endregion
        #region search_o()
        public void search_o(string sql)
        {
            string sqlo = " ORDER BY B.CNAME ASC";
            //string v7 = bc.getOnlyString("SELECT SCOPE FROM SCOPE_OF_AUTHORIZATION WHERE USID='" + LOGIN.USID + "'");
            string v7 = "Y";
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
            if (v7 == "Y")
            {
               // btnToExcel.Visible = true;
            
            }
            else
            {
                //btnToExcel.Visible = false;
              
            }
            //dt = vou.GET_CALCULATE(dt);
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
            IDO = ccustomer_info.GETID();
            CUSTOMER_INFOT FRM = new CUSTOMER_INFOT(this);
            FRM.IDO = ccustomer_info.GETID();
            FRM.ADD_OR_UPDATE = "ADD";
            FRM.Show();
          
        }
        public void load()
        {
            bind();
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
                dataGridView1.Columns[i].ReadOnly = true;
                i = i + 1;
               
            }
            dataGridView1.Columns["客户代码"].ReadOnly = true;
            dataGridView1.Columns["联系人"].ReadOnly = true;
            dataGridView1.Columns["联系电话"].ReadOnly = true;
            dataGridView1.Columns["传真"].ReadOnly = true;
            dataGridView1.Columns["EMAIL"].ReadOnly = true;
            dataGridView1.Columns["客户名称"].ReadOnly = true;
        }
        #endregion

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
               
                    CUSTOMER_INFOT FRM = new CUSTOMER_INFOT(this);
                    FRM.IDO = dt.Rows[dataGridView1.CurrentCell.RowIndex]["客户编号"].ToString();
                    FRM.ADD_OR_UPDATE = "UPDATE";
                    FRM.Show();
                
            }
            catch (Exception)
            {

            }
        }
    
        private void btnToExcel_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                bc.dgvtoExcel(dataGridView1, "客户信息");
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            if (select == 1 || SELECT == 1)
            {
                SELL_MANAGE.ORDERT.CUID = this.dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString().Trim();
                SELL_MANAGE.ORDERT.IF_DOUBLE_CLICK = true;
                IF_DOUBLE_CLICK = true;
                CNAME = dt.Rows[dataGridView1.CurrentCell.RowIndex]["客户名称"].ToString();
                this.Close();
            }   
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

   
    }
}
