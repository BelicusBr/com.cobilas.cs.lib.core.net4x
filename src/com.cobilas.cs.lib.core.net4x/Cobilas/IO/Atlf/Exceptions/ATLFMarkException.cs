using System;
using System.Runtime.Serialization;

namespace Cobilas.IO.Atlf.Exceptions;
/// <summary>Represents errors that occur during ATLF mark (tag) processing.</summary>
/// <remarks>
/// This exception is thrown when there are issues with ATLF tag parsing or validation,
/// such as malformed tags, missing required components, or invalid tag structures.
/// </remarks>
[Serializable]
public class ATLFMarkException : Exception {
	/// <summary>Initializes a new instance of the <see cref="ATLFMarkException"/> class.</summary>
	public ATLFMarkException() { }
	/// <inheritdoc/>
	public ATLFMarkException(string message) : base(message) { }
	/// <inheritdoc/>
	public ATLFMarkException(string message, Exception inner) : base(message, inner) { }
	/// <inheritdoc/>
#if NET8_0_OR_GREATER
    [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051")]
#endif
	protected ATLFMarkException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	/// <inheritdoc/>
#if NET8_0_OR_GREATER
    [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051")]
#endif
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
	}
}