using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Entities
{
    public class DateTimeComparisonWithChangeOfDay
    {
        public bool IsDateTimeEqualToDateConsideringChangeOfDay(DateTime dateTimeToCheck, DateTime referenceDate, TimeSpan changeOfDay)
        {
            if (changeOfDay < TimeSpan.FromHours(12))
            {
                return (dateTimeToCheck.Date == referenceDate.Date && dateTimeToCheck.TimeOfDay >= changeOfDay) ||
                       (dateTimeToCheck.Date == referenceDate.AddDays(1).Date && dateTimeToCheck.TimeOfDay < changeOfDay);
            }
            else
            {
                return (dateTimeToCheck.Date == referenceDate.Date && dateTimeToCheck.TimeOfDay < changeOfDay) ||
                       (dateTimeToCheck.Date == referenceDate.AddDays(-1).Date && dateTimeToCheck.TimeOfDay >= changeOfDay);
            }
        }

        public bool IsDateTimeExclusivelyBetweenDatesConsideringChangeOfDay(DateTime dateTimeToCheck, DateTime leftBorder, DateTime rightBorder, TimeSpan changeOfDay)
        {
            if (changeOfDay < TimeSpan.FromHours(12))
            {
                return (dateTimeToCheck.Date == leftBorder.Date.AddDays(1) && dateTimeToCheck.TimeOfDay >= changeOfDay ||
                        dateTimeToCheck.Date > leftBorder.Date.AddDays(1)) &&
                       (dateTimeToCheck.Date == rightBorder.Date && dateTimeToCheck.TimeOfDay < changeOfDay ||
                        dateTimeToCheck.Date < rightBorder.Date);
            }
            else
            {
                return (dateTimeToCheck.Date == leftBorder.Date && dateTimeToCheck.TimeOfDay >= changeOfDay ||
                        dateTimeToCheck.Date > leftBorder.Date) &&
                       (dateTimeToCheck.Date < rightBorder.Date.AddDays(-1) ||
                        dateTimeToCheck.Date == rightBorder.Date.AddDays(-1) && dateTimeToCheck.TimeOfDay < changeOfDay);
            }
        }
    }
}
