using FrameworksAndDrivers.Gui.Wpf.Assistent;
using FrameworksAndDrivers.Gui.Wpf.EventArgs;
using FrameworksAndDrivers.Gui.Wpf.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using InterfaceAdapters;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    public class AssistentViewModel : BindableBase
    {
        #region Properties
        private ObservableCollection<AssistentItemModel> _assistentItems;
        public ListCollectionView AssistentItemCollectionView { get; private set; }

        public ParentAssistentPlan Plan { get; private set; }

        private string _description;
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private AssistentItemModel _selectedAssistentItem;
        public AssistentItemModel SelectedAssistentItem
        {
            get => _selectedAssistentItem;
            set
            {
                if (value != null)
                    PreviousSelectedItem = _selectedAssistentItem;

                Set(ref _selectedAssistentItem, value);
                if (_assistentItems.Count > 0)
                {
                    IsLastAssistentItem = value == _assistentItems.Last();
                }
            }
        }

        public AssistentItemModel PreviousSelectedItem { get; private set; }

        private bool _isLastAssistentItem;
        public bool IsLastAssistentItem
        {
            get => _isLastAssistentItem;
            set => Set(ref _isLastAssistentItem, value);
        }
        #endregion


        #region Events
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        public event EventHandler EndOfAssistent;
        #endregion


        #region Commands
        public RelayCommand NextCommand { get; private set; }
        private bool NextCanExecute(object arg) { return SelectedAssistentItem.IsSet; }
        private void NextExecute(object obj)
        {
            // Check for Error
            if (SelectedAssistentItem.ErrorCheck(SelectedAssistentItem))
            {
                var args = new MessageBoxEventArgs((r) => { },
                                                   SelectedAssistentItem.ErrorText,
                                                   messageBoxImage: System.Windows.MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                return;
            }

            // Check for warning
            if (SelectedAssistentItem.WarningCheck(SelectedAssistentItem))
            {
                var args = new MessageBoxEventArgs((r) => { },
                                                   SelectedAssistentItem.WarningText(SelectedAssistentItem),
                                                   messageBoxImage: System.Windows.MessageBoxImage.Warning);
                MessageBoxRequest?.Invoke(this, args);
            }

            if (IsLastAssistentItem)
            {
                EndOfAssistent?.Invoke(this, null);
            }
            else
            {
                var selected = SelectedAssistentItem;

                RefillAssistentItems();

                // Move to next
                var newSelectedModel = _assistentItems[_assistentItems.IndexOf(selected) + 1];

                while (newSelectedModel is ChapterHeadingAssistentItemModel chapterHeadingItem)
                {
                    chapterHeadingItem.AlreadySelected = true;
                    newSelectedModel = _assistentItems[_assistentItems.IndexOf(newSelectedModel) + 1];
                }

                newSelectedModel.AlreadySelected = true;
                SelectedAssistentItem = newSelectedModel;
            }
        }

        public RelayCommand PreviousCommand { get; private set; }
        private bool PreviousCanExecute(object arg) { return _assistentItems.IndexOf(SelectedAssistentItem) > 0 && SelectedAssistentItem.IsSet; }
        private void PreviousExecute(object obj)
        {
            // Check for Error
            if (SelectedAssistentItem.ErrorCheck(SelectedAssistentItem))
            {
                var args = new MessageBoxEventArgs((r) => { },
                    SelectedAssistentItem.ErrorText,
                    messageBoxImage: System.Windows.MessageBoxImage.Error);
                MessageBoxRequest?.Invoke(this, args);
                return;
            }

            // Check for warning
            if (SelectedAssistentItem.WarningCheck(SelectedAssistentItem))
            {
                var args = new MessageBoxEventArgs((r) => { },
                    SelectedAssistentItem.WarningText(SelectedAssistentItem),
                    messageBoxImage: System.Windows.MessageBoxImage.Warning);
                MessageBoxRequest?.Invoke(this, args);
            }

            var newSelectedModel = _assistentItems[_assistentItems.IndexOf(SelectedAssistentItem) - 1];

            while (newSelectedModel is ChapterHeadingAssistentItemModel chapterHeadingItem)
            {
                newSelectedModel = _assistentItems[_assistentItems.IndexOf(newSelectedModel) - 1];
            }

            SelectedAssistentItem = newSelectedModel;
        }

        public RelayCommand SelectedAssistentItemChangedCommand { get; private set; }
        private bool SelectedAssistentItemChangedCanExecute(object arg) { return true; }
        private void SelectedAssistentItemChangedExecute(object obj)
        {
            var e = obj as SelectionChangedEventArgs;
            var indexOfSelected = _assistentItems.IndexOf(SelectedAssistentItem);
            var indexOfDeselected = _assistentItems.IndexOf(PreviousSelectedItem);

            if (indexOfSelected == indexOfDeselected)
            {
                return;
            }

            if (indexOfDeselected != -1 && _assistentItems[indexOfDeselected].ErrorCheck(_assistentItems[indexOfDeselected]) ||
                indexOfDeselected != -1 && _assistentItems[indexOfDeselected]?.IsSet == false ||
                indexOfSelected != -1 && _assistentItems[indexOfSelected]?.AlreadySelected == false)
            {
                // Reset selection
                _selectedAssistentItem = (AssistentItemModel)e.RemovedItems[0];
                SelectedAssistentItem = (AssistentItemModel)e.RemovedItems[0];
                return; 
            }


            // Foreward and had previous selected item
            if (indexOfDeselected >= 0)
            {
                // Check for Error
                if (_assistentItems[indexOfDeselected].ErrorCheck(_assistentItems[indexOfDeselected]))
                {
                    var args = new MessageBoxEventArgs((r) => { },
                                                       _assistentItems[indexOfDeselected].ErrorText,
                                                       messageBoxImage: System.Windows.MessageBoxImage.Error);
                    MessageBoxRequest?.Invoke(this, args);

                    // Reset selection
                    SelectedAssistentItem = (AssistentItemModel)e.RemovedItems[0];
                    return;
                }

                // Check for warning
                if (_assistentItems[indexOfDeselected].WarningCheck(_assistentItems[indexOfDeselected]))
                {
                    var args = new MessageBoxEventArgs((r) => { },
                                                       _assistentItems[indexOfDeselected].WarningText(_assistentItems[indexOfDeselected]),
                                                       messageBoxImage: System.Windows.MessageBoxImage.Warning);
                    MessageBoxRequest?.Invoke(this, args);
                }
            }

            // Foreward
            if (indexOfSelected > indexOfDeselected)
            {
                if (!SelectedAssistentItem.AlreadySelected)
                {
                    // Reset selection
                    SelectedAssistentItem = (AssistentItemModel)e.RemovedItems[0];
                }
                return;
            }
        }
        #endregion


        #region Methods
        public void SetParentAssistentPlan(ParentAssistentPlan plan)
        {
            Plan = plan;
            plan.Initialize();

            RefillAssistentItems();
            SelectedAssistentItem = _assistentItems[0];
            SelectedAssistentItem.AlreadySelected = true;
        }

        public void RefillAssistentItems()
        {
            if (Plan == null)
            {
                return;
            }

            _assistentItems.Clear();

            var list = new List<AssistentPlan>();
            Plan.AddNext(list);
            foreach (var p in list)
            {
                _assistentItems.Add(p.AssistentItem);
            }
        }

        public object FillResultObject(object resultObject)
        {
            if (resultObject == null)
            {
                return null;
            }

            foreach (var i in _assistentItems)
            {
                i.SetAttributeValueToResultObject(resultObject, i);
            }

            return resultObject;
        }

        public void RaiseEndOfAssistant()
        {
            EndOfAssistent?.Invoke(this, null);
        }
        #endregion


        public AssistentViewModel()
        {
            _assistentItems = new ObservableCollection<AssistentItemModel>();
            AssistentItemCollectionView = new ListCollectionView(_assistentItems);

            SelectedAssistentItemChangedCommand = new RelayCommand(SelectedAssistentItemChangedExecute, SelectedAssistentItemChangedCanExecute);
            NextCommand = new RelayCommand(NextExecute, NextCanExecute);
            PreviousCommand = new RelayCommand(PreviousExecute, PreviousCanExecute);
        }

        public AssistentViewModel(ParentAssistentPlan plan) : this()
        {
            SetParentAssistentPlan(plan);
        }
    }
}
