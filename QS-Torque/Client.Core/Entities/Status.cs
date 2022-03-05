namespace Core.Entities
{
    public class StatusDescription: TypeCheckedString<MaxLength<CtInt30>, Blacklist<NewLines>, NoCheck>
	{
		public StatusDescription(string description)
			: base(description)
		{
		}
	}

    public class Status : HelperTableEntity, IQstEquality<Status>, IUpdate<Status>, ICopy<Status>
    {
        public StatusDescription Value { get; set; }


        public bool EqualsById(Status other)
        {
            return this.ListId.Equals(other?.ListId);
        }

        public bool EqualsByContent(Status other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ListId.Equals(other.ListId) &&
                   this.Value.Equals(other.Value);
        }

        public void UpdateWith(Status other)
        {
            if (other == null)
            {
                return;
            }

            this.ListId = other.ListId;
            this.Value = other.Value;
        }

        public Status CopyDeep()
        {
            return new Status()
            {
                ListId = this.ListId != null ? new HelperTableEntityId(this.ListId.ToLong()) : null,
                Value = this.Value != null ? new StatusDescription(this.Value.ToDefaultString()) : null
            };
        }
    }
}
