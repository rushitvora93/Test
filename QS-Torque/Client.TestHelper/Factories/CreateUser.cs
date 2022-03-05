using System;
using Core.Entities;

namespace TestHelper.Factories
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

        public static User Randomized(int seed)
        {
            var randomizer = new Random(seed);
            return Parametrized(
                randomizer.Next(),
                CreateString.Randomized(randomizer.Next(0, 50), randomizer.Next()),
                CreateGroup.Randomized(randomizer.Next()));
        }
    }
}
