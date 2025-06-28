using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSPSS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] a = new string[] { "100", "200", "3000", "4", "500" };
            int n = 0;
            for (int i = 0; i < a.Length; i++)
            {

                if (Convert.ToInt32(a[i]) > n)
                {
                    n = Convert.ToInt32(a[i]);
                }
            }
            MessageBox.Show(string.Format("最大值是:{0}", n.ToString()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
