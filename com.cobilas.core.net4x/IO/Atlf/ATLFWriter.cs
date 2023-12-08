using System;
using System.IO;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFWriter : ATLFBase {

        public abstract string IndentChars { get; set;}

        public abstract void WriteHeader();
        public abstract void WriteWhitespace(string value);
        public abstract void WriteWhitespace(int count, string value);
        public abstract void WriteComment(string value);
        public abstract void WriteNode(string name, string value);
        protected abstract void AddNode(string name, string value, ATLFNodeType nodeType);

        public static T Create<T>(Stream stream) where T : ATLFWriter {
            T writer = Activator.CreateInstance<T>();
            writer.RefObject = stream;
            writer.Indent = true;
            writer.IndentChars = "\r\n";
            return writer;
        }

        public static T Create<T>(TextWriter text) where T : ATLFWriter {
            T writer = Activator.CreateInstance<T>();
            writer.RefObject = text;
            writer.Indent = true;
            writer.IndentChars = "\r\n";
            return writer;
        }

        public static T Create<T>(string filePath) where T : ATLFWriter
            => Create<T>(File.OpenWrite(filePath));

        public static ATLFWriter Create(Stream stream)
            => Create<ATLFStreamWriter>(stream);

        public static ATLFWriter Create(TextWriter text)
            => Create<ATLFTextWriter>(text);

        public static ATLFWriter Create(string filePath)
            => Create<ATLFStreamWriter>(filePath);
    }
}