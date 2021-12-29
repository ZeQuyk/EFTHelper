using System.Collections.Generic;
using Tarkov_Maps.ViewModels;

namespace Tarkov_Maps.Services
{
    public interface IMapsService
    {
        IEnumerable<MapViewModel> GetMapViewModels();
    }
}
