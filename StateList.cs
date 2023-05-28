/* Author:  Leonardo Trevisan Silio
 * Date:    28/05/2023
 */
using System.Collections.Generic;
using System.Collections;

namespace Stately;

public class StateList<T> : State, ICollection<T>
{
    private List<T> list = new List<T>();

    public int Count => list.Count;
    public bool IsReadOnly => false;

    public void Add(T item)
    {
        list.Add(item);

        if (item is State state)
        {
            SubStateWatcher watcher = new SubStateWatcher(this);
            watcher.Watch(state);
        }

        this.OnChanged();
    }

    public void Clear()
    {
        list.Clear();
        this.OnChanged();
    }

    public bool Contains(T item)
        => list.Contains(item);

    public void CopyTo(T[] array, int arrayIndex)
        => list.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator()
        => list.GetEnumerator();

    public bool Remove(T item)
    {
        bool removed = list.Remove(item);

        if (!removed)
            return false;

        this.OnChanged();
        return true;
    }

    IEnumerator IEnumerable.GetEnumerator()
        => list.GetEnumerator();
}