using System;
using Core.Entities;
using TestHelper.Factories;

namespace Client.TestHelper.Factories
{
    public class CreateHistoryComment
    {
        public static HistoryComment Parametrized(string comment)
        {
            return new HistoryComment(comment);
        }

        public static HistoryComment Anonymous()
        {
            return Parametrized("");
        }

        public static HistoryComment Randomized(int seed)
        {
            var randomizer = new Random(seed);
            // 4000 max length because comments can be this long
            // may be reduced for performance
            return Parametrized(CreateString.Randomized(randomizer.Next(0, 4000), randomizer.Next()));
        }
    }
}
