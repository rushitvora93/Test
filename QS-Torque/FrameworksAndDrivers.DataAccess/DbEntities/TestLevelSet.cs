using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrameworksAndDrivers.DataAccess.DbEntities
{
    [Table("TestLevelSet")]
    public class TestLevelSet
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
    }

    [Table("TestLevelSetChanges")]
    public class TestLevelSetChanges
    {
        [Key]
        public long GlobalHistoryId { get; set; }

        public long Id { get; set; }
        public long UserId { get; set; }
        public string Action { get; set; }
        public string UserComment { get; set; }
        public string NameOld { get; set; }
        public string NameNew { get; set; }
    }
}
