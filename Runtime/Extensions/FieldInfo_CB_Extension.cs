namespace System.Reflection;
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
public static class FieldInfo_CB_Extension {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    /// <summary>
    /// Determines whether the field is a background field used by automatic properties.
    /// </summary>
    /// <param name="F">Target field.</param>
    /// <returns>Returns <c>true</c> when it is a background field and <c>false</c> when it is not a background field.</returns>
    public static bool IsBackingField(this FieldInfo F)
        => F.Name.Contains(">k__BackingField");
}
