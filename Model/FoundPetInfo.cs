using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model
{
    public class FoundPetInfo
    {
        public string name { get; set; }
        public int cost { get; set; }
        public string purchaseDate { get; set; }
        public long unlockDate { get; set; }
        public ulong uid { get; set; }
    }
}
