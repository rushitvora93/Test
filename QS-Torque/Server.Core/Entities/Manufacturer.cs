using System;
using Core;
using Core.Entities;

namespace Server.Core.Entities
{
	public class ManufacturerId : QstIdentifier, IEquatable<ManufacturerId>
	{
		public ManufacturerId(long value)
			: base(value)
		{
		}

		public bool Equals(ManufacturerId other)
		{
			return Equals((QstIdentifier)other);
		}
	}

	public class ManufacturerName: TypeCheckedString<MaxLength<CtInt30>, Blacklist<NewLines>, NoCheck>
	{
		public ManufacturerName(string name)
			: base(name)
		{
		}
	}

	public class Manufacturer : IQstEquality<Manufacturer>, IUpdate<Manufacturer>, ICopy<Manufacturer>
    {
        public ManufacturerId Id{ get; set; }
        public ManufacturerName Name { get; set; }
        public string Person { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Plz { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Comment { get; set; }
        public bool Alive { get; set; }


        public bool EqualsById(Manufacturer other)
        {
            return Id.Equals(other?.Id);
        }

        public bool EqualsByContent(Manufacturer other)
        {
            if (other == null)
            {
                return false;
            }

            return Id.Equals(other.Id) &&
                   (Name?.Equals(other.Name) ?? other.Name == null) &&
                   Person == other.Person &&
                   PhoneNumber == other.PhoneNumber &&
                   Fax == other.Fax &&
                   Street == other.Street &&
                   HouseNumber == other.HouseNumber &&
                   Plz == other.Plz &&
                   City == other.City &&
                   Country == other.Country &&
                   Comment == other.Comment;
        }

        public void UpdateWith(Manufacturer other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.Name = other.Name;
            this.Person = other.Person;
            this.PhoneNumber = other.PhoneNumber;
            this.Fax = other.Fax;
            this.Street = other.Street;
            this.HouseNumber = other.HouseNumber;
            this.Plz = other.Plz;
            this.City = other.City;
            this.Country = other.Country;
            this.Comment = other.Comment;
        }

        public Manufacturer CopyDeep()
        {
            return new Manufacturer()
            {
                Id = this.Id != null ? new ManufacturerId(this.Id.ToLong()) : null,
                Name = this.Name != null ? new ManufacturerName(this.Name.ToDefaultString()) : null,
                Person = this.Person,
                PhoneNumber = this.PhoneNumber,
                Fax = this.Fax,
                Street = this.Street,
                HouseNumber = this.HouseNumber,
                Plz = this.Plz,
                City = this.City,
                Country = this.Country,
                Comment = this.Comment
            };
        }
    }
}
