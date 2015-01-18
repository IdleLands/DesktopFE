using IdleLandsGUI.CustomAttributes;
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
        [IconElement(Name = "bus.png")]
        public long sentimentality { get; set; }
        [IconElement(Name = "bus.png")]
        public long piety { get; set; }
        [IconElement(Name = "bus.png")]
        public long xp { get; set; }
        [IconElement(Name = "bus.png")]
        public long gold { get; set; }
        [EquipmentStat(BelongsTo = "str", IsPercent = true)]
        public long strPercent { get; set; }
        [EquipmentStat(BelongsTo = "con", IsPercent = true)]
        public long conPercent { get; set; }
        [EquipmentStat(BelongsTo = "dex", IsPercent = true)]
        public long dexPercent { get; set; }
        [EquipmentStat(BelongsTo = "_int", IsPercent = true)]
        public long intPercent { get; set; }
        [EquipmentStat(BelongsTo = "wis", IsPercent = true)]
        public long wisPercent { get; set; }
        [EquipmentStat(BelongsTo = "agi", IsPercent = true)]
        public long agiPercent { get; set; }
        [EquipmentStat(BelongsTo = "luck", IsPercent = true)]
        public long luckPercent { get; set; }
        [EquipmentStat(BelongsTo = "sentimentality", IsPercent = true)]
        public long sentimentalityPercent { get; set; }
        [EquipmentStat(BelongsTo = "piety", IsPercent = true)]
        public long pietyPercent { get; set; }
        [EquipmentStat(BelongsTo = "fire", IsPercent = true)]
        public long firePercent { get; set; }
        [EquipmentStat(BelongsTo = "water", IsPercent = true)]
        public long waterPercent { get; set; }
        [EquipmentStat(BelongsTo = "earth", IsPercent = true)]
        public long earthPercent { get; set; }
        [EquipmentStat(BelongsTo = "thunder", IsPercent = true)]
        public long thunderPercent { get; set; }
        [EquipmentStat(BelongsTo = "ice", IsPercent = true)]
        public long icePercent { get; set; }
        [EquipmentStat(BelongsTo = "xp", IsPercent = true)]
        public long xpPercent { get; set; }
        [EquipmentStat(BelongsTo = "gold", IsPercent = true)]
        public string goldPercent { get; set; }
        public string enchantLevel { get; set; }
        public string _calcScore { get; set; }
        public string _baseScore { get; set; }
        public string foundAt { get; set; }
        public ulong uid { get; set; }

        // modifiers ???

        public int punish { get; set; }
        public int glowing { get; set; }
        public int sturdy { get; set; }
        public int prone { get; set; }
        public int defense { get; set; }
        public int offense { get; set; }
        public int absolute { get; set; }
        public int hpregenPercent { get; set; }
        public int hpregen { get; set; }
        public int crit { get; set; }
        public int startle { get; set; }
        public int limitless { get; set; }

        public List<float> itemFindRangeMultiplier { get; set; }


    }
}
