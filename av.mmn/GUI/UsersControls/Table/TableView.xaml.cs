//using UserControl = GUI.UsersControls.ControlPanel.UserControl;

using System.Windows;
using System.Windows.Controls;

namespace GUI.UsersControls.Table
{
    /// <summary>
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class TableView : UserControl
    {
        public TableView()
        {
            InitializeComponent();
        }

        private void On_DataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
