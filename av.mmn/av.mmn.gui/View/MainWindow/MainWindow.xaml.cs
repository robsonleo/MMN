using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using av.mmn.gui.View.Tables.UniversalTable;

namespace av.mmn.gui.View.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //todo delete after test
            var list =new List<Object>();
            for(int i=0;i<100000;i++)
            { list.Add(new MockRow());}
            Data = list;
        }

        //todo delete after test
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data", typeof(IEnumerable<Object>), typeof(MainWindow), new PropertyMetadata(default(IEnumerable<Object>)));

        //todo delete after test
        public IEnumerable<Object> Data
        {
            get { return (IEnumerable<Object>) GetValue(DataProperty); }
            set { SetValue(DataProperty, value);}
        }
    }


    //todo delete after test
    public class MockRow
    {
        private static int count = 0;
        static readonly Random r=new Random();

        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }

        [InfoField("User", "Пользователь")]
        public string Field7 { get; set; }

        [InfoField("Date")]
        public DateTime DAte { get { return DateTime.Now; } }

        public MockRow()
        {
            Field1 = "Field 1." + r.Next(15);
            Field2 = "Field 2." + r.Next(15);
            Field3 = "Field 3." + r.Next(15);
            Field4 = "Field 4." + r.Next(15);
            Field5 = "Field 5." + r.Next(15);
            Field6 = "Field 6." + r.Next(15);
            Field7 = "Field 7." + r.Next(15);
            count++;
        }
    }

    
}
