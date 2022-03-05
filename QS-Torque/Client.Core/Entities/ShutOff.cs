namespace Core.Entities
{
    public class ShutOff : HelperTableEntity, IQstEquality<ShutOff>, IUpdate<ShutOff>, ICopy<ShutOff>, IStandardHelperTable
    {
        public HelperTableDescription Value { get; set; }


        public bool EqualsById(ShutOff other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(ShutOff other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(ShutOff other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public ShutOff CopyDeep()
        {
            return new ShutOff()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new HelperTableDescription(this.Value.ToDefaultString()) : null
            };
        }
    }
}
