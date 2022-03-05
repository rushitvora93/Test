using System;
using Core;
using Core.Entities;

namespace InterfaceAdapters.Models
{
    public class ToleranceClassModel : BindableBase, IEquatable<ToleranceClassModel>, IQstEquality<ToleranceClassModel>, IUpdate<ToleranceClass>, ICopy<ToleranceClassModel>
    {
        public ToleranceClass Entity { get; private set; }

        
        public long Id
        {
            get => Entity.Id.ToLong();
            set
            {
                Entity.Id = new ToleranceClassId(value);
                RaisePropertyChanged();
            }
        }
        
        public string Name
        {
            get => Entity.Name;
            set
            {
                Entity.Name = value;
                RaisePropertyChanged();
            }
        }
        

        public bool Relative
        {
            get => Entity.Relative;
            set
            {
                Entity.Relative = value;
                RaisePropertyChanged();
            }
        }

        private bool _symmetricalLimits;

        public bool SymmetricalLimits
        {
            get => _symmetricalLimits;
            set => Set(ref _symmetricalLimits, value);
        }

        private double _symmetricLimitsValue;
        public double SymmetricLimitsValue
        {
            get => _symmetricLimitsValue;
            set
            {
                if (SymmetricalLimits)
                {
                    LowerLimit = value;
                    UpperLimit = value; 
                }
                Set(ref _symmetricLimitsValue, value);
            }
        }
        

        public double LowerLimit
        {
            get => Entity.LowerLimit;
            set
            {
                Entity.LowerLimit = value;
                RaisePropertyChanged();
            }
        }
        
        public double UpperLimit
        {
            get => Entity.UpperLimit;
            set
            {
                Entity.UpperLimit = value;
                RaisePropertyChanged();
            }
        }


        public ToleranceClassModel(ToleranceClass entity)
        {
            Entity = entity ?? new ToleranceClass();
            SymmetricalLimits = entity.LowerLimit.Equals(entity.UpperLimit);
            SymmetricLimitsValue = entity.LowerLimit.Equals(entity.UpperLimit) ? entity.LowerLimit : 0;
            RaisePropertyChanged();
        }

        public static ToleranceClassModel GetModelFor(ToleranceClass entity)
        {
            return entity != null ? new ToleranceClassModel(entity) : null;
        }


        public void UpdateWith(ToleranceClass toleranceClass)
        {
            this.Entity.UpdateWith(toleranceClass);
            RaisePropertyChanged("");
        }
        
        public ToleranceClassModel CopyDeep()
        {
            return new ToleranceClassModel(Entity.CopyDeep());
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Relative)}: {Relative}, {nameof(_symmetricalLimits)}: {_symmetricalLimits}, {nameof(LowerLimit)}: {LowerLimit}, {nameof(UpperLimit)}: {UpperLimit}, {nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Relative)}: {Relative}, {nameof(SymmetricalLimits)}: {SymmetricalLimits}, {nameof(LowerLimit)}: {LowerLimit}, {nameof(UpperLimit)}: {UpperLimit}";
        }

        public override bool Equals(object obj)
        {
            return obj is ToleranceClassModel tmc && Equals(tmc);
        }
        
        public bool Equals(ToleranceClassModel other)
        {
            return this.EqualsById(other);
        }

        public bool EqualsById(ToleranceClassModel other)
        {
            return Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(ToleranceClassModel other)
        {
            return Entity.EqualsByContent(other?.Entity);
        }
    }
}
