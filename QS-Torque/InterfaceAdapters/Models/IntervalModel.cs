using Core.Entities;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class IntervalModel : BindableBase
    {
        public Interval Entity { get; set; }

        public IntervalType Type
        {
            get => Entity.Type;
            set
            {
                Entity.Type = value;
                RaisePropertyChanged();
            }
        }

        public long IntervalValue
        {
            get => Entity.IntervalValue;
            set
            {
                Entity.IntervalValue = value;
                RaisePropertyChanged();
            }
        }

        private IntervalModel(Interval interval)
        {
            Entity = interval;
        }

        public static IntervalModel GetModelFor(Interval interval)
        {
            return new IntervalModel(interval);
        }

        public static string GetIntervalTypeTranslation(IntervalType type, ILocalizationWrapper localization)
        {
            switch(type)
            {
                case IntervalType.EveryXShifts: return localization.Strings.GetParticularString("IntervalType", "Every x shifts");
                case IntervalType.EveryXDays: return localization.Strings.GetParticularString("IntervalType", "Every x days");
                case IntervalType.XTimesAShift: return localization.Strings.GetParticularString("IntervalType", "X times a shift");
                case IntervalType.XTimesADay: return localization.Strings.GetParticularString("IntervalType", "X times a day");
                case IntervalType.XTimesAWeek: return localization.Strings.GetParticularString("IntervalType", "X times a week");
                case IntervalType.XTimesAMonth: return localization.Strings.GetParticularString("IntervalType", "X times a month");
                case IntervalType.XTimesAYear: return localization.Strings.GetParticularString("IntervalType", "X times a year");
                default: return "";
            }
        }
    }
}
