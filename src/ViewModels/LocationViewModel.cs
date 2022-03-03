using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Models;
using MahApps.Metro.IconPacks;

namespace EFTHelper.ViewModels
{
    public class LocationViewModel : PropertyChangedBase, IMenuItem
    {
        #region Constants

        private const string ImageUrlBase = "https://raw.githubusercontent.com/ZeQuyk/EFTHelper/main/src/Images";

        #endregion

        #region Constructors

        public LocationViewModel(Locations location)
        {
            var locationName = location.ToString();
            Name = locationName.ToSentence();
            ImagePath = $"{ImageUrlBase}/{locationName.ToLower()}.jpg";
            Icon = BuildIcon(location);
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Label => Name.Substring(0, 1);

        public string Title => Name;

        public object Icon { get; set; }

        public System.Action OnClick => null;

        #endregion

        #region Methods

        private static PackIconBase BuildIcon(Locations location)
        {
            PackIconBase icon = null;
            switch (location)
            {
                case Locations.Customs:
                    icon = new PackIconMaterial() { Kind = PackIconMaterialKind.PoliceBadgeOutline };
                    break;
                case Locations.Factory:
                    icon = new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.Factory };
                    break;
                case Locations.Interchange:
                    icon = new PackIconUnicons() { Kind = PackIconUniconsKind.ShoppingCart };
                    break;
                case Locations.Lighthouse:
                    icon = new PackIconRPGAwesome() { Kind = PackIconRPGAwesomeKind.Lighthouse };
                    break;
                case Locations.Reserve:
                    icon = new PackIconBoxIcons() { Kind = PackIconBoxIconsKind.RegularTrain };
                    break;
                case Locations.Shoreline:
                    icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.HotelSolid };
                    break;
                case Locations.TheLab:
                    icon = new PackIconUnicons() { Kind = PackIconUniconsKind.Flask };
                    break;
                case Locations.Woods:
                    icon = new PackIconMaterial() { Kind = PackIconMaterialKind.Forest };
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