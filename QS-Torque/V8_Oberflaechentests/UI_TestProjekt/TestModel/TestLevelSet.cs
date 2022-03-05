using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI_TestProjekt.TestModel
{
    public class TestLevelSet
    {
        private string name = "";

        private int interval1 = 1;
        private string intervalType1 = IntervalTypes.XTimesPerDay;
        private int sampleNumber1 = 1;
        private bool considerWorkingCalendar1 = true;

        private bool isActive2 = false;
        private int interval2 = 1;
        private string intervalType2 = IntervalTypes.XTimesPerDay;
        private int sampleNumber2 = 1;
        private bool considerWorkingCalendar2 = true;

        private bool isActive3 = false;
        private int interval3 = 1;
        private string intervalType3 = IntervalTypes.XTimesPerDay;
        private int sampleNumber3 = 1;
        private bool considerWorkingCalendar3 = true;


        public string Name { get => name; set => name = value; }
        public int Interval1 { get => interval1; set => interval1 = value; }
        public string IntervalType1 { get => intervalType1; set => intervalType1 = value; }
        public int SampleNumber1 { get => sampleNumber1; set => sampleNumber1 = value; }
        public bool ConsiderWorkingCalendar1 { get => considerWorkingCalendar1; set => considerWorkingCalendar1 = value; }
        public bool IsActive2 { get => isActive2; set => isActive2 = value; }
        public int Interval2 { get => interval2; set => interval2 = value; }
        public string IntervalType2 { get => intervalType2; set => intervalType2 = value; }
        public int SampleNumber2 { get => sampleNumber2; set => sampleNumber2 = value; }
        public bool ConsiderWorkingCalendar2 { get => considerWorkingCalendar2; set => considerWorkingCalendar2 = value; }
        public bool IsActive3 { get => isActive3; set => isActive3 = value; }
        public int Interval3 { get => interval3; set => interval3 = value; }
        public string IntervalType3 { get => intervalType3; set => intervalType3 = value; }
        public int SampleNumber3 { get => sampleNumber3; set => sampleNumber3 = value; }
        public bool ConsiderWorkingCalendar3 { get => considerWorkingCalendar3; set => considerWorkingCalendar3 = value; }

        public TestLevelSet(string name, int interval1, string intervalType1, int sampleNumber1, bool considerWorkingCalendar1, bool isActive2, int interval2, string intervalType2, int sampleNumber2, bool considerWorkingCalendar2, bool isActive3, int interval3, string intervalType3, int sampleNumber3, bool considerWorkingCalendar3)
        {
            Name = name;
            Interval1 = interval1;
            IntervalType1 = intervalType1;
            SampleNumber1 = sampleNumber1;
            ConsiderWorkingCalendar1 = considerWorkingCalendar1;
            IsActive2 = isActive2;
            Interval2 = interval2;
            IntervalType2 = intervalType2;
            SampleNumber2 = sampleNumber2;
            ConsiderWorkingCalendar2 = considerWorkingCalendar2;
            IsActive3 = isActive3;
            Interval3 = interval3;
            IntervalType3 = intervalType3;
            SampleNumber3 = sampleNumber3;
            ConsiderWorkingCalendar3 = considerWorkingCalendar3;
        }

        public TestLevelSet() { }

        public static class IntervalTypes
        {
            public const string EveryXShifts = "Every x shifts";
            public const string EveryXDays = "Every x days";
            //Gibts vorerst nicht mehr
            //public const string EveryXWeeks = "Every x weeks";
            public const string XTimesPerShift = "X times per shift";
            public const string XTimesPerDay = "X times per day";
            public const string XTimesPerWeek = "X times per week";
            public const string XTimesPerMonth = "X times per month";
            public const string XTimesPerYear = "X times per year";
        }
    }
}