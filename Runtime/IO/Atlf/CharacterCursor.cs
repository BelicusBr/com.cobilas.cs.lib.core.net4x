using System;
using System.Text;
using Cobilas.Collections;

namespace Cobilas.IO.Atlf.Components; 
/// <summary>Allows you to read and modify a string of characters.</summary>
public sealed class CharacterCursor : IDisposable {
    private char[] characters;
    private long index;
    private long line;
    private long column;

    /// <summary>Returns the current line of the reading cursor.</summary>
    public long Line => line;
    /// <summary>Returns the current index of the string being read.</summary>
    public long Index => index;
    /// <summary>Returns the current reading cursor column.</summary>
    public long Column => column;
    /// <summary>Returns a current representation of the row, column, and index of the string being read.</summary>
    public LineEndColumn Cursor => new(line, column, index);
    /// <summary>Returns the length of the string.</summary>
    public long Count => ArrayManipulation.ArrayLongLength(characters);
    /// <summary>Returns the current character.</summary>
    public char CurrentCharacter => index < Count ? characters[index] : '\0';

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public char this[long index] => characters[index];

    public CharacterCursor(char[] characters) {
        this.characters = characters;
        index = -1L;
        line = 1L;
        column = 0L;
    }

    public CharacterCursor(string text) :
        this(text.ToCharArray()) { }

    public CharacterCursor(StringBuilder text) :
        this(text.ToString()) { }

    public CharacterCursor(byte[] bytes, Encoding encoding) :
        this(encoding.GetChars(bytes)) { }
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

    /// <summary>Move the reading cursor to the next character.</summary>
    /// <param name="index">How many jumps the cursor must make to move to the next character.</param>
    /// <returns>The method will return true whenever there are characters to read.</returns>
    public bool MoveToCharacter(long index) {
        bool res = (this.index += index) < Count;
        ++column;
        if (CurrentCharacter == '\n') {
            ++line;
            column = 0L;
        }
        return res;
    }

    /// <summary>Add escape character.</summary>
    public void AddEscape(char escape) {
        char[] newCharacters = new char[Count + 1];
        for (long I = 0; I < characters.LongLength; I++) {
            if (I == index) {
                newCharacters[I] = escape;
                newCharacters[I + 1L] = characters[I];
            } else if (I > index)
                newCharacters[I + 1L] = characters[I];
            else
                newCharacters[I] = characters[I];
        }
        characters = Array.Empty<char>();
        characters = newCharacters;
    }

    /// <summary>Move the reading cursor to the next character.</summary>
    /// <returns>The method will return true whenever there are characters to read.</returns>
    public bool MoveToCharacter()
        => MoveToCharacter(1L);

    /// <summary>Reset the position of the index, line and column of the reading course.</summary>
    public void Reset() {
        line = 1;
        column = 1;
        index = -1L;
    }

    /// <summary>Allows you to make a cut in the character string.</summary>
    /// <param name="index">The index that will start the clipping.</param>
    /// <param name="count">The number of characters that will be cut.</param>
    /// <returns>The method will return a string containing the string of characters that were cut.</returns>
    public string SliceText(long index, long count) {
        StringBuilder builder = new();
        for (long I = index; I < count + index && I < Count; I++)
            builder.Append(this[I]);
        return builder.ToString();
    }

    /// <summary>Compare a character with the current character.</summary>
    public bool CharIsEqualToIndex(char character)
        => index < Count && character == characters[index];

    /// <summary>Compare a character with the current character.</summary>
    public bool CharIsEqualToIndex(params char[] characters) {
        for (long I = 0; I < ArrayManipulation.ArrayLongLength(characters) && index < Count; I++)
            if (characters[I] == this.characters[index])
                return true;
        return false;
    }

    /// <summary>Compare a character with the current character.</summary>
    public bool CharIsEqualToIndex(string text) {
        if (text is null) return false;
        char[] list = text.ToCharArray();
        for (long I = index, C = 0L; C < list.LongLength && I < Count; I++, C++)
            if (text[(int)C] != characters[I])
                return false;
        return true;
    }

    /// <summary>Compare a character with the current character.</summary>
    public bool CharIsEqualToIndex(params string[] texts) {
        if (texts is null) return false;
        foreach (string item in texts)
            if (CharIsEqualToIndex(item))
                return true;
        return false;
    }

#pragma warning disable CS1591 
    public void Dispose() => ArrayManipulation.ClearArraySafe(ref characters);

    public override string ToString() => new(this.characters);
#pragma warning restore

    /// <summary>Represents the current column, row and index of the reading cursor.</summary>
    public readonly struct LineEndColumn {
        private readonly long line;
        private readonly long index;
        private readonly long column;

        /// <summary>Returns the line where the reading cursor is.</summary>
        public long Line => line;
        /// <summary>Returns the index of where the reading cursor is.</summary>
        public long Index => index;
        /// <summary>Returns the column where the reading cursor is.</summary>
        public long Column => column;

        /// <summary>Default value.(L:1, C:1, I:0)</summary>
        public static LineEndColumn Default => new(1, 1, 0);

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        public LineEndColumn(long line, long column, long index) {
            this.line = line;
            this.column = column;
            this.index = index + 1;
        }

        public override string ToString() => string.Format("(L:{0} C:{1} I:{2})", line, column, index);
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    }
}
