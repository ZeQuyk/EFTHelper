namespace EscapeFromTarkov.Utility.Models
{
    public class Settings
    {
        public Settings()
        {
            MapSelectorInformations = new WindowInformations();
        }

        public WindowInformations MapSelectorInformations { get; set; }
    }
}
