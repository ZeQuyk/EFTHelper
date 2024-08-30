using EFTHelper.Enums;

namespace EFTHelper.Models;

public class Settings
{
    #region Constructors

    public Settings()
    {
        WindowInformation = new WindowInformations();
        Theme = Theme.Dark;
        Scheme = Scheme.Amber;
        Opacity = 100;
    }

    #endregion

    #region Properties

    public WindowInformations WindowInformation { get; set; }

    public Theme Theme { get; set; }

    public Scheme Scheme { get; set; }

    public int Opacity { get; set; }

    #endregion
}
