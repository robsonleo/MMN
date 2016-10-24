using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления поля "Шифр изделия"
    /// </summary>
    [Serializable]
    public class ProductCipherViewModel : FieldViewModelBase
    {
        /// <summary>
        /// Идентификатор записи(для модели)
        /// </summary>
        public short Id { get; private set; }

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

            //todo uncoment if (!KtcCache.ProductCodes.ContainsKey(Value))
            //{
            //    IsValid = false;
            //    ErrorInfo = new ErrorInfoViewModel(ErrorType.FIELD_DIRECTORY_NOT_FOUND);
            //    return false;
            //}

            //Id = KtcCache.ProductCodes[Value];
            return true;
        }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ProductCipherViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
