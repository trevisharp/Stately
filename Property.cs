/* Author:  Leonardo Trevisan Silio
 * Date:    08/08/2023
 */
namespace Stately;

using Exceptions;

/// <summary>
/// A Property to use in States. Property is a State.
/// </summary>
public class Property<T> : BaseProperty
{
    private T value;
    private Watcher watcher;

    public T Value
    {
        get => this.value;
        set => this.tryChange(value);
    }

    public Property(State state)
    {
        this.SetState(state);
        this.watcher = null;
        this.value = default(T);
        this.watchSubState();
    }

    private void watchSubState()
    {
        if (value is null)
            return;

        if (value is State state)
        {
            if (watcher is not null)
                watcher = null;
            
            this.watcher = this.CreateSubWatcher();
            this.watcher.Watch(state);
        }
    }

    private void tryChange(object value)
    {
        var thisIsNull = this.value is null;
        var valueIsNull = value is null;
        if ((thisIsNull && valueIsNull) || (!thisIsNull && this.value.Equals(value)))
            return;

        if (value is T data)
        {
            this.value = data;
            this.watchSubState();
            this.UpdateState();
            return;
        }
        
        throw new InvalidPropertyDataTypeException();
    }
    
    public override int GetHashCode()
        => this.value?.GetHashCode() ?? 0;

    public override string ToString()
        => value?.ToString() ?? null;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (ReferenceEquals(obj, null))
            return false;

        return this.value.Equals(obj);
    }

    public static bool operator ==(Property<T> prop, T obj)
        => prop.value.Equals(obj);

    public static bool operator !=(Property<T> prop, T obj)
        => !(prop == obj);

    public static bool operator >(Property<T> prop, T obj)
    {
        dynamic value = prop.value;
        dynamic dyn = obj;
        return value > dyn;
    }

    public static bool operator <(Property<T> prop, T obj)
    {
        dynamic value = prop.value;
        dynamic dyn = obj;
        return value < dyn;
    }

    public static bool operator >=(Property<T> prop, T obj)
    {
        dynamic value = prop.value;
        dynamic dyn = obj;
        return value >= dyn;
    }

    public static bool operator <=(Property<T> prop, T obj)
    {
        dynamic value = prop.value;
        dynamic dyn = obj;
        return value <= dyn;
    }

    public static T operator +(Property<T> prop, T obj)
    {
        dynamic value = prop.value;
        dynamic dyn = obj;
        dynamic sum = value + dyn;
        return (T)sum;
    }

    public static T operator -(Property<T> prop, T obj)
    {
        dynamic value = prop.value;
        dynamic dyn = obj;
        dynamic sub = value - dyn;
        return (T)sub;
    }

    public static T operator *(Property<T> prop, T obj)
    {
        dynamic value = prop.value;
        dynamic dyn = obj;
        dynamic mul = value * dyn;
        return (T)mul;
    }

    public static T operator /(Property<T> prop, T obj)
    {
        dynamic value = prop.value;
        dynamic dyn = obj;
        dynamic div = value / dyn;
        return (T)div;
    }

    public static Property<T> operator |(Property<T> prop, T obj)
    {
        prop.tryChange(obj);
        return prop;
    }

    public static implicit operator T(Property<T> prop)
        => prop.value;
}