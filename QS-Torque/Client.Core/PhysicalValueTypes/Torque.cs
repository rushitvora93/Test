using System;

namespace Core.PhysicalValueTypes
{
    public class Torque : IEquatable<Torque>
    {
        public double Nm { get; private set; }


        private Torque() { }

        public static Torque FromNm(double nm)
        {
            return new Torque()
            {
                Nm = nm
            };
        }

        public bool Equals(Torque other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Nm.Equals(other.Nm);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Torque) obj);
        }

        public override int GetHashCode()
        {
            return Nm.GetHashCode();
        }
    }
}
