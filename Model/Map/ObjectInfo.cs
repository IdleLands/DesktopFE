using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model.Map
{
    public class ObjectInfo
    {
        public int gid { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public bool visible { get; set; }
        public string name { get; set; }
        public Dictionary<string, string> properties { get; set; }
        public int rotation { get; set; }
        public string type { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
