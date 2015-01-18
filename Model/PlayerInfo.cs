using IdleLandsGUI.CustomAttributes;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model
{
    public class PlayerInfo
    {
        public string name { get; set; }
        public string identifier { get; set; }
        [IconElement(Name = "heart.png", Colour = "Red")]
        public StatInfo hp { get; set; }
        [IconElement(Name = "magic.png", Colour = "Blue")]
        public StatInfo mp { get; set; }
        [IconElement(Name = "asterisk.png", Colour = "YellowGreen")]
        public StatInfo special { get; set; }
        public StatInfo level { get; set; }
        public List<EquipmentInfo> equipment { get; set; }
        [NotAGuiElement]
        public string createDate { get; set; }
        [IconElement(HideMax = false)]
        public StatInfo xp { get; set; }
        [IconElement(Name = "money.png", Colour = "Yellow", HideMax = true)]
        public StatInfo gold { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string map { get; set; }
        public string professionName { get; set; }
        public List<EquipmentInfo> overflow { get; set; }
        public string lastLogin { get; set; }
        public string gender { get; set; }
        public PriorityPointsInfo priorityPoints { get; set; }
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
        public StatCacheInfo _statCache { get; set; }
        public string wildcard { get; set; }
        [NotAGuiElement]
        public string listenerTree { get; set; }
        [NotAGuiElement]
        public string newListener { get; set; }
        [NotAGuiElement]
        public string _conf { get; set; }
        public int? guildStatus { get; set; }
        public string guild { get; set; }
        public string personalityStrings { get; set; }
        public string permanentAchievements { get; set; }
        public List<EventInfo> recentEvents { get; set; }
        [NotAGuiElement]
        public string foundPets { get; set; }
        //Restsharp doesn't support dictionaries, see https://github.com/restsharp/RestSharp/issues/607
        public List<FoundPetInfo> workaroundPets { get; set; }
    }
}
