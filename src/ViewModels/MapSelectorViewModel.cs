using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using EscapeFromTarkov.Utility.Enums;
using EscapeFromTarkov.Utility.Helpers;
using EscapeFromTarkov.Utility.Services;

namespace EscapeFromTarkov.Utility.ViewModels
{
    public class MapSelectorViewModel : PropertyChangedBase
    {
        private IMapsService _mapsService;

        private Maps _selectedMapType;
        public Maps SelectedMapType {
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

        private MapViewModel _selectedMap;
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

        public MapSelectorViewModel(IMapsService mapsService)
        {
            _mapsService = mapsService;
            MapTypes = new ObservableCollection<Maps>(MapsHelper.GetMaps());
            SelectedMap = new MapViewModel(MapTypes.FirstOrDefault());
        }
    }
}
