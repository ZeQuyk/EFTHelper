using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using EFTHelper.Models;
using MahApps.Metro.Controls;

namespace EFTHelper.Converters
{
    public class HamuburgerMenuItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<IMenuItem> nmItemCollection)
            {
                return nmItemCollection.Select(item => new HamburgerMenuIconItem
                {
                    Tag = item,
                    Label = item.Title
                });
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is HamburgerMenuIconItem menuItem ? menuItem.Tag : value;
        }
    }
}
