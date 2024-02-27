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
        TopMost = true;
        Opacity = 100;
    }

    #endregion

    #region Properties

    public WindowInformations WindowInformation { get; set; }

    public Theme Theme { get; set; }

    public Scheme Scheme { get; set; }

    public bool TopMost { get; set; }

    public int Opacity { get; set; }

    #endregion
}
