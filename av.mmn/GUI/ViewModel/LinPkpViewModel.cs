using System.Linq;

namespace GUI.ViewModel
{
    public class LinPkpViewModel : FieldViewModelBase
    {
        private static readonly string[] CustomPkp = { "М", "И", "А" };

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

            if (!CustomPkp.Contains(Value))
            {
                IsValid = false;
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public LinPkpViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
