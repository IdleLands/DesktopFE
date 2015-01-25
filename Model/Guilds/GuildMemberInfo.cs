using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model.Guilds
{
    public class GuildMemberInfo
    {
        public string identifier { get; set; }
        public string name { get; set; }
        public bool isAdmin { get; set; }
        public GuildMemberCacheInfo _cache { get; set; }
    }
}
