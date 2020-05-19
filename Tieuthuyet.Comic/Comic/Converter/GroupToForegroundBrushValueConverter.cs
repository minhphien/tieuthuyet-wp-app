using Comic.ViewModels;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Comic.Converter
{
    public class GroupToForegroundBrushValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            NovelInGroup group = value as NovelInGroup;
            object result = null;

            if (group != null)
            {
                if (group.Count == 0)
                {
                    result = (SolidColorBrush)Application.Current.Resources["PhoneDisabledBrush"];
                }
                else
                {
                    result = new SolidColorBrush(Colors.White);
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
