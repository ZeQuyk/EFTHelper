using System;
using System.Collections.Generic;
using System.Text;

namespace Tarkov_Maps.ViewModels
{
    public class MapViewModel
    {
        public string Name { get; set; }

        public string ImagePath => $"pack://application:,,,/Images/{Name.ToLower()}.png";
    }
}
