namespace System {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public static class Char_CB_Extension {
        public static sbyte ToSByte(this char c)
            => Convert.ToSByte(c);
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

        /// <summary>
        /// Method that interprets a char value.
        /// </summary>
        /// <returns>The method returns a value of type <see cref="EscapeSequence"/> representing the type of escape sequence.</returns>
        public static EscapeSequence InterpretEscapeSequence(this char c)
            => c switch {
                '\'' => EscapeSequence.SingleQuote,
                '\"' => EscapeSequence.DoubleQuote,
                '\\' => EscapeSequence.BackSlash,
                '\0' => EscapeSequence.Null,
                '\a' => EscapeSequence.Alert,
                '\b' => EscapeSequence.Backspace,
                '\f' => EscapeSequence.FormFeed,
                '\n' => EscapeSequence.NewLine,
                '\r' => EscapeSequence.CarriageReturn,
                '\t' => EscapeSequence.HorizontalTab,
                '\v' => EscapeSequence.VerticalTab,
                _ => EscapeSequence.None,
            };

        /// <summary>
        /// Method that interprets a char value.
        /// </summary>
        /// <returns>The method returns a value of type <see cref="EscapeSequence"/> representing the type of escape sequence.
        /// <para>The return is converted to a <see cref="string"/>.</para>
        /// </returns>
        public static string EscapeSequenceToString(this char c)
            => InterpretEscapeSequence(c) switch {
                EscapeSequence.SingleQuote => @"\'",
                EscapeSequence.DoubleQuote => "\\\"",
                EscapeSequence.BackSlash => @"\\",
                EscapeSequence.Null => @"\0",
                EscapeSequence.Alert => @"\a",
                EscapeSequence.Backspace => @"\b",
                EscapeSequence.FormFeed => @"\f",
                EscapeSequence.NewLine => @"\n",
                EscapeSequence.CarriageReturn => @"\r",
                EscapeSequence.HorizontalTab => @"\t",
                EscapeSequence.VerticalTab => @"\v",
                _ => c.ToString(),
            };
    }
}
