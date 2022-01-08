using EFTHelper.Enums;
using EFTHelper.Extensions;

namespace EFTHelper.Models
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