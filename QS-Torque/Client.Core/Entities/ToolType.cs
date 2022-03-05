namespace Core.Entities
{
    public class ToolType : HelperTableEntity, IQstEquality<ToolType>, IUpdate<ToolType>, ICopy<ToolType>, IStandardHelperTable
    {
        public HelperTableDescription Value { get; set; }


        public bool EqualsById(ToolType other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(ToolType other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(ToolType other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public ToolType CopyDeep()
        {
            return new ToolType()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new HelperTableDescription(this.Value.ToDefaultString()) : null
            };
        }
    }
}
