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
        public static List<Map> GetMaps()
        {
            return Enum.GetValues(typeof(Maps)).Cast<Maps>().Select(x => new Map { Name = x.ToString()}).ToList();
        }
    }
}
