/* Author:  Leonardo Trevisan Silio
 * Date:    06/04/2023
 */
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Stately;

/// <summary>
/// Represent a state of a object.
/// </summary>
public abstract class State
{
    internal List<Watcher> watchers = null;

    /// <summary>
    /// Notify the changes in this state.
    /// </summary>
    public virtual void OnChanged()
    {
        foreach (var watcher in this.watchers)
            watcher.OnWatchUpdate();
    }

    protected State()
    {
        this.watchers = new List<Watcher>();
        init();
    }

    protected virtual void init()
    {
        var type = GetType();
        init(type, this);
    }

    protected void init(Type type, object obj)
    {
        foreach (var prop in type.GetRuntimeProperties())
        {
            var propType = prop.PropertyType;
            object[] parameters = { this };
            var property = Activator.CreateInstance(
                propType, parameters
            );
            prop.SetValue(obj, property);
        }
    }
}