using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SportMetricsViewer.Extensions;

public class ExtendedObservableCollection<T> : ObservableCollection<T>
{
    public ExtendedObservableCollection()
    {
        
    }

    public ExtendedObservableCollection(IEnumerable<T> collection) : base(collection)
    {
        
    }

    public void AddRange(IEnumerable<T> collection)
    {
        AddRangeInternal(collection);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void AddRangeInternal(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        foreach (var item in collection)
        {
            Items.Add(item);
        }
    }

    public void RemoveRange(ICollection<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        if (collection.Count == 0)
        {
            return;
        }
        RemoveRangeInternal(collection);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    private void RemoveRangeInternal(IEnumerable<T> collection)
    {
        foreach (var item in collection)
        {
            Items.Remove(item);
        }
    }

    public void RefillBy(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        var toDelete = this.ToArray();
        AddRangeInternal(collection);
        RemoveRangeInternal(toDelete);
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
}
