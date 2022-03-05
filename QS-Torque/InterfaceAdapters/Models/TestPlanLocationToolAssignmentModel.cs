using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Enums;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class TestPlanLocationToolAssignmentModel : BindableBase
    {
        public TestPlanLocationToolAssignment Entity { get; private set; }
        private ILocalizationWrapper _localization;


        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new TestPlanLocationToolAssignmentId(value);
                RaisePropertyChanged();
            }
        }

        public LocationToolAssignmentModel LocationToolAssignment
        {
            get => LocationToolAssignmentModel.GetModelFor(Entity.LocationToolAssignment, _localization);
            set
            {
                Entity.LocationToolAssignment = value.Entity;
                RaisePropertyChanged();
            }
        }

        public List<TestPlanModel> TestPlans
        {
            get => Entity.TestPlans.Select(x => TestPlanModel.GetModelFor(x, _localization)).ToList();
            set
            {
                Entity.TestPlans = value.Select(x => x.Entity).ToList();
                RaisePropertyChanged();
            }
        }

        public bool IsActive
        {
            get => Entity.IsActive;
            set
            {
                Entity.IsActive = value;
                RaisePropertyChanged();
            }
        }

        public TestType TestType
        {
            get => Entity.TestType;
            set
            {
                Entity.TestType = value;
                RaisePropertyChanged();
            }
        }

        public DateTime TestPeriodStartDate
        {
            get => Entity.TestPeriodStartDate;
            set
            {
                Entity.TestPeriodStartDate = value;
                RaisePropertyChanged();
            }
        }

        public Shift TestPeriodStartShift
        {
            get => Entity.TestPeriodStartShift;
            set
            {
                Entity.TestPeriodStartShift = value;
                RaisePropertyChanged();
            }
        }


        public TestPlanLocationToolAssignmentModel(TestPlanLocationToolAssignment entity, ILocalizationWrapper localization)
        {
            Entity = entity ?? new TestPlanLocationToolAssignment();
            _localization = localization;
            RaisePropertyChanged(null);
        }

        public static TestPlanLocationToolAssignmentModel GetModelFor(TestPlanLocationToolAssignment entity, ILocalizationWrapper localization)
        {
            return entity != null ? new TestPlanLocationToolAssignmentModel(entity, localization) : null;
        }
    }
}
