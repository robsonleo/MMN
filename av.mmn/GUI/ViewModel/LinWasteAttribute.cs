namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления поля "Из отхода" документа ЛИН
    /// </summary>
    public class LinWasteAttribute : FieldViewModelBase
    {
        /// <summary>
        /// Значение поля в виде целого числа
        /// </summary>
        public int? IntValue { get; private set; }

        private bool FormatValidation()
        {
            IntValue = null;

            if (string.IsNullOrEmpty(Value))
            {
                if (AllowEmpty)
                {
                    return true;
                }

                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY);
                return false;
            }

            int temp;
            if (int.TryParse(Value, out temp))
            {
                if (temp == 1 || temp == 9)
                {
                    IntValue = temp;
                    return true;
                }

                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
            }
            else
            {
                ErrorInfo = new ErrorInfoViewModel(ErrorType.Quantity_NOT_A_NUMBER);
            }

            IsValid = false;
            return false;
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public LinWasteAttribute()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
