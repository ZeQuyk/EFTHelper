using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tarkov_Maps.Helpers;
using Tarkov_Maps.Models;
using Tarkov_Maps.ViewModels;

namespace Tarkov_Maps.Services
{
    public class MapsService : IMapsService
    {
        private const string FilePath = "Resources/Maps.json";
        public IEnumerable<MapViewModel> GetMapViewModels()
        {
            var maps = MapsHelper.GetMaps();
            if (maps != null)
            {
                return GetMapViewModels(maps);
            }

            return Enumerable.Empty<MapViewModel>();
        }

        private List<MapViewModel> GetMapViewModels(List<Map> maps)
        {        
            var viewModels = maps.Select(x => new MapViewModel { Name = x.Name}).ToList();
            viewModels.Sort((x, y) => x.Name.CompareTo(y.Name));
            return viewModels;
        }
    }
}
