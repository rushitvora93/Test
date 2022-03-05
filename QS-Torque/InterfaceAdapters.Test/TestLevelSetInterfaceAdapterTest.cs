using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using Client.Core.Diffs;
using Core.Entities;
using InterfaceAdapters.Models;
using NUnit.Framework;

namespace InterfaceAdapters.Test
{
    public class TestLevelSetInterfaceAdapterTest
    {
        [Test]
        public void LoadTestLevelSetsTestLevelSets()
        {
            var adapter = new TestLevelSetInterfaceAdapter(new NullLocalizationWrapper());
            var list = new List<TestLevelSet>()
            {
                new TestLevelSet(),
                new TestLevelSet()
            };
            adapter.LoadTestLevelSets(list);
            Assert.AreEqual(2, adapter.TestLevelSets.Count);
            Assert.AreEqual(1, adapter.TestLevelSets.Count(x => x.Entity == list[0]));
            Assert.AreEqual(1, adapter.TestLevelSets.Count(x => x.Entity == list[1]));
        }

        [Test]
        public void LoadTestLevelSetsClearsPreviousTestLevelSets()
        {
            var adapter = new TestLevelSetInterfaceAdapter(new NullLocalizationWrapper());
            var model = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(1) });
            adapter.TestLevelSets.Add(model);
            adapter.LoadTestLevelSets(new List<TestLevelSet>()
            {
                new TestLevelSet() { Id = new TestLevelSetId(2) },
                new TestLevelSet() { Id = new TestLevelSetId(3) }
            });
            Assert.AreEqual(2, adapter.TestLevelSets.Count);
            Assert.IsFalse(adapter.TestLevelSets.Contains(model));
        }

        [Test]
        public void AddTestLevelSetAddsToTestLevelSets()
        {
            var adapter = new TestLevelSetInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            adapter.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet()));
            adapter.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet()));

            var testLevelSet = new TestLevelSet();
            adapter.AddTestLevelSet(testLevelSet);
            Assert.AreEqual(3, adapter.TestLevelSets.Count);
            Assert.AreEqual(1, adapter.TestLevelSets.Count(x => x.Entity == testLevelSet));
        }

        [TestCase(1)]
        [TestCase(2)]
        public void AddTestLevelSetSelectsNewItem(long id)
        {
            var adapter = new TestLevelSetInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var newItem = new TestLevelSet() { Id = new TestLevelSetId(id) };

            adapter.AddTestLevelSet(newItem);
            Assert.AreSame(newItem, adapter.SelectedTestLevelSet.Entity);
            Assert.AreEqual(id, adapter.TestLevelSetWithoutChanges.Id);
            Assert.AreNotSame(newItem, adapter.TestLevelSetWithoutChanges.Entity);
        }

        [Test]
        public void RemoveTestLevelSetRemovesTestLevelSetsById()
        {
            var adapter = new TestLevelSetInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var id = new TestLevelSetId(978654);
            adapter.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(0) });
            adapter.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(963) }));
            adapter.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet() { Id = id }));
            adapter.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(741) }));

            adapter.RemoveTestLevelSet(new TestLevelSet() { Id = id });
            Assert.AreEqual(2, adapter.TestLevelSets.Count);
            Assert.AreEqual(0, adapter.TestLevelSets.Count(x => x.Entity.Id.Equals(id)));
        }

        [TestCase(1)]
        [TestCase(2)]
        public void RemoveTestLevelSetResetsSelectedItemById(long id)
        {
            var adapter = new TestLevelSetInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SetGuiDispatcher(Dispatcher.CurrentDispatcher);
            var set = new TestLevelSet() { Id = new TestLevelSetId(id) };
            adapter.SelectedTestLevelSet = TestLevelSetModel.GetModelFor(set);

            adapter.RemoveTestLevelSet(set);
            Assert.IsNull(adapter.SelectedTestLevelSet);
            Assert.IsNull(adapter.TestLevelSetWithoutChanges);
        }

        [Test]
        public void UpdateTestLevelSetUpdatesModelsById()
        {
            var adapter = new TestLevelSetInterfaceAdapter(new NullLocalizationWrapper());
            var id = new TestLevelSetId(978654);
            adapter.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(963) }));
            adapter.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet() { Id = id }));
            adapter.TestLevelSets.Add(new TestLevelSetModel(new TestLevelSet() { Id = new TestLevelSetId(741) }));

            var newItem = new TestLevelSet()
            {
                Id = id,
                Name = new TestLevelSetName("987654321asdfasdgadfg"),
                TestLevel1 = new TestLevel()
                {
                    ConsiderWorkingCalendar = true,
                    SampleNumber = 987654,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 753951,
                        Type = IntervalType.EveryXShifts
                    }
                },
                TestLevel2 = new TestLevel()
                {
                    ConsiderWorkingCalendar = false,
                    SampleNumber = 42,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 753,
                        Type = IntervalType.XTimesADay
                    }
                },
                TestLevel3 = new TestLevel()
                {
                    ConsiderWorkingCalendar = true,
                    SampleNumber = 963,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 159,
                        Type = IntervalType.XTimesAWeek
                    }
                }
            };

            adapter.UpdateTestLevelSet(new TestLevelSetDiff() { New = newItem });
            Assert.AreEqual(3, adapter.TestLevelSets.Count);
            Assert.AreEqual(1, adapter.TestLevelSets.Count(x => x.Entity.Id.Equals(id)));
            Assert.AreEqual(newItem.Id, adapter.TestLevelSets[1].Entity.Id);
            Assert.AreEqual(newItem.Name, adapter.TestLevelSets[1].Entity.Name);
            Assert.AreEqual(newItem.TestLevel1.ConsiderWorkingCalendar, adapter.TestLevelSets[1].Entity.TestLevel1.ConsiderWorkingCalendar);
            Assert.AreEqual(newItem.TestLevel1.SampleNumber, adapter.TestLevelSets[1].Entity.TestLevel1.SampleNumber);
            Assert.IsTrue(newItem.TestLevel1.TestInterval.EqualsByContent(adapter.TestLevelSets[1].Entity.TestLevel1.TestInterval));
            Assert.AreEqual(newItem.TestLevel2.ConsiderWorkingCalendar, adapter.TestLevelSets[1].Entity.TestLevel2.ConsiderWorkingCalendar);
            Assert.AreEqual(newItem.TestLevel2.SampleNumber, adapter.TestLevelSets[1].Entity.TestLevel2.SampleNumber);
            Assert.IsTrue(newItem.TestLevel2.TestInterval.EqualsByContent(adapter.TestLevelSets[1].Entity.TestLevel2.TestInterval));
            Assert.AreEqual(newItem.TestLevel3.ConsiderWorkingCalendar, adapter.TestLevelSets[1].Entity.TestLevel3.ConsiderWorkingCalendar);
            Assert.AreEqual(newItem.TestLevel3.SampleNumber, adapter.TestLevelSets[1].Entity.TestLevel3.SampleNumber);
            Assert.IsTrue(newItem.TestLevel3.TestInterval.EqualsByContent(adapter.TestLevelSets[1].Entity.TestLevel3.TestInterval));
        }

        [Test]
        public void UpdateTestLevelSetUpdatesTestLevelSetWithoutChanges()
        {
            var adapter = new TestLevelSetInterfaceAdapter(new NullLocalizationWrapper());
            var id = new TestLevelSetId(978654);
            adapter.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet() { Id = id });

            var newItem = new TestLevelSet()
            {
                Id = id,
                Name = new TestLevelSetName("987654321asdfasdgadfg"),
                TestLevel1 = new TestLevel()
                {
                    ConsiderWorkingCalendar = true,
                    SampleNumber = 987654,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 753951,
                        Type = IntervalType.EveryXShifts
                    }
                },
                TestLevel2 = new TestLevel()
                {
                    ConsiderWorkingCalendar = false,
                    SampleNumber = 42,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 753,
                        Type = IntervalType.XTimesADay
                    }
                },
                TestLevel3 = new TestLevel()
                {
                    ConsiderWorkingCalendar = true,
                    SampleNumber = 963,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 159,
                        Type = IntervalType.XTimesAWeek
                    }
                }
            };

            adapter.UpdateTestLevelSet(new TestLevelSetDiff() { New = newItem });
            Assert.AreEqual(newItem.Name, adapter.TestLevelSetWithoutChanges.Entity.Name);
            Assert.AreEqual(newItem.TestLevel1.ConsiderWorkingCalendar, adapter.TestLevelSetWithoutChanges.Entity.TestLevel1.ConsiderWorkingCalendar);
            Assert.AreEqual(newItem.TestLevel1.SampleNumber, adapter.TestLevelSetWithoutChanges.Entity.TestLevel1.SampleNumber);
            Assert.IsTrue(newItem.TestLevel1.TestInterval.EqualsByContent(adapter.TestLevelSetWithoutChanges.Entity.TestLevel1.TestInterval));
            Assert.AreEqual(newItem.TestLevel2.ConsiderWorkingCalendar, adapter.TestLevelSetWithoutChanges.Entity.TestLevel2.ConsiderWorkingCalendar);
            Assert.AreEqual(newItem.TestLevel2.SampleNumber, adapter.TestLevelSetWithoutChanges.Entity.TestLevel2.SampleNumber);
            Assert.IsTrue(newItem.TestLevel2.TestInterval.EqualsByContent(adapter.TestLevelSetWithoutChanges.Entity.TestLevel2.TestInterval));
            Assert.AreEqual(newItem.TestLevel3.ConsiderWorkingCalendar, adapter.TestLevelSetWithoutChanges.Entity.TestLevel3.ConsiderWorkingCalendar);
            Assert.AreEqual(newItem.TestLevel3.SampleNumber, adapter.TestLevelSetWithoutChanges.Entity.TestLevel3.SampleNumber);
            Assert.IsTrue(newItem.TestLevel3.TestInterval.EqualsByContent(adapter.TestLevelSetWithoutChanges.Entity.TestLevel3.TestInterval));
        }

        [Test]
        public void ChangeSelectedTestLevelSetUpdatesTestLevelSetWithoutChanges()
        {
            var adapter = new TestLevelSetInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SelectedTestLevelSet = new TestLevelSetModel(new TestLevelSet()
            {
                Id = new TestLevelSetId(987654159),
                Name = new TestLevelSetName("987654321asdfasdgadfg"),
                TestLevel1 = new TestLevel()
                {
                    ConsiderWorkingCalendar = true,
                    SampleNumber = 987654,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 753951,
                        Type = IntervalType.EveryXShifts
                    }
                },
                TestLevel2 = new TestLevel()
                {
                    ConsiderWorkingCalendar = false,
                    SampleNumber = 42,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 753,
                        Type = IntervalType.XTimesADay
                    }
                },
                TestLevel3 = new TestLevel()
                {
                    ConsiderWorkingCalendar = true,
                    SampleNumber = 963,
                    TestInterval = new Interval()
                    {
                        IntervalValue = 159,
                        Type = IntervalType.XTimesAWeek
                    }
                }
            });

            Assert.AreNotSame(adapter.SelectedTestLevelSet, adapter.TestLevelSetWithoutChanges);
            Assert.AreNotSame(adapter.SelectedTestLevelSet.Entity, adapter.TestLevelSetWithoutChanges.Entity);
            Assert.AreEqual(adapter.SelectedTestLevelSet.Name, adapter.TestLevelSetWithoutChanges.Entity.Name.ToDefaultString());
            Assert.AreEqual(adapter.SelectedTestLevelSet.ConsiderWorkingCalendar1, adapter.TestLevelSetWithoutChanges.Entity.TestLevel1.ConsiderWorkingCalendar);
            Assert.AreEqual(adapter.SelectedTestLevelSet.SampleNumber1, adapter.TestLevelSetWithoutChanges.Entity.TestLevel1.SampleNumber);
            Assert.IsTrue(adapter.SelectedTestLevelSet.Entity.TestLevel1.TestInterval.EqualsByContent(adapter.TestLevelSetWithoutChanges.Entity.TestLevel1.TestInterval));
            Assert.AreEqual(adapter.SelectedTestLevelSet.ConsiderWorkingCalendar2, adapter.TestLevelSetWithoutChanges.Entity.TestLevel2.ConsiderWorkingCalendar);
            Assert.AreEqual(adapter.SelectedTestLevelSet.SampleNumber2, adapter.TestLevelSetWithoutChanges.Entity.TestLevel2.SampleNumber);
            Assert.IsTrue(adapter.SelectedTestLevelSet.Entity.TestLevel2.TestInterval.EqualsByContent(adapter.TestLevelSetWithoutChanges.Entity.TestLevel2.TestInterval));
            Assert.AreEqual(adapter.SelectedTestLevelSet.ConsiderWorkingCalendar3, adapter.TestLevelSetWithoutChanges.Entity.TestLevel3.ConsiderWorkingCalendar);
            Assert.AreEqual(adapter.SelectedTestLevelSet.SampleNumber3, adapter.TestLevelSetWithoutChanges.Entity.TestLevel3.SampleNumber);
            Assert.IsTrue(adapter.SelectedTestLevelSet.Entity.TestLevel3.TestInterval.EqualsByContent(adapter.TestLevelSetWithoutChanges.Entity.TestLevel3.TestInterval));
        }
    }
}
