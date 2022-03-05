using Core;

namespace Server.Core.Entities
{
    public class ConfigurableField : HelperTableEntity, IQstEquality<ConfigurableField>, IUpdate<ConfigurableField>,
        ICopy<ConfigurableField>
    {
        public bool EqualsById(ConfigurableField other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(ConfigurableField other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(ConfigurableField other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public ConfigurableField CopyDeep()
        {
            return new ConfigurableField()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new HelperTableEntityValue(this.Value.ToDefaultString()) : null
            };
        }
    }
}
