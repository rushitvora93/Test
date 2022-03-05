using System;

namespace Core.PhysicalValueTypes
{
    public class Angle : IEquatable<Angle>
    {
        // Main unit
        public double Degree { get; private set; }

        /*
         * Example: How to create a PhysicalValueType with more/different units
         */
        public double Radiant
        {
            get => (Degree * Math.PI) / 180;
            private set => Degree = (value * 180) / Math.PI;
        }


        private Angle() { }

        public static Angle FromDegree(double degree)
        {
            return new Angle()
            {
                Degree = degree
            };
        }

        /*
         * Example: How to create a PhysicalValueType with more/different units
         */
        public static Angle FromRadiant(double radiant)
        {
            return new Angle()
            {
                Radiant = radiant
            };
        }


        public bool Equals(Angle other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Degree.Equals(other.Degree);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Angle) obj);
        }

        public override int GetHashCode()
        {
            return Degree.GetHashCode();
        }
    }
}
