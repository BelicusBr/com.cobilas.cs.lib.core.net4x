namespace System;  
/// <summary>Represents an escape sequence.</summary>
public enum EscapeSequence {
    /// <summary>Represents no escape sequence.</summary>
    None = 0,
    /// <summary>Represents the escape sequence.(<c>\'</c>)</summary>
    SingleQuote = 1,
    /// <summary>Represents the escape sequence.(<c>\"</c>)</summary>
    DoubleQuote = 2,
    /// <summary>Represents the escape sequence.(<c>\\</c>)</summary>
    BackSlash = 3,
    /// <summary>Represents the escape sequence.(<c>\0</c>)</summary>
    Null = 4,
    /// <summary>Represents the escape sequence.(<c>\a</c>)</summary>
    Alert = 5,
    /// <summary>Represents the escape sequence.(<c>\b</c>)</summary>
    Backspace = 6,
    /// <summary>Represents the escape sequence.(<c>\f</c>)</summary>
    FormFeed = 7,
    /// <summary>Represents the escape sequence.(<c>\n</c>)</summary>
    NewLine = 8,
    /// <summary>Represents the escape sequence.(<c>\r</c>)</summary>
    CarriageReturn = 9,
    /// <summary>Represents the escape sequence.(<c>\t</c>)</summary>
    HorizontalTab = 10,
    /// <summary>Represents the escape sequence.(<c>\v</c>)</summary>
    VerticalTab = 11
}