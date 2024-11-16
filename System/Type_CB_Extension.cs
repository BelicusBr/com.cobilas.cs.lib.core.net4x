namespace System; 
/// <summary>Consecrate more methods for <seealso cref="Type"/> class.</summary>
public static class Type_CB_Extension {

    /// <summary>
    /// Gets the type attribute.
    /// </summary>
    /// <typeparam name="T">The generic type used to obtain the target attribute.</typeparam>
    public static T GetAttribute<T>(this Type type) where T : Attribute
        => GetAttribute<T>(type, true);

    /// <summary>
    /// Gets the type attribute.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="inherit">true to get attributes marked as inherited.</param>
    /// <typeparam name="T">The generic type used to obtain the target attribute.</typeparam>
    public static T GetAttribute<T>(this Type type, bool inherit) where T : Attribute {
        T[] types = GetAttributes<T>(type, inherit);
        if (types is not null && types.LongLength != 0 && types[0] is not null)
            return types[0];
        return default!;
    }

    /// <summary>
    /// Gets all attributes of the type.
    /// </summary>
    /// <typeparam name="T">The generic type used to obtain the target attribute.</typeparam>
    public static T[] GetAttributes<T>(this Type type) where T : Attribute
        => GetAttributes<T>(type, true);

    /// <summary>
    /// Gets all attributes of the type.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="inherit">true to get attributes marked as inherited.</param>
    /// <typeparam name="T">The generic type used to obtain the target attribute.</typeparam>
    public static T[] GetAttributes<T>(this Type type, bool inherit) where T : Attribute
        => (T[])type.GetCustomAttributes(typeof(T), inherit);

    /// <summary>
    /// Creates an instance of an object from a type.
    /// </summary>
    public static object Activator(this Type type)
        => System.Activator.CreateInstance(type) ?? Cobilas.NullObject.Null;

    /// <summary>
    /// Creates an instance of an object from a type.
    /// </summary>
    public static T Activator<T>(this Type type)
        => (T)Activator(type);
}
