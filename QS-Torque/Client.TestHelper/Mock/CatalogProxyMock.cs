using Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TestHelper.Mock
{
    public class CatalogProxyMock : ICatalogProxy
    {
        public string GetParticularStringContext { get; set; }
        public string GetParticularStringText { get; set; }
        public string GetParticularStringReturnValue { get; set; }
        public string GetStringText { get; set; }
        public string GetStringReturnValue { get; set; }

        public List<String> GetParticularStringContextList { get; set; } = new List<string>();
        public List<String> GetParticularStringTextList { get; set; } = new List<string>();
        public List<String> GetStringTextList { get; set; } = new List<string>();

        public string GetParticularString(string context, string text)
        {
            GetParticularStringContext = context;
            GetParticularStringContextList.Add(context);
            GetParticularStringText = text;
            GetParticularStringTextList.Add(text);
            return GetParticularStringReturnValue;
        }

        public string GetString(string text)
        {
            GetStringText = text;
            GetStringTextList.Add(text);
            return GetStringReturnValue;
        }
    }
}
