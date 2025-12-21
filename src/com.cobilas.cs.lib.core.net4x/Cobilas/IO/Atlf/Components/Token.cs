namespace Cobilas.IO.Atlf.Components;
/// <summary>Represents a lexical token in the ATLF format with its type, position, and optional value.</summary>
/// <remarks>
/// This structure is immutable and holds information about a token parsed from ATLF source code,
/// including its type, location in the source file, and optional textual value.
/// </remarks>
/// <param name="type">The <see cref="TokenType"/> of the token.</param>
/// <param name="lec">The line and column position where the token was found.</param>
/// <param name="value">The optional textual value of the token (e.g., identifier name, string content).</param>
public readonly struct Token(TokenType type, CharacterCursor.LineEndColumn lec, string? value = null) {
	/// <summary>Gets the optional textual value of the token.</summary>
	/// <returns>The string value of the token, or <see langword="null"/> if the token has no associated value.</returns>
	public readonly string? Value { get; } = value;
	/// <summary>Gets the type of the token.</summary>
	/// <returns>A <see cref="TokenType"/> value indicating the category of this token.</returns>
	public readonly TokenType Type { get; } = type;
	/// <summary>Gets the position of the token in the source file.</summary>
	/// <returns>A <see cref="CharacterCursor.LineEndColumn"/> structure containing line and column information.</returns>
	public readonly CharacterCursor.LineEndColumn LineEndColumn { get; } = lec;
	/// <inheritdoc/>
	public override string ToString() =>
		Value == null ? $"{Type} {LineEndColumn}" : $"{Type} {LineEndColumn}({Value})";
}