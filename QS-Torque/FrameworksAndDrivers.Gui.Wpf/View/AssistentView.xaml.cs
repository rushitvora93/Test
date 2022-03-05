using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using TextBox = System.Windows.Controls.TextBox;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    class AssistantValueListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is DateTime dt ? dt.ToShortDateString() : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public interface IAssistentView
    {
        event EventHandler EndOfAssistent;

        Dispatcher Dispatcher{ get; }
        AssistentViewModel ViewModel{ get; }

        void SetParentPlan(ParentAssistentPlan plan);
        bool? ShowDialog();
    }
    /// <summary>
    /// Interaction logic for AssistentView.xaml
    /// </summary>
    public partial class AssistentView : Window, ICanShowDialog, IAssistentView
    {
        public AssistentViewModel ViewModel { get; private set; }

        #region Events
        public event EventHandler EndOfAssistent
        {
            add { ViewModel.EndOfAssistent += value; }
            remove { ViewModel.EndOfAssistent -= value; }
        }
        #endregion


        #region Methods
        public void SetParentPlan(ParentAssistentPlan plan)
        {
            ViewModel.SetParentAssistentPlan(plan);
        }

        public void ShowMessageBox(MessageBoxEventArgs args)
        {
            args.Show();
        }
        #endregion


        #region Event-Handler
        private void ViewModel_MessageBoxRequest(object sender, EventArgs.MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void TextBlockAssistentValue_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (AssistentItemValueColumn.ActualWidth < e.NewSize.Width + 15)
            {
                AssistentItemValueColumn.Width = e.NewSize.Width + 15;
            }
        }

        private void ButtonCancle_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void InputControl_Loaded(object sender, System.EventArgs e)
        {
            (sender as FrameworkElement).Focus();
            (sender as TextBox)?.SelectAll();
        }

        private void InputControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                ViewModel.NextCommand.Invoke(null);
                e.Handled = true;
            }
        }
        #endregion

        
        public AssistentView(string description)
        {
            InitializeComponent();
            ViewModel = new AssistentViewModel() { Description = description };
            this.DataContext = ViewModel;
            ViewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
            ViewModel.EndOfAssistent += (s, e) => this.DialogResult = true;
        }
    }
}
