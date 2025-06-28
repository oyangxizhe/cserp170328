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
using System.Net;
using System.Web;
using System.Xml;
using System.Collections;
using System.Data.OleDb;

namespace CSPSS.FINANCIAL_MANAGE
{
    public partial class VOUCHERT : Form
    {
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        private string _ACID;
        public string ACID
        {
            set { _ACID = value; }
            get { return _ACID; }

        }
        private string _ACCOUNTING_PERIOD_START_DATE;
        public string ACCOUNTING_PERIOD_START_DATE
        {
            set { _ACCOUNTING_PERIOD_START_DATE = value; }
            get { return _ACCOUNTING_PERIOD_START_DATE; }

        }
        private string _ACCOUNTING_PERIOD_EXPIRATION_DATE;
        public string ACCOUNTING_PERIOD_EXPIRATION_DATE
        {
            set { _ACCOUNTING_PERIOD_EXPIRATION_DATE = value; }
            get { return _ACCOUNTING_PERIOD_EXPIRATION_DATE; }

        }
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
        protected int i, j;
        protected int M_int_judge, t;
        basec bc = new basec();
        CVOUCHER vou = new CVOUCHER();
        ExcelToCSHARP etc = new ExcelToCSHARP();
        CFileInfo cfileinfo = new CFileInfo();
        //BaseInfo.FrmCurrency cur = new CSPSS.BASE_INFO.FrmCurrency();
        VOUCHER F1 = new VOUCHER();
        DataTable dt1 = new DataTable();
        Color c2 = System.Drawing.ColorTranslator.FromHtml("#990033");
        public VOUCHERT()
        {
            InitializeComponent();
        }
        public VOUCHERT(VOUCHER Frm)
        {
            InitializeComponent();
            F1 = Frm;
        }
        private void VOUCHERT_Load(object sender, EventArgs e)
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
             textBox1.Text = IDO;
             comboBox1.DataSource = bc.getdt("SELECT * FROM ACCOUNTANT_COURSE ORDER BY ACID ASC");
             comboBox1.DisplayMember = "ACCODE";
             comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            DataTable dtx = basec.getdts(vou.getsql +" where A.VOID='" + textBox1.Text + "' ORDER BY  A.VOKEY ASC ");
                if (dtx.Rows.Count > 0)
                {
                    dt = vou.GET_TABLEINFO(dtx,1);
                    comboBox1.Text = dt.Rows[0]["科目"].ToString();
                    if (dt.Rows.Count > 0 && dt.Rows.Count < 6)
                    {
                        int n = 6 - dt.Rows.Count;
                        for (int i = 0; i <n; i++)
                        {
                           
                            DataRow dr = dt.NewRow();
                            int b1 = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["项次"].ToString());
                            dr["项次"] = Convert.ToString(b1 + 1);
                            dr["日期"] = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
                            //dr["币别"] = dt.Rows[dt.Rows.Count - 1]["币别"].ToString();
                            //dr["汇率"] = decimal.Parse(dt.Rows[dt.Rows.Count - 1]["汇率"].ToString());
                            dt.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                  dt = total1();
                }
         dataGridView1.DataSource = dt;
         bind2();
        }
        #endregion
        #region bind2
        private void bind2()
        {
            dgvStateControl();
            this.WindowState = FormWindowState.Maximized;
            Color c = System.Drawing.ColorTranslator.FromHtml("#efdaec");
            t1.BackColor = c;
            t2.BackColor = c;
            IF_DOUBLE_CLICK = false;
        }
        #endregion
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;

            int numCols1 = dataGridView1.Columns.Count;
        
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;/*自动调整DATAGRIDVIEW的列宽*/
          
              dataGridView1.Columns["摘要"].HeaderText = "说明/Description";
         
              //dataGridView1.Columns["币别"].Width =40;
              //dataGridView1.Columns["汇率"].Width =60;
          
              dataGridView1.Columns["单价"].Visible = false;
              dataGridView1.Columns["数量"].Visible = false;
              dataGridView1.Columns["备注"].Visible = false;
              dataGridView1.Columns["科目"].Visible = false;
              dataGridView1.Columns["数量"].Width =60;
              dataGridView1.Columns["支出金额"].Width =70;
              dataGridView1.Columns["支出金额"].HeaderText = "支出/Outging";
              //dataGridView1.Columns["支出本币"].Width =80;
              dataGridView1.Columns["收入金额"].Width =70;
              dataGridView1.Columns["收入金额"].HeaderText = "收入/Recd";
              //dataGridView1.Columns["收入本币"].Width =80;


      
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
            dataGridView1.Columns["摘要"].DefaultCellStyle.BackColor = CCOLOR.YELLOW;
            dataGridView1.Columns["科目"].DefaultCellStyle.BackColor = CCOLOR.YELLOW;
            dataGridView1.Columns["支出金额"].DefaultCellStyle.BackColor = CCOLOR.YELLOW;
            dataGridView1.Columns["收入金额"].DefaultCellStyle.BackColor = CCOLOR.YELLOW;
            dataGridView1.Columns["项次"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["单价"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dataGridView1.Columns["支出金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dataGridView1.Columns["收入金额"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
        

            dataGridView1.Columns["项次"].ReadOnly = true;
            dataGridView1.Columns["摘要"].ReadOnly = false;
            dataGridView1.Columns["科目"].ReadOnly = false;
            //dataGridView1.Columns["币别"].ReadOnly = true;
            //dataGridView1.Columns["汇率"].ReadOnly = true;
            dataGridView1.Columns["单价"].ReadOnly = false;
            dataGridView1.Columns["数量"].ReadOnly = false;
            dataGridView1.Columns["支出金额"].ReadOnly = false;
            //dataGridView1.Columns["支出本币"].ReadOnly = true;
            dataGridView1.Columns["收入金额"].ReadOnly = false;
            //dataGridView1.Columns["收入本币"].ReadOnly = true;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
          
        }
        #endregion
     
        #region total1
        private DataTable total1()
        {
            DataTable dtt2 = vou.GetTableInfo();
            for (i = 1; i <= 6; i++)
            {
                DataRow dr = dtt2.NewRow();
                dr["项次"] = i;
                dr["日期"] = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
                //dr["币别"] ="RMB";
                //dr["汇率"] = "1";
                //dr["支出金额"] = "0";
                dtt2.Rows.Add(dr);
            }
            return dtt2;
        }
        #endregion
        #region override enter
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter &&(( !(ActiveControl is System.Windows.Forms.TextBox) ||
                !((System.Windows.Forms.TextBox)ActiveControl).AcceptsReturn) ))
            {
               
                if (dataGridView1.CurrentCell.ColumnIndex == 7 && 
                    dataGridView1["支出金额",dataGridView1.CurrentCell.RowIndex].Value .ToString ()!=null )
                {
                    
                    SendKeys.SendWait("{Tab}");
                    SendKeys.SendWait("{Tab}");
                }
                else if (dataGridView1.CurrentCell.ColumnIndex == 9 )
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

                double_info();
              
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
      
        #region juage()
        private bool juage()
        {
            bool b = false;
            for (int k = 0; k <dt.Rows .Count ; k++)
            {
                if (juage(k))
                {
                    b = true;
                    break;
                }
            }
            return b;
        }
        #endregion

        
        #region juage()
        private bool juage(int k)
        {
            bool b = false;
                string v7 = dt.Rows[k]["支出金额"].ToString();
                string v8 = dt.Rows[k]["收入金额"].ToString();
                if (v7 != "" && v8 != "")
                {
                    b = true;
                    //MessageBox.Show("支出金额与收入金额同行只能输入一方！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    hint.Text = "支出金额与收入金额同行只能输入一方！";
                }
            return b;
        }
        #endregion
        #region dgvDataSourceChanged
        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
           /* int i;
            for (i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Columns[i].ValueType.ToString() == "System.Decimal")
                {
                    
                    dataGridView1.Columns[i].DefaultCellStyle.Format = "#0.00";
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
                }
              
            }
            if (dataGridView1.Columns["汇率"].ValueType.ToString() == "System.Decimal")
            {
                dataGridView1.Columns["汇率"].DefaultCellStyle.Format = "#0.0000";
                dataGridView1.Columns["汇率"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }*/
        }
        #endregion
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("只能输入数字！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            try
            {
                hint.Text = "只能输入数字！";
            }
            catch (Exception)
            {


            }
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
        #region btnExcelPrint
        private void btnExcelPrint_Click(object sender, EventArgs e)
        {
           /* try
            {
                DataTable dtn = boperate.PrintOrder(" WHERE ORID='" + textBox1.Text + "'");
                if (dtn.Rows.Count > 0)
                {
                    string v1 = @"D:\PrintModelForOrder.xls";
                    if (File.Exists(v1))
                    {
                        boperate.ExcelPrint(dtn, "订单", v1);
                    }
                    else
                    {
                        MessageBox.Show("指定路径不存在打印模版！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    MessageBox.Show("无数据可打印！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }*/
        }
        #endregion
        private void ClearText()
        {
          
     
            t1.Text = "";
            t2.Text = "";

        }
        #region save
        private void btnSave_Click(object sender, EventArgs e)
        {
            save();
            try
            {
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
         
        }
        #endregion
        private void save()
        {

            btnSave.Focus();
            dgvfoucs();
            dt1 = bc.GET_NOEMPTY_ROW_COURSE_DT(dt);
            if (juage2())
            {


            }
            else if (dt1.Rows.Count > 0)
            {
                vou.VOUCHER_DATE = "";
                vou.EMID = "";
                vou.ACCOUNTING_PERIOD_EXPIRATION_DATE = DateTime.Now.ToString("yyyy/MM/dd");
                vou.MANAGE_AUDIT_STATUS = "N";
                vou.FINANCIAL_AUDIT_STATUS = "N";
                vou.GENERAL_MANAGE_AUDIT_STATUS = "N";
                vou.ACID = bc.getOnlyString("SELECT ACID FROM ACCOUNTANT_COURSE WHERE ACCODE='"+comboBox1 .Text +"'");
                vou.save("VOUCHER_MST", "VOUCHER_DET", "VOID", textBox1.Text, dt1);
                IFExecution_SUCCESS = true;
                bind();
                F1.Bind();
                F1.search();
            }
            else
            {
                hint.Text = "至少有一项收入或支出金额才能保存！";

            }
            try
            {
     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }


        }

        #region juage2()
        private bool juage2()
        {
            bool b = false;
            string v5 = dt.Compute("sum(支出金额)","").ToString();
            string v6 = dt.Compute("sum(收入金额)","").ToString();
            decimal d1 = 0, d2 = 0;
          
            if (!string.IsNullOrEmpty(v5))
            {
                d1 = decimal.Parse(v5);
            }
            if (!string.IsNullOrEmpty(v6))
            {
                d2 = decimal.Parse(v6);
            }
            if (juage())
            {
                b = true;
              
            }
           else if (juage_5())
           {
               b = true;

           }
            return b;
        }
        #endregion
 
        #region juage_5()
        private bool  juage_5()
        {
            bool b = false;
            DataTable dtx =vou.GetTableInfo();
            dt1 = bc.GET_NOEMPTY_ROW_COURSE_DT(dt);
            foreach (DataRow dr in dt1.Rows)
            {
                DataTable dtx1 = bc.GET_DT_TO_DV_TO_DT(dtx, "", string.Format("科目='{0}'", bc.REMOVE_NAME (dr["科目"].ToString())));
                if (dtx1.Rows.Count > 0)
                {
                  
                }
                else
                {
                    DataRow dr1 = dtx.NewRow();
                    dr1["科目"] = bc.REMOVE_NAME(dr["科目"].ToString());
                    dtx1 = bc.GET_DT_TO_DV_TO_DT(dt1, "", string.Format("科目='{0}'", dr["科目"].ToString()));
               
                    dtx.Rows.Add(dr1);

                }

            }
            StringBuilder sqb = new StringBuilder("SELECT SUM(A.DEBIT_ORIGINALAMOUNT) AS 累计支出金额,");
            sqb.AppendFormat("SUM(A.AMOUNT_PAYABLE) AS 累计应付金额,B.ACCODE AS 科目 FROM VOUCHER_DET A");
             sqb.AppendFormat(" LEFT JOIN ACCOUNTANT_COURSE B ON A.ACID=B.ACID");
            sqb.AppendFormat(" WHERE A.VOID!='"+textBox1 .Text +"' GROUP BY B.ACCODE ORDER BY B.ACCODE ASC");
            DataTable dtx2 = bc.getdt(sqb.ToString());
       
            foreach (DataRow dr1 in dtx.Rows)
            {
                if (b == true)
                {
                    break;
                }
              
                if (dtx2.Rows.Count > 0)
                {
              
             
                    foreach (DataRow dr2 in dtx2.Rows)
                    {

                        if (dr1["科目"].ToString() == dr2["科目"].ToString())
                        {
                            decimal d1 = 0, d2 = 0, d3 = 0, d4 = 0;

                     
                            if (!string.IsNullOrEmpty(dr1["支出金额"].ToString()))
                            {
                                d2 = decimal.Parse(dr1["支出金额"].ToString());
                            }

                       
                            if (!string.IsNullOrEmpty(dr2["累计支出金额"].ToString()))
                            {
                                d4 = decimal.Parse(dr2["累计支出金额"].ToString());
                            }
                         
                            dr1["支出金额"] = d2 + d4;
                            if (d1 + d3 == 0)
                            {
                                b = false;
                                break;
                            }
                            else if (d2 + d4 > d1 + d3)
                            {
                                StringBuilder sqb1 = new StringBuilder();
                                sqb1.AppendFormat("科目 {0} 的当前凭证累计支出金额 {1} + 该凭证除外该科目累计支出金额 {2}={3}", dr1["科目"].ToString(), d2, d4, d2 + d4);
                                sqb1.AppendFormat(" > 科目 {0} 的当前凭证累计应付金额 {1} + 该凭证除外该科目累计应付金额 {2}", dr1["科目"].ToString(), d1, d3);
                                sqb1.AppendFormat("={0}", d1 + d3);

                                MessageBox.Show(sqb1.ToString());
                                b = true;
                            }
                            break;
                        }
                    }
                

                }
                else
                {

          
                }
             
            }
          
     
            return b;

        }
        #endregion
        #region juage_ABSTRACT_NOEMPTY()
        private int juage_ABSTRACT_NOEMPTY()
        {
           
            int n = 0;
            for (int k = dt.Rows.Count - 1; k >= 0; k--)
            {

                if (dt.Rows[k]["支出金额"].ToString() != "" && dt.Rows[k]["收入金额"].ToString() == ""
                    || dt.Rows[k]["支出金额"].ToString() == "" && dt.Rows[k]["收入金额"].ToString() != "")
                {
                    n = k;
                    break;

                }
            }
            return n;

        }
        #endregion
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        private void btnDel_Click(object sender, EventArgs e)
        {
          
         
            try
            {
                if (vou.CheckIfALLOW_SAVEOR_DELETE(textBox1.Text,LOGIN .USID ))
                {
                    hint.Text = vou.ErrowInfo;
                }
                else if (MessageBox.Show("确定要删除该条凭证吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    basec.getcoms("DELETE VOUCHER_MST WHERE VOID='" + textBox1.Text + "'");
                    basec.getcoms("DELETE VOUCHER_DET WHERE VOID='" + textBox1.Text + "'");
                    bind();
                    ClearText();
                    textBox1.Text = "";
                    F1.Bind();
                    F1.search();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }
        #region dgvCellEndEdit
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
       

            try
            {

                int a = dataGridView1.CurrentCell.ColumnIndex;
                int b = dataGridView1.CurrentCell.RowIndex;
                int c = dataGridView1.Columns.Count - 1;
                int d = dataGridView1.Rows.Count - 1;
                if (a == 2)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[b]["科目"].ToString()))
                    {
                        dt2 = bc.getdt(etc.getsql + " WHERE A.ACCODE='" + dt.Rows[b]["科目"].ToString() + "'");
                        if (dt2.Rows.Count > 0)
                        {
                            string v1 = bc.getOnlyString("SELECT COURSE_NATURE FROM ACCOUNTANT_COURSE WHERE ACCODE='" + dt.Rows[b]["科目"].ToString() + "'");
                            dt.Rows[b]["科目"] = dt.Rows[b]["科目"].ToString() +
                                " " + etc.GetLastCourseAnd_CurrentCourseName(dt.Rows[b]["科目"].ToString()) + " " + v1;

                            if (b != 0)
                            {
                                if (dt.Rows[b]["摘要"].ToString() == "" && dt.Rows[b - 1]["摘要"].ToString() != "")
                                {

                                    dt.Rows[b]["摘要"] = dt.Rows[b - 1]["摘要"].ToString();
                                }
                                if (dt.Rows[b]["支出金额"].ToString() == "" && dt.Rows[b]["收入金额"].ToString() == "" && dt.Rows[b - 1]["支出金额"].ToString() != "")
                                {

                                    dt.Rows[b]["支出金额"] = dt.Rows[b - 1]["支出金额"].ToString();
                                }
                                else if (dt.Rows[b]["支出金额"].ToString() == "" && dt.Rows[b]["收入金额"].ToString() == "" && dt.Rows[b - 1]["收入金额"].ToString() != "")
                                {

                                    dt.Rows[b]["收入金额"] = dt.Rows[b - 1]["收入金额"].ToString();
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }
        #endregion
        #region dgvDoubleClick
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            /*IF_DOUBLE_CLICK = false;
            try
            {
                int currentrowsindex = dataGridView1.CurrentCell.RowIndex;
                int currentcolumnindex = dataGridView1.CurrentCell.ColumnIndex;
                if (currentcolumnindex == 1)
                {
                  
                   
                    if (IF_DOUBLE_CLICK)
                    {
                        //dataGridView1["摘要", currentrowsindex].Value = frm.ABCODE;
                        dataGridView1.CurrentCell = dataGridView1["科目", dataGridView1.CurrentCell.RowIndex];
                        IF_DOUBLE_CLICK = false;
                    }
                }
                if (currentcolumnindex == 2)
                {

                    CSPSS.BASE_INFO.ACCOUNTANT_COURSE frm = new CSPSS.BASE_INFO.ACCOUNTANT_COURSE();
                    frm.a5();
                    frm.SELECT = 1;
                    frm.ShowDialog();
                    if (IF_DOUBLE_CLICK)
                    {
                        dataGridView1["科目", currentrowsindex].Value = frm.ACCODE;
                        //dataGridView1.CurrentCell = dataGridView1["单价", dataGridView1.CurrentCell.RowIndex];
                    }
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }*/

        }
        #endregion
        private void double_info()
        {

            CSPSS.BASE_INFO.ACCOUNTANT_COURSE frm = new CSPSS.BASE_INFO.ACCOUNTANT_COURSE();
            frm.a5();
            frm.ShowDialog();
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            int j = dataGridView1.CurrentCell.ColumnIndex;
            if (dataGridView1.Columns[j].Name == "科目")
            {
                dgvr.Cells["科目"].Value = frm.ACCODE;
                //dataGridView1.CurrentCell = dataGridView1["币别", dataGridView1.CurrentCell.RowIndex];
            } 
        }

        #region dgvCellEnter
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
          
            try
            {
                int a = dataGridView1.CurrentCell.ColumnIndex;
                int b = dataGridView1.CurrentCell.RowIndex;
                int c = 4;//因为有些列是隐藏的
                int d = dataGridView1.Rows.Count - 1;

          
                if (a == c && b == d)
                {
                   
                    if (dt.Rows.Count >= 6)
                    {

                        DataRow dr = dt.NewRow();
                        int b1 = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["项次"].ToString());
                        dr["项次"] = Convert.ToString(b1 + 1);
                        dr["日期"] = DateTime.Now.ToString("yyyy/MM/dd").Replace("-", "/");
                        //dr["币别"] = dt.Rows[dt.Rows.Count - 1]["币别"].ToString();
                        //dr["汇率"] = decimal.Parse(dt.Rows[dt.Rows.Count - 1]["汇率"].ToString());
                        dt.Rows.Add(dr);
                    }

                }
                dgvfoucs();
           
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }

        }
        #endregion
        #region ask
        private void ask(int k)
        {
            int n = k;
            //decimal v1 = decimal.Parse(dt.Rows[k]["汇率"].ToString());
            decimal v2=0, v3=0;
            if (!string.IsNullOrEmpty(dt.Rows[k]["支出金额"].ToString()))
            {
                v2 = decimal.Parse(dt.Rows[k]["支出金额"].ToString());
            }
            if (!string.IsNullOrEmpty(dt.Rows[k]["收入金额"].ToString()))
            {
                v3 = decimal.Parse(dt.Rows[k]["收入金额"].ToString());
            }
         
      
            ask1();
        }
        #endregion
        #region ask1
        private void ask1()
        {
            t1.Text = "";
            t2.Text = "";
         
            string v5 = dt.Compute("sum(支出金额)", "").ToString();
            string v6 = dt.Compute("sum(收入金额)", "").ToString();
            //string v7 = dt.Compute("sum(支出本币)", "").ToString();
            //string v8 = dt.Compute("sum(收入本币)", "").ToString();
            if (!string.IsNullOrEmpty(v5))
            {
                t1.Text = string.Format("{0:F2}", Convert.ToDouble(v5));
            
            }
            /*if (!string.IsNullOrEmpty(v7))
            {
                
                t3.Text = string.Format("{0:F2}", Convert.ToDouble(v7));
            }*/
            if (!string.IsNullOrEmpty(v6))
            {
                t2.Text = string.Format("{0:F2}", Convert.ToDouble(v6));
             
            }
            /*if (!string.IsNullOrEmpty(v8))
            {
                t4.Text = string.Format("{0:F2}", Convert.ToDouble(v8));
            }*/
        }
        #endregion
        #region dgvCellValidating
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
      
            try
            {
             
                /*else if (e.ColumnIndex == 3 && bc.CheckKeyInValueIfNoExistsOrEmpty("CURRENCY_MST", "CYCODE", e.FormattedValue.ToString(), "币别"))
                {

                    e.Cancel = true;
                }*/
                /*else if (e.ColumnIndex == 4 && bc.CheckKeyInValueIfNoDigitOrEmpty(e.FormattedValue.ToString(), "汇率"))
                {

                    e.Cancel = true;
                }*/
                if (e.ColumnIndex == 2 && bc.yesno(e.FormattedValue.ToString()) == 0)
                {
                    e.Cancel = true;
                    //MessageBox.Show("单价只能输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    hint.Text = "单价只能输入数字！";


                }
                else if (e.ColumnIndex == 4 && bc.yesno(e.FormattedValue.ToString()) == 0)
                {
                    e.Cancel = true;

                    hint.Text = "数量只能输入数字！";


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }

        }
        #endregion
        private void dgvfoucs()
        {
            
            for (i = 0; i < dt.Rows .Count ; i++)
            {
                ask(i);
            }
        }
        private void TSMI_Click(object sender, EventArgs e)
        {
            dgvclear(dataGridView1.CurrentCell.RowIndex);
            
        }
        private void dgvclear(int r)
        {
            
            dt.Rows[r]["摘要"] = "";
            dt.Rows[r]["科目"] = null;
            //dt.Rows[r]["币别"] = "";

            //dt.Rows[r]["汇率"] = DBNull.Value;
            dt.Rows[r]["单价"] = "";
            dt.Rows[r]["数量"] = "";
            dt.Rows[r]["支出金额"] = DBNull.Value;
            //dt.Rows[r]["支出本币"] = DBNull.Value;
            dt.Rows[r]["收入金额"] = DBNull.Value;
            //dt.Rows[r]["收入本币"] = DBNull.Value;
            btnSave.Focus();
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
          
            if (vou.CheckIfALLOW_SAVEOR_DELETE (textBox1 .Text,LOGIN .USID  ))
            {
                hint.Text = vou.ErrowInfo;
            }
            else
            {
                dgvclear(dataGridView1.CurrentCell.RowIndex);
            }
        }

        private void btnAllSelect_Click(object sender, EventArgs e)
        {
            if (vou.CheckIfALLOW_SAVEOR_DELETE (textBox1 .Text,LOGIN .USID  ))
            {
                hint.Text = vou.ErrowInfo;
            }
            else 
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dgvclear(i);
                }
            }
        }

        private void dataGridView1_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {

            try
            {
                int r = dataGridView1.CurrentCell.RowIndex;
                if (dataGridView1["支出金额", r].Value.ToString() != "" && dataGridView1["收入金额", r].Value.ToString() != "")
                {
                    e.Cancel = true;
                    hint.Text = "支出金额与收入金额同行只能输入一方！";

                }
            }
            catch (Exception)
            {

            }
        }

        private void 提取科目F7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double_info();
            
        }
        #region lkmange_audit
        private void lkmange_audit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
         
            try
            {

                if (vou.RETURN_GENERAL_AUDIT_STATUS(textBox1.Text) == "Y")
                {
                    if (vou.RETURN_MANAGE_AUDIT_STATUS(textBox1.Text) == "N")
                    {

                        basec.getcoms("UPDATE VOUCHER_MST SET MANAGE_AUDIT_STATUS='Y',MANAGE_AUDIT_DATE='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE VOID='" + textBox1.Text + "'");
                        bind();
                        F1.Bind();
                        F1.search();
                    }
                    else
                    {
                        //hint.Text = "状态为开立或经理已审核才能操作审核与撤审核";
                        hint.Text = "状态为总经理已审核不能操作撤审核";
                    }

                }
                else if (vou.RETURN_FINANCIAL_AUDIT_STATUS(textBox1.Text) == "Y")
                {
                    if (vou.RETURN_MANAGE_AUDIT_STATUS(textBox1.Text) == "N")
                    {

                        basec.getcoms("UPDATE VOUCHER_MST SET MANAGE_AUDIT_STATUS='Y',MANAGE_AUDIT_DATE='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE VOID='" + textBox1.Text + "'");
                        bind();
                        F1.Bind();
                        F1.search();
                    }
                    else
                    {
                        //hint.Text = "状态为开立或经理已审核才能操作审核与撤审核";
                        hint.Text = "状态为财务已审核不能操作撤审核";
                    }

                }
                else if (vou.RETURN_MANAGE_AUDIT_STATUS(textBox1.Text) == "N")
                {
                    basec.getcoms("UPDATE VOUCHER_MST SET MANAGE_AUDIT_STATUS='Y',MANAGE_AUDIT_DATE='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE VOID='" + textBox1.Text + "'");
                    bind();
                    F1.Bind();
                    F1.search();

                }
                else
                {

                    basec.getcoms("UPDATE VOUCHER_MST SET MANAGE_AUDIT_STATUS='N',MANAGE_AUDIT_DATE='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE VOID='" + textBox1.Text + "'");
                    bind();
                    F1.Bind();
                    F1.search();

                }
         

            }
            catch (Exception)
            {

            }
        }
        #endregion
        #region lkfinancial_audit
        private void lkfinancial_audit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string s2 = bc.getOnlyString("SELECT FINANCIAL_AUDIT_STATUS FROM VOUCHER_MST WHERE VOID='" + textBox1.Text + "'");
                if (vou.RETURN_GENERAL_AUDIT_STATUS(textBox1.Text) == "Y")
                {
                    if (vou.RETURN_FINANCIAL_AUDIT_STATUS(textBox1.Text) == "N")
                    {
                        basec.getcoms("UPDATE VOUCHER_MST SET FINANCIAL_AUDIT_STATUS='Y',FINANCIAL_AUDIT_DATE='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE VOID='" + textBox1.Text + "'");
                        bind();
                        F1.Bind();
                        F1.search();
                    }
                    else
                    {

                        hint.Text = "状态为总经理已审核不能操作撤审核";
                    }
                }
                else if (vou.RETURN_FINANCIAL_AUDIT_STATUS(textBox1.Text) == "N")
                {

                    basec.getcoms("UPDATE VOUCHER_MST SET FINANCIAL_AUDIT_STATUS='Y',FINANCIAL_AUDIT_DATE='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE VOID='" + textBox1.Text + "'");
                    bind();
                    F1.Bind();
                    F1.search();
                }
                else
                {
                    basec.getcoms("UPDATE VOUCHER_MST SET FINANCIAL_AUDIT_STATUS='N',FINANCIAL_AUDIT_DATE='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE VOID='" + textBox1.Text + "'");
                    bind();
                    F1.Bind();
                    F1.search();

                }
            }
            catch (Exception)
            {


            }
        }
        #endregion
        #region lkgeneral_manage
        private void lkgeneral_manage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (vou.RETURN_GENERAL_AUDIT_STATUS(textBox1.Text) == "N")
                {
                    basec.getcoms("UPDATE VOUCHER_MST SET GENERAL_MANAGE_AUDIT_STATUS='Y',GENERAL_MANAGE_AUDIT_DATE='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE VOID='" + textBox1.Text + "'");
                    bind();
                    F1.Bind();
                    F1.search();
                }
                else
                {

                    basec.getcoms("UPDATE VOUCHER_MST SET GENERAL_MANAGE_AUDIT_STATUS='N',GENERAL_MANAGE_AUDIT_DATE='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' WHERE VOID='" + textBox1.Text + "'");
                    bind();
                    F1.Bind();
                    F1.search();

                }
            }
            catch (Exception)
            {


            }
          
        }
        #endregion 
        #region btnupload
        private void btnupload_Click(object sender, EventArgs e)
        {
            string v2 = bc.getOnlyString("SELECT EDIT FROM RIGHTLIST WHERE USID='" + LOGIN.USID + "' AND NODE_NAME='录入凭证作业'");
            if (v2 != "Y" && ADD_OR_UPDATE == "UPDATE")
            {
                hint.Text = "您没有修改权限不能修改上传";
            }
            else if (!bc.exists("SELECT * FROM UPLOADFILE_DOMAIN"))
            {
                hint.Text = "未设置服务器IP或域名";
            }
            else
            {
                OpenFileDialog openf = new OpenFileDialog();
                if (openf.ShowDialog() == DialogResult.OK)
                {
                    cfileinfo.SERVER_IP_OR_DOMAIN = bc.getOnlyString("SELECT UPLOADFILE_DOMAIN FROM UPLOADFILE_DOMAIN");
                    cfileinfo.UploadFile(openf.FileName, System.IO.Path.GetFileName(openf.FileName), "File/", textBox1.Text);
                    //cfileinfo.UploadImage(openf.FileName, Path.GetFileName(openf.FileName), textBox1 .Text );
                    cfileinfo.UploadFile(openf.FileName, System.IO.Path.GetFileName(openf.FileName), "File/", textBox1.Text);
                    bind2();
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
        #endregion
     
        #region btndelfile
        private void btndelfile_Click(object sender, EventArgs e)
        {
          
        }
        #endregion
     
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearText();
            IFExecution_SUCCESS = false;
            IDO = vou.GETID();
          
            bind();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                bind();
                F1.Bind();
                F1.search();
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            juage_5();
        }

    }
}
