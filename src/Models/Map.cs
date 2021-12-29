using EscapeFromTarkov.Utility.Enums;

namespace EscapeFromTarkov.Utility.Models
{
    public class Map
    {
        public string Name { get; set; }

        public Maps MapType { get; set; }

        public Map(Maps map)
        {
            Name = map.ToString();
            MapType = map;
        }
    }
}
