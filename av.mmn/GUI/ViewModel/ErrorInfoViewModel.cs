using GalaSoft.MvvmLight;

namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления информации об ошибке
    /// </summary>
    public class ErrorInfoViewModel : ViewModelBase
    {
        private ErrorInfoViewModel()
        {
        }
        /// <summary>
        /// Конструкто копированивя
        /// </summary>
        /// <param name="errorInfo"></param>
        public ErrorInfoViewModel(ErrorInfoViewModel errorInfo)
        {
            Type = errorInfo.Type;
            ErrorInfo = errorInfo.ErrorInfo;
            ErrorName = errorInfo.ErrorName;
            ResolveInfo = errorInfo.ResolveInfo;
        }
        /// <summary>
        /// Пользовательский конструктор
        /// </summary>
        /// <param name="type"></param>
        /// <param name="errorInfo"></param>
        /// <param name="errorName"></param>
        /// <param name="resolveInfo"></param>
        public ErrorInfoViewModel(ErrorType type, string errorInfo = @"", string errorName = @"", string resolveInfo = @"")
        {
            //todo do it
            //Type = type;
            //var prototype = KtcCache.TypicalErrors[type];

            //ErrorInfo = string.IsNullOrEmpty(errorInfo) ? prototype.ErrorInfo : errorInfo;
            //ErrorName = string.IsNullOrEmpty(errorName) ? prototype.ErrorName : errorName;
            //ResolveInfo = string.IsNullOrEmpty(resolveInfo) ? prototype.ResolveInfo : resolveInfo;
        }
        /// <summary>
        /// Пустая ошибка
        /// </summary>
        public static ErrorInfoViewModel GetEmptyErrorInfo => new ErrorInfoViewModel();
        /// <summary>
        /// Тип ошибки
        /// </summary>
        public ErrorType Type { get; }
        /// <summary>
        /// Информация об ошибке
        /// </summary>
        public string ErrorInfo { get; set; }
        /// <summary>
        /// название ошибки
        /// </summary>
        public string ErrorName { get; set; }
        /// <summary>
        /// информация о решении ошибки
        /// </summary>
        public string ResolveInfo { get; set; }
    }
}
/// <summary>
/// Тип Ошибки
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Пустое поле
    /// </summary>
    /// <remarks>
    /// Поле обязательное для заполнения является пустым
    /// </remarks>
    /// <example>
    /// Заполнение поля
    /// </example>
    FIELD_EMPTY,
    /// <summary>
    /// Неверный формат поля
    /// </summary>
    /// <remarks>
    /// Значение поля не совпадает с правилом заполнения
    /// </remarks>
    /// <example>
    /// Заполнение поля согласно правила или стандарта
    /// </example>
    FIELD_INVALID_FORMAT,
    /// <summary>
    /// Значение поля не найдено в справочнике
    /// </summary>
    /// <remarks>
    /// Значение поля несоответсвует ни одному значению в справочнике,
    /// который используется при проверке этого поля
    /// </remarks>
    /// <example>
    /// Повторная проверка значения на ошибки, сравнение значения с соответсвующим справочником
    /// </example>
    FIELD_DIRECTORY_NOT_FOUND,
    /// <summary>
    /// Номер документа слишком короткий
    /// </summary>
    /// <remarks>
    /// Значения поля номера документа слишком короткое, возможен вариант некорректного входного значения
    /// </remarks>
    /// <example>
    /// Повторная проверка входного значения
    /// </example>
    Number_Doc_TO_SHORT_LENGTH,
    /// <summary>
    /// Некорректное значение даты
    /// </summary>
    /// <remarks>
    /// Значение даты не целесообразно
    /// </remarks>
    /// <example>
    /// Повторная проверка даты
    /// </example>
    Date_INCORRECT_VALUE,
    /// <summary>
    /// Значение поля равно нулю
    /// </summary>
    /// <remarks>
    /// Некорректное значение количественного поля не является целесообразным
    /// </remarks>
    /// <example>
    /// Повторная проверка входного значения
    /// </example>
    Quantity_ZERO_VALUE,
    /// <summary>
    /// Значение поля меньше нуля
    /// </summary>
    /// <remarks>
    /// Некорректное значение количественного поля не является целесообразным
    /// </remarks>
    /// <example>
    /// Повторная проверка входного значения
    /// </example>
    Quantity_LESS_ZERO_VALUE,
    /// <summary>
    /// Значение поля не является числом
    /// </summary>
    /// <remarks>
    /// Значение поля не удается интерпретировать как число
    /// </remarks>
    /// <example>
    /// Повторная проверка входных данных
    /// </example>
    Quantity_NOT_A_NUMBER,
    /// <summary>
    /// Ошибка конвертации серии
    /// </summary>
    /// <remarks>
    /// Некорректное значение поля для перевода 
    /// </remarks> 
    /// <example>
    /// Повторная проверка входных данных, возможно исходное значение 
    /// является недопустимым
    /// </example>
    Series_INVALID_CONVERT_ARGUMENT,
    /// <summary>
    /// Серия С больше Серии ПО
    /// </summary>
    /// <remarks>
    /// После перевода полей серий, было обнаружено неверное равенство
    /// </remarks>
    /// <example>
    /// Повторная проверка корректности входных данных
    /// </example>
    Series_WRONG_DIAPASONE,
    /// <summary>
    /// Некорректное количество цехов
    /// </summary>
    ///  <remarks>
    /// Значение поля содержит недопустимое количество цехов (для этого поля)
    /// </remarks>
    /// <example>
    /// Изменение количества цехов в значении согласно правилу
    /// </example>
    Werkstatt_WRONG_COUNT,
    /// <summary>
    /// Некорректно значение поля
    /// </summary>
    /// <remarks>
    /// При проверке поля было обнаружено неверное равенство
    /// </remarks>
    /// <example>
    /// Повторная проверка корректности входных данных
    /// </example>
    Spsh_WRONG_DIAPASONE,
    /// <summary>
    /// Несовпадение количества в полях
    /// </summary>
    /// <remarks>
    /// При проверке количетсвенных полей обнаружено несовпадение значений
    /// </remarks>
    /// <example>
    /// Проверка количественных полей на наличие ошибок
    /// </example>
    QUANTITIES_UNSUGGESTION,
    /// <summary>
    /// Некорректное количество ТК
    /// </summary>
    /// <remarks>
    /// Значения количества ТК не совпадает с правилом проставления в соотношении с цехами потребителями
    /// </remarks>
    /// <example>
    /// Изменение количества значений ТК
    /// </example>
    Tk_WRONG_COUNT,
    /// <summary>
    /// Другое
    /// </summary>
    /// <remarks>
    /// Нетипизированная ошибка
    /// </remarks>
    /// <example>
    /// -
    /// </example>
    OTHER,
    /// <summary>
    /// Существование секции "Без ПКП"
    /// </summary>
    /// <remarks>
    /// Входные значения записей не содержат значение поля "ПКП"
    /// </remarks>
    /// <example>
    /// -
    /// </example>
    Section_NO_PKP_EXISTS,
    /// <summary>
    /// Тег не найден
    /// </summary>
    /// <remarks>
    /// В файле XML не найден соответсвущий тег поля
    /// </remarks>
    /// <example>
    /// -
    /// </example>
    TAG_NOT_FOUND
}
