using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления поля Серия
    /// </summary>
    [Serializable]
    public class SeriesViewModel : FieldViewModelBase
    {
        private bool FormatValidation()
        {
            if (string.IsNullOrEmpty(base.Value))
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

            if (Regex.Match(base.Value, @"([0-9]{2,3}-((0[1-9])|(10)))|([0-9]{1,4})").Value != Value)
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
        public SeriesViewModel()
        {
            FormatValidator validator = new FormatValidator(this, FormatValidation);
            Validator = validator;
        }
        /// <summary>
        /// Перевод серии в прямой счет
        /// </summary>
        public static int GetIntSerie(string serie, string productChipher)
        {
            //int number;
            //if (serie.Contains("-"))
            //{
            //    string[] temp = serie.Split('-');

            //    //todo uncoment number = KtcDataBase.StaticDbContext.SerieToNumber(productChipher, int.Parse(temp[0]), int.Parse(temp[1]));

            //    if (number != -1)
            //        return number;
            //}
            //else
            //{
            //    if (!int.TryParse(serie, out number))
            //        number = int.MaxValue;
            //}

            //return number;
            return 0;
        }
    }
}
