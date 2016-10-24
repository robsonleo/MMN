using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace av.mmn.gui.View.Tables.UniversalTable
{
    public partial class TableView : UserControl
    {
        public TableView()
        {
            InitializeComponent();
            WidthCell = 10;
        }


        public static readonly DependencyProperty WidthCellProperty = DependencyProperty.Register(
            "WidthCell", typeof(int), typeof(TableView), new FrameworkPropertyMetadata(50,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        public int WidthCell
        {
            get { return (int)GetValue(WidthCellProperty); }
            set { SetValue(WidthCellProperty, value); }
        }

        private string _tableName;
        public string TableName { get { return _tableName ?? InitialTableName(); } }

        private ObservableCollection<List<string>> _rows;
        public ObservableCollection<List<string>> Rows { get { return _rows ?? (_rows = InitialRows()); } }

        private ObservableCollection<string> _columns;
        public ObservableCollection<string> Columns { get { return _columns ?? (_columns = InitialColumns()); } }


        private string InitialTableName()
        {
            return "Table name";
        }

        private ObservableCollection<List<string>> InitialRows()
        {
            ObservableCollection<List<string>> rows = new ObservableCollection<List<string>>() { new List<string> { "с1трока 1", "wf" }, new List<string> { "с2трока 2", "wf 2" }, new List<string> { "с3трока 2", "wf 2" } };
            return rows;
        }

        private ObservableCollection<string> InitialColumns()
        {
            ObservableCollection<string> columns = new ObservableCollection<string>(){"№ ЛИН", "№ документа", "ПИМ", "Обозначение", "Масса", "Цех-потр.",
                "Шифр материала", "ПРО", "Длина", "Ширина", "Толщина", "Норма расхода", " Единица измерения", "Количество на заготовку", "Кол. образцов (КДОНДО)",
                "ШПИ", "Изделия", "Серия с", "Серия по", "Дата", "Пользователь"};
            return columns;
        }

        public int WidthColumn
        {
            get
            {
                int maxLength = _rows.Select(row => row[0].Length).Concat(new[] { 0 }).Max();
                return maxLength * 10;
            }
        }

        public int WidthBorder
        {
            get { return WidthColumn; }
        }


        private String _user;
        public String User { get { return _user ?? (_user = InitialUser()); } }

        private DateTime _date;
        public DateTime Date { get { return _date == DateTime.MinValue ? (_date = InitialDate()) : _date; } }

        private int _page;
        public int Page { get { return _page > 0 ? _page : (_page = InitialPage()); } }

        private int _pages;
        public int Pages { get { return _pages >= _page ? _pages : (_pages = InitialPages()); } }


        private string InitialUser()
        {
            return "pr074061";
        }

        private DateTime InitialDate()
        {
            return DateTime.Now;
        }

        private int InitialPage()
        {
            return 1;
        }

        private int InitialPages()
        {
            return 100;
        }
    }
}
