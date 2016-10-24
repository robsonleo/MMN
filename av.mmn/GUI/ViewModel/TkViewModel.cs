using System.Text.RegularExpressions;


namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления поля "ТК"
    /// </summary>
    public class TkViewModel : FieldViewModelBase
    {
        private bool FormatValidation()
        {
            //проверяем пустая ли строка
            if (string.IsNullOrEmpty(Value))
            {
                // если пустая то проверяем допустимы ли пустые значения в ней
                if (AllowEmpty)
                {
                    //если да то желтым, если нет то красным
                    IsValid = null;
                    return true;
                }

                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY);
                return false;
            }

            // далее если поле не пустое то проверяем на совпадение регулярному выражению
            if (Regex.Match(Value,
                @"((?!0{4})\d{4}(,(?!0{4})\d{4}){0,9}|-)(/((?!0{4})\d{4}(,(?!0{4})\d{4}){0,9}|-))?")
                .Value != Value)
            {
                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Перевод серии в прямой счет
        /// </summary>
        public TkViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
