using System;
using System.Runtime.Serialization;

namespace Cobilas.IO.Atlf;
/// <summary>Class to generate ATLF exceptions.</summary>
[Serializable]
public class ATLFException : Exception {
    /// <inheritdoc/>
    public ATLFException() : base() { }
    /// <inheritdoc/>
    public ATLFException(string message) : base(message) { }
    /// <inheritdoc/>
    public ATLFException(string message, Exception inner) : base(message, inner) { }
    /// <inheritdoc/>
    protected ATLFException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    /// <summary>I build an exception <seealso cref="ATLFException"/>.</summary>
    public static ATLFException GetATLFException(string format, params object[] args)
        => new(string.Format(format, args));

    /// <summary>msg:The ATLFWriter object has already been closed.</summary>
    public static InvalidOperationException ATLFClosed()
        => new("The ATLF object has already been closed.");
    /// <summary>msg:The read operation cannot be done after closing the ATLF object!</summary>
    public static InvalidOperationException ATLFReaderAfterClosing()
        => new("The read operation cannot be done after closing the ATLF object!");
    /// <summary>msg:The tag reading operation cannot be done after closing the ATLF object!</summary>
    public static InvalidOperationException ATLFReaderTagAfterClosing()
        => new("The tag reading operation cannot be done after closing the ATLF object!");
    /// <summary>msg:It is not possible to release resources to the flow after closing the ATLF object.</summary>
    public static InvalidOperationException ATLFFlowAfterClosing()
        => new("It is not possible to release resources to the flow after closing the ATLF object.");
    /// <summary>msg:You cannot add new tags to the stream after closing the ATLF object.</summary>
    public static InvalidOperationException ATLFTagsAfterClosing()
        => new("You cannot add new tags to the stream after closing the ATLF object.");
}