using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cobilas.IO.Alf.Components {
    public class ALFMemoryWriterSetting : ALFWriterSettings {

        private bool indent;
        private Encoding encoding = Encoding.UTF8;
        private MarshalByRefObject obj = Stream.Null;
        private bool addEscapeOnSpecialCharacters;

        public override Encoding Encoding => encoding;
        public override bool Indent { get => indent; set => indent = value; }
        public override bool AddEscapeOnSpecialCharacters { 
            get => addEscapeOnSpecialCharacters; 
            set => addEscapeOnSpecialCharacters = value; 
        }
        public static ALFMemoryWriterSetting DefaultSettings {
            get => new() {
                indent = true,
                addEscapeOnSpecialCharacters = true
            };
        }

        public override void Close() {
            if (obj is null) return;
            if (IsStream()) {
                (obj as Stream)!.Close();
                return;
            }
            (obj as TextWriter)!.Close();
        }

        public override void Dispose() {
            encoding = default!;
            obj = Stream.Null;
        }

        public override void Flush() {
            if (obj is null) return;
            if (IsStream()) {
                (obj as Stream)!.Flush();
                return;
            }
            (obj as TextWriter)!.Flush();
        }

        public override TypeStream GetStrem<TypeStream>() => (TypeStream)obj;

        public override void Set(MarshalByRefObject obj, Encoding encoding) {
            this.obj = obj;
            this.encoding = encoding;
        }

        public override void Writer(string text) {
            if (obj is null) return;
            if (IsStream()) {
                (obj as Stream)!.Write(text, encoding);
                return;
            }
            (obj as TextWriter)!.Write(text);
        }

        public override void Writer(char[] buffer) {
            if (obj is null) return;
            if (IsStream()) {
                (obj as Stream)!.Write(buffer, encoding);
                return;
            }
            (obj as TextWriter)!.Write(buffer);
        }

        public override void Writer(StringBuilder builder)
            => Writer(builder.ToString());

        protected override bool IsStream() => obj is Stream;
    }
}
