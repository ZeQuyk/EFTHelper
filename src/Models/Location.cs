using EscapeFromTarkov.Utility.Enums;
using EscapeFromTarkov.Utility.Extensions;

namespace EscapeFromTarkov.Utility.Models
{
    public class Location
    {
        public string Name { get; set; }

        public Locations LocationType { get; set; }

        public Location(Locations location)
        {
            Name = location.ToString().ToSentence();
            LocationType = location;
        }
    }
}