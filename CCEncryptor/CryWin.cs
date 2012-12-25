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
    public partial class CryWin : Form
    {
        public CryWin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                
             string file=   saveFileDialog1.FileName;
             if (!File.Exists(file))
             {
                 Stream s = File.Create(file);
                 s.Close();
             }
             string crypted = CreateCode();
                File.WriteAllText(file
              , crypted);
                MessageBox.Show("保存成功");
            }
        }
        
        private string CreateCode()
        {
            string server = tbxServer.Text.Trim();
            string database = tbxDatabase.Text.Trim();
            string uid = tbxUID.Text.Trim();
            string pwd = tbxPwd.Text.Trim();

            string sharedsecret = "P@ssw0rd";

            string original = server+"|"+database+"|"+uid+"|"+pwd;

            string crypted = Crypto.EncryptStringAES(original, sharedsecret);
            return crypted;
          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
