using System;
using Cobilas.IO.Atlf.Components;
using Cobilas.IO.Atlf.Exceptions;
using System.Runtime.Serialization;

namespace Cobilas.IO.Atlf;
/// <summary>Class to generate ATLF exceptions.</summary>
public class ATLFException : Exception {
	/// <inheritdoc/>
	public ATLFException() : base() { }
	/// <inheritdoc/>
	public ATLFException(string message) : base(message) { }
	/// <inheritdoc/>
	public ATLFException(string message, Exception inner) : base(message, inner) { }
	/// <inheritdoc/>
#if NET8_0_OR_GREATER
    [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051")]
#endif
	protected ATLFException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	/// <inheritdoc/>
#if NET8_0_OR_GREATER
    [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051")]
#endif
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
	}
	/// <summary>Builds an <see cref="ATLFException"/> with a formatted message.</summary>
	/// <param name="format">The format string for the exception message.</param>
	/// <param name="args">Arguments to format into the message.</param>
	/// <returns>A new instance of <see cref="ATLFException"/>.</returns>
	public static ATLFException GetATLFException(string format, params object[] args)
		=> new(string.Format(format, args));
	/// <summary>Creates an <see cref="InvalidOperationException"/> indicating the ATLF object is closed.</summary>
	/// <returns>An <see cref="InvalidOperationException"/> with a descriptive message.</returns>
	public static InvalidOperationException ATLFClosed()
		=> new("The ATLF object has already been closed.");
	/// <summary>Creates an <see cref="InvalidOperationException"/> for read operations after closure.</summary>
	/// <returns>An <see cref="InvalidOperationException"/> with a descriptive message.</returns>
	public static InvalidOperationException ATLFReaderAfterClosing()
		=> new("The read operation cannot be done after closing the ATLF object!");
	/// <summary>Creates an <see cref="InvalidOperationException"/> for tag read operations after closure.</summary>
	/// <returns>An <see cref="InvalidOperationException"/> with a descriptive message.</returns>
	public static InvalidOperationException ATLFReaderTagAfterClosing()
		=> new("The tag reading operation cannot be done after closing the ATLF object!");
	/// <summary>Creates an <see cref="InvalidOperationException"/> for resource release after closure.</summary>
	/// <returns>An <see cref="InvalidOperationException"/> with a descriptive message.</returns>
	public static InvalidOperationException ATLFFlowAfterClosing()
		=> new("It is not possible to release resources to the flow after closing the ATLF object.");
	/// <summary>Creates an <see cref="InvalidOperationException"/> for adding tags after closure.</summary>
	/// <returns>An <see cref="InvalidOperationException"/> with a descriptive message.</returns>
	public static InvalidOperationException ATLFTagsAfterClosing()
		=> new("You cannot add new tags to the stream after closing the ATLF object.");
	/// <summary>Creates an <see cref="ATLFException"/> for an unopened comment block.</summary>
	/// <param name="lec">The line and column position where the error occurred.</param>
	/// <returns>An <see cref="ATLFException"/> with an inner <see cref="ATLFCommentException"/>.</returns>
	public static ATLFException CommentNotOpen(CharacterCursor.LineEndColumn lec)
		=> new("Comment not open", new ATLFCommentException(
			$"The comment block '{lec}' was not initialized correctly!"
		));
	/// <summary>Creates an <see cref="ATLFException"/> for an unclosed comment block.</summary>
	/// <param name="lec">The line and column position where the error occurred.</param>
	/// <returns>An <see cref="ATLFException"/> with an inner <see cref="ATLFCommentException"/>.</returns>
	public static ATLFException CommentNotClosed(CharacterCursor.LineEndColumn lec)
		=> new("Comment not closed", new ATLFCommentException(
			$"The comment block '{lec}' was not properly terminated!"
		));
	/// <summary>Creates an <see cref="ATLFException"/> for an unopened identification block.</summary>
	/// <param name="lec">The line and column position where the error occurred.</param>
	/// <returns>An <see cref="ATLFException"/> with an inner <see cref="ATLFCommentException"/>.</returns>
	public static ATLFException IdentificationBlockNotOpen(CharacterCursor.LineEndColumn lec)
		=> new("Identification block not open", new ATLFCommentException(
			$"The tag identifier block '{lec}' was not initialized correctly!"
		));
	/// <summary>Creates an <see cref="ATLFException"/> for an unclosed identification block.</summary>
	/// <param name="lec">The line and column position where the error occurred.</param>
	/// <returns>An <see cref="ATLFException"/> with an inner <see cref="ATLFCommentException"/>.</returns>
	public static ATLFException IdentificationBlockNotClosed(CharacterCursor.LineEndColumn lec)
		=> new("Identification block not closed", new ATLFCommentException(
			$"The tag identification block '{lec}' was not terminated correctly!"
		));
	/// <summary>Creates an <see cref="ATLFException"/> for an unopened text block within a tag.</summary>
	/// <param name="lec">The line and column position where the error occurred.</param>
	/// <returns>An <see cref="ATLFException"/> with an inner <see cref="ATLFCommentException"/>.</returns>
	public static ATLFException TextBlockNotOpen(CharacterCursor.LineEndColumn lec)
		=> new("Text block not open", new ATLFCommentException(
			$"The text block within the tag '{lec}' has not been initialized correctly!"
		));
	/// <summary>Creates an <see cref="ATLFException"/> for an unclosed text block within a tag.</summary>
	/// <param name="lec">The line and column position where the error occurred.</param>
	/// <returns>An <see cref="ATLFException"/> with an inner <see cref="ATLFCommentException"/>.</returns>
	public static ATLFException TextBlockNotClosed(CharacterCursor.LineEndColumn lec)
		=> new("Text block not closed", new ATLFCommentException(
			$"The text block within the tag '{lec}' was not terminated correctly!"
		));
	/// <summary>Creates an <see cref="ATLFException"/> for an unclosed tag block.</summary>
	/// <param name="lec">The line and column position where the error occurred.</param>
	/// <returns>An <see cref="ATLFException"/> with an inner <see cref="ATLFCommentException"/>.</returns>
	public static ATLFException MarkNotClosed(CharacterCursor.LineEndColumn lec)
		=> new("Mark not closed", new ATLFCommentException(
			$"The tag block '{lec}' was not finalized correctly!"
		));
	/// <summary>Creates an <see cref="ATLFException"/> for an invalid character at a specific position.</summary>
	/// <param name="c">The invalid character encountered.</param>
	/// <param name="lec">The line and column position where the error occurred.</param>
	/// <returns>An <see cref="ATLFException"/> with a descriptive message.</returns>
	/// <remarks>The character is displayed using its escape sequence representation for safety.</remarks>
	public static ATLFException InvalidCharacter(char c, CharacterCursor.LineEndColumn lec)
		=> new($"{lec}The character '{c.EscapeSequenceToString()}' is not valid!");
}