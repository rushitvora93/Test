using System.Collections.Generic;
using BasicTypes;
using Client.Core.Diffs;
using Client.TestHelper.Mock;
using Core.Entities;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using NUnit.Framework;
using TestHelper.Checker;
using TestHelper.Mock;
using WorkingCalendarService;
using DateTime = System.DateTime;

namespace FrameworksAndDrivers.RemoteData.GRPC.Test.DataAccess
{
    class WorkingCalendarClientMock : IWorkingCalendarClient
    {
        public DtoTypes.WorkingCalendar GetWorkingCalendarReturnValue { get; set; }
        public LongRequest GetWorkingCalendarEntriesForWorkingCalendarIdParameter { get; set; }
        public ListOfWorkingCalendarEntries GetWorkingCalendarEntriesForWorkingCalendarIdReturnValue { get; set; }
        public InsertWorkingCalendarEntryParameter InsertWorkingCalendarEntryParameter { get; set; }
        public DtoTypes.WorkingCalendarEntry DeleteWorkingCalendarEntryParameter { get; set; }
        public DtoTypes.WorkingCalendarDiff SaveWorkingCalendarDiffParameter { get; set; }

        public DtoTypes.WorkingCalendar GetWorkingCalendar()
        {
            return GetWorkingCalendarReturnValue;
        }

        public ListOfWorkingCalendarEntries GetWorkingCalendarEntriesForWorkingCalendarId(LongRequest id)
        {
            GetWorkingCalendarEntriesForWorkingCalendarIdParameter = id;
            return GetWorkingCalendarEntriesForWorkingCalendarIdReturnValue;
        }

        public void InsertWorkingCalendarEntry(InsertWorkingCalendarEntryParameter param)
        {
            InsertWorkingCalendarEntryParameter = param;
        }

        public void DeleteWorkingCalendarEntry(DtoTypes.WorkingCalendarEntry oldEntry)
        {
            DeleteWorkingCalendarEntryParameter = oldEntry;
        }

        public void SaveWorkingCalendar(DtoTypes.WorkingCalendarDiff diff)
        {
            SaveWorkingCalendarDiffParameter = diff;
        }
    }


    public class WorkingCalendarDataAccessTest
    {
        [TestCaseSource(nameof(CreateAnonymousWorkingCalendarEntryDtos))]
        public void LoadWorkingCalendarEntriesReturnsDataOfClient(List<DtoTypes.WorkingCalendarEntry> dtoList)
        {
            var tuple = CreateDataAccessTuple();
            tuple.client.GetWorkingCalendarEntriesForWorkingCalendarIdReturnValue = new ListOfWorkingCalendarEntries();

            foreach (var dto in dtoList)
            {
                tuple.client.GetWorkingCalendarEntriesForWorkingCalendarIdReturnValue.WorkingCalendarEntries.Add(dto);
            }
            
            var result = tuple.dataAccess.LoadWorkingCalendarEntriesForWorkingCalendarId(new WorkingCalendarId(0));

            Assert.AreEqual(dtoList.Count, result.Count);
            CheckerFunctions.CollectionAssertAreEquivalent(dtoList, result, AreWorkingCalendarEntryDtoAndEntityEqual);
        }

        [TestCase("2222-06-22 00:00:00", "rfgewdmkopfjg", WorkingCalendarEntryRepetition.Yearly, WorkingCalendarEntryType.Holiday, 8)]
        [TestCase("2222-07-22 00:00:00", "ozmkonbtvc", WorkingCalendarEntryRepetition.Once, WorkingCalendarEntryType.ExtraShift, 6)]
        public void AddWorkingCalendarEntryCallsClientWithCorrectParameter(DateTime date, string desc, WorkingCalendarEntryRepetition repetition, WorkingCalendarEntryType type, long calendarId)
        {
            var tuple = CreateDataAccessTuple();
            var entity = new WorkingCalendarEntry()
            {
                Date = DateTime.Today,
                Description = new WorkingCalendarEntryDescription("cv78f6gb8hn9jm0ikßól"),
                Repetition = WorkingCalendarEntryRepetition.Yearly,
                Type = WorkingCalendarEntryType.Holiday
            };
            tuple.dataAccess.AddWorkingCalendarEntry(entity, new WorkingCalendarId(calendarId));
            Assert.IsTrue(AreWorkingCalendarEntryDtoAndEntityEqual(tuple.client.InsertWorkingCalendarEntryParameter.Entry, entity));
            Assert.AreEqual(calendarId, tuple.client.InsertWorkingCalendarEntryParameter.CalendarId);
        }

        [TestCase("2222-06-22 00:00:00", "rfgewdmkopfjg", WorkingCalendarEntryRepetition.Yearly, WorkingCalendarEntryType.Holiday)]
        [TestCase("2222-07-22 00:00:00", "ozmkonbtvc", WorkingCalendarEntryRepetition.Once, WorkingCalendarEntryType.ExtraShift)]
        public void DeleteWorkingCalendarEntryCallsClientWithCorrectParameter(DateTime date, string desc, WorkingCalendarEntryRepetition repetition, WorkingCalendarEntryType type)
        {
            var tuple = CreateDataAccessTuple();
            var entity = new WorkingCalendarEntry()
            {
                Date = DateTime.Today,
                Description = new WorkingCalendarEntryDescription("cv78f6gb8hn9jm0ikßól"),
                Repetition = WorkingCalendarEntryRepetition.Yearly,
                Type = WorkingCalendarEntryType.Holiday
            };
            tuple.dataAccess.RemoveWorkingCalendarEntry(entity);
            Assert.IsTrue(AreWorkingCalendarEntryDtoAndEntityEqual(tuple.client.DeleteWorkingCalendarEntryParameter, entity));
        }

        [TestCase(987, "hgafasgfad", true, false)]
        [TestCase(963, "34567890iuhgv", false, true)]
        public void LoadWeekendSettingsReturnsCorrectValueFromClient(long id, string name, bool saturdaysFree, bool sundaysFree)
        {
            var tuple = CreateDataAccessTuple();
            var dto = new DtoTypes.WorkingCalendar()
            {
                Id = id,
                Name = name,
                AreSaturdaysFree = saturdaysFree,
                AreSundaysFree = sundaysFree
            };
            tuple.client.GetWorkingCalendarReturnValue = dto;
            var result = tuple.dataAccess.LoadWeekendSettings();
            Assert.AreEqual(id, result.Id.ToLong());
            Assert.AreEqual(name, result.Name.ToDefaultString());
            Assert.AreEqual(saturdaysFree, result.AreSaturdaysFree);
            Assert.AreEqual(sundaysFree, result.AreSundaysFree);
        }

        [TestCase(true, false, false, true, 4)]
        [TestCase(false, true, true, false, 9)]
        public void SetWeekendSettingsPassesCorrectDataToClient(bool saturdaysFreeOld, bool sundaysFreeOld, bool saturdaysFreeNew, bool sundaysFreeNew, long userId)
        {
            var tuple = CreateDataAccessTuple();
            tuple.dataAccess.SetWeekendSettings(new WorkingCalendarDiff(new WorkingCalendar()
                {
                    AreSaturdaysFree = saturdaysFreeOld,
                    AreSundaysFree = sundaysFreeOld
                },
                new WorkingCalendar()
                {
                    AreSaturdaysFree = saturdaysFreeNew,
                    AreSundaysFree = sundaysFreeNew
                },
                new User() { UserId = new UserId(userId) }
            )); ;
            
            Assert.AreEqual(1, tuple.client.SaveWorkingCalendarDiffParameter.Old.Id);
            Assert.AreEqual(1, tuple.client.SaveWorkingCalendarDiffParameter.New.Id);
            Assert.AreEqual(saturdaysFreeOld, tuple.client.SaveWorkingCalendarDiffParameter.Old.AreSaturdaysFree);
            Assert.AreEqual(sundaysFreeOld, tuple.client.SaveWorkingCalendarDiffParameter.Old.AreSundaysFree);
            Assert.AreEqual(saturdaysFreeNew, tuple.client.SaveWorkingCalendarDiffParameter.New.AreSaturdaysFree);
            Assert.AreEqual(sundaysFreeNew, tuple.client.SaveWorkingCalendarDiffParameter.New.AreSundaysFree);
            Assert.AreEqual(userId, tuple.client.SaveWorkingCalendarDiffParameter.UserId);
        }

        [TestCase(32)]
        [TestCase(951)]
        public void LoadWorkingCalendarEntriesForWorkingCalendarIdPassesIdToClient(long id)
        {
            var tuple = CreateDataAccessTuple();
            tuple.client.GetWorkingCalendarEntriesForWorkingCalendarIdReturnValue = new ListOfWorkingCalendarEntries();
            tuple.dataAccess.LoadWorkingCalendarEntriesForWorkingCalendarId(new WorkingCalendarId(id));
            Assert.AreEqual(id, tuple.client.GetWorkingCalendarEntriesForWorkingCalendarIdParameter.Value);
        }
        
        private bool AreWorkingCalendarDtoAndEntityEqual(DtoTypes.WorkingCalendar dto, WorkingCalendar entity)
        {
            return dto.Id == entity.Id.ToLong() &&
                   dto.Name == entity.Name.ToDefaultString() &&
                   dto.AreSaturdaysFree == entity.AreSaturdaysFree &&
                   dto.AreSundaysFree == entity.AreSundaysFree;
        }

        private bool AreWorkingCalendarEntryDtoAndEntityEqual(DtoTypes.WorkingCalendarEntry dto, WorkingCalendarEntry entity)
        {
            return EqualityChecker.ArePrimitiveDateTimeAndDtoEqual(entity.Date, dto.Date) &&
                   dto.Description == entity.Description.ToDefaultString() &&
                   dto.Repeated == (entity.Repetition == WorkingCalendarEntryRepetition.Yearly ? 1 : 0) &&
                   dto.IsFree == (entity.Type == WorkingCalendarEntryType.Holiday ? 1 : 0);
        }

        
        private static IEnumerable<List<DtoTypes.WorkingCalendarEntry>> CreateAnonymousWorkingCalendarEntryDtos()
        {
            yield return new List<DtoTypes.WorkingCalendarEntry>()
            {
                new DtoTypes.WorkingCalendarEntry()
                {
                    Date = new BasicTypes.DateTime() { Ticks = new System.DateTime(7,6,5).Ticks },
                    Description = "üguj",
                    Repeated = 1,
                    IsFree = 0
                },
                new DtoTypes.WorkingCalendarEntry()
                {
                    Date = new BasicTypes.DateTime() { Ticks = new System.DateTime(8,9,1).Ticks },
                    Description = "fpwoerifjwer",
                    Repeated = 0,
                    IsFree = 1
                }
            };
            yield return new List<DtoTypes.WorkingCalendarEntry>()
            {
                new DtoTypes.WorkingCalendarEntry()
                {
                    Date = new BasicTypes.DateTime() {Ticks = new System.DateTime(77,8,14).Ticks},
                    Description = "wertzui",
                    Repeated = 1,
                    IsFree = 1
                }
            };
        }
        
        static (WorkingCalendarDataAccess dataAccess, WorkingCalendarClientMock client) CreateDataAccessTuple()
        {
            var client = new WorkingCalendarClientMock();
            var clientFactory = new ClientFactoryMock()
            {
                AuthenticationChannel = new ChannelWrapperMock()
                {
                    GetWorkingCalendarClientReturnValue = client
                }
            };
            var dataAccess = new WorkingCalendarDataAccess(clientFactory);

            return (dataAccess, client);
        }
    }
}
