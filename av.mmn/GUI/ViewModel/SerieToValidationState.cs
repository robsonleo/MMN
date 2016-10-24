using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.ViewModel
{
    /// <summary>
    /// Состояние валидации дялп яол 
    /// </summary>
    public class SerieToValidationState : ValidatorStateBase
    {
        /// <summary>
        /// Ссылка на шифр изделия
        /// </summary>
        public ProductCipherViewModel ProductChipher { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="context">Валидатор поля</param>
        public SerieToValidationState(ValidatorBase context) : base(context)
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

                int to = SeriesViewModel.GetIntSerie(_this.SeriesTo.Value, ProductChipher.Value);

                if (to < 1)
                {
                    Context.Field.IsValid = false;
                    Context.Field.ErrorInfo = new ErrorInfoViewModel(ErrorType.Series_INVALID_CONVERT_ARGUMENT);
                    return;
                }

                if (_this.SeriesFrom.IsValid != true)
                {
                    Context.Field.IsValid = false;
                    Context.Field.ErrorInfo = new ErrorInfoViewModel(ErrorType.Series_INVALID_CONVERT_ARGUMENT,
                        "Недопустимая Серия С");
                    return;
                }

                int from = SeriesViewModel.GetIntSerie(_this.SeriesFrom.Value, ProductChipher.Value);

                //якщо серія С менша чи рівна ПО все вірно, вони і так валідні. виходимо
                if (from > to)
                {
                    Context.Field.IsValid = false;
                    Context.Field.ErrorInfo = new ErrorInfoViewModel(ErrorType.Series_WRONG_DIAPASONE);
                    return;
                }
            }
            Context.State = Context.PreparatoryState;
            Context.Validate();
        }
    }
}
