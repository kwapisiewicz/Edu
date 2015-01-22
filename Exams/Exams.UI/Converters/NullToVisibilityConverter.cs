using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Exams.UI.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public Visibility VisibilityWhenNotNull { get; set; }
        public Visibility VisibilityWhenNull { get; set; }

        public NullToVisibilityConverter()
        {
            VisibilityWhenNotNull = Visibility.Visible;
            VisibilityWhenNull = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? VisibilityWhenNotNull : VisibilityWhenNull;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
