using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Helpers;
using EFTHelper.Models.TarkovTools;
using EFTHelper.Services;
using MahApps.Metro.Controls;

namespace EFTHelper.ViewModels
{
    public class ItemsListViewModel : Screen, IViewAware
    {
        #region Fields

        private readonly TarkovToolsService _tarkovToolsService;
        private readonly SettingsService _settingsService;
        private bool _isBusy = false;
        private int _pageIndex = 0;
        private int _pageSize = 80;
        private bool _listenScroll = true;
        private string _query;
        private readonly object pageLock = new();
        private List<ItemTypeViewModel> _itemTypes;
        private ItemTypeViewModel _selectedType;
        private ObservableCollection<ItemBaseViewModel> _displayedItems;
        private List<ItemBaseViewModel> _items;

        #endregion

        #region Constructors

        public ItemsListViewModel(
            TarkovToolsService tarkovToolsService,
            VersionViewModel versionViewModel,
            SettingsService settingsService)
        {
            VersionViewModel = versionViewModel;
            _tarkovToolsService = tarkovToolsService;
            _settingsService = settingsService;
            ItemTypes = EnumHelper.GetEnumValues<ItemTypes>().Select(x => new ItemTypeViewModel(x)).ToList();
            SelectedType = ItemTypes.First();
            DisplayedItems = new ObservableCollection<ItemBaseViewModel>();
            DisplayName = "EFTHelper";
        }

        #endregion

        #region Properties

        public List<ItemTypeViewModel> ItemTypes
        {
            get => _itemTypes;
            set
            {
                _itemTypes = value;
                NotifyOfPropertyChange();
            }
        }

        public ItemTypeViewModel SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;                
                NotifyOfPropertyChange();
            }
        }

        public ObservableCollection<ItemBaseViewModel> DisplayedItems
        {
            get => _displayedItems;
            set
            {
                if (value != null)
                {
                    _displayedItems = value;
                    NotifyOfPropertyChange();
                }
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

        public VersionViewModel VersionViewModel { get; set; }

        public string Query
        {
            get => _query;
            set
            {
                _query = string.IsNullOrEmpty(value) ? string.Empty : value;
                NotifyOfPropertyChange();
            }
        }

        #endregion

        #region Methods

        public async void MenuSelectionChanged(object value, ItemClickEventArgs args)
        {
            var clickedType = args.ClickedItem as ItemTypeViewModel;
            if (clickedType != null)
            {
                SelectedType = clickedType;
            }

            await RefreshDisplayedItemsAsync(clearQuery:true);
        }

        public void OnScroll(System.Windows.Controls.ScrollChangedEventArgs scrollEvent)
        {
            if (!_listenScroll)
            {
                _listenScroll = true;
                return;
            }

            var position = scrollEvent.VerticalOffset + scrollEvent.ViewportHeight;
            var heightThreshold = scrollEvent.ExtentHeight / 1.1;

            if (position >= heightThreshold)
            {
                if (Monitor.TryEnter(pageLock))
                {
                    try
                    {
                        DisplayNextPage();
                    }
                    finally
                    {
                        Monitor.Exit(pageLock);
                    }
                }
            }
        }

        public async void Search(EventArgs args)
        {
            if (!string.IsNullOrEmpty(Query))
            {
                await RefreshDisplayedItemsAsync(false);
            }
        }

        private void DisplayNextPage()
        {
            if (DisplayedItems.Count >= _items.Count())
            {
                return;
            }

            var newItems = _items.Skip(_pageIndex * _pageSize).Take(_pageSize);
            foreach (var item in newItems)
            {
                DisplayedItems.Add(item);
            }           
            
            _pageIndex++;
        }

        protected override void OnViewLoaded(object view)
        {
            var window = view as Window;
            var informations = _settingsService.LocationSelectorInformations;
            RefreshDisplayedItemsAsync(!string.IsNullOrEmpty(Query));

            // Todo: Check if out off screen
            Execute.OnUIThread(() =>
            {
                window.Width = informations.Width;
                window.Height = informations.Height;
                window.Left = informations.Position.Left;
                window.Top = informations.Position.Top;
            });
        }

        private void Clear(bool clearQuery)
        {    
            DisplayedItems.Clear();
            _listenScroll = false;            
            _pageIndex = 0;

            if (clearQuery)
            {
                Query = string.Empty;
            }
        }

        private Task RefreshDisplayedItemsAsync(bool clearQuery)
        {
            return RefreshDisplayedItems(clearQuery).ContinueWith((t) => IsBusy = false);
        }

        private async Task RefreshDisplayedItems(bool clearQuery)
        {
            IsBusy = true;
            Clear(clearQuery);
            _items = await GetItemsAsync();
            DisplayNextPage();
        }

        private Task<List<ItemBaseViewModel>> GetItemsAsync()
        {
            if (!string.IsNullOrEmpty(Query))
            {
                return GetItemsByQueryAsync();
            }
            else
            {
                return GetItemsByTypeAsync();
            }          
        }

        private async Task<List<ItemBaseViewModel>> GetItemsByQueryAsync()
        {
            var items = new List<ItemBase>();

            var itemsByNameResponse = await _tarkovToolsService.GetItemsByNameAsync(Query);
            items = itemsByNameResponse.ItemsByName;

            var itemViewModels = items.Select(x => new ItemBaseViewModel(x)).ToList();

            if (SelectedType.ItemType != Enums.ItemTypes.Any)
            {
                itemViewModels.RemoveAll(x => !x.Types.Contains(SelectedType.ItemType));
            }

            return itemViewModels;
        }

        private async Task<List<ItemBaseViewModel>> GetItemsByTypeAsync()
        {
            var itemsByTypeResponse = await _tarkovToolsService.GetItemsByTypeAsync(SelectedType.ItemType);            

            return itemsByTypeResponse.ItemsByType.Select(x => new ItemBaseViewModel(x)).ToList();
        }

        #endregion
    }
}
