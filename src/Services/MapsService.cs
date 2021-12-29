using System.Collections.Generic;
using System.Linq;
using EscapeFromTarkov.Utility.Helpers;
using EscapeFromTarkov.Utility.Models;
using EscapeFromTarkov.Utility.ViewModels;

namespace EscapeFromTarkov.Utility.Services
{
    public class MapsService : IMapsService
    {
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
