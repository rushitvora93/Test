using Core.Entities;
using Core.UseCases;

namespace TestHelper.Mock
{
    public class UserGetterMock : ISessionInformationUserGetter
	{
		public User NextReturnedUser;

		public User GetCurrentUser()
		{
			return NextReturnedUser;
		}
	}
}
