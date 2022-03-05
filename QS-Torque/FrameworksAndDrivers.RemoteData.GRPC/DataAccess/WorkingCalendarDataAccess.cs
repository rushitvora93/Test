using System.Collections.Generic;
using BasicTypes;
using Client.Core.Diffs;
using Core.Entities;
using Core.UseCases;
using FrameworksAndDrivers.RemoteData.GRPC.T4Mapper;
using WorkingCalendarService;
using WorkingCalendar = DtoTypes.WorkingCalendar;
using WorkingCalendarEntry = Core.Entities.WorkingCalendarEntry;

namespace FrameworksAndDrivers.RemoteData.GRPC.DataAccess
{
    public interface IWorkingCalendarClient
    {
        DtoTypes.WorkingCalendar GetWorkingCalendar();
        ListOfWorkingCalendarEntries GetWorkingCalendarEntriesForWorkingCalendarId(LongRequest id);
        void InsertWorkingCalendarEntry(InsertWorkingCalendarEntryParameter param);
        void DeleteWorkingCalendarEntry(DtoTypes.WorkingCalendarEntry oldEntry);
        void SaveWorkingCalendar(DtoTypes.WorkingCalendarDiff diff);
    }


    public class WorkingCalendarDataAccess : IWorkingCalendarData
    {
        private readonly IClientFactory _clientFactory;
        private readonly Mapper _mapper = new Mapper();


        public WorkingCalendarDataAccess(IClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private IWorkingCalendarClient GetClient()
        {
            return _clientFactory.AuthenticationChannel.GetWorkingCalendarClient();
        }


        public List<WorkingCalendarEntry> LoadWorkingCalendarEntriesForWorkingCalendarId(WorkingCalendarId id)
        {
            var dtos = GetClient().GetWorkingCalendarEntriesForWorkingCalendarId(new LongRequest() { Value = id.ToLong() });
            var entities = new List<WorkingCalendarEntry>();
            var mapper = new Mapper();

            foreach (var dto in dtos.WorkingCalendarEntries)
            {
                var entity = mapper.DirectPropertyMapping(dto);
                entities.Add(entity);
            }
            return entities;
        }

        public void AddWorkingCalendarEntry(WorkingCalendarEntry newEntry, WorkingCalendarId calendarId)
        {
            var mapper = new Mapper();
            var assigner = new Assigner();
            var dto = mapper.DirectPropertyMapping(newEntry);
            GetClient().InsertWorkingCalendarEntry(new InsertWorkingCalendarEntryParameter()
            {
                Entry = dto,
                CalendarId = calendarId.ToLong()
            });
        }

        public void RemoveWorkingCalendarEntry(WorkingCalendarEntry oldEntry)
        {
            var mapper = new Mapper();
            var assigner = new Assigner();
            var dto = mapper.DirectPropertyMapping(oldEntry);
            GetClient().DeleteWorkingCalendarEntry(dto);
        }

        public Core.Entities.WorkingCalendar LoadWeekendSettings()
        {
            var mapper = new Mapper();
            var calendarDto = GetClient().GetWorkingCalendar();
            return mapper.DirectPropertyMapping(calendarDto);
        }

        public void SetWeekendSettings(WorkingCalendarDiff diff)
        {
            GetClient().SaveWorkingCalendar(new DtoTypes.WorkingCalendarDiff()
            {
                UserId = diff.User.UserId.ToLong(),
                Comment = "",
                Old = new WorkingCalendar()
                {
                    Id = 1,
                    AreSaturdaysFree = diff.Old.AreSaturdaysFree,
                    AreSundaysFree = diff.Old.AreSundaysFree
                },
                New = new WorkingCalendar()
                {
                    Id = 1,
                    AreSaturdaysFree = diff.New.AreSaturdaysFree,
                    AreSundaysFree = diff.New.AreSundaysFree
                }
            });
        }
    }
}
