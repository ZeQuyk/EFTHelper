using System.Collections.Generic;
using System.Linq;
using EscapeFromTarkov.Utility.Helpers;
using EscapeFromTarkov.Utility.Models;
using EscapeFromTarkov.Utility.ViewModels;

namespace EscapeFromTarkov.Utility.Services
{
    public class MapsService : IMapsService
    {

        public MapsService()
        {

        }

        public IEnumerable<MapViewModel> GetMapViewModels()
        {
            var maps = MapsHelper.GetMaps().Select(x => new Map(x));
            if (maps != null)
            {
                return GetMapViewModels(maps);
            }

            return Enumerable.Empty<MapViewModel>();
        }
      
        private IEnumerable<MapViewModel> GetMapViewModels(IEnumerable<Map> maps)
        {
            return maps.OrderBy(m => m.Name).Select(x => new MapViewModel(x.MapType));
        }
    }
}
