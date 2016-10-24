using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления номера документа 
    /// </summary>
    [Serializable]
    public class NumberDocumentViewModel : FieldViewModelBase
    {
        private bool FormatValidation()
        {
            if (string.IsNullOrEmpty(Value))
            {
                if (AllowEmpty)
                {
                    IsValid = null;
                    return true;
                }

                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_EMPTY);
                IsValid = false;
                return false;
            }

            if (Value.Length < 3)
            {
                ErrorInfo = new ErrorInfoViewModel(ErrorType.Number_Doc_TO_SHORT_LENGTH);
                IsValid = false;
                return false;
            }

            if (Value.Contains(" "))
            {
                ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_INVALID_FORMAT);
                IsValid = false;
                return false;
            }

            return true;
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public NumberDocumentViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
