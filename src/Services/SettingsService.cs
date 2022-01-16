using System;
using System.IO;
using System.Text.Json;
using EFTHelper.Models;

namespace EFTHelper.Services
{
    public class SettingsService
    {
        #region Fields

        private Settings _settings;

        #endregion

        #region Constructors

        public SettingsService()
        {
            _settings = new Settings();
            if (!Directory.Exists(this.SettingsFolderPath))
            {
                Directory.CreateDirectory(this.SettingsFolderPath);
            }

            if (File.Exists(this.SettingsFilePath))
            {
                try
                {
                    _settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(this.SettingsFilePath));
                }
                catch
                {
                    File.Delete(this.SettingsFilePath);
                    _settings = new Settings();
                    Save();
                }
            }
            else
            {
                // Create File with default values
                Save();
            }
        }

        #endregion

        #region Properties

        public WindowInformations LocationSelectorInformations
        {
            get
            {
                return _settings.LocationSelectorInformations;
            }

            set
            {
                _settings.LocationSelectorInformations = value;
            }
        }

        #endregion

        #region Methods

        private string FolderName => "TarkovUtility";

        private string FileName => "Settings.json";

        private string AppDataFolderPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private string SettingsFolderPath => Path.Combine(this.AppDataFolderPath, this.FolderName);

        private string SettingsFilePath => Path.Combine(this.SettingsFolderPath, this.FileName);

        public void Save()
        {
            var jsonValue = JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(SettingsFilePath, jsonValue);
        }

        #endregion
    }
}