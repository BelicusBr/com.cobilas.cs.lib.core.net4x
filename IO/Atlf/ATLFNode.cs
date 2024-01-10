using System;
using System.Text;

namespace Cobilas.IO.Atlf {
    public struct ATLFNode : IDisposable {
        public string Name { get; private set; }
        public string Value { get; private set; }
        public ATLFNodeType NodeType { get; private set; }

        internal ATLFNode(string name, string value, ATLFNodeType nodeType) {
            Name = name;
            Value = value;
            NodeType = nodeType;
        }

        public void Dispose() {
            Name =
            Value = string.Empty;
            NodeType = default;
        }

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("{");
            builder.AppendLine($"\tname:{Name}");
            builder.AppendLine($"\ttype:{NodeType}");
            builder.AppendLine($"\tvalue:[{Value}]");
            builder.Append("}");
            return builder.ToString();
        }
    }
}