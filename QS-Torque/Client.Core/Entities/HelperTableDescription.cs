namespace Core.Entities
{
    public class HelperTableDescription: TypeCheckedString<MaxLength<CtInt50>, Blacklist<NewLines>, NoCheck>
	{
		public HelperTableDescription(string description)
			: base(description)
		{
		}
	}
}
