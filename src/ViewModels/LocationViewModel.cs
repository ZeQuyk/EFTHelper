using Caliburn.Micro;
using EFTHelper.Extensions;
using EFTHelper.Models;

namespace EFTHelper.ViewModels
{
    public class LocationViewModel : PropertyChangedBase, IMenuItem
    {
        #region Constructors

        public LocationViewModel(Enums.Locations location)
        {
            var locationName = location.ToString();
            Name = locationName.ToSentence();
            ImagePath = $"pack://application:,,,/Images/{locationName.ToLower()}.png";
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public string Label => Name.Substring(0,1);

        public string Title => Name;

        public object Icon => null;

        #endregion
    }
}