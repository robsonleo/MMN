using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GUI.ViewModel
{
    /// <summary>
    /// Базовая модель представления
    /// </summary>
    public abstract class ValidatedViewModelBase : NamedViewModelBase, ISupportInitializeNotification
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected ValidatedViewModelBase()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            BeginInit();
        }

        private bool? _isValid;
        /// <summary>
        /// Признак соответсвия правилам 
        /// </summary>
        public virtual bool? IsValid
        {
            get { return _isValid; }
            set
            {
                if (_isValid == value) return;
                _isValid = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Виртуальный метод проверки
        /// </summary>
        public abstract void RefreshIsValid();

        /// <summary>
        /// Внутренний статус изменения
        /// </summary>
        private bool _isChanged;
        /// <summary>
        /// Признак изменения
        /// </summary>
        public virtual bool IsChanged
        {
            get { return _isChanged; }
            protected set
            {
                if (_isChanged == value)
                    return;

                _isChanged = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Признак режима "только для чтения"
        /// </summary>
        public abstract bool IsReadOnly { get; }

        /// <summary>
        /// Сброс признака изменений
        /// </summary>
        public virtual void RefreshChanges()
        {
            IsChanged = false;
        }

        /// <summary>
        /// Виртуальный метод регистрации зависимостей вычисляемых полей
        /// </summary>
        protected virtual void RegisteDependencies()
        {

        }

        /// <summary>
        /// Регистрация зависимостей от  полей 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="element"></param>
        /// <param name="destinationPropertyNames"></param>
        /// <param name="actionOnChanged"></param>
        protected void RegisterElementPropertyDependencies(string propertyName, object element, ICollection<string> destinationPropertyNames, Action actionOnChanged = null)
        {
            if (element == null)
                return;

            actionOnChanged?.Invoke();

            //if (element is INotifyPropertyChanged == false)
            //    throw new Exception($"Невозможно отслеживать изменения при биндинге в {element.GetType()}, т.к. он не реализует INotifyPropertyChanged");

            ((INotifyPropertyChanged)element).PropertyChanged += (o, eventArgs) =>
            {
                if (destinationPropertyNames.Contains(eventArgs.PropertyName))
                {
                    RaisePropertyChanged(propertyName);

                    actionOnChanged?.Invoke();
                }
            };
        }

        /// <summary>
        /// Регистрация зависимостей от коллекции
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="collection"></param>
        /// <param name="destinationPropertyNames"></param>
        /// <param name="actionOnChanged"></param>
        protected void RegisterCollectionPropertyDependencies<T>(string propertyName, ObservableCollection<T> collection, ICollection<string> destinationPropertyNames, Action actionOnChanged = null)
        {
            if (collection == null)
                return;

            actionOnChanged?.Invoke();

            foreach (var element in collection)
            {
                RegisterElementPropertyDependencies(propertyName, element, destinationPropertyNames);
            }

            collection.CollectionChanged += (sender, args) =>
            {
                RaisePropertyChanged(propertyName);

                if (args.NewItems != null)
                {
                    foreach (var addedItem in args.NewItems)
                    {
                        RegisterElementPropertyDependencies(propertyName, addedItem, destinationPropertyNames, actionOnChanged);
                    }
                }

            };
        }
        /// <summary>
        /// Началао инициализации
        /// </summary>
        public virtual void BeginInit()
        {
            IsInitialized = false;
        }
        /// <summary>
        /// Завершение инициализации
        /// </summary>
        public virtual void EndInit()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
            Initialized?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Признак состояния инициализации
        /// </summary>
        public bool IsInitialized { get; private set; }
        /// <summary>
        /// Событие инициализации
        /// </summary>
        public event EventHandler Initialized;
    }
}
