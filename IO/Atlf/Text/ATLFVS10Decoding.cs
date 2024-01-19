using System;
using System.Text;
using Cobilas.Collections;
using Cobilas.IO.Atlf.Components;

namespace Cobilas.IO.Atlf.Text {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public class ATLFVS10Decoding : ATLFDecoding {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        /// <inheritdoc/>
        public override string Version => "std:1.0";

        /// <inheritdoc/>
        /// <param name="args">
        /// <para>args[0] = <seealso cref="string"/></para>
        /// </param>
        public override ATLFNode[] Reader(params object[] args) {
            if (args[0] is not null)
                return Reader(new CharacterCursor((args[0] as string)!));
            return Array.Empty<ATLFNode>();
        }

        /// <inheritdoc/>
        /// <param name="args">
        /// <para>args[0] = <seealso cref="byte"/>[]</para>
        /// <para>args[1] = <seealso cref="Encoding"/></para>
        /// </param>
        public override ATLFNode[] Reader4Byte(params object[] args) {
            if (args[1] is not null)
                return Reader(new CharacterCursor((args[1] as Encoding)!.GetString(args[0] as byte[])));
            return Array.Empty<ATLFNode>();
        }

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        protected virtual ATLFNode[] Reader(CharacterCursor cursor) {
            ATLFNode[] res = Array.Empty<ATLFNode>();
            if (cursor.Count == 0)
                throw new ATLFException("The ATLF object cannot read empty text!");
            while (cursor.MoveToCharacter()) {
                if (cursor.CharIsEqualToIndex("#!")) {
                    cursor.MoveToCharacter(1L);
                    ArrayManipulation.Add(GetTag(cursor), ref res);
                } else if (cursor.CharIsEqualToIndex("#>")) {
                    cursor.MoveToCharacter(1L);
                    ArrayManipulation.Add(GetComment(cursor), ref res);
                } else if (!char.IsWhiteSpace(cursor.CurrentCharacter)) {
                    throw ATLFException.GetATLFException("(L:{0} C:{1})\"{2}\" Unidentified tag!", 
                    cursor.Line, cursor.Column, cursor.CurrentCharacter.EscapeSequenceToString());
                }
            }
            return res;
        }

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

        protected virtual ATLFNode GetTag(CharacterCursor cursor) => GetTag(cursor, ATLFNodeType.Tag);

        protected virtual ATLFNode GetTag(CharacterCursor cursor, ATLFNodeType nodeType) {
            StringBuilder name = new();
            StringBuilder text = new();
            CharacterCursor.LineEndColumn lineEndColumn = cursor.Cursor;
            bool getText = false;
            bool firstSpace = false;
            while (cursor.MoveToCharacter()) {
                if (getText) {
                    if (cursor.CharIsEqualToIndex("\\*/")) {
                        text.Append("*/");
                        cursor.MoveToCharacter(2L);
                    } else if (cursor.CharIsEqualToIndex("*/")) {
                        cursor.MoveToCharacter(1L);
                        return new ATLFNode(name.ToString().Trim(), text.ToString(), nodeType);
                    } else text.Append(cursor.CurrentCharacter);
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
                            throw ATLFException.GetATLFException("(L:{0} C:{1})O carácter {2} não é valido.('.', '_', '/', '\\', '>')"
                            , cursor.Line, cursor.Column, cursor.CurrentCharacter);
                        name.Append(cursor.CurrentCharacter);
                    }
                }
            }

            if (!getText)
                throw ATLFException.GetATLFException("(L:{0} C:{1})The text block was not opened!"
                , lineEndColumn.Line, lineEndColumn.Column);
            throw ATLFException.GetATLFException("(L:{0} C:{1})The text block was not closed!"
                , lineEndColumn.Line, lineEndColumn.Column);
        }
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

        /// <inheritdoc/>
        protected override bool ValidCharacter(char c)
            => char.IsLetterOrDigit(c) || c == '.' || c == '_' ||
                c == '/' || c == '\\' || c == '>';
    }
}