using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Cells;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using InterfaceAdapters.Localization;

namespace FrameworksAndDrivers.Gui.Wpf.View
{

    public interface IChangeToolStateView
    {
        event EventHandler EndOfAssistent;

        void SetAssignedTools(List<LocationToolAssignmentChangeStateModel> list);

        object DataContext{ get; set; }
    }

    /// <summary>
    /// Interaktionslogik für ChangeToolStateView.xaml
    /// </summary>
    public partial class ChangeToolStateView : Window, IChangeToolStateView, ICanShowDialog
    {
        public ChangeToolStateViewModel ViewModel { get; private set; }


        #region Events

        public event EventHandler EndOfAssistent
        {
            add { ViewModel.EndOfAssistent += value; }
            remove { ViewModel.EndOfAssistent -= value; }
        }

        #endregion

        #region Methods

        public void SetAssignedTools(List<LocationToolAssignmentChangeStateModel> list)
        {
            ViewModel.SetAssignedTools(list);
        }

        #endregion

        #region Event-Handler

        private void ViewModel_MessageBoxRequest(object sender, EventArgs.MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void DataGridOnDetailsViewExpanding(object sender, GridDetailsViewExpandingEventArgs e)
        {
            e.Cancel = !(e.Record as LocationToolAssignmentChangeStateModel).HasOtherConnectedLocations;
        }

        #endregion

        public ChangeToolStateView(string description, IStartUp startUp, ILocalizationWrapper localization,
            IChangeToolStateForLocationUseCase useCase)
        {
            InitializeComponent();
            ViewModel = new ChangeToolStateViewModel(startUp, useCase, localization) {Description = description};

            this.DataContext = ViewModel;
            ViewModel.SetDispatcher(Dispatcher);
            ViewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
            ViewModel.EndOfAssistent += (s, e) => this.DialogResult = true;
            DataGrid.LocalizationWrapper = localization;
            localization.Subscribe(DataGrid);
            DataGrid.DetailsViewExpanding += DataGridOnDetailsViewExpanding;
            DataGrid.CellRenderers.Remove("ComboBox");
            DataGrid.CellRenderers.Add("ComboBox", new GridCellComboBoxRendererExt());
        }
    }

    internal class GridCellComboBoxRendererExt : GridCellComboBoxRenderer
    {
        protected override void OnEditElementLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            base.OnEditElementLoaded(sender, e);
            var combobox = sender as ComboBox;
            combobox.IsDropDownOpen = true;
        }
    }
}
