using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core
{
    public interface ICatalogProxy
    {
        string GetString(string text);
        string GetParticularString(string context, string text);
    }
}
