using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Threading;
using Core.Entities;
using InterfaceAdapters.Models;
using NUnit.Framework;

namespace InterfaceAdapters.Test
{
    public class WorkingCalendarInterfaceAdapterTest
    {
        [Test]
        public void ShowCalendarEntriesUpdatesWorkingCalendarEntries()
        {
            var adapter = new WorkingCalendarInterfaceAdapter();
            adapter.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var entries = new List<WorkingCalendarEntry>()
            {
                new WorkingCalendarEntry(),
                new WorkingCalendarEntry()
            };
            adapter.ShowCalendarEntries(entries);

            Assert.AreEqual(2, adapter.WorkingCalendarEntries.Count);
            Assert.AreSame(entries[0], adapter.WorkingCalendarEntries[0].Entity);
            Assert.AreSame(entries[1], adapter.WorkingCalendarEntries[1].Entity);
        }

        [Test]
        public void AddCalendarEntryUpdatesWorkingCalendarEntries()
        {
            var adapter = new WorkingCalendarInterfaceAdapter();
            adapter.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var entry = new WorkingCalendarEntry();
            adapter.AddWorkingCalendarEntry(entry);

            Assert.AreSame(entry, adapter.WorkingCalendarEntries.Last().Entity);
        }

        [Test]
        public void RemoveCalendarEntryUpdatesWorkingCalendarEntries()
        {
            var adapter = new WorkingCalendarInterfaceAdapter();
            adapter.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var entry = new WorkingCalendarEntry();
            var model = new WorkingCalendarEntryModel(entry);
            adapter.WorkingCalendarEntries.Add(model);
            adapter.RemoveWorkingCalendarEntry(entry);

            Assert.IsFalse(adapter.WorkingCalendarEntries.Contains(model));
        }
        
        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void LoadWeekendSettingsSetsPropertiesToCorrectValue(bool saturdaysFree, bool sundaysFree)
        {
            var adapter = new WorkingCalendarInterfaceAdapter();
            adapter.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            adapter.LoadWeekendSettings(new WorkingCalendar() { AreSaturdaysFree = saturdaysFree, AreSundaysFree = sundaysFree });
            Assert.AreEqual(saturdaysFree, adapter.AreSaturdaysFree);
            Assert.AreEqual(sundaysFree, adapter.AreSundaysFree);
            Assert.AreEqual(saturdaysFree, adapter.WorkingCalendarWithoutChanges.AreSaturdaysFree);
            Assert.AreEqual(sundaysFree, adapter.WorkingCalendarWithoutChanges.AreSundaysFree);
        }

        [TestCase("2020-03-06 00:00:00")]
        [TestCase("1958-11-22 00:00:00")]
        public void SelectedDateInCalendarSetSelectedEntryInListToExistingEntryForDate(DateTime date)
        {
            var adapter = new WorkingCalendarInterfaceAdapter();
            var entry = new WorkingCalendarEntry()
            {
                Date = date
            };
            adapter.WorkingCalendarEntries.Add(new WorkingCalendarEntryModel(entry));
            adapter.SelectedDateInCalendar = date;
            Assert.AreEqual(entry, adapter.SelectedEntryInList.Entity);
        }

        [Test]
        public void AddWorkingCalendarEntrySelectsAddedEntry()
        {
            var adapter = new WorkingCalendarInterfaceAdapter();
            adapter.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var entry = new WorkingCalendarEntry();
            adapter.AddWorkingCalendarEntry(entry);
            Assert.AreSame(entry, adapter.SelectedEntryInList.Entity);
        }

        [TestCase("2021-03-14")]
        [TestCase("2021-03-21")]
        public void SelectedEntryInListSetsSelectedDateInCalendar(DateTime date)
        {
            var adapter = new WorkingCalendarInterfaceAdapter();
            var entry = new WorkingCalendarEntry() { Date = date };
            var model = new WorkingCalendarEntryModel(entry);
            adapter.WorkingCalendarEntries.Add(model);
            adapter.SelectedEntryInList = model;
            Assert.AreEqual(date, adapter.SelectedDateInCalendar);
        }
    }
}
