using InterfaceAdapters;
using InterfaceAdapters.Models;
using InterfaceAdapters.Models.Diffs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TestHelper.Mock
{
    public class HistoryInterfaceMock : IHistoryInterface
    {
        public ObservableCollection<ValueChangesContainerModel> LocationChanges => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
