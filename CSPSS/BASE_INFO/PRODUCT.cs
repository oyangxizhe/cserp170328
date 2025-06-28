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
using System.IO;
using System.Net;

namespace CSPSS.BASE_INFO
{
    public partial class PRODUCT : Form
    {
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt3 = new DataTable();
        CFileInfo cfileinfo = new CFileInfo();
        StringBuilder sqb = new StringBuilder();
        private string _IDO;
        public string IDO
        {
            set { _IDO = value; }
            get { return _IDO; }

        }
        private string _RETAIL_PRICE;
        public string RETAIL_PRICE
        {
            set { _RETAIL_PRICE = value; }
            get { return _RETAIL_PRICE; }
        }
        private int _SELECT;
        public int SELECT
        {
            set { _SELECT = value; }
            get { return _SELECT; }
        }
        private string _INITIAL_OR_OTHER;
        public string INITIAL_OR_OTHER
        {
            set { _INITIAL_OR_OTHER = value; }
            get { return _INITIAL_OR_OTHER; }
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
        private string _WATER_MARK_CONTENT;
        public string WATER_MARK_CONTENT
        {
            set { _WATER_MARK_CONTENT = value; }
            get { return _WATER_MARK_CONTENT; }
        }
        private string _OLD_FILE_NAME;
        public string OLD_FILE_NAME
        {
            set { _OLD_FILE_NAME = value; }
            get { return _OLD_FILE_NAME; }

        }
        private string _NEW_FILE_NAME;
        public string NEW_FILE_NAME
        {
            set { _NEW_FILE_NAME = value; }
            get { return _NEW_FILE_NAME; }

        }
        public string _WAREID;
        public string WAREID
        {
            set { _WAREID = value; }
            get { return _WAREID; }

        }

        private string _CO_WAREID;
        public string CO_WAREID
        {
            set { _CO_WAREID = value; }
            get { return _CO_WAREID; }
        }
        private string _WNAME;
        public string WNAME
        {
            set { _WNAME = value; }
            get { return _WNAME; }

        }
        private string _MODEL;
        public string MODEL
        {
            set { _MODEL = value; }
            get { return _MODEL; }
        }
        private string _BUYING_PRICE;
        public string BUYING_PRICE
        {
            set { _BUYING_PRICE = value; }
            get { return _BUYING_PRICE; }
        }
        private string _PRODUCT_TYPE;
        public string PRODUCT_TYPE
        {
            set { _PRODUCT_TYPE = value; }
            get { return _PRODUCT_TYPE; }
        }
        private string _SNAME;
        public string SNAME
        {
            set { _SNAME = value; }
            get { return _SNAME; }
        }
        basec bc = new basec();
        CWARE_INFO cware_info = new CWARE_INFO();

        protected int M_int_judge, i;
        protected int select;
        public PRODUCT()
        {
            InitializeComponent();
        }
        #region double_click
        private void dgvEmployeeInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.Enabled == true)
            {
                int indexNumber = dataGridView1.CurrentCell.RowIndex;
                string id = dataGridView1.Rows[indexNumber].Cells[0].Value.ToString().Trim();
                string sendEName = dataGridView1.Rows[indexNumber].Cells[1].Value.ToString().Trim();
                string sendDepart = dataGridView1.Rows[indexNumber].Cells[2].Value.ToString().Trim();
                string[] inputarry = new string[] { sendEName, sendDepart,id};
                if (select == 0)
                {
                    //CSPSS.SellManage.FrmOrders.inputgetOEName[0] = inputarry[0]; 
                }
                if (select == 1)
                {
                  
                }
                if (select == 2)
                {

                }
                if (select == 3)
                {
                  
                }
                if (select == 4)
                {

                }
                if (select == 5)
                {

                }
                if (select == 6)
                {
               
                }
                if (select == 7)
                {
                   
                }
                if (select == 8)
                {

                }
                if (select == 9)
                {
                  
                }
                if (select == 10)
                {
                 
                }
                if (select == 11)
                {
                  
                }
                if (select == 12)
                {
               
                }
                if (select == 13)
                {

                }
                if (select == 14)
                {

                }
                if (select == 15)
                {
           
                }
                if (select == 16)
                {
                  
                
                }
                if (select == 17)
                {
                  


                }
                if (select == 18)
                {

                 
                }
                if (select == 19)
                {
                   
                }
                this.Close();
            }

        }
        #endregion
        #region only read
        public void dgvReadOnlyOrders()
        {
            dataGridView1.Enabled = true;
            select = 0;

        }
        public void dgvReadOnlyStock()
        {
            dataGridView1.Enabled = true;
            select = 1;
        }
        public void SellTable()
        {
            dataGridView1.Enabled = true;
            select = 2;
        }
        public void dgvReadOnlyPur()
        {
            dataGridView1.Enabled = true;
            select = 3;
        }
        public void GodE()
        {
            dataGridView1.Enabled = true;
            select = 4;
        }
        public void dgvReadOnlyOrdersT()
        {
            dataGridView1.Enabled = true;
            select = 5;

        }
        public void dgvReadOnlyStockT()
        {
            dataGridView1.Enabled = true;
            select = 6;
        }
        public void dgvReadOnlyPurT()
        {
            dataGridView1.Enabled = true;
            select = 7;
        }
        public void SellTableT()
        {
            dataGridView1.Enabled = true;
            select = 8;
        }
        public void ReturnT()
        {
            dataGridView1.Enabled = true;
            select = 9;
        }
        public void Return()
        {
            dataGridView1.Enabled = true;
            select = 10;
        }
        public void SellReT()
        {
            dataGridView1.Enabled = true;
            select = 11;
        }
        public void SellRe()
        {
            dataGridView1.Enabled = true;
            select = 12;
        }
        public void MateReT()
        {
            dataGridView1.Enabled = true;
            select = 13;
        }
        public void MateRe()
        {
            dataGridView1.Enabled = true;
            select = 14;
        }
        public void GodET()
        {
            dataGridView1.Enabled = true;
            select = 15;
        }
        public void USER_INFO_USE()
        {
            dataGridView1.Enabled = true;
            select = 16;
        }
        public void a1()
        {
            dataGridView1.Enabled = true;
            select = 17;

        }
        public void a2()
        {
            dataGridView1.Enabled = true;
            select = 18;

        }
        public void a3()
        {
            dataGridView1.Enabled = true;
            select = 19;

        }
  
        #endregion
        private void PRODUCT_Load(object sender, EventArgs e)
        {
            WAREID = "";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox3.BackColor = CCOLOR.YELLOW;
            this.WindowState = FormWindowState.Maximized;
            DataGridViewCheckBoxColumn dgvc1 = new DataGridViewCheckBoxColumn();
            dgvc1.Name = "复选框";
            dataGridView2.Columns.Add(dgvc1);
            DataGridViewTextBoxColumn dgvc2 = new DataGridViewTextBoxColumn();
            dgvc2.Name = "文件名";
            dataGridView2.Columns.Add(dgvc2);
            DataGridViewImageColumn dgvc3 = new DataGridViewImageColumn();
            dgvc3.Name = "缩略图";
            dataGridView2.Columns.Add(dgvc3);
            DataGridViewTextBoxColumn dgvc4 = new DataGridViewTextBoxColumn();
            dgvc4.Name = "索引";
            dgvc4.Visible = false;
            dataGridView2.Columns.Add(dgvc4);
            DataGridViewTextBoxColumn dgvc5 = new DataGridViewTextBoxColumn();
            dgvc5.Name = "新文件名";
            dgvc5.Visible = false;
            dataGridView2.Columns.Add(dgvc5);
            label52.Text = "";
            label53.Visible = false;
            label55.Visible = false;
            label56.Visible = false;
            label57.Visible = false;
            progressBar1.Visible = false;
            Bind();
            if (LOGIN.DEPART == "业务")//业务不能看进货价
            {
                label20.Visible = false;
                textBox8.Visible = false;
            }
            else
            {
                label20.Visible = true;
                textBox8.Visible = true;
            }

        }
        private void Bind()
        {
         
       
            textBox1.Text = IDO;
            StringBuilder sqb = new StringBuilder();

            if (LOGIN.DEPART == "业务")//业务不能看进货价
            {
                sqb.Append(cware_info.sqlo);
            }
            else
            {
                sqb.Append(cware_info.sql);
            }
        
            sqb.Append(" WHERE DateDiff(day,A.DATE,getdate()) >-1 and DateDiff(day,A.DATE,getdate()) <+7");
            sqb.Append(" AND SUBSTRING(WAREID,1,1)='9'");
           
            if (PRODUCT_TYPE != null && PRODUCT_TYPE !="")
            {
                sqb.AppendFormat(" AND PRODUCT_TYPE='{0}'",PRODUCT_TYPE );
            }
            if (SNAME != null && SNAME  != "")
            {
                sqb.AppendFormat(" AND B.UNAME='{0}'", SNAME );
            }
           string v7 = bc.getOnlyString("SELECT SCOPE FROM SCOPE_OF_AUTHORIZATION WHERE USID='" + LOGIN.USID + "'");
      
            if (v7 == "Y")
            {
             
                dt = bc.getdt(sqb.ToString () + " ORDER BY A.WAREID ASC");
               

            }
            else
            {
                dt = bc.getdt(sqb.ToString ()+ " AND A.MAKERID='" + LOGIN.USID + "'");

            }




            
            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            textBox2.Focus();
            textBox2.BackColor = Color.Yellow;
       
           
            hint.Location = new Point(400,100);
            hint.ForeColor = Color.Red;
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(cware_info.IFExecution_SUCCESS) != "")
            {
               
                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(cware_info.IFExecution_SUCCESS);
                
            }
            else
            {
               
                hint.Text = "";
            }
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.DataSource = bc.RETURN_ADD_EMPTY_COLUMN("TYPE", "TYPE");
            comboBox1.DisplayMember = "TYPE";

            label12.Text = "物料编号";
            label14.Text = "品名";
            groupBox1.Text = "产品信息";
            label1.Text = "物料编号";
           
            this.Text = "产品信息";
            this.Icon =  Resource1.xz_200X200;
          
            comboBox1.BackColor = CCOLOR.YELLOW;
            textBox2.BackColor = CCOLOR.YELLOW;
            comboBox1.BackColor = CCOLOR.YELLOW;
          
    
            try
            {
                bind2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region dgvStateControl
        private void dgvStateControl()
        {
            int i;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;/*自动调整DATAGRIDVIEW的列宽*/
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
            int numCols1 = dataGridView1.Columns.Count;
            for (i = 0; i < numCols1; i++)
            {
                dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
              
            
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
    
       
        #region juage()
        private bool juage()
        {

          
            bool b = false;
            if (textBox1.Text == "")
            {
                b = true;

                hint.Text = "ID不能为空！";
             
            }
            else if (textBox2.Text == "")
            {
                b = true;

                hint.Text = "品名不能为空！";
            }
            else if (textBox3.Text == "")
            {
                b = true;

                hint.Text = "型号不能为空！";
            }
            return b;

        }
        #endregion
        public void ClearText()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            comboBox1.Text = "";
            comboBox1.Text = "";
         
        
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            add();
        }
        private void add()
        {

            IDO  = cware_info.GETID("9");
            textBox1.Text = IDO;
            ClearText();
            textBox2.Focus();
            bind2();

        }
      

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (juage())
                {

                }
                else
                {
                    save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region save
        private void save()
        {

            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss");

            cware_info.WAREID = textBox1.Text;
            cware_info.CO_WAREID = textBox6.Text;
            cware_info.MAKERID = LOGIN.USID;
            cware_info.WNAME = textBox2.Text;
            cware_info.PRODUCT_TYPE = comboBox1.Text;
            cware_info.MODEL = textBox3.Text;
            cware_info.SIMPLE_CODE = textBox7.Text;
            cware_info.BUYING_PRICE = textBox8.Text;
            cware_info.TRADE_PRICE = textBox9.Text;
            cware_info.RETAIL_PRICE = textBox10.Text;
            cware_info.save();
            if (cware_info.IFExecution_SUCCESS)
            {
                add();
                Bind();
            }
            else
            {
        
                hint.Text = cware_info.ErrowInfo;
            }
           
        }
        #endregion
        private void btnSearch_Click(object sender, EventArgs e)
        {

            StringBuilder sqb = new StringBuilder();

            if (LOGIN.DEPART == "业务")//业务不能看进货价
            {
                
                sqb.Append(cware_info.sqlo);
            }
            else
            {
                sqb.Append(cware_info.sql);
            }
            sqb.Append(" WHERE A.WAREID LIKE '%" + textBox4.Text +
                "%' AND A.WNAME LIKE '%" + textBox5.Text + "%'");
            sqb.Append(" AND SUBSTRING(WAREID,1,1)='9'");
            if (PRODUCT_TYPE != null && PRODUCT_TYPE != "")
            {
                sqb.AppendFormat(" AND PRODUCT_TYPE='{0}'", PRODUCT_TYPE);
            }
            if (SNAME != null && SNAME != "")
            {
                sqb.AppendFormat(" AND B.UNAME='{0}'", SNAME);
            }
            string v7 = bc.getOnlyString("SELECT SCOPE FROM SCOPE_OF_AUTHORIZATION WHERE USID='" + LOGIN.USID + "'");
            if (v7 == "Y")
            {


            }
            else
            {
                sqb.Append(" AND A.MAKERID='"+LOGIN .USID +"'");

            }
            dt = basec.getdts(sqb.ToString());
        
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
            try
            {


              
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string id = Convert.ToString(dataGridView1["物料编号", dataGridView1.CurrentCell.RowIndex].Value).Trim();
            if (bc.exists ("SELECT * FROM GODE WHERE WAREID='"+id  +"'"))
            {
                hint.Text = "此型号在入库单中存在不允许删除";
                IFExecution_SUCCESS = false;
            }
            else if (bc.exists("SELECT * FROM ORDER_DET WHERE WAREID='" + id + "'"))
            {
                hint.Text = "此型号在订单中存在不允许删除";
                IFExecution_SUCCESS = false;
            }
            else
            {
                string strSql = "DELETE FROM WAREINFO WHERE WAREID='" + id + "'";
                basec.getcoms(strSql);
                IFExecution_SUCCESS = true;
                Bind();
                ClearText();
            }
            try
            {
            
            }
            catch (Exception)
            {


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

                dataGridView1.Focus();

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
        public void MISC_STORAGE_USE()
        {
            dataGridView1.Enabled = true;
            SELECT = 1;

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        


            hint.Text = "";

            if (SELECT != 0 || select != 0)
            {
                int indexNumber = dataGridView1.CurrentCell.RowIndex;
                WAREID = dt.Rows[dataGridView1.CurrentCell.RowIndex]["物料编号"].ToString();
             CO_WAREID = dt.Rows[dataGridView1.CurrentCell.RowIndex]["产品编号"].ToString();
                WNAME = dt.Rows[dataGridView1.CurrentCell.RowIndex]["品名"].ToString();
                MODEL = dt.Rows[dataGridView1.CurrentCell.RowIndex]["型号"].ToString();
                if (LOGIN.DEPART == "业务")
                {
                }
                else
                {
                    BUYING_PRICE = dt.Rows[dataGridView1.CurrentCell.RowIndex]["进货价"].ToString();
                }
            
                RETAIL_PRICE = dt.Rows[dataGridView1.CurrentCell.RowIndex]["零售价"].ToString();
                if (SELECT  == 1)
                {
                   
                }

            
                this.Close();

            }
            else
            {
                string v1 = Convert.ToString(dataGridView1["物料编号", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                IDO = v1;
                if (v1 != "")
                {
                    textBox1.Text = Convert.ToString(dataGridView1["物料编号", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    comboBox1.Text = Convert.ToString(dataGridView1["产品分类", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    textBox2.Text = Convert.ToString(dataGridView1["品名", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    textBox3.Text = Convert.ToString(dataGridView1["型号", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    textBox6.Text = Convert.ToString(dataGridView1["产品编号", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    textBox7.Text = Convert.ToString(dataGridView1["搜索简码", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    if (LOGIN.DEPART == "业务")//业务不能看进货价
                    {

                    }
                    else
                    {
                        textBox8.Text = Convert.ToString(dataGridView1["进货价", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    }

                    textBox9.Text = Convert.ToString(dataGridView1["批发价", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    textBox10.Text = Convert.ToString(dataGridView1["零售价", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                    bind2();
                }

            }


          
        }

        private void btnupload_Click(object sender, EventArgs e)
        {
         
            DataTable dty = bc.getdt("SELECT * FROM WAREFILE WHERE WAREID='" + textBox1.Text + "'");
            if (juage())
            {

            }
            else if (dty.Rows.Count.ToString() == "2")
            {

                hint.Text = "最多只能上传一张图片";
            }
            else
            {
                
                uploadfile();
            }
            try
            {
            

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }
        #region uploadfile
        private void uploadfile()
        {
            int i = 0;
            label53.Visible = false;
            label55.Visible = false;
            label56.Visible = false;
            label57.Visible = false;
            progressBar1.Visible = false;
            /*  string v2 = bc.getOnlyString("SELECT EDIT FROM RIGHTLIST WHERE USID='" + LOGIN.USID + "' AND NODE_NAME='传单作业'");
              if (v2 != "Y" && ADD_OR_UPDATE == "UPDATE")
              {
                  hint.Text = "您没有修改权限不能修改上传";
              }
              else*/
            label52.Text = "";
            if (bc.RETURN_SERVER_IP_OR_DOMAIN() == "")
            {
                hint.Text = "未设置服务器IP或域名";
            }

            else
            {
                OpenFileDialog openf = new OpenFileDialog();
                if (openf.ShowDialog() == DialogResult.OK)
                {
                  
                    Random ro = new Random();
                    string stro = ro.Next(80, 10000000).ToString() + "-";
                    string NeWAREID = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + stro;

                    cfileinfo.SERVER_IP_OR_DOMAIN = bc.RETURN_SERVER_IP_OR_DOMAIN();
                    WATER_MARK_CONTENT = "";//水印内容
                    //cfileinfo.UploadImage(openf.FileName, Path.GetFileName(openf.FileName), textBox1 .Text );
                    //this.UploadFile(openf.FileName, System.IO.Path.GetFileName(openf.FileName), "File/", textBox1.Text);

                    string v21 = bc.FROM_RIGHT_UNTIL_CHAR(Path.GetFileName(openf.FileName), 46);
                    OLD_FILE_NAME = Path.GetFileName(openf.FileName);
                    NEW_FILE_NAME = NeWAREID + Path.GetFileName(openf.FileName);
                    //如果上传的是图片文件
                    if (v21 == "jpeg" || v21 == "jpg" || v21 == "JPG" || v21 == "png" || v21 == "bmp" || v21 == "gif")
                    {
                       
                       
                        //裁切小图
                        cfileinfo.MakeThumbnail(openf.FileName, "d:\\" + Path.GetFileName(openf.FileName), 80, 80, "Cut");
                        //小图加水印
                        cfileinfo.ADD_WATER_MARK("d:\\" + Path.GetFileName(openf.FileName), "d:\\80X80" + NeWAREID + Path.GetFileName(openf.FileName), WATER_MARK_CONTENT);
                        //原图加水印
                        cfileinfo.ADD_WATER_MARK(openf.FileName, "d:\\INITIAL" + NeWAREID + Path.GetFileName(openf.FileName), WATER_MARK_CONTENT);
                        INITIAL_OR_OTHER = "INITIAL";
                   
                        //上传原图
                        i = Upload_Request("http://" + bc.RETURN_SERVER_IP_OR_DOMAIN() + "/webuploadfile/default.aspx", "D:\\INITIAL" + NeWAREID + System.IO.Path.GetFileName(openf.FileName),
                                "INITIAL" + NeWAREID + System.IO.Path.GetFileName(openf.FileName), progressBar1, textBox1.Text);
                    
                        //上传80X80的缩略图
                        INITIAL_OR_OTHER = "80X80";
                        i = Upload_Request("http://" + bc.RETURN_SERVER_IP_OR_DOMAIN() + "/webuploadfile/default.aspx", "D:\\80X80" + NeWAREID + System.IO.Path.GetFileName(openf.FileName),
                                "80X80" + NeWAREID + System.IO.Path.GetFileName(openf.FileName), progressBar1, textBox1.Text);

                   
                        //删除本地临时水印图及剪切图
                        if (File.Exists("d:\\80X80" + NeWAREID + Path.GetFileName(openf.FileName)))
                        {
                            File.Delete("d:\\80X80" + NeWAREID + Path.GetFileName(openf.FileName));
                            File.Delete("d:\\" + Path.GetFileName(openf.FileName));
                            File.Delete("d:\\INITIAL" + NeWAREID + Path.GetFileName(openf.FileName));
                        }
                        if (i == 1)
                        {
                            label52.Text = "成功上传";
                        }
                        else
                        {
                            label52.Text = "上传失败";
                        }
                       
                        bind2();
                    }
                    else
                    {
                        MessageBox.Show("只能上传图片格式为jpeg/jpg/png/bmp/gif", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        /*label53.Visible = true;
                        label55.Visible = true;
                        label56.Visible = true;
                        label57.Visible = true;
                        progressBar1.Visible = true;
                        //上传的是非图片文件
                        INITIAL_OR_OTHER = "INITIAL";
                        i = Upload_Request("http://" + bc.RETURN_SERVER_IP_OR_DOMAIN() + "/webuploadfile/default.aspx", openf.FileName,
                                                      "INITIAL" + NeWAREID + System.IO.Path.GetFileName(openf.FileName), progressBar1, textBox1.Text);*/
                    }
                 
                }
            }

        }
        #endregion
        #region HttpWebRequst_uploadfile
        /// <summary>
        /// 将本地文件上传到指定的服务器(HttpWebRequest方法)
        /// </summary>
        /// <param name="address">文件上传到的服务器</param>
        /// <param name="fileNamePath">要上传的本地文件（全路径）</param>
        /// <param name="saveName">文件上传后的名称</param>
        /// <param name="progressBar">上传进度条</param>
        /// <returns>成功返回1，失败返回0</returns>
        /// 
        #region Upload_Request
        public int Upload_Request(string address, string fileNamePath, string saveName, ProgressBar progressBar, string WAREID)
        {
            int returnValue = 0;
            // 要上传的文件

            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            //时间戳
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");
            //请求头部信息
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(strBoundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append(saveName);
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append("application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");
            string strPostHeader = sb.ToString();


            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);
            // 根据uri创建HttpWebRequest对象
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));
            httpReq.Method = "POST";
            //对发送的数据不使用缓存
            httpReq.AllowWriteStreamBuffering = false;
            //设置获得响应的超时时间（300秒）
            httpReq.Timeout = 300000;
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
            long fileLength = fs.Length;
            httpReq.ContentLength = length;
            if (fileLength / 1048576.0 > 2.5)
            {

                label52.Visible = false;
                label53.Visible = false;
                label55.Visible = false;
                label56.Visible = false;
                label57.Visible = false;
                progressBar1.Visible = false;
                MessageBox.Show("上传的图片长度为:" + (fileLength / 1048576.0).ToString("F2") + "M" + " 已经大于允许上传的2.5M");
            }
            else
            {
                try
                {
                    progressBar.Maximum = int.MaxValue;
                    progressBar.Minimum = 0;
                    progressBar.Value = 0;
                    //每次上传4k
                    int bufferLength = 4096;
                    byte[] buffer = new byte[bufferLength];
                    //已上传的字节数
                    long offset = 0;
                    //开始上传时间
                    DateTime startTime = DateTime.Now;
                    int size = r.Read(buffer, 0, bufferLength);

                    Stream postStream = httpReq.GetRequestStream();
                    //发送请求头部消息
                    postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                    while (size > 0)
                    {
                        postStream.Write(buffer, 0, size);
                        offset += size;
                        progressBar.Value = (int)(offset * (int.MaxValue / length));
                        TimeSpan span = DateTime.Now - startTime;
                        double second = span.TotalSeconds;
                        label53.Text = "已用时：" + second.ToString("F2") + "秒";

                        if (second > 0.001)
                        {
                            label55.Text = "平均速度：" + (offset / 1024 / second).ToString("0.00") + "KB/秒";
                        }
                        else
                        {
                            label55.Text = "正在连接…";
                        }
                        label56.Text = "已上传：" + (offset * 100.0 / length).ToString("F2") + "%";
                        label57.Text = (offset / 1048576.0).ToString("F2") + "M/" + (fileLength / 1048576.0).ToString("F2") + "M";
                        Application.DoEvents();
                        size = r.Read(buffer, 0, bufferLength);
                    }
                    //添加尾部的时间戳
                    postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    postStream.Close();

                    string year = DateTime.Now.ToString("yy");
                    string month = DateTime.Now.ToString("MM");
                    string day = DateTime.Now.ToString("dd");
                    string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    string v1 = bc.numYMD(20, 12, "000000000001", "SELECT * FROM WAREFILE", "FLKEY", "FL");
                    string newFileName, uriString;
                    newFileName = System.IO.Path.GetFileName(saveName);
                    uriString = "http://" + bc.RETURN_SERVER_IP_OR_DOMAIN() + "/uploadfile/" + newFileName;


                    String sql = @"
INSERT INTO  WAREFILE 
(
FLKEY,
WAREID,
OLD_FILE_NAME,
NEW_FILE_NAME,
PATH,
INITIAL_OR_OTHER,
DATE,
YEAR,
MONTH,
DAY
) 
VALUES
(
@FLKEY,
@WAREID,
@OLD_FILE_NAME,
@NEW_FILE_NAME,
@PATH,
@INITIAL_OR_OTHER,
@DATE,
@YEAR,
@MONTH,
@DAY

)";
                    SqlConnection sqlcon = bc.getcon();
                    SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
                    sqlcom.Parameters.Add("@FLKEY", SqlDbType.VarChar, 20).Value = v1;
                    sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = IDO;
                    sqlcom.Parameters.Add("@OLD_FILE_NAME", SqlDbType.VarChar, 100).Value = OLD_FILE_NAME;
                    sqlcom.Parameters.Add("@NEW_FILE_NAME", SqlDbType.VarChar, 100).Value = NEW_FILE_NAME;
                    sqlcom.Parameters.Add("@PATH", SqlDbType.VarChar, 100).Value = uriString;
                    sqlcom.Parameters.Add("@INITIAL_OR_OTHER", SqlDbType.VarChar, 100).Value = INITIAL_OR_OTHER;
                    sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                    sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
                    sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
                    sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlcon.Close();


                    //获取服务器端的响应
                    WebResponse webRespon = httpReq.GetResponse();
                    Stream s = webRespon.GetResponseStream();
                    StreamReader sr = new StreamReader(s);
                    //读取服务器端返回的消息
                    String sReturnString = sr.ReadLine();
                    s.Close();
                    sr.Close();
                    if (sReturnString == "Success")
                    {
                        returnValue = 1;
                    }
                    else if (sReturnString == "Error")
                    {
                        returnValue = 0;
                    }
                }
                catch
                {
                    returnValue = 0;
                }
                finally
                {
                    fs.Close();
                    r.Close();
                }
            }
            return returnValue;
        }
        #endregion
        #endregion
        #region bind2
        private void bind2()
        {

            dt3 = bc.getdt(@"
SELECT cast(0   as   bit)   as   复选框,
OLD_FILE_NAME AS 文件名,NEW_FILE_NAME AS 新文件名,FLKEY AS 索引,
PATH FROM WAREFILE WHERE WAREID='" + IDO + "'  AND INITIAL_OR_OTHER='80X80'");


            dataGridView2.Rows.Clear();//在下一次增加行前需清空上一次产生的行，否则显示行数不正常
            if (dt3.Rows.Count > 0)
            {
                for (int i = 0; i < dt3.Rows.Count; i++)
                {

                    DataGridViewRow dgr = new DataGridViewRow();
                    dataGridView2.Rows.Add(dgr);
                    dataGridView2["复选框", i].Value = false;
                    dataGridView2["文件名", i].Value = dt3.Rows[i]["文件名"].ToString();
                    dataGridView2["缩略图", i].Value = Image.FromStream(System.Net.WebRequest.Create(dt3.Rows[i]["PATH"].ToString()).GetResponse().GetResponseStream());
               
                    dataGridView2["索引", i].Value = dt3.Rows[i]["索引"].ToString();

                }
                for (i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    dataGridView2.Rows[i].Height = 80;
                }
                this.WindowState = FormWindowState.Maximized;
                Color c = System.Drawing.ColorTranslator.FromHtml("#efdaec");
            }
            dgvStateControl();
        }
        #endregion
    
        private void btndelfile_Click(object sender, EventArgs e)
        {
            try
            {
                /*string v21 = bc.getOnlyString("SELECT EDIT FROM RIGHTLIST WHERE USID='" + LOGIN.USID + "' AND NODE_NAME='传单作业'");
                if (v21 != "Y" && ADD_OR_UPDATE == "UPDATE")
                {
                    hint.Text = "您没有修改权限不能删除文件";
                }
                else if (vou.CheckIfALLOW_SAVEOR_DELETE(textBox1.Text, LOGIN.USID))
                {
                    hint.Text = vou.ErrowInfo;
                }
                else
                {
                

                }*/
                if (MessageBox.Show("确定要删除该文件吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    if (dt3.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            if (dataGridView2.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                            {

                                string v2 = dt3.Rows[i]["索引"].ToString();
                                string v4 = dt3.Rows[i]["新文件名"].ToString();
                                bc.getcom(@"INSERT INTO SERVER_DELETE_FILE(FLKEY,NEW_FILE_NAME) VALUES ('" + v2 + "','" + v4 + "')");
                                bc.getcom("DELETE WAREFILE WHERE NEW_FILE_NAME='" + v4 + "'");

                            }
                        }
                        bind2();

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = dataGridView2.CurrentCell.RowIndex;

                if (dataGridView2.CurrentCell.ColumnIndex == 1)
                {
                    SaveFileDialog sfl = new SaveFileDialog();
                    sfl.FileName = dt3.Rows[dataGridView2.CurrentCell.RowIndex]["文件名"].ToString();
                    sfl.DefaultExt = "jpg";
                    sfl.Filter = "(*.jpg)|*.jpg";
                    if (sfl.ShowDialog() == DialogResult.OK)
                    {
                        sqb = new StringBuilder();
                        sqb.AppendFormat("SELECT PATH FROM WAREFILE WHERE ");
                        sqb.AppendFormat(" NEW_FILE_NAME='{0}'", dt3.Rows[i]["新文件名"].ToString());
                        sqb.AppendFormat(" AND INITIAL_OR_OTHER='INITIAL'");
                        WebClient wclient = new WebClient();
                        string v1 = bc.getOnlyString(sqb.ToString());
                        wclient.DownloadFile(v1, sfl.FileName);

                        /*DataTable dt3x = bc.getdt("SELECT * FROM WAREFILE WHERE FLKEY='" + dt3.Rows[dataGridView1.CurrentCell.RowIndex]["索引"].ToString() + "'");
                        Byte[] byte2 = (byte[])dt3x.Rows[0]["IMAGE_DATA"];
                        System.IO.File.WriteAllBytes(sfl.FileName, byte2);*/
                        hint.Text = "已下载";
                    }
                }

            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }
        }

    }
}
