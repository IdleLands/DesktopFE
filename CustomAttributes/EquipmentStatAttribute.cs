using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.CustomAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    class EquipmentStatAttribute : System.Attribute
    {
        public string BelongsTo { get; set; }
        public bool IsPercent { get; set; }
    }
}
