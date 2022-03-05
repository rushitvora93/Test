using System;
using System.ComponentModel;
using System.Linq;
using Core;
using Core.Entities;
using InterfaceAdapters.Localization;

namespace InterfaceAdapters.Models
{
    public class TestPlanModel : BindableBase, IUpdate<TestPlan>, IDataErrorInfo
    {
        private ILocalizationWrapper _localization;

        public TestPlan Entity { get; private set; }


        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new TestPlanId(value);
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get => Entity.Name.ToDefaultString();
            set
            {
                Entity.Name = new TestPlanName(value);
                RaisePropertyChanged();
            }
        }

        public IntervalModel TestInterval
        {
            get => IntervalModel.GetModelFor(Entity.TestInterval);
            set
            {
                Entity.TestInterval = value.Entity;
                RaisePropertyChanged();
            }
        }

        public long SampleNumber
        {
            get => Entity.SampleNumber;
            set
            {
                Entity.SampleNumber = value;
                RaisePropertyChanged();
            }
        }

        public TestPlanBehavior Behavior
        {
            get => Entity.Behavior;
            set
            {
                Entity.Behavior = value;
                RaisePropertyChanged();
            }
        }

        public bool ConsiderHolidays
        {
            get => Entity.ConsiderHolidays;
            set
            {
                Entity.ConsiderHolidays = value;
                RaisePropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => Entity.StartDate;
            set
            {
                Entity.StartDate = value;
                RaisePropertyChanged();
            }
        }

        public bool IsEndDateEnabled
        {
            get => Entity.IsEndDateEnabled;
            set
            {
                Entity.IsEndDateEnabled = value;
                RaisePropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get => Entity.EndDate;
            set
            {
                Entity.EndDate = value;
                RaisePropertyChanged();
            }
        }


        public TestPlanModel(TestPlan entity, ILocalizationWrapper localization)
        {
            Entity = entity ?? new TestPlan();
            _localization = localization;
            RaisePropertyChanged(null);
        }

        public static TestPlanModel GetModelFor(TestPlan entity, ILocalizationWrapper localization)
        {
            return entity != null ? new TestPlanModel(entity, localization) : null;
        }

        public void UpdateWith(TestPlan other)
        {
            Entity.UpdateWith(other);
            RaisePropertyChanged(null);
        }

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                var errors = Entity.Validate(columnName);
                if (errors == null || errors.ToList().Count <= 0)
                {
                    return null;
                }
                string result = "";

                foreach (var error in errors)
                {
                    switch (error)
                    {
                        case TestPlanValidationError.IntervalIsLessThanOneShift:
                            result += _localization.Strings.GetParticularString("TestPlanValidationError", "The interval has to be greater than or equal to one shift") + "\n";
                            break;
                        case TestPlanValidationError.SampleNumberIsLessThanOne:
                            result += _localization.Strings.GetParticularString("TestPlanValidationError", "The sample number has to be greater than zero") + "\n";
                            break;
                        case TestPlanValidationError.EndDateIsNotGreaterThanStartDate:
                            result += _localization.Strings.GetParticularString("TestPlanValidationError", "The end date has to be greater than the start date") + "\n";
                            break;
                    }
                }

                return result.TrimEnd();
            }
        }
    }
}
