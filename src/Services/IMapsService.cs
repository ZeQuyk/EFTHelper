using System.Collections.Generic;
using EscapeFromTarkov.Utility.ViewModels;

namespace EscapeFromTarkov.Utility.Services
{
    public interface IMapsService
    {
        IEnumerable<MapViewModel> GetMapViewModels();
    }
}
