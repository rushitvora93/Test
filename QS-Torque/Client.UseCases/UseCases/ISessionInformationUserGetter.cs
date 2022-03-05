using Core.Entities;

namespace Core.UseCases
{
	public interface ISessionInformationUserGetter
	{
		User GetCurrentUser();
	}
}