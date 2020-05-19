using Comic.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Comic.Converter
{
    public class ComicFontSizeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value??ContentFormat.Sizes.M.ToString()).ToString();
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ComicFontSizeToValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)((ContentFormat.Sizes)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sizeValues = Enum.GetValues(typeof(ContentFormat.Sizes));

            foreach (var item in sizeValues)
            {
                if (value == item) return (ContentFormat.Sizes)item;
            }
            return ContentFormat.Sizes.M;
        }
    }
}
