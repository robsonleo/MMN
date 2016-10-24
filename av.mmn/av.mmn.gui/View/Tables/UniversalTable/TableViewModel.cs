using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;
using av.mmn.gui.View.MainWindow;
using CustomFunctions;
using CustomFunctions.Extensions;
using Expression = System.Linq.Expressions.Expression;

namespace av.mmn.gui.View.Tables.UniversalTable
{
    public class TableViewModel
    {
        private ObservableCollection<GridViewColumnDescription> _columns;
        private ObservableCollectionWithMaxLenElement<object> _rows;
        private IEnumerable<object> _dataSource;
        private bool _showNameHeaderByAttribute;
        private List<PropertyInfo> _sortPropertiesPriority;
        private Action _sortAction;
        private Action _changeCallBackAction;
        private Func<Object, Object, int> _rowCompareFunc;

        /// <summary>
        /// Колекция данных для привязки.
        /// </summary>
        public IEnumerable<object> DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
                _columns = new ObservableCollection<GridViewColumnDescription>();
                var col = _dataSource?.FirstOrDefault(p => p != null);
                if (col != null)
                {
                    _rows = new ObservableCollectionWithMaxLenElement<object>(_dataSource);

                    List<string> columnsBindingName = new List<string>(col.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name));

                    foreach (var nameProperty in columnsBindingName)
                    {
                        var atr = col.GetType()
                            .GetProperty(nameProperty, BindingFlags.Public | BindingFlags.Instance)
                            .GetCustomAttributes(typeof(InfoFieldAttribute), true);
                        string showedNameHeader = atr.Any() && _showNameHeaderByAttribute ? ((InfoFieldAttribute)atr.First()).ShowName : nameProperty;

                        _columns.Add(new GridViewColumnDescription(nameProperty, _sortAction, showedNameHeader,
                            Rows.GetMaxLengthColumn(showedNameHeader) * (int)Unit.Pixel(showedNameHeader.Length + 2).Value,
                            false, true));
                    }
                }
            }
        }

        /// <summary>
        /// Колекция столбцов.
        /// </summary>
        public ObservableCollection<GridViewColumnDescription> Columns
        {
            get { return _columns; }
        }

        /// <summary>
        /// Колекция строк для отображения.
        /// </summary>
        public ObservableCollectionWithMaxLenElement<Object> Rows
        {
            get
            {
                return _rows;
            }
        }

        /// <summary>
        /// Порядок сортировки строк.
        /// </summary>
        public SortOrder SortOrderRows { get; set; } = SortOrder.Ascending;


        public TableViewModel(IEnumerable<object> collection, Action changeCallBackAction, Action sortActionCallBack, bool showNameHeaderByAttribute = true)
        {
            _changeCallBackAction += changeCallBackAction;
            _rowCompareFunc = Comparer;
            _sortAction += sortActionCallBack;
            _sortAction += QuickOrderRows;
            _showNameHeaderByAttribute = showNameHeaderByAttribute;
            DataSource = collection;
        }

        private int Comparer(Object obj1, Object obj2)
        {
            return CompareByFields(obj1, obj2);
        }

        private int CompareByFields(Object obj1, Object obj2, int indexProperty = 0)
        {
            if (_sortPropertiesPriority?[indexProperty] == null) throw new ArgumentNullException(nameof(_sortPropertiesPriority));

            var sortProperty = _sortPropertiesPriority[indexProperty];

            var value1 = sortProperty.GetValue(obj1, null).ToString();
            var value2 = sortProperty.GetValue(obj2, null).ToString();

            int resultCompare = String.Compare(value1, value2);

            if (resultCompare == 0 && ++indexProperty < _sortPropertiesPriority.Count)
            {
                resultCompare = CompareByFields(obj1, obj2, indexProperty);
            }

            return resultCompare;

        }

        /// <summary>
        /// Быстрая сортировка с учетом приоритета колонок.
        /// </summary>
        private void QuickOrderRows()
        {
            StartOperation(() =>
            {
                _sortPropertiesPriority = new List<PropertyInfo>();
                foreach (
                    var column in
                        Columns.OrderByDescending(p => p.SortPriority)
                            .Select(p => ((Binding)p.DisplayMemberBinding).Path.Path))
                {
                    if (Rows.Count > 0)
                        _sortPropertiesPriority.Add(Rows[0].GetType()
                            .GetProperty(column, BindingFlags.Instance | BindingFlags.Public));
                }

                _rows =
                    new ObservableCollectionWithMaxLenElement<object>(QuickSort<Object>.StartSort(Rows.ToArray(),
                        SortOrderRows, _rowCompareFunc));

            }, _changeCallBackAction);
        }

        /// <summary>
        /// Запуск действия в новом потоке.
        /// </summary>
        /// <param name="workAction"></param>
        /// <param name="afterWorkAction"></param>
        private void StartOperation(Action workAction, Action afterWorkAction)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (sender, args) => workAction();
            worker.RunWorkerCompleted += (sender, args) => afterWorkAction();
            worker.RunWorkerAsync();
        }

        //todo delete
        /// <summary>
        /// Сортировка строк с учетом приоритета колонок.
        /// Примечание: работает медленней чем быстрая сортировка.
        /// </summary>
        private void ExpressionTreeOrderRows()
        {
            _rows = new ObservableCollectionWithMaxLenElement<object>(Rows.ExpressionTreeOrderWithPriority(Columns.Cast<GridViewColumn>(), SortOrderRows));
            _changeCallBackAction();
        }

        private void FilterRows()
        {
            
        }

        
        
    }
    
}
