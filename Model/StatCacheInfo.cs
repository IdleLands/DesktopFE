using IdleLandsGUI.CustomAttributes;
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
        public long str { get; set; }
        [IconElement(Name = "heart.png")]
        public long con { get; set; }
        [IconElement(Name = "crosshairs.png")]
        public long dex { get; set; }
        [IconElement(Name = "mortar-board.png")]
        [DeserializeAs(Name = "int")]
        public long _int { get; set; }
        [IconElement(Name = "book.png")]
        public long wis { get; set; }
        [IconElement(Name = "bicycle.png")]
        public long agi { get; set; }
        [IconElement(Name = "moon-o.png")]
        public long luck { get; set; }
        [IconElement(Name = "fire.png")]
        public long fire { get; set; }
        [IconElement(Name = "tint.png")]
        public long water { get; set; }
        [IconElement(Name = "leaf.png")]
        public long earth { get; set; }
        [IconElement(Name = "bolt.png")]
        public long thunder { get; set; }
        [IconElement(Name = "bus.png")]
        public long ice { get; set; }
        public long strPercent { get; set; }
        public long conPercent { get; set; }
        public long dexPercent { get; set; }
        public long intPercent { get; set; }
        public long wisPercent { get; set; }
        public long agiPercent { get; set; }
        public long luckPercent { get; set; }
        public long firePercent { get; set; }
        public long waterPercent { get; set; }
        public long earthPercent { get; set; }
        public long thunderPercent { get; set; }
        public long icePercent { get; set; }
    }
}
