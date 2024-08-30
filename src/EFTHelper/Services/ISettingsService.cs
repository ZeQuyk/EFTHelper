using System;
using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Models;

namespace EFTHelper.Services;

public interface ISettingsService
{
    event EventHandler<Settings> OnFileSaved;

    void SetWindowInformation(WindowInformations windowInformations);

    void SetWindowInformation(IViewAware viewAware);

    WindowInformations GetWindowInformation();

    void SetTheme(Theme theme);

    Theme GetTheme();

    void SetScheme(Scheme scheme);

    Scheme GetScheme();

    void SetOpacity(int opacity);

    int GetOpacity();

    void Save();
}
