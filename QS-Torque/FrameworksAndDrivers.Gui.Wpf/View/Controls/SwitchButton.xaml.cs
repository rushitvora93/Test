using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;

namespace FrameworksAndDrivers.Gui.Wpf.View.Controls
{
    /// <summary>
    /// Interaction logic for SwitchButton.xaml
    /// </summary>
    public partial class SwitchButton : UserControl
    {
        private static readonly DependencyProperty LeftTextProperty = DependencyProperty.Register("LeftText", typeof(string), typeof(SwitchButton), new PropertyMetadata(""));
        public string LeftText
        {
            get { return (string)this.GetValue(LeftTextProperty); }
            set { this.SetValue(LeftTextProperty, value); }
        }

        private static readonly DependencyProperty RightTextProperty = DependencyProperty.Register("RightText", typeof(string), typeof(SwitchButton), new PropertyMetadata(""));
        public string RightText
        {
            get { return (string)this.GetValue(RightTextProperty); }
            set { this.SetValue(RightTextProperty, value); }
        }

        private static readonly DependencyProperty IsLeftCheckedProperty = DependencyProperty.Register("IsLeftChecked", typeof(bool), typeof(SwitchButton), new PropertyMetadata(true, IsLeftCheckedCallback));
        public bool IsLeftChecked
        {
            get { return (bool)this.GetValue(IsLeftCheckedProperty); }
            set { this.SetValue(IsLeftCheckedProperty, value); }
        }
        private static void IsLeftCheckedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SwitchButton).IsRightChecked = !((bool)e.NewValue);
            (d as SwitchButton).LeftButton.IsEnabled = !((bool)e.NewValue);
            (d as SwitchButton).RightButton.IsEnabled = ((bool)e.NewValue);
        }

        private static readonly DependencyProperty IsRightCheckedProperty = DependencyProperty.Register("IsRightChecked", typeof(bool), typeof(SwitchButton), new PropertyMetadata(false, IsRightCheckedCallback));
        public bool IsRightChecked
        {
            get { return (bool)this.GetValue(IsRightCheckedProperty); }
            set { this.SetValue(IsRightCheckedProperty, value); }
        }
        private static void IsRightCheckedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SwitchButton).IsLeftChecked = !((bool)e.NewValue);
            (d as SwitchButton).RightButton.IsEnabled = !((bool)e.NewValue);
            (d as SwitchButton).LeftButton.IsEnabled = ((bool)e.NewValue);
        }


        public event RoutedEventHandler LeftClick
        {
            add { LeftButton.Click += value; }
            remove { LeftButton.Click -= value; }
        }

        public event RoutedEventHandler RightClick
        {
            add { RightButton.Click += value; }
            remove{ RightButton.Click -= value; }
        }


        private static readonly DependencyProperty RightButtonCommandProperty = DependencyProperty.Register("RightButtonCommand", typeof(ICommand), typeof(SwitchButton), new PropertyMetadata(null));
        public ICommand RightButtonCommand
        {
            get { return (ICommand)GetValue(RightButtonCommandProperty); }
            set { SetValue(RightButtonCommandProperty, value); }
        }


        private static readonly DependencyProperty LeftButtonCommandProperty = DependencyProperty.Register("LeftButtonCommand", typeof(ICommand), typeof(SwitchButton), new PropertyMetadata(null));
        public ICommand LeftButtonCommand
        {
            get { return (ICommand)GetValue(LeftButtonCommandProperty); }
            set { SetValue(LeftButtonCommandProperty, value); }
        }

        public SwitchButton()
        {
            InitializeComponent();
        }
    }
}
