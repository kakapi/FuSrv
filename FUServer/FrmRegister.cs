using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.IO;
namespace FUServer
{
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            
            tbxMachinaCode.Text = GlobalVariables.MachineCode;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbxRegisterCode.Text.Trim()))
            {
              MessageBox.Show("请填写注册码");
            }
            string decryptedCode = FuLib.Crypto.DecryptStringAES(tbxRegisterCode.Text.Trim(), "P@ssw0rd");
            if (tbxMachinaCode.Text == decryptedCode)
            {
                File.WriteAllText(GlobalVariables.SerialFileFullName, tbxRegisterCode.Text);
                MessageBox.Show("注册完成");
                GlobalVariables.IsRegisted = true;
                new FrmMain().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("注册失败:不是有效的注册码");
                new FuLib.Logger().GetLoggerInstance().Info("注册码无效,注册失败.填写的注册码是:"+tbxRegisterCode.Text);
            }
        }

        private void FrmRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(tbxMachinaCode.Text.Trim());
        }
    }
}
