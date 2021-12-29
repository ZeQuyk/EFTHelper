using Caliburn.Micro;

namespace EscapeFromTarkov.Utility.ViewModels
{
    public class ShellViewModel : Screen
    {
        public PropertyChangedBase SelectedViewModel { get; set; }
        
        public ShellViewModel(MapSelectorViewModel mapSelectorViewModel)
        {
            SelectedViewModel = mapSelectorViewModel;
        }
    }
}
