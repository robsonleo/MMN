using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CustomFunctions
{
    //todo нуждается в доработке
    /// <summary>
    /// ListView с возможностью деактивации елементов без их выключения
    /// </summary>
    public class CustomListView : ListView
    {
        protected int LastSelectedIndex;

        public static readonly DependencyProperty IsActivateProperty = DependencyProperty.Register(
            nameof(IsActivate), typeof(bool), typeof(CustomListView), new FrameworkPropertyMetadata()
            {
                BindsTwoWayByDefault = true,
                PropertyChangedCallback = (o, args) =>
                {
                    ((CustomListView)o).IsActivate = (bool)args.NewValue;
                }
            });


        public bool IsActivate
        {
            get { return (bool)GetValue(IsActivateProperty); }
            set
            {
                SetValue(IsActivateProperty, value);
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            if (!IsActivate)
                ((CustomListView)e.Source).SelectedIndex = ((CustomListView)e.Source).LastSelectedIndex;
            else
                LastSelectedIndex = ((CustomListView)e.Source).SelectedIndex;
        }

    }
}
