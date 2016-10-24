using System;
using System.Globalization;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления поля размера документа ЛИН
    /// </summary>
    [Serializable]
    public class LinSizeViewModel : FieldViewModelBase
    {
        static readonly IFormatProvider DotNumberInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
        static readonly IFormatProvider CommaNumberInfo = new NumberFormatInfo { NumberDecimalSeparator = "," };
        /// <summary>
        /// Длинна
        /// </summary>
        public int? Length { get; private set; }
        /// <summary>
        /// Ширина
        /// </summary>
        public int? Width { get; private set; }
        /// <summary>
        /// Высота
        /// </summary>
        public float? Height { get; private set; }

        private bool FormatValidation()
        {
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

            Length = null;
            Width = null;
            Height = null;

            string[] values = Value.Split('x', 'х', 'X', 'Х');
            if (values.Length > 3)
            {
                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                return false;
            }

            float? value = GetValue(values, 0);
            if (value == null)
            {
                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                return false;
            }
            Length = (int)value.Value;

            value = GetValue(values, 1);
            if (value == null)
            {
                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                return false;
            }
            Width = (int)value.Value;

            if ((Height = GetValue(values, 2)) == null)
            {
                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                return false;
            }

            return true;

            //if (Length != 0 || Width != 0 || Height != 0) return;

            //IsValid = false;
            //ErrorInfo = new ErrorInfoViewModel(ErrorType.OTHER, "Длинна, ширина и высота равны нулю", "Отсутствует размер заготовки");
        }

        private float? GetValue(string[] values, int index)
        {
            if (values.Length < index + 1)
                return null;

            float temp;

            //если же поле не пустое то пытаемся спарсить то что в нем лежит и если получается радуемся
            if (float.TryParse(values[index], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, DotNumberInfo, out temp) ||
                float.TryParse(values[index], NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CommaNumberInfo, out temp))
            {
                return temp;
            }

            return null;
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public LinSizeViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
