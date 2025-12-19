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
	EscapeCharacter,
	EndOfFile
}


public sealed class Token
{
	public TokenType Type { get; }
	public string Value { get; }

	public Token(TokenType type, string value = null)
	{
		Type = type;
		Value = value;
	}

	public override string ToString() =>
		Value == null ? Type.ToString() : $"{Type} ({Value})";
}
