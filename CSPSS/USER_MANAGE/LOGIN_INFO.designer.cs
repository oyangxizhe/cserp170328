namespace CSPSS.USER_MANAGE
{
    partial class LOGIN_INFO
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LOGIN_INFO));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.PictureBox();
            this.hint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 151);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1020, 466);
            this.dataGridView1.TabIndex = 100;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployeeInfo_CellClick);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dgvEmployeeInfo_DoubleClick);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(731, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "退出";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(57, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "将所有在线用户更新为离线";
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.InitialImage = null;
            this.btnExit.Location = new System.Drawing.Point(715, 20);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(60, 60);
            this.btnExit.TabIndex = 19;
            this.btnExit.TabStop = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnExit);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1014, 121);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单栏";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.InitialImage = null;
            this.btnSave.Location = new System.Drawing.Point(100, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 60);
            this.btnSave.TabIndex = 18;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // hint
            // 
            this.hint.AutoSize = true;
            this.hint.Location = new System.Drawing.Point(248, 136);
            this.hint.Name = "hint";
            this.hint.Size = new System.Drawing.Size(29, 12);
            this.hint.TabIndex = 101;
            this.hint.Text = "hint";
            // 
            // LOGIN_INFO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1020, 616);
            this.Controls.Add(this.hint);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "LOGIN_INFO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录信息维护";
            this.Load += new System.EventHandler(this.DEPAET_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox btnExit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox btnSave;
        private System.Windows.Forms.Label hint;

    }
}