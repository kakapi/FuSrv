using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace CCEncryptorServer
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
           
        }
        

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string machinecode = tbxOriginal.Text.Trim();
            string encrypted = FuLib.Crypto.EncryptStringAES(machinecode, "P@ssw0rd");
            tbxEncrypted.Text = encrypted;
        }

        private void llbCopy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(tbxEncrypted.Text);
            lblMsg.Text = "复制成功";
        }
    }
}
