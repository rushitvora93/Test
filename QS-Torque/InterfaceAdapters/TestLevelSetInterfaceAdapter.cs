using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;
using Client.Core.Diffs;
using Core.Entities;
using Core.UseCases;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace InterfaceAdapters
{
    public interface ITestLevelSetInterface : INotifyPropertyChanged
    {
        ObservableCollection<TestLevelSetModel> TestLevelSets { get; }
        TestLevelSetModel SelectedTestLevelSet { get; set; }
        TestLevelSetModel TestLevelSetWithoutChanges { get; }

        event EventHandler<bool> ShowLoadingControlRequest;

        void SetGuiDispatcher(Dispatcher guiDispatcher);
    }

    public class TestLevelSetInterfaceAdapter : BindableBase, ITestLevelSetInterface, ITestLevelSetGui
    {
        private Dispatcher _guiDispatcher;
        private ILocalizationWrapper _localization;

        private ObservableCollection<TestLevelSetModel> _testLevelSets = new ObservableCollection<TestLevelSetModel>();
        public ObservableCollection<TestLevelSetModel> TestLevelSets
        {
            get => _testLevelSets;
            private set
            {
                _testLevelSets = value;
                RaisePropertyChanged();
            }
        }

        private TestLevelSetModel _selectedTestLevelSet;
        public TestLevelSetModel SelectedTestLevelSet
        {
            get => _selectedTestLevelSet;
            set
            {
                _selectedTestLevelSet = value; 
                TestLevelSetWithoutChanges = value != null ? TestLevelSetModel.GetModelFor(_selectedTestLevelSet.Entity.CopyDeep()) : null;
                RaisePropertyChanged();
            }
        }

        private TestLevelSetModel _testLevelSetWithoutChanges;
        public TestLevelSetModel TestLevelSetWithoutChanges
        {
            get => _testLevelSetWithoutChanges;
            private set
            {
                _testLevelSetWithoutChanges = value;
                RaisePropertyChanged();
            }
        }

        public event EventHandler<bool> ShowLoadingControlRequest;


        public TestLevelSetInterfaceAdapter(ILocalizationWrapper localization)
        {
            _localization = localization;
        }


        public void SetGuiDispatcher(Dispatcher guiDispatcher)
        {
            _guiDispatcher = guiDispatcher;
        }

        public void LoadTestLevelSets(List<TestLevelSet> testLevelSets)
        {
            TestLevelSets = new ObservableCollection<TestLevelSetModel>(testLevelSets.Select(x => TestLevelSetModel.GetModelFor(x)));
            ShowLoadingControlRequest?.Invoke(this, false);
        }

        public void AddTestLevelSet(TestLevelSet newItem)
        {
            _guiDispatcher.Invoke(() =>
            {
                var model = TestLevelSetModel.GetModelFor(newItem);
                TestLevelSets.Add(model);
                SelectedTestLevelSet = model;
            });
        }

        public void RemoveTestLevelSet(TestLevelSet oldItem)
        {
            if(SelectedTestLevelSet.Entity.EqualsById(oldItem))
            {
                _guiDispatcher.Invoke(() =>
                {
                    SelectedTestLevelSet = null;
                    TestLevelSetWithoutChanges = null;
                });
            }

            var toRemove = TestLevelSets.Where(x => x.Entity.EqualsById(oldItem)).ToList();
            foreach (var item in toRemove)
            {
                _guiDispatcher.Invoke(() => TestLevelSets.Remove(item));
            }
        }

        public void UpdateTestLevelSet(TestLevelSetDiff diff)
        {
            foreach (var item in TestLevelSets)
            {
                if (item.Entity.EqualsById(diff.New))
                {
                    item.UpdateWith(diff.New);
                }
            }

            if (SelectedTestLevelSet?.Entity.EqualsById(diff.New) ?? false)
            {
                TestLevelSetWithoutChanges.UpdateWith(diff.New);
            }
        }
    }
}
