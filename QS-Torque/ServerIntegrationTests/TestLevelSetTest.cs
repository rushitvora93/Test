using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper.Factories;
using TestHelper.Mock;

namespace ServerIntegrationTests
{
    [TestClass]
    public class TestLevelSetTest
    {
        private readonly TestSetup _testSetup;

        public TestLevelSetTest()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddTestLevelSet()
        {
            var dataAccess = new TestLevelSetDataAccess(_testSetup.ClientFactory);
            var entry = AddNewTestLevelSetWithDataAccess(dataAccess);

            var entries = dataAccess.LoadTestLevelSets();
            var result = entries.Find(x => x.Id.Equals(entry.Id));

            Assert.IsTrue(entry.Id.ToLong() != 0);
            Assert.IsTrue(entry.EqualsByContent(result));
        }

        [TestMethod]
        public void SaveTestLevelSet()
        {
            var dataAccess = new TestLevelSetDataAccess(_testSetup.ClientFactory);
            var oldEntry = AddNewTestLevelSetWithDataAccess(dataAccess);

            var updatedEntry = new TestLevelSet()
            {
                Id = oldEntry.Id,
                Name = new TestLevelSetName("glkdbhnjkl"),
                TestLevel1 = new TestLevel()
                {
                    Id = oldEntry.TestLevel1.Id,
                    ConsiderWorkingCalendar = false,
                    IsActive = true,
                    SampleNumber = 74120,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 290,
                        Type = IntervalType.EveryXShifts
                    }
                },
                TestLevel2 = new TestLevel()
                {
                    Id = oldEntry.TestLevel2.Id,
                    ConsiderWorkingCalendar = false,
                    IsActive = false,
                    SampleNumber = 345,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 37,
                        Type = IntervalType.XTimesAMonth
                    }
                },
                TestLevel3 = new TestLevel()
                {
                    Id = oldEntry.TestLevel3.Id,
                    ConsiderWorkingCalendar = true,
                    IsActive = true,
                    SampleNumber = 3456,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 59,
                        Type = IntervalType.EveryXDays
                    }
                }
            };
            
            dataAccess.UpdateTestLevelSet(new Client.Core.Diffs.TestLevelSetDiff() { Old = oldEntry, New = updatedEntry, User = new User() { UserId = new UserId(0) }, Comment = new HistoryComment("wrht7") });

            var testLevelSets = dataAccess.LoadTestLevelSets();
            var result = testLevelSets.Find(x => x.Id.ToLong() == updatedEntry.Id.ToLong());

            Assert.IsTrue(updatedEntry.EqualsByContent(result));
        }

        [TestMethod]
        public void RemoveTestLevelSet()
        {
            var dataAccess = new TestLevelSetDataAccess(_testSetup.ClientFactory);
            var oldEntry = AddNewTestLevelSetWithDataAccess(dataAccess);

            var entries = dataAccess.LoadTestLevelSets();
            var result = entries.Find(x => x.Id.ToLong() == oldEntry.Id.ToLong());
            Assert.IsNotNull(result);

            dataAccess.RemoveTestLevelSet(new Client.Core.Diffs.TestLevelSetDiff() { Old = oldEntry, User = new User() { UserId = new UserId(0) } });
            entries = dataAccess.LoadTestLevelSets();
            result = entries.Find(x => x.Id.ToLong() == oldEntry.Id.ToLong());
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadTestLevelSets()
        {
            var dataAccess = new TestLevelSetDataAccess(_testSetup.ClientFactory);
            var entry = AddNewTestLevelSetWithDataAccess(dataAccess);

            var entries = dataAccess.LoadTestLevelSets();
            var result = entries.Find(x => x.Id.ToLong() == entry.Id.ToLong());

            Assert.IsTrue(entry.EqualsByContent(result));
        }

        [TestMethod]
        public void IsTestLevelSetNameUniqueReturnsTrue()
        {
            var dataAccess = new TestLevelSetDataAccess(_testSetup.ClientFactory);
            var entry = AddNewTestLevelSetWithDataAccess(dataAccess);

            var result = dataAccess.IsTestLevelSetNameUnique(entry.Name.ToDefaultString() + "T");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsTestLevelSetNameUniqueReturnsFalse()
        {
            var dataAccess = new TestLevelSetDataAccess(_testSetup.ClientFactory);
            var entry = AddNewTestLevelSetWithDataAccess(dataAccess);

            var result = dataAccess.IsTestLevelSetNameUnique(entry.Name.ToDefaultString());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DoesTestLevelSetHaveReferencesReturnsFalse()
        {
            var dataAccess = new TestLevelSetDataAccess(_testSetup.ClientFactory);
            var entry = AddNewTestLevelSetWithDataAccess(dataAccess);

            var result = dataAccess.DoesTestLevelSetHaveReferences(entry);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DoesTestLevelSetHaveReferencesReturnsTrue()
        {
            var dataAccess = new TestLevelSetDataAccess(_testSetup.ClientFactory);
            var entry = AddNewTestLevelSetWithDataAccess(dataAccess);
            var locationToolAssignmentDataAccess = new LocationToolAssignmentDataAccess(_testSetup.ClientFactory, new MockLocationDisplayFormatter(), new TimeDataAccessMock());
            
            locationToolAssignmentDataAccess.AddTestConditions(new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(DateTime.Now.Ticks),
                AssignedLocation = new Location() { Id = new LocationId(DateTime.Now.Ticks * 10 + 1) },
                AssignedTool = new Tool() { Id = new ToolId(DateTime.Now.Ticks * 10 + 2), SerialNumber = new ToolSerialNumber($"s_{DateTime.Now.Ticks}"), InventoryNumber = new ToolInventoryNumber($"invNum_{DateTime.Now.Ticks}") },
                TestLevelSetChk = entry,
                TestLevelSetMfu = entry
            },
            new User() { UserId = new UserId(0) });

            var result = dataAccess.DoesTestLevelSetHaveReferences(entry);

            Assert.IsTrue(result);
        }


        private TestLevelSet AddNewTestLevelSetWithDataAccess(TestLevelSetDataAccess dataAccess)
        {
            var name = "testLevelSet_" + System.DateTime.Now.Ticks;

            var entries = dataAccess.LoadTestLevelSets();
            var result = entries.Find(x => x.Name.ToDefaultString() == name);
            Assert.IsNull(result);

            var entry = new TestLevelSet()
            {
                Id = new TestLevelSetId(0),
                Name = new TestLevelSetName(name),
                TestLevel1 = new TestLevel()
                {
                    Id = new TestLevelId(0),
                    ConsiderWorkingCalendar = true,
                    IsActive = false,
                    SampleNumber = 3456,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 357,
                        Type = IntervalType.XTimesADay
                    }
                },
                TestLevel2 = new TestLevel()
                {
                    Id = new TestLevelId(0),
                    ConsiderWorkingCalendar = false,
                    IsActive = false,
                    SampleNumber = 345,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 65,
                        Type = IntervalType.XTimesAShift
                    }
                },
                TestLevel3 = new TestLevel()
                {
                    Id = new TestLevelId(0),
                    ConsiderWorkingCalendar = true,
                    IsActive = false,
                    SampleNumber = 3456,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 357,
                        Type = IntervalType.XTimesAWeek
                    }
                }
            };

            return dataAccess.AddTestLevelSet(new Client.Core.Diffs.TestLevelSetDiff() { New = entry, User = new User() { UserId = new UserId(0) } });
        }
    }
}
