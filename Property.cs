/* Author:  Leonardo Trevisan Silio
 * Date:    07/08/2023
 */
namespace Stately;

using Exceptions;

/// <summary>
/// A Property to use in States. Property is a State.
/// </summary>
public class Property<T>
{
    public T Value
    {
        get => this.value;
        set => tryChange(value);
    }

    private T value;
    private State state;

    public Property(State state)
    {
        this.state = state;
        this.value = default(T);
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
            this.state.OnChanged();
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
    
    public static Property<T> operator <<(Property<T> prop, T obj)
    {
        prop.Value = obj;
        return prop;
    }

    public static implicit operator T(Property<T> prop)
        => prop.value;
}