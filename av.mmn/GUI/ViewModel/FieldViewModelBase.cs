using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace GUI.ViewModel
{
    /// <summary>
    /// Абстрактный класс представления поля 
    /// </summary>
    public abstract class FieldViewModelBase : ValidatedViewModelBase
    {
        /// <summary>
        /// Делегат поля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void FieldViewModelBaseDelegate(object sender, EventArgs e);
        /// <summary>
        /// Событие потери фокуса
        /// </summary>
        public event FieldViewModelBaseDelegate LostFocusEvent;

        private bool _allowEmpty;
        /// <summary>
        /// Допуск пустого значения
        /// </summary>
        public bool AllowEmpty
        {
            get { return _allowEmpty; }
            set
            {
                if (AllowEmpty == value)
                    return;

                _allowEmpty = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Ссылка на строку
        /// </summary>
        public RowViewModelBase Row { get; set; }

        private string _value = string.Empty;
        /// <summary>
        /// Значение поля
        /// </summary>
        public virtual string Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                    return;

                _value = value?.TrimStart();
                RaisePropertyChanged();
                IsChanged = true;
            }
        }

        private ErrorInfoViewModel _errorInfo;
        /// <summary>
        /// Информация об ошибке при заполнении поля
        /// </summary>
        public ErrorInfoViewModel ErrorInfo
        {
            get { return _errorInfo; }
            set
            {
                if (_errorInfo == value)
                    return;

                _errorInfo = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Провайдер проверок поля
        /// </summary>
        public ValidatorBase Validator { get; protected set; }
        /// <summary>
        /// Обновление Состояния поля IsValid
        /// </summary>
        public override void RefreshIsValid()
        {
            if (ErrorInfo?.Type == ErrorType.TAG_NOT_FOUND)
                return;

            if (Validator == null)
            {
                if (string.IsNullOrEmpty(Value))
                {
                    if (AllowEmpty)
                    {
                        IsValid = null;
                    }
                    else
                    {
                        IsValid = false;
                        ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY);
                    }
                }
                else
                {
                    IsValid = true;
                }
            }
            else
            {
                Validator.Validate();
            }
        }

        private bool _isReadOnly;
        /// <summary>
        /// Признак режима "только для чтения"
        /// </summary>
        public override bool IsReadOnly => _isReadOnly || Row.IsReadOnly;
        /// <summary>
        /// Измение режима только для чтения
        /// </summary>
        public void SetReadOnlyMode(bool value)
        {
            _isReadOnly = value;
            RaisePropertyChanged(nameof(IsReadOnly));
        }

        private RelayCommand _lostFocusCommand;
        /// <summary>
        /// Команда модели представления, вызова потери фокуса
        /// </summary>
        public ICommand LostFocus
        {
            get
            {
                return _lostFocusCommand ?? (_lostFocusCommand = new RelayCommand(() =>
                {
                    Value = _value?.TrimEnd();
                    LostFocusEvent?.Invoke(this, EventArgs.Empty);
                    RefreshIsValid();
                }));
            }
        }


        /// <summary>
        /// Возвращает строку представляющую данный объект
        /// </summary>
        public override string ToString()
        {
            return $"{base.ToString()}; N:{Name}; V:{Value};";
        }
    }
}
