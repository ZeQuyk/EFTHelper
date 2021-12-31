﻿using System;
using System.IO;
using System.Text.Json;
using EscapeFromTarkov.Utility.Models;

namespace EscapeFromTarkov.Utility.Services
{
    public class SettingsService
    {
        private Settings _settings;

        public SettingsService()
        {
            _settings = new Settings();
            if (!Directory.Exists(this.SettingsFolderPath))
            {
                Directory.CreateDirectory(this.SettingsFolderPath);
            }

            if (!File.Exists(this.SettingsFilePath))
            {
                Save();
            }
            else
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
        }

        public WindowInformations MapSelectorInformations
        {
            get
            {
                return _settings.MapSelectorInformations;
            }

            set
            {
                _settings.MapSelectorInformations = value;
            }
        }

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
    }
}
