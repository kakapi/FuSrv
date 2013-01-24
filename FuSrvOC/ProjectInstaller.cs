using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Configuration;
using System.Threading;
namespace FuSrvOC
{

    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
       
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            
            try
            {
                var map = new ExeConfigurationFileMap();

                //Get app.config path
                map.ExeConfigFilename = Context.Parameters["assemblypath"] + ".config";

                //Get Config and AppSettings
                var config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                var appSettings = config.AppSettings;

                appSettings.Settings["DbFilePath"].Value = Context.Parameters["DbFilePath"];
                
                appSettings.Settings["ServerIP"].Value = Context.Parameters["ServerIP"];
                appSettings.Settings["SocketPort"].Value = Context.Parameters["SocketPort"];
               
                //save app.config
                config.Save();
                
            }
            catch (Exception e)
            {
                string s = e.Message;
            }
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
        }
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }
    }
}



