using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model.Guilds
{
    public class GuildInfo
    {
        public string name { get; set; }
        public string leader { get; set; }
        public string createDate { get; set; }
        public List<string> invites { get; set; }
        public string leaderName { get; set; }
        public int level { get; set; }
        public int invitesAvailable { get; set; }
        public int taxPercent { get; set; }
        public string buffs { get; set; }
        public StatInfo gold { get; set; }
        public string @base { get; set;}
        public bool autoRenew { get; set; }
        public List<GuildMemberInfo> members { get; set; }
    }
}
