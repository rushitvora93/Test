using FrameworksAndDrivers.Gui.Wpf.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using InterfaceAdapters;
using InterfaceAdapters.Models;

namespace FrameworksAndDrivers.Gui.Wpf.ViewModel
{
    class VerifyChangesViewModel : BindableBase
    {
        #region Properties
        private ObservableCollection<SingleValueChangeModel> _changedValues;
        public ListCollectionView ChangedValues { get; private set; }

        private string _comment = "";
        public string Comment
        {
            get => _comment;
            set => Set(ref _comment, value);
        }
        #endregion


        #region Methods
        private void SetNumbersOfEntires()
        {
            var entities = _changedValues.Select(x => x.AffectedEntity).Distinct().ToList();
            var counter = new Dictionary<string, int>();

            // Inititialize counter
            foreach(var e in entities)
            {
                counter.Add(e, 1);
            }

            // Set Numbers
            foreach(var c in _changedValues)
            {
                foreach(var e in entities)
                {
                    if(c.AffectedEntity == e)
                    {
                        c.Number = counter[e];
                        counter[e]++;
                    }
                }
            }
        }
        #endregion


        public VerifyChangesViewModel(IEnumerable<SingleValueChangeModel> changedValues)
        {
            _changedValues = new ObservableCollection<SingleValueChangeModel>(changedValues);
            ChangedValues = new ListCollectionView(_changedValues);
            SetNumbersOfEntires();
            ChangedValues.GroupDescriptions.Add(new PropertyGroupDescription(nameof(SingleValueChangeModel.AffectedEntity)));
        }
    }
}
