using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace GUI.ViewModel
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
