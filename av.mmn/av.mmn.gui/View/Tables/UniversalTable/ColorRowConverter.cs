using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace av.mmn.gui.View.Tables.UniversalTable
{
    internal class ColorRowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object[] param = parameter as object[];
            if (param == null || param.Length < 3) return Colors.Transparent;
            int? index = param[0] as int?;
            Color? colorFirst = param[1] as Color?;
            Color? colorSecond = param[2] as Color?;
            if (index == null || colorFirst == null || colorSecond == null) return Colors.Transparent;
            return index % 2 == 0 ? colorSecond : colorFirst;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Colors.Transparent;
        }
    }
}
