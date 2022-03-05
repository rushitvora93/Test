using System;
using System.Diagnostics;

namespace Core.Entities
{
    public class ToleranceClassId: QstIdentifier, IEquatable<ToleranceClassId>
	{
		public ToleranceClassId(long value)
			: base(value)
		{
			Debug.Assert(value >= 0, "ToleranceClassIds should be positive");
		}

        public bool Equals(ToleranceClassId other)
        {
            return Equals((QstIdentifier) other);
        }
	}

	public class ToleranceClass : IQstEquality<ToleranceClass>, IUpdate<ToleranceClass>, ICopy<ToleranceClass>
    {
        public ToleranceClassId Id { get; set; }
        public string Name { get; set; }
        public bool Relative { get; set; }
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }

        public virtual double GetLowerLimitForValue(double value)
        {
            if (!Relative)
            {
                if (value > LowerLimit)
                {
                    return value - LowerLimit;
                }
            }
            else
            {
                if (value > (value * LowerLimit / 100))
                {
                    return value - (value * LowerLimit / 100); 
                }
            }

            return 0;
        }

        public virtual double GetUpperLimitForValue(double value)
        {
            if (!Relative)
            {
                return value + UpperLimit;
            }
            else
            {
                return value + (value * UpperLimit / 100);
            }
        }

        public bool EqualsById(ToleranceClass other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(ToleranceClass other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                   this.Name == other.Name &&
                   this.Relative == other.Relative &&
                   this.LowerLimit == other.LowerLimit &&
                   this.UpperLimit == other.UpperLimit;
        }

        public void UpdateWith(ToleranceClass other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.Name = other.Name;
            this.Relative = other.Relative;
            this.LowerLimit = other.LowerLimit;
            this.UpperLimit = other.UpperLimit;
        }

        public ToleranceClass CopyDeep()
        {
            return new ToleranceClass()
            {
                Id = this.Id != null ? new ToleranceClassId(this.Id.ToLong()) : null,
                Name = this.Name,
                Relative = this.Relative,
                LowerLimit = this.LowerLimit,
                UpperLimit = this.UpperLimit
            };
        }
    }
}
