using System;

namespace Core.Entities
{
    public class LocationDirectoryId : QstIdentifier, IEquatable<LocationDirectoryId>
    {
        public LocationDirectoryId(long value)
            : base(value)
        {
        }

        public bool Equals(LocationDirectoryId other)
        {
            return Equals((QstIdentifier) other);
        }
    }

    public class LocationDirectoryName : TypeCheckedString<MaxLength<CtInt50>, Blacklist<NewLines>, NoCheck>
    {
        public LocationDirectoryName(string value)
            : base(value)
        {
        }
    }

    public class LocationDirectory : IQstEquality<LocationDirectory>, IUpdate<LocationDirectory>, ICopy<LocationDirectory>
    {
        public  LocationDirectoryId Id { get; set; }
        public LocationDirectoryName Name { get; set; }
        public LocationDirectoryId ParentId { get; set; }


        public bool EqualsById(LocationDirectory other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(LocationDirectory other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id.Equals(other.Id) &&
                   (this.Name?.Equals(other.Name) ?? other.Name == null) &&
                   (this.ParentId?.Equals(other.ParentId) ?? other.ParentId == null);
        }

        public void UpdateWith(LocationDirectory other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.Name = other.Name;
            this.ParentId = other.ParentId;
        }

        public LocationDirectory CopyDeep()
        {
            return new LocationDirectory()
            {
                Id = this.Id != null ? new LocationDirectoryId(this.Id.ToLong()) : null,
                Name = this.Name != null ? new LocationDirectoryName(this.Name.ToDefaultString()) : null,
                ParentId = this.ParentId != null ? new LocationDirectoryId(this.ParentId.ToLong()) : null
            };
        }
    }
}
