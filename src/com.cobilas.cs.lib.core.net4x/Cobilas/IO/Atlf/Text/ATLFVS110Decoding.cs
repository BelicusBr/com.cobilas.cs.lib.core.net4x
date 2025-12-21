using System;
using System.Text;
using Cobilas.Collections;
using System.Collections.Generic;
using Cobilas.IO.Atlf.Components;

using LineEndColumn = Cobilas.IO.Atlf.Components.CharacterCursor.LineEndColumn;

namespace Cobilas.IO.Atlf.Text;
/// <summary>This class allows decoding a string in ATLF format in version <c>1.1.0</c></summary>
public class ATLFVS110Decoding : ATLFDecoding {
	private bool markOpen = false;
	/// <inheritdoc/>
	public override string Version => "std:1.1.0";
	/// <inheritdoc/>
	/// <param name="args">
	/// <para>args[0] = <seealso cref="string"/></para>
	/// </param>
	public override ATLFNode[] Reader(params object[] args) {
		if (args[0] is string stg && stg is not null)
			return GetATLFNodes(Tokenize(new CharacterCursor(stg)));
		return [];
	}
	/// <inheritdoc/>
	/// <param name="args">
	/// <para>args[0] = <seealso cref="byte"/>[]</para>
	/// <para>args[1] = <seealso cref="Encoding"/></para>
	/// </param>
	public override ATLFNode[] Reader4Byte(params object[] args) {
		if (args[1] is Encoding edg && edg is not null)
			if (args[0] is byte[] list && list is not null)
				return GetATLFNodes(Tokenize(new CharacterCursor(edg.GetString(list))));
		return [];
	}
	/// <inheritdoc/>
	protected override bool ValidCharacter(char c)
		=> char.IsLetterOrDigit(c) || c == '.' || c == '_' ||
			c == '/' || c == '\\' || c == '>';

	private IEnumerable<Token> Tokenize(CharacterCursor cursor) {
		cursor.AddEscape('\\');
		while (cursor.MoveToCharacter()) {
			if (char.IsWhiteSpace(cursor.CurrentCharacter) || char.IsControl(cursor.CurrentCharacter)) continue;
			else if (cursor.CharIsEqualToIndex("#>")) yield return CreateToken(cursor, TokenType.CommentOpen, 0L);
			else if (cursor.CharIsEqualToIndex("<#")) yield return CreateToken(cursor, TokenType.CommentClose);
			else if (cursor.CharIsEqualToIndex("#!")) yield return CreateToken(cursor, TokenType.MarkOpen, 0L);
			else if (cursor.CharIsEqualToIndex("/*")) yield return CreateToken(cursor, TokenType.BlockOpen, 0L);
			else if (cursor.CharIsEqualToIndex("*/")) yield return CreateToken(cursor, TokenType.BlockClose);
			else if (cursor.CharIsEqualToIndex(':')) yield return CreateToken(cursor, TokenType.Colon, 0L);
			else if (markOpen) yield return GetIdentifier(cursor);
			else yield return GetText(cursor);
		}
		yield return new(TokenType.EndOfFile, cursor.Cursor);
	}

	private static ATLFNode[] GetATLFNodes(IEnumerator<Token>? tokens) {
		ExceptionMessages.ThrowIfNull(tokens, nameof(tokens));
		ulong cmtCount = 0UL;
		ATLFNode[]? result = [];
		while (tokens.MoveNext())
			switch (tokens.Current.Type) {
				case TokenType.CommentOpen:
					ArrayManipulation.Add(ATLFVS110Decoding.GetComment(tokens, ref cmtCount), ref result);
					break;
				case TokenType.MarkOpen:
					ArrayManipulation.Add(ATLFVS110Decoding.GetMark(tokens), ref result);
					break;
				case TokenType.CommentClose:
					throw ATLFException.CommentNotOpen(tokens.Current.LineEndColumn);
				case TokenType.BlockOpen:
				case TokenType.Identifier:
				case TokenType.Colon:
					throw ATLFException.IdentificationBlockNotOpen(tokens.Current.LineEndColumn);
				case TokenType.BlockClose:
				case TokenType.Text:
					throw ATLFException.TextBlockNotOpen(tokens.Current.LineEndColumn);
			}
		return result ?? [];
	}

	private static ATLFNode[] GetATLFNodes(IEnumerable<Token>? tokens) {
		ExceptionMessages.ThrowIfNull(tokens, nameof(tokens));
		return ATLFVS110Decoding.GetATLFNodes(tokens.GetEnumerator());
	}

	private static ATLFNode GetComment(IEnumerator<Token> tokens, ref ulong cmtCount) {
		LineEndColumn lec = tokens.Current.LineEndColumn;
		string value = string.Empty;
		bool cmtOpen = false;
		bool close = false;

		do {
			Token temp = tokens.Current;
			switch (temp.Type) {
				case TokenType.CommentOpen:
					cmtOpen = true;
					break;
				case TokenType.CommentClose:
					if (!cmtOpen) throw ATLFException.CommentNotOpen(temp.LineEndColumn);
					cmtOpen = false;
					close = true;
					break;
				case TokenType.Text:
					if (!cmtOpen) throw ATLFException.CommentNotOpen(temp.LineEndColumn);
					value = temp.Value ?? string.Empty;
					break;
			}
		} while (!close && tokens.MoveNext());

		if (!cmtOpen) return new($"cmt-{cmtCount++}", value, ATLFNodeType.Comment);
		throw ATLFException.CommentNotClosed(lec);
	}

	private static ATLFNode GetMark(IEnumerator<Token> tokens) {
		LineEndColumn lec = tokens.Current.LineEndColumn;
		string name = string.Empty;
		string value = string.Empty;
		bool tagOpen = false;
		bool textOpen = false;
		bool close = false;

		do {
			Token temp = tokens.Current;
			switch (temp.Type) {
				case TokenType.MarkOpen:
					tagOpen = true;
					break;
				case TokenType.Colon:
					if (!tagOpen) throw ATLFException.IdentificationBlockNotOpen(temp.LineEndColumn);
					tagOpen = false;
					break;
				case TokenType.BlockOpen:
					if (tagOpen) throw ATLFException.IdentificationBlockNotClosed(temp.LineEndColumn);
					textOpen = true;
					break;
				case TokenType.BlockClose:
					if (!textOpen) throw ATLFException.TextBlockNotOpen(temp.LineEndColumn);
					textOpen = false;
					close = true;
					break;
				case TokenType.Identifier:
					if (!tagOpen) throw ATLFException.IdentificationBlockNotOpen(temp.LineEndColumn);
					name = temp.Value!;
					break;
				case TokenType.Text:
					if (!textOpen) throw ATLFException.TextBlockNotOpen(temp.LineEndColumn);
					value = temp.Value ?? string.Empty;
					break;
			}
		} while (!close && tokens.MoveNext());

		if (!textOpen && !tagOpen) return new(name, value, ATLFNodeType.Tag);
		throw ATLFException.MarkNotClosed(lec);
	}

	private Token GetIdentifier(CharacterCursor cursor) {
		StringBuilder builder = new();
		bool firstChar = false;
		LineEndColumn lineEndColumn = cursor.Cursor;

		while (cursor.MoveToCharacter()) {
			if (cursor.CharIsEqualToIndex(':')) break;
			else if (ValidCharacter(cursor.CurrentCharacter)) firstChar = true;

			if (!ValidCharacter(cursor.CurrentCharacter) && firstChar)
				throw ATLFException.InvalidCharacter(cursor.CurrentCharacter, cursor.Cursor);
			builder.Append(cursor.CurrentCharacter);
		}
		markOpen = false;
		cursor.MoveToCharacter(-1L);
		return new(TokenType.Identifier, lineEndColumn, builder.ToString().Trim());
	}

	private static Token GetText(CharacterCursor cursor) {
		StringBuilder builder = new();
		LineEndColumn lineEndColumn = cursor.Cursor;

		while (cursor.MoveToCharacter()) {
			if (cursor.CharIsEqualToIndex("\\")) {
				cursor.MoveToCharacter(1L);
				builder.Append(cursor.CurrentCharacter);
				continue;
			}
			else if (cursor.CharIsEqualToIndex("<#", "*/", "#!")) break;
			builder.Append(cursor.CurrentCharacter);
		}
		cursor.MoveToCharacter(-1L);
		return new(TokenType.Text, lineEndColumn, builder.ToString());
	}

	private Token CreateToken(CharacterCursor cursor, TokenType tokenType, long moveChar = 1L) {
		cursor.MoveToCharacter(moveChar);
		markOpen = tokenType == TokenType.MarkOpen;
		return new(tokenType, cursor.Cursor);
	}
}
