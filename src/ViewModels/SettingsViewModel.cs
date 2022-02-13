using System;
using System.Collections.Generic;
using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Services;

namespace EFTHelper.ViewModels
{
    public class SettingsViewModel : PropertyChangedBase
    {
        #region Fields

        private SettingsService _settingsService;
        private ThemeService _themeService;
        private Theme _selectedTheme;
        private Scheme _selectedScheme;
        private bool _topMost;

        #endregion

        #region Constructors

        public SettingsViewModel(SettingsService settingsService, ThemeService themeService)
        {
            _settingsService = settingsService;
            _themeService = themeService;
            _selectedScheme = _themeService.Scheme;
            _selectedTheme = _themeService.Theme;
            _topMost = _settingsService.TopMost;
            PropertyChanged += SettingsViewModel_PropertyChanged;
        }

        #endregion

        #region Properties

        public IEnumerable<Theme> Themes => _themeService.GetThemes();

        public IEnumerable<Scheme> Schemes => _themeService.GetSchemes();

        public Theme SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                _selectedTheme = value;
                NotifyOfPropertyChange();
            }
        }

        public Scheme SelectedScheme
        {
            get => _selectedScheme;
            set
            {
                _selectedScheme = value;
                NotifyOfPropertyChange();
            }
        }

        public bool TopMost
        {
            get => _topMost;
            set
            {
                _topMost = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion

        #region Methods

        private void SettingsViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _settingsService.TopMost = TopMost;
            _themeService.Change(SelectedTheme, SelectedScheme);
        }

        #endregion
    }
}
