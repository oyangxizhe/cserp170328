using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.ServiceModel;
using System.Net;
using System.Data.OleDb;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace XizheC
{
    public class UploadFile
    {
        /// <summary> 
        /// 将本地文件上传到指定的服务器(HttpWebRequest方法) 
        /// </summary> 
        /// <param name="address">文件上传到的服务器</param> 
        /// <param name="fileNamePath">要上传的本地文件（全路径）</param> 
        /// <param name="progressBar">上传进度条</param> 
        /// <returns>成功返回1，失败返回0</returns> 
        public static int Upload_Request(string address, string fileNamePath, System.Windows.Forms.ProgressBar progressBar)
        {
            int returnValue = 0;
            // 要上传的文件 
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);
            // 根据uri创建HttpWebRequest对象 
            address = string.Concat(address, "?filename=", Path.GetFileName(fileNamePath));
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));
            httpReq.Method = "POST";
            //对发送的数据不使用缓存 
            httpReq.AllowWriteStreamBuffering = false;
            //设置获得响应的超时时间（30秒） 
            httpReq.Timeout = 30000;
            long fileLength = fs.Length;
            httpReq.ContentLength = fileLength;
            progressBar.Maximum = (int)fs.Length;
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
            while (size > 0)
            {
                postStream.Write(buffer, 0, size);
                offset += size;
                progressBar.Value = (int)offset;
                TimeSpan span = DateTime.Now - startTime;
                double second = span.TotalSeconds;
                Console.WriteLine("已用时：" + second.ToString("F2") + "秒");
                if (second > 0.1)
                {
                    MessageBox.Show(" 平均速度：" + (offset / 1024 / second).ToString("0.00") + "KB/秒");
                }
                else
                {
                    MessageBox.Show(" 正在连接…");
                }
                Console.WriteLine("已上传：" + (offset * 100.0 / fileLength).ToString("F2") + "%");
                Console.WriteLine((offset / 1048576.0).ToString("F2") + "M/" + (fileLength / 1048576.0).ToString("F2") + "M");
                Application.DoEvents();
                size = r.Read(buffer, 0, bufferLength);
            }
            postStream.Close();
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
            try
            {
 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                returnValue = 0;
            }
            finally
            {
                fs.Close();
                r.Close();
            }
            return returnValue;
        }

        /// <summary> 
        /// 将本地文件上传到指定的服务器(WebClient方法) 
        /// </summary> 
        /// <param name="address">文件上传到的服务器</param> 
        /// <param name="fileNamePath">要上传的本地文件（全路径）</param> 
        /// <param name="progressBar">上传进度条</param> 
        /// <returns>成功返回1，失败返回0</returns> 
 

    }
}
