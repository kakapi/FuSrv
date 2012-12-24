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
        
        private void btnSelFld_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                savepath = folderBrowserDialog1.SelectedPath;
                lblSavedPath.Text = savepath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string dbstr=tbxStr.Text.Trim();
            string code = tbxCode.Text.Trim();
            string crypted = Crypto.EncryptStringAES(dbstr,code);
            File.WriteAllText(FuLib.GlobalHelper.EnsurePathEndWithSlash( savepath) + tbxFileName.Text.Trim()
                ,crypted);

            MessageBox.Show("success");
        }
    }
}
