using System;
using System.IO;
using System.Text;
using Cobilas.Collections;
using Cobilas.IO.Atlf.Text;

namespace Cobilas.IO.Atlf {
    public class ATLFTextWriter : ATLFWriter {
        private bool disposedValue;

        public override bool Indent { get; set; }
        public override string IndentChars { get; set;}
        public override Encoding Encoding { get; set; }
        public override string TargetVersion { get; set; }
        public override bool Closed { get; protected set; }
        public override long NodeCount => ArrayManipulation.ArrayLongLength(Nodes);
        protected override ATLFNode[] Nodes { get; set; }
        protected override MarshalByRefObject RefObject { get; set; }
        protected virtual TextWriter Stream { get => (TextWriter)RefObject; set => RefObject = value; }

        ~ATLFTextWriter()
            => Dispose(disposing: false);

        public override void WriteHeader() {
            WriteNode("version", GetATLFEncoding(TargetVersion).Version);
            WriteNode("encoding", (Encoding ?? Encoding.UTF8).BodyName);
        }

        public override void WriteComment(string value) {
            AddNode("cmt", value, ATLFNodeType.Comment);
            WriteIndentation();
        }

        public override void WriteNode(string name, string value) {
            AddNode(name, value, ATLFNodeType.Tag);
            WriteIndentation();
        }

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public override void WriteWhitespace(string spacing)
            => AddNode("wts", spacing, ATLFNodeType.Spacing);

        public override void WriteWhitespace(int count, string spacing) {
            for (int I = 0; I < count; I++)
                WriteWhitespace(spacing);
        }

        public override void Flush() {
            if (Closed)
                throw ATLFException.ATLFFlowAfterClosing();
            if(NodeCount == 0) return;
            Encoding encoding = Encoding ?? Encoding.UTF8;
            Stream.Write(GetATLFEncoding(TargetVersion).Writer4Byte(this.Nodes, encoding));
            ArrayManipulation.ClearArraySafe(Nodes);
            Nodes = null;
        }

        public override void Close() {
            if (Closed)
                throw ATLFException.ATLFClosed();
            Closed = true;
            Stream = null;
            if(NodeCount == 0) return;
            ArrayManipulation.ClearArraySafe(Nodes);
            Nodes = null;
        }

        protected override ATLFEncoding GetATLFEncoding(string targetVersion) {
            if (EncodingsCollection.ContainsEncoding(targetVersion))
                return EncodingsCollection.GetEncoding(targetVersion);
            return EncodingsCollection.GetEncoding(def_version);
        }

        protected void WriteIndentation() {
            if (Indent) {
                IndentChars = IndentChars ?? "\r\n";
                WriteWhitespace(IndentChars);
            }
        }

        protected override void AddNode(string name, string value, ATLFNodeType nodeType) {
            if (Closed)
                throw ATLFException.ATLFTagsAfterClosing();
            Nodes = ArrayManipulation.Add(new ATLFNode(name, value, nodeType), Nodes);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Flush();
                    Close();
                }
                disposedValue = true;
            }
        }
    }
}