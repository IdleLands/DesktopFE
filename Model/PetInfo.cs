using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model
{
    public class PetInfo
    {
        public string name { get; set; }
        public StatInfo hp { get; set; }
        public StatInfo mp { get; set; }
        public StatInfo special { get; set; }
        public StatInfo level { get; set; }
        public List<EquipmentInfo> equipment { get; set; }
        public string createDate { get; set; }
        public string type { get; set; }
        public List<string> attrs { get; set; }
        public PetOwnerInfo owner { get; set; }
        public PetOwnerInfo creator { get; set; }
        public StatInfo gold { get; set; }
        public StatInfo xp { get; set; }
        public string gender { get; set; }
        public string professionName { get; set; }
        public bool isMonster { get; set; }
        public bool isPet { get; set; }
        public bool isActive { get; set; }
        public bool smartSell { get; set; }
        public bool smartEquip { get; set; }
        public bool smartSelf { get; set; }
        public List<EquipmentInfo> inventory { get; set; }
        public PetScaleInfo scaleLevel { get; set; }
        public ulong lastInteraction { get; set; }
        public ulong createdAt { get; set; }
        public PetConfigCacheInfo _configCache { get; set; }
        public string nextItemFind { get; set; }
        public string delimiter { get; set; }
        public string @event {get;set;}
        public PetScaleInfo _baseStats { get; set; } //check if these are correct?
        public string _statCache { get; set; }
    }
}
