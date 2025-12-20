using System.Text;
using System.Collections.Generic;
using Cobilas.IO.Atlf.Components;

namespace com.cobilas.cs.lib.core.net4x.test;

public sealed class AtlLexer
{
	private int _pos;
	private TokenType textBlock;
	private readonly string _input;

	private bool isCommentBlock => textBlock == TokenType.CommentOpen;
	private bool isTextBlock => textBlock == TokenType.BlockOpen;
	private bool isValueBlock => isCommentBlock || isTextBlock;

	public AtlLexer(string input)
	{
		_input = input;
		_pos = 0;
	}

	public IEnumerable<Token> Tokenize()
	{
		while (!IsEOF())
		{
			if (Match("#>") && !isValueBlock) yield return new Token(textBlock = TokenType.CommentOpen, CharacterCursor.LineEndColumn.Default);
			else if (Match("<#") && !isTextBlock) yield return new Token(textBlock = TokenType.CommentClose, CharacterCursor.LineEndColumn.Default);
			else if (Match("#!") && !isValueBlock) yield return new Token(TokenType.MarkOpen, CharacterCursor.LineEndColumn.Default);
			else if (Match("/*") && !isValueBlock) yield return new Token(textBlock = TokenType.BlockOpen, CharacterCursor.LineEndColumn.Default);
			else if (Match("*/") && !isCommentBlock) yield return new Token(textBlock = TokenType.BlockClose, CharacterCursor.LineEndColumn.Default);
			else if (Peek() == ':' && !isValueBlock)
			{
				_pos++;
				yield return new Token(TokenType.Colon, CharacterCursor.LineEndColumn.Default);
			}
			else if (char.IsWhiteSpace(Peek()) && !isValueBlock)
			{
				_pos++;
			}
			else if (IsIdentifierStart(Peek()) && !isValueBlock)
			{
				yield return ReadIdentifier();
			}
			else
			{
				yield return ReadText();
			}
		}

		yield return new Token(TokenType.EndOfFile, CharacterCursor.LineEndColumn.Default);
	}

	private Token ReadIdentifier()
	{
		var sb = new StringBuilder();
		while (!IsEOF() && IsIdentifierPart(Peek()))
		{
			sb.Append(Peek());
			_pos++;
		}
		return new Token(TokenType.Identifier, CharacterCursor.LineEndColumn.Default, sb.ToString());
	}

	private Token ReadText()
	{
		var sb = new StringBuilder();
		while (!IsEOF() && CheckBlock()) {

			if (StartsWith("\\*"))
				_pos++;
			sb.Append(Peek());
			_pos++;
		}

		return new Token(TokenType.Text, CharacterCursor.LineEndColumn.Default, sb.ToString());
	}

	public bool CheckBlock()
		=> (!StartsWith("#>") || isValueBlock) &&
		   (!StartsWith("<#") || isTextBlock) &&
		   (!StartsWith("#!") || isValueBlock) &&
		   (!StartsWith("/*") || isValueBlock) &&
		   (!StartsWith("*/") || isCommentBlock);

	private bool Match(string value)
	{
		if (!StartsWith(value))
			return false;

		_pos += value.Length;
		return true;
	}

	private bool StartsWith(string value)
	{
		if (_pos + value.Length > _input.Length)
			return false;

		for (int i = 0; i < value.Length; i++)
			if (_input[_pos + i] != value[i])
				return false;

		return true;
	}

	private char Peek() => _input[_pos];

	private bool IsEOF() => _pos >= _input.Length;

	private static bool IsIdentifierStart(char c) =>
		char.IsLetter(c) || c == '_' || c == '/' || c == '.';

	private static bool IsIdentifierPart(char c) =>
		char.IsLetterOrDigit(c) || c == '_' || c == '-' || c == '/' || c == '.';
}