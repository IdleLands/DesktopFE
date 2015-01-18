using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI
{
    class AppSettings : ApplicationSettingsBase
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
    }
}
