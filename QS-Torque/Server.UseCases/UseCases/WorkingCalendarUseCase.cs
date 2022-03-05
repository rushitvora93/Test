using System.Collections.Generic;
using Server.Core.Diffs;
using Server.Core.Entities;

namespace Server.UseCases.UseCases
{
    public interface IWorkingCalendarData
    {
        void Commit();
        WorkingCalendar GetWorkingCalendar();
        List<WorkingCalendarEntry> GetWorkingCalendarEntriesForWorkingCalendarId(WorkingCalendarId id);
        void InsertWorkingCalendarEntry(WorkingCalendarEntry newEntry, WorkingCalendarId calendarId);
        void DeleteWorkingCalendarEntry(WorkingCalendarEntry oldEntry);
        void SaveWorkingCalendar(WorkingCalendarDiff calendar);
    }

    public interface IWorkingCalendarUseCase
    {
        WorkingCalendar GetWorkingCalendar();
        List<WorkingCalendarEntry> GetWorkingCalendarEntriesForWorkingCalendarId(WorkingCalendarId id);
        void InsertWorkingCalendarEntry(WorkingCalendarEntry newEntry, WorkingCalendarId calendarId);
        void DeleteWorkingCalendarEntry(WorkingCalendarEntry oldEntry);
        void SaveWorkingCalendar(WorkingCalendarDiff calendar);
    }

    public class WorkingCalendarUseCase : IWorkingCalendarUseCase
    {
        private IWorkingCalendarData _data;

        public WorkingCalendarUseCase(IWorkingCalendarData data)
        {
            _data = data;
        }


        public WorkingCalendar GetWorkingCalendar()
        {
            return _data.GetWorkingCalendar();
        }

        public List<WorkingCalendarEntry> GetWorkingCalendarEntriesForWorkingCalendarId(WorkingCalendarId id)
        {
            return _data.GetWorkingCalendarEntriesForWorkingCalendarId(id);
        }

        public void InsertWorkingCalendarEntry(WorkingCalendarEntry newEntry, WorkingCalendarId calendarId)
        {
            _data.InsertWorkingCalendarEntry(newEntry, calendarId);
            _data.Commit();
        }

        public void DeleteWorkingCalendarEntry(WorkingCalendarEntry oldEntry)
        {
            _data.DeleteWorkingCalendarEntry(oldEntry);
            _data.Commit();
        }

        public void SaveWorkingCalendar(WorkingCalendarDiff calendar)
        {
            _data.SaveWorkingCalendar(calendar);
            _data.Commit();
        }
    }
}
