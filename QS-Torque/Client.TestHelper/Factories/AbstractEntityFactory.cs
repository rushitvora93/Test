using System;


namespace Client.TestHelper.Factories
{
    public abstract class AbstractEntityFactory
    {
        protected static double? RandomNullableDouble(Random randomizer)
        {
            if (randomizer.Next() % 2 == 0)
            {
                return randomizer.NextDouble();
            }
            return null;
        }

        protected static long? RandomNullableLong(Random randomizer)
        {
            if (randomizer.Next() % 2 == 0)
            {
                return randomizer.Next();
            }
            return null;
        }
    }
}
