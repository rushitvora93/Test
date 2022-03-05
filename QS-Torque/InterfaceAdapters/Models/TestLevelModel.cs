using Core.Entities;

namespace InterfaceAdapters.Models
{
    public class TestLevelModel : BindableBase
    {
        public TestLevel Entity { get; private set; }

        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new TestLevelId(value);
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

        public int SampleNumber
        {
            get => Entity.SampleNumber;
            set
            {
                Entity.SampleNumber = value;
                RaisePropertyChanged();
            }
        }

        public bool ConsiderWorkingCalendar
        {
            get => Entity.ConsiderWorkingCalendar;
            set
            {
                Entity.ConsiderWorkingCalendar = value;
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


        public TestLevelModel(TestLevel entity)
        {
            Entity = entity ?? new TestLevel();
            RaisePropertyChanged(null);
        }

        public static TestLevelModel GetModelFor(TestLevel entity)
        {
            return entity != null ? new TestLevelModel(entity) : null;
        }
    }
}
