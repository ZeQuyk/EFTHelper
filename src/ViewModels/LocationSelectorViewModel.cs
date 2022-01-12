using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using EFTHelper.Helpers;
using EFTHelper.Services;
using MahApps.Metro.Controls;

namespace EFTHelper.ViewModels
{
    public class LocationSelectorViewModel : Screen, IViewAware
    {
        private bool _needUpdate;
        private SettingsService _settingsService;
        private LocationViewModel _selectedLocation;
        private string _selectedLocationName;
        private UpdateManagerService _updateManagerService;

        public LocationSelectorViewModel(SettingsService settingsService, UpdateManagerService updateManagerService, VersionViewModel versionViewModel)
        {
            VersionViewModel = versionViewModel;
            _updateManagerService = updateManagerService;
            _settingsService = settingsService;
            LocationViewModels = LocationsHelper.GetLocations().Select(x => new LocationViewModel(x)).OrderBy(x => x.Name).ToList();
            SelectedLocationName = LocationViewModels.FirstOrDefault().Name;
            LocationNames = new ObservableCollection<string>(LocationViewModels.Select(x => x.Name));
            DisplayName = "EFTHelper";
        }

        public string SelectedLocationName 
        {
            get => _selectedLocationName;
            set
            {
                _selectedLocationName = value;
                SelectedLocation = LocationViewModels.FirstOrDefault(x => x.Name == value);
            }
        }

        public LocationViewModel SelectedLocation 
        { 
            get => _selectedLocation;
            set 
            { 
                if (_selectedLocation != value)
                {
                    _selectedLocation = value;
                    NotifyOfPropertyChange(() => SelectedLocation);
                }
            }
        }

        public List<LocationViewModel> LocationViewModels { get; set; }

        public ObservableCollection<string> LocationNames { get; set; }        

        public VersionViewModel VersionViewModel { get; set; }

        public void MenuSelectionChanged(object value, ItemClickEventArgs args)
        {
            var location = args.ClickedItem as LocationViewModel;
            if (location != null)
            {
                SelectedLocation = location;
            }
        }

        protected override async void OnViewLoaded(object view)
        {            
            var window = view as Window;
            var informations = _settingsService.LocationSelectorInformations;

            // Todo: Check if out off screen
            Execute.OnUIThread(() =>
            {
                window.Width = informations.Width;
                window.Height = informations.Height;
                window.Left = informations.Position.Left;
                window.Top = informations.Position.Top;
            });
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            var window = GetView() as Window;
            _settingsService.LocationSelectorInformations.Copy(window);
            _settingsService.Save();
            return base.OnDeactivateAsync(close, cancellationToken);
        }     
    }
}
