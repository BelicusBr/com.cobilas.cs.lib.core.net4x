using System;
using System.Text;
using Cobilas.Collections;
using Cobilas.IO.Atlf.Components;

namespace Cobilas.IO.Atlf.Text;
/// <summary>This class allows decoding a string in ATLF format in version <c>1.0</c></summary>
public class ATLFVS10Decoding : ATLFDecoding {
	/// <inheritdoc/>
	public override string Version => "std:1.0";
	/// <inheritdoc/>
	/// <param name="args">
	/// <para>args[0] = <seealso cref="string"/></para>
	/// </param>
	/// <returns>An array of <see cref="ATLFNode"/> objects parsed from the input string.</returns>
	public override ATLFNode[] Reader(params object[] args) {
		if (args[0] is string stg && stg is not null)
			return Reader(new CharacterCursor(stg));
		return [];
	}
	/// <inheritdoc/>
	/// <param name="args">
	/// <para>args[0] = <seealso cref="byte"/>[]</para>
	/// <para>args[1] = <seealso cref="Encoding"/></para>
	/// </param>
	/// <returns>An array of <see cref="ATLFNode"/> objects parsed from the byte array.</returns>
	public override ATLFNode[] Reader4Byte(params object[] args) {
		if (args[1] is Encoding edg && edg is not null)
			if (args[0] is byte[] list && list is not null)
				return Reader(new CharacterCursor(edg.GetString(list)));
		return [];
	}
	/// <summary>Reads ATLF nodes from a <see cref="CharacterCursor"/>.</summary>
	/// <param name="cursor">The character cursor providing the source text.</param>
	/// <returns>An array of <see cref="ATLFNode"/> objects parsed from the cursor.</returns>
	/// <exception cref="ATLFException">Thrown when the input text is empty or contains unrecognized tokens.</exception>
	/// <remarks>
	/// This method processes the ATLF format by reading tokens sequentially from the cursor.
	/// It recognizes tag openings (#!), comment openings (#>), and ignores whitespace.
	/// </remarks>
	protected virtual ATLFNode[] Reader(CharacterCursor cursor) {
		ATLFNode[] res = [];
		if (cursor.Count == 0)
			throw new ATLFException("The ATLF object cannot read empty text!");
		while (cursor.MoveToCharacter())
			if (cursor.CharIsEqualToIndex("#!")) {
				cursor.MoveToCharacter(1L);
				ArrayManipulation.Add(GetTag(cursor), ref res!);
			} else if (cursor.CharIsEqualToIndex("#>")) {
				cursor.MoveToCharacter(1L);
				ArrayManipulation.Add(GetComment(cursor), ref res!);
			} else if (!char.IsWhiteSpace(cursor.CurrentCharacter)) {
				throw ATLFException.GetATLFException("(L:{0} C:{1})\"{2}\" Unidentified tag!",
				cursor.Line, cursor.Column, cursor.CurrentCharacter.EscapeSequenceToString());
			}
		return res;
	}
	/// <summary>Parses a comment node from the cursor.</summary>
	/// <param name="cursor">The character cursor positioned at the start of the comment content.</param>
	/// <returns>An <see cref="ATLFNode"/> of type <see cref="ATLFNodeType.Comment"/> containing the comment text.</returns>
	/// <exception cref="ATLFException">Thrown when the comment block is not properly closed.</exception>
	/// <remarks>
	/// The comment block is expected to end with the "&lt;#" token. The escape sequence "\&lt;#" is allowed within the comment.
	/// </remarks>
	protected virtual ATLFNode GetComment(CharacterCursor cursor) {
		StringBuilder text = new();
		CharacterCursor.LineEndColumn lineEndColumn = cursor.Cursor;

		while (cursor.MoveToCharacter()) {
			if (cursor.CharIsEqualToIndex("<#")) {
				cursor.MoveToCharacter(1L);
				return new ATLFNode("cmt", text.ToString(), ATLFNodeType.Comment);
			} else if (cursor.CharIsEqualToIndex("\\<#"))
				text.Append("<#");
			else text.Append(cursor.CurrentCharacter);
		}

		throw ATLFException.GetATLFException("(L:{0} C:{1})The text block was not closed!"
			, lineEndColumn.Line, lineEndColumn.Column);
	}
	/// <summary>Parses a tag node from the cursor.</summary>
	/// <param name="cursor">The character cursor positioned after the "#!" token.</param>
	/// <returns>An <see cref="ATLFNode"/> of type <see cref="ATLFNodeType.Tag"/> containing the tag name and text.</returns>
	protected virtual ATLFNode GetTag(CharacterCursor cursor) => GetTag(cursor, ATLFNodeType.Tag);
	/// <summary>Parses a tag node from the cursor with a specified node type.</summary>
	/// <param name="cursor">The character cursor positioned after the "#!" token.</param>
	/// <param name="nodeType">The <see cref="ATLFNodeType"/> to assign to the parsed node.</param>
	/// <returns>An <see cref="ATLFNode"/> with the specified type, containing the tag name and text.</returns>
	/// <exception cref="ATLFException">
	/// Thrown when the tag has no name, contains invalid characters, or the text block is not properly opened or closed.
	/// </exception>
	/// <remarks>
	/// The tag format is: #! name :/* text */. The text block ends with "*/" and the escape sequence "\*/" is allowed.
	/// Valid characters for tag names are letters, digits, '.', '_', '/', '\', and '>'.
	/// </remarks>
	protected virtual ATLFNode GetTag(CharacterCursor cursor, ATLFNodeType nodeType) {
		StringBuilder name = new();
		StringBuilder text = new();
		CharacterCursor.LineEndColumn lineEndColumn = cursor.Cursor;
		bool getText = false;
		bool firstSpace = false;
		while (cursor.MoveToCharacter())
			if (getText) {
				if (cursor.CharIsEqualToIndex("\\*/")) {
					text.Append("*/");
					cursor.MoveToCharacter(2L);
				} else if (cursor.CharIsEqualToIndex("*/")) {
					cursor.MoveToCharacter(1L);
					return new ATLFNode(name.ToString().Trim(), text.ToString(), nodeType);
				}
				else text.Append(cursor.CurrentCharacter);
			} else {
				if (cursor.CharIsEqualToIndex(":/*")) {
					if (name.ToString().Trim() == string.Empty)
						throw ATLFException.GetATLFException("(L:{0} C:{1})The tag must have a name!", cursor.Line, cursor.Column);
					getText = true;
					cursor.MoveToCharacter(2L);
				} else {
					if (!firstSpace && cursor.CurrentCharacter == ' ') {
						firstSpace = true;
						continue;
					}
					if (!ValidCharacter(cursor.CurrentCharacter))
						throw ATLFException.GetATLFException("(L:{0} C:{1})The character {2} is not valid.('.', '_', '/', '\\', '>')"
						, cursor.Line, cursor.Column, cursor.CurrentCharacter);
					name.Append(cursor.CurrentCharacter);
				}
			}

		if (!getText)
			throw ATLFException.GetATLFException("(L:{0} C:{1})The text block was not opened!"
			, lineEndColumn.Line, lineEndColumn.Column);
		throw ATLFException.GetATLFException("(L:{0} C:{1})The text block was not closed!"
			, lineEndColumn.Line, lineEndColumn.Column);
	}
	/// <inheritdoc/>
	/// <returns>
	/// <see langword="true"/> if the character is a letter, digit, or one of the allowed special characters ('.', '_', '/', '\', '>'); 
	/// otherwise, <see langword="false"/>.
	/// </returns>
	protected override bool ValidCharacter(char c)
		=> char.IsLetterOrDigit(c) || c == '.' || c == '_' ||
			c == '/' || c == '\\' || c == '>';
}