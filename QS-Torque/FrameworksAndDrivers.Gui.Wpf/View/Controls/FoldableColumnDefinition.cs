using System.Windows;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.View.Controls
{
    /// <summary>
    /// Allows the user to resize the column with a GridSplitter and fold/unfold it 
    /// (not possible usually, because you can't set the with of a column with a Trigger after the column was resized with a GridSplitter)
    /// </summary>
    public class FoldableColumnDefinition : ColumnDefinition
    {
        // IsFolded
        private static readonly DependencyProperty IsFoldedProperty =
            DependencyProperty.Register("IsFolded", typeof(bool), typeof(FoldableColumnDefinition), new PropertyMetadata(false, new PropertyChangedCallback(IsFoldedChangedCallback)));
        public bool IsFolded
        {
            get { return (bool)GetValue(IsFoldedProperty); }
            set { SetValue(IsFoldedProperty, value); }
        }
        private static void IsFoldedChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cd = d as FoldableColumnDefinition;

            if ((bool)e.NewValue)
            {
                // Set Width to FoldedWidth if the Column should be folded
                cd._unfoldedWidth = cd.Width.Value;
                cd.Width = new GridLength(cd.FoldedWidth);
            }
            else
            {
                // Set Width to the Value before the Column was folded
                cd.Width = new GridLength(cd._unfoldedWidth, GridUnitType.Star);
            }
        }

        //  FoldedWidth (usually the Width of the FoldButton)
        private static readonly DependencyProperty FoldedWidthProperty =
            DependencyProperty.Register("FoldedWidth", typeof(double), typeof(FoldableColumnDefinition), new PropertyMetadata(0.0));
        public double FoldedWidth
        {
            get { return (double)GetValue(FoldedWidthProperty); }
            set { SetValue(FoldedWidthProperty, value); }
        }


        // ColumnWidth before the Column was folded
        private double _unfoldedWidth;
    }
}
