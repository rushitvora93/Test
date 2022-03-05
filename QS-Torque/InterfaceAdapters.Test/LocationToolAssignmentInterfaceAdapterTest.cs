using System.Collections.Generic;
using Core.Entities;
using NUnit.Framework;

namespace InterfaceAdapters.Test
{
    public class LocationToolAssignmentInterfaceAdapterTest
    {
        [Test]
        public void LoadLocationToolAssignmentsRefillsLocationToolAssignmentDoubled()
        {
            var adapter = new LocationToolAssignmentInterfaceAdapter(new NullLocalizationWrapper());
            var previousList = adapter.LocationToolAssignments;
            var entity1 = new LocationToolAssignment();
            var entity2 = new LocationToolAssignment();
            adapter.LoadLocationToolAssignments(new List<LocationToolAssignment>() { entity1, entity2 });

            Assert.AreNotSame(previousList, adapter.LocationToolAssignments);
            Assert.AreEqual(2, adapter.LocationToolAssignments.Count);
            Assert.AreSame(entity1, adapter.LocationToolAssignments[0].Entity);
            Assert.AreSame(entity2, adapter.LocationToolAssignments[1].Entity);
        }

        [Test]
        public void ShowLocationToolAssignmentErrorRaisesLocationToolAssignmentErrorRequest()
        {
            var adapter = new LocationToolAssignmentInterfaceAdapter(new NullLocalizationWrapper());
            bool errorRequestInvoked = false;
            adapter.LocationToolAssignmentErrorRequest += (s, e) => errorRequestInvoked = true;
            adapter.ShowLocationToolAssignmentError();
            Assert.IsTrue(errorRequestInvoked);
        }
    }
}
