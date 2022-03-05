using Server.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Entities
{
    public class DiffTypeUtil
    {
        public const string InsertKey = "INSERT";
        public const string UpdateKey = "UPDATE";
        public const string DeleteKey = "DELETE";

        public static DiffType ConvertToDiffType(string key)
        {
            switch(key)
            {
                case InsertKey: return DiffType.Insert;
                case UpdateKey: return DiffType.Update;
                case DeleteKey: return DiffType.Delete;
                default: return DiffType.Undefined;
            }
        }
    }
}
