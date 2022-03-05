using Client.Core.ChangesGenerators;
using Client.UseCases.UseCases;
using Core.Diffs;
using InterfaceAdapters.Models;
using InterfaceAdapters.Models.Diffs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapters
{
    public interface IHistoryInterface : INotifyPropertyChanged
    {
        ObservableCollection<ValueChangesContainerModel> LocationChanges { get; }
    }


    public class HistoryInterfaceAdapter : BindableBase, IHistoryInterface, IHistoryGui
    {
        private ObservableCollection<ValueChangesContainerModel> _locationChanges;
        public ObservableCollection<ValueChangesContainerModel> LocationChanges
        {
            get => _locationChanges;
            set
            {
                _locationChanges = value;
                RaisePropertyChanged();
            }
        }

        public void LoadLocationChanges(List<ValueChangesContainer> diffs)
        {
            LocationChanges = new ObservableCollection<ValueChangesContainerModel>(diffs.Select(x => ValueChangesContainerModel.GetModelFor(x)));
        }
    }
}
