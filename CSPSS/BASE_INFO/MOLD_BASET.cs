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
    public partial class MOLD_BASET : Form
    {
        DataTable dt = new DataTable();
        basec bc=new basec ();
        CFileInfo cfileinfo = new CFileInfo();
        StringBuilder sqb = new StringBuilder();
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
        private string _MAID;
        public string MAID
        {
            set { _MAID = value; }
            get { return _MAID; }
        }
        private string _INITIAL_OR_OTHER;
        public string INITIAL_OR_OTHER
        {
            set { _INITIAL_OR_OTHER = value; }
            get { return _INITIAL_OR_OTHER; }
        }
        private string _CUID;
        public string CUID
        {
            set { _CUID = value; }
            get { return _CUID; }
        }
        private string _WEIGHT;
        public string WEIGHT
        {
            set { _WEIGHT = value; }
            get { return _WEIGHT; }
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
        protected int M_int_judge, i;
        protected int select;
        CMOLD_BASE cMOLD_BASE = new CMOLD_BASE();
        DataTable dt3 = new DataTable();
        public MOLD_BASET()
        {
            InitializeComponent();
        }

        private void MOLD_BASET_Load(object sender, EventArgs e)
        {

            this.Icon =  Resource1.xz_200X200;
            hint.Location = new Point(256, 136);
            hint.ForeColor = Color.Red;
            comboBox1.BackColor = CCOLOR.YELLOW;
            textBox2.BackColor = CCOLOR.YELLOW;
            comboBox2.BackColor = CCOLOR.YELLOW;
            hint.Text = "";
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
            bind2();
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
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
        public void ClearText()
        {
            comboBox1.Text = "";
            textBox1.Text = "";
            comboBox2.Text = "";
            textBox2.Text = "";
        }

        #region bind
        private void bind()
        {
       
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
     
         
            /*
            textBox1.BackColor = CCOLOR.YELLOW;
            textBox2.BackColor = CCOLOR.YELLOW;*/
            if (bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS) != "")
            {

                hint.Text = bc.GET_IFExecutionSUCCESS_HINT_INFO(IFExecution_SUCCESS);
            }
            else
            {
                hint.Text = "";
            }
            StringBuilder sqb = new StringBuilder();
            sqb.Append(cMOLD_BASE. sql);
            sqb.Append(" WHERE B.CNAME LIKE '%" + comboBox1.Text + "%' ");
            sqb.Append(" AND A.WAREID LIKE '%" + textBox1.Text + "%'");
            sqb.Append(" AND C.MATERIAL LIKE '%" + comboBox2.Text  + "%'");
            sqb.Append(" AND A.WEIGHT LIKE '%" + textBox2 .Text  + "%'");
            dt = bc.getdt(sqb.ToString());
            dataGridView1.DataSource = dt;
            bind2();
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

                btnSave.Focus();
                string year = DateTime.Now.ToString("yy");
                string month = DateTime.Now.ToString("MM");
                string day = DateTime.Now.ToString("dd");
                string varDate = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss");
                cMOLD_BASE.MBID = IDO;
                cMOLD_BASE.CNAME = comboBox1.Text;
                cMOLD_BASE.WAREID = textBox1.Text;
                cMOLD_BASE.MATERIAL = comboBox2.Text;
                cMOLD_BASE.WEIGHT = textBox2.Text;
                cMOLD_BASE.EMID = LOGIN.EMID;
                cMOLD_BASE.save();
                if (cMOLD_BASE.IFExecution_SUCCESS == true)
                {
                    IFExecution_SUCCESS = true;
                    add();
                    bind();
                }
                else
                {
                    hint.Text = cMOLD_BASE.ErrowInfo;
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
            IDO = cMOLD_BASE.GETID();
            bind();
        }
               
        private bool juage()
        {
            bool b = false;
            if (IDO == "")
            {
                hint.Text = "编号不能为空";
                b = true;
            }
            else if (comboBox1.Text == "")
            {
                hint.Text = "客户名称不能为空";
                b = true;
            }
            else if (!bc.exists("SELECT * FROM CUSTOMERINFO_MST WHERE CNAME='"+comboBox1 .Text +"'"))
            {
                hint.Text = "系统不存在该客户名称";
                b = true;
            }
            /* else if (textBox1 .Text  == "")
             {
                 hint.Text = "型号不能为空";
                 b = true;
             }*/
            else if (comboBox2.Text == "")
            {
                hint.Text = "材料不能为空";
                b = true;
            }
            else if (comboBox2 .Text !="" && !bc.exists("SELECT * FROM MATERIAL WHERE MATERIAL='" + comboBox2.Text + "'"))
            {
                hint.Text = "系统不存在该材料";
                b = true;
            }
            else if (textBox2 .Text  == "")
            {
                hint.Text = "重量不能为空";
                b = true;
            }
            else if (textBox2 .Text !="" &&  bc.yesno (textBox2 .Text ) ==0)
            {
                hint.Text = "重量只能输入数字";
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
                    basec.getcoms("DELETE MOLD_BASE WHERE MBID='" + IDO + "'");
                    add();
                    bind(); 
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
            dataGridView1.ReadOnly = true;
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
            dataGridView2.AllowUserToAddRows = false;
            if (dataGridView1.Rows.Count > 0)
            {

                dataGridView2.RowHeadersDefaultCellStyle.BackColor = Color.Lavender;
                int numCols2 = dataGridView2.Columns.Count;
                dataGridView2.Columns["复选框"].Width = 50;
                dataGridView2.Columns["文件名"].Width = 130;
                dataGridView2.Columns["索引"].Width = 130;

                for (i = 0; i < numCols2; i++)
                {

                    dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    //this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.EnableHeadersVisualStyles = false;
                    dataGridView2.Columns[i].HeaderCell.Style.BackColor = Color.Lavender;
                }
                for (i = 0; i < dataGridView2.Columns.Count; i++)
                {
                    dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView2.Columns[i].DefaultCellStyle.BackColor = Color.OldLace;
                    i = i + 1;
                }
                dataGridView2.Columns["文件名"].ReadOnly = true;
                dataGridView2.Columns["索引"].ReadOnly = true;
            }
       
       
       
         
        }
        #endregion


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

   

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
     
            //dgvfoucs();

        }

        private void 删除此项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
    
       
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

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

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
      
            BASE_INFO.CUSTOMER_INFO FRM = new CSPSS.BASE_INFO.CUSTOMER_INFO();
            FRM.SELECT = 1;
            FRM.ShowDialog();
            this.comboBox1.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.comboBox1.DroppedDown = false;//使组合框不显示其下拉部分
            this.comboBox1.IntegralHeight = true;//恢复默认值
            if (FRM.IF_DOUBLE_CLICK)
            {
                comboBox1.Text = FRM.CNAME;
                textBox1.Focus();
            }
          
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            BASE_INFO.MATERIAL FRM = new BASE_INFO.MATERIAL();
            FRM.ORDER_USE();
            FRM.ShowDialog();
            if (FRM.MATERIAL_VALUE != "")
            {
              comboBox2 .Text = FRM.MATERIAL_VALUE;
              textBox2.Focus();
            }
            this.comboBox2.IntegralHeight = false;//使组合框不调整大小以显示其所有项
            this.comboBox2.DroppedDown = false;//使组合框不显示其下拉部分
            this.comboBox2.IntegralHeight = true;//恢复默认值
           
         
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bind();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string v1 = Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value).Trim();
            IDO = v1;
            if (v1 != "")
            {
                comboBox1 .Text  = Convert.ToString(dataGridView1["客户名称", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                textBox1.Text = Convert.ToString(dataGridView1["型号", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                comboBox2.Text = Convert.ToString(dataGridView1["材料", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                textBox2.Text = Convert.ToString(dataGridView1["重量", dataGridView1.CurrentCell.RowIndex].Value).Trim();
                bind2();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {

                bc.dgvtoExcel(dataGridView1, this.Text);
            }
            else
            {
                MessageBox.Show("没有数据可导出！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnupload_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dty = bc.getdt("SELECT * FROM WAREFILE WHERE WAREID='" + textBox1.Text + "'");
                if (juage())
                {

                }
                else if (dty.Rows.Count.ToString() == "6")
                {

                    hint.Text = "最多只能上传三张图片";
                }
                else
                {
                    uploadfile();
                }

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
                    }
                    else
                    {
                        label53.Visible = true;
                        label55.Visible = true;
                        label56.Visible = true;
                        label57.Visible = true;
                        progressBar1.Visible = true;
                        //上传的是非图片文件
                        INITIAL_OR_OTHER = "INITIAL";
                        i = Upload_Request("http://" + bc.RETURN_SERVER_IP_OR_DOMAIN() + "/webuploadfile/default.aspx", openf.FileName,
                                                      "INITIAL" + NeWAREID + System.IO.Path.GetFileName(openf.FileName), progressBar1, textBox1.Text);
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

            dgvStateControl();
        }
        #endregion
    

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
    }
}
