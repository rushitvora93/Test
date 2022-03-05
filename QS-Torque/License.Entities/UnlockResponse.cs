using System.Collections.Generic;

namespace UnlockToolShared.Entities
{
    public class UnlockResponse
    {
        public ulong UnlockResponseVersion { get; set;  } = 1;
        public string QstVersion { get; set; }
        public List<SpecificLicense> Licenses { get; set; } = new List<SpecificLicense>();
        public string ServerFqdn { get; set; }
        public string PackageComment { get; set; }
    }
}
