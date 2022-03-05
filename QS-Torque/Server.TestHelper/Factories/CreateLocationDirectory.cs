using Server.Core.Entities;

namespace Server.TestHelper.Factories
{
    public class CreateLocationDirectory
    {
        public static LocationDirectory Parameterized(long id, string name, long? parentId, bool alive)
        {
            LocationDirectoryId realParentId = parentId.HasValue ? new LocationDirectoryId(parentId.Value) : null;
            return new LocationDirectory()
            {
                Id = new LocationDirectoryId(id), 
                Name = new LocationDirectoryName(name), 
                ParentId = realParentId,
                Alive = alive
            };
        }

        public static LocationDirectory WithId(long id)
        {
            return Parameterized(id, "", 2, true);
        }

        public static LocationDirectory Anonymous()
        {
            return Parameterized(1, "", 2, true);
        }
    }
}
