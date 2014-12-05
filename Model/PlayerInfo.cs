using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model
{
    public class PlayerInfo
    {
        public string name { get; set; }
        public string identifier { get; set; }
        public StatInfo hp { get; set; }
        public StatInfo mp { get; set; }
        public StatInfo special { get; set; }
        public StatInfo level { get; set; }
        public List<EquipmentInfo> equipment { get; set; }
        [NotAGuiElement]
        public string createDate { get; set; }
        public StatInfo xp { get; set; }
        public StatInfo gold { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string map { get; set; }
        public string professionName { get; set; }
        public string overflow { get; set; }
        public string lastLogin { get; set; }
        public string gender { get; set; }
        [NotAGuiElement]
        public string priorityPoints { get; set; }
        public string isOnline { get; set; }
        public string registrationDate { get; set; }
        [NotAGuiElement]
        public string tempSecureToken { get; set; }
        [NotAGuiElement]
        public string _all { get; set; }
        [NotAGuiElement]
        public string password { get; set; }
        public string isBusy { get; set; }
        [NotAGuiElement]
        public string delimiter { get; set; }
        [DeserializeAs(Name = "event")]
        [NotAGuiElement]
        public string _event { get; set; }
        [NotAGuiElement]
        public string _baseStats { get; set; }
        [NotAGuiElement]
        public string _statCache { get; set; }
        public string wildcard { get; set; }
        [NotAGuiElement]
        public string listenerTree { get; set; }
        [NotAGuiElement]
        public string newListener { get; set; }
        [NotAGuiElement]
        public string _conf { get; set; }
        public string guildStatus { get; set; }
        public string personalityStrings { get; set; }
        public string permanentAchievements { get; set; }
        public List<EventInfo> recentEvents { get; set; }

    }
}
