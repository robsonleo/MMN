using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace av.mmn.gui.View.Tables.UniversalTable
{
    /// <summary>
    /// Конвертер последоватеоьности сортировки в угол поворота (Ascending - 0 градусов; Descending - 180 градусов)
    /// </summary>
    public class SortOrderConverterToAngleRotate:IValueConverter
    {
        /// <summary>
        /// Конвертация последоватеоьности сортировки в угол поворота (Ascending - 0 градусов; Descending - 180 градусов)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SortOrder order = value as SortOrder? ?? SortOrder.Ascending;
            return order == SortOrder.Ascending ? 0 : 180;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
