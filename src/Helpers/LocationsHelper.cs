using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EscapeFromTarkov.Utility.Enums;
using EscapeFromTarkov.Utility.Models;

namespace EscapeFromTarkov.Utility.Helpers
{
    public static class LocationsHelper
    {
        public static IEnumerable<Locations> GetLocations()
        {
            return Enum.GetValues(typeof(Locations)).Cast<Locations>();
        }
    }
}
