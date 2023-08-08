/* Author:  Leonardo Trevisan Silio
 * Date:    08/08/2023
 */
using System.Collections.Generic;
using System.Collections;

namespace Stately.Collections;

/// <summary>
/// A State List to model complex states. 
/// </summary>
public class StateList<T> : BaseProperty, ICollection<T>
{
    private List<T> list = new List<T>();

    public int Count => list.Count;
    public bool IsReadOnly => false;

    public StateList(State state)
        => this.SetState(state);

    public void Add(T item)
    {
        list.Add(item);

        if (item is State state)
        {
            var watcher = this.CreateSubWatcher();
            watcher.Watch(state);
        }

        this.UpdateState();
    }

    public void Clear()
    {
        list.Clear();
        this.UpdateState();
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
        
        this.UpdateState();
        return true;
    }

    IEnumerator IEnumerable.GetEnumerator()
        => list.GetEnumerator();
}