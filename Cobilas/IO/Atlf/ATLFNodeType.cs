namespace Cobilas.IO.Atlf; 
/// <summary>Represents the ATLF node type.</summary>
public enum ATLFNodeType : byte {
    /// <summary>ATLF Comment.</summary>
    Comment = 0,
    /// <summary>ATLF element.</summary>
    Tag = 1,
    /// <summary>The spacing used in the ATLF file.</summary>
    Spacing = 2
}