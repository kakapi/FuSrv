namespace FUServer
{
    partial class FrmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.pnlAction = new System.Windows.Forms.Panel();
            this.lblServerState = new System.Windows.Forms.Label();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.tbxLog = new System.Windows.Forms.RichTextBox();
            this.tbMain = new System.Windows.Forms.TabControl();
            this.tpStatus = new System.Windows.Forms.TabPage();
            this.tpConfig = new System.Windows.Forms.TabPage();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblCheckProgress = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.userConfig1 = new FUServer.UserConfig();
            this.systrayicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsExit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmServerStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExit = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlAction.SuspendLayout();
            this.tbMain.SuspendLayout();
            this.tpStatus.SuspendLayout();
            this.tpConfig.SuspendLayout();
            this.cmsExit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAction
            // 
            this.pnlAction.Controls.Add(this.lblServerState);
            this.pnlAction.Controls.Add(this.btnClearLog);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAction.Location = new System.Drawing.Point(3, 3);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Size = new System.Drawing.Size(515, 44);
            this.pnlAction.TabIndex = 0;
            // 
            // lblServerState
            // 
            this.lblServerState.AutoSize = true;
            this.lblServerState.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServerState.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblServerState.Location = new System.Drawing.Point(12, 10);
            this.lblServerState.Name = "lblServerState";
            this.lblServerState.Size = new System.Drawing.Size(100, 24);
            this.lblServerState.TabIndex = 2;
            this.lblServerState.Text = "服务已启动";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(344, 11);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 0;
            this.btnClearLog.Text = "清空日志";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // tbxLog
            // 
            this.tbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxLog.Location = new System.Drawing.Point(3, 47);
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.ReadOnly = true;
            this.tbxLog.Size = new System.Drawing.Size(515, 306);
            this.tbxLog.TabIndex = 1;
            this.tbxLog.Text = "";
            this.tbxLog.TextChanged += new System.EventHandler(this.tbxLog_TextChanged);
            // 
            // tbMain
            // 
            this.tbMain.Controls.Add(this.tpStatus);
            this.tbMain.Controls.Add(this.tpConfig);
            this.tbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.SelectedIndex = 0;
            this.tbMain.Size = new System.Drawing.Size(529, 381);
            this.tbMain.TabIndex = 2;
            // 
            // tpStatus
            // 
            this.tpStatus.Controls.Add(this.tbxLog);
            this.tpStatus.Controls.Add(this.pnlAction);
            this.tpStatus.Location = new System.Drawing.Point(4, 21);
            this.tpStatus.Name = "tpStatus";
            this.tpStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tpStatus.Size = new System.Drawing.Size(521, 356);
            this.tpStatus.TabIndex = 0;
            this.tpStatus.Text = "服务状态";
            this.tpStatus.UseVisualStyleBackColor = true;
            // 
            // tpConfig
            // 
            this.tpConfig.Controls.Add(this.btnStop);
            this.tpConfig.Controls.Add(this.btnStart);
            this.tpConfig.Controls.Add(this.lblCheckProgress);
            this.tpConfig.Controls.Add(this.btnReset);
            this.tpConfig.Controls.Add(this.btnSave);
            this.tpConfig.Controls.Add(this.userConfig1);
            this.tpConfig.Location = new System.Drawing.Point(4, 21);
            this.tpConfig.Name = "tpConfig";
            this.tpConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfig.Size = new System.Drawing.Size(521, 356);
            this.tpConfig.TabIndex = 1;
            this.tpConfig.Text = "相关配置";
            this.tpConfig.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ForeColor = System.Drawing.Color.Crimson;
            this.btnStop.Location = new System.Drawing.Point(109, 323);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 180;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.ForeColor = System.Drawing.Color.Green;
            this.btnStart.Location = new System.Drawing.Point(21, 323);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 170;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblCheckProgress
            // 
            this.lblCheckProgress.AutoSize = true;
            this.lblCheckProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblCheckProgress.Location = new System.Drawing.Point(318, 304);
            this.lblCheckProgress.Name = "lblCheckProgress";
            this.lblCheckProgress.Size = new System.Drawing.Size(179, 12);
            this.lblCheckProgress.TabIndex = 16;
            this.lblCheckProgress.Text = "正在检查服务器配置,请稍候....";
            this.lblCheckProgress.Visible = false;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(330, 323);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 140;
            this.btnReset.Text = "还原";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(422, 323);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 130;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // userConfig1
            // 
            this.userConfig1.Location = new System.Drawing.Point(12, 10);
            this.userConfig1.Name = "userConfig1";
            this.userConfig1.Size = new System.Drawing.Size(507, 296);
            this.userConfig1.TabIndex = 15;
            // 
            // systrayicon
            // 
            this.systrayicon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.systrayicon.ContextMenuStrip = this.cmsExit;
            this.systrayicon.Icon = ((System.Drawing.Icon)(resources.GetObject("systrayicon.Icon")));
            this.systrayicon.Text = "呼叫中心";
            this.systrayicon.Visible = true;
            this.systrayicon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.systrayicon_MouseDoubleClick);
            // 
            // cmsExit
            // 
            this.cmsExit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmServerStatus,
            this.tsmConfig,
            this.tsmExit});
            this.cmsExit.Name = "cmsExit";
            this.cmsExit.Size = new System.Drawing.Size(125, 70);
            // 
            // tsmServerStatus
            // 
            this.tsmServerStatus.Name = "tsmServerStatus";
            this.tsmServerStatus.Size = new System.Drawing.Size(124, 22);
            this.tsmServerStatus.Text = "服务状态";
            this.tsmServerStatus.Click += new System.EventHandler(this.tsmServerStatus_Click);
            // 
            // tsmConfig
            // 
            this.tsmConfig.Name = "tsmConfig";
            this.tsmConfig.Size = new System.Drawing.Size(124, 22);
            this.tsmConfig.Text = "配置";
            this.tsmConfig.Click += new System.EventHandler(this.tsmConfig_Click);
            // 
            // tsmExit
            // 
            this.tsmExit.Name = "tsmExit";
            this.tsmExit.Size = new System.Drawing.Size(124, 22);
            this.tsmExit.Text = "退出";
            this.tsmExit.Click += new System.EventHandler(this.tsmExit_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 381);
            this.Controls.Add(this.tbMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "呼叫中心 - 服务";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.pnlAction.ResumeLayout(false);
            this.pnlAction.PerformLayout();
            this.tbMain.ResumeLayout(false);
            this.tpStatus.ResumeLayout(false);
            this.tpConfig.ResumeLayout(false);
            this.tpConfig.PerformLayout();
            this.cmsExit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAction;
        private System.Windows.Forms.RichTextBox tbxLog;
        private System.Windows.Forms.Label lblServerState;
        private System.Windows.Forms.TabControl tbMain;
        private System.Windows.Forms.TabPage tpStatus;
        private System.Windows.Forms.TabPage tpConfig;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSave;
        private UserConfig userConfig1;
        private System.Windows.Forms.NotifyIcon systrayicon;
        private System.Windows.Forms.ContextMenuStrip cmsExit;
        private System.Windows.Forms.ToolStripMenuItem tsmExit;
        private System.Windows.Forms.ToolStripMenuItem tsmConfig;
        private System.Windows.Forms.Label lblCheckProgress;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.ToolStripMenuItem tsmServerStatus;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
    }
}

