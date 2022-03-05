using System;
using System.ComponentModel;
using Client.Core.Entities;
using Common.Types.Enums;
using Core;
using Core.PhysicalValueTypes;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class QstProcessControlTechHumbleModel : 
        BindableBase,
        IEquatable<QstProcessControlTechHumbleModel>,
        IQstEquality<QstProcessControlTechHumbleModel>,   
        ICopy<QstProcessControlTechHumbleModel>,
        IUpdate<QstProcessControlTech>,
        IDataErrorInfo
    {

        private ILocalizationWrapper _localization;
        public QstProcessControlTech Entity { get; private set; }

        public QstProcessControlTechHumbleModel(QstProcessControlTech entity, ILocalizationWrapper localization)
        {
            _localization = localization;
            Entity = entity ?? new QstProcessControlTech();
            RaisePropertyChanged();
        }

        public static QstProcessControlTechHumbleModel GetModelFor(QstProcessControlTech entity, ILocalizationWrapper localization)
        {
            return entity != null ? new QstProcessControlTechHumbleModel(entity, localization) : null;
        }

        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new ProcessControlTechId(value);
                RaisePropertyChanged();
            }
        }

        public TestMethod TestMethod
        {
            get => Entity.TestMethod;
            set
            {
                Entity.TestMethod = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsQstMinimumTorqueVisible));
                RaisePropertyChanged(nameof(IsQstPeakVisible));
                RaisePropertyChanged(nameof(IsQstPrevailTorqueAngleVisible));
            }
        }

        public long AngleLimitMt
        {
            get => (long?)Entity?.AngleLimitMt?.Degree ?? 0;
            set
            {
                Entity.AngleLimitMt = Angle.FromDegree(value);
                RaisePropertyChanged();
            }
        }

        public double StartMeasurementPeak
        {
            get => Entity?.StartMeasurementPeak?.Nm ?? 0;
            set
            {
                Entity.StartMeasurementPeak = Torque.FromNm(value); 
                RaisePropertyChanged();
            }
        }

        public double StartAngleCountingPa
        {
            get => Entity?.StartAngleCountingPa?.Nm ?? 0;
            set
            {
                Entity.StartAngleCountingPa = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AlarmTorquePa));
                RaisePropertyChanged(nameof(StartMeasurementPa));
            }
        }

        public double AngleForFurtherTurningPa
        {
            get => Entity?.AngleForFurtherTurningPa?.Degree ?? 0;
            set
            {
                Entity.AngleForFurtherTurningPa = Angle.FromDegree(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AlarmAnglePa));
            }
        }

        public double TargetAnglePa
        {
            get => Entity?.TargetAnglePa?.Degree ?? 0;
            set
            {
                Entity.TargetAnglePa = Angle.FromDegree(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AlarmAnglePa));
            }
        }

        public double StartMeasurementPa
        {
            get => Entity?.StartMeasurementPa?.Nm ?? 0;
            set
            {
                Entity.StartMeasurementPa = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AlarmTorquePa));
            }
        }

        public double AlarmTorquePa
        {
            get => Entity?.AlarmTorquePa?.Nm ?? 0;
            set
            {
                Entity.AlarmTorquePa = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(StartAngleCountingPa));
                RaisePropertyChanged(nameof(StartMeasurementPa));
            }
        }
        
        public double AlarmAnglePa
        {
            get => Entity?.AlarmAnglePa?.Degree ?? 0;
            set
            {
                Entity.AlarmAnglePa = Angle.FromDegree(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AngleForFurtherTurningPa));
                RaisePropertyChanged(nameof(TargetAnglePa));
            }
        }

        public double MinimumTorqueMt
        {
            get => Entity?.MinimumTorqueMt?.Nm ?? 0;
            set
            {
                Entity.MinimumTorqueMt = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(AlarmTorqueMt));
            }
        }

        public double StartAngleMt
        {
            get => Entity?.StartAngleMt?.Nm ?? 0;
            set
            {
                Entity.StartAngleMt = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(StartMeasurementMt));
                RaisePropertyChanged(nameof(AlarmTorqueMt));
            }
        }

        public double StartMeasurementMt
        {
            get => Entity?.StartMeasurementMt?.Nm ?? 0;
            set
            {
                Entity.StartMeasurementMt = Torque.FromNm(value);
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(StartAngleMt));
                RaisePropertyChanged(nameof(AlarmTorqueMt));
            }
        }

        public double AlarmTorqueMt
        {
            get => Entity?.AlarmTorqueMt?.Nm ?? 0;
            set
            {
                Entity.AlarmTorqueMt = Torque.FromNm(value);
                RaisePropertyChanged();
            }
        }

        public double AlarmAngleMt
        {
            get => Entity?.AlarmAngleMt?.Degree ?? 0;
            set
            {
                Entity.AlarmAngleMt = Angle.FromDegree(value); 
                RaisePropertyChanged();
            }
        }

        public bool IsQstMinimumTorqueVisible => TestMethod == TestMethod.QST_MT;
        public bool IsQstPeakVisible => TestMethod == TestMethod.QST_PEAK;
        public bool IsQstPrevailTorqueAngleVisible => TestMethod == TestMethod.QST_PA;


        public QstProcessControlTechHumbleModel CopyDeep()
        {
            return new QstProcessControlTechHumbleModel((QstProcessControlTech)Entity.CopyDeep(), _localization);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(QstProcessControlTechHumbleModel other)
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
            return Equals((QstProcessControlTechHumbleModel)obj);
        }

        public bool EqualsById(QstProcessControlTechHumbleModel other)
        {
            return this.Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(QstProcessControlTechHumbleModel other)
        {
            return this.Entity.EqualsByContent(other?.Entity);
        }

        public void UpdateWith(QstProcessControlTech other)
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
                    case ProcessControlTechValidationError.MinimumTorqueMtNotBetween0Point5And999:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The minimum torque has to be between 0.5 and 999");
                    case ProcessControlTechValidationError.StartAngleMtNotBetween0Point5And999:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The start angle count has to be between 0.5 and 999");
                    case ProcessControlTechValidationError.StartMeasurementMtNotBetween0Point5And999AndGreaterThanStartAngleMt:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The start measurement has to be between 0.5 and 999 and less than or equal to start angle count");
                    case ProcessControlTechValidationError.AlarmTorqueMtNotBetween0And9999AndLessThanMinimumTorqueMtOrStartAngleMtOrStartMeasurementMt:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The alarm torque has to be between 0 and 9999 and greater than or equal to minimum torque, start angle count and start measurement");
                    case ProcessControlTechValidationError.StartAngleCountingPaNotBetween0Point5And999:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The start angle count has to be between 0.5 and 999");
                    case ProcessControlTechValidationError.AlarmTorquePaNotBetween0And9999AndLessThanStartAngleCountingPaOrStartMeasurementPa:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The alarm torque has to be between 0 and 9999 and greater than or equal to start angle count and start measurement");
                    case ProcessControlTechValidationError.StartMeasurementPaNotBetween0Point5And999OrGreaterThanStartAngleCountingPa:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The start measurement has to be between 0.5 and 999 and less than or equal to start angle count");
                    case ProcessControlTechValidationError.AngleForFurtherTurningPaNotBetween0And99:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The angle for further turning has to be between 0 and 99");
                    case ProcessControlTechValidationError.TargetAnglePaNotBetween0And20:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The target angle has to be between 0 and 20");
                    case ProcessControlTechValidationError.AlarmAnglePaNotBetween0And9999OrLessThanAngleForFurtherTurningPaOrTargetAnglePa:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The alarm angle has to be between 0 and 9999 and greater than or equal to angle for further turning and target angle");
                    case ProcessControlTechValidationError.AngleLimitMtNotBetween1And99:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The alarm limit angle has to be between 1 and 99");
                    case ProcessControlTechValidationError.AlarmAngleMtNotBetween0And9999:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The alarm limit angle has to be between 0 and 9999");
                    case ProcessControlTechValidationError.StartMeasurementPeakNotBetween0Point5And999:
                        return _localization.Strings.GetParticularString("ProcessControlValidationError", "The start measurement has to be greater than 0.5 and less or equal to 999");
                }

                return "";
            }
        }
    }
}
