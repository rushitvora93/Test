using System;

namespace Core.UseCases
{
    public interface ITimeDataAccess
    {
        DateTime UtcNow();

        DateTime LocalNow();

        DateTime ConvertToUtc(DateTime dateTime);

        DateTime? ConvertToUtc(DateTime? dateTime);

        DateTime ConvertToLocalDate(DateTime dateTime);

        DateTime? ConvertToLocalDate(DateTime? dateTime);

        void ConvertToLocalDate<T>(T entity);
    }
}