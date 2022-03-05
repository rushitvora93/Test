using System.Windows;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.View.Controls
{
    public class ExtendedHeaderGroupBox : GroupBox
    {
        public static readonly DependencyProperty TopRightCornerObjectProperty =
            DependencyProperty.Register(nameof(TopRightCornerObject), typeof(object), typeof(ExtendedHeaderGroupBox));
        public object TopRightCornerObject
        {
            get { return (object)GetValue(TopRightCornerObjectProperty); }
            set { SetValue(TopRightCornerObjectProperty, value); }
        }
    }
}
