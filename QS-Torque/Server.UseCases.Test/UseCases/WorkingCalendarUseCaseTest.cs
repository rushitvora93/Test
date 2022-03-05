using System.Collections.Generic;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;

namespace Server.UseCases.Test.UseCases
{
    class WorkingCalendarDataMock : IWorkingCalendarData
    {
        public bool GetWorkingCalendarCalled { get; set; }
        public WorkingCalendar GetWorkingCalendarReturnValue { get; set; }
        public bool GetWorkingCalendarEntriesForWorkingCalendarIdCalled { get; set; }
        public WorkingCalendarId GetWorkingCalendarEntriesForWorkingCalendarIdParameter { get; set; }
        public List<WorkingCalendarEntry> GetWorkingCalendarEntriesReturnValue { get; set; }
        public WorkingCalendarEntry InsertWorkingCalendarEntryParameterNewEntry { get; set; }
        public WorkingCalendarId InsertWorkingCalendarEntryParameterCalendarId { get; set; }
        public WorkingCalendarEntry DeleteWorkingCalendarEntryParameter { get; set; }
        public WorkingCalendarDiff SaveWorkingCalendarDiffParameter { get; set; }
        public bool CommitCalled { get; set; }


        public void Commit()
        {
            CommitCalled = true;
        }

        public WorkingCalendar GetWorkingCalendar()
        {
            GetWorkingCalendarCalled = true;
            return GetWorkingCalendarReturnValue;
        }

        public List<WorkingCalendarEntry> GetWorkingCalendarEntriesForWorkingCalendarId(WorkingCalendarId id)
        {
            GetWorkingCalendarEntriesForWorkingCalendarIdCalled = true;
            GetWorkingCalendarEntriesForWorkingCalendarIdParameter = id;
            return GetWorkingCalendarEntriesReturnValue;
        }

        public void InsertWorkingCalendarEntry(WorkingCalendarEntry newEntry, WorkingCalendarId calendarId)
        {
            InsertWorkingCalendarEntryParameterNewEntry = newEntry;
            InsertWorkingCalendarEntryParameterCalendarId = calendarId;
        }

        public void DeleteWorkingCalendarEntry(WorkingCalendarEntry oldEntry)
        {
            DeleteWorkingCalendarEntryParameter = oldEntry;
        }

        public void SaveWorkingCalendar(WorkingCalendarDiff diff)
        {
            SaveWorkingCalendarDiffParameter = diff;
        }
    }


    public class WorkingCalendarUseCaseTest
    {
        [Test]
        public void GetWorkingCalendarPassesDataFromDataAccess()
        {
            var tuple = CreateUseCaseTuple();
            var entity = new WorkingCalendar();
            tuple.data.GetWorkingCalendarReturnValue = entity;
            var result = tuple.useCase.GetWorkingCalendar();
            Assert.AreEqual(entity, result);
        }

        [Test]
        public void GetWorkingCalendarEntriesPassesDataFromDataAccess()
        {
            var tuple = CreateUseCaseTuple();
            var list = new List<WorkingCalendarEntry>() { new WorkingCalendarEntry(), new WorkingCalendarEntry() };
            tuple.data.GetWorkingCalendarEntriesReturnValue = list;
            var result = tuple.useCase.GetWorkingCalendarEntriesForWorkingCalendarId(new WorkingCalendarId(0));
            Assert.AreEqual(list, result);
        }

        [Test]
        public void InsertWorkingCalendarEntryCallsDataAccessWithData()
        {
            var tuple = CreateUseCaseTuple();
            var entry = new WorkingCalendarEntry();
            var calendarId = new WorkingCalendarId(0);
            tuple.useCase.InsertWorkingCalendarEntry(entry, calendarId);
            Assert.AreEqual(entry, tuple.data.InsertWorkingCalendarEntryParameterNewEntry);
            Assert.AreEqual(calendarId, tuple.data.InsertWorkingCalendarEntryParameterCalendarId);
        }

        [Test]
        public void InsertWorkingCalendarEntryCallsCommit()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.InsertWorkingCalendarEntry(null, null);
            Assert.IsTrue(tuple.data.CommitCalled);
        }

        [Test]
        public void DeleteWorkingCalendarEntryCallsDataAccessWithData()
        {
            var tuple = CreateUseCaseTuple();
            var entry = new WorkingCalendarEntry();
            tuple.useCase.DeleteWorkingCalendarEntry(entry);
            Assert.AreEqual(entry, tuple.data.DeleteWorkingCalendarEntryParameter);
        }

        [Test]
        public void DeleteWorkingCalendarEntryCallsCommit()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.DeleteWorkingCalendarEntry(null);
            Assert.IsTrue(tuple.data.CommitCalled);
        }

        [Test]
        public void SaveWorkingCalendarCallsDataAccessWithData()
        {
            var tuple = CreateUseCaseTuple();
            var diff = new WorkingCalendarDiff(null, null);
            tuple.useCase.SaveWorkingCalendar(diff);
            Assert.AreEqual(diff, tuple.data.SaveWorkingCalendarDiffParameter);
        }

        [Test]
        public void SaveWorkingCalendarCallsCommit()
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.SaveWorkingCalendar(null);
            Assert.IsTrue(tuple.data.CommitCalled);
        }

        [TestCase(9654)]
        [TestCase(2145)]
        public void GetWorkingCalendarEntriesForWorkingCalendarIdCallsDataAccessWithCorrectParameter(long id)
        {
            var tuple = CreateUseCaseTuple();
            tuple.useCase.GetWorkingCalendarEntriesForWorkingCalendarId(new WorkingCalendarId(id));
            Assert.AreEqual(id, tuple.data.GetWorkingCalendarEntriesForWorkingCalendarIdParameter.ToLong());
        }


        private static (WorkingCalendarUseCase useCase, WorkingCalendarDataMock data) CreateUseCaseTuple()
        {
            var data = new WorkingCalendarDataMock();
            var useCase = new WorkingCalendarUseCase(data);
            return (useCase, data);
        }
    }
}
