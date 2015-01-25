using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI
{
    public class AppSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool LogResponses
        {
            get
            {
                return ((bool)this["LogResponses"]);
            }
            set
            {
                this["LogResponses"] = (bool)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool LogRequests
        {
            get
            {
                return ((bool)this["LogRequests"]);
            }
            set
            {
                this["LogRequests"] = (bool)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("")]
        public string Server
        {
            get
            {
                return ((string)this["Server"]);
            }
            set
            {
                this["Server"] = (string)value;
            }
        }
    }
}
