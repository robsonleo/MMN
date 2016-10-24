using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
//using GUI.UsersControls.Table.LinControls;

namespace GUI.XamlExtensions
{
    //internal class InternalRowFocuser
    //{
    //    private readonly int _focusIndex;
    //    //private readonly ScrollDirection _scrollDirection;


    //    //public InternalRowFocuser(int index, ScrollDirection direction = ScrollDirection.WithoutScroll)
    //    //{
    //    //    _focusIndex = index;
    //    //    _scrollDirection = direction;
    //    //}

    //    public void FocusRow(object sender, EventArgs e)
    //    {
    //        //ItemContainerGenerator generator = (ItemContainerGenerator)sender;

    //        //if (generator.Status != GeneratorStatus.ContainersGenerated)
    //        //    return;

    //        //PropertyInfo hostProperty = generator.GetType().GetProperty("Host", BindingFlags.NonPublic | BindingFlags.Instance);
    //        //ItemsControl itemsControl = (ItemsControl)hostProperty.GetValue(generator);

    //        //ScrollViewer scrollViewer = (ScrollViewer)itemsControl.Template.FindName("PART_ScrollViewer", itemsControl);
    //        //if (scrollViewer != null)
    //        //    switch (_scrollDirection)
    //        //    {
    //        //        case ScrollDirection.Home:
    //        //            scrollViewer.ScrollToHome();
    //        //            break;
    //        //        case ScrollDirection.End:
    //        //            scrollViewer.ScrollToEnd();
    //        //            break;
    //        //        case ScrollDirection.WithoutScroll:
    //        //            break;
    //        //        default:
    //        //            throw new ArgumentOutOfRangeException();
    //        //    }

    //        //ContentPresenter child = (ContentPresenter)generator.ContainerFromIndex(_focusIndex);

    //        //if (child == null)
    //        //    return;

    //        //Grid grandSon = (Grid)child.ContentTemplate.FindName("PART_RowGrid", child);
    //        //UIElement grandSonsChild = grandSon.Children.Cast<UIElement>().First(i => Grid.GetColumn(i) == 0);

    //        //grandSonsChild.Focus();
    //        //Keyboard.Focus(grandSonsChild);

    //        //generator.StatusChanged -= FocusRow;
    //    }
    //}
}
