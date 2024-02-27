using Caliburn.Micro;
using EFTHelper.Models;

namespace EFTHelper.ViewModels;

public abstract class ScreenBase : PropertyChangedBase
{
    public abstract HamburgerMenuInformation GetHamburgerMenuInformation();

    public abstract void MenuSelectionChanged(IMenuItem item);
}