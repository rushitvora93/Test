using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace FrameworksAndDrivers.Gui.Wpf
{
    public interface IFilteredObservableCollectionExtension<T>
    {
        Predicate<T> Filter { get; set; }
        int Count { get; }
        event EventHandler Refiltered;
        void RefilterCollection();
        void SetNewSourceCollection(ObservableCollection<T> newColl);
        void Dispose();
        void Add(T item);
        void Clear();
        bool Contains(T item);
        void CopyTo(T[] array, int index);
        IEnumerator GetEnumerator();
        int IndexOf(T item);
        void Insert(int index, T item);
        bool Remove(T item);
        void RemoveAt(int index);
        T this[int index] { get; set; }
        void Move(int oldIndex, int newIndex);
        event NotifyCollectionChangedEventHandler? CollectionChanged;
    }

    public class FilteredObservableCollectionExtension<T> : ObservableCollection<T>, IFilteredObservableCollectionExtension<T>
    {
        private ObservableCollection<T> _realCollection;

        private Predicate<T> _filter;
        public Predicate<T> Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                if (_filter != null)
                {
                    RefilterCollection(); 
                }
            }
        }

        public event EventHandler Refiltered;

        public FilteredObservableCollectionExtension(ObservableCollection<T> real)
        {
            if (real != null)
            {
                _realCollection = real;
                _realCollection.CollectionChanged += RealCollection_CollectionChanged; 
            }
            Filter = x => true;
        }

        private void RealCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (T item in e.NewItems)
                {
                    if(Filter(item))
                    {
                        this.Add(item);
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (T item in e.OldItems)
                {
                    this.Remove(item);
                }
            }

            if(e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.Clear();
            }
        }

        public void RefilterCollection()
        {
            if (_realCollection == null) { return; }
            Items.Clear();

            foreach (var item in _realCollection)
            {
                if (Filter(item))
                {
                    Items.Add(item);
                }
            }
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            Refiltered?.Invoke(this, null);
        }

        public void SetNewSourceCollection(ObservableCollection<T> newColl)
        {
            if(_realCollection == newColl) { return; }

            if (_realCollection != null)
            {
                _realCollection.CollectionChanged -= RealCollection_CollectionChanged; 
            }
            _realCollection = newColl;
            _realCollection.CollectionChanged += RealCollection_CollectionChanged;
            RefilterCollection();
        }

        public void Dispose()
        {
            _realCollection.CollectionChanged -= RealCollection_CollectionChanged;
            this.Clear();
            Filter = null;
            _realCollection = null;
        }

        IEnumerator IFilteredObservableCollectionExtension<T>.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
