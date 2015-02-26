using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model.Map
{
    public class SubMapInfo
    {
        public int height { get; set; }
        public int width { get; set; }
        public int tileheight { get; set; }
        public int tilewidth { get; set; }
        public List<LayerInfo> layers { get; set; }
        public string orientation { get; set; }
        public Dictionary<string, string> properties { get; set; }
        //public string tilesets { get; set; }
        //public List<TilesetInfo> tilesetsWorkaround { get; set; }
        public List<TilesetInfo> tilesets { get; set; }
    }
}
