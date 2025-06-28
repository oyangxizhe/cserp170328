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
using System.Xml;
using System.IO;
namespace CSPSS
{
    public partial class LOADING : Form
    {
        private bool _IFExecutionSUCCESS;
        public bool IFExecution_SUCCESS
        {
            set { _IFExecutionSUCCESS = value; }
            get { return _IFExecutionSUCCESS; }

        }
        public LOADING()
        {
            InitializeComponent();
        }
        basec bc = new basec();
        private void LOADING_Load(object sender, EventArgs e)
        {
            /*this.Icon =  Resource1.xz_200X200;
            PictureBox pic = new PictureBox();
            pic.Image = Image.FromFile(System.IO.Path.GetFullPath("Image/loading.GIF"));
            pic.Size = new Size(32,32);
            pic.Location = new Point((this.Width-pic.Size.Width ) / 2, (this.Height-pic.Size .Height ) / 2);
            this.Controls.Add(pic);*/
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ControlBox = false;
            createXml();
       
        }
            private static void createXml()
        {
            XmlTextWriter writer = new XmlTextWriter("titles.xml", null);
            //使用自动缩进便于阅读
            writer.Formatting = Formatting.Indented;
            //写入根元素
            writer.WriteStartElement("items");
            writer.WriteStartElement("item");
            //写入属性及属性的名字
            writer.WriteAttributeString("用户信息", "用户1");
            //加入子元素
            writer.WriteAttributeString("UNAME", "U1");
            writer.WriteAttributeString("PWD", "P1");
            writer.WriteAttributeString("IF_RECORD", "Y");
 
            //关闭根元素，并书写结束标签
            writer.WriteEndElement();
            //将XML写入文件并且关闭XmlTextWriter
            writer.Close();
        }

        private static void readtext()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("titles.xml");
            XmlNode xn = xmlDoc.SelectSingleNode("items");//找出批配值的第一个节点
            foreach (XmlNode xnf in xn.ChildNodes )
            {
                XmlElement xe = (XmlElement)xnf;
                MessageBox.Show(xe.GetAttribute("用户信息"));//显示属性值
                MessageBox.Show(xe.GetAttribute("UNAME"));//显示属性值
                MessageBox.Show(xe.GetAttribute("PWD"));//显示属性值
                MessageBox.Show(xnf.InnerText);
                XmlNodeList xnf1 = xe.ChildNodes;
                foreach (XmlNode xn2 in xnf1)
                {
                    XmlElement xe1 = (XmlElement)xn2;
                    MessageBox.Show(xe1.GetAttribute("UNAME"));//显示子节点点文本
                }
            }
        }
    

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
             
            }
            catch (Exception)
            {


            }
        }

        #region ProcessCmdKey
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter &&
             (
             (
              !(ActiveControl is System.Windows.Forms.TextBox) ||
              !((System.Windows.Forms.TextBox)ActiveControl).AcceptsReturn)
             )
             )
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

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = bc.getdt("SELECT * FROM USERINFO");
            dt.TableName = "USERINFO";
            dt.WriteXml("USERINFO.xml");
            //dataGridView1.DataSource = CXmlFileToDataSet(AppDomain .CurrentDomain .BaseDirectory+"ti.xml").Tables[0];
            dataGridView1.DataSource = basec.XML_TO_DT("USERINFO.xml");
        }
        
    }
}
