namespace GUI.ViewModel
{
    /// <summary>
    /// Модель представления строки заглушки
    /// </summary>
    public sealed class CorkRowViewModel : RowViewModelBase
    {
        /// <summary>
        /// Ссылка на родительский елеменет
        /// </summary>
        public ValidatedViewModelBase Parent { get; set; }

        /// <summary>
        /// Признак режима "только для чтения"
        /// </summary>
        public override bool IsReadOnly => Parent.IsReadOnly;
        /// <summary>
        ///заглушка
        /// </summary>
        public override KcViewModel Kc { get { return null; } protected set { } }
        /// <summary>
        /// заглушка
        /// </summary>
        public override bool IsLast => false;
    }
}
