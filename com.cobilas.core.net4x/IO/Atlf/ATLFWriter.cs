using System;
using System.IO;
using Cobilas.IO.Atlf.Text;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFWriter : ATLFBase {

        public abstract string IndentChars { get; set;}

        public abstract void Flush();
        public abstract void WriteHeader();
        public abstract void WriteComment(string value);
        public abstract void WriteWhitespace(string value);
        public abstract void WriteNode(string name, string value);
        public abstract void WriteWhitespace(int count, string value);
        protected abstract ATLFEncoding GetATLFEncoding(string targetVersion);
        protected abstract void AddNode(string name, string value, ATLFNodeType nodeType);

        public static T Create<T>(Stream stream) where T : ATLFWriter => Create<T>((MarshalByRefObject)stream);
        public static T Create<T>(TextWriter text) where T : ATLFWriter => Create<T>((MarshalByRefObject)text);

        public static T Create<T>(string filePath) where T : ATLFWriter
            => Create<T>(File.OpenWrite(filePath));

        public static ATLFWriter Create(Stream stream)
            => Create<ATLFStreamWriter>(stream);

        public static ATLFWriter Create(TextWriter text)
            => Create<ATLFTextWriter>(text);

        public static ATLFWriter Create(string filePath)
            => Create<ATLFStreamWriter>(filePath);

        private static T Create<T>(MarshalByRefObject refObject) where T : ATLFWriter {
            T writer = Activator.CreateInstance<T>();
            writer.RefObject = refObject;
            writer.Indent = true;
            writer.IndentChars = "\r\n";
            return writer;
        }
    }
}