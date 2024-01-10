using System;
using System.IO;
using System.Text;

namespace Cobilas.IO.Alf.Components {
    public class ALFMemoryReadSettings : ALFReadSettings {

        private Encoding encoding = Encoding.UTF8;
        private MarshalByRefObject obj = Stream.Null;
        private bool removeEscapeOnSpecialCharacters;

        public override Encoding Encoding => encoding;
        public override bool RemoveEscapeOnSpecialCharacters { 
            get => removeEscapeOnSpecialCharacters; 
            set => removeEscapeOnSpecialCharacters = value;
        }
        public static ALFMemoryReadSettings DefaultSettings {
            get => new() {
                removeEscapeOnSpecialCharacters = true
            };
        }

        public override void Close() {
            if (obj is null) return;
            if (IsStream()) {
                (obj as Stream)!.Close();
                return;
            }
            (obj as TextReader)!.Close();
        }

        public override void Dispose() {
            this.encoding = default!;
            this.obj = Stream.Null;
        }

        public override void Flush() {
            if (IsStream()) {
                (obj as Stream)!.Flush();
                return;
            }
        }

        public override TypeStream GetStrem<TypeStream>() => (TypeStream)obj;

        public override char[] Read() {
            if (IsStream())
                return (obj as Stream)!.GetChars(encoding);
            return (obj as TextReader)!.ReadToEnd().ToCharArray();
        }

        public override void Set(MarshalByRefObject obj, Encoding encoding) {
            this.obj = obj;
            this.encoding = encoding;
        }

        protected override bool IsStream() => obj is Stream;
    }
}
