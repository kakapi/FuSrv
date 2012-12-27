using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using FuLib;
using System.IO;
namespace CCEncryptor
{

    public partial class Cryp : Form
    {
       
        public Cryp()
        {
            InitializeComponent();
        }
        string savepath = string.Empty;
      
       

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string pwd = tbxPwd.Text.Trim();
            if (string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("不能为空");
                return;
            }
            tbxEncrypted.Text = FuLib.Crypto.EncryptStringAES(pwd, "P@ssw0rd");
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbxEncrypted.Text);
            MessageBox.Show("复制成功.");
        }
    }
}
