using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Types.Enums;
using Core.Entities;
using Core.Enums;
using Core.PhysicalValueTypes;
using Core.UseCases.Communication;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class ProcessControlForTransferHumbleModel : BindableBase
    {
        public ProcessControlForTransferHumbleModel(ProcessControlForTransfer processControlForTransfer, ILocalizationWrapper localization)
        {
            _processControlForTransfer = processControlForTransfer;
            _localization = localization;
        }

        public bool Selected
        {
            get => _selected;
            set => Set(ref _selected, value);
        }

        public ProcessControlForTransfer GetEntity()
        {
            return _processControlForTransfer;
        }

        public string LocationNumber => _processControlForTransfer.LocationNumber.ToDefaultString();
        public string LocationDescription => _processControlForTransfer.LocationDescription.ToDefaultString();
        public double SetPointTorque => _processControlForTransfer.SetPointTorque.Nm;
        public double MinimumTorque => _processControlForTransfer.MinimumTorque.Nm;
        public double MaximumTorque => _processControlForTransfer.MaximumTorque.Nm;
        public DateTime? LastTestDate => _processControlForTransfer.LastTestDate;
        public DateTime? NextTestDate => _processControlForTransfer.NextTestDate;
        public int SampleNumber => _processControlForTransfer.SampleNumber;
        public IntervalModel TestInterval => IntervalModel.GetModelFor(_processControlForTransfer.TestInterval);
        public string TestMethod => _localization.Strings.GetParticularString("ProcessControl test method", _processControlForTransfer.TestMethod.ToString());
        public Shift? NextTestDateShift => _processControlForTransfer.NextTestDateShift;

        private bool _hasCapacityError;
        public bool HasCapacityError
        {
            get => _hasCapacityError;
            set
            {
                _hasCapacityError = value;
                RaisePropertyChanged(nameof(HasCapacityError));
                RaisePropertyChanged(nameof(CapacityErrorSign));
                RaisePropertyChanged(nameof(HasNoCapacityError));
            }
        }

        public bool HasNoCapacityError => !HasCapacityError;
        public string CapacityErrorSign => HasCapacityError ? "!" : "";

        private readonly ProcessControlForTransfer _processControlForTransfer;
        private readonly ILocalizationWrapper _localization;
        private bool _selected;
    }
}
