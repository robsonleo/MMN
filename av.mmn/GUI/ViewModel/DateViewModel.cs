using System;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления поля даты
    /// </summary>
    [Serializable]
    public class DateViewModel : FieldViewModelBase
    {
        private bool FormatValidation()
        {
            Date = DateTime.MinValue;

            if (string.IsNullOrEmpty(Value))
            {
                if (AllowEmpty)
                {
                    IsValid = null;
                    return true;
                }

                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY);
                return false;
            }

            DateTime date;

            if (!DateTime.TryParse(Value, out date))
            {
                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                return false;
            }

            if (date <= new DateTime(1920, 1, 1) || date > DateTime.Now.AddDays(30))
            {
                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.Date_INCORRECT_VALUE);
                return false;
            }

            Date = date;
            return true;
        }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; private set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public DateViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
