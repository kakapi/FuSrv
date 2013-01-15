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

        private void LoadData()
        {
            string decrypted = ServerInfo.GetDecryptedInfo();
            string[] splitedInfo = decrypted.Split(';');
            if (splitedInfo.Length != 5)
            {
                return;
            }
            if (string.IsNullOrEmpty(decrypted))
            {
                return;
            }
            string[] dbconfig = splitedInfo[0].Split('|');
            string[] ftpconfig = splitedInfo[1].Split('|');

            tbxServer.Text = dbconfig[0];
            tbxDatabase.Text = dbconfig[1];
            tbxTableCallLog.Text = dbconfig[2];
            //Properties.Settings.Default.DbServer;
            tbxUID.Text = dbconfig[3];
            tbxPwd.Text = dbconfig[4];

            ftpIP.Text = ftpconfig[0];
            ftpPort.Text = ftpconfig[1];
            ftpUser.Text = ftpconfig[2];
            ftpPwd.Text = ftpconfig[3];

            tbxAccessPwd.Text = splitedInfo[2];
            tbxClientValidationURL.Text = splitedInfo[3];

            tbxSocketPort.Text = splitedInfo[4];

        }
     

        public bool Save(out string errMsg)
        {

            errMsg = string.Empty;
            if (string.IsNullOrEmpty(tbxServer.Text))
            {
              errMsg="请填写数据库服务器地址.";
                return false;
            }
            if (string.IsNullOrEmpty(tbxDatabase.Text))
            {
                 errMsg="请填写数据库名称.";
                return false;
            }

            if (string.IsNullOrEmpty(ftpIP.Text))
            {
                 errMsg="请填写ftp服务器路径.";
                return false;
            }
            if (string.IsNullOrEmpty(ftpPort.Text))
            {
                errMsg = "请填写ftp端口号.";
                return false;
            }
            if (string.IsNullOrEmpty(tbxSocketPort.Text))
            {
                errMsg = "请填写Socket端口号.";
                return false;
            }
            if (! FuLib.ServerInfo.CheckServer(ftpIP.Text,ftpPort.Text, ftpUser.Text, ftpPwd.Text, tbxServer.Text,
                tbxDatabase.Text,GlobalVariables.CallLogTableName, tbxUID.Text, tbxPwd.Text, tbxClientValidationURL.Text, out errMsg))
            {
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

            string server = tbxServer.Text.Trim();
            string database = tbxDatabase.Text.Trim();
            string uid = tbxUID.Text.Trim();
            string pwd = tbxPwd.Text.Trim();
            string tableName=tbxTableCallLog.Text.Trim();

            string _ftpPath = ftpIP.Text;
            string _ftpPort = ftpPort.Text;
            string _ftpUid = ftpUser.Text;
            string _ftpPassword = ftpPwd.Text;

            string clientValidationUrl = tbxClientValidationURL.Text;
            string accessPwd = tbxAccessPwd.Text;
            
            string sharedsecret = "P@ssw0rd";
            string socketPort = tbxSocketPort.Text;

            string original = server + "|" + database + "|" + uid + "|" + pwd + "|" + tableName + ";"
                + _ftpPath + "|" + _ftpPort + "|" + _ftpUid + "|" + _ftpPassword + ";"
                + accessPwd + ";"
                + clientValidationUrl+";"
                +socketPort;
              

            string crypted = FuLib.Crypto.EncryptStringAES(original, sharedsecret);
            ServerInfo.SaveEncryptedInfo(crypted);

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

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

