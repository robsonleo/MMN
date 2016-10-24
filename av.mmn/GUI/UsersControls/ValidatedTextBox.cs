using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using GUI.ViewModel;

namespace GUI.UsersControls
{
    public class ValidatedTextBox : TextBox
    {
        private static RowViewModelBase _lastRow;
        private static readonly SolidColorBrush[,] Brushes = {
            {
                new SolidColorBrush(), // nullable
                new SolidColorBrush(), // success
                new SolidColorBrush()  // error
            }, //без фокусу
            {
                new SolidColorBrush(),
                new SolidColorBrush(),
                new SolidColorBrush()
            }, //фокус на рядку
            {
                new SolidColorBrush(),
                new SolidColorBrush(),
                new SolidColorBrush()
            }  //фокус на полі
        };

        private static void UpdateBrushesColors(object sender, PropertyChangedEventArgs e)
        {
            Brushes[0, 0].Color = Properties.ColorSettings.Default.NullableColor;
            Brushes[0, 1].Color = Properties.ColorSettings.Default.ValidColor;
            Brushes[0, 2].Color = Properties.ColorSettings.Default.ErrorColor;

            Brushes[1, 0].Color = Properties.ColorSettings.Default.SelectedRowNullableColor;
            Brushes[1, 1].Color = Properties.ColorSettings.Default.SelectedRowValidColor;
            Brushes[1, 2].Color = Properties.ColorSettings.Default.SelectedRowErrorColor;

            Brushes[2, 0].Color = Properties.ColorSettings.Default.SelectedFieldNullableColor;
            Brushes[2, 1].Color = Properties.ColorSettings.Default.SelectedFieldValidColor;
            Brushes[2, 2].Color = Properties.ColorSettings.Default.SelectedFieldErrorColor;
        }

        /// <summary>
        /// Свойство зависимости для признака соответсвия правилам 
        /// </summary>
        public static readonly DependencyProperty IsValidProperty = DependencyProperty.Register("IsValid",
            typeof(bool?), typeof(ValidatedTextBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, IsValidChanged));

        /// <summary>
        /// Признак соответсвия правилам
        /// </summary>
        public bool? IsValid
        {
            get; set;
        }

        static ValidatedTextBox()
        {
            Properties.ColorSettings.Default.PropertyChanged += UpdateBrushesColors;

            UpdateBrushesColors(null, null);// Set Default Values
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ValidatedTextBox()
        {
            LostFocus += KtcTextBox_LostFocus;
            GotFocus += KtcTextBox_GotFocus;

            PreviewKeyDown += ValidatedTextBox_PreviewKeyDown;
            Initialized += OnInitialized;
            Loaded += KtcTextBox_Loaded;

            _focusVerticalMoveCommand = new RelayCommand<int>(FocusVerticalMoveCommandAction);
            _tkFocusNextCommand = new RelayCommand(TkFocusNextCommandAction);
        }

        private void KtcTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SheetStyle();

            ValidatedTextBox box = sender as ValidatedTextBox;
            var context = box?.DataContext as FieldViewModelBase;

            if (context != null)
            {
                var newRow = context.Row;

                if (_lastRow != newRow)
                {
                    //todo _lastRow?.LostFocus.Execute(null);
                    //todo newRow?.GotFocus.Execute(null);
                    _lastRow = newRow;
                }
            }

        }

        private void KtcTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SheetStyle();
        }

        private void OnInitialized(object sender, EventArgs eventArgs)
        {
            FrameworkElement parent = (FrameworkElement)Parent;
            parent.IsKeyboardFocusWithinChanged += ParentIsKeyboardFocusWithinChanged;
            parent.Initialized += ParentOnInitialized;

            //InputBindings.AddRange(new ValidatedTextBoxHotKeyManager(this).InputBindings);
        }

        private void KtcTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            SheetStyle();
        }

        private void ParentOnInitialized(object sender, EventArgs eventArgs)
        {
            FrameworkElement parent = (FrameworkElement)Parent;

            //if (parent.Resources.Contains("RowHotKeyManager"))
            //    InputBindings.AddRange(((HotKeyManagerBase)parent.Resources["RowHotKeyManager"]).InputBindings);
        }

        private void ParentIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SheetStyle();
        }

        private void SheetStyle()
        {
            bool? isValid = IsValid;
            bool isFocused = IsFocused;
            bool isFocusWithin = ((FrameworkElement)Parent).IsKeyboardFocusWithin;

            int x = 0, y;

            switch (isValid)
            {
                case false: y = 2; break;
                case true: y = 1; break;
                default: y = 0; break;
            }

            if (isFocused)
                x = 2;
            else if (isFocusWithin)
                x = 1;

            Background = Brushes[x, y];
        }

        private static void IsValidChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as ValidatedTextBox;
            if (box != null)
            {
                box.IsValid = (bool?)e.NewValue;
                box.SheetStyle();
            }
        }

        private void ValidatedTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsReadOnly || e.Key != Key.Decimal)
                return;

            e.Handled = true;

            int start = SelectionStart, len = SelectionLength;
            Text = Text.Insert(start, ".");
            SelectionStart = start + 1;
            SelectionLength = len;
        }

        private void FocusVerticalMoveCommandAction(int direction)
        {
            if (Parent is Grid)
            {
                var smallGrid = Parent as Grid;
                var presenter = VisualTreeHelper.GetParent(smallGrid) as ContentPresenter;

                if (presenter == null)
                    return;

                var panel = VisualTreeHelper.GetParent(presenter) as Panel;

                if (panel == null)
                    return;

                int height = panel.Children.Count;

                int x = Grid.GetColumn(this),
                    y = panel.Children.IndexOf(presenter);

                int newY = y + direction;
                if (newY >= 0 && newY < height)
                {
                    var nextGrid = VisualTreeHelper.GetChild(panel.Children[newY], 0) as Grid;
                    nextGrid?.Children[x].Focus();
                }
            }
        }

        private void TkFocusNextCommandAction()
        {
            var viewModel = DataContext as TkViewModel;
            if (viewModel != null && (viewModel.Row).IsLast)
            {
                viewModel.Row.InsertRowCommand.Execute(null);
            }
            else
            {
                UIElement element = Keyboard.FocusedElement as UIElement;
                element?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        /// <summary>
        /// Комманда перемещения фокуса между строками
        /// </summary>
        public ICommand FocusVerticalMoveCommand => _focusVerticalMoveCommand;
        private readonly RelayCommand<int> _focusVerticalMoveCommand;

        /// <summary>
        /// Комманда добавления новой строки по виходу из последнего поля
        /// </summary>
        public ICommand TkFocusNextCommand => _tkFocusNextCommand;
        private readonly RelayCommand _tkFocusNextCommand;
    }
}
