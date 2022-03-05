using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class ShiftManagement : IQstEquality<ShiftManagement>, ICopy<ShiftManagement>, IUpdate<ShiftManagement>
    {
        public TimeSpan FirstShiftStart { get; set; }
        public TimeSpan FirstShiftEnd { get; set; }
        public TimeSpan SecondShiftStart { get; set; }
        public TimeSpan SecondShiftEnd { get; set; }
        public TimeSpan ThirdShiftStart { get; set; }
        public TimeSpan ThirdShiftEnd { get; set; }
        public bool IsSecondShiftActive { get; set; }
        public bool IsThirdShiftActive { get; set; }
        public TimeSpan ChangeOfDay { get; set; }
        public DayOfWeek FirstDayOfWeek { get; set; }


        public bool EqualsById(ShiftManagement other)
        {
            throw new InvalidOperationException("ShiftManagement has no ID");
        }

        public bool EqualsByContent(ShiftManagement other)
        {
            if (other == null)
            {
                return false;
            }

            return FirstShiftStart == other.FirstShiftStart &&
                   FirstShiftEnd == other.FirstShiftEnd &&
                   SecondShiftStart == other.SecondShiftStart &&
                   SecondShiftEnd == other.SecondShiftEnd &&
                   ThirdShiftStart == other.ThirdShiftStart &&
                   ThirdShiftEnd == other.ThirdShiftEnd &&
                   IsSecondShiftActive == other.IsSecondShiftActive &&
                   IsThirdShiftActive == other.IsThirdShiftActive &&
                   ChangeOfDay == other.ChangeOfDay &&
                   FirstDayOfWeek == other.FirstDayOfWeek;
        }

        public ShiftManagement CopyDeep()
        {
            return new ShiftManagement()
            {
                FirstShiftStart = this.FirstShiftStart,
                FirstShiftEnd = this.FirstShiftEnd,
                SecondShiftStart = this.SecondShiftStart,
                SecondShiftEnd = this.SecondShiftEnd,
                ThirdShiftStart = this.ThirdShiftStart,
                ThirdShiftEnd = this.ThirdShiftEnd,
                IsSecondShiftActive = this.IsSecondShiftActive,
                IsThirdShiftActive = this.IsThirdShiftActive,
                ChangeOfDay = this.ChangeOfDay,
                FirstDayOfWeek = this.FirstDayOfWeek
            };
        }

        public void UpdateWith(ShiftManagement other)
        {
            if (other == null)
            {
                return;
            }

            this.FirstShiftStart = other.FirstShiftStart;
            this.FirstShiftEnd = other.FirstShiftEnd;
            this.SecondShiftStart = other.SecondShiftStart;
            this.SecondShiftEnd = other.SecondShiftEnd;
            this.ThirdShiftStart = other.ThirdShiftStart;
            this.ThirdShiftEnd = other.ThirdShiftEnd;
            this.IsSecondShiftActive = other.IsSecondShiftActive;
            this.IsThirdShiftActive = other.IsThirdShiftActive;
            this.ChangeOfDay = other.ChangeOfDay;
            this.FirstDayOfWeek = other.FirstDayOfWeek;
        }

        public IEnumerable<string> Validate()
        {
            if (!IsSecondShiftActive && !IsThirdShiftActive)
            {
                return new List<string>();
            }

            var timeSpans = new List<TimeSpan>()
            {
                FirstShiftStart,
                FirstShiftEnd
            };
            var shiftNames = new List<string>()
            {
                nameof(FirstShiftStart),
                nameof(FirstShiftEnd)
            };
            if(IsSecondShiftActive)
            {
                timeSpans.Add(SecondShiftStart);
                timeSpans.Add(SecondShiftEnd);
                shiftNames.Add(nameof(SecondShiftStart));
                shiftNames.Add(nameof(SecondShiftEnd));
            }
            if (IsThirdShiftActive)
            {
                timeSpans.Add(ThirdShiftStart);
                timeSpans.Add(ThirdShiftEnd);
                shiftNames.Add(nameof(ThirdShiftStart));
                shiftNames.Add(nameof(ThirdShiftEnd));
            }

            var invalidShifts = new List<string>();

            for (int i = 0; i < timeSpans.Count; i++)
            {
                // Basically: FirstShiftStart <= FirstShiftEnd <= SecondShiftStart <= SecondShiftEnd <= ThirdShiftStart <= ThirdShiftEnd <= FirstShiftStart
                // BUT: One(!) of these LessThanOrEqualTos (<=) can be a GreaterThan (>)  --> This would be the day change: e. g. SecondShiftEnd = 23:00:00 & ThirdShiftStart = 06:00:00

                if(timeSpans[i] > timeSpans[(i + 1) % timeSpans.Count])
                {
                    if (timeSpans[i] != timeSpans.Max())
                    {
                        invalidShifts.Add(shiftNames[i]);
                    }
                    if (timeSpans[(i + 1) % timeSpans.Count] != timeSpans.Min())
                    {
                        invalidShifts.Add(shiftNames[(i + 1) % timeSpans.Count]);
                    }
                }
            }
            return invalidShifts;
        }
    }
}
