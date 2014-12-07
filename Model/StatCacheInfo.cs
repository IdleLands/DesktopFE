using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model
{
    public class StatCacheInfo
    {
        [IconElement(Name = "legal.png")]
        public string str { get; set; }
        [IconElement(Name = "heart.png")]
        public string con { get; set; }
        [IconElement(Name = "crosshairs.png")]
        public string dex { get; set; }
        [IconElement(Name = "mortar-board.png")]
        [DeserializeAs(Name = "int")]
        public string _int { get; set; }
        [IconElement(Name = "book.png")]
        public string wis { get; set; }
        [IconElement(Name = "bicycle.png")]
        public string agi { get; set; }
        [IconElement(Name = "moon-o.png")]
        public string luck { get; set; }
        [IconElement(Name = "fire.png")]
        public string fire { get; set; }
        [IconElement(Name = "tint.png")]
        public string water { get; set; }
        [IconElement(Name = "leaf.png")]
        public string earth { get; set; }
        [IconElement(Name = "bolt.png")]
        public string thunder { get; set; }
        [IconElement(Name = "bus.png")]
        public string ice { get; set; }
        public string strPercent { get; set; }
        public string conPercent { get; set; }
        public string dexPercent { get; set; }
        public string intPercent { get; set; }
        public string wisPercent { get; set; }
        public string agiPercent { get; set; }
        public string luckPercent { get; set; }
        public string firePercent { get; set; }
        public string waterPercent { get; set; }
        public string earthPercent { get; set; }
        public string thunderPercent { get; set; }
        public string icePercent { get; set; }
    }
}
