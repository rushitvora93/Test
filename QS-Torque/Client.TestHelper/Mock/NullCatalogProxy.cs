using Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TestHelper.Mock
{
	public class NullCatalogProxy : ICatalogProxy
	{
		public string GetParticularString(string context, string text)
		{
			return "";
		}

		public string GetString(string text)
		{
			return "";
		}
	}
}
