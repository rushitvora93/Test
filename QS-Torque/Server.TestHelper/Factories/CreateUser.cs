using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateUser
    {
        public static User Anonymous()
        {
            return Parametrized(5, "", CreateGroup.Anonymous());
        }

        public static User IdOnly(long userId)
        {
            return Parametrized(userId, "", CreateGroup.Anonymous());
        }

        public static User Parametrized(long id, string name, Group group)
        {
            return new User { UserId = new UserId(id), UserName = name, Group = group };
        }
    }
}
