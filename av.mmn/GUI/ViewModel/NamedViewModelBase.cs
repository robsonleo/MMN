using System;
using GalaSoft.MvvmLight;

namespace GUI.ViewModel
{
        /// <summary>
        /// Модель представления с именем
        /// </summary>
        public abstract class NamedViewModelBase : ViewModelBase
        {
            private string _name;

            /// <summary>
            /// Название обьекта
            /// </summary>
            public string Name
            {
                get { return _name; }
                set
                {
                    if (_name == value)
                        return;
                    _name = value;
                    RaisePropertyChanged();
                }
            }

        protected virtual void RaisePropertyChanged(string propertyName = null, object oldValue = null, object newValue = null, bool broadcast = false)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("This method cannot be called with an empty string", "propertyName");
            this.RaisePropertyChanged(propertyName);
            if (!broadcast)
                return;
            this.Broadcast<object>(oldValue, newValue, propertyName);
        }
    }
}
