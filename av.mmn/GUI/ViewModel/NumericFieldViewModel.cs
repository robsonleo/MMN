using System;
using System.Globalization;
using System.Linq;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления численного поля
    /// </summary>
    [Serializable]
    public class NumericFieldViewModel : FieldViewModelBase
    {
        static readonly IFormatProvider DotNumberInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
        static readonly IFormatProvider CommaNumberInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };

        private bool FormatValidation()
        {
            DecimalValue = null;
            string value = Value;

            //проверяем пустое ли значение
            if (string.IsNullOrEmpty(value))
            {
                //если пустое то проверяем может ли поле содержать пустое значение
                if (AllowEmpty)
                {
                    IsValid = null;
                }
                else
                {
                    IsValid = false;
                    ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY);
                    return false;
                }
            }
            else
            {
                decimal temp;
                //если же поле не пустое то пытаемся спарсить то что в нем лежит и если получается радуемся
                if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, DotNumberInfo, out temp) ||
                    decimal.TryParse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CommaNumberInfo, out temp))
                {
                    if (temp == 0)
                    {
                        IsValid = false;
                        ErrorInfo = new ErrorInfoViewModel(ErrorType.Quantity_ZERO_VALUE);
                        return false;
                    }

                    if (temp < 0)
                    {
                        IsValid = false;
                        ErrorInfo = new ErrorInfoViewModel(ErrorType.Quantity_LESS_ZERO_VALUE);
                        return false;
                    }


                    if (AllowFloat)
                    {
                        //ну и можем теперь присвоить это численному представлению нашего количества
                        DecimalValue = temp;
                    }
                    else
                    {
                        if (value.Contains(',') || value.Contains('.'))
                        {
                            IsValid = false;
                            ErrorInfo = new ErrorInfoViewModel(ErrorType.OTHER, "Поле не допускает дробных значений");
                            return false;
                        }

                        DecimalValue = temp;
                    }
                }
                else
                {
                    IsValid = false;
                    ErrorInfo = new ErrorInfoViewModel(ErrorType.Quantity_NOT_A_NUMBER);
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Флаг разрешения не целочисленных значений
        /// </summary>
        public bool AllowFloat { get; set; } = true;

        private decimal? _decimalValue;
        /// <summary>
        /// Числовое значение поля
        /// </summary>
        public decimal? DecimalValue
        {
            get { return _decimalValue; }
            private set { _decimalValue = value; }
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public NumericFieldViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
