namespace System; 
public static class Type_CB_Extension {
    public static T GetAttribute<T>(this Type type) where T : Attribute
        => GetAttribute<T>(type, true);

    public static T GetAttribute<T>(this Type type, bool inherit) where T : Attribute {
        T[] types = GetAttributes<T>(type, inherit);
        if (types[0] is not null)
            return types[0];
        return default!;
    }

    public static T[] GetAttributes<T>(this Type type) where T : Attribute
        => GetAttributes<T>(type, true);

    public static T[] GetAttributes<T>(this Type type, bool inherit) where T : Attribute
        => (T[])type.GetCustomAttributes(typeof(T), inherit);

    public static object Activator(this Type type)
        => System.Activator.CreateInstance(type);

    public static T Activator<T>(this Type type)
        => (T)Activator(type);
}
