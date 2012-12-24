namespace CCEncryptor
{
    partial class Cryp
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbxFileName = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.btnSelFld = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbxStr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxCode = new System.Windows.Forms.TextBox();
            this.lblSavedPath = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbxFileName
            // 
            this.tbxFileName.Location = new System.Drawing.Point(92, 44);
            this.tbxFileName.Name = "tbxFileName";
            this.tbxFileName.Size = new System.Drawing.Size(274, 21);
            this.tbxFileName.TabIndex = 0;
            this.tbxFileName.Text = "dbstr";
            // 
            // btnSelFld
            // 
            this.btnSelFld.Location = new System.Drawing.Point(102, 106);
            this.btnSelFld.Name = "btnSelFld";
            this.btnSelFld.Size = new System.Drawing.Size(75, 23);
            this.btnSelFld.TabIndex = 1;
            this.btnSelFld.Text = "....";
            this.btnSelFld.UseVisualStyleBackColor = true;
            this.btnSelFld.Click += new System.EventHandler(this.btnSelFld_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(92, 151);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbxStr
            // 
            this.tbxStr.Location = new System.Drawing.Point(88, 71);
            this.tbxStr.Name = "tbxStr";
            this.tbxStr.Size = new System.Drawing.Size(346, 21);
            this.tbxStr.TabIndex = 0;
            this.tbxStr.Text = "Server|Database|UserId|Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "wenjianming";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "zifuchuan";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "encryptcode";
            this.label3.Visible = false;
            // 
            // tbxCode
            // 
            this.tbxCode.Location = new System.Drawing.Point(92, 12);
            this.tbxCode.Name = "tbxCode";
            this.tbxCode.Size = new System.Drawing.Size(274, 21);
            this.tbxCode.TabIndex = 4;
            this.tbxCode.Text = "P@ssw0rd";
            this.tbxCode.Visible = false;
            // 
            // lblSavedPath
            // 
            this.lblSavedPath.AutoSize = true;
            this.lblSavedPath.Location = new System.Drawing.Point(203, 106);
            this.lblSavedPath.Name = "lblSavedPath";
            this.lblSavedPath.Size = new System.Drawing.Size(0, 12);
            this.lblSavedPath.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "ChooseSavePath";
            // 
            // Cryp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 262);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblSavedPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSelFld);
            this.Controls.Add(this.tbxStr);
            this.Controls.Add(this.tbxFileName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Cryp";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxFileName;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnSelFld;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbxStr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxCode;
        private System.Windows.Forms.Label lblSavedPath;
        private System.Windows.Forms.Label label5;
    }
}

