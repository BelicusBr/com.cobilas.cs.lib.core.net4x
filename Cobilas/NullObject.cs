using System;
using System.Collections.Generic;

namespace Cobilas;

/// <summary>This class represents a null object.</summary>
public readonly struct NullObject : INullObject {
    private static Dictionary<Type, object> nulls = [];
    private static readonly NullObject nullObject = new();
    /// <summary>Obtain a null representation of the object</summary>
    /// <returns>Retorna uma representação nulo do objeto</returns>
    public static NullObject Null => nullObject;
    /// <summary>Obtain a null representation of the object that inherits the <seealso cref="INullObject"/> interface.</summary>
    /// <param name="target">The type that will be used to obtain a null representation.</param>
    /// <param name="ignoreINullObject">It causes the method to return objects that do not inherit <seealso cref="INullObject"/>.</param>
    /// <exception cref="InvalidCastException">It occurs when the <c>target</c> type does not inherit the <seealso cref="INullObject"/> interface.</exception>
    /// <exception cref="ArgumentNullException">It occurs when the <c>target</c> parameter is null.</exception>
    /// <returns>Returns a null representation of the object that inherits the <seealso cref="INullObject"/> interface.</returns>
    public static object GetNullObject(Type? target, bool ignoreINullObject = false) {
#if NET7_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(target, nameof(target));
#else
        if (target is null)
            throw new ArgumentNullException(nameof(target));
#endif
        if (!typeof(INullObject).IsAssignableFrom(target) && !ignoreINullObject)
            throw new InvalidCastException($"Object {target} does not inherit the {nameof(INullObject)} interface.");
        if (target == typeof(NullObject)) return nullObject;
        if (nulls.TryGetValue(target, out object? result))
            return result;
        nulls.Add(target, result = target.Activator());
        return result;
    }
    /// <inheritdoc cref="GetNullObject(Type, bool)"/>
    /// <typeparam name="T">The type that will be used to obtain a null representation.</typeparam>
    public static T GetNullObject<T>(bool ignoreINullObject = false)
        => (T)GetNullObject(typeof(T), ignoreINullObject);
}
