using System.Collections.Generic;

namespace Core.Entities
{
    public enum IntervalErrors
    {
        PeriodEveryXDaysHasToBeGreaterThan0,
        PeriodEveryXDaysHasToBeLessOrEqual900,
    }

    public enum IntervalType
    {
        EveryXShifts = 0,
        EveryXDays = 1,
        XTimesAShift = 2,
        XTimesADay = 3,
        XTimesAWeek = 4,
        XTimesAMonth = 5,
        XTimesAYear = 6
    }

    public class Interval : IEqualsByContent<Interval>, ICopy<Interval>
    {
        public IntervalType Type { get; set; } = IntervalType.EveryXDays;
        public long IntervalValue { get; set; }

        public bool EqualsByContent(Interval other)
        {
            return this.IntervalValue == other?.IntervalValue &&
                   Type == other?.Type;
        }

        public List<IntervalErrors> Validate(string propertyName)
        {
            List<IntervalErrors> intervalErrors = new List<IntervalErrors>();
            switch (propertyName)
            {
                case (nameof(Interval.IntervalValue)):
                    if (IntervalValue < 1)
                    {
                        intervalErrors.Add(IntervalErrors.PeriodEveryXDaysHasToBeGreaterThan0);
                    }

                    if (IntervalValue > 900)
                    {
                        intervalErrors.Add(IntervalErrors.PeriodEveryXDaysHasToBeLessOrEqual900);
                    }

                    break;
            }
            return intervalErrors;
        }

        public Interval CopyDeep()
        {
            return new Interval()
            {
                Type = this.Type,
                IntervalValue = this.IntervalValue
            };
        }

        public bool IsIntervalStatic()
        {
            return Type == IntervalType.XTimesAShift || Type == IntervalType.XTimesADay || Type == IntervalType.XTimesAWeek || Type == IntervalType.XTimesAMonth || Type == IntervalType.XTimesAYear;
        }

        public bool IsIntervalDynamic()
        {
            return Type == IntervalType.EveryXShifts || Type == IntervalType.EveryXDays;
        }
    }
}
