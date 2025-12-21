using System;
using System.Runtime.Serialization;

namespace Cobilas.IO.Atlf.Exceptions;
/// <summary>Represents errors that occur during ATLF comment block processing.</summary>
/// <remarks>
/// This exception is thrown when there are issues with ATLF comment parsing or validation,
/// such as unopened or unclosed comment blocks, or malformed comment syntax.
/// </remarks>
[Serializable]
public class ATLFCommentException : Exception {
	/// <summary>Initializes a new instance of the <see cref="ATLFCommentException"/> class.</summary>
	public ATLFCommentException() { }
	/// <inheritdoc/>
	public ATLFCommentException(string message) : base(message) { }
	/// <inheritdoc/>
	public ATLFCommentException(string message, Exception inner) : base(message, inner) { }
	/// <inheritdoc/>
	/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data.</param>
	/// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
#if NET8_0_OR_GREATER
	[Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051")]
#endif
	protected ATLFCommentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	/// <inheritdoc/>
#if NET8_0_OR_GREATER
	[Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051")]
#endif
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
	}
}