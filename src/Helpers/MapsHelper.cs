using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EscapeFromTarkov.Utility.Enums;
using EscapeFromTarkov.Utility.Models;

namespace EscapeFromTarkov.Utility.Helpers
{
    public static class MapsHelper
    {
        public static IEnumerable<Maps> GetMaps()
        {
            return Enum.GetValues(typeof(Maps)).Cast<Maps>();
        }
    }
}
