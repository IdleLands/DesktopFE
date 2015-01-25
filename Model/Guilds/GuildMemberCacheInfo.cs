using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model.Guilds
{
    public class GuildMemberCacheInfo
    {
        public bool online { get; set; }
        public int level { get; set; }
        public string @class { get; set; }
        public string lastSeen { get; set; }
    }
}
