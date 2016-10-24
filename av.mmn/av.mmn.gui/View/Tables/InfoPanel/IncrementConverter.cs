using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace av.mmn.gui.View.Tables.InfoPanel
{
    class IncrementConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = 0;
            return Double.TryParse(value.ToString(), out val) ? val + 1 : val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = 0;
            return Double.TryParse(value.ToString(), out val) ? val - 1 : val;
        }
    }
}
