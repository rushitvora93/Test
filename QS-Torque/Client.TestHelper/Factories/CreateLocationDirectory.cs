using Core.Entities;

namespace TestHelper.Factories
{
    public class CreateLocationDirectory
    {
        public static LocationDirectory Parameterized(long id, string name, long? parentid)
        {
            LocationDirectoryId realParentid = parentid.HasValue ? new LocationDirectoryId(parentid.Value) : null;
            return new LocationDirectory(){Id = new LocationDirectoryId(id), Name = new LocationDirectoryName(name), ParentId = realParentid};
        }

        public static LocationDirectory WithId(long id)
        {
            return Parameterized(id, "", 2);
        }

        public static LocationDirectory Anonymous()
        {
            return Parameterized(1, "", 2);
        }
    }
}
