using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Core.Entities;
using Core.UseCases;
using InterfaceAdapters.Models;

namespace InterfaceAdapters
{
    public interface IWorkingCalendarInterface : INotifyPropertyChanged
    {
        ObservableCollection<WorkingCalendarEntryModel> WorkingCalendarEntries{ get; }
        WorkingCalendarEntryModel SelectedEntryInList{ get; set; }
        DateTime? SelectedDateInCalendar { get; set; }
        bool AreSaturdaysFree { get; set; }
        bool AreSundaysFree { get; set; }
        public WorkingCalendar WorkingCalendarWithoutChanges { get; }

        event EventHandler<bool> ShowLoadingControlRequest;
        
        void SetGuiDispatcher(Dispatcher dispatcher);
    }

    public class WorkingCalendarInterfaceAdapter : BindableBase, IWorkingCalendarInterface, IWorkingCalendarGui
    {
        private Dispatcher _guiDispatcher;
        

        private ObservableCollection<WorkingCalendarEntryModel> _workingCalendarEntries;
        public ObservableCollection<WorkingCalendarEntryModel> WorkingCalendarEntries
        {
            get { return _workingCalendarEntries; }
            set
            {
                _workingCalendarEntries = value;
                RaisePropertyChanged();
            }
        }

        private WorkingCalendarEntryModel _selectedEntryInList;
        public WorkingCalendarEntryModel SelectedEntryInList
        {
            get { return _selectedEntryInList; }
            set
            {
                _selectedEntryInList = value;
                if(_selectedEntryInList != null && SelectedDateInCalendar != _selectedEntryInList.Date)
                {
                    SelectedDateInCalendar = _selectedEntryInList.Date;
                }
                RaisePropertyChanged();
            }
        }

        private DateTime? _selectedDateInCalendar;
        public DateTime? SelectedDateInCalendar
        {
            get { return _selectedDateInCalendar; }
            set
            {
                _selectedDateInCalendar = value;

                var entry = WorkingCalendarEntries.FirstOrDefault(x => x.Date.Date == value?.Date ||
                                                                       x.Repetition == WorkingCalendarEntryRepetition.Yearly && x.Date.Month == value?.Month && x.Date.Day == value?.Day);
                if(entry != null)
                {
                    SelectedEntryInList = entry;
                }

                RaisePropertyChanged();
            }
        }

        private bool _areSaturdaysFree;
        public bool AreSaturdaysFree
        {
            get { return _areSaturdaysFree; }
            set
            {
                var valueChanged = _areSaturdaysFree != value;
                _areSaturdaysFree = value;
                if (valueChanged)
                {
                    RaisePropertyChanged();
                }
            }
        }

        private bool _areSundaysFree;
        public bool AreSundaysFree
        {
            get { return _areSundaysFree; }
            set
            {
                var valueChanged = _areSundaysFree != value;
                _areSundaysFree = value;
                if (valueChanged)
                {
                    RaisePropertyChanged();
                }
            }
        }

        private WorkingCalendar _workingCalendarWithoutChanges;
        public WorkingCalendar WorkingCalendarWithoutChanges
        {
            get { return _workingCalendarWithoutChanges; }
            private set 
            { 
                _workingCalendarWithoutChanges = value;
                RaisePropertyChanged();
            }
        }


        public event EventHandler<bool> ShowLoadingControlRequest;


        public WorkingCalendarInterfaceAdapter()
        {
            WorkingCalendarEntries = new ObservableCollection<WorkingCalendarEntryModel>();
        }

        public void SetGuiDispatcher(Dispatcher dispatcher)
        {
            _guiDispatcher = dispatcher;
        }


        public void ShowCalendarEntries(List<WorkingCalendarEntry> entries)
        {
            _guiDispatcher.Invoke(() =>
            {
                WorkingCalendarEntries = new ObservableCollection<WorkingCalendarEntryModel>(entries.Select(x => new WorkingCalendarEntryModel(x)));
                
                ShowLoadingControlRequest?.Invoke(this, false);
            });
        }

        public void AddWorkingCalendarEntry(WorkingCalendarEntry newEntry)
        {
            _guiDispatcher.Invoke(() =>
            {
                var model = WorkingCalendarEntryModel.GetModelFor(newEntry);
                WorkingCalendarEntries.Add(model);
                SelectedEntryInList = model;
            });
        }

        public void RemoveWorkingCalendarEntry(WorkingCalendarEntry oldEntry)
        {
            _guiDispatcher.Invoke(() =>
            {
                var model = WorkingCalendarEntries.FirstOrDefault(x => x.Entity == oldEntry);
                if (model != null)
                {
                    WorkingCalendarEntries.Remove(model);
                }
            });
        }

        public void LoadWeekendSettings(WorkingCalendar workingCalendar)
        {
            _guiDispatcher.Invoke(() =>
            {
                AreSaturdaysFree = workingCalendar.AreSaturdaysFree;
                AreSundaysFree = workingCalendar.AreSundaysFree;
                WorkingCalendarWithoutChanges = new WorkingCalendar()
                {
                    AreSaturdaysFree = workingCalendar.AreSaturdaysFree,
                    AreSundaysFree = workingCalendar.AreSundaysFree
                };
            });
        }
    }
}
