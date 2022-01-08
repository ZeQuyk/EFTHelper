namespace EFTHelper.Models
{
    public class Settings
    {
        public Settings()
        {
            LocationSelectorInformations = new WindowInformations();
        }

        public WindowInformations LocationSelectorInformations { get; set; }
    }
}
