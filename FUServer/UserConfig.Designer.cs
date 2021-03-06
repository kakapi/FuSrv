﻿namespace FUServer
{
    partial class UserConfig
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbxClientValidationURL = new System.Windows.Forms.TextBox();
            this.ftpPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ftpPwd = new System.Windows.Forms.TextBox();
            this.ftpIP = new System.Windows.Forms.TextBox();
            this.ftpUser = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbxAccessPwd = new System.Windows.Forms.TextBox();
            this.tbxDatabase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbxPwd = new System.Windows.Forms.TextBox();
            this.tbxServer = new System.Windows.Forms.TextBox();
            this.tbxUID = new System.Windows.Forms.TextBox();
            this.tbxTableCallLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tbxSocketPort = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ftpPort);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.ftpPwd);
            this.groupBox2.Controls.Add(this.ftpIP);
            this.groupBox2.Controls.Add(this.ftpUser);
            this.groupBox2.Location = new System.Drawing.Point(217, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 157);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FTP配置";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbxClientValidationURL);
            this.groupBox4.Location = new System.Drawing.Point(217, 166);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(281, 59);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "客户端有效性检查URL";
            // 
            // tbxClientValidationURL
            // 
            this.tbxClientValidationURL.Location = new System.Drawing.Point(6, 25);
            this.tbxClientValidationURL.Name = "tbxClientValidationURL";
            this.tbxClientValidationURL.Size = new System.Drawing.Size(246, 21);
            this.tbxClientValidationURL.TabIndex = 11;
            // 
            // ftpPort
            // 
            this.ftpPort.Location = new System.Drawing.Point(72, 56);
            this.ftpPort.Name = "ftpPort";
            this.ftpPort.Size = new System.Drawing.Size(193, 21);
            this.ftpPort.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "远程路径";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "用户名";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(37, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "密码";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "端口号";
            // 
            // ftpPwd
            // 
            this.ftpPwd.Location = new System.Drawing.Point(72, 124);
            this.ftpPwd.Name = "ftpPwd";
            this.ftpPwd.PasswordChar = '*';
            this.ftpPwd.Size = new System.Drawing.Size(193, 21);
            this.ftpPwd.TabIndex = 10;
            // 
            // ftpIP
            // 
            this.ftpIP.Location = new System.Drawing.Point(76, 22);
            this.ftpIP.Name = "ftpIP";
            this.ftpIP.Size = new System.Drawing.Size(193, 21);
            this.ftpIP.TabIndex = 7;
            // 
            // ftpUser
            // 
            this.ftpUser.Location = new System.Drawing.Point(72, 90);
            this.ftpUser.Name = "ftpUser";
            this.ftpUser.Size = new System.Drawing.Size(193, 21);
            this.ftpUser.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbxTableCallLog);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.tbxDatabase);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbxPwd);
            this.groupBox1.Controls.Add(this.tbxServer);
            this.groupBox1.Controls.Add(this.tbxUID);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 288);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库配置";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbxAccessPwd);
            this.groupBox3.Location = new System.Drawing.Point(0, 212);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(179, 59);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "客户端Access密码";
            // 
            // tbxAccessPwd
            // 
            this.tbxAccessPwd.Location = new System.Drawing.Point(36, 25);
            this.tbxAccessPwd.Name = "tbxAccessPwd";
            this.tbxAccessPwd.PasswordChar = '*';
            this.tbxAccessPwd.Size = new System.Drawing.Size(133, 21);
            this.tbxAccessPwd.TabIndex = 6;
            this.tbxAccessPwd.Text = "quanjiu";
            // 
            // tbxDatabase
            // 
            this.tbxDatabase.Location = new System.Drawing.Point(75, 63);
            this.tbxDatabase.Name = "tbxDatabase";
            this.tbxDatabase.Size = new System.Drawing.Size(100, 21);
            this.tbxDatabase.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "服务器";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "用户名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "密码";
          
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "数据库";
            // 
            // tbxPwd
            // 
            this.tbxPwd.Location = new System.Drawing.Point(75, 186);
            this.tbxPwd.Name = "tbxPwd";
            this.tbxPwd.PasswordChar = '*';
            this.tbxPwd.Size = new System.Drawing.Size(100, 21);
            this.tbxPwd.TabIndex = 5;
            // 
            // tbxServer
            // 
            this.tbxServer.Location = new System.Drawing.Point(75, 23);
            this.tbxServer.Name = "tbxServer";
            this.tbxServer.Size = new System.Drawing.Size(100, 21);
            this.tbxServer.TabIndex = 1;
            // 
            // tbxUID
            // 
            this.tbxUID.Location = new System.Drawing.Point(75, 145);
            this.tbxUID.Name = "tbxUID";
            this.tbxUID.Size = new System.Drawing.Size(100, 21);
            this.tbxUID.TabIndex = 4;
            // 
            // tbxTableCallLog
            // 
            this.tbxTableCallLog.Location = new System.Drawing.Point(75, 104);
            this.tbxTableCallLog.Name = "tbxTableCallLog";
            this.tbxTableCallLog.Size = new System.Drawing.Size(100, 21);
            this.tbxTableCallLog.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "表名";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tbxSocketPort);
            this.groupBox5.Location = new System.Drawing.Point(217, 231);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(281, 50);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Socket端口号";
            // 
            // tbxSocketPort
            // 
            this.tbxSocketPort.Location = new System.Drawing.Point(6, 21);
            this.tbxSocketPort.Name = "tbxSocketPort";
            this.tbxSocketPort.Size = new System.Drawing.Size(130, 21);
            this.tbxSocketPort.TabIndex = 12;
            // 
            // UserConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "UserConfig";
            this.Size = new System.Drawing.Size(504, 298);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox ftpPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ftpPwd;
        private System.Windows.Forms.TextBox ftpIP;
        private System.Windows.Forms.TextBox ftpUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxDatabase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbxPwd;
        private System.Windows.Forms.TextBox tbxServer;
        private System.Windows.Forms.TextBox tbxUID;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbxAccessPwd;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbxClientValidationURL;
        private System.Windows.Forms.TextBox tbxTableCallLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox tbxSocketPort;
    }
}
