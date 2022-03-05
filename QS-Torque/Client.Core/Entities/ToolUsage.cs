namespace Core.Entities
{
    public class ToolUsageDescription : TypeCheckedString<MaxLength<CtInt25>, Blacklist<NewLines>, NoCheck>
    {
        public ToolUsageDescription(string description)
            : base(description)
        {
        }
    }
    

    public class ToolUsage : HelperTableEntity, IQstEquality<ToolUsage>, IUpdate<ToolUsage>, ICopy<ToolUsage>
    {
        public ToolUsageDescription Value { get; set; }


        public bool EqualsById(ToolUsage other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(ToolUsage other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(ToolUsage other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public ToolUsage CopyDeep()
        {
            return new ToolUsage()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new ToolUsageDescription(this.Value.ToDefaultString()) : null
            };
        }
    }
}