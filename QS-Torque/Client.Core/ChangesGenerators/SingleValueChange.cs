using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.ChangesGenerators
{
    public class SingleValueChange
    {
        public string ChangedAttribute { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        /// <summary>
        /// Just the name of the entity - 
        /// Necessary to group the changes by the entity they belong to
        /// </summary>
        public string AffectedEntity { get; set; }
    }
}
