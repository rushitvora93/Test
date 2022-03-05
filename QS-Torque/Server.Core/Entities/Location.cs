using System;
using Core;
using Core.Entities;
using Server.Core.Enums;

namespace Server.Core.Entities
{
    public class LocationId : QstIdentifier, IEquatable<LocationId>
    {
        public LocationId(long value)
            : base(value)
        {
        }

        public bool Equals(LocationId other)
        {
            return Equals((QstIdentifier) other);
        }
    }

    public class LocationNumber : TypeCheckedString<MaxLength<CtInt30>, Blacklist<NewLines>, NoCheck>
    {
        public LocationNumber(string number)
            : base(number)
        {
        }
    }

    public class LocationDescription : TypeCheckedString<MaxLength<CtInt50>, Blacklist<NewLines>, NoCheck>
    {
        public LocationDescription(string description)
            : base(description)
        {
        }
    }

    public class LocationConfigurableField1 : TypeCheckedString<MaxLength<CtInt15>, Blacklist<NewLines>, NoCheck>
    {
        public LocationConfigurableField1(string value)
            : base(value)
        {
        }
    }

    public class LocationConfigurableField2 : TypeCheckedString<MaxLength<CtInt1>, Blacklist<NewLines>, NoCheck>
    {
        public LocationConfigurableField2(string value)
            : base(value)
        {
        }
    }

    public class Location : IQstEquality<Location>, IUpdate<Location>, ICopy<Location>
    {
        public LocationId Id { get; set; }
        public LocationNumber Number { get; set; }
        public LocationDescription Description { get; set; }
        public LocationDirectoryId ParentDirectoryId { get; set; }
        public LocationControlledBy ControlledBy { get; set; }
        public double SetPoint1 { get; set; }
        public ToleranceClass ToleranceClass1 { get; set; }
        public double Minimum1 { get; set; }
        public double Maximum1 { get; set; }
        public double Threshold1 { get; set; }
        public double SetPoint2 { get; set; }
        public ToleranceClass ToleranceClass2 { get; set; }
        public double Minimum2 { get; set; }
        public double Maximum2 { get; set; }
        public LocationConfigurableField1 ConfigurableField1 { get; set; }
        public LocationConfigurableField2 ConfigurableField2 { get; set; }
        public bool ConfigurableField3 { get; set; }
        public bool Alive { get; set; }
        public string Comment { get; set; }

        public bool EqualsById(Location other)
        {
            return this.Id.Equals(other?.Id);
        }

        public bool EqualsByContent(Location other)
        {
            if (other == null)
            {
                return false;
            }

            var eqByContent = this.Id.Equals(other.Id) &&
                   (this.Number?.Equals(other.Number) ?? other.Number == null) &&
                   (this.Description?.Equals(other.Description) ?? other.Description == null) &&
                   (this.ParentDirectoryId?.Equals(other.ParentDirectoryId) ?? other.ParentDirectoryId == null) &&
                   this.ControlledBy == other.ControlledBy &&
                   this.SetPoint1.Equals(other.SetPoint1) &&
                   (this.ToleranceClass1?.EqualsByContent(other.ToleranceClass1) ??
                    other.ToleranceClass1 == null) &&
                   this.Minimum1.Equals(other.Minimum1) &&
                   this.Maximum1.Equals(other.Maximum1) &&
                   this.Threshold1.Equals(other.Threshold1) &&
                   this.SetPoint2.Equals(other.SetPoint2) &&
                   (this.ToleranceClass2?.EqualsByContent(other.ToleranceClass2) ??
                    other.ToleranceClass2 == null) &&
                   this.Minimum2.Equals(other.Minimum2) &&
                   this.Maximum2.Equals(other.Maximum2) &&
                   (this.ConfigurableField1?.Equals(other.ConfigurableField1) ?? other.ConfigurableField1 == null) &&
                   (this.ConfigurableField2?.Equals(other.ConfigurableField2) ?? other.ConfigurableField2 == null) &&
                   this.ConfigurableField3 == other.ConfigurableField3 &&
                   this.Alive == other.Alive &&
                   this.Comment == other.Comment;

            return eqByContent;
        }

        public void UpdateWith(Location other)
        {
            if (other == null)
            {
                return;
            }

            this.Id = other.Id;
            this.Number = other.Number;
            this.Description = other.Description;
            this.ParentDirectoryId = other.ParentDirectoryId;
            this.ControlledBy = other.ControlledBy;
            this.SetPoint1 = other.SetPoint1;
            this.ToleranceClass1 = other.ToleranceClass1;
            this.Minimum1 = other.Minimum1;
            this.Maximum1 = other.Maximum1;
            this.Threshold1 = other.Threshold1;
            this.SetPoint2 = other.SetPoint2;
            this.ToleranceClass2 = other.ToleranceClass2;
            this.Minimum2 = other.Minimum2;
            this.Maximum2 = other.Maximum2;
            this.ConfigurableField1 = other.ConfigurableField1;
            this.ConfigurableField2 = other.ConfigurableField2;
            this.ConfigurableField3 = other.ConfigurableField3;
            this.Comment = other.Comment;
            this.Alive = other.Alive;
        }

        public Location CopyDeep()
        {
            return new Location()
            {
                Id = this.Id != null ? new LocationId(this.Id.ToLong()) : null,
                Number = this.Number != null ? new LocationNumber(this.Number.ToDefaultString()) : null,
                Description = this.Description != null ? new LocationDescription(this.Description.ToDefaultString()) : null,
                ParentDirectoryId = this.ParentDirectoryId != null ? new LocationDirectoryId(this.ParentDirectoryId.ToLong()) : null,
                ControlledBy = this.ControlledBy,
                SetPoint1 = this.SetPoint1,
                ToleranceClass1 = this.ToleranceClass1?.CopyDeep(),
                Minimum1 = this.Minimum1,
                Maximum1 = this.Maximum1,
                Threshold1= this.Threshold1,
                SetPoint2 = this.SetPoint2,
                ToleranceClass2 = this.ToleranceClass2?.CopyDeep(),
                Minimum2 = this.Minimum2,
                Maximum2 = this.Maximum2,
                ConfigurableField1 = this.ConfigurableField1 != null ? new LocationConfigurableField1(this.ConfigurableField1.ToDefaultString()) : null,
                ConfigurableField2 = this.ConfigurableField2 != null ? new LocationConfigurableField2(this.ConfigurableField2.ToDefaultString()) : null,
                ConfigurableField3 = this.ConfigurableField3,
                Comment = this.Comment,
                Alive = this.Alive
            };
        }
    }
}