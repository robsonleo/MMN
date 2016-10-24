using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using av.mmn.gui.View.MainWindow;

namespace antonov.mmn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            ShowUserInterface();
        }

        private void App_Exit(object sender, ExitEventArgs e){}
        
        private void ShowUserInterface()
        {
            MainWindow form = new MainWindow();
            form.ShowDialog();
        }
    }
}
