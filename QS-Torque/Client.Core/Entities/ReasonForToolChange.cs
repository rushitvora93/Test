namespace Core.Entities
{
    public class ReasonForToolChange : HelperTableEntity, IQstEquality<ReasonForToolChange>, IUpdate<ReasonForToolChange>, ICopy<ReasonForToolChange>, IStandardHelperTable
    {
        public HelperTableDescription Value { get; set; }


        public bool EqualsById(ReasonForToolChange other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(ReasonForToolChange other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(ReasonForToolChange other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public ReasonForToolChange CopyDeep()
        {
            return new ReasonForToolChange()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new HelperTableDescription(this.Value.ToDefaultString()) : null
            };
        }
    }
}
