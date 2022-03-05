using System;

namespace Server.Core.Entities
{
    public class ShiftManagement
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
    }
}
