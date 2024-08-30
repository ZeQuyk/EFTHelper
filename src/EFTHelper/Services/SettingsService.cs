using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Models;
using Lurker.AppData;

namespace EFTHelper.Services;

public class SettingsService : AppDataFileBase<Settings>, ISettingsService
{
    #region Constants

    public const int OPACITY_MIN = 20;
    public const int OPACITY_MAX = 100;

    #endregion

    #region Constructors

    public SettingsService()
    {
        Initialize();
    }

    #endregion

    #region Properties

    protected override string FileName => "Settings.json";

    protected override string FolderName => "EFTHelper";

    #endregion

    #region Methods

    public int GetOpacity()
        => Entity.Opacity;

    public Theme GetTheme()
        => Entity.Theme;

    public WindowInformations GetWindowInformation()
        => Entity.WindowInformation;

    public void SetOpacity(int opacity)
        => Entity.Opacity = opacity;

    public void SetScheme(Scheme scheme)
        => Entity.Scheme = scheme;

    public Scheme GetScheme()
        => Entity.Scheme;

    public void SetTheme(Theme theme)
        => Entity.Theme = theme;

    public void SetWindowInformation(WindowInformations windowInformations)
        => Entity.WindowInformation = windowInformations;

    public void SetWindowInformation(IViewAware viewAware)
    {
        var windowInformation = GetWindowInformation();
        windowInformation.Copy(viewAware);

        SetWindowInformation(windowInformation);
    }

    #endregion
}