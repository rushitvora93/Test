namespace Core.Entities
{
    public class ConstructionType : HelperTableEntity, IQstEquality<ConstructionType>, IUpdate<ConstructionType>, ICopy<ConstructionType>, IStandardHelperTable
    {
        public HelperTableDescription Value { get; set; }


        public bool EqualsById(ConstructionType other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(ConstructionType other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(ConstructionType other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public ConstructionType CopyDeep()
        {
            return new ConstructionType()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new HelperTableDescription(this.Value.ToDefaultString()) : null
            };
        }
    }
}
