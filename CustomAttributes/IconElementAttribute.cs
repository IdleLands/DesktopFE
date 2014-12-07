using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class IconElementAttribute : System.Attribute
    {
        public string Name { get; set; }
        public string Colour { get; set; }
        public bool HideMax { get; set; }

        public IconElementAttribute()
        {
            Colour = "Black";
        }
    }
}
