using System;
using System.Text;
using Cobilas.IO.Atlf.Components;
using System.Collections.Generic;
using Cobilas.IO.Atlf;

namespace com.cobilas.cs.lib.core.net4x.test; 
public static class ATLFLexer {

	private static bool markOpen;
	private static CharacterCursor? cursor = null;

	public static IEnumerable<Token> Tokenize(string stg) {
		cursor = new(stg);
		cursor.AddEscape('\\');
		while (cursor.MoveToCharacter()) {
			if (char.IsWhiteSpace(cursor.CurrentCharacter) || char.IsControl(cursor.CurrentCharacter)) continue;
			else if (cursor.CharIsEqualToIndex("#>")) yield return CreateToken(TokenType.CommentOpen, 0L);
			else if (cursor.CharIsEqualToIndex("<#")) yield return CreateToken(TokenType.CommentClose);
			else if (cursor.CharIsEqualToIndex("#!")) yield return CreateToken(TokenType.MarkOpen, 0L);
			else if (cursor.CharIsEqualToIndex("/*")) yield return CreateToken(TokenType.BlockOpen, 0L);
			else if (cursor.CharIsEqualToIndex("*/")) yield return CreateToken(TokenType.BlockClose);
			else if (cursor.CharIsEqualToIndex(':')) yield return CreateToken(TokenType.Colon, 0L);
			else if (markOpen) yield return GetIdentifier();
			else yield return GetText();
		}
		yield return new(TokenType.EndOfFile, cursor.Cursor);
	}

	private static Token GetIdentifier() {
		StringBuilder builder = new();
		bool fastChar = false;
		CharacterCursor.LineEndColumn lineEndColumn = cursor!.Cursor;

		while (cursor.MoveToCharacter()) {
			if (cursor.CharIsEqualToIndex(':')) break;
			else if (IsIdentifierCharValid(cursor.CurrentCharacter)) fastChar = true;

			if (!IsIdentifierCharValid(cursor.CurrentCharacter) && fastChar)
				throw new ATLFException($"O caractere '{cursor.CurrentCharacter.EscapeSequenceToString()}' não é valido!");
			builder.Append(cursor.CurrentCharacter);
		}
		markOpen = false;
		cursor.MoveToCharacter(-1L);
		return new(TokenType.Identifier, lineEndColumn, builder.ToString().Trim());
	}

	private static bool IsIdentifierCharValid(char c)
		=> char.IsLetterOrDigit(c) || c == '.' || c == '_' ||
			c == '/' || c == '\\' || c == '>';

	private static Token GetText() {
		StringBuilder builder = new();
		CharacterCursor.LineEndColumn lineEndColumn = cursor!.Cursor;

		while (cursor.MoveToCharacter()) {
			if (cursor.CharIsEqualToIndex("\\")) {
				cursor.MoveToCharacter(1L);
				builder.Append(cursor.CurrentCharacter);
				continue;
			} else if (cursor.CharIsEqualToIndex("<#", "*/", "#!")) break;
			builder.Append(cursor.CurrentCharacter);
		}
		cursor.MoveToCharacter(-1L);
		return new(TokenType.Text, lineEndColumn, builder.ToString());
	}

	private static Token CreateToken(TokenType tokenType, long moveChar = 1L) {
		cursor!.MoveToCharacter(moveChar);
		markOpen = tokenType == TokenType.MarkOpen;
		return new(tokenType, cursor.Cursor);
	}
}
