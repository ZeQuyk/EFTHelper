using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using EFTHelper.Models;
using EFTHelper.Services;
using Gma.System.MouseKeyHook;

namespace EFTHelper.ViewModels
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        #region Fields

        private LocationSelectorViewModel _locationSelectorViewModel;
        private VersionViewModel _versionViewModel;
        private IWindowManager _windowManager;
        private ProcessService _processService;
        private IKeyboardMouseEvents _globalHook;
        private UpdateManagerService _updateManagerService;

        #endregion

        #region Constructors

        public ShellViewModel(LocationSelectorViewModel locationSelectorViewModel, IWindowManager windowManager, UpdateManagerService updateManagerService, VersionViewModel versionViewModel)
        {
            _locationSelectorViewModel = locationSelectorViewModel;
            _versionViewModel = versionViewModel;
            _windowManager = windowManager;
            _updateManagerService = updateManagerService;
            DisplayName = "EFTHelper";
            ChangeActiveItemAsync(_locationSelectorViewModel, true, CancellationToken.None);
            _processService = new ProcessService("EscapeFromTarkov");
            _processService.ProcessClosed += Service_ProcessClosed;
            _ = WaitForTarkov();
        }

        #endregion

        #region Properties

        public DoubleClickCommand ShowLocations => new DoubleClickCommand(ShowActiveItem);

        public string Version
        {
            get
            {
                var version = _updateManagerService.GetVersion().ToString(3);
                return $"{version}";
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public async void Close()
        {
            if (ActiveItem != null)
            {
                await ActiveItem.TryCloseAsync();
            }

            await TryCloseAsync();
        }

        private async void ShowActiveItem()
        {
            if (ActiveItem == null)
            {
                ActiveItem = _locationSelectorViewModel;
            }

            await _windowManager.ShowWindowAsync(ActiveItem);
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
                if (ActiveItem != null)
                {
                    ActiveItem.TryCloseAsync();
                }
                else
                {
                    ShowActiveItem();
                }
            }
        }

        private async void Service_ProcessClosed(object sender, System.EventArgs e)
        {
            await ActiveItem.TryCloseAsync();
            _globalHook.KeyDown -= _globalHook_KeyDown;
            _globalHook?.Dispose();
            _ = WaitForTarkov();
        }

        #endregion
    }
}
