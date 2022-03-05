using FrameworksAndDrivers.Gui.Wpf.ViewModel.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using FrameworksAndDrivers.Localization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace FrameworksAndDrivers.Gui.Wpf.View.Controls
{
    public class GloablTreeItem : TreeViewItem
    {
        public static readonly DependencyProperty IsCurrentSelectedItemProperty =
            DependencyProperty.Register("IsCurrentSelectedItem", typeof(bool), typeof(GloablTreeItem));
        public bool IsCurrentSelectedItem
        {
            get { return (bool)GetValue(IsCurrentSelectedItemProperty); }
            set { SetValue(IsCurrentSelectedItemProperty, value); }
        }
    }


    /// <summary>
    /// Interaction logic for GlobalTree.xaml
    /// </summary>
    public partial class GlobalTree : UserControl
    {

        public event EventHandler<UserControl> TreeWindowSelectionChanged;

		private LocalizationWrapper _localization;
		private IStartUp _startUp;

        public GloablTreeItem LastSelectedItem { get; private set; }
        public GloablTreeItem SelectedItem { get; private set; }
        

        #region Methods
        protected virtual void RaiseTreeWindowSelectionChanged(UserControl e)
        {
            TreeWindowSelectionChanged?.Invoke(this, e);
        }

		internal void SetLocalizationWrapper(LocalizationWrapper localization)
		{
			_localization = localization;
			(DataContext as GlobalTreeViewModel).SetLocalizationWrapper(_localization);
			_localization.Subscribe(DataContext as GlobalTreeViewModel);
		}

		public void SetStartUp(IStartUp startUp)
		{
			_startUp = startUp;
			(DataContext as GlobalTreeViewModel).SetStartUp(_startUp);
		}

        public void SetViewModel(GlobalTreeViewModel globalTreeViewModel)
        {
            DataContext = globalTreeViewModel;
            globalTreeViewModel.TreeWindowSelectionChanged += (obj, e) => RaiseTreeWindowSelectionChanged(e);
            globalTreeViewModel.LoadSessionInformation();
        }

        private void OpenSingleBaseTreeItem(TreeViewItem treeViewItem)
        {
            if(treeViewItem.IsExpanded && (this.DataContext as GlobalTreeViewModel).IsTreeExpanded)
            {
                treeViewItem.IsExpanded = false;
                return;
            }

            var previousSelected = SelectedItem;

            foreach (TreeViewItem i in MegamainSubmoduleSelectorTree.Items)
            {
                i.IsExpanded = false;
            }

            treeViewItem.IsExpanded = true;

            (this.DataContext as GlobalTreeViewModel).ExpandTreeCommand.Invoke(null);


            if (previousSelected != null)
            {
                previousSelected.IsCurrentSelectedItem = true; 
            }
        }

        public void SelectPreviousTreeItemAgain()
        {
            SelectedItem.IsCurrentSelectedItem = false;
            LastSelectedItem.IsCurrentSelectedItem = true;

            var val = SelectedItem;
            SelectedItem = LastSelectedItem;
            LastSelectedItem = val;
        }
        #endregion


        #region Event-Handler
        private void HighlightButton_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject element = sender as DependencyObject;
            TreeViewItem treeViewItem = sender as TreeViewItem;

            // Find Ancester: TreeViewItem
            while (!(element is TreeView))
            {
                element = VisualTreeHelper.GetParent(element);
                if (element is TreeViewItem tvi)
                {
                    treeViewItem = tvi;
                    break;
                }
            }

            if (treeViewItem is GloablTreeItem globalTvi)
            {
                if (SelectedItem != null)
                {
                    SelectedItem.IsCurrentSelectedItem = false; 
                }

                globalTvi.IsCurrentSelectedItem = true;

                if (LastSelectedItem != SelectedItem)
                {
                    LastSelectedItem = SelectedItem; 
                }

                SelectedItem = globalTvi;
            }

            if (treeViewItem.Items.Count == 0)
            {
                (this.DataContext as GlobalTreeViewModel).CollapseTreeCommand.Invoke(null); 
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HighlightButton_Click(TviHomeButton, null);
        }

        private void MasterDataButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSingleBaseTreeItem(TreeViewItemMasterData);
        }

        private void CommunicationButton_Click(object sender, RoutedEventArgs e)
        {
            HighlightButton_Click(TreeViewItemCommunication, null);
        }
        
        private void TestPlanningButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSingleBaseTreeItem(TreeViewItemTestPlanning);
        }

        private void EvaluationButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSingleBaseTreeItem(TreeViewItemEvaluation);
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            HighlightButton_Click(TreeViewItemStatistics, null);
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSingleBaseTreeItem(TreeViewItemHistory);
        }

        private void AdministrationButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSingleBaseTreeItem(TreeViewItemAdministration);
        }

        private void ModulesExtrasButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSingleBaseTreeItem(TreeViewItemModulesExtras);
        }

        private void OpenCloseParentTreeViewItem_Click(object sender, RoutedEventArgs e)
        {
            DependencyObject element = sender as DependencyObject;
            TreeViewItem treeViewItem = sender as TreeViewItem;

            // Find Ancester: TreeViewItem
            while (!(element is TreeView))
            {
                element = VisualTreeHelper.GetParent(element);
                if (element is TreeViewItem tvi)
                {
                    treeViewItem = tvi;
                    break;
                }
            }

            treeViewItem.IsExpanded = !treeViewItem.IsExpanded;
        }

        private void GlobalTreeViewItem_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key != Key.Enter || (sender as TreeViewItem)?.IsFocused != true)
            {
                return;
            }

            var button = (sender as TreeViewItem)?.Header as Button;

            if(button == null)
            {
                return;
            }

            // Press button from code
            ButtonAutomationPeer peer = new ButtonAutomationPeer(button);
            IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            invokeProv.Invoke();
        }
        #endregion


        public GlobalTree()
        {
            InitializeComponent();

            TviHome.IsCurrentSelectedItem = true;
            SelectedItem = TviHome;
        }
    }
}
