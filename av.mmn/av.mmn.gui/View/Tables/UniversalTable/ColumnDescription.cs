using System;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace av.mmn.gui.View.Tables.UniversalTable
{
    /// <summary>
    /// Представление для отображения данных в колонках.
    /// </summary>
    public sealed class GridViewColumnDescription : GridViewColumn
    {
        private static int _countSortPriority=0;
        private int _sortPriority=-1;
        private ICommand _increaseSortPriority;

        /// <summary>
        /// Строка для фильтрации данных.
        /// </summary>
        public string FilterParametr { get; set; }

        /// <summary>
        /// Порядок сортировки.
        /// </summary>
        public SortOrder SortOrderParametr { get; set; }
        
        /// <summary>
        /// Приоритет сортировки.
        /// </summary>
        public int SortPriority{get { return _sortPriority; }}
        
        /// <summary>
        /// Ширина колонки.
        /// </summary>
        public int WidthCells { get; set; }

        /// <summary>
        /// Отображаемое название колонки.
        /// </summary>
        public Object Header
        {
            get { return base.Header ?? base.DisplayMemberBinding.BindingGroupName; }
            set { base.Header = value; }
        }

        /// <summary>
        /// Перенос заголовков колонок.
        /// </summary>
        public bool IsHeaderHyphenition { get; set; }

        /// <summary>
        /// Перенос текста в ечейках.
        /// </summary>
        public bool IsCellHyphenition { get; set; }

        /// <summary>
        /// Автоширина колонки.
        /// </summary>
        public bool AutoWidthCells { get; set; }

        /// <summary>
        /// Увеличение приоритета сортировки и вызов функции сортировки.
        /// </summary>
        public ICommand IncreaseSortPrioryity
        {
            get
            {
                return _increaseSortPriority ?? (_increaseSortPriority = new RelayCommand<Object>((obj) =>
                {
                    if (_sortPriority != _countSortPriority) _sortPriority = ++_countSortPriority;
                    SortTableAction();
                }));
            }
        }

        /// <summary>
        /// Функция сортировки.
        /// </summary>
        public Action SortTableAction { get; set; } = () => { };

        public GridViewColumnDescription(string bindingPropertyName, Action sortAction, string header = "Header",
            int widthCells = 50, bool isHeaderHyphenetion = false, bool isCellHyphenetion = false,
            bool autoWidthColumn = true)
        {
            base.DisplayMemberBinding = new Binding(bindingPropertyName);
            Header = header;
            IsHeaderHyphenition = isHeaderHyphenetion;
            IsCellHyphenition = isCellHyphenetion;
            AutoWidthCells = autoWidthColumn;
            WidthCells = widthCells;
            SortTableAction += sortAction;
        }

        public GridViewColumnDescription()
        {
        }
    }


    


    
}
