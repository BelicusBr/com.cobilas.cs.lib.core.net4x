using System;
using System.IO;
using System.Text;
using Cobilas.Collections;
using Cobilas.IO.Atlf.Text;
using System.Collections.Generic;

namespace Cobilas.IO.Atlf;
/// <summary>This class allows you to read a stream in ATLF format.</summary>
public class ATLFStreamReader : ATLFSBReader {
    private bool disposedValue;

    /// <inheritdoc/>
    public override long NodeCount => ArrayManipulation.ArrayLongLength(Nodes);

    /// <inheritdoc/>
    public override bool Indent { get; set; }
    /// <inheritdoc/>
    public override Encoding Encoding { get; set; } = Encoding.UTF8;
    /// <inheritdoc/>
    public override string TargetVersion { get; set; } = string.Empty;
    /// <inheritdoc/>
    public override bool Closed { get; protected set; }
    /// <inheritdoc/>
    protected override bool CloseFlow { get; set; }
    /// <inheritdoc/>
    protected override ATLFNode[] Nodes { get; set; } = [];
    /// <inheritdoc/>
    protected override MarshalByRefObject RefObject { get; set; } = Stream.Null;
    /// <inheritdoc/>
    protected override Stream Stream { get => (Stream)RefObject; set => RefObject = value; }

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    ~ATLFStreamReader()
        => Dispose(disposing: false);
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

    /// <inheritdoc/>
    public override void Close() {
        if (Closed)
            throw ATLFException.ATLFClosed();
        Closed = true;
        Nodes = [];
        Encoding = Encoding.Default;
        if (CloseFlow) Stream.Close();
        else  Stream = Stream.Null;
    }

    /// <inheritdoc/>
    public override void Reader() {
        if (Closed)
            throw ATLFException.ATLFReaderAfterClosing();
        Encoding encoding = Encoding ?? Encoding.UTF8;
        ATLFDecoding decoding = GetATLFDecoding(TargetVersion);
        Nodes = decoding.Reader4Byte(Stream.Read(), encoding);
    }

    /// <inheritdoc/>
    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public override ATLFNode[] GetHeader() {
        if (Closed)
            throw ATLFException.ATLFReaderTagAfterClosing();
        ATLFNode[] res = new ATLFNode[2];
        int index = 0;
        foreach (var item in Nodes)
            if (item.Name == "version" || item.Name == "encoding") {
                res[index++] = item;
                if (index >= 2) break;
            }
        return res;
    }

    /// <inheritdoc/>
    public override string GetTag(string name) {
        if (Closed)
            throw ATLFException.ATLFReaderTagAfterClosing();
        foreach (var item in Nodes)
            if (item.Name == name && item.NodeType == ATLFNodeType.Tag)
                return item.Value;
        return string.Empty;
    }

    /// <inheritdoc/>
    public override ATLFNode[] GetAllComments() {
        if (Closed)
            throw ATLFException.ATLFReaderTagAfterClosing();
        ATLFNode[] res = [];
        foreach (var item in Nodes)
            if (item.NodeType == ATLFNodeType.Comment)
                ArrayManipulation.Add(item, ref res!);
        return res;
    }

    /// <inheritdoc/>
    public override ATLFNode[] GetTagGroup(string path) {
        if (Closed)
            throw ATLFException.ATLFReaderTagAfterClosing();
        ATLFNode[] res = [];
        foreach (var item in Nodes)
            if (item.Name.Contains(path) && item.NodeType == ATLFNodeType.Tag)
                ArrayManipulation.Add(item, ref res!);
        return res;
    }

    /// <inheritdoc/>
    public override IEnumerator<ATLFNode> GetEnumerator()
        => new ArrayToIEnumerator<ATLFNode>(Nodes);

    /// <summary>Performs an internal disposal of the object.</summary>
    protected virtual void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                Close();
                if (CloseFlow) Stream.Dispose();
            }
            disposedValue = true;
        }
    }
/// <inheritdoc/>

    protected override ATLFDecoding GetATLFDecoding(string targetVersion) {
        if (EncodingsCollection.ContainsDecoding(targetVersion))
            return EncodingsCollection.GetDecoding(targetVersion);
        return EncodingsCollection.GetDecoding(def_version);
    }
}