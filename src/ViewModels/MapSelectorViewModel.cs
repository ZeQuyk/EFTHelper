using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Caliburn.Micro;
using EscapeFromTarkov.Utility.Enums;
using EscapeFromTarkov.Utility.Helpers;
using EscapeFromTarkov.Utility.Services;

namespace EscapeFromTarkov.Utility.ViewModels
{
    public class MapSelectorViewModel : Screen, IViewAware
    {
        private Maps _selectedMapType;
        private IMapsService _mapsService;
        private SettingsService _settingsService;
        private MapViewModel _selectedMap;

        public MapSelectorViewModel(IMapsService mapsService, SettingsService settingsService)
        {
            _mapsService = mapsService;
            _settingsService = settingsService;

            MapTypes = new ObservableCollection<Maps>(MapsHelper.GetMaps());
            SelectedMap = new MapViewModel(MapTypes.FirstOrDefault());
            DisplayName = "EFT Utility";
        }

        public Maps SelectedMapType 
        {
            get => _selectedMapType;

            set 
            {
                if (_selectedMapType != value)
                {
                    _selectedMapType = value;
                    SelectedMap = new MapViewModel(_selectedMapType);
                    NotifyOfPropertyChange(() => SelectedMapType);
                }
            }
        }

        public MapViewModel SelectedMap 
        { 
            get => _selectedMap;
            set 
            { 
                if (_selectedMap != value)
                {
                    _selectedMap = value;
                    NotifyOfPropertyChange(() => SelectedMap);
                }
            }
        }

        public ObservableCollection<Maps> MapTypes { get; set; }

        protected override void OnViewLoaded(object view)
        {
            var window = view as Window;
            var informations = _settingsService.MapSelectorInformations;

            // Todo: Check if out off screen
            Execute.OnUIThread(() =>
            {
                window.Width = informations.Width;
                window.Height = informations.Height;
                window.Left = informations.Position.Left;
                window.Top = informations.Position.Top;
            });
        }
    }
}
