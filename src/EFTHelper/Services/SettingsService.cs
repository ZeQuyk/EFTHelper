using EFTHelper.Enums;
using EFTHelper.Models;
using Lurker.AppData;

namespace EFTHelper.Services;

public class SettingsService : AppDataFileBase<Settings>
{
    #region Constants

    public const int OPACITY_MIN = 20;
    public const int OPACITY_MAX = 100;

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
}