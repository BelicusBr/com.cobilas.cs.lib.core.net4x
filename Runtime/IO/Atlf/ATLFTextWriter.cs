using System;
using System.IO;
using System.Text;
using Cobilas.Collections;
using Cobilas.IO.Atlf.Text;

namespace Cobilas.IO.Atlf { 
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public class ATLFTextWriter : ATLFTBWriter {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        private bool disposedValue;

        /// <inheritdoc/>
        public override bool Indent { get; set; }
        /// <inheritdoc/>
        public override string IndentChars { get; set;} = "\r\n";
        /// <inheritdoc/>
        public override Encoding Encoding { get; set; } = Encoding.UTF8;
        /// <inheritdoc/>
        /// <value>By default the value is <see cref="string"/>.Empty which represents the current version.</value>
        public override string TargetVersion { get; set; } = string.Empty;
        /// <inheritdoc/>
        public override bool Closed { get; protected set; }
        /// <inheritdoc/>
        protected override bool CloseFlow { get; set; }
        /// <inheritdoc/>
        protected override MarshalByRefObject RefObject { get; set; } = default;
        /// <inheritdoc/>
        /// <value>Returns the number of nodes already written.</value>
        public override long NodeCount => ArrayManipulation.ArrayLongLength(Nodes);
        /// <inheritdoc/>
        protected override ATLFNode[] Nodes { get; set; } = Array.Empty<ATLFNode>();
        /// <inheritdoc/>
        protected override TextWriter Stream { get => (TextWriter)RefObject; set => RefObject = value; }

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        ~ATLFTextWriter()
            => Dispose(disposing: false);
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

        /// <inheritdoc/>
        public override void WriteHeader() {
            if (Closed)
                throw ATLFException.ATLFTagsAfterClosing();
            AddNode("version", GetATLFEncoding(TargetVersion).Version, ATLFNodeType.Tag);
            WriteIndentation();
            AddNode("encoding", (Encoding ?? Encoding.UTF8).BodyName, ATLFNodeType.Tag);
            WriteIndentation();
        }

        /// <inheritdoc/>
        public override void WriteComment(string value) {
            AddNode("cmt", value, ATLFNodeType.Comment);
            WriteIndentation();
        }
    
        /// <inheritdoc/>
        public override void WriteNode(string name, string value) {
            AddNode(name, value, ATLFNodeType.Tag);
            WriteIndentation();
        }
    
        /// <inheritdoc/>
        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    
        /// <inheritdoc/>
        public override void WriteWhitespace(string spacing)
            => AddNode("wts", spacing, ATLFNodeType.Spacing);
    
        /// <inheritdoc/>
        public override void WriteWhitespace(int count, string spacing) {
            for (int I = 0; I < count; I++)
                WriteWhitespace(spacing);
        }
    
        /// <inheritdoc/>
        public override void Flush() {
            if (Closed)
                throw ATLFException.ATLFFlowAfterClosing();
            if(NodeCount == 0) return;
            Encoding encoding = Encoding ?? Encoding.UTF8;
            Stream.Write(GetATLFEncoding(TargetVersion).Writer(this.Nodes));
            ArrayManipulation.ClearArraySafe(Nodes);
            Nodes = Array.Empty<ATLFNode>();
        }
    
        /// <inheritdoc/>
        public override void Close() {
            if (Closed)
                throw ATLFException.ATLFClosed();
            Closed = true;
            if (CloseFlow) Stream.Close();
            else  Stream = TextWriter.Null;
            if(NodeCount == 0) return;
            ArrayManipulation.ClearArraySafe(Nodes);
            Nodes = Array.Empty<ATLFNode>();
        }
    
        /// <inheritdoc/>
        protected override ATLFEncoding GetATLFEncoding(string targetVersion) {
            if (EncodingsCollection.ContainsEncoding(targetVersion))
                return EncodingsCollection.GetEncoding(targetVersion);
            return EncodingsCollection.GetEncoding(def_version);
        }
    
        /// <summary>Performs automatic indentation.</summary>
        protected void WriteIndentation() {
            if (Indent) {
                IndentChars = string.IsNullOrEmpty(IndentChars) ? "\r\n" : IndentChars;
                WriteWhitespace(IndentChars);
            }
        }
    
        /// <inheritdoc/>
        protected override void AddNode(string name, string value, ATLFNodeType nodeType) {
            if (Closed)
                throw ATLFException.ATLFTagsAfterClosing();
            Nodes = ArrayManipulation.Add(new ATLFNode(name, value, nodeType), Nodes);
        }
    
        /// <summary>Performs an internal disposal of the object.</summary>
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Flush();
                    Close();
                    if (CloseFlow) Stream.Dispose();
                }
                disposedValue = true;
            }
        }
    }
}