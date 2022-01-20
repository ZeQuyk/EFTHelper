using System.Linq;
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
        private ItemsListViewModel _itemsListViewModel;
        private System.Windows.Forms.Keys[] _hotkeys = { System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.F3 };

        #endregion

        #region Constructors

        public ShellViewModel(
            LocationSelectorViewModel locationSelectorViewModel,
            IWindowManager windowManager,
            UpdateManagerService updateManagerService,
            VersionViewModel versionViewModel,
            ItemsListViewModel itemsListViewModel)
        {
            _locationSelectorViewModel = locationSelectorViewModel;
            _versionViewModel = versionViewModel;
            _windowManager = windowManager;
            _updateManagerService = updateManagerService;
            _itemsListViewModel = itemsListViewModel;
            DisplayName = "EFTHelper";
            ChangeActiveItemAsync(_locationSelectorViewModel, true, CancellationToken.None);
            _processService = new ProcessService("EscapeFromTarkov");
            _processService.ProcessClosed += Service_ProcessClosed;
            _ = WaitForTarkov();
        }

        #endregion

        #region Properties

        public DoubleClickCommand ShowActiveScreen => new DoubleClickCommand(ShowActiveItem);

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

        public async void ShowActiveItem()
        {
            await ShowItem(ActiveItem);
        }

        public async void ShowLocationsView()
        {
            await ShowItem(_locationSelectorViewModel);
        }

        public async void ShowItemsView()
        {
            await ShowItem(_itemsListViewModel);
        }

        private async Task ShowItem(Screen item)
        {
            ActiveItem = item ?? _locationSelectorViewModel;

            await _windowManager.ShowWindowAsync(ActiveItem);
        }

        private void ShowItem(System.Windows.Forms.Keys key)
        {
            switch (key)
            {
                case System.Windows.Forms.Keys.F2:
                    ShowLocationsView();
                    break;
                case System.Windows.Forms.Keys.F3:
                    ShowItemsView();
                    break;
            }
        }

        private async Task HandleKeyboard(System.Windows.Forms.Keys key)
        {
            if (_hotkeys.Contains(key))
            {
                if (ActiveItem != null)
                {
                    await ActiveItem.TryCloseAsync();
                }
                else
                {
                    ShowItem(key);
                }
            }
        }

        private async Task WaitForTarkov()
        {
            var processId = await _processService.WaitForProcess();
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += _globalHook_KeyDown;
        }

        private async void _globalHook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            await HandleKeyboard(e.KeyCode);
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
