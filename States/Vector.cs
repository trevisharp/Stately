/* Author:  Leonardo Trevisan Silio
 * Date:    06/04/2023
 */
using System.Collections;
using System.Collections.Generic;

namespace Stately.States;

/// <summary>
/// A State List to model complex states. 
/// </summary>
public class Vector<T> : State, IEnumerable<T>
{
    private List<T> list;
    public Vector()
        => this.list = new List<T>();
    
    public void Add(T obj)
    {
        list.Add(obj);
        this.OnChanged();
    }

    public void Remove(T obj)
    {
        list.Remove(obj);
        this.OnChanged();
    }

    public void Insert(int index, T obj)
    {
        list.Insert(index, obj);
        this.OnChanged();
    }

    public IEnumerator<T> GetEnumerator()
        => this.list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}