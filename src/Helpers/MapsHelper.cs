using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tarkov_Maps.Enums;
using Tarkov_Maps.Models;

namespace Tarkov_Maps.Helpers
{
    public static class MapsHelper
    {
        public static List<Map> GetMaps()
        {
            return Enum.GetValues(typeof(Maps)).Cast<Maps>().Select(x => new Map { Name = x.ToString()}).ToList();
        }
    }
}
