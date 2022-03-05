using System;
using Core;
using Core.Entities;

namespace InterfaceAdapters.Models
{
    public class TestLevelSetModel : BindableBase, IUpdate<TestLevelSet>, IEquatable<TestLevelSetModel>
    {
        public TestLevelSet Entity { get; private set; }

        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new TestLevelSetId(value);
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get => Entity.Name.ToDefaultString();
            set
            {
                Entity.Name = new TestLevelSetName(value);
                RaisePropertyChanged();
            }
        }

        public IntervalType IntervalType1
        {
            get => Entity.TestLevel1?.TestInterval?.Type ?? IntervalType.EveryXShifts;
            set
            {
                Entity.TestLevel1.TestInterval.Type = value;
                RaisePropertyChanged();
            }
        }

        public long IntervalValue1
        {
            get => Entity.TestLevel1?.TestInterval?.IntervalValue ?? -1;
            set
            {
                Entity.TestLevel1.TestInterval.IntervalValue = value;
                RaisePropertyChanged();
            }
        }

        public int SampleNumber1
        {
            get => Entity.TestLevel1?.SampleNumber ?? -1;
            set
            {
                Entity.TestLevel1.SampleNumber = value;
                RaisePropertyChanged();
            }
        }

        public bool ConsiderWorkingCalendar1
        {
            get => Entity.TestLevel1?.ConsiderWorkingCalendar ?? false;
            set
            {
                Entity.TestLevel1.ConsiderWorkingCalendar = value;
                RaisePropertyChanged();
            }
        }

        public IntervalType IntervalType2
        {
            get => Entity.TestLevel2?.TestInterval?.Type ?? IntervalType.EveryXShifts;
            set
            {
                Entity.TestLevel2.TestInterval.Type = value;
                RaisePropertyChanged();
            }
        }

        public long IntervalValue2
        {
            get => Entity.TestLevel2?.TestInterval?.IntervalValue ?? -1;
            set
            {
                Entity.TestLevel2.TestInterval.IntervalValue = value;
                RaisePropertyChanged();
            }
        }

        public int SampleNumber2
        {
            get => Entity.TestLevel2?.SampleNumber ?? -1;
            set
            {
                Entity.TestLevel2.SampleNumber = value;
                RaisePropertyChanged();
            }
        }

        public bool ConsiderWorkingCalendar2
        {
            get => Entity.TestLevel2?.ConsiderWorkingCalendar ?? false;
            set
            {
                Entity.TestLevel2.ConsiderWorkingCalendar = value;
                RaisePropertyChanged();
            }
        }

        public bool IsActive2
        {
            get => Entity.TestLevel2?.IsActive ?? false;
            set
            {
                Entity.TestLevel2.IsActive = value;
                RaisePropertyChanged();
            }
        }

        public IntervalType IntervalType3
        {
            get => Entity.TestLevel3?.TestInterval?.Type ?? IntervalType.EveryXShifts;
            set
            {
                Entity.TestLevel3.TestInterval.Type = value;
                RaisePropertyChanged();
            }
        }

        public long IntervalValue3
        {
            get => Entity.TestLevel3?.TestInterval?.IntervalValue ?? -1;
            set
            {
                Entity.TestLevel3.TestInterval.IntervalValue = value;
                RaisePropertyChanged();
            }
        }

        public int SampleNumber3
        {
            get => Entity.TestLevel3?.SampleNumber ?? -1;
            set
            {
                Entity.TestLevel3.SampleNumber = value;
                RaisePropertyChanged();
            }
        }

        public bool ConsiderWorkingCalendar3
        {
            get => Entity.TestLevel3?.ConsiderWorkingCalendar ?? false;
            set
            {
                Entity.TestLevel3.ConsiderWorkingCalendar = value;
                RaisePropertyChanged();
            }
        }

        public bool IsActive3
        {
            get => Entity.TestLevel3?.IsActive ?? false;
            set
            {
                Entity.TestLevel3.IsActive = value;
                RaisePropertyChanged();
            }
        }


        public TestLevelSetModel(TestLevelSet entity)
        {
            Entity = entity ?? new TestLevelSet();
            RaisePropertyChanged(null);
        }

        public static TestLevelSetModel GetModelFor(TestLevelSet entity)
        {
            return entity != null ? new TestLevelSetModel(entity) : null;
        }

        public void UpdateWith(TestLevelSet other)
        {
            Entity.UpdateWith(other);
            RaisePropertyChanged(null);
        }



        // Necessary for showing TestLevelSets in ComboBoxes
        public bool Equals(TestLevelSetModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Entity.EqualsById(other?.Entity);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TestLevelSetModel)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
