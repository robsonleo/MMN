using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления поля КС
    /// </summary>
    public class KcViewModel : FieldViewModelBase
    {
        /// <summary>
        /// Значение поля
        /// </summary>
        //public override string Value
        //{
        //    get { return base.Value; }
        //    set
        //    {
        //        base.Value = Properties.ChecksSe ttings.Default.Перевод_обозначений_в_старый_вид
        //                        ? value.ToDenotation()
        //                        : value;
        //    }
        //}

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

            if (Value.Length < 2 || Value.Contains(" "))
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
        public KcViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
    }
}
