using System.Text.RegularExpressions;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления поля "Номер ЛИН" документа ЛИН
    /// </summary>
    public class NumberLinViewModel : FieldViewModelBase
    {
        /// <summary>
        /// Численное значение поля
        /// </summary>
        public decimal? DecimalValue { get; private set; }

        private bool FormatValidation()
        {
            DecimalValue = null;

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

            if (Regex.Match(Value, @"(?!0{4})\d{4}").Value != Value)
            {
                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                return false;
            }

            DecimalValue = decimal.Parse(Value);
            return true;
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public NumberLinViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
