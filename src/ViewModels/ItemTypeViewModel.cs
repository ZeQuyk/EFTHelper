using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Models;
using MahApps.Metro.IconPacks;

namespace EFTHelper.ViewModels
{
    public class ItemTypeViewModel : PropertyChangedBase, IMenuItem
    {
        #region Fields

        private ItemTypes _itemType;

        #endregion

        #region Constructors

        public ItemTypeViewModel(ItemTypes itemType)
        {
            _itemType = itemType;
            Icon = BuildIcon(_itemType);
        }

        #endregion

        #region Properties

        public string Label => _itemType.ToString().ToSentence();

        public string Title => _itemType.ToString().ToSentence();

        public object Icon { get; }

        public ItemTypes ItemType => _itemType;

        #endregion

        #region Methods

        private static PackIconBase BuildIcon(ItemTypes itemType)
        {
            PackIconBase icon = null;
            switch (itemType)
            {
                case ItemTypes.Any:
                    icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.AsteriskSolid };
                    break;
                case ItemTypes.Ammo:
                    icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Ammunition };
                    break;
                case ItemTypes.AmmoBox:
                    icon = new PackIconRPGAwesome() { Kind = PackIconRPGAwesomeKind.AmmoBag };
                    break;
                case ItemTypes.Armor:
                    icon = new PackIconRemixIcon() { Kind = PackIconRemixIconKind.TShirt2Fill};
                    break;
                case ItemTypes.Backpack:
                    icon = new PackIconUnicons() { Kind = PackIconUniconsKind.Backpack };
                    break;
            }

            if (icon != null) 
            { 
                icon.Width = 24;
                icon.Height = 24;
            }

            return icon;
        }

        #endregion
    }
}
