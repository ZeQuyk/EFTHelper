using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tarkov_Maps.Services;
using Tarkov_Maps.ViewModels;

namespace Tarkov_Maps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        private MapService mapService;
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

        public MainWindow()
        {   
            DataContext = this;
            mapService = new MapService();
            LoadViewModels();            
            InitializeComponent();           
        }

        private void LoadViewModels()
        {
            MapViewModels = new ObservableCollection<MapViewModel>(mapService.GetMapViewModels());
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
