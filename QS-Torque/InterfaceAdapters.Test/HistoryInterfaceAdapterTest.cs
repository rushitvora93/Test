using Client.Core.ChangesGenerators;
using Core.Diffs;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters.Test
{
    public class HistoryInterfaceAdapterTest
    {
        [TestCase(2)]
        [TestCase(5)]
        public void LoadLocationDiffsFillsLocationChanges(int count)
        {
            var adapter = new HistoryInterfaceAdapter();
            var entityList = new List<ValueChangesContainer>();
            for (int i = 0; i < count; i++)
            {
                entityList.Add(new ValueChangesContainer() { Changes = new List<SingleValueChange>() });
            }
            adapter.LoadLocationChanges(entityList);
            for (int i = 0; i < count; i++)
            {
                Assert.AreSame(entityList[i], adapter.LocationChanges[i].Entity);
            }
        }
    }
}
