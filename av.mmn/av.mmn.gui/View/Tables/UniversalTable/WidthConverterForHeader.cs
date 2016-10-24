using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace av.mmn.gui.View.Tables.UniversalTable
{
    internal class WidthConverterForHeader : IValueConverter
    {
        private const int _borderThickness = 4;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = ((double)value) - _borderThickness;
            return width > 0 ? width : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((double)value) + _borderThickness;
        }
    }
}
