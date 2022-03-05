using Core.Entities;
using Core.Entities.ReferenceLink;
using Core.UseCases;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using InterfaceAdapters;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class ChangeToolStateViewModel 
        : BindableBase
        , IHelperTableGui<Status>
        , IHelperTableErrorGui<Status>
        , IChangeToolStateForLocationGui
    {
        #region Properties

        private Dispatcher _guiDispatcher;

        private string _description;
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private readonly IStartUp _startUp;
        private IChangeToolStateForLocationUseCase _useCase;
        private ILocalizationWrapper _localization;

        private ObservableCollection<LocationToolAssignmentChangeStateModel> _assignedTools;

        public ListCollectionView AssignedToolsCollectionView { get; private set; }

        public ObservableCollection<HelperTableItemModel<Status, string>> ToolStatusCollectionView { get; private set; }


        /// <summary>
        /// Returns the Value of the displayed property for a HelperTableItem
        /// </summary>
        public Func<Status, string> GetDisplayMember { get; set; }

        #endregion

        #region Events
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler EndOfAssistent;
        #endregion

        #region Commands
        public RelayCommand NextCommand { get; private set; }
        private bool NextCanExecute(object arg) { return true; } 
        private void NextExecute(object obj)
        {
            EndOfAssistent?.Invoke(this, null);
        }

        public RelayCommand OpenHelperTableCommand { get; private set; }
        private bool OpenHelperTableCanExecute(object arg) { return true; }
        private void OpenHelperTableExecute(object obj)
        {
            _startUp.OpenStatusHelperTableDialog();
        }

        #endregion

        #region Methods
        public void SetDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
        }

        public void SetAssignedTools(List<LocationToolAssignmentChangeStateModel> list)
        {
            foreach (var p in list)
            {
                _assignedTools.Add(p);
            }

        }

        public void SaveNewToolStates()
        {
            List<ToolDiff> tools = new List<ToolDiff>();
            foreach (LocationToolAssignmentChangeStateModel assignedTool in _assignedTools)
            {
                Tool newTool = assignedTool.LocationToolAssignment.AssignedTool.CopyDeep();
                newTool.Status = assignedTool.StateId;
                tools.Add(new ToolDiff(null, null, assignedTool.LocationToolAssignment.AssignedTool, newTool));
            }

            _useCase.SetNewToolStates(tools);

        }

        public List<LocationToolAssignment> FillResultObject(List<LocationToolAssignment> resultObject)
        {
            if (resultObject == null)
            {
                return null;
            }

            foreach (var assignedTool in _assignedTools)
            {
                assignedTool.SetAttributeValueToResultObject(resultObject);
            }

            return resultObject;
        }

        public void ShowItems(List<Status> items)
        {
            if (items is null)
            {
                return;
            }
            _guiDispatcher.Invoke(() =>
            {
                ToolStatusCollectionView.Clear();
                items.ForEach(x => ToolStatusCollectionView.Add(HelperTableItemModel.GetModelForStatus(x)));
            });
        }

        public void ShowErrorMessage()
        {
            // Intentionally empty
        }

        public void ShowEntryAlreadyExists(Status newItem)
        {
            // Intentionally empty
        }

        public void Add(Status newItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                ToolStatusCollectionView.Add(HelperTableItemModel.GetModelForStatus(newItem));
            });
        }

        public void Remove(Status removeItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var models = ToolStatusCollectionView.Where(x => x.Entity.EqualsById(removeItem)).ToList();
                foreach (var m in models)
                {
                    ToolStatusCollectionView.Remove(m);
                }
            });
        }

        public void Save(Status savedItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var itemToUpdate =
                    ToolStatusCollectionView.FirstOrDefault((item) => item.Entity.EqualsById(savedItem));
                if (itemToUpdate is null)
                {
                    return;
                }
                itemToUpdate.Value = savedItem.Value.ToDefaultString();
            });
        }

        public void ShowReferencedToolModels(List<ToolModelReferenceLink> toolModelReferenceLinks)
        {
            // Intentionally empty
        }

        public void ShowToolReferenceLinks(List<ToolReferenceLink> toolReferenceLinks)
        {
            // Intentionally empty
        }

        public void ShowReferencedLocationToolAssignments(List<LocationToolAssignment> assignments)
        {
            // Intentionally empty
        }

        public void ShowReferencesError()
        {
            // Intentionally empty
        }

        public void ShowRemoveHelperTableItemPreventingReferences(List<ToolModelReferenceLink> toolModels, List<ToolReferenceLink> tools, List<LocationToolAssignment> locationToolAssignments)
        {
            // Intentionally empty
        }
        

        public void ShowLocationsForTools(Dictionary<Tool, List<LocationReferenceLink>> locationsForTools)
        {
            _guiDispatcher.Invoke(() =>
            {
                foreach (var assignedTool in _assignedTools)
                {
                    List<LocationReferenceLink> locationReferenceLinks = locationsForTools
                        .FirstOrDefault(x => x.Key.Id.Equals(assignedTool.LocationToolAssignment.AssignedTool.Id))
                        .Value;

                    assignedTool.ShowLocationReferenceLinksForTool(locationReferenceLinks);
                }
            });
        }

        public void ShowErrorForLoadLocationsForTools()
        {
            // Intentionally empty
        }

        public void ShowErrorForSaveToolStates()
        {
            _guiDispatcher.Invoke(() =>
            {
                MessageBoxRequest.Invoke(this,
                    new MessageBoxEventArgs(
                        null,
                        _localization.Strings.GetParticularString("Change tool states",
                            "An error occured while saving tool states"),
                        _localization.Strings.GetParticularString("Change tool states", "Error"),
                        MessageBoxButton.OK, MessageBoxImage.Error));
            });

        }

        #endregion

        public ChangeToolStateViewModel(IStartUp startUp, IChangeToolStateForLocationUseCase useCase, ILocalizationWrapper localization)
        {
            _startUp = startUp;
            _useCase = useCase;
            _localization = localization;
            _assignedTools = new ObservableCollection<LocationToolAssignmentChangeStateModel>();
            AssignedToolsCollectionView = new ListCollectionView(_assignedTools);

            ToolStatusCollectionView = new ObservableCollection<HelperTableItemModel<Status, string>>();

            NextCommand = new RelayCommand(NextExecute, NextCanExecute);
            OpenHelperTableCommand = new RelayCommand(OpenHelperTableExecute, OpenHelperTableCanExecute);
        }

    }
}
