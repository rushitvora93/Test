using System;
using System.Collections.Generic;

namespace UnlockToolShared.Entities
{
    public class SpecificLicense
    {
        public string LicenseName { get; set; }
        public List<ulong> Rights { get; set; }
        public List<string> PcFqdns { get; set; } = new List<string>();
        public string Uuid { get; set; }
        public ulong UseCount { get; set; }
        private DateTime LicenseStart_;
        public DateTime LicenseStart
        {
            get{ return LicenseStart_; }
            set { LicenseStart_ = new DateTime(value.Ticks); }
        }
        private DateTime LicenseEnd_;
        public DateTime LicenseEnd
        {
            get { return LicenseEnd_; }
            set { LicenseEnd_ = new DateTime(value.Ticks); }
        }
        public bool LicenseValid { get; set; }
        public string Issuer { get; set; }
        public string LicenseComment { get; set; }
    }
}
