using System;
using System.IO;
using System.Text;
using Cobilas.IO.Atlf.Text;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFWriter : ATLFBase {

        public abstract string IndentChars { get; set; }

        public abstract void Flush();
        public abstract void WriteHeader();
        public abstract void WriteComment(string value);
        public abstract void WriteWhitespace(string value);
        public abstract void WriteNode(string name, string value);
        public abstract void WriteWhitespace(int count, string value);
        protected abstract ATLFEncoding GetATLFEncoding(string targetVersion);
        protected abstract void AddNode(string name, string value, ATLFNodeType nodeType);

        public static T Create<T>(Stream stream) where T : ATLFSBWriter => Create<T>((MarshalByRefObject)stream);
        public static T Create<T>(string filePath) where T : ATLFSBWriter => Create<T>(File.OpenWrite(filePath), true);
        public static T Create<T>(TextWriter text) where T : ATLFTBWriter => Create<T>((MarshalByRefObject)text);
        public static T Create<T>(StringBuilder text) where T : ATLFTBWriter => Create<T>(new StringWriter(text), true);
        public static T Create<T>(StringBuilder text, IFormatProvider formatProvider) where T : ATLFTBWriter => Create<T>(new StringWriter(text, formatProvider), true);

        public static ATLFWriter Create(Stream stream) => Create<ATLFStreamWriter>(stream);
        public static ATLFWriter Create(string filePath) => Create<ATLFStreamWriter>(filePath);
        public static ATLFWriter Create(TextWriter text) => Create<ATLFTextWriter>(text);
        public static ATLFWriter Create(StringBuilder text) => Create<ATLFTextWriter>(text);
        public static ATLFWriter Create(StringBuilder text, IFormatProvider formatProvider) => Create<ATLFTextWriter>(text, formatProvider);

        private static T Create<T>(MarshalByRefObject refObject, bool closeFlow = false) where T : ATLFWriter {
            T writer = Activator.CreateInstance<T>();
            writer.RefObject = refObject;
            writer.Indent = true;
            writer.CloseFlow = closeFlow;
            return writer;
        }
    }
}