using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace CCEncryptor
{
    public partial class CryWin : Form
    {
        public CryWin()
        {
            InitializeComponent();
            
        }

        private string CreateCode()
        {
            string server = tbxServer.Text.Trim();
            string database = tbxDatabase.Text.Trim();
            string uid = tbxUID.Text.Trim();
            string pwd = tbxPwd.Text.Trim();

            string sharedsecret = "P@ssw0rd";

            string original = server + "|" + database + "|" + uid + "|" + pwd + ";" + ftpIP.Text + "|" + ftpPort.Text + "|" + ftpUser.Text + "|" + ftpPwd.Text;

            string crypted =FuLib.Crypto.EncryptStringAES(original, sharedsecret);
            return crypted;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxServer.Text))
            {
                MessageBox.Show("请填写数据库服务器地址.");
                return;
            }
            if (string.IsNullOrEmpty( tbxDatabase.Text))
            {
            MessageBox.Show("请填写数据库名称.");
                return;
            }
           
            if (string.IsNullOrEmpty(ftpIP.Text))
            {
            MessageBox.Show("请填写ftp服务器路径.");
                return;
            }
            if (string.IsNullOrEmpty(ftpPort.Text))
            {
            MessageBox.Show("请填写ftp端口号.");
                return;
            }
            //    | 
            //    | string.IsNullOrEmpty(ftpIP.Text)
            //    | string.IsNullOrEmpty(ftpPort.Text)
            //    )
            //{
            //    MessageBox.Show("请填写相关信息.");
            //    return;
            //}

            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                string file = saveFileDialog1.FileName;
                if (!File.Exists(file))
                {
                    Stream s = File.Create(file);
                    s.Close();
                }
                string crypted = CreateCode();
                File.WriteAllText(file, crypted);
                MessageBox.Show("保存成功");
            }
        }

        private IEnumerable<Control> GetAllTextBoxControls(Control container)
        {
            List<Control> controlList = new List<Control>();
            foreach (Control c in container.Controls)
            {
                controlList.AddRange(GetAllTextBoxControls(c));
                if (c is TextBox)
                    controlList.Add(c);
            }
            return controlList;
        }
    }
}
