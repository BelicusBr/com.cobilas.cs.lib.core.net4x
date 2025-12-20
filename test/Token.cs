using Cobilas.IO.Atlf.Components;

namespace com.cobilas.cs.lib.core.net4x.test;

public enum TokenType
{
	None,
	CommentOpen,      // #>
	CommentClose,     // <#
	MarkOpen,         // #!
	Colon,            // :
	BlockOpen,        // /*
	BlockClose,       // */
	Identifier,       // Tag, version, encoding...
	Text,             // Conteúdo
	EndOfFile
}


public sealed class Token
{
	public TokenType Type { get; }
	public string Value { get; }
	public CharacterCursor.LineEndColumn LineEndColumn { get; }

	public Token(TokenType type, CharacterCursor.LineEndColumn lc, string value = null)
	{
		Type = type;
		Value = value;
		LineEndColumn = lc;
	}

	public override string ToString() =>
		Value == null ? $"{Type} [{LineEndColumn}]" : $"{Type} [{LineEndColumn}]({Value})";
}
