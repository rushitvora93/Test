namespace Core.Entities
{
    public class DriveSize : HelperTableEntity, IQstEquality<DriveSize>, IUpdate<DriveSize>, ICopy<DriveSize>, IStandardHelperTable
    {
        public HelperTableDescription Value { get; set; }


        public bool EqualsById(DriveSize other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(DriveSize other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(DriveSize other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public DriveSize CopyDeep()
        {
            return new DriveSize()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new HelperTableDescription(this.Value.ToDefaultString()) : null
            };
        }
    }
}
