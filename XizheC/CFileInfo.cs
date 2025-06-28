using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web;
using System.Net;


namespace XizheC
{
   
    public class CFileInfo
    {
        #region nature
        private string _FileName;
        public string FileName
        {
            set { _FileName = value; }
            get { return _FileName; }

        }
        private string _ALREADY_UPLOADED;
        public string ALREADY_UPLOADED
        {
            set { _ALREADY_UPLOADED = value; }
            get { return _ALREADY_UPLOADED; }

        }
        private string _dir;
        public string dir
        {
            set { _dir = value; }
            get { return _dir; }

        }
        private string _LastFileUpdateTime;
        public string LastFileUpdateTime
        {

            set { _LastFileUpdateTime = value; }
            get { return _LastFileUpdateTime; }


        }
        private string _FileNameAndPath;
        public string FileNameAndPath
        {
            set { _FileNameAndPath = value; }
            get { return _FileNameAndPath; }

        }
        private string _Path;
        public string Path
        {

            set { _Path = value; }
            get { return _Path; }
        }
        private string _ErrowInfo;
        public string ErrowInfo
        {

            set { _ErrowInfo = value; }
            get { return _ErrowInfo; }

        }
        private string _PROGRESS;
        public string PROGRESS
        {
            set { _PROGRESS = value; }
            get { return _PROGRESS; }

        }
        private int _MaxFileSize;
        public int MaxFileSize
        {
            set { _MaxFileSize = value; }
            get { return _MaxFileSize; }

        }
        private string _TIME;
        public string TIME
        {
            set { _TIME = value; }
            get { return _TIME; }

        }
        private string _AVERAGE_SPEED;
        public string AVERAGE_SPEED
        {
            set { _AVERAGE_SPEED = value; }
            get { return _AVERAGE_SPEED; }

        }
        private string _SERVER_IP_OR_DOMAIN;
        public string SERVER_IP_OR_DOMAIN
        {
            set { _SERVER_IP_OR_DOMAIN = value; }
            get { return _SERVER_IP_OR_DOMAIN; }

        }
        private string _INITIAL_OR_OTHER;
        public string INITIAL_OR_OTHER
        {
            set { _INITIAL_OR_OTHER = value; }
            get { return _INITIAL_OR_OTHER; }
        }
        private string _FLKEY;
        public string FLKEY
        {
            set { _FLKEY = value; }
            get { return _FLKEY; }

        }
        #endregion
        basec bc = new basec();
        public CFileInfo()
        {
            _MaxFileSize = 20971520;
        }
        #region CFileInfo
        public CFileInfo(int j)
        {
            if (j > 0)
            {
                _MaxFileSize = j;

            }
            else
            {

                _MaxFileSize = 20971520;
            }


        }
        #endregion
        public CFileInfo(string DIR)
        {
            dir = DIR;
        }
  
     
        #region FindFile
        public List<CFileInfo> FindFile(string dir)
        {
            //在指定目录及子目录下查找文件,在listBox1中列出子目录及文件
            List<CFileInfo> list1 = new List<CFileInfo>();
            DirectoryInfo Dir = new DirectoryInfo(dir);
            try
            {

                foreach (DirectoryInfo d in Dir.GetDirectories())
                {
                    //查找子目录  
                    FindFile(Dir + d.ToString() + "\\");
                    //listBox1.Items.Add(Dir + d.ToString() + "\\");
                    //MessageBox.Show(Dir +d.ToString ());

                }
                //listBox1中填加目录名}    
                foreach (FileInfo f in Dir.GetFiles("*.*"))
                {  //查找文件
                    CFileInfo cfileinfo = new CFileInfo();
                    cfileinfo.FileName = f.ToString();
                    cfileinfo.Path = Dir.ToString();
                    cfileinfo.FileNameAndPath = Dir + f.ToString();
                    cfileinfo.LastFileUpdateTime = File.GetLastWriteTime(Dir + f.ToString()).ToString();
                    list1.Add(cfileinfo);
                    //MessageBox.Show(cfileinfo .FileNameAndPath );
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return list1;
        }
        #endregion
        #region FindFile
        public List<CFileInfo> FindFile()
        {
            //在指定目录及子目录下查找文件,在listBox1中列出子目录及文件
            List<CFileInfo> list1 = new List<CFileInfo>();
            DirectoryInfo Dir = new DirectoryInfo(dir);
            try
            {

                foreach (DirectoryInfo d in Dir.GetDirectories())
                {
                    //查找子目录  
                    FindFile(Dir + d.ToString() + "\\");
                    //listBox1.Items.Add(Dir + d.ToString() + "\\");
                    //MessageBox.Show(Dir +d.ToString ());

                }
                //listBox1中填加目录名}    
                foreach (FileInfo f in Dir.GetFiles("*.*"))
                {  //查找文件
                    CFileInfo cfileinfo = new CFileInfo();
                    cfileinfo.FileName = f.ToString();
                    cfileinfo.Path = Dir.ToString();
                    cfileinfo.FileNameAndPath = Dir + f.ToString();
                    cfileinfo.LastFileUpdateTime = File.GetLastWriteTime(Dir + f.ToString()).ToString();
                    list1.Add(cfileinfo);
                    //MessageBox.Show(cfileinfo .FileNameAndPath );
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return list1;
        }
        #endregion
        #region CExists
        public bool CExists(string clientPath, string serverfilename)
        {
            bool a1 = true;

            if (File.Exists(clientPath + serverfilename) == true)
            {

            }
            else
            {
                a1 = false;

            }
            return a1;

        }
        #endregion
        #region GetTheLastUpdateTime
        public DateTime GetTheLastUpdateTime(string Dir)
        {
            //获取客户端应用程序及服务器端升级程序的最近一次更新日期
            DateTime LastUpdateTime = Convert.ToDateTime("2016/01/01 0:00");
            string AutoUpdaterFileName = Dir; ;
            if (!File.Exists(AutoUpdaterFileName))
                return LastUpdateTime;
            //打开xml文件  
            FileStream myFile = new FileStream(AutoUpdaterFileName, FileMode.Open);
            //xml文件阅读器  
            XmlTextReader xml = new XmlTextReader(myFile);
            while (xml.Read())
            {
                if (xml.Name == "UpdateTime")
                {  //获取升级文档的最后一次更新日期 
                    string v1 = Convert.ToDateTime(xml.GetAttribute("Date")).ToString("yyyy/MM/dd HH:mm").Replace("-", "/");
                    LastUpdateTime = Convert.ToDateTime(v1);
                    break;
                }
            }
            xml.Close();
            myFile.Close();
            return LastUpdateTime;
        }
        #endregion
        #region GetTheLastUpdateVersion
        public string GetTheLastUpdateVersion(string Dir)
        {
            //获取客户端应用程序及服务器端升级程序的最近一次更新版本
            string LastUpdateVersion = "";
            string AutoUpdaterFileName = Dir;
            if (!File.Exists(AutoUpdaterFileName))
                return LastUpdateVersion;
            //打开xml文件  
            FileStream myFile = new FileStream(AutoUpdaterFileName, FileMode.Open);
            //xml文件阅读器  
            XmlTextReader xml = new XmlTextReader(myFile);
            while (xml.Read())
            {
                if (xml.Name == "Version")
                {  //获取升级文档的最后一次更新版本
                    LastUpdateVersion = xml.GetAttribute("Num");
                    break;
                }
            }
            xml.Close();
            myFile.Close();
            return LastUpdateVersion;
        }
        #endregion
        #region importExcelToDataSet
        public DataSet importExcelToDataSet(string FilePath, string tablename)
        {
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);

            OleDbDataAdapter myCommand = new OleDbDataAdapter("SELECT * FROM [" + tablename + "] ", strConn);
            DataSet myDataSet = new DataSet();
            try
            {
                myCommand.Fill(myDataSet);
            }
            catch (Exception ex)
            {
                MessageBox.Show("error," + ex.Message);
            }
            return myDataSet;
        }
        #endregion
        #region GetExcelFirstTableName
        public static string GetExcelFirstTableName(string excelFileName)
        {
            string tableName = null;
            if (File.Exists(excelFileName))
            {
                using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet." +
                  "OLEDB.4.0;Extended Properties=\"Excel 8.0\";Data Source=" + excelFileName))
                {
                    conn.Open();
                    DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    tableName = dt.Rows[0][2].ToString().Trim();

                }
            }
            return tableName;
        }
        #endregion
        #region EXCEL_TO_DT
        public DataTable  EXCEL_TO_DT(string FilePath)
        {
            DataTable dt = new DataTable();
        
            DataSet ds = importExcelToDataSet(FilePath,CFileInfo . GetExcelFirstTableName (FilePath ));
            dt = ds.Tables[0];
            return dt;
        }
        #endregion
        #region OnloadFile /*CS*/
        public void UploadFile(string localFilePath, string OLD_FILE_NAME, string serverFolder, string WAREID)/*CS UPONLOAD*/
        {

            try
            {
                string year = DateTime.Now.ToString("yy");
                string month = DateTime.Now.ToString("MM");
                string day = DateTime.Now.ToString("dd");
                string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                WebClient myWebClient = new WebClient();
                string newFileName, uriString;
                newFileName = System.IO.Path.GetFileName(localFilePath);
                uriString = "http://" + SERVER_IP_OR_DOMAIN + "/uploadfile/" + newFileName;
                myWebClient.UploadFile(uriString, "PUT", localFilePath);
                string v1 = bc.numYMD(20, 12, "000000000001", "SELECT * FROM WAREFILE", "FLKEY", "FL");
                basec.getcoms(@"INSERT INTO WAREFILE(FLKEY,WAREID,OLD_FILE_NAME,PATH,INITIAL_OR_OTHER,DATE,YEAR,MONTH,DAY) VALUES 
('" + v1 + "','" + WAREID + "','" + System.IO.Path.GetFileName(OLD_FILE_NAME) +
    "','" + uriString + "','" + INITIAL_OR_OTHER + "','" + varDate + "','" + year + "','" + month + "','" + day + "')");
                //IFExecution_SUCCESS = true;
                //hint.Text = "上传成功";
                FLKEY = v1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }


        }
        #endregion
        #region OnloadImage /* CS UPONLOAD_IMAGE */
        public void UploadImage(string fullpath, string OLD_FILE_NAME, string WAREID)/*CS UPONLOAD_IMAGE*/
        {
            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            string varDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string v2 = bc.FROM_RIGHT_UNTIL_CHAR(OLD_FILE_NAME, 46);
            try
            {

                if (v2 != "jpeg" && v2 != "jpg" && v2 != "png" && v2 != "bmp" && v2 != "gif")
                {
                    MessageBox.Show("上传的文件需为图片格式 JPEG JPE PNG BMP GIF");
                }
                else
                {
                    FileStream filestream = new FileStream(fullpath, FileMode.Open);
                    BinaryReader binaryreader = new BinaryReader(filestream);
                    Byte[] bytes = binaryreader.ReadBytes((int)filestream.Length);
                    string v1 = bc.numYMD(20, 12, "000000000001", "SELECT * FROM WAREFILE", "FLKEY", "FL");
                    String sql = @"
INSERT INTO  WAREFILE 
(
FLKEY,
WAREID,
OLD_FILE_NAME,
PATH,
IMAGE_DATA,
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
@PATH,
@IMAGE_DATA,
@DATE,
@YEAR,
@MONTH,
@DAY

)";
                    SqlConnection sqlcon = bc.getcon();
                    SqlCommand sqlcom = new SqlCommand(sql, sqlcon);
                    sqlcom.Parameters.Add("@FLKEY", SqlDbType.VarChar, 20).Value = v1;
                    sqlcom.Parameters.Add("@WAREID", SqlDbType.VarChar, 20).Value = WAREID;
                    sqlcom.Parameters.Add("@OLD_FILE_NAME", SqlDbType.VarChar, 100).Value = OLD_FILE_NAME;
                    sqlcom.Parameters.Add("@PATH", SqlDbType.VarChar, 100).Value = fullpath;
                    sqlcom.Parameters.Add("@IMAGE_DATA", SqlDbType.Image, (int)filestream.Length).Value = bytes;
                    sqlcom.Parameters.Add("@DATE", SqlDbType.VarChar, 20).Value = varDate;
                    sqlcom.Parameters.Add("@YEAR", SqlDbType.VarChar, 20).Value = year;
                    sqlcom.Parameters.Add("@MONTH", SqlDbType.VarChar, 20).Value = month;
                    sqlcom.Parameters.Add("@DAY", SqlDbType.VarChar, 20).Value = day;
                    sqlcon.Open();
                    sqlcom.ExecuteNonQuery();
                    sqlcon.Close();
                    filestream.Close();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            }


        }
        #endregion
        #region ADD_WATER_MARK
        /**/
        /// <summary>
        /// 在图片上增加文字水印
        /// </summary>
        /// <param name="Path">原服务器图片路径</param>
        /// <param name="Path_sy">生成的带文字水印的图片路径</param>
        public void ADD_WATER_MARK(string Path, string Path_sy, string WATER_MARK)
        {
            Color c2 = System.Drawing.ColorTranslator.FromHtml("#9c9c9c");
            string addText = WATER_MARK;
            System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            System.Drawing.Font f = new System.Drawing.Font("Verdana", 30);
            System.Drawing.Brush b = new System.Drawing.SolidBrush(c2);
            g.DrawString(addText, f, b, 35, 35);
            g.Dispose();
            image.Save(Path_sy);
            image.Dispose();
        }
        #endregion
        #region  MakeThumbnail
        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
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
        public int Upload_Request(string address, string fileNamePath, string saveName, ProgressBar progressBar,string WAREID)
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
                    TIME  = "已用时：" + second.ToString("F2") + "秒";

                    if (second > 0.001)
                    {
                       AVERAGE_SPEED = " 平均速度：" + (offset / 1024 / second).ToString("0.00") + "KB/秒";
                    }
                    else
                    {
                        AVERAGE_SPEED  = " 正在连接…";
                    }
                    ALREADY_UPLOADED = "已上传：" + (offset * 100.0 / length).ToString("F2") + "%";
                    PROGRESS = (offset / 1048576.0).ToString("F2") + "M/" + (fileLength / 1048576.0).ToString("F2") + "M";
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
                newFileName = System.IO.Path.GetFileName(saveName );
                uriString = "http://" + SERVER_IP_OR_DOMAIN + "/uploadfile/" + newFileName;
                basec.getcoms(@"INSERT INTO WAREFILE(FLKEY,WAREID,OLD_FILE_NAME,PATH,INITIAL_OR_OTHER,DATE,YEAR,MONTH,DAY) VALUES 
('" + v1 + "','" + WAREID + "','" + System.IO.Path.GetFileName(fileNamePath) +
"','" + uriString + "','" + INITIAL_OR_OTHER + "','" + varDate + "','" + year + "','" + month + "','" + day + "')");




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
            return returnValue;
        }
        #endregion
    }
   
}
