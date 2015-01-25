using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model.Pets
{
    public class PetConfigCacheInfo
    {
        public int cost { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public string slots { get; set; }
        public Dictionary<string, int> workaroundSlots
        {
            get
            {
                return JsonConvert.DeserializeObject<Dictionary<string, int>>(slots);
            }
        }
        public EquipmentInfo specialStats { get; set; }
        public string requirements { get; set; }
        public PetScaleUpgradeInfo scale { get; set; }
        public PetScaleUpgradeCostInfo scaleCost { get; set; }
    }
}
