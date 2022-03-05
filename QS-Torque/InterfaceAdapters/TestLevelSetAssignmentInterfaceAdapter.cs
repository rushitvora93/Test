using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Client.Core.Entities;
using Client.UseCases.UseCases;
using Core.Entities;
using Core.Enums;
using InterfaceAdapters.Localization;
using InterfaceAdapters.Models;

namespace InterfaceAdapters
{
    public interface ITestLevelSetAssignmentInterface : INotifyPropertyChanged
    {
        ObservableCollection<LocationToolAssignmentModelWithTestType> LocationToolAssignments { get; }
        ObservableCollection<LocationToolAssignmentModelWithTestType> SelectedLocationToolAssignments { get; set; }
        TestLevelSetModel SelectedTestLevelSet { get; set; }
    }

    public class TestLevelSetAssignmentInterfaceAdapter : BindableBase, ITestLevelSetAssignmentInterface, ITestLevelSetAssignmentGui
    {
        private ILocalizationWrapper _localization;

        private ObservableCollection<LocationToolAssignmentModelWithTestType> _locationToolAssignments = new ObservableCollection<LocationToolAssignmentModelWithTestType>();
        public ObservableCollection<LocationToolAssignmentModelWithTestType> LocationToolAssignments
        {
            get => _locationToolAssignments;
            private set
            {
                _locationToolAssignments = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<LocationToolAssignmentModelWithTestType> _selectedLocationToolAssignments = new ObservableCollection<LocationToolAssignmentModelWithTestType>();
        public ObservableCollection<LocationToolAssignmentModelWithTestType> SelectedLocationToolAssignments
        {
            get => _selectedLocationToolAssignments;
            set
            {
                _selectedLocationToolAssignments = value;
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
                RaisePropertyChanged();
            }
        }
        

        public TestLevelSetAssignmentInterfaceAdapter(ILocalizationWrapper localization)
        {
            _localization = localization;
        }

        public void LoadLocationToolAssignments(List<LocationToolAssignment> assignments)
        {
            LocationToolAssignments = new ObservableCollection<LocationToolAssignmentModelWithTestType>();
            foreach (var assignment in assignments)
            {
                LocationToolAssignments.Add(LocationToolAssignmentModelWithTestType.GetModelFor(assignment, TestType.Mfu, _localization));
                LocationToolAssignments.Add(LocationToolAssignmentModelWithTestType.GetModelFor(assignment, TestType.Chk, _localization));
            }
        }

        public void RemoveTestLevelSetAssignmentFor(List<(LocationToolAssignmentId, TestType)> ids)
        {
            foreach (var id in ids)
            {
                var toRemove = LocationToolAssignments
                    .Where(x => x.Entity.Id.Equals(id.Item1) && x.TestType == id.Item2)
                    .ToList();
                foreach (var item in toRemove)
                {
                    if(id.Item2 == TestType.Mfu)
                    {
                        item.TestLevelSetMfu = null;
                    } 
                    else if (id.Item2 == TestType.Chk)
                    {
                        item.TestLevelSetChk = null;
                    }
                }
            }
        }

        public void AssignTestLevelSetToLocationToolAssignments(TestLevelSet testLevelSet, List<(LocationToolAssignmentId, TestType)> locationToolAssignmentIds)
        {
            foreach (var id in locationToolAssignmentIds)
            {
                var toUpdate = LocationToolAssignments
                    .Where(x => x.Entity.Id.Equals(id.Item1) && x.TestType == id.Item2)
                    .ToList();
                foreach (var item in toUpdate)
                {
                    if (id.Item2 == TestType.Mfu)
                    {
                        item.TestLevelSetMfu = TestLevelSetModel.GetModelFor(testLevelSet);
                        item.TestLevelNumberMfu = 1;
                    }
                    else if (id.Item2 == TestType.Chk)
                    {
                        item.TestLevelSetChk = TestLevelSetModel.GetModelFor(testLevelSet);
                        item.TestLevelNumberChk = 1;
                    }
                }
            }
        }
    }
}
