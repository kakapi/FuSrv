using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FUServer.Properties;
using System.IO;
namespace FUServer
{
    public partial class UserConfig : UserControl
    {
        public UserConfig()
        {
            InitializeComponent();
            LoadData();
        }
        string configFileName = AppDomain.CurrentDomain.BaseDirectory + "conf.ig";
       
        private void LoadData()
        {

            if (!File.Exists(configFileName))
            {
                return;
            }
            string encrypted = File.ReadAllText(configFileName);
            string decrypted = FuLib.Crypto.DecryptStringAES(encrypted,"P@ssw0rd");

            string[] dbconfig = decrypted.Split(';')[0].Split('|');
            string[] ftpconfig = decrypted.Split(';')[1].Split('|');
 tbxServer.Text = dbconfig[0];
            tbxDatabase.Text =dbconfig[1];
           //Properties.Settings.Default.DbServer;
            tbxUID.Text = dbconfig[2];
            tbxPwd.Text = dbconfig[3];

            ftpIP.Text = ftpconfig[0];
            ftpPort.Text = ftpconfig[1];
            ftpUser.Text = ftpconfig[2];
            ftpPwd.Text = ftpconfig[3];
           
        }
        private string CreateCode()
        {
            string server = tbxServer.Text.Trim();
            string database = tbxDatabase.Text.Trim();
            string uid = tbxUID.Text.Trim();
            string pwd = tbxPwd.Text.Trim();

            string sharedsecret = "P@ssw0rd";

            string original = server + "|" + database + "|" + uid + "|" + pwd + ";" + ftpIP.Text + "|" + ftpPort.Text + "|" + ftpUser.Text + "|" + ftpPwd.Text;

            string crypted = FuLib.Crypto.EncryptStringAES(original, sharedsecret);
            return crypted;
        }



        public bool Save()
        {
            if (string.IsNullOrEmpty(tbxServer.Text))
            {
                MessageBox.Show("请填写数据库服务器地址.");
                return false;
            }
            if (string.IsNullOrEmpty(tbxDatabase.Text))
            {
                MessageBox.Show("请填写数据库名称.");
                return false;
            }

            if (string.IsNullOrEmpty(ftpIP.Text))
            {
                MessageBox.Show("请填写ftp服务器路径.");
                return false;
            }
            if (string.IsNullOrEmpty(ftpPort.Text))
            {
                MessageBox.Show("请填写ftp端口号.");
                return false;
            }
            //    | 
            //    | string.IsNullOrEmpty(ftpIP.Text)
            //    | string.IsNullOrEmpty(ftpPort.Text)
            //    )
            //{
            //    MessageBox.Show("请填写相关信息.");
            //    return;
            //}

            SaveData();
            return true;
          

        }

        public void Reset()
        {
            LoadData();
        }
        private void SaveData()
        {
            if (!File.Exists(configFileName))
            {
              FileStream fs=  File.Create(configFileName);
              fs.Close();
            }
            string server = tbxServer.Text.Trim();
            string database = tbxDatabase.Text.Trim();
            string uid = tbxUID.Text.Trim();
            string pwd = tbxPwd.Text.Trim();

            string _ftpPath = ftpIP.Text;
            string _ftpPort = ftpPort.Text;
            string _ftpUid = ftpUser.Text;
            string _ftpPassword = ftpPwd.Text;

            string sharedsecret = "P@ssw0rd";

            string original = server + "|" + database + "|" + uid + "|" + pwd + ";"
                + _ftpPath + "|" + _ftpPort + "|" + _ftpUid + "|" + _ftpPassword;

            string crypted = FuLib.Crypto.EncryptStringAES(original, sharedsecret);
            File.WriteAllText(configFileName, crypted);
           
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

