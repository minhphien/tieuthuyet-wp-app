using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Comic.Converter
{
    public class ImageThemeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if(parameter == null) return null;

        Visibility darkBackgroundVisibility = 
            (Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"];

        if (darkBackgroundVisibility == Visibility.Visible)
        {
            return (ImageSource)(new ImageSourceConverter().ConvertFromString("/Comic;component/" + parameter.ToString()));
        }
        else
        {
            string path = parameter.ToString();
            path = 
                System.IO.Path.GetDirectoryName(path) +
                System.IO.Path.GetPathRoot(path) + 
                System.IO.Path.AltDirectorySeparatorChar+
                System.IO.Path.GetFileNameWithoutExtension(path) + 
                ".light"+
                System.IO.Path.GetExtension(path);
            return (ImageSource)(new ImageSourceConverter().ConvertFromString("/Comic;component/" + path.ToString()));
        }
    }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
