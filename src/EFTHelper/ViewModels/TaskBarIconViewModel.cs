using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using EFTHelper.Models;
using EFTHelper.Services;
using Gma.System.MouseKeyHook;

namespace EFTHelper.ViewModels;

public class TaskBarIconViewModel : Conductor<ScreenBase>.Collection.OneActive
{
    #region Fields

    private readonly ShellViewModel _shellViewModel;
    private readonly LocationSelectorViewModel _locationSelectorViewModel;
    private readonly VersionViewModel _versionViewModel;
    private readonly IWindowManager _windowManager;
    private readonly ProcessService _processService;
    private readonly IUpdateManagerService _updateManagerService;
    private readonly ItemsListViewModel _itemsListViewModel;
    private readonly System.Windows.Forms.Keys[] _hotkeys = { System.Windows.Forms.Keys.F2, System.Windows.Forms.Keys.F3 };
    private IKeyboardMouseEvents _globalHook;

    #endregion

    #region Constructors

    public TaskBarIconViewModel(
        ShellViewModel shellViewModel,
        LocationSelectorViewModel locationSelectorViewModel,
        IWindowManager windowManager,
        IUpdateManagerService updateManagerService,
        VersionViewModel versionViewModel,
        ItemsListViewModel itemsListViewModel,
        ThemeService themeService)
    {
        _shellViewModel = shellViewModel;
        _locationSelectorViewModel = locationSelectorViewModel;
        _versionViewModel = versionViewModel;
        _windowManager = windowManager;
        _updateManagerService = updateManagerService;
        _itemsListViewModel = itemsListViewModel;
        DisplayName = "EFTHelper";
        themeService.Apply();
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
        if (ActiveItem is not null)
        {
            await _shellViewModel.TryCloseAsync();
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

    private async Task ShowItem(ScreenBase item)
    {
        ActiveItem = item ?? _locationSelectorViewModel;

        if (_shellViewModel.IsActive && _shellViewModel.Content == ActiveItem)
        {
            await _shellViewModel.TryCloseAsync();
        }
        else
        {
            _shellViewModel.SetContent(ActiveItem);

            if (!_shellViewModel.IsActive)
            {
                await _windowManager.ShowWindowAsync(_shellViewModel);
            }
        }
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

    private void HandleKeyboard(System.Windows.Forms.Keys key, System.Windows.Forms.Keys modifiers)
    {
        if (_hotkeys.Contains(key))
        {
            ShowItem(key);
        }

        _shellViewModel.HandleKeyboard(key, modifiers);
    }

    private async Task WaitForTarkov()
    {
        var processId = await _processService.WaitForProcess();
        _globalHook = Hook.GlobalEvents();
        _globalHook.KeyDown += _globalHook_KeyDown;
    }

    private void _globalHook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        => HandleKeyboard(e.KeyCode, e.Modifiers);

    private void Service_ProcessClosed(object sender, System.EventArgs e)
    {
        _globalHook.KeyDown -= _globalHook_KeyDown;
        _globalHook?.Dispose();
        _ = WaitForTarkov();
    }

    #endregion
}
