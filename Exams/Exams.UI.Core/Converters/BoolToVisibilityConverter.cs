using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Exams.UI.Core.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility VisibilityWhenTrue { get; set; }
        public Visibility VisibilityWhenFalse { get; set; }

        public BoolToVisibilityConverter()
        {
            VisibilityWhenTrue = Visibility.Visible;
            VisibilityWhenFalse = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? VisibilityWhenTrue : VisibilityWhenFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
