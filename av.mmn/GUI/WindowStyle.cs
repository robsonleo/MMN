using System;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace GUI
{
    internal static class LocalExtensions
    {
        public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
        {
            Window window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as Window;
            if (window != null) action(window);
        }

        public static IntPtr GetWindowHandle(this Window window)
        {
            WindowInteropHelper helper = new WindowInteropHelper(window);
            return helper.Handle;
        }
    }

    public partial class Vs2012WindowStyle
    {
        void IconMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.ClickCount > 1)
                //sender.ForWindowFromTemplate(SystemCommands.CloseWindow);
        }

        void IconMouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null)
                return;
            var point = element.PointToScreen(new Point(element.ActualWidth / 2, element.ActualHeight));
            //sender.ForWindowFromTemplate(w => SystemCommands.ShowSystemMenu(w, point));
        }

        void WindowLoaded(object sender, RoutedEventArgs e)
        {
            ((Window)sender).StateChanged += WindowStateChanged;
            WindowStateChanged(sender, EventArgs.Empty);
        }

        void WindowStateChanged(object sender, EventArgs e)
        {
            var w = ((Window)sender);
            var handle = w.GetWindowHandle();
            var containerBorder = (Border)w.Template.FindName("PART_Container", w);

            if (w.WindowState == WindowState.Maximized)
            {
                // Make sure window doesn't overlap with the taskbar.
                //todo uncoment
                //var screen = Screen.FromHandle(handle);
                //if (screen.Primary)
                //{
                //    containerBorder.Padding = new Thickness(
                //        SystemParameters.WorkArea.Left + 7,
                //        SystemParameters.WorkArea.Top + 7,
                //        (SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right) + 7,
                //        (SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Bottom) + 5);
                //}
            }
            else
            {
                containerBorder.Padding = new Thickness(7, 7, 7, 5);
            }
        }

        void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            //sender.ForWindowFromTemplate(SystemCommands.CloseWindow);
        }

        void MinButtonClick(object sender, RoutedEventArgs e)
        {
            //sender.ForWindowFromTemplate(SystemCommands.MinimizeWindow);
        }

        void MaxButtonClick(object sender, RoutedEventArgs e)
        {
            //sender.ForWindowFromTemplate(w =>
            //{
            //    if (w.WindowState == WindowState.Maximized) SystemCommands.RestoreWindow(w);
            //    else SystemCommands.MaximizeWindow(w);
            //});
        }
        /// <summary>
        /// Отодвигание скроллбара документов
        /// </summary>
        public static bool FixScrollBarMargin(object sender, RoutedEventArgs args)
        {
            int childCount = VisualTreeHelper.GetChildrenCount((DependencyObject)sender);
            while (childCount > 0)
            {
                var scrollBar = (FrameworkElement)VisualTreeHelper.GetChild((DependencyObject)sender, --childCount);
                if (scrollBar.Name == "PART_VerticalScrollBar")
                {
                    scrollBar.Margin = new Thickness(0, 0, -27, 0);
                    return true;
                }

                if (FixScrollBarMargin(scrollBar, args))
                    return true;
            }
            return false;
        }
    }
}
