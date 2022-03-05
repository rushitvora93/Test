using Core.Entities;
using TestHelper.Mock;

namespace TestHelper.Factories
{
    public class CreateHelperTableEntityMock
    {
        public static HelperTableEntityMock Anonymous()
        {
            return Parametrized(0, null);
        }

        public static HelperTableEntityMock Parametrized(long id, string description)
        {
            return new HelperTableEntityMock { ListId = new HelperTableEntityId(id), Description = new HelperTableDescription(description) };
        }

        public static HelperTableEntityMock WithDescription(string description)
        {
            return Parametrized(0, description);
        }

        public static HelperTableEntityMock WithId(long id)
        {
            return Parametrized(id, "");
        }
    }
}
