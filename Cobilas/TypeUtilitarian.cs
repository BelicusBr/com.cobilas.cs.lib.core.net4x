using System;
using System.Reflection;
using Cobilas.Collections;

namespace Cobilas; 
/// <summary>Utility static class to obtain type or assembly.</summary>
public static class TypeUtilitarian {

    /// <summary>Get all types of assembly.</summary>
    /// <returns>Returns all types of objects from all assemblies.</returns>
    public static Type[] GetTypes() {
        Type[] types = [];
        Assembly[] assemblies = GetAssemblies();
        foreach (var item in assemblies)
            ArrayManipulation.Add(item.GetTypes(), ref types!);
        return types;
    }

    /// <summary>Checks if the type exists.</summary>
    /// <param name="fullName">The full name of the type. (example:<c>System.String</c>)</param>
    /// <returns>Returns <c>true</c> when the type exists.</returns>
    public static bool TypeExist(string fullName) {
        foreach (var item in GetTypes())
            if (item.Name == fullName)
                return true;
        return false;
    }

    /// <summary>Get a specific type.</summary>
    /// <param name="fullName">The full name of the type. (example:<c>System.String</c>)</param>
    /// <returns>Returns the type that was specified in the parameter.
    /// <para>If the specified type is not found had returned the <seealso cref="NullObject"/> type.</para>
    /// </returns>
    public static Type GetType(string fullName) {
        foreach (Type item in GetTypes())
            if (item.FullName == fullName)
                return item;
        return NullObject.Null.GetType();
    }

    /// <summary>Get all assemblies for the current domain.</summary>
    /// <returns>Returns a list all assemblies of the domain.</returns>
    public static Assembly[] GetAssemblies()
        => AppDomain.CurrentDomain.GetAssemblies();
}
