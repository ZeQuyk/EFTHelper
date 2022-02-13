using System;
using System.IO;
using AppDataFileManager;
using EFTHelper.Enums;
using EFTHelper.Models;

namespace EFTHelper.Services
{
    public class SettingsService: AppDataFileBase<Settings>
    {
        #region Events

        public event EventHandler OnSaved;

        #endregion

        #region Properties

        public WindowInformations WindowInformation
        {
            get
            {
                return Entity.WindowInformation;
            }

            set
            {
                Entity.WindowInformation = value;
            }
        }

        public Theme Theme
        {
            get
            {
                return Entity.Theme;
            }

            set
            {
                Entity.Theme = value;
            }
        }

        public Scheme Scheme
        {
            get
            {
                return Entity.Scheme;
            }

            set
            {
                Entity.Scheme = value;
            }
        }

        public bool TopMost
        {
            get
            {
                return Entity.TopMost;
            }

            set
            {
                Entity.TopMost = value;
            }
        }

        protected override string FileName => "Settings.json";

        protected override string FolderName => "EFTHelper";

        #endregion

        #region Methods

        public new void Save()
        {
            base.Save();
            OnSaved?.Invoke(this, EventArgs.Empty);
        }

        private string AppDataFolderPath => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private string SettingsFolderPath => Path.Combine(this.AppDataFolderPath, this.FolderName);

        private string SettingsFilePath => Path.Combine(this.SettingsFolderPath, this.FileName);

        #endregion
    }
}