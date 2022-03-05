using System.Collections.Generic;
using BasicTypes;
using NUnit.Framework;
using Server.Core.Diffs;
using Server.Core.Entities;
using Server.UseCases.UseCases;
using WorkingCalendarService;
using DateTime = System.DateTime;

namespace FrameworksAndDrivers.NetworkView.Test.Services
{
    class WorkingCalendarUseCaseMock : IWorkingCalendarUseCase
    {
        public WorkingCalendar GetWorkingCalendarReturnValue { get; set; }

        public List<WorkingCalendarEntry> GetWorkingCalendarEntriesForWorkingCalendarIdReturnValue { get; set; }
        public WorkingCalendarId GetWorkingCalendarEntriesForWorkingCalendarIdParameter { get; set; }
        public WorkingCalendarEntry InsertWorkingCalendarEntryParameterNewEntry { get; set; }
        public WorkingCalendarId InsertWorkingCalendarEntryParameterCalendarId { get; set; }
        public WorkingCalendarEntry DeleteWorkingCalendarEntryParameter { get; set; }
        public WorkingCalendarDiff SaveWorkingCalendarDiffParameter { get; set; }

        public WorkingCalendar GetWorkingCalendar()
        {
            return GetWorkingCalendarReturnValue;
        }

        public List<WorkingCalendarEntry> GetWorkingCalendarEntriesForWorkingCalendarId(WorkingCalendarId id)
        {
            GetWorkingCalendarEntriesForWorkingCalendarIdParameter = id;
            return GetWorkingCalendarEntriesForWorkingCalendarIdReturnValue;
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


    public class WorkingCalendarServiceTest
    {
        [TestCase(987, true, false)]
        [TestCase(963, false, true)]
        public void GetWorkingCalendarReturnsResultOfUseCase(long id, bool saturdaysFree, bool sundaysFree)
        {
            var tuple = CreateServiceTuple();
            var entity = new WorkingCalendar()
            {
                Id = new WorkingCalendarId(id),
                AreSaturdaysFree = saturdaysFree,
                AreSundaysFree = sundaysFree
            };
            tuple.useCase.GetWorkingCalendarReturnValue = entity;
            var dto = tuple.service.GetWorkingCalendar(new NoParams(), null).Result;

            Assert.IsTrue(AreWorkingCalendarDtoAndEntityEqual(dto, entity));
        }

        [Test]
        public void GetWorkingCalendarEntriesForWorkingCalendarIdReturnsResultOfUseCase()
        {
            var tuple = CreateServiceTuple();
            var entity1 = new WorkingCalendarEntry()
            {
                Date = DateTime.Today,
                Description = new WorkingCalendarEntryDescription("cv78f6gb8hn9jm0ikßól"),
                Repetition = WorkingCalendarEntryRepetition.Yearly,
                Type = WorkingCalendarEntryType.Holiday
            };
            var entity2 = new WorkingCalendarEntry()
            {
                Date = DateTime.Today.AddDays(1),
                Description = new WorkingCalendarEntryDescription("hgcawgshdj8fkga"),
                Repetition = WorkingCalendarEntryRepetition.Once,
                Type = WorkingCalendarEntryType.ExtraShift
            };
            tuple.useCase.GetWorkingCalendarEntriesForWorkingCalendarIdReturnValue = new List<WorkingCalendarEntry>() { entity1, entity2 };

            var result = tuple.service.GetWorkingCalendarEntriesForWorkingCalendarId(new LongRequest(), null).Result;
            Assert.AreEqual(2, result.WorkingCalendarEntries.Count);
            Assert.IsTrue(AreWorkingCalendarEntryDtoAndEntityEqual(result.WorkingCalendarEntries[0], entity1));
            Assert.IsTrue(AreWorkingCalendarEntryDtoAndEntityEqual(result.WorkingCalendarEntries[1], entity2));
        }

        [TestCase(5, 8, 1234, "gbnimokp,", 1, 0, 98)]
        [TestCase(6, 12, 1999, "disrusfznoe", 0, 1, 32)]
        public void InsertWorkingCalendarEntryCallsUseCaseWithCorrectData(int day, int month, int year, string desc, long repeated, long isFree, long calendarId)
        {
            var tuple = CreateServiceTuple();
            var dto = new DtoTypes.WorkingCalendarEntry()
            {
                Date = new BasicTypes.DateTime() { Ticks = new System.DateTime(year, month, day).Ticks },
                Description = desc,
                Repeated = repeated,
                IsFree = isFree
            };
            tuple.service.InsertWorkingCalendarEntry(new InsertWorkingCalendarEntryParameter()
            {
                Entry = dto,
                CalendarId = calendarId
            }, null);
            Assert.IsTrue(AreWorkingCalendarEntryDtoAndEntityEqual(dto, tuple.useCase.InsertWorkingCalendarEntryParameterNewEntry));
            Assert.AreEqual(calendarId, tuple.useCase.InsertWorkingCalendarEntryParameterCalendarId.ToLong());
        }

        [TestCase(5, 8, 1234, "gbnimokp,", 1, 0)]
        [TestCase(6, 12, 1999, "disrusfznoe", 0, 1)]
        public void DeleteWorkingCalendarEntryCallsUseCaseWithCorrectData(int day, int month, int year, string desc, long repeated, long isFree)
        {
            var tuple = CreateServiceTuple();
            var dto = new DtoTypes.WorkingCalendarEntry()
            {
                Date = new BasicTypes.DateTime() { Ticks = new System.DateTime(year, month, day).Ticks },
                Description = desc,
                Repeated = repeated,
                IsFree = isFree
            };
            tuple.service.DeleteWorkingCalendarEntry(dto, null);
            Assert.IsTrue(AreWorkingCalendarEntryDtoAndEntityEqual(dto, tuple.useCase.DeleteWorkingCalendarEntryParameter));
        }

        [TestCase(987, true, false, 963, false, true, 8, "hidgoapükphio")]
        [TestCase(963, false, true, 987, true, false, 1, "zho9jpokjhon")]
        public void SaveWorkingCalendarCallsUseCaseWithCorrectData(long idOld, bool saturdaysFreeOld, bool sundaysFreeOld, long idNew, bool saturdaysFreeNew, bool sundaysFreeNew, long userId, string comment)
        {
            var tuple = CreateServiceTuple();
            var oldVal = new DtoTypes.WorkingCalendar()
            {
                Id = idOld,
                AreSaturdaysFree = saturdaysFreeOld,
                AreSundaysFree = sundaysFreeOld
            };
            var newVal = new DtoTypes.WorkingCalendar()
            {
                Id = idNew,
                AreSaturdaysFree = saturdaysFreeNew,
                AreSundaysFree = sundaysFreeNew
            };
            tuple.service.SaveWorkingCalendar(new DtoTypes.WorkingCalendarDiff() { Old = oldVal, New = newVal, UserId = userId, Comment = comment }, null);
            Assert.IsTrue(AreWorkingCalendarDtoAndEntityEqual(oldVal, tuple.useCase.SaveWorkingCalendarDiffParameter.Old));
            Assert.IsTrue(AreWorkingCalendarDtoAndEntityEqual(newVal, tuple.useCase.SaveWorkingCalendarDiffParameter.New));
            Assert.AreEqual(userId, tuple.useCase.SaveWorkingCalendarDiffParameter.User.UserId.ToLong());
            Assert.AreEqual(comment, tuple.useCase.SaveWorkingCalendarDiffParameter.Comment.ToDefaultString());
        }

        [TestCase(27)]
        [TestCase(91)]
        public void GetWorkingCalendarEntriesForWorkingCalendarIdCallsUseCaseWithCorrectParameter(long id)
        {
            var tuple = CreateServiceTuple();
            tuple.useCase.GetWorkingCalendarEntriesForWorkingCalendarIdReturnValue = new List<WorkingCalendarEntry>();
            tuple.service.GetWorkingCalendarEntriesForWorkingCalendarId(new LongRequest() { Value = id }, null);
            Assert.AreEqual(id, tuple.useCase.GetWorkingCalendarEntriesForWorkingCalendarIdParameter.ToLong());
        }


        private bool AreWorkingCalendarDtoAndEntityEqual(DtoTypes.WorkingCalendar dto, WorkingCalendar entity)
        {
            return dto.Id == entity.Id.ToLong() &&
                   dto.AreSaturdaysFree == entity.AreSaturdaysFree &&
                   dto.AreSundaysFree == entity.AreSundaysFree;
        }

        private bool AreWorkingCalendarEntryDtoAndEntityEqual(DtoTypes.WorkingCalendarEntry dto, WorkingCalendarEntry entity)
        {
            return EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(entity.Date, dto.Date) &&
                   dto.Repeated == (entity.Repetition == WorkingCalendarEntryRepetition.Yearly ? 1 : 0) &&
                   dto.IsFree == (entity.Type == WorkingCalendarEntryType.Holiday ? 1 : 0);
        }

        private (NetworkView.Services.WorkingCalendarService service, WorkingCalendarUseCaseMock useCase) CreateServiceTuple()
        {
            var useCase = new WorkingCalendarUseCaseMock();
            var service = new NetworkView.Services.WorkingCalendarService(null, useCase);
            return (service, useCase);
        }
    }
}
