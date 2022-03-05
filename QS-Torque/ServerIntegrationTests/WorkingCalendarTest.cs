using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Core.Diffs;
using Core.Entities;
using FrameworksAndDrivers.RemoteData.GRPC.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestHelper.Mock;

namespace ServerIntegrationTests
{
    [TestClass]
    public class WorkingCalendarTest
    {
        private readonly TestSetup _testSetup;

        public WorkingCalendarTest()
        {
            _testSetup = new TestSetup();
        }

        [TestMethod]
        public void AddWorkingCalendarEntry()
        {
            var dataAccess = new WorkingCalendarDataAccess(_testSetup.ClientFactory);
            var entry = AddNewWorkingCalendarEntryWithDataAccess(dataAccess);

            var entries = dataAccess.LoadWorkingCalendarEntriesForWorkingCalendarId(new WorkingCalendarId(1));
            var result = entries.Find(x =>  x.Description.Equals(entry.Description));
            
            Assert.AreEqual(entry.Date, result.Date);
            Assert.AreEqual(entry.Description.ToDefaultString(), result.Description.ToDefaultString());
            Assert.AreEqual(entry.Repetition, result.Repetition);
            Assert.AreEqual(entry.Type, result.Type);
        }

        [TestMethod]
        public void RemoveWorkingCalendarEntry()
        {
            var dataAccess = new WorkingCalendarDataAccess(_testSetup.ClientFactory);
            var entry = AddNewWorkingCalendarEntryWithDataAccess(dataAccess);

            var entries = dataAccess.LoadWorkingCalendarEntriesForWorkingCalendarId(new WorkingCalendarId(1));
            var result = entries.Find(x => x.Date == entry.Date);
            Assert.IsNotNull(result);

            dataAccess.RemoveWorkingCalendarEntry(entry);
            entries = dataAccess.LoadWorkingCalendarEntriesForWorkingCalendarId(new WorkingCalendarId(1));
            result = entries.Find(x => x.Description.Equals(entry.Description));
            Assert.IsNull(result);
        }

        [TestMethod]
        public void LoadWorkingCalendarEntriesForWorkingCalendarId()
        {
            var dataAccess = new WorkingCalendarDataAccess(_testSetup.ClientFactory);
            var entry = AddNewWorkingCalendarEntryWithDataAccess(dataAccess);

            var entries = dataAccess.LoadWorkingCalendarEntriesForWorkingCalendarId(new WorkingCalendarId(1));
            var result = entries.Find(x => x.Description.Equals(entry.Description));

            Assert.AreEqual(entry.Date, result.Date);
            Assert.AreEqual(entry.Description.ToDefaultString(), result.Description.ToDefaultString());
            Assert.AreEqual(entry.Repetition, result.Repetition);
            Assert.AreEqual(entry.Type, result.Type);
        }

        [TestMethod]
        public void SetWeekendSettings()
        {
            var dataAccess = new WorkingCalendarDataAccess(_testSetup.ClientFactory);
            var calendar = SaveWeekendSettingsWithDataAccess(dataAccess);
            var calendarNew = new WorkingCalendar()
            {
                Id = calendar.Id,
                AreSaturdaysFree = !calendar.AreSaturdaysFree,
                AreSundaysFree = !calendar.AreSundaysFree
            };
            var diff = new WorkingCalendarDiff(calendar, calendarNew, _testSetup.TestUser);
            dataAccess.SetWeekendSettings(diff);

            var loadedCalendar = dataAccess.LoadWeekendSettings();

            Assert.IsTrue(calendar.Id.Equals(loadedCalendar.Id));
            Assert.AreEqual(calendarNew.AreSaturdaysFree, loadedCalendar.AreSaturdaysFree);
            Assert.AreEqual(calendarNew.AreSundaysFree, loadedCalendar.AreSundaysFree);
        }

        [TestMethod]
        public void LoadWeekendSettings()
        {
            var dataAccess = new WorkingCalendarDataAccess(_testSetup.ClientFactory);
            var calendar = SaveWeekendSettingsWithDataAccess(dataAccess);

            var loadedCalendar = dataAccess.LoadWeekendSettings();

            Assert.IsTrue(calendar.Id.Equals(loadedCalendar.Id));
            Assert.AreEqual(calendar.AreSaturdaysFree, loadedCalendar.AreSaturdaysFree);
            Assert.AreEqual(calendar.AreSundaysFree, loadedCalendar.AreSundaysFree);
        }


        private WorkingCalendarEntry AddNewWorkingCalendarEntryWithDataAccess(WorkingCalendarDataAccess dataAccess)
        {
            var name = "entry_" + System.DateTime.Now.Ticks;

            var workingCalendarEntries = dataAccess.LoadWorkingCalendarEntriesForWorkingCalendarId(new WorkingCalendarId(1));
            var result = workingCalendarEntries.Find(x => x.Description.ToDefaultString() == name);
            Assert.IsNull(result);

            var entry = new WorkingCalendarEntry()
            {
                Date = new DateTime(2020, 12, 16),
                Description = new WorkingCalendarEntryDescription(name),
                Repetition = WorkingCalendarEntryRepetition.Yearly,
                Type = WorkingCalendarEntryType.Holiday
            };

            dataAccess.AddWorkingCalendarEntry(entry, new WorkingCalendarId(1));
            return entry;
        }

        private WorkingCalendar SaveWeekendSettingsWithDataAccess(WorkingCalendarDataAccess dataAccess)
        {
            var calendarOld = new WorkingCalendar()
            {
                Id = new WorkingCalendarId(1),
                AreSaturdaysFree = false,
                AreSundaysFree = true
            };
            var calendarNew = new WorkingCalendar()
            {
                Id = new WorkingCalendarId(1),
                AreSaturdaysFree = true,
                AreSundaysFree = false
            };

            dataAccess.SetWeekendSettings(new WorkingCalendarDiff(calendarOld, calendarNew, _testSetup.TestUser));
            return calendarNew;
        }
    }
}
