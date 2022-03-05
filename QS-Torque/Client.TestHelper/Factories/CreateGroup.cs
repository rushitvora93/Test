using Core.Entities;
using System;

namespace TestHelper.Factories
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

        public static Group Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()));
        }
    }
}
