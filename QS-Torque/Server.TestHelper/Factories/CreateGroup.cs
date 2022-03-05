using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateGroup
    {
        public static Group Parametrized(long id, string name)
        {
            return new Group { Id = new GroupId(id), GroupName = name };
        }

        public static Group Anonymous()
        {
            return Parametrized(7, "");
        }

        public static Group WithName(string name)
        {
            return Parametrized(7, name);
        }
    }
}
