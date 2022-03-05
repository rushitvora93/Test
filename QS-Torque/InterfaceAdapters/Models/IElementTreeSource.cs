using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace InterfaceAdapters.Models
{
    public interface IElementTreeSource<T>
    {
        ObservableCollection<T> TreeElements { get; }
        
        /// <returns>Value-Item is the parent of the Key-Item</returns>
        Dictionary<T, T> GetTreeEdgesFor(IEnumerable<T> elements);
        IEnumerable<T> GetElementsTopologicalSorted(IEnumerable<T> elements, Dictionary<T, T> edges);
    }
}
