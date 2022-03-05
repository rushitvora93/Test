using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FrameworksAndDrivers.Gui.Wpf.View.Controls
{
    /// <summary>
    /// Interaction logic for GlobalTreeGrid.xaml
    /// </summary>
    public partial class CollapsibleGrid : Grid, INotifyPropertyChanged
    {
        
        private object _collapsiblePanel;
        public object CollapsiblePanel
        {
            get => _collapsiblePanel;
            set
            {
                _collapsiblePanel = value;
                if(_collapsiblePanel is IDefaultColumnWidth defaultWidth)
                {
                    // defaultWidth.GetDefaultColumn() returns the width without the FoldButtonWidth
                    FoldableColumn.Width = new GridLength(defaultWidth.GetDefaultColumn() + FoldableColumn.FoldedWidth, GridUnitType.Star);
                }
                RaisePropertyChanged(nameof(CollapsiblePanel));
            }
        }

        private object _contentPanel;
        public object ContentPanel
        {
            get => _contentPanel;
            set
            {
                _contentPanel = value;
                RaisePropertyChanged(nameof(ContentPanel));
            }
        }

        public double CollapsibleGridWidth
        {
            get { return FoldableColumn.Width.Value; }
            set { FoldableColumn.Width = new GridLength(value, GridUnitType.Star); }
        }

        public CollapsibleGrid()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
