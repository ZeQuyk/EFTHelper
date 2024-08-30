using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Helpers;
using EFTHelper.Models;
using EFTHelper.Services;
using MahApps.Metro.Controls;

namespace EFTHelper.ViewModels;

public class ShellViewModel : Screen
{
    #region Fields

    private string _USDollarValue;
    private string _EuroValue;
    private bool _isFlyoutOpen;
    private bool _isBusy;
    private ScreenBase _content;
    private PropertyChangedBase _flyoutContent;
    private string _flyoutHeader;
    private Position _flyoutPosition;
    private VersionViewModel _versionViewModel;
    private ObservableCollection<IMenuItem> _items;
    private ObservableCollection<IMenuItem> _optionItems;
    private IMenuItem _selectedItem;
    private readonly SettingsService _settingsService;
    private readonly FlyoutService _flyoutService;
    private readonly DialogService _dialogService;
    private readonly IUpdateManagerService _updateManagerService;
    private double _opacity;

    #endregion

    #region Constructors

    public ShellViewModel(
        SettingsService settingsService,
        VersionViewModel versionViewModel,
        FlyoutService flyoutService,
        SettingMenuItem settingMenuItem,
        DialogService dialogService,
        IUpdateManagerService updateManagerService)
    {
        _settingsService = settingsService;
        _settingsService.OnFileSaved += SettingsService_OnSaved;
        _flyoutService = flyoutService;
        VersionViewModel = versionViewModel;
        Items = new ObservableCollection<IMenuItem>();
        DisplayName = string.Empty;
        OptionItems = new ObservableCollection<IMenuItem>
        {
            settingMenuItem
        };
        _dialogService = dialogService;
        _updateManagerService = updateManagerService;
        _dialogService.Register(this);
        Opacity = 1;
    }

    #endregion

    #region Properties

    public bool IsFlyoutOpen
    {
        get => _isFlyoutOpen;
        set
        {
            _isFlyoutOpen = value;
            NotifyOfPropertyChange();
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            NotifyOfPropertyChange();
        }
    }

    public bool IsTopMost => _settingsService.TopMost;

    public ScreenBase Content
    {
        get => _content;
        set
        {
            if (_content != value && value is not null)
            {
                _content = value;
                NotifyOfPropertyChange();
            }
        }
    }

    public PropertyChangedBase FlyoutContent
    {
        get => _flyoutContent;
        set
        {
            if (_flyoutContent != value && value is not null)
            {
                _flyoutContent = value;
                NotifyOfPropertyChange();
            }
        }
    }

    public string FlyoutHeader
    {
        get => _flyoutHeader;
        set
        {
            _flyoutHeader = value;
            NotifyOfPropertyChange();
        }
    }

    public Position FlyoutPosition
    {
        get => _flyoutPosition;
        set
        {
            _flyoutPosition = value;
            NotifyOfPropertyChange();
        }
    }

    public VersionViewModel VersionViewModel
    {
        get => _versionViewModel;
        set
        {
            _versionViewModel = value;
            NotifyOfPropertyChange();
        }
    }

    public ObservableCollection<IMenuItem> Items
    {
        get => _items;

        set
        {
            _items = value;
            NotifyOfPropertyChange();
        }
    }

    public ObservableCollection<IMenuItem> OptionItems
    {
        get => _optionItems;

        set
        {
            _optionItems = value;
            NotifyOfPropertyChange();
        }
    }

    public IMenuItem SelectedItem
    {
        get => _selectedItem;

        set
        {
            if (value == null)
            {
                return;
            }

            _selectedItem = value;
            SelectedIndex = Items.IndexOf(value);
            NotifyOfPropertyChange();
            NotifyOfPropertyChange(nameof(SelectedIndex));
        }
    }

    public int SelectedIndex
    {
        get;
        set;
    }

    public string USDollarValue
    {
        get => _USDollarValue;
        set
        {
            _USDollarValue = value;
            NotifyOfPropertyChange();
        }
    }

    public string EuroValue
    {
        get => _EuroValue;
        set
        {
            _EuroValue = value;
            NotifyOfPropertyChange();
        }
    }

    public double Opacity
    {
        get => _opacity;
        set
        {
            _opacity = value;
            NotifyOfPropertyChange();
        }
    }

    #endregion

    #region Methods

    public void ShowLocations() => ShowContent<LocationSelectorViewModel>();

    public void ShowItems() => ShowContent<ItemsListViewModel>();

    public void SetContent(ScreenBase screen)
    {
        _ = SetCurrencyValues();
        Content = screen;
        var menuInformation = screen.GetHamburgerMenuInformation();
        Items.Clear();

        foreach (var item in menuInformation.Items)
        {
            Items.Add(item);
        }

        NotifyOfPropertyChange(() => Items);
        SelectedItem = menuInformation.SelectedItem ?? Items.FirstOrDefault();
        Content.MenuSelectionChanged(SelectedItem);
    }

    public void MenuSelectionChanged(object value, ItemClickEventArgs args)
    {
        var clickedItem = args.ClickedItem as IMenuItem;

        Content.MenuSelectionChanged(clickedItem);
    }

    public void OptionMenuSelectionChanged(object value, ItemClickEventArgs args)
    {
        if (args.ClickedItem is HamburgerMenuIconItem iconItem && iconItem.Tag is IMenuItem optionItem)
        {
            optionItem.OnClick?.Invoke();
        }

        args.Handled = true;
    }

    public async Task UpdateApplication()
    {
        var needUpdate = await _updateManagerService.CheckForUpdateAsync();
        if (needUpdate)
        {
            await _updateManagerService.UpdateAsync();
        }
        else
        {
            VersionViewModel.NeedUpdate = false;
        }
    }

    public void GotFocus()
    {
        Opacity = 1;
    }

    public void LostFocus()
    {
        Opacity = (double)_settingsService.Opacity / 100;
    }

    public void HandleKeyboard(System.Windows.Forms.Keys key, System.Windows.Forms.Keys modifiers)
    {
        if (!IsActive || !modifiers.HasFlag(System.Windows.Forms.Keys.Control))
        {
            return;
        }

        switch (key)
        {
            case System.Windows.Forms.Keys.Up:
                SelectPreviousMenuItem();
                break;
            case System.Windows.Forms.Keys.Down:
                SelectNextMenuItem();
                break;
        }
    }

    protected override void OnViewLoaded(object view)
    {
        var window = view as Window;
        var informations = _settingsService.WindowInformation;

        // Todo: Check if out off screen
        Execute.OnUIThread(() =>
        {
            window.Width = informations.Width;
            window.Height = informations.Height;
            window.Left = informations.Position.Left;
            window.Top = informations.Position.Top;
        });
    }

    protected override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _flyoutService.ShowFlyoutRequested += FlyoutService_ShowFlyoutRequested;
        _flyoutService.CloseFlyoutRequested += FlyoutService_CloseFlyoutRequested;
        VersionViewModel.OnUpdateRequested += VersionViewModel_OnUpdateRequested;

        return base.OnActivateAsync(cancellationToken);
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        _settingsService.WindowInformation.Copy(this);
        _settingsService.Save();
        _flyoutService.ShowFlyoutRequested -= FlyoutService_ShowFlyoutRequested;
        _flyoutService.CloseFlyoutRequested -= FlyoutService_CloseFlyoutRequested;
        VersionViewModel.OnUpdateRequested -= VersionViewModel_OnUpdateRequested;

        return base.OnDeactivateAsync(close, cancellationToken);
    }

    private static async Task<string> GetCurrencyValue(Currencies currency) => $"1{CurrencyHelper.GetCurrencySymbol(currency)} = {await CurrencyHelper.GetValueInRoublesAsync(currency):N0}{CurrencyHelper.GetCurrencySymbol(Currencies.Rouble)}";

    private void FlyoutService_CloseFlyoutRequested(object sender, System.EventArgs e)
    {
        IsFlyoutOpen = false;
        FlyoutContent = null;
        FlyoutHeader = null;
    }

    private void FlyoutService_ShowFlyoutRequested(object sender, FlyoutRequest e)
    {
        FlyoutContent = e.Content;
        FlyoutPosition = e.Position;
        FlyoutHeader = e.Header;
        IsFlyoutOpen = true;
    }

    private void SettingsService_OnSaved(object sender, Settings e)
    {
        NotifyOfPropertyChange(() => Opacity);
        NotifyOfPropertyChange(() => IsTopMost);
    }

    private async void VersionViewModel_OnUpdateRequested(object sender, System.EventArgs e)
    {
        await _dialogService.ShowProgressAsync("Updating", "EFTHelper will restart...", UpdateApplication());
    }

    private async Task SetCurrencyValues()
    {
        USDollarValue = await GetCurrencyValue(Currencies.USDollar);
        EuroValue = await GetCurrencyValue(Currencies.Euro);
    }

    private void ShowContent<TScreenBase>()
        where TScreenBase : ScreenBase
    {
        if (Content is TScreenBase)
        {
            return;
        }

        var viewModel = IoC.Get<TScreenBase>();
        SetContent(viewModel);
    }

    private void SelectPreviousMenuItem()
    {
        var currentIndex = Items.IndexOf(SelectedItem);
        SelectedItem = currentIndex == 0 ? Items.Last() : Items.ElementAt(currentIndex - 1);
        Content.MenuSelectionChanged(SelectedItem);
    }

    private void SelectNextMenuItem()
    {
        var currentIndex = Items.IndexOf(SelectedItem);
        SelectedItem = currentIndex == Items.Count - 1 ? Items.First() : Items.ElementAt(currentIndex + 1);
        Content.MenuSelectionChanged(SelectedItem);
    }

    #endregion
}
