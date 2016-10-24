using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Состояние валидации поля "Серия С"
    /// </summary>
    public class SerieFromValidationState : ValidatorStateBase
    {
        /// <summary>
        /// Ссылка на шифр изделия
        /// </summary>
        public ProductCipherViewModel ProductChipher { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="context">принимает ссылку на валидатор</param>
        public SerieFromValidationState(ValidatorBase context) : base(context)
        {
        }
        /// <summary>
        /// Метод Валидации
        /// </summary>
        public override void Validate()
        {
            if (ProductChipher == null)
                throw new ArgumentNullException();

            IContainsSeries _this = (IContainsSeries)Context.Field.Row;

            if (!string.IsNullOrEmpty(Context.Field.Value) || !Context.Field.AllowEmpty)
            {
                if (string.IsNullOrEmpty(ProductChipher.Value))
                {
                    Context.Field.IsValid = false;
                    Context.Field.ErrorInfo = new ErrorInfoViewModel(ErrorType.Series_INVALID_CONVERT_ARGUMENT,
                        "Шифр изделия незаполнен");
                    return;
                }

                int from = SeriesViewModel.GetIntSerie(_this.SeriesFrom.Value, ProductChipher.Value);

                if (from < 1)
                {
                    Context.Field.IsValid = false;
                    Context.Field.ErrorInfo = new ErrorInfoViewModel(ErrorType.Series_INVALID_CONVERT_ARGUMENT);
                    return;
                }
            }

            Context.State = Context.PreparatoryState;
            Context.Validate();
        }
    }
}
