namespace CCEncryptorServer
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.tbxOriginal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.tbxEncrypted = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.llbCopy = new System.Windows.Forms.LinkLabel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbxOriginal
            // 
            this.tbxOriginal.Location = new System.Drawing.Point(104, 24);
            this.tbxOriginal.Multiline = true;
            this.tbxOriginal.Name = "tbxOriginal";
            this.tbxOriginal.Size = new System.Drawing.Size(331, 37);
            this.tbxOriginal.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "机器码";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(203, 83);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(102, 40);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "生成";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // tbxEncrypted
            // 
            this.tbxEncrypted.Location = new System.Drawing.Point(104, 150);
            this.tbxEncrypted.Multiline = true;
            this.tbxEncrypted.Name = "tbxEncrypted";
            this.tbxEncrypted.Size = new System.Drawing.Size(331, 36);
            this.tbxEncrypted.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 164);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "注册码";
            // 
            // llbCopy
            // 
            this.llbCopy.AutoSize = true;
            this.llbCopy.Location = new System.Drawing.Point(324, 199);
            this.llbCopy.Name = "llbCopy";
            this.llbCopy.Size = new System.Drawing.Size(29, 12);
            this.llbCopy.TabIndex = 3;
            this.llbCopy.TabStop = true;
            this.llbCopy.Text = "复制";
            this.llbCopy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbCopy_LinkClicked);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.BackColor = System.Drawing.Color.LimeGreen;
            this.lblMsg.ForeColor = System.Drawing.Color.Firebrick;
            this.lblMsg.Location = new System.Drawing.Point(382, 199);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 12);
            this.lblMsg.TabIndex = 4;
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 242);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.llbCopy);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxEncrypted);
            this.Controls.Add(this.tbxOriginal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxOriginal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox tbxEncrypted;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel llbCopy;
        private System.Windows.Forms.Label lblMsg;
    }
}

