using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdleLandsGUI.Model.Map
{
    public class MapInfo
    {
        public SubMapInfo map { get; set; }
        //public string map { get; set; }
        public int tileHeight { get; set; }
        public int tileWidth { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public string name { get; set; }
        public List<string> regionMap { get; set; }
        public Dictionary<string, string> gidMap { get; set; }
        public List<int> blockers { get; set; }
        public List<int> interactables { get; set; }
    }
}
