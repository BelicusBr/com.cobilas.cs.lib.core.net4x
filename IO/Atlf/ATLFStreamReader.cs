using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cobilas.Collections;
using Cobilas.IO.Atlf.Text;

namespace Cobilas.IO.Atlf {
    public class ATLFStreamReader : ATLFSBReader {
        private bool disposedValue;

        public override long NodeCount => ArrayManipulation.ArrayLongLength(Nodes);

        public override bool Indent { get; set; }
        public override Encoding Encoding { get; set; } = Encoding.UTF8;
        public override string TargetVersion { get; set; } = string.Empty;
        public override bool Closed { get; protected set; }
        protected override bool CloseFlow { get; set; }
        protected override ATLFNode[] Nodes { get; set; } = Array.Empty<ATLFNode>();
        protected override MarshalByRefObject RefObject { get; set; } = Stream.Null;
        protected override Stream Stream { get => (Stream)RefObject; set => RefObject = value; }

        ~ATLFStreamReader()
            => Dispose(disposing: false);

        public override void Close() {
            if (Closed)
                throw ATLFException.ATLFClosed();
            Closed = true;
            Nodes = Array.Empty<ATLFNode>();
            Encoding = default!;
            if (CloseFlow) Stream.Close();
            else  Stream = Stream.Null;
        }

        public override void Reader() {
            if (Closed)
                throw ATLFException.ATLFReaderAfterClosing();
            Encoding encoding = Encoding ?? Encoding.UTF8;
            ATLFDecoding decoding = GetATLFDecoding(TargetVersion);
            Nodes = decoding.Reader4Byte(Stream.Read(), encoding);
        }

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public override ATLFNode[] GetHeader() {
            if (Closed)
                throw ATLFException.ATLFReaderTagAfterClosing();
            ATLFNode[] res = new ATLFNode[0];
            foreach (var item in Nodes)
                ArrayManipulation.Add(item, ref res);
            return res;
        }

        public override string GetTag(string name) {
            if (Closed)
                throw ATLFException.ATLFReaderTagAfterClosing();
            foreach (var item in Nodes)
                if (item.Name == name && item.NodeType == ATLFNodeType.Tag)
                    return item.Value;
            return string.Empty;
        }

        public override ATLFNode[] GetAllComments() {
            if (Closed)
                throw ATLFException.ATLFReaderTagAfterClosing();
            ATLFNode[] res = Array.Empty<ATLFNode>();
            foreach (var item in Nodes)
                if (item.NodeType == ATLFNodeType.Comment)
                    ArrayManipulation.Add(item, ref res);
            return res;
        }

        public override ATLFNode[] GetTagGroup(string path) {
            if (Closed)
                throw ATLFException.ATLFReaderTagAfterClosing();
            ATLFNode[] res = Array.Empty<ATLFNode>();
            foreach (var item in Nodes)
                if (item.Name.Contains(path) && item.NodeType == ATLFNodeType.Tag)
                    ArrayManipulation.Add(item, ref res);
            return res;
        }

        public override IEnumerator<ATLFNode> GetEnumerator()
            => new ArrayToIEnumerator<ATLFNode>(Nodes);

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Close();
                    if (CloseFlow) Stream.Dispose();
                }
                disposedValue = true;
            }
        }

        protected override ATLFDecoding GetATLFDecoding(string targetVersion) {
            if (EncodingsCollection.ContainsDecoding(targetVersion))
                return EncodingsCollection.GetDecoding(targetVersion);
            return EncodingsCollection.GetDecoding(def_version);
        }
    }
}