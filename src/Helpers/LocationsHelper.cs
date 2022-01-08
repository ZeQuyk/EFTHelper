using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFTHelper.Enums;
using EFTHelper.Models;

namespace EFTHelper.Helpers
{
    public static class LocationsHelper
    {
        public static IEnumerable<Locations> GetLocations()
        {
            return Enum.GetValues(typeof(Locations)).Cast<Locations>();
        }
    }
}
