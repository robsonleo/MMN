using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using GUI.ViewModel;

namespace GUI.UsersControls.Table
{
    /// <summary>
    /// Interaction logic for Lin.xaml
    /// </summary>
    public partial class Lin : UserControl
    {
        public Lin()
        {
            InitializeComponent();
        }

        private void TurnOffIndicator(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
                return;

            Vs2012WindowStyle.FixScrollBarMargin(sender, e);

            ((DocumentViewModelBase)DataContext).IsBusy = false;
        }

        private void GridRow_OnInitialized(object sender, EventArgs e)
        {
            FrameworkElement grid = (FrameworkElement)sender;
            //grid.Resources.Add("RowHotKeyManager", new RowHotKeyManager(grid));
        }

        private void ItemsControl_OnInitialized(object sender, EventArgs e)
        {
            INotifyCollectionChanged collection = ItemsControl.DataContext as INotifyCollectionChanged;
            if (collection != null)
            {
                collection.CollectionChanged += RowsChanged;
            }
        }

        private void ItemsControl_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            INotifyCollectionChanged oldDocument = e.OldValue as INotifyCollectionChanged;
            if (oldDocument != null)
                oldDocument.CollectionChanged -= RowsChanged;

            INotifyCollectionChanged newDocument = e.NewValue as INotifyCollectionChanged;
            if (newDocument != null)
                newDocument.CollectionChanged += RowsChanged;
        }

        private void RowsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            int count = ((LinDocumentViewModel)sender).Count;
            ItemContainerGenerator generator = ItemsControl.ItemContainerGenerator;

            //InternalRowFocuser focuser;

            //switch (e.Action)
            //{
            //    case NotifyCollectionChangedAction.Add:
            //        focuser = new InternalRowFocuser(count - 1);
            //        generator.StatusChanged += focuser.FocusRow;
            //        (ItemsControl?.Template.FindName("PART_ScrollViewer", ItemsControl) as ScrollViewer)?.ScrollToEnd();
            //        break;
            //    case NotifyCollectionChangedAction.Remove:
            //        if (generator.Items.Count > 0)
            //        {
            //            focuser =
            //                new InternalRowFocuser(e.OldStartingIndex < count
            //                    ? e.OldStartingIndex
            //                    : count - 1);
            //            generator.StatusChanged += focuser.FocusRow;
            //        }
            //        break;
            //}
        }

        private void FocusFirstTextBox()
        {
            //FirstTextBox.Focus();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            FocusFirstTextBox();
        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FocusFirstTextBox();
        }
    }
}
