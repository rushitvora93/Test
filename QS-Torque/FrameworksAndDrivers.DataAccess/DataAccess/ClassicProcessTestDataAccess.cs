using System;
using System.Collections.Generic;
using System.Linq;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Microsoft.EntityFrameworkCore;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ClassicProcessTestDataAccess : DataAccessBase, IClassicProcessTestData
    {
        private readonly Mapper _mapper = new Mapper();
        public ClassicProcessTestDataAccess(ITransactionDbContext transactionContext, SqliteDbContext dbContext) : 
            base(transactionContext, dbContext)
        {
        }

        public List<ClassicProcessTest> GetClassicProcessHeaderFromLocation(LocationId locationId)
        {
            var tests = _dbContext.ClassicProcessTest
                .Include(x => x.GlobalHistory)
                .Include(x => x.TestEquipment.TestEquipmentModel.Manufacturer)
                .Include(x => x.ClassicProcessTestLocation)
                .Where(x => x.ClassicProcessTestLocation.LOCATION_ID == locationId.ToLong()).ToList();

            var results = new List<ClassicProcessTest>();
            foreach (var classicTest in tests)
            {
                var data = _mapper.DirectPropertyMapping(classicTest);
                data.Timestamp = classicTest.GlobalHistory.TIMESTAMP.Value;
                results.Add(data);
            }

            return results;
        }

        public List<ClassicProcessTestValue> GetValuesFromClassicProcessHeader(List<GlobalHistoryId> ids)
        {
            var longIds = ids.Select(x => x.ToLong()).ToList();
            var values = _dbContext.ClassicProcessTestValues.Where(x => longIds.Contains(x.GLOBALHISTORYID)).ToList();

            var datas = new List<Server.Core.Entities.ClassicProcessTestValue>();
            values.ForEach(x => datas.Add(_mapper.DirectPropertyMapping(x)));

            return datas;
        }

        public List<(DateTime, Shift?)> GetTestsForTimePeriod(ProcessControlConditionId locToolId, DateTime startPeriodDate, Shift? startPeriodShift,
            DateTime endPeriodDate, Shift? endPeriodShift, TimeSpan changeOfDay)
        {
            var result = new List<(DateTime, Shift?)>();
            var tests = _dbContext.ClassicProcessTest.Where(x => x.CONDLOCID == locToolId.ToLong()).Join(_dbContext.GlobalHistories,
                x => x.GlobalHistoryId,
                y => y.ID,
                (x, y) => new { ClassicProcessTest = x, GlobalHistory = y });

            var dateTimeComparison = new DateTimeComparisonWithChangeOfDay();
            foreach (var test in tests)
            {
                if (test.GlobalHistory.TIMESTAMP == null) { continue; }

                if (dateTimeComparison.IsDateTimeEqualToDateConsideringChangeOfDay(test.GlobalHistory.TIMESTAMP.Value, startPeriodDate, changeOfDay) &&
                   dateTimeComparison.IsDateTimeEqualToDateConsideringChangeOfDay(test.GlobalHistory.TIMESTAMP.Value, endPeriodDate, changeOfDay))
                {
                    // Left border, right border and test date are the same date
                    if (test.ClassicProcessTest.SHIFT != null && startPeriodShift != null && endPeriodShift != null)
                    {
                        if ((long)startPeriodShift <= test.ClassicProcessTest.SHIFT && (long)endPeriodShift >= test.ClassicProcessTest.SHIFT)
                        {
                            result.Add((test.GlobalHistory.TIMESTAMP.Value, (Shift)test.ClassicProcessTest.SHIFT));
                            continue;
                        }
                    }
                    else
                    {
                        result.Add((test.GlobalHistory.TIMESTAMP.Value, null));
                        continue;
                    }
                }

                if (dateTimeComparison.IsDateTimeEqualToDateConsideringChangeOfDay(test.GlobalHistory.TIMESTAMP.Value, startPeriodDate, changeOfDay))
                {
                    // Left border and test date are the same date
                    if (test.ClassicProcessTest.SHIFT != null && startPeriodShift != null)
                    {
                        if ((long)startPeriodShift <= test.ClassicProcessTest.SHIFT)
                        {
                            result.Add((test.GlobalHistory.TIMESTAMP.Value, (Shift)test.ClassicProcessTest.SHIFT));
                            continue;
                        }
                    }
                    else
                    {
                        result.Add((test.GlobalHistory.TIMESTAMP.Value, null));
                        continue;
                    }
                }

                if (dateTimeComparison.IsDateTimeEqualToDateConsideringChangeOfDay(test.GlobalHistory.TIMESTAMP.Value, endPeriodDate, changeOfDay))
                {
                    // Right border and test date are the same date
                    if (test.ClassicProcessTest.SHIFT != null && endPeriodShift != null)
                    {
                        if ((long)endPeriodShift >= test.ClassicProcessTest.SHIFT)
                        {
                            result.Add((test.GlobalHistory.TIMESTAMP.Value, (Shift)test.ClassicProcessTest.SHIFT));
                            continue;
                        }
                    }
                    else
                    {
                        result.Add((test.GlobalHistory.TIMESTAMP.Value, null));
                        continue;
                    }
                }

                if (dateTimeComparison.IsDateTimeExclusivelyBetweenDatesConsideringChangeOfDay(test.GlobalHistory.TIMESTAMP.Value, startPeriodDate, endPeriodDate, changeOfDay))
                {
                    // Test date is between left border and right border exclusively
                    if (startPeriodShift != null && test.ClassicProcessTest.SHIFT != null && endPeriodShift != null)
                    {
                        result.Add((test.GlobalHistory.TIMESTAMP.Value, (Shift)test.ClassicProcessTest.SHIFT));
                        continue;
                    }
                    else
                    {
                        result.Add((test.GlobalHistory.TIMESTAMP.Value, null));
                        continue;
                    }
                }
            }

            return result;
        }
    }
}
