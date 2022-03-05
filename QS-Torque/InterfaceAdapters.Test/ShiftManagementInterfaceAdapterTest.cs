using System;
using Client.Core.Diffs;
using Core.Entities;
using NUnit.Framework;

namespace InterfaceAdapters.Test
{
    public class ShiftManagementInterfaceAdapterTest
    {
        [TestCase(654321)]
        [TestCase(741852)]
        public void LoadShiftManagementSetsPropertiesToCorrectValue(long ticks)
        {
            var adapter = new ShiftManagementInterfaceAdapter(new NullLocalizationWrapper());
            var shiftManagement = new ShiftManagement() { FirstShiftStart = TimeSpan.FromTicks(ticks) };
            adapter.LoadShiftManagement(shiftManagement);
            Assert.AreSame(shiftManagement, adapter.CurrentShiftManagement.Entity);
            Assert.AreNotSame(shiftManagement, adapter.ShiftManagementWithoutChanges);
            Assert.AreEqual(TimeSpan.FromTicks(ticks), adapter.ShiftManagementWithoutChanges.FirstShiftStart);
        }

        [TestCase(654321)]
        [TestCase(741852)]
        public void SaveShiftManagementWithHistorySetsPropertiesToCorrectValue(long ticks)
        {
            var adapter = new ShiftManagementInterfaceAdapter(new NullLocalizationWrapper());
            var shiftManagement = new ShiftManagement() { FirstShiftStart = TimeSpan.FromTicks(ticks) };
            adapter.SaveShiftManagement(new ShiftManagementDiff(null, shiftManagement));
            Assert.AreSame(shiftManagement, adapter.CurrentShiftManagement.Entity);
            Assert.AreNotSame(shiftManagement, adapter.ShiftManagementWithoutChanges);
            Assert.AreEqual(TimeSpan.FromTicks(ticks), adapter.ShiftManagementWithoutChanges.FirstShiftStart);
        }
    }
}
