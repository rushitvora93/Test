using System;
using System.Collections.Generic;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class TimeDataAccessMock : ITimeDataAccess
    {
        public int ConvertToUtcCalled { get; set; }

        public List<DateTime?> ConvertToUtcNullAbleParameter { get; set; } = new List<DateTime?>();
        public List<DateTime?> ConvertToLocalNullAbleParameter { get; set; } = new List<DateTime?>();
        public List<DateTime> ConvertToLocalParameters { get; set; } = new List<DateTime>();
        public DateTime NextLocalNow { get; set; }
        public List<object> ConvertToLocalDateParameters { get; set; } = new List<object>();
        public DateTime ConvertToUtcParameter { get; set; }
        public List<DateTime> ConvertToUtcReturnValues { get; set; }
        private int _convertToUtcCallCount;

        public DateTime UtcNow()
        {
            throw new NotImplementedException();
        }

        public DateTime LocalNow()
        {
            return NextLocalNow;
        }

        public DateTime ConvertToUtc(DateTime dateTime)
        {
            ConvertToUtcParameter = dateTime;
            if (ConvertToUtcReturnValues == null)
            {
                return dateTime;
            }
            return ConvertToUtcReturnValues[_convertToUtcCallCount++];
        }

        public DateTime? ConvertToUtc(DateTime? dateTime)
        {
            ConvertToUtcNullAbleParameter.Add(dateTime);
            return dateTime;
        }

        public DateTime ConvertToLocalDate(DateTime dateTime)
        {
            ConvertToLocalParameters.Add(dateTime);
            return dateTime;
        }

        public DateTime? ConvertToLocalDate(DateTime? dateTime)
        {
            ConvertToLocalNullAbleParameter.Add(dateTime);
            return dateTime;
        }

        public void ConvertToLocalDate<T>(T entity)
        {
            ConvertToLocalDateParameters.Add(entity);
        }
    }
}
