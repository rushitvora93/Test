using System;
using System.Collections.Generic;
using Core;
using Core.Entities;

namespace InterfaceAdapters.Models
{
    public class ManufacturerModelComparer : IEqualityComparer<ManufacturerModel>
    {
        public bool Equals(ManufacturerModel x, ManufacturerModel y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(ManufacturerModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }

    public class ManufacturerModel :  BindableBase, IEquatable<ManufacturerModel>, IQstEquality<ManufacturerModel>, IUpdate<Manufacturer>, ICopy<ManufacturerModel>
    {
        public Manufacturer Entity { get; private set; }


        #region Properties
        public long Id
        {
            get { return Entity.Id.ToLong(); }
            set
            {
                Entity.Id = new ManufacturerId(value);
                RaisePropertyChanged();
            }
        }
        
        public string Name
        {
            get { return Entity.Name.ToDefaultString(); }
            set
            {
                Entity.Name = new ManufacturerName(value);
                RaisePropertyChanged();
            }
        }
        
        public string Person
        {
            get { return Entity.Person; }
            set
            {
                Entity.Person = value;
                RaisePropertyChanged();
            }
        }
        
        public string PhoneNumber
        {
            get { return Entity.PhoneNumber; }
            set
            {
                Entity.PhoneNumber = value;
                RaisePropertyChanged();
            }
        }
        
        public string Fax
        {
            get { return Entity.Fax; }
            set
            {
                Entity.Fax = value;
                RaisePropertyChanged();
            }
        }
        
        public string Street
        {
            get { return Entity.Street; }
            set
            {
                Entity.Street = value;
                RaisePropertyChanged();
            }
        }
        
        public string HouseNumber
        {
            get { return Entity.HouseNumber; }
            set
            {
                Entity.HouseNumber = value;
                RaisePropertyChanged();
            }
        }
        
        public string Plz
        {
            get { return Entity.Plz; }
            set
            {
                Entity.Plz = value;
                RaisePropertyChanged();
            }
        }
        
        public string City
        {
            get { return Entity.City; }
            set
            {
                Entity.City = value;
                RaisePropertyChanged();
            }
        }
        
        public string Country
        {
            get { return Entity.Country; }
            set
            {
                Entity.Country = value;
                RaisePropertyChanged();
            }
        }
        
        public string Comment
        {
            get { return Entity.Comment; }
            set
            {
                Entity.Comment = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        public ManufacturerModel(Manufacturer entity)
        {
            Entity = entity ?? new Manufacturer();
            RaisePropertyChanged();
        }

        public static ManufacturerModel GetModelFor(Manufacturer entity)
        {
            return entity != null ? new ManufacturerModel(entity) : null;
        }


        #region Methods
        public void UpdateWith(Manufacturer manufacturer)
        {
			this.Entity.UpdateWith(manufacturer);
            RaisePropertyChanged();
        }
        #endregion

        
        public override bool Equals(object obj)
        {
            if (obj is ManufacturerModel manu)
            {
                return Equals(manu);
            }
            else
            { 
                return false;
            }
        }

        #region Interface

        public bool Equals(ManufacturerModel other)
        {
            return this.EqualsById(other);
        }

        public bool EqualsById(ManufacturerModel other)
        {
            return Entity.EqualsById(other?.Entity);
        }

        public bool EqualsByContent(ManufacturerModel other)
        {
            return Entity.EqualsByContent(other?.Entity);
        }

        #endregion

        public void UpdateWidth(ManufacturerModel other)
        {
            this.Entity.UpdateWith(other?.Entity);
        }

        public ManufacturerModel CopyDeep()
        {
            return new ManufacturerModel(Entity.CopyDeep());
        }
    }
}