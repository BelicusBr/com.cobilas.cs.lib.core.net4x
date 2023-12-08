using System;

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
            Value = null;
            NodeType = default;
        }
    }
}