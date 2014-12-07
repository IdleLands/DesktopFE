using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model
{
    public class EquipmentInfo
    {
        public string type { get; set; }
        [DeserializeAs(Name = "class")]
        public string _class { get; set; }
        public string name { get; set; }
        public string itemClass { get; set; }
        public string str { get; set; }
        public string con { get; set; }
        public string dex { get; set; }
        [DeserializeAs(Name = "int")]
        public string _int { get; set; }
        public string wis { get; set; }
        public string agi { get; set; }
        public string luck { get; set; }
        public string sentimentality { get; set; }
        public string piety { get; set; }
        public string fire { get; set; }
        public string water { get; set; }
        public string earth { get; set; }
        public string thunder { get; set; }
        public string ice { get; set; }
        public string xp { get; set; }
        public string gold { get; set; }
        public string strPercent { get; set; }
        public string conPercent { get; set; }
        public string dexPercent { get; set; }
        public string intPercent { get; set; }
        public string wisPercent { get; set; }
        public string agiPercent { get; set; }
        public string luckPercent { get; set; }
        public string sentimentalityPercent { get; set; }
        public string pietyPercent { get; set; }
        public string firePercent { get; set; }
        public string waterPercent { get; set; }
        public string earthPercent { get; set; }
        public string thunderPercent { get; set; }
        public string icePercent { get; set; }
        public string xpPercent { get; set; }
        public string goldPercent { get; set; }
        public string enchantLevel { get; set; }
        public string _calcScore { get; set; }
        public string _baseScore { get; set; }
        public string foundAt { get; set; }

    }
}
