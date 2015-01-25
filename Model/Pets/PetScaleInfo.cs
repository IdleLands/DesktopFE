using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model.Pets
{
    public class PetScaleInfo
    {
        public int maxLevel { get; set; }
        public int maxItemScore { get; set; }
        public int inventory { get; set; }
        public int goldStorage { get; set; }
        public int battleJoinPercent { get; set; }
        public int itemFindTimeDuration { get; set; }
        public int itemSellMultiplier { get; set; }
        public int itemFindBonus { get; set; }
        public int itemFindRangeMultiplier { get; set; }
        public int xpPerGold { get; set; }
    }
}
