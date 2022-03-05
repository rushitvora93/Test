using Client.Core.Entities;
using Client.TestHelper.Factories;
using Core.Entities;
using InterfaceAdapters.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace InterfaceAdapters.Test
{
    public class ProcessControlInterfaceAdapterTest
    {
        [Test]
        public void ShowProcessControlConditionForLocationSetsSelectedProcessControl()
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            var processControl = CreateProcessControlCondition.Randomized(2354);
            adapter.ShowProcessControlConditionForLocation(processControl);

            Assert.AreSame(processControl, adapter.SelectedProcessControl.Entity);
        }

        [Test]
        public void SelectProcessControlConditionSetsProcessControlConditionWithoutChanges()
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            var processControl = ProcessControlConditionHumbleModel.GetModelFor(CreateProcessControlCondition.Randomized(2354), new NullLocalizationWrapper());
            adapter.SelectedProcessControl = processControl;

            Assert.IsTrue(adapter.SelectedProcessControl.EqualsByContent(adapter.SelectedProcessControlWithoutChanges));
        }

        [Test]
        public void AddProcessControlConditionSetsSelectedProcessControl()
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            var processControl = CreateProcessControlCondition.Randomized(2354);
            adapter.AddProcessControlCondition(processControl);

            Assert.AreSame(processControl, adapter.SelectedProcessControl.Entity);
        }

        [Test]
        public void UpdateProcessControlConditionInvokesShowLoadingControlRequest()
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            var invoked = false;
            adapter.ShowLoadingControlRequest += (o, e) => invoked = true;
            adapter.UpdateProcessControlCondition(new List<ProcessControlCondition>() { CreateProcessControlCondition.Randomized(32553) });
            Assert.IsTrue(invoked);
        }

        [Test]
        public void UpdateProcessControlConditionDontUpdateSelectedProcessControlCondition()
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(CreateProcessControlCondition.Randomized(32454), null);

            var updatedProcessControlCondition = CreateProcessControlCondition.Randomized(32553);
            adapter.UpdateProcessControlCondition(new List<ProcessControlCondition>() { updatedProcessControlCondition });

            Assert.IsFalse(adapter.SelectedProcessControl.Entity.EqualsByContent(updatedProcessControlCondition));
            Assert.IsFalse(adapter.SelectedProcessControlWithoutChanges.Entity.EqualsByContent(updatedProcessControlCondition));
        }

        [Test]
        public void UpdateProcessControlConditionUpdateSelectedProcessControlCondition()
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            adapter.SelectedProcessControl = ProcessControlConditionHumbleModel.GetModelFor(CreateProcessControlCondition.Randomized(32454), null);

            var updatedProcessControlCondition = CreateProcessControlCondition.RandomizedWithId(32553, adapter.SelectedProcessControl.Id);
            adapter.UpdateProcessControlCondition(new List<ProcessControlCondition>() { updatedProcessControlCondition });

            Assert.IsTrue(adapter.SelectedProcessControl.Entity.EqualsByContent(updatedProcessControlCondition));
            Assert.IsTrue(adapter.SelectedProcessControlWithoutChanges.Entity.EqualsByContent(updatedProcessControlCondition));
        }

        [Test]
        public void RemoveProcessControlConditionSetsSelectedProcessControlCondition()
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            var oldProcessControl = ProcessControlConditionHumbleModel.GetModelFor(CreateProcessControlCondition.RandomizedWithId(32454, 1), null);
            adapter.SelectedProcessControl = oldProcessControl;

            var deletedProcessControlCondition = CreateProcessControlCondition.RandomizedWithId(1232, 2);
            adapter.RemoveProcessControlCondition(deletedProcessControlCondition);

            Assert.AreSame(oldProcessControl, adapter.SelectedProcessControl);

            adapter.RemoveProcessControlCondition(oldProcessControl.Entity);
            Assert.IsNull(adapter.SelectedProcessControl);
        }

        [Test]
        public void RemoveProcessControlConditionInvokesShowLoadingControlRequest()
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            var invoked = false;
            adapter.ShowLoadingControlRequest += (o, e) => invoked = true;
            adapter.RemoveProcessControlCondition(CreateProcessControlCondition.Randomized(32553));
            Assert.IsTrue(invoked);
        }

        [Test]
        public void ShowProcessControlConditionsFillsProcessControlConditions()
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            var list = new List<ProcessControlCondition>() { new ProcessControlCondition(), new ProcessControlCondition() };
            adapter.ShowProcessControlConditions(list);
            Assert.AreSame(list[0], adapter.ProcessControlConditions[0].Entity);
            Assert.AreSame(list[1], adapter.ProcessControlConditions[1].Entity);
        }

        [Test]
        public void AssignTestLevelSetToProcessControlConditionsFillsTestLevelSetsCorrect()
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            adapter.ProcessControlConditions = new System.Collections.ObjectModel.ObservableCollection<ProcessControlConditionHumbleModel>(new List<ProcessControlConditionHumbleModel>()
            {
                new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = new ProcessControlConditionId(1), TestLevelNumber = 2 }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = new ProcessControlConditionId(2), TestLevelNumber = 2 }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = new ProcessControlConditionId(3), TestLevelNumber = 3 }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = new ProcessControlConditionId(4), TestLevelNumber = 3 }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = new ProcessControlConditionId(5), TestLevelNumber = 3 }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = new ProcessControlConditionId(6), TestLevelNumber = 3 }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = new ProcessControlConditionId(7), TestLevelNumber = 2 }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = new ProcessControlConditionId(8), TestLevelNumber = 2 }, new NullLocalizationWrapper()),
                new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = new ProcessControlConditionId(9), TestLevelNumber = 2 }, new NullLocalizationWrapper())
            });
            var testLevelSet = new TestLevelSet();
            adapter.AssignTestLevelSetToProcessControlConditions(testLevelSet, new List<ProcessControlConditionId>()
            {
                new ProcessControlConditionId(2),
                new ProcessControlConditionId(6),
                new ProcessControlConditionId(9),
                new ProcessControlConditionId(4),
                new ProcessControlConditionId(3),
                new ProcessControlConditionId(8),
            });

            Assert.AreSame(testLevelSet, adapter.ProcessControlConditions[1].TestLevelSet.Entity);
            Assert.AreSame(testLevelSet, adapter.ProcessControlConditions[5].TestLevelSet.Entity);
            Assert.AreSame(testLevelSet, adapter.ProcessControlConditions[8].TestLevelSet.Entity);
            Assert.AreSame(testLevelSet, adapter.ProcessControlConditions[3].TestLevelSet.Entity);
            Assert.AreSame(testLevelSet, adapter.ProcessControlConditions[2].TestLevelSet.Entity);
            Assert.AreSame(testLevelSet, adapter.ProcessControlConditions[7].TestLevelSet.Entity);
            Assert.AreEqual(1, adapter.ProcessControlConditions[1].TestLevelNumber);
            Assert.AreEqual(1, adapter.ProcessControlConditions[5].TestLevelNumber);
            Assert.AreEqual(1, adapter.ProcessControlConditions[8].TestLevelNumber);
            Assert.AreEqual(1, adapter.ProcessControlConditions[3].TestLevelNumber);
            Assert.AreEqual(1, adapter.ProcessControlConditions[2].TestLevelNumber);
            Assert.AreEqual(1, adapter.ProcessControlConditions[7].TestLevelNumber);
        }

        [TestCase(987)]
        [TestCase(45)]
        public void RemoveTestLevelSetAssignmentForSetsAssignmentMfuAttributesToNull(long id)
        {
            var adapter = new ProcessControlInterfaceAdapter(new NullLocalizationWrapper());
            var entity = new ProcessControlCondition()
            {
                Id = new ProcessControlConditionId(id),
                TestLevelSet = new TestLevelSet()
            };

            adapter.ProcessControlConditions = new System.Collections.ObjectModel.ObservableCollection<ProcessControlConditionHumbleModel>();
            adapter.ProcessControlConditions.Add(new ProcessControlConditionHumbleModel(new ProcessControlCondition() { Id = new ProcessControlConditionId(0) }, new NullLocalizationWrapper()));
            adapter.ProcessControlConditions.Add(new ProcessControlConditionHumbleModel(entity, new NullLocalizationWrapper()));
            adapter.RemoveTestLevelSetAssignmentFor(new List<ProcessControlConditionId>() { new ProcessControlConditionId(id) });

            Assert.IsNull(entity.TestLevelSet);
        }
    }
}
