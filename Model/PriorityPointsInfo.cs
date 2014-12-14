using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model
{
    public class PriorityPointsInfo
    {
        public int str { get; set; }
        public int con { get; set; }
        public int dex { get; set; }
        [DeserializeAs(Name = "int")]
        public int _int { get; set; }
        public int wis { get; set; }
        public int agi { get; set; }
    }
}
