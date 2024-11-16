using System;
using System.Text;

namespace Cobilas.IO.Atlf;  
/// <summary>Represents an ATLF node.</summary>
public struct ATLFNode : IDisposable {
    /// <summary>The name of the node.</summary>
    public string Name { get; private set; }
    /// <summary>The value of the node.</summary>
    public string Value { get; private set; }
    /// <summary>The type of node.</summary>
    public ATLFNodeType NodeType { get; private set; }

    internal ATLFNode(string name, string value, ATLFNodeType nodeType) {
        Name = name;
        Value = value;
        NodeType = nodeType;
    }
    /// <inheritdoc/>
    public void Dispose() {
        Name =
        Value = string.Empty;
        NodeType = default;
    }
    /// <inheritdoc/>
    public override readonly string ToString() {
        StringBuilder builder = new();
        builder.AppendLine("{");
        builder.AppendLine($"\tname:{Name}");
        builder.AppendLine($"\ttype:{NodeType}");
        builder.AppendLine($"\tvalue:[{Value}]");
        builder.Append("}");
        return builder.ToString();
    }
}