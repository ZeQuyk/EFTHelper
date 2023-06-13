using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EFTHelper.Enums;
using EFTHelper.Helpers;
using EFTHelper.Models;
using EFTHelper.Services;

namespace EFTHelper.ViewModels;

public class LocationSelectorViewModel : ScreenBase
{
    #region Fields

    private SettingsService _settingsService;
    private LocationViewModel _selectedLocation;

    #endregion

    #region Constructors

    public LocationSelectorViewModel(
        SettingsService settingsService,
        SettingsViewModel settingsViewModel)
    {
        SettingsViewModel = settingsViewModel;
        _settingsService = settingsService;
        LocationViewModels = EnumHelper.GetEnumValues<Locations>().Select(x => new LocationViewModel(x)).OrderBy(x => x.Name).ToList();
        SelectedLocation = LocationViewModels.FirstOrDefault();
        LocationNames = new ObservableCollection<string>(LocationViewModels.Select(x => x.Name));
    }

    #endregion

    #region Properties

    public string SelectedLocationName => SelectedLocation.Name;

    public bool TopMost => _settingsService.TopMost;

    public LocationViewModel SelectedLocation
    {
        get => _selectedLocation;
        set
        {
            if (_selectedLocation != value)
            {
                _selectedLocation = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => SelectedLocationName);
            }
        }
    }

    public List<LocationViewModel> LocationViewModels { get; set; }

    public ObservableCollection<string> LocationNames { get; set; }

    public SettingsViewModel SettingsViewModel { get; set; }

    #endregion

    #region Methods

    public override HamburgerMenuInformation GetHamburgerMenuInformation()
    {
        return new HamburgerMenuInformation
        {
            Items = LocationViewModels.Cast<IMenuItem>(),
            Header = "Locations",
            SelectedItem = SelectedLocation,
        };
    }

    public override void MenuSelectionChanged(IMenuItem item)
    {
        var location = item as LocationViewModel;
        if (location != null)
        {
            SelectedLocation = location;
        }
    }

    #endregion
}
