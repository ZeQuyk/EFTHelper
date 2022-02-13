using EFTHelper.Enums;

namespace EFTHelper.Models
{
    public class Settings
    {
        #region Constructors

        public Settings()
        {
            WindowInformation = new WindowInformations();
            Theme = Theme.Dark;
            Scheme = Scheme.Amber;
            TopMost = true;
        }

        #endregion

        #region Properties

        public WindowInformations WindowInformation { get; set; }

        public Theme Theme { get; set; }

        public Scheme Scheme { get; set; }

        public bool TopMost { get; set; }

        #endregion
    }
}
