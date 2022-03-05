using System;
using System.ComponentModel;
using Client.Core.Diffs;
using Core.Entities;
using Core.UseCases;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace InterfaceAdapters
{
    public interface IShiftManagementInterface : INotifyPropertyChanged
    {
        ShiftManagementModel CurrentShiftManagement { get; set; }
        ShiftManagementModel ShiftManagementWithoutChanges { get; }

        event EventHandler<bool> ShowLoadingControlRequest;
    }


    public class ShiftManagementInterfaceAdapter : BindableBase, IShiftManagementGui, IShiftManagementInterface
    {
        private ILocalizationWrapper _localization;

        private ShiftManagementModel _currentShiftManagement;
        public ShiftManagementModel CurrentShiftManagement
        {
            get { return _currentShiftManagement; }
            set
            {
                _currentShiftManagement = value;
                RaisePropertyChanged();
            }
        }

        private ShiftManagementModel _shiftManagementWithoutChanges;
        public ShiftManagementModel ShiftManagementWithoutChanges
        {
            get { return _shiftManagementWithoutChanges; }
            private set
            {
                _shiftManagementWithoutChanges = value;
                RaisePropertyChanged();
            }
        }

        public event EventHandler<bool> ShowLoadingControlRequest;

        public ShiftManagementInterfaceAdapter(ILocalizationWrapper localization)
        {
            _localization = localization;
        }
        
        public void LoadShiftManagement(ShiftManagement entity)
        {
            CurrentShiftManagement = ShiftManagementModel.GetModelFor(entity, _localization);
            ShiftManagementWithoutChanges = ShiftManagementModel.GetModelFor(entity.CopyDeep(), _localization);
            ShowLoadingControlRequest?.Invoke(this, false);
        }

        public void SaveShiftManagement(ShiftManagementDiff diff)
        {
            CurrentShiftManagement = ShiftManagementModel.GetModelFor(diff.New, _localization);
            ShiftManagementWithoutChanges = ShiftManagementModel.GetModelFor(diff.New.CopyDeep(), _localization);
        }
    }
}
