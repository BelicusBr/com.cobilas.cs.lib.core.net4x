using Cobilas.Collections;

namespace System;
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
public static class Object_CB_Extension {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    
    /// <summary>
    /// Compares the object's type to a specified type.
    /// </summary>
    /// <param name="O">The target object of comparison.</param>
    /// <param name="type">The specified comparison type.</param>
    public static bool CompareType(this object O, Type type)
        => (O is Type tres ? tres : O.GetType()) == type;

    /// <summary>
    /// Compares the object's type to a specified list of types.
    /// </summary>
    /// <param name="O">The target object of comparison.</param>
    /// <param name="types">The specified comparison types.</param>
    public static bool CompareType(this object O, params Type[] types) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(types); I++)
            if (CompareType(O, types[I])) 
                return true;
        return false;
    }

    /// <summary>
    /// Compares the object's type with a specified generic type.
    /// </summary>
    /// <param name="O">The target object of comparison.</param>
    /// <typeparam name="T">The generic comparison type specified.</typeparam>
    public static bool CompareType<T>(this object O)
        => CompareType(O, typeof(T));

    /// <summary>Compares class type and the class it inherits.</summary>
    public static bool CompareTypeAndSubType(this object O, Type type, bool IncludeInterface)
        => CompareType(O, type) || IsSubclassOf(O, type) ||
        (IncludeInterface && IsAssignableFrom(O, type));

    /// <summary>Compares class type and the class it inherits.</summary>
    public static bool CompareTypeAndSubType(this object O, Type type)
        => CompareTypeAndSubType(O, type, false);

    /// <summary>Compares class type and the class it inherits.</summary>
    public static bool CompareTypeAndSubType<T>(this object O, bool IncludeInterface)
        => CompareTypeAndSubType(O, typeof(T), IncludeInterface);

    /// <summary>Compares class type and the class it inherits.</summary>
    public static bool CompareTypeAndSubType<T>(this object O)
        => CompareTypeAndSubType<T>(O, false);

    private static bool IsSubclassOf(object O, Type type)
        => (O is Type tp ? tp : O.GetType()).IsSubclassOf(type);

    private static bool IsAssignableFrom(object O, Type type)
        => (O is Type tp ? tp : O.GetType()).IsAssignableFrom(type);
}
