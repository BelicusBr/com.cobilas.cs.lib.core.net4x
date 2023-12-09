using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cobilas.Collections;
using Cobilas.IO.Alf.Components;

namespace Cobilas.IO.Atlf.Text {
    public class ATLFVS10Decoding : ATLFDecoding {
        public override string Version => "std:1.0";

        /// <param name="args">
        /// <para>args[0] = <seealso cref="string"/></para>
        /// </param>
        public override ATLFNode[] Reader(params object[] args)
            => Reader(new CharacterCursor(args[0] as string));

        protected virtual ATLFNode[] Reader(CharacterCursor cursor) {
            ATLFNode[] res = null;
            while (cursor.MoveToCharacter()) {
                if (cursor.CharIsEqualToIndex("#!")) {
                    cursor.MoveToCharacter(1L);
                    ArrayManipulation.Add(GetTag(cursor), ref res);
                } else if (cursor.CharIsEqualToIndex("#>")) {
                    cursor.MoveToCharacter(1L);
                    ArrayManipulation.Add(GetComment(cursor), ref res);
                } else if (!cursor.CharIsEqualToIndex(" ")) {
                    throw ATLFException.GetATLFException("(L:{0} C:{1})\"{2}\" Unidentified tag!", 
                    cursor.Line, cursor.Column, cursor.CurrentCharacter);
                }
            }
            return res;
        }

        protected virtual ATLFNode GetComment(CharacterCursor cursor) {
            StringBuilder text = new StringBuilder();
            while (cursor.MoveToCharacter()) {
                if (cursor.CharIsEqualToIndex("<#")) {
                    cursor.MoveToCharacter(1L);
                    break;
                } else if (cursor.CharIsEqualToIndex("\\<#"))
                    text.Append("<#");
                else text.Append(cursor.CurrentCharacter);
            }
            ATLFNode node = new ATLFNode("cmt", text.ToString(), ATLFNodeType.Comment);
            return node;
        }

        protected virtual ATLFNode GetTag(CharacterCursor cursor) {
            StringBuilder name = new StringBuilder();
            StringBuilder text = new StringBuilder();
            CharacterCursor.LineEndColumn lineEndColumn = cursor.Cursor;
            bool getText = false;
            bool closed = false;
            bool firstSpace = false;
            while (cursor.MoveToCharacter()) {
                if (getText) {
                    if (cursor.CharIsEqualToIndex("\\*/")) {
                        text.Append("*/");
                        cursor.MoveToCharacter(2L);
                    } else if (cursor.CharIsEqualToIndex("*/")) {
                        cursor.MoveToCharacter(1L);
                        closed = true;
                        break;
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
            else if  (!closed)
                throw ATLFException.GetATLFException("(L:{0} C:{1})The text block was not closed!"
                , lineEndColumn.Line, lineEndColumn.Column);

            return new ATLFNode(name.ToString().Trim(), text.ToString(), ATLFNodeType.Tag);
        }

        protected override bool ValidCharacter(char c)
            => char.IsLetterOrDigit(c) || c == '.' || c == '_' || c == '/' || c == '\\' || c == '>';
    }
}