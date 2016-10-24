using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Интерфейс контейнера полей
    /// </summary>
    public interface IImageSourceContainer
    {
        /// <summary>
        /// Путь к файлу
        /// </summary>
        string ImageSourceUri { get; }
    }
}
