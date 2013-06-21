using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TestCompiler
{
    class AppSettings
    {
        private static AppSettings _appSettings = null;

        private AppSettings()
        {
                
        }

        public  static AppSettings Default
        {
            get
            {
                if (_appSettings == null)
                    _appSettings = new AppSettings();
                return _appSettings;
            }
        }

        public string[] AssembliesReferences
        {
            get { 
                string setting = ConfigurationManager.AppSettings["Assemblies"];
                if (string.IsNullOrEmpty(setting))
                {
                    return new string[0];
                }
                setting = setting.Trim();
                if (setting.EndsWith(";"))
                {
                    setting = setting.Substring(0, setting.Length - 1);
                }
                return setting.Split(';');
            }
        }

        public string OutputName
        {
            get
            {
                return ConfigurationManager.AppSettings["OutputName"];
            }
        }

    }
}
