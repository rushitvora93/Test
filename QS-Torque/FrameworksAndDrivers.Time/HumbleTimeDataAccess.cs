using System;
using Core.UseCases;

namespace FrameworksAndDrivers.Time
{
    public class HumbleTimeDataAccess: ITimeDataAccess
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }

        public DateTime LocalNow()
        {
            return DateTime.Now;
        }

        public DateTime ConvertToUtc(DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }

        public DateTime? ConvertToUtc(DateTime? dateTime)
        {
            return dateTime?.ToUniversalTime();
        }

        public DateTime ConvertToLocalDate(DateTime dateTime)
        {
            return dateTime.ToLocalTime();
        }

        public DateTime? ConvertToLocalDate(DateTime? dateTime)
        {
            return dateTime?.ToLocalTime();
        }

        public void ConvertToLocalDate<T>(T entity)
        {
            if (entity == null)
            {
                return;
            }

            foreach (var property in typeof(T).GetProperties())
            {
                if (property.PropertyType == typeof(DateTime))
                {
                    var date = (DateTime)property.GetValue(entity);
                    date = date.ToLocalTime();
                    property.SetValue(entity, date);
                }
                else if (property.PropertyType == typeof(DateTime?))
                {
                    var date = (DateTime?)property.GetValue(entity);
                    if (date != null)
                    {
                        date = date.Value.ToLocalTime();
                        property.SetValue(entity, date);
                    }
                }
            }
        }
    }
}
