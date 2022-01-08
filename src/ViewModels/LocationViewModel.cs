using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using EFTHelper.Extensions;

namespace EFTHelper.ViewModels
{
    public class LocationViewModel : PropertyChangedBase
    {
        public string Name { get; set; }

        public string ImagePath
        {
            get; set;
        }

        public LocationViewModel(Enums.Locations location)
        {
            var locationName = location.ToString();
            Name = locationName.ToSentence();
            ImagePath = $"pack://application:,,,/Images/{locationName.ToLower()}.png";
        }
    }
}
