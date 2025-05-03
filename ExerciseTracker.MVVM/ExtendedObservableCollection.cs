using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ExerciseTracker.MVVM;

public class ExtendedObservableCollection<T> : ObservableCollection<T>
{
    public ExtendedObservableCollection()
    {
        
    }

    public ExtendedObservableCollection(IEnumerable<T> collection) : base(collection)
    {
        
    }

    public void AddRange(IList<T> list)
    {
        ArgumentNullException.ThrowIfNull(list);
        AddRangeInternal(list);
        if (list.Count > 0)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list as IList));
        }
    }

    public void RemoveRange(IList<T> list)
    {
        ArgumentNullException.ThrowIfNull(list);
        RemoveRangeInternal(list);
        if (list.Count > 0)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, list as IList));
        }
    }

    private void AddRangeInternal(IEnumerable<T> collection)
    {
        foreach (var item in collection)
        {
            Items.Add(item);
        }
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
