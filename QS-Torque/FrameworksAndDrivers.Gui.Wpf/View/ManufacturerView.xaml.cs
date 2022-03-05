using System;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.View.Dialogs;
using FrameworksAndDrivers.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for ManufacturerView.xaml
    /// </summary>
    public partial class ManufacturerView : UserControl, ICanClose, IDisposable
    {
        private ManufacturerViewModel _manufacturerViewModel;

        #region Event-Handler
        private void Manufacturer_MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void ManufacturerViewModelOnReferencesDialogRequest(object sender,ReferenceList e)
        {
            var dialog = new ViewReferencesDialog();
            dialog.ShowDialog(Window.GetWindow(this), new List<ReferenceList>{e});
        }
        #endregion
        
		public ManufacturerView(ManufacturerViewModel manufacturerViewModel, LocalizationWrapper localization)
		{
			InitializeComponent();
			DataGrid.LocalizationWrapper = localization;
			localization.Subscribe(DataGrid);
            _manufacturerViewModel = manufacturerViewModel;
            DataContext = manufacturerViewModel;
			manufacturerViewModel.registerEventHandler(Manufacturer_MessageBoxRequest);
			manufacturerViewModel.SetDispatcher(Dispatcher);
            manufacturerViewModel.ReferencesDialogRequest += ManufacturerViewModelOnReferencesDialogRequest;
        }

        public bool CanClose()
        {
            if (DataContext is ICanClose canClose)
            {
                return canClose.CanClose();
            }
            else
            {
                return true;
            }
        }

        public void Dispose()
        {
            DataGrid?.Dispose();
            _manufacturerViewModel.MessageBoxRequest -= Manufacturer_MessageBoxRequest;
            _manufacturerViewModel.ReferencesDialogRequest += ManufacturerViewModelOnReferencesDialogRequest;
            _manufacturerViewModel = null;
            DataContext = null;
        }
    }
}
