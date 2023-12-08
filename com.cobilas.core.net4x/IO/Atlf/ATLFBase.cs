using System;
using System.IO;
using System.Text;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFBase : IDisposable {
        public abstract long NodeCount { get; }
        public abstract bool Indent { get; set; }
        public abstract Encoding Encoding { get; set; }
        public abstract bool Closed { get; protected set; }
        protected abstract ATLFNode[] Nodes { get; set; }
        protected abstract MarshalByRefObject RefObject { get; set; }

        internal static string _version = "1.0";

        public abstract void Flush();
        public abstract void Close();
        public abstract void Dispose();

        internal static string Writer(ATLFWriter writer) {
            StringBuilder builder = new StringBuilder();
            foreach (var item in writer.Nodes) {
                switch (item.NodeType) {
                    case ATLFNodeType.Tag:
                        builder.AppendFormat("#! {0}:/*{1}*/", item.Name, item.Value);
                        break;
                    case ATLFNodeType.Spacing:
                        builder.Append(item.Value);
                        break;
                    case ATLFNodeType.Comment:
                        builder.AppendFormat("#> {0} <#", item.Value);
                        break;
                }
            }
            return builder.ToString();
        }
    }
}