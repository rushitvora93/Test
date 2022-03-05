using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.View.Controls;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Gui.Wpf.ViewModel.Controls;
using FrameworksAndDrivers.Localization;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window, INotifyPropertyChanged
    {
        public enum ResultCode
        {
            EXIT,
            RELOAD,
            LOGIN,
        }

        #region Properties
        private UserControl _mainContent;
        public UserControl MainContent
        {
            get => _mainContent;
            set
            {
                _mainContent = value;
                RaisePropertyChanged(nameof(MainContent));
            }
        }

        public ResultCode Result { get; set; }
        #endregion


        #region Event-Handler
        private void GlobalTree_OnTreeWindowSelectionChanged(object sender, UserControl e)
        {
            ChangeControls(e);
        }

        private void NotMegaMainSubModuleSelector_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(!(sender is GlobalTree) || !(sender is TreeViewItem))
            {
                (MegaMainSubmoduleSelector.DataContext as GlobalTreeViewModel).CollapseTreeCommand.Invoke(null);
            }
        }
        
        private void ChangeControls(UserControl newControl)
        {
            if (MainContent is ICanClose canClose)
            {
                if (!canClose.CanClose())
                {
                    MegaMainSubmoduleSelector.SelectPreviousTreeItemAgain();
                    return;
                }
            }
            if (MainContent is IDisposable disposable)
            {
                disposable.Dispose();
                GC.Collect();
            }
            MainContent = newControl;
        }

        private void MainView_OnClosing(object sender, CancelEventArgs e)
        {
            if (!(MainContent is ICanClose canClose))
            {
                return;
            }
            if (!canClose.CanClose())
            {
                e.Cancel = true;
            }
        }

        private void GlobalToolBar_LogoutClick(object sender, RoutedEventArgs e)
        {
            if (MainContent is ICanClose canClose)
            {
                if (!canClose.CanClose())
                {
                    return;
                }
            }

            var dataContext = DataContext as MainViewViewModel;
            if (dataContext != null)
            {
                dataContext.Logout();
            }
        }
        private void LogoutCompletedEventHandler(object sender, System.EventArgs e)
        {
            this.Result = ResultCode.LOGIN;
            this.Close();
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public MainView(LocalizationWrapper localizationWrapper,MainViewViewModel viewModel, IStartUp startUp, GlobalToolBarViewModel toolbarViewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.LogoutCompletedEventHandler += LogoutCompletedEventHandler;
            MainContent = new StartView();

            globalToolBar.SetLocalizationWrapper(localizationWrapper);
            globalToolBar.SetViewModel(toolbarViewModel);
            globalToolBar.SetStartUp(startUp);

            MegaMainSubmoduleSelector.SetViewModel(startUp.OpenGlobalTree());
            MegaMainSubmoduleSelector.SetLocalizationWrapper(localizationWrapper);
            MegaMainSubmoduleSelector.SetStartUp(startUp);
        }
    }
}
