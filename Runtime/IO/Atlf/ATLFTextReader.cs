using System;
using System.IO;
using System.Text;
using Cobilas.Collections;
using Cobilas.IO.Atlf.Text;
using System.Collections.Generic;

namespace Cobilas.IO.Atlf;
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
public class ATLFTextReader : ATLFTBReader {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
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
    protected override ATLFNode[] Nodes { get; set; } = Array.Empty<ATLFNode>();
    /// <inheritdoc/>
    protected override MarshalByRefObject RefObject { get; set; } = TextReader.Null;
    /// <inheritdoc/>
    protected override TextReader Stream { get => (TextReader)RefObject; set => RefObject = value; }

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    ~ATLFTextReader()
        => Dispose(disposing: false);
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

    /// <inheritdoc/>
    public override void Close() {
        if (Closed)
            throw ATLFException.ATLFClosed();
        Closed = true;
        Nodes = Array.Empty<ATLFNode>();
        Encoding = default!;
        if (CloseFlow) Stream.Close();
        else Stream = TextReader.Null;
    }
    
    /// <inheritdoc/>
    public override void Reader() {
        if (Closed)
            throw ATLFException.ATLFReaderAfterClosing();
        ATLFDecoding decoding = GetATLFDecoding(TargetVersion);
        Nodes = decoding.Reader(Stream.ReadToEnd());
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
        ATLFNode[] res = Array.Empty<ATLFNode>();
        foreach (var item in Nodes)
            if (item.NodeType == ATLFNodeType.Comment)
                ArrayManipulation.Add(item, ref res);
        return res;
    }
    
    /// <inheritdoc/>
    public override ATLFNode[] GetTagGroup(string path) {
        if (Closed)
            throw ATLFException.ATLFReaderTagAfterClosing();
        ATLFNode[] res = Array.Empty<ATLFNode>();
        foreach (var item in Nodes)
            if (item.Name.Contains(path) && item.NodeType == ATLFNodeType.Tag)
                ArrayManipulation.Add(item, ref res);
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