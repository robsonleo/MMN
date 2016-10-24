using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using av.mmn.gui.Annotations;
using av.mmn.gui.View.MainWindow;
using CustomFunctions;
using GalaSoft.MvvmLight.Command;


namespace av.mmn.gui.View.Tables.UniversalTable
{
    /// <summary>
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class TableView : UserControl, INotifyPropertyChanged
    {
#region DEPENDENCY PROPERTY



        public static readonly DependencyProperty TableDataSourceProperty = DependencyProperty.Register(
            "TableDataSource", typeof (IEnumerable<Object>), typeof (TableView),
            new FrameworkPropertyMetadata()
            {
                BindsTwoWayByDefault = true,
                PropertyChangedCallback =
                    ((o, args) =>
                    {
                        ((TableView)o).Initializer();
                    })
            });

        public static readonly DependencyProperty JoinDataSourceProperty = DependencyProperty.Register(
            "JoinDataSource", typeof(bool), typeof(TableView), new PropertyMetadata(false));

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
            "Rows", typeof(ObservableCollectionWithMaxLenElement<object>), typeof(TableView), new PropertyMetadata(new ObservableCollectionWithMaxLenElement<object>()));

       public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(
           "Columns", typeof(ObservableCollection<GridViewColumnDescription>), typeof(TableView), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty TableNameProperty = DependencyProperty.Register(
           "TableName", typeof(string), typeof(TableView), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty SelectedItemIndexProperty = DependencyProperty.Register(
            "SelectedItemIndexIndex", typeof (int), typeof (TableView), new PropertyMetadata(0));
        
        public static readonly DependencyProperty ItemsCountProperty = DependencyProperty.Register(
            "ItemsCount", typeof (int), typeof (TableView), new PropertyMetadata(default(int)));

        public static readonly DependencyProperty ShowDateEditProperty = DependencyProperty.Register(
            "ShowDateEdit", typeof (bool), typeof (TableView), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty ShowUserProperty = DependencyProperty.Register(
            "ShowUser", typeof (bool), typeof (TableView), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof (Object), typeof (TableView), new PropertyMetadata(default(Object)));

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems", typeof (IEnumerable<Object>), typeof (TableView), new PropertyMetadata(default(IEnumerable<Object>)));

        public static readonly DependencyProperty UserProperty = DependencyProperty.Register(
            "User", typeof (string), typeof (TableView), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty DateProperty = DependencyProperty.Register(
            "Data", typeof(string), typeof(TableView), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ShowNameHeadersByAttributeProperty = DependencyProperty.Register(
            "ShowNameHeadersByAttribute", typeof (bool), typeof (TableView), new PropertyMetadata(true));

        public static readonly DependencyProperty AlternateColorRowFirstProperty = DependencyProperty.Register(
            "AlternateColorRowFirst", typeof(Brush), typeof(TableView), new PropertyMetadata(Brushes.AliceBlue));

        public static readonly DependencyProperty AlternateColorRowSecondProperty = DependencyProperty.Register(
            "AlternateColorRowSecond", typeof(Brush), typeof(TableView), new PropertyMetadata(Brushes.AntiqueWhite));

        public static readonly DependencyProperty SelectedRowColorProperty = DependencyProperty.Register(
            "SelectedRowColor", typeof(Brush), typeof(TableView), new PropertyMetadata(Brushes.Blue));
        
        public static readonly DependencyProperty BorderRowColorProperty = DependencyProperty.Register(
            "BorderRowColor", typeof(Brush), typeof(TableView), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty SortOrderRowsProperty = DependencyProperty.Register(
           "SortOrderRows", typeof(SortOrder), typeof(TableView), new PropertyMetadata(default(SortOrder), (o, args) =>
           {
               ((TableView) o).Table.SortOrderRows = ((TableView) o).SortOrderRows;
           }));
        #endregion


        #region PROPERTY
        public IEnumerable<Object> TableDataSource
        {
            get { return (IEnumerable<Object>) GetValue(TableDataSourceProperty); }
            set
            {
                SetValue(TableDataSourceProperty, value);
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Добавляет новый источник к старому
        /// </summary>
        public bool JoinDataSource
        {
            get { return (bool) GetValue(JoinDataSourceProperty); }
            set
            {
                SetValue(JoinDataSourceProperty, value);
                OnPropertyChanged();
            }
        }
        
        public ObservableCollectionWithMaxLenElement<object> Rows
        {
            get
            {
                return (ObservableCollectionWithMaxLenElement<object>) GetValue(RowsProperty);
            }
            private set
            {
                SetValue(RowsProperty, value);
                SetValue(ItemsCountProperty, Table.Rows.Count);
                ChangeStateComponent(Cursors.Arrow, true);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty IsActivatedCommandProperty = DependencyProperty.Register(
            nameof(IsActivatedCommand), typeof (bool), typeof (TableView), new PropertyMetadata(default(bool)));

        public bool IsActivatedCommand
        {
            get { return (bool) GetValue(IsActivatedCommandProperty); }
            set
            {
                SetValue(IsActivatedCommandProperty, value); 
                OnPropertyChanged();
            }
        }
        public void ChangeStateComponent(Cursor cursor, bool enabled)
        {
            Cursor = cursor;
            IsActivatedCommand = enabled;
        }

        public ObservableCollection<GridViewColumnDescription> Columns => _columns ?? new ObservableCollection<GridViewColumnDescription>();

        public string TableName
        {
            get { return (string) GetValue(TableNameProperty); }
            set
            {
                SetValue(TableNameProperty, value);
                OnPropertyChanged();
            }
        }

        public int SelectedItemIndex
        {
            get { return (int)GetValue(SelectedItemIndexProperty); }
            set
            {
                SetValue(SelectedItemIndexProperty, value);
                OnPropertyChanged();
            }
        }

        public int ItemsCount => ((ObservableCollectionWithMaxLenElement<Object>) GetValue(RowsProperty)).Count;

        public bool ShowDateEdit
        {
            get { return (bool)GetValue(ShowDateEditProperty); }
            set
            {
                SetValue(ShowDateEditProperty, value); 
                OnPropertyChanged();
            }
        }

        public bool ShowUser
        {
            get { return (bool)GetValue(ShowUserProperty); }
            set
            {
                SetValue(ShowUserProperty, value); 
                OnPropertyChanged();
            }
        }

        public Object SelectedItem
        {
            get { return (Object)GetValue(SelectedItemProperty); }
            set
            {
                SetValue(SelectedItemProperty, value);
                OnPropertyChanged();
            }
        }

        public IEnumerable<Object> SelectedItems
        {
            get { return (IEnumerable<Object>)GetValue(SelectedItemsProperty); }
            set
            {
                SetValue(SelectedItemsProperty, value);
                OnPropertyChanged();
            }
        }

        public string User
        {
            get { return (string)GetValue(UserProperty); }
            set { SetValue(UserProperty, value); }
        }

        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public bool ShowNameHeadersByAttribute
        {
            get { return (bool)GetValue(ShowNameHeadersByAttributeProperty); }
            set
            {   SetValue(ShowNameHeadersByAttributeProperty, value);
                OnPropertyChanged();
            }
        }

        public Brush AlternateColorRowFirst
        {
            get { return (Brush)GetValue(AlternateColorRowFirstProperty); }
            set
            {
                SetValue(AlternateColorRowFirstProperty, value); 
                OnPropertyChanged();
            }
        }

        public Brush AlternateColorRowSecond
        {
            get { return (Brush)GetValue(AlternateColorRowSecondProperty); }
            set
            {
                SetValue(AlternateColorRowSecondProperty, value);
                OnPropertyChanged();
            }
        }

        public Brush SelectedRowColor
        {
            get { return (Brush)GetValue(SelectedRowColorProperty); }
            set
            {
                SetValue(SelectedRowColorProperty, value);
                OnPropertyChanged();
            }
        }

        public Brush BorderRowColor
        {
            get { return (Brush)GetValue(BorderRowColorProperty); }
            set
            {
                SetValue(BorderRowColorProperty, value);
                OnPropertyChanged();
            }
        }

       

        public SortOrder SortOrderRows
        {
            get { return (SortOrder) GetValue(SortOrderRowsProperty); }
            set
            {
                SetValue(SortOrderRowsProperty, value);
                ChangeStateComponent(Cursors.Wait, false);
                OnPropertyChanged();
            }
        }

        

        private ObservableCollection<GridViewColumnDescription> _columns;
        private ObservableCollectionWithMaxLenElement<object> _rows;

        protected TableViewModel _table;
        protected TableViewModel Table
        {
            get { return _table; }
            set
            {
                _table = value;
                Rows = value.Rows;
                SetValue(ItemsCountProperty, value.Rows.Count);
                
            }
        }




        #endregion





        public TableView()
        {
            InitializeComponent();   
            Loaded+= (sender, args) => Initializer();
        }

        protected void Initializer()
        {
            if(TableDataSource?.GetHashCode()==Table?.DataSource.GetHashCode()) return;
           
            
            Table=new TableViewModel(TableDataSource, () =>
            {
                Rows = Table.Rows;
                OnPropertyChanged(nameof(Rows));
            }, ChangeSortOrder, ShowNameHeadersByAttribute);
            CreateGridView();
        }

        private void CreateGridView()
        {
            GridView gr = JoinDataSource ? list.View as GridView : new GridView();
            if (gr == null) return;

            foreach (var column in Table.Columns)
            {
                column.HeaderTemplate = (DataTemplate) FindResource("HeaderTemplate");
                gr.Columns.Add(column);
            }
            
            list.View = gr;
        }
        
        

        //private ObservableCollection<string> InitialColumns()
        //{
        //    ObservableCollection<string> columns = new ObservableCollection<string>(){"№ ЛИН", "№ документа", "ПИМ", "Обозначение", "Масса", "Цех-потр.",
        //        "Шифр материала", "ПРО", "Длина", "Ширина", "Толщина", "Норма расхода", " Единица измерения", "Количество на заготовку", "Кол. образцов (КДОНДО)",
        //        "ШПИ", "Изделия", "Серия с", "Серия по", "Дата", "Пользователь"};
        //    return columns;
        //}

        public static readonly DependencyProperty SortActionProperty = DependencyProperty.Register(
            "SortAction", typeof (Action<Object, EventArgs>), typeof (TableView), new PropertyMetadata(default(Action<Object, EventArgs>)));

        public Action<Object, EventArgs> SortAction
        {
            get { return (Action<Object, EventArgs>) GetValue(SortActionProperty); }
            set { SetValue(SortActionProperty, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void List_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User = CustomReflection.GetValuePropertyByAttribute(SelectedItem,
                    typeof(InfoFieldAttribute), o => ((InfoFieldAttribute)o).Name == "User").ToString();

            Date = CustomReflection.GetValuePropertyByAttribute(SelectedItem,
                typeof(InfoFieldAttribute), o => ((InfoFieldAttribute)o).Name == "Date").ToString();
        }

        private Action ChangeSortOrder
        {
            get
            {
                return () => SortOrderRows = SortOrderRows == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
        }

        //public ICommand IncreaseSortPrioryity
        //{
        //    get
        //    {
        //        return new RelayCommand(() =>
        //        {

        //            //_sortPriority = ++_countSortPriority;
        //            //SortTableAction();
        //        });
        //    }
        //}

       //public Func<Object, Object, int> RowCompareFunc { get; set; }

       // private int Comparer(Object obj1, Object obj2)
       // {
       //     return CompareByFields(obj1, obj2);
       // }

       // private int CompareByFields(Object obj1, Object obj2, int indexProperty = 0)
       // {
       //     if (_sortPropertiesPriority?[indexProperty] == null) throw new ArgumentNullException(nameof(_sortPropertiesPriority));

       //     var sortProperty = _sortPropertiesPriority[indexProperty];

       //     var value1 = sortProperty.GetValue(obj1, null).ToString();
       //     var value2 = sortProperty.GetValue(obj2, null).ToString();

       //     int resultCompare = String.Compare(value1, value2);

       //     if (resultCompare == 0 && ++indexProperty < _sortPropertiesPriority.Count)
       //     {
       //         resultCompare = CompareByFields(obj1, obj2, indexProperty);
       //     }

       //     return resultCompare;

       // }

       // private List<PropertyInfo> _sortPropertiesPriority;

       // public SortOrder SortOrderRows { get; set; } = SortOrder.Ascending;

       // private void OrderRows()
       // {
       //     _sortPropertiesPriority = new List<PropertyInfo>();
       //     foreach (var column in Columns.OrderByDescending(p => p.SortPriority).Select(p => ((Binding)p.DisplayMemberBinding).Path.Path))
       //     {
       //         if (Rows.Count > 0)
       //             _sortPropertiesPriority.Add(Rows[0].GetType()
       //                 .GetProperty(column, BindingFlags.Instance | BindingFlags.Public));
       //     }
            
       //     Rows =
       //         new ObservableCollectionWithMaxLenElement<object>(QuickSort<Object>.StartSort(Rows.ToArray(),
       //             SortOrderRows, RowCompareFunc));
       //    _changeCallBackAction();
       // }

       // private Action _sortAction;
       // private Action _changeCallBackAction;

       // public TableViewModel(IEnumerable<object> collection, Action changeCallBackAction, Action sortActionCallBack, bool showNameHeaderByAttribute = true)
       // {
       //     _changeCallBackAction += changeCallBackAction;
       //     RowCompareFunc = Comparer;
       //     _sortAction += sortActionCallBack;
       //     _sortAction += OrderRows;
       //     DataSource = collection;
       //     _columns = new ObservableCollection<GridViewColumnDescription>();
       //     var col = collection == null ? null : collection.FirstOrDefault(p => p != null);
       //     if (col != null)
       //     {
       //         //_rowsMock=new ObservableCollection<object>(collection);
       //         _rows = new ObservableCollectionWithMaxLenElement<object>(collection);

       //         List<string> columnsBindingName = new List<string>(col.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.Name));

       //         foreach (var nameProperty in columnsBindingName)
       //         {
       //             var atr = col.GetType()
       //                 .GetProperty(nameProperty, BindingFlags.Public | BindingFlags.Instance)
       //                 .GetCustomAttributes(typeof(InfoFieldAttribute), true);
       //             string showedNameHeader = atr.Any() && showNameHeaderByAttribute ? ((InfoFieldAttribute)atr.First()).ShowName : nameProperty;

       //             _columns.Add(new GridViewColumnDescription(nameProperty, _sortAction, showedNameHeader,
       //                 Rows.GetMaxLengthColumn(showedNameHeader) * (int)Unit.Pixel(showedNameHeader.Length + 2).Value,
       //                 false, true));
       //         }
       //     }
       // }

        public static readonly DependencyProperty comProperty = DependencyProperty.Register(
            "com", typeof (ICommand), typeof (TableView), new PropertyMetadata(new RelayCommand(() => MessageBox.Show("tata"))));

        public ICommand com
        {
            get { return (ICommand) GetValue(comProperty); }
            set { SetValue(comProperty, value); }
        }
        private void EventSetter_OnHandler(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
