namespace Core.Entities
{
    public class DriveType : HelperTableEntity, IQstEquality<DriveType>, IUpdate<DriveType>, ICopy<DriveType>, IStandardHelperTable
    {
        public HelperTableDescription Value { get; set; }


        public bool EqualsById(DriveType other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(DriveType other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(DriveType other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public DriveType CopyDeep()
        {
            return new DriveType()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new HelperTableDescription(this.Value.ToDefaultString()) : null
            };
        }
    }
}
