using System;
using System.IO;
using AppDataFileManager;
using EFTHelper.Enums;
using EFTHelper.Models;

namespace EFTHelper.Services;

public class SettingsService : AppDataFileBase<Settings>
{
    #region Constants

    public const int OPACITY_MIN = 20;
    public const int OPACITY_MAX = 100;

    #endregion

    #region Events

    public event EventHandler OnSaved;

    #endregion

    #region Properties

    public WindowInformations WindowInformation
    {
        get => Entity.WindowInformation;
        set
        {
            Entity.WindowInformation = value;
        }
    }

    public Theme Theme
    {
        get => Entity.Theme;
        set
        {
            Entity.Theme = value;
        }
    }

    public Scheme Scheme
    {
        get => Entity.Scheme;
        set
        {
            Entity.Scheme = value;
        }
    }

    public bool TopMost
    {
        get => Entity.TopMost;
        set
        {
            Entity.TopMost = value;
        }
    }

    public int Opacity
    {
        get => Entity.Opacity;
        set
        {
            Entity.Opacity = value;
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