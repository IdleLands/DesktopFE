using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model
{
    public class PetScaleUpgradeInfo
    {
        public List<int> maxLevel { get; set; }
        public List<int> maxItemScore { get; set; }
        public List<int> inventory { get; set; }
        public List<int> goldStorage { get; set; }
        public List<int> battleJoinPercent { get; set; }
        public List<int> itemFindTimeDuration { get; set; }
        public List<float> itemSellMultiplier { get; set; }
        public List<int> itemFindBonus { get; set; }
        public List<float> itemFindRangeMultiplier { get; set; }
        public List<int> xpPerGold { get; set; }
    }
}
