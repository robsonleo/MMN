using System.Text.RegularExpressions;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления поля "Шифр материала"
    /// </summary>
    public class MaterialChiperViewModel : FieldViewModelBase
    {
        /// <summary>
        /// Идентификатор записи(для модели)
        /// </summary>
        public int Id { get; private set; }

        private bool FormatValidation()
        {
            Id = -1;

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

            if (Regex.Match(Value, @"\d{9}").Value != Value)
            {
                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                return false;
            }

            //todo uncoment if (!KtcCache.MaterialsChipers.ContainsKey(Value))
            //{
            //    IsValid = false;
            //    ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_DIRECTORY_NOT_FOUND,
            //        "Шифр материала не найден в справочнике шифраторе материалов");
            //    return false;
            //}

            //Id = KtcCache.MaterialsChipers[Value];
            return true;
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MaterialChiperViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
