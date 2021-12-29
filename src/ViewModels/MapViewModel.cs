using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace EscapeFromTarkov.Utility.ViewModels
{
    public class MapViewModel : PropertyChangedBase
    {
        public string Name { get; set; }

        public string ImagePath
        {
            get; set;
        }

        public MapViewModel(Enums.Maps map)
        {
            Name = map.ToString();
            ImagePath = $"pack://application:,,,/Images/{Name.ToLower()}.png";
        }
    }
}
