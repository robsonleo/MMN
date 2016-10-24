using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GUI.ViewModel;

namespace GUI.Interfaces
{
    /// <summary>
    /// Интерфейс контейнера полей
    /// </summary>
    public interface IFieldContainer : IFieldContainer<FieldViewModelBase>
    {
    }
    /// <summary>
    /// Интерфейс контейнера полей
    /// </summary>
    public interface IFieldContainer<T> where T : ViewModelBase
    {
        /// <summary>
        /// Список полей
        /// </summary>
        ObservableCollection<T> FieldList { get; }
    }
}
