using NUnit.Framework;
using Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Test.Entities
{
    public class DateTimeComparisonWithChangeOfDayTest
    {
        [TestCase("2021-12-12 23:00:00", "2021-12-13 12:00:00", "22:00:00")]
        [TestCase("2021-12-12 22:00:00", "2021-12-12 12:00:00", "23:00:00")]
        [TestCase("2021-12-12 23:00:00", "2021-12-13 12:00:00", "23:00:00")]
        [TestCase("2021-12-12 09:00:00", "2021-12-11 12:00:00", "10:00:00")]
        [TestCase("2021-12-12 11:00:00", "2021-12-12 12:00:00", "10:00:00")]
        [TestCase("2021-12-12 11:00:00", "2021-12-12 12:00:00", "11:00:00")]
        public void IsDateTimeEqualToDateConsideringChangeOfDay(DateTime dateTimeToCheck, DateTime referenceDate, TimeSpan changeOfDay)
        {
            var comparer = new DateTimeComparisonWithChangeOfDay();
            Assert.IsTrue(comparer.IsDateTimeEqualToDateConsideringChangeOfDay(dateTimeToCheck, referenceDate, changeOfDay));
        }

        [TestCase("2021-12-12 23:00:00", "2021-12-12 12:00:00", "22:00:00")]
        [TestCase("2021-12-12 22:00:00", "2021-12-13 12:00:00", "23:00:00")]
        [TestCase("2021-12-12 09:00:00", "2021-12-12 12:00:00", "10:00:00")]
        [TestCase("2021-12-12 11:00:00", "2021-12-11 12:00:00", "10:00:00")]
        public void IsDateTimeNotEqualToDateConsideringChangeOfDay(DateTime dateTimeToCheck, DateTime referenceDate, TimeSpan changeOfDay)
        {
            var comparer = new DateTimeComparisonWithChangeOfDay();
            Assert.IsFalse(comparer.IsDateTimeEqualToDateConsideringChangeOfDay(dateTimeToCheck, referenceDate, changeOfDay));
        }

        [TestCase("2021-12-11 23:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "22:00:00")]
        [TestCase("2021-12-10 23:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "22:00:00")]
        [TestCase("2021-12-13 21:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "22:00:00")]
        [TestCase("2021-12-14 21:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "22:00:00")]
        [TestCase("2021-12-11 12:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "10:00:00")]
        [TestCase("2021-12-12 09:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "10:00:00")]
        [TestCase("2021-12-14 21:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "10:00:00")]
        [TestCase("2021-12-15 09:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "10:00:00")]
        public void IsDateTimeExclusivelyBetweenDatesConsideringChangeOfDay(DateTime dateTimeToCheck, DateTime leftBorder, DateTime rightBorder, TimeSpan changeOfDay)
        {
            var comparer = new DateTimeComparisonWithChangeOfDay();
            Assert.IsTrue(comparer.IsDateTimeExclusivelyBetweenDatesConsideringChangeOfDay(dateTimeToCheck, leftBorder, rightBorder, changeOfDay));
        }

        [TestCase("2021-12-10 21:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "22:00:00")]
        [TestCase("2021-12-09 21:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "22:00:00")]
        [TestCase("2021-12-15 21:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "22:00:00")]
        [TestCase("2021-12-15 23:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "22:00:00")]
        [TestCase("2021-12-16 23:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "22:00:00")]
        [TestCase("2021-12-10 11:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "10:00:00")]
        [TestCase("2021-12-10 09:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "10:00:00")]
        [TestCase("2021-12-09 12:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "10:00:00")]
        [TestCase("2021-12-15 11:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "10:00:00")]
        [TestCase("2021-12-16 11:00:00", "2021-12-10 12:00:00", "2021-12-15 12:00:00", "10:00:00")]
        public void IsDateTimeNotExclusivelyBetweenDatesConsideringChangeOfDay(DateTime dateTimeToCheck, DateTime leftBorder, DateTime rightBorder, TimeSpan changeOfDay)
        {
            var comparer = new DateTimeComparisonWithChangeOfDay();
            Assert.IsFalse(comparer.IsDateTimeExclusivelyBetweenDatesConsideringChangeOfDay(dateTimeToCheck, leftBorder, rightBorder, changeOfDay));
        }
    }
}
