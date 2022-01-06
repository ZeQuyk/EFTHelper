using System.Threading.Tasks;
using System.Windows;
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
        private SettingsService _settingsService;

        public ShellViewModel(LocationSelectorViewModel locationSelectorViewModel, IWindowManager windowManager, SettingsService settingsService)
        {
            _screen = locationSelectorViewModel;
            _windowManager = windowManager;
            _settingsService = settingsService;
            _processService = new ProcessService("EscapeFromTarkov");
            _processService.ProcessClosed += Service_ProcessClosed;
            _ = WaitForTarkov();
        }

        public DoubleClickCommand ShowLocations => new DoubleClickCommand(ShowLocationsWindow);

        private async void ShowLocationsWindow()
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
                    ShowLocationsWindow();
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
