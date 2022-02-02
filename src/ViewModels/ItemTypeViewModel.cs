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
                case ItemTypes.Barter:
                    icon = new PackIconForkAwesome() { Kind = PackIconForkAwesomeKind.Exchange };
                    break;
                case ItemTypes.Container:
                    icon = new PackIconZondicons() { Kind = PackIconZondiconsKind.Box };
                    break;
                case ItemTypes.Glasses:
                    icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Glasses };
                    break;
                case ItemTypes.Grenade:
                    icon = new PackIconRPGAwesome() { Kind = PackIconRPGAwesomeKind.Grenade };
                    break;
                case ItemTypes.Gun:
                    icon = new PackIconRPGAwesome() { Kind = PackIconRPGAwesomeKind.CrossedPistols };
                    break;
                case ItemTypes.Headphones:
                    icon = new PackIconUnicons() { Kind = PackIconUniconsKind.Headphones };
                    break;
                case ItemTypes.Helmet:
                    icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.HardHatSolid };
                    break;
                case ItemTypes.Injectors:
                    icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Needle };
                    break;
                case ItemTypes.Keys:
                    icon = new PackIconCodicons { Kind = PackIconCodiconsKind.Key };
                    break;
                case ItemTypes.MarkedOnly:
                    icon = new PackIconMaterial { Kind = PackIconMaterialKind.CheckboxMarkedCircle }; 
                    break;
                case ItemTypes.Meds:
                    icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.BriefcaseMedicalSolid };
                    break;
                case ItemTypes.Mods:
                    icon = new PackIconMaterial() { Kind = PackIconMaterialKind.MagazineRifle };
                    break;
                case ItemTypes.NoFlea:
                    icon = new PackIconUnicons() { Kind = PackIconUniconsKind.Ban };
                    break;
                case ItemTypes.PistolGrip:
                    icon = new PackIconRadixIcons() { Kind = PackIconRadixIconsKind.DragHandleVertical };
                    break;
                case ItemTypes.Preset:
                    icon = new PackIconVaadinIcons() { Kind = PackIconVaadinIconsKind.Tools };
                        break;
                case ItemTypes.Provisions:
                    icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FridgeIndustrialOutline };
                    break;
                case ItemTypes.Rig:
                    icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.VestSolid };
                    break;
                case ItemTypes.Suppressor:
                    icon = new PackIconMicrons() { Kind = PackIconMicronsKind.MuteVolume };
                    break;
                case ItemTypes.Wearable: 
                    icon = new PackIconModern() { Kind = PackIconModernKind.ClothesShirt };
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
