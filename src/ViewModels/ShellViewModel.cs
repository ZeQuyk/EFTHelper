using System.Reflection;
using System.Threading.Tasks;
using Caliburn.Micro;
using EFTHelper.Models;
using EFTHelper.Services;
using Gma.System.MouseKeyHook;

namespace EFTHelper.ViewModels
{
    public class ShellViewModel : Screen
    {
        private Screen _screen;
        private IWindowManager _windowManager;
        private ProcessService _processService;
        private IKeyboardMouseEvents _globalHook;
        private UpdateManagerService _updateManagerService;

        public ShellViewModel(LocationSelectorViewModel locationSelectorViewModel, IWindowManager windowManager, UpdateManagerService updateManagerService)
        {
            _screen = locationSelectorViewModel;
            _windowManager = windowManager;
            _updateManagerService = updateManagerService;
            _processService = new ProcessService("EscapeFromTarkov");
            _processService.ProcessClosed += Service_ProcessClosed;
            _ = WaitForTarkov();
        }

        public DoubleClickCommand ShowLocations => new DoubleClickCommand(ShowLocationsWindow);

        public string Version
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return $"Version {version.Major}.{version.Minor}.{version.Build}";
            }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public async void Close()
        {
            await TryCloseAsync();
        }

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
