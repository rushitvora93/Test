using BasicTypes;
using DtoTypes;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Grpc.Core;
using WorkingCalendarService;

namespace FrameworksAndDrivers.RemoteData.GRPC.Wrapper
{
    public class WorkingCalendarClient : IWorkingCalendarClient
    {
        private readonly WorkingCalendars.WorkingCalendarsClient _workingCalendarsClient;

        public WorkingCalendarClient(WorkingCalendars.WorkingCalendarsClient workingCalendarsClient)
        {
            _workingCalendarsClient = workingCalendarsClient;
        }

        public WorkingCalendar GetWorkingCalendar()
        {
            return _workingCalendarsClient.GetWorkingCalendar(new NoParams(), new CallOptions());
        }

        public ListOfWorkingCalendarEntries GetWorkingCalendarEntriesForWorkingCalendarId(LongRequest id)
        {
            return _workingCalendarsClient.GetWorkingCalendarEntriesForWorkingCalendarId(id, new CallOptions());
        }

        public void InsertWorkingCalendarEntry(InsertWorkingCalendarEntryParameter param)
        {
            _workingCalendarsClient.InsertWorkingCalendarEntry(param, new CallOptions());
        }

        public void DeleteWorkingCalendarEntry(WorkingCalendarEntry oldEntry)
        {
            _workingCalendarsClient.DeleteWorkingCalendarEntry(oldEntry, new CallOptions());
        }

        public void SaveWorkingCalendar(WorkingCalendarDiff diff)
        {
            _workingCalendarsClient.SaveWorkingCalendar(diff, new CallOptions());
        }
    }
}
