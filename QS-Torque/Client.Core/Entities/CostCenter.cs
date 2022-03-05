namespace Core.Entities
{
    public class CostCenter : HelperTableEntity, IQstEquality<CostCenter>, IUpdate<CostCenter>, ICopy<CostCenter>, IStandardHelperTable
    {
        public HelperTableDescription Value { get; set; }


        public bool EqualsById(CostCenter other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(CostCenter other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(CostCenter other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public CostCenter CopyDeep()
        {
            return new CostCenter()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new HelperTableDescription(this.Value.ToDefaultString()) : null
            };
        }
    }
}
