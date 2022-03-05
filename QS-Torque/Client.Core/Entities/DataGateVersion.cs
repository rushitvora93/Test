using System.Collections.Generic;
using System;

namespace Client.Core.Entities
{
    public class DataGateVersion : IEquatable<DataGateVersion>
    {
        private readonly int _dataGateVersion;

        public static Dictionary<int, string> DataGateVersions = new Dictionary<int, string>()
        {
            {0, ""},
            {1, "1.0"},
            {2, "2.0"},
            {3, "3.0"},
            {4, "4.0"},
            {5, "5.0"},
            {6, "6.0"},
            {7, "7.0"},
        };

        public DataGateVersion(int dataGateVersion)
        {
            if (!DataGateVersions.ContainsKey(dataGateVersion))
            {
                throw new ArgumentException("Unknown dataGate version");
            }
            _dataGateVersion = dataGateVersion;
        }

        public string DataGateVersionsString => DataGateVersions[_dataGateVersion];

        public int DataGateVersionsId => _dataGateVersion;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DataGateVersion)obj);
        }

        public override int GetHashCode()
        {
            return _dataGateVersion.GetHashCode();
        }

        public bool Equals(DataGateVersion other)
        {
            return this.DataGateVersionsId == other?.DataGateVersionsId;
        }
    }
}
