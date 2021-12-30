using System.Threading.Tasks;
using Caliburn.Micro;
using EscapeFromTarkov.Utility.Models;
using EscapeFromTarkov.Utility.Services;
using Gma.System.MouseKeyHook;

namespace EscapeFromTarkov.Utility.ViewModels
{
    public class ShellViewModel : Screen
    {
        private Screen _screen;
        private IWindowManager _windowManager;
        private ProcessService _processService;
        private IKeyboardMouseEvents _globalHook;

        public ShellViewModel(MapSelectorViewModel mapSelectorViewModel, IWindowManager windowManager)
        {
            _screen = mapSelectorViewModel;
            _windowManager = windowManager;
            _processService = new ProcessService("EscapeFromTarkov");
            _processService.ProcessClosed += Service_ProcessClosed;
            _ = WaitForTarkov();
        }     

        public DoubleClickCommand ShowMaps => new DoubleClickCommand(ShowMapsWindow);

        private async void ShowMapsWindow()
        {
            if (_screen.IsActive)
            {
                return;
            }

            await _windowManager.ShowWindowAsync(_screen);          
        }

        private async Task WaitForTarkov()
        {           
            var processId = await _processService.WaitForProcess();
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += _globalHook_KeyDown;
        }

        private void _globalHook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F2)
            {
                if (_screen.IsActive)
                {
                    _screen.TryCloseAsync();
                }
                else
                {
                    ShowMapsWindow();
                }
            }
        }

        private async void Service_ProcessClosed(object sender, System.EventArgs e)
        {
            await _screen.TryCloseAsync();
            _globalHook.KeyDown -= _globalHook_KeyDown;
            _globalHook?.Dispose();
            _ = WaitForTarkov();
        }
    }
}
