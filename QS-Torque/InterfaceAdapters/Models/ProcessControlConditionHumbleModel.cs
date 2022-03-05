using System;
using System.ComponentModel;
using Client.Core.Entities;
using Core;
using Core.Enums;
using Core.PhysicalValueTypes;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class ProcessControlConditionHumbleModel :
        BindableBase, 
        IEquatable<ProcessControlConditionHumbleModel>, 
        IQstEquality<ProcessControlConditionHumbleModel>, 
        ICopy<ProcessControlConditionHumbleModel>, 
        IUpdate<ProcessControlCondition>,
        IDataErrorInfo
    {
        private ILocalizationWrapper _localization;
        public ProcessControlCondition Entity { get; private set; }

        public ProcessControlConditionHumbleModel(ProcessControlCondition entity, ILocalizationWrapper localization)
        {
            _localization = localization;
            Entity = entity ?? new ProcessControlCondition();
            if (entity?.ProcessControlTech is QstProcessControlTech qstProcessControl)
            {
                QstProcessControlTechHumbleModel = QstProcessControlTechHumbleModel.GetModelFor(qstProcessControl, _localization);
            }
            
            RaisePropertyChanged();
        }

        public static ProcessControlConditionHumbleModel GetModelFor(ProcessControlCondition entity, ILocalizationWrapper localization)
        {
            return entity != null ? new ProcessControlConditionHumbleModel(entity, localization) : null;
        }

        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new ProcessControlConditionId(value);
                RaisePropertyChanged();
            }
        }

        public LocationModel Location
        {
            get => LocationModel.GetModelFor(Entity.Location, _localization, null);
            set
            {
                Entity.Location = value.Entity;
                RaisePropertyChanged();
            }
        }

        public double LowerInterventionLimit
        {
            get => Entity.LowerInterventionLimit.Nm;
            set
            {
                Entity.LowerInterventionLimit = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(UpperInterventionLimit));
            }
        }

        public double UpperInterventionLimit
        {
            get => Entity.UpperInterventionLimit.Nm;
            set
            {
                Entity.UpperInterventionLimit = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(LowerInterventionLimit));
            }
        }

        public double LowerMeasuringLimit
        {
            get => Entity.LowerMeasuringLimit.Nm;
            set
            {
                Entity.LowerMeasuringLimit = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(UpperMeasuringLimit));
            }
        }

        public double UpperMeasuringLimit
        {
            get => Entity.UpperMeasuringLimit.Nm;
            set
            {
                Entity.UpperMeasuringLimit = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(LowerMeasuringLimit));
            }
        }

        public TestLevelSetModel TestLevelSet
        {
            get => TestLevelSetModel.GetModelFor(Entity.TestLevelSet);
            set
            {
                Entity.TestLevelSet = value?.Entity;
                TestLevelNumber = 1;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(TestLevelNumber));
            }
        }

        public int? TestLevelNumber
        {
            get => Entity.TestLevelSet == null ? null : Entity.TestLevelNumber;
            set
            {
                Entity.TestLevelNumber = value ?? 1;
                RaisePropertyChanged();
            }
        }

        public DateTime? StartDate
        {
            get => Entity.StartDate;
            set
            {
                Entity.StartDate = value;
                RaisePropertyChanged();
            }
        }

        public bool TestOperationActive
        {
            get => Entity.TestOperationActive;
            set
            {
                Entity.TestOperationActive = value;
                RaisePropertyChanged();
            }
        }

        private QstProcessControlTechHumbleModel _qstProcessControlTechHumbleModel;

        public QstProcessControlTechHumbleModel QstProcessControlTechHumbleModel
        {
            get => _qstProcessControlTechHumbleModel;

            set
            {
                _qstProcessControlTechHumbleModel = value;
                RaisePropertyChanged();
            }
        }

        public DateTime? NextTestDate
        {
            get => Entity.NextTestDate;
            set
            {
                Entity.NextTestDate = value;
                RaisePropertyChanged();
            }
        }

        public Shift? NextTestShift
        {
            get => Entity.NextTestShift;
            set
            {
                Entity.NextTestShift = value;
                RaisePropertyChanged();
            }
        }

        public void RaiseTestLevelNumberChanged()
        {
            RaisePropertyChanged(nameof(TestLevelNumber));
        }

        public ProcessControlConditionHumbleModel CopyDeep()
        {
            return new ProcessControlConditionHumbleModel(Entity.CopyDeep(), _localization);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(ProcessControlConditionHumbleModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.EqualsById(other);
        } 

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProcessControlConditionHumbleModel)obj);
        }

        public bool EqualsById(ProcessControlConditionHumbleModel other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(ProcessControlConditionHumbleModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }

        public void UpdateWith(ProcessControlCondition other)
        {
            Entity.UpdateWith(other);
            RaisePropertyChanged(null);
        }

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                var error = Entity.Validate(columnName);
                if (error == null)
                {
                    return null;
                }

                switch (error)
                {
                    case ProcessControlValidationError.LowerMeasuringLimitGreaterThanOrEqualToUpperMeasuringLimitOrNotBetween0And9999:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The lower measuring limit has to be between 0 and 9999 and less than the upper measuring limit");

                    case ProcessControlValidationError.UpperMeasuringLimitLessThanOrEqualToLowerMeasuringLimitOrNotBetween0And9999:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The upper measuring limit has to be between 0 and 9999 and greater than the lower measuring limit");

                    case ProcessControlValidationError.LowerInterventionLimitGreaterThanUpperMeasuringLimitOrNotBetween0And9999:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The lower intervention limit has to be between 0 and 9999 and less than or equal to upper intervention limit");

                    case ProcessControlValidationError.UpperInterventionLimitLessThanLowerInterventionLimitOrNotBetween0And9999:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The upper intervention limit has to be between 0 and 9999 and greater than or equal to lower intervention limit");
                }

                return "";
            }
        }
    }
}
