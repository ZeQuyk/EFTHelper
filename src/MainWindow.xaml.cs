using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using EscapeFromTarkov.Utility.Services;
using EscapeFromTarkov.Utility.ViewModels;

namespace EscapeFromTarkov.Utility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        private IMapsService _mapService;

        private ObservableCollection<MapViewModel> _mapViewModels;
        public ObservableCollection<MapViewModel> MapViewModels
        {
            get
            {
                return _mapViewModels;
            }

            set
            {
                _mapViewModels = value;
                OnPropertyChanged(nameof(MapViewModels));
            }
        }

        private MapViewModel _selectedMap;
        public MapViewModel SelectedMap
        {
            get 
            { 
                return _selectedMap;
            }
            set
            {
                _selectedMap = value;
                MapImageSource = _selectedMap.ImagePath;
                OnPropertyChanged(nameof(SelectedMap));
            }           
        }

        private string _mapImageSource;
        public string MapImageSource 
        { 
            get
            {
                return _mapImageSource;
            } 
            set
            {
                _mapImageSource = value;               
                OnPropertyChanged(nameof(MapImageSource));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow(IMapsService mapService)
        {    
            DataContext = this;
            _mapService = mapService;
            LoadViewModels();            
            InitializeComponent();           
        }

        private void LoadViewModels()
        {
            MapViewModels = new ObservableCollection<MapViewModel>(_mapService.GetMapViewModels());
            SelectedMap = MapViewModels.FirstOrDefault();         
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
