using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Интерфейс содержания серии(применяется в строках
    /// </summary>
    public interface IContainsSeries
    {
        /// <summary>
        /// Поле серия С
        /// </summary>
        SeriesViewModel SeriesTo { get; }
        /// <summary>
        /// Поле Серия ПО
        /// </summary>
        SeriesViewModel SeriesFrom { get; }
    }
}
