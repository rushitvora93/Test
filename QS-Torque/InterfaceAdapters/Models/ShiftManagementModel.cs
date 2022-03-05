using System;
using Core;
using Core.Entities;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class ShiftManagementModel : BindableBase, IQstEquality<ShiftManagement>, IUpdate<ShiftManagement>
    {
        private ILocalizationWrapper _localization;
        public ShiftManagement Entity { get; private set; }

        public TimeSpan FirstShiftStart
        {
            get => Entity.FirstShiftStart;
            set
            {
                Entity.FirstShiftStart = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan FirstShiftEnd
        {
            get => Entity.FirstShiftEnd;
            set
            {
                Entity.FirstShiftEnd = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan SecondShiftStart
        {
            get => Entity.SecondShiftStart;
            set
            {
                Entity.SecondShiftStart = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan SecondShiftEnd
        {
            get => Entity.SecondShiftEnd;
            set
            {
                Entity.SecondShiftEnd = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan ThirdShiftStart
        {
            get => Entity.ThirdShiftStart;
            set
            {
                Entity.ThirdShiftStart = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan ThirdShiftEnd
        {
            get => Entity.ThirdShiftEnd;
            set
            {
                Entity.ThirdShiftEnd = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSecondShiftActive
        {
            get => Entity.IsSecondShiftActive;
            set
            {
                Entity.IsSecondShiftActive = value;
                RaisePropertyChanged();
            }
        }

        public bool IsThirdShiftActive
        {
            get => Entity.IsThirdShiftActive;
            set
            {
                Entity.IsThirdShiftActive = value;
                RaisePropertyChanged();
            }
        }

        public TimeSpan ChangeOfDay
        {
            get => Entity.ChangeOfDay;
            set
            {
                Entity.ChangeOfDay = value;
                RaisePropertyChanged();
            }
        }

        public DayOfWeek FirstDayOfWeek
        {
            get => Entity.FirstDayOfWeek;
            set
            {
                Entity.FirstDayOfWeek = value;
                RaisePropertyChanged();
            }
        }

        public ShiftManagementModel(ShiftManagement entity, ILocalizationWrapper localization)
        {
            Entity = entity ?? new ShiftManagement();
            _localization = localization;
            RaisePropertyChanged(null);
        }

        public static ShiftManagementModel GetModelFor(ShiftManagement entity, ILocalizationWrapper localization)
        {
            return entity != null ? new ShiftManagementModel(entity, localization) : null;
        }

        public bool EqualsById(ShiftManagement other)
        {
            return Entity.EqualsById(other);
        }

        public bool EqualsByContent(ShiftManagement other)
        {
            return Entity.EqualsByContent(other);
        }

        public void UpdateWith(ShiftManagement other)
        {
            Entity.UpdateWith(other);
            RaisePropertyChanged(null);
        }
    }
}
