using System.Collections.Generic;
using Core.Entities;
using Core.Enums;
using InterfaceAdapters.Models;
using NUnit.Framework;

namespace InterfaceAdapters.Test
{
    public class TestLevelSetAssignmentInterfaceAdapterTest
    {
        [Test]
        public void LoadLocationToolAssignmentsRefillsLocationToolAssignmentDoubled()
        {
            var adapter = new TestLevelSetAssignmentInterfaceAdapter(new NullLocalizationWrapper());
            var previousList = adapter.LocationToolAssignments;
            var entity1 = new LocationToolAssignment();
            var entity2 = new LocationToolAssignment();
            adapter.LoadLocationToolAssignments(new List<LocationToolAssignment>() { entity1, entity2 });

            Assert.AreNotSame(previousList, adapter.LocationToolAssignments);
            Assert.AreEqual(4, adapter.LocationToolAssignments.Count);
            Assert.AreSame(entity1, adapter.LocationToolAssignments[0].Entity);
            Assert.AreSame(entity1, adapter.LocationToolAssignments[1].Entity);
            Assert.AreSame(entity2, adapter.LocationToolAssignments[2].Entity);
            Assert.AreEqual(entity2, adapter.LocationToolAssignments[3].Entity);
            Assert.AreEqual(TestType.Mfu, adapter.LocationToolAssignments[0].TestType);
            Assert.AreEqual(TestType.Chk, adapter.LocationToolAssignments[1].TestType);
            Assert.AreEqual(TestType.Mfu, adapter.LocationToolAssignments[2].TestType);
            Assert.AreEqual(TestType.Chk, adapter.LocationToolAssignments[3].TestType);
        }

        [TestCase(987)]
        [TestCase(45)]
        public void RemoveTestLevelSetAssignmentForSetsAssignmentMfuAttributesToNull(long id)
        {
            var adapter = new TestLevelSetAssignmentInterfaceAdapter(new NullLocalizationWrapper());
            var entity = new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(id),
                TestLevelSetMfu = new TestLevelSet()
            };

            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(new LocationToolAssignment() { Id = new LocationToolAssignmentId(0) }, new NullLocalizationWrapper()));
            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(entity, new NullLocalizationWrapper())
            {
                TestType = TestType.Mfu
            });
            adapter.RemoveTestLevelSetAssignmentFor(new List<(LocationToolAssignmentId, TestType)>() { (new LocationToolAssignmentId(id), TestType.Mfu) });

            Assert.IsNull(entity.TestLevelSetMfu);
        }

        [TestCase(987)]
        [TestCase(45)]
        public void RemoveTestLevelSetAssignmentForSetsAssignmentChkAttributesToNull(long id)
        {
            var adapter = new TestLevelSetAssignmentInterfaceAdapter(new NullLocalizationWrapper());
            var entity = new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(id),
                TestLevelSetMfu = new TestLevelSet()
            };

            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(new LocationToolAssignment() { Id = new LocationToolAssignmentId(0) }, new NullLocalizationWrapper()));
            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(entity, new NullLocalizationWrapper())
            {
                TestType = TestType.Chk
            });
            adapter.RemoveTestLevelSetAssignmentFor(new List<(LocationToolAssignmentId, TestType)>() { (new LocationToolAssignmentId(id), TestType.Chk) });

            Assert.IsNull(entity.TestLevelSetChk);
        }

        [TestCase(987, 55)]
        [TestCase(45, 321)]
        public void RemoveTestLevelSetAssignmentForSetsAssignmentAttributesToNull(long id1, long id2)
        {
            var adapter = new TestLevelSetAssignmentInterfaceAdapter(new NullLocalizationWrapper());
            var entity1 = new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(id1),
                TestLevelSetMfu = new TestLevelSet()
            };
            var entity2 = new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(id2),
                TestLevelSetChk = new TestLevelSet()
            };

            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(new LocationToolAssignment() { Id = new LocationToolAssignmentId(0) }, new NullLocalizationWrapper()));
            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(entity1, new NullLocalizationWrapper())
            {
                TestType = TestType.Mfu
            });
            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(entity2, new NullLocalizationWrapper())
            {
                TestType = TestType.Chk
            });
            adapter.RemoveTestLevelSetAssignmentFor(new List<(LocationToolAssignmentId, TestType)>() { (new LocationToolAssignmentId(id1), TestType.Mfu), (new LocationToolAssignmentId(id2), TestType.Chk) });

            Assert.IsNull(entity1.TestLevelSetMfu);
            Assert.IsNull(entity2.TestLevelSetChk);
        }

        [TestCase(987)]
        [TestCase(45)]
        public void AssignTestLevelSetToLocationToolAssignmentsSetsMfuAssignmentAttributes(long id)
        {
            var adapter = new TestLevelSetAssignmentInterfaceAdapter(new NullLocalizationWrapper());
            var locTool = new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(id)
            };
            var testLevelSet = new TestLevelSet();

            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(new LocationToolAssignment() { Id = new LocationToolAssignmentId(0) }, new NullLocalizationWrapper())
            {
                TestType = TestType.Chk
            });
            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(locTool, new NullLocalizationWrapper())
            {
                TestType = TestType.Mfu
            });
            adapter.AssignTestLevelSetToLocationToolAssignments(testLevelSet, new List<(LocationToolAssignmentId, TestType)>() { (new LocationToolAssignmentId(id), TestType.Mfu) });

            Assert.AreSame(testLevelSet, locTool.TestLevelSetMfu);
        }

        [TestCase(987)]
        [TestCase(45)]
        public void AssignTestLevelSetToLocationToolAssignmentsSetsChkAssignmentAttributes(long id)
        {
            var adapter = new TestLevelSetAssignmentInterfaceAdapter(new NullLocalizationWrapper());
            var locTool = new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(id)
            };
            var testLevelSet = new TestLevelSet();

            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(new LocationToolAssignment() { Id = new LocationToolAssignmentId(0) }, new NullLocalizationWrapper())
            {
                TestType = TestType.Chk
            });
            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(locTool, new NullLocalizationWrapper())
            {
                TestType = TestType.Chk
            });
            adapter.AssignTestLevelSetToLocationToolAssignments(testLevelSet, new List<(LocationToolAssignmentId, TestType)>() { (new LocationToolAssignmentId(id), TestType.Chk) });

            Assert.AreSame(testLevelSet, locTool.TestLevelSetChk);
        }

        [TestCase(987, 55)]
        [TestCase(45, 321)]
        public void AssignTestLevelSetToLocationToolAssignmentsSetsAssignmentAttributes(long id1, long id2)
        {
            var adapter = new TestLevelSetAssignmentInterfaceAdapter(new NullLocalizationWrapper());
            var entity1 = new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(id1)
            };
            var entity2 = new LocationToolAssignment()
            {
                Id = new LocationToolAssignmentId(id2)
            };
            var testLevelSet = new TestLevelSet();

            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(new LocationToolAssignment() { Id = new LocationToolAssignmentId(0) }, new NullLocalizationWrapper()));
            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(entity1, new NullLocalizationWrapper())
            {
                TestType = TestType.Chk
            });
            adapter.LocationToolAssignments.Add(new LocationToolAssignmentModelWithTestType(entity2, new NullLocalizationWrapper())
            {
                TestType = TestType.Mfu
            });
            adapter.AssignTestLevelSetToLocationToolAssignments(testLevelSet, new List<(LocationToolAssignmentId, TestType)>()
            {
                (new LocationToolAssignmentId(id1), TestType.Chk), 
                (new LocationToolAssignmentId(id2), TestType.Mfu)
            });

            Assert.AreSame(testLevelSet, entity1.TestLevelSetChk);
            Assert.AreSame(testLevelSet, entity2.TestLevelSetMfu);
        }
    }
}
