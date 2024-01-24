using System;
using System.IO;
using System.Text;
using Cobilas.IO.Atlf.Text;

namespace Cobilas.IO.Atlf { 
    /// <summary>Base class for ATLF writing classes.</summary>
    public abstract class ATLFWriter : ATLFBase {
        /// <summary>Represents the character of indentation.</summary>
        public abstract string IndentChars { get; set; }

        /// <summary>When overridden in a derived class, clears all buffers for this stream and causes any 
        /// buffered data to be written to the underlying device.</summary>
        public abstract void Flush();
        /// <summary>Writes the atlf header to the stream.</summary>
        public abstract void WriteHeader();
        /// <summary>Write a comment in the stream.</summary>
        /// <param name="value">Write the message.</param>
        public abstract void WriteComment(string value);
        /// <summary>Writes an escape character to the stream.</summary>
        public abstract void WriteWhitespace(string value);
        /// <summary>Writes an ATLF node to the stream.</summary>
        public abstract void WriteNode(string name, string value);
        /// <summary>Writes an escape character to the stream.</summary>
        public abstract void WriteWhitespace(int count, string value);
        /// <summary>Gets the ATLF encoding.</summary>
        /// <returns>The method returns the encoder corresponding to the version passed in the <paramref name="targetVersion"/> parameter. 
        /// If the previous version does not exist, the default version will be returned.</returns>
        protected abstract ATLFEncoding GetATLFEncoding(string targetVersion);
        /// <summary>Adds a new node to the buffer.</summary>
        protected abstract void AddNode(string name, string value, ATLFNodeType nodeType);

#pragma warning disable CS1591
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
#pragma warning restore

        private static T Create<T>(MarshalByRefObject refObject, bool closeFlow = false) where T : ATLFWriter {
            T writer = Activator.CreateInstance<T>();
            writer.RefObject = refObject;
            writer.Indent = true;
            writer.CloseFlow = closeFlow;
            return writer;
        }
    }
}