using System;
using System.Collections.Generic;
using System.Linq;
using FrameworksAndDrivers.DataAccess.DbContext;
using FrameworksAndDrivers.DataAccess.DbEntities;
using FrameworksAndDrivers.DataAccess.T4Mapper;
using Microsoft.EntityFrameworkCore;
using Server.Core.Entities;
using Server.Core.Enums;
using Server.UseCases.UseCases;
using ClassicChkTest = Server.Core.Entities.ClassicChkTest;

namespace FrameworksAndDrivers.DataAccess.DataAccess
{
    public class ClassicChkTestDataAccess : DataAccessBase, IClassicChkTestData
    {
        private readonly IGlobalHistoryDataAccess _globalHistoryDataAccess;
        private readonly Mapper _mapper = new Mapper();

        public ClassicChkTestDataAccess(ITransactionDbContext transactionContext, SqliteDbContext dbContext, IGlobalHistoryDataAccess globalHistoryDataAccess) 
            : base(transactionContext, dbContext)
        {
            _globalHistoryDataAccess = globalHistoryDataAccess;
        }

        public List<(DateTime, Shift?)> GetTestsForTimePeriod(LocationToolAssignmentId locToolId, DateTime startPeriodDate, Shift? startPeriodShift,
            DateTime endPeriodDate, Shift? endPeriodShift, TimeSpan changeOfDay)
        {
            var result = new List<(DateTime, Shift?)>();
            var tests = _dbContext.ClassicChkTests.Where(x => x.LocPowId == locToolId.ToLong()).Join(_dbContext.GlobalHistories,
                x => x.GlobalHistoryId,
                y => y.ID,
                (x, y) => new { ClassicChkTest = x, GlobalHistory = y });

            var dateTimeComparison = new DateTimeComparisonWithChangeOfDay();
            foreach (var test in tests)
            {
                if (test.GlobalHistory.TIMESTAMP == null) { continue; }

                if (dateTimeComparison.IsDateTimeEqualToDateConsideringChangeOfDay(test.GlobalHistory.TIMESTAMP.Value, startPeriodDate, changeOfDay) &&
                    dateTimeComparison.IsDateTimeEqualToDateConsideringChangeOfDay(test.GlobalHistory.TIMESTAMP.Value, endPeriodDate, changeOfDay))
                {
                    // Left border, right border and test date are the same date
                    if (test.ClassicChkTest.Shift != null && startPeriodShift != null && endPeriodShift != null)
                    {
                        if ((long)startPeriodShift <= test.ClassicChkTest.Shift && (long)endPeriodShift >= test.ClassicChkTest.Shift)
                        {
                            result.Add((test.GlobalHistory.TIMESTAMP.Value, (Shift)test.ClassicChkTest.Shift));
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
                    if (test.ClassicChkTest.Shift != null && startPeriodShift != null)
                    {
                        if ((long)startPeriodShift <= test.ClassicChkTest.Shift)
                        {
                            result.Add((test.GlobalHistory.TIMESTAMP.Value, (Shift)test.ClassicChkTest.Shift));
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
                    if (test.ClassicChkTest.Shift != null && endPeriodShift != null)
                    {
                        if ((long)endPeriodShift >= test.ClassicChkTest.Shift)
                        {
                            result.Add((test.GlobalHistory.TIMESTAMP.Value, (Shift)test.ClassicChkTest.Shift));
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
                    if (startPeriodShift != null && test.ClassicChkTest.Shift != null && endPeriodShift != null)
                    {
                        result.Add((test.GlobalHistory.TIMESTAMP.Value, (Shift)test.ClassicChkTest.Shift));
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

        public List<ClassicChkTest> GetClassicChkHeaderFromTool(long powToolId, long? locationId)
        {
            var classicChkTestsQuery = _dbContext.ClassicChkTests
                .Include(x => x.GlobalHistory)
                .Include(x => x.TestEquipment.TestEquipmentModel.Manufacturer)
                .Where(x => x.POW_TOOL_ID == powToolId);

            if (locationId != null)
            {
                classicChkTestsQuery = classicChkTestsQuery.Include(x => x.ClassicChkTestLocation)
                    .Where(x => x.ClassicChkTestLocation != null && x.ClassicChkTestLocation.LOCATION_ID == locationId.Value);
            }

            var classicTests = classicChkTestsQuery.ToList();

            var results = new List<ClassicChkTest>();
            foreach (var classicTest in classicTests)
            {
                var data = _mapper.DirectPropertyMapping(classicTest);
                data.Timestamp = classicTest.GlobalHistory.TIMESTAMP.Value;
                results.Add(data);
            }
            
            return results;
        }


        public List<Server.Core.Entities.ClassicChkTestValue> GetValuesFromClassicChkHeader(List<GlobalHistoryId> ids)
        {
            var longIds = ids.Select(x => x.ToLong()).ToList();
            var values = _dbContext.ClassicChkTestValues.Where(x => longIds.Contains(x.GLOBALHISTORYID)).ToList();

            var datas = new List<Server.Core.Entities.ClassicChkTestValue>();
            values.ForEach(x => datas.Add(_mapper.DirectPropertyMapping(x)));

            return datas;
        }

        public void InsertClassicChkTests(List<ClassicChkTest> tests)
        {
            foreach (var test in tests)
            {
                var globalHistoryId = _globalHistoryDataAccess.CreateGlobalHistory("DATA INSERTED", test.Timestamp);

                var dbTest = _mapper.DirectPropertyMapping(test);
                dbTest.GlobalHistoryId = globalHistoryId;

                _dbContext.ClassicChkTests.Add(dbTest);

                var classicTestLocation = new ClassicChkTestLocation()
                {
                    GLOBALHISTORYID = dbTest.GlobalHistoryId,
                    LOCATION_ID = test.TestLocation.LocationId.ToLong(),
                    LOCTREE_ID = test.TestLocation.LocationDirectoryId.ToLong(),
                    LOCATION_TREE_PATH = test.TestLocation.LocationTreePath
                };

                _dbContext.ClassicChkTestLocations.Add(classicTestLocation);

                foreach (var val in test.TestValues)
                {
                    var dbTestValue = new DbEntities.ClassicChkTestValue()
                    {
                        GLOBALHISTORYID = dbTest.GlobalHistoryId,
                        POSITION = val.Position,
                        VALUE_UNIT1 = val.ValueUnit1,
                        VALUE_UNIT2 = val.ValueUnit2
                    };

                    _dbContext.ClassicChkTestValues.Add(dbTestValue);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
