namespace EFTHelper.Models
{
    public class Settings
    {
        #region Constructors

        public Settings()
        {
            LocationSelectorInformations = new WindowInformations();
        }

        #endregion

        #region Properties

        public WindowInformations LocationSelectorInformations { get; set; }

        #endregion
    }
}
