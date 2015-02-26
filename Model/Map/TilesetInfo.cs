using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model.Map
{
    public class TilesetInfo
    {
        public int firstgid { get; set; }
        public string image { get; set; }
        public int imageheight { get; set; }
        public int imagewidth { get; set; }
        public int margin { get; set; }
        public string name { get; set; }
        public Dictionary<string, string> properties { get; set; }
        public int spacing { get; set; }
        public int tilewidth { get; set; }
        public int tileheight { get; set; }
    }
}
