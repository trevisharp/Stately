/* Author:  Leonardo Trevisan Silio
 * Date:    08/08/2023
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
            if (!propType.IsGenericType)
                continue;

            bool isProp = propType.GetGenericTypeDefinition() == typeof(Property<>);
            bool isBaseTypeProp = propType.BaseType.GetGenericTypeDefinition() == typeof(Property<>);
            if (!isProp && !isBaseTypeProp)
                continue;
            
            var property = createProperty(propType);
            prop.SetValue(obj, property);
        }
        
        foreach (var field in type.GetRuntimeFields())
        {
            var fieldType = field.FieldType;
            if (!fieldType.IsGenericType)
                continue;
            
            bool isProp = fieldType.GetGenericTypeDefinition() == typeof(Property<>);
            bool isBaseTypeProp = fieldType.BaseType.GetGenericTypeDefinition() == typeof(Property<>);
            if (!isProp && !isBaseTypeProp)
                continue;
            
            var property = createProperty(fieldType);
            field.SetValue(obj, property);
        }
    }

    private object createProperty(Type propertyType)
    {
        object[] parameters = { this };
        var property = Activator.CreateInstance(
            propertyType, parameters
        );
        return property;
    }
}