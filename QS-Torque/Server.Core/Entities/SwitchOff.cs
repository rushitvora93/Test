using Core;

namespace Server.Core.Entities
{
    public class SwitchOff : HelperTableEntity, IQstEquality<SwitchOff>, IUpdate<SwitchOff>, ICopy<SwitchOff>
    {
        public bool EqualsById(SwitchOff other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(SwitchOff other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(SwitchOff other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public SwitchOff CopyDeep()
        {
            return new SwitchOff()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new HelperTableEntityValue(this.Value.ToDefaultString()) : null
            };
        }
    }
}
