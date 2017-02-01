using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EatCometsClear
{
    class MyConfig
    {
        public string screenX;
        public string screenY;
        public string windowMode;

        public MyConfig()
        {
            string x = System.Configuration.ConfigurationManager.AppSettings["screenX"];
            string y = System.Configuration.ConfigurationManager.AppSettings["screenY"];
            string windowMode = System.Configuration.ConfigurationManager.AppSettings["WindowMode"];

            this.screenX = x;
            this.screenY = y;
            this.windowMode = windowMode;

        }

        public void SaveConfig()
        {
            UpdateSetting("screenX",Convert.ToString(screenX));
            UpdateSetting("screenY",Convert.ToString(screenY));
            UpdateSetting("WindowMode", Convert.ToString(windowMode));

            System.Configuration.ConfigurationManager.AppSettings.Set("screenX", Convert.ToString(screenX));
            System.Configuration.ConfigurationManager.AppSettings.Set("screenY", Convert.ToString(screenY));
            System.Configuration.ConfigurationManager.AppSettings.Set("WindowMode", Convert.ToString(windowMode));

        }

        private static void UpdateSetting(string key, string value)
        {
            System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.
                OpenExeConfiguration( System.Reflection.Assembly.GetExecutingAssembly().Location);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
