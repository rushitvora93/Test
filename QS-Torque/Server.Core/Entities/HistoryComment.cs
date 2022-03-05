using Core.Entities;

namespace Server.Core.Entities
{
	public class HistoryComment: TypeCheckedString<MaxLength<CtInt4000>, NoCheck, NoCheck>
	{
		public HistoryComment(string comment)
			: base(comment)
		{
		}
	}
}
