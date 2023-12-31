using System;
using System.IO;
using System.Text;
using System.Collections;
using Cobilas.IO.Atlf.Text;
using System.Collections.Generic;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFReader : ATLFBase, IEnumerable<ATLFNode> {
        public abstract void Reader();
        public abstract ATLFNode[] GetHeader();
        public abstract string GetTag(string name);
        public abstract ATLFNode[] GetAllComments();
        public abstract ATLFNode[] GetTagGroup(string path);
        public abstract IEnumerator<ATLFNode> GetEnumerator();
        protected abstract ATLFDecoding GetATLFDecoding(string targetVersion);

        public static T Create<T>(Stream stream) where T : ATLFSBReader => Create<T>((MarshalByRefObject)stream);
        public static T Create<T>(string filePath) where T : ATLFSBReader => Create<T>(File.OpenRead(filePath), true);
        public static T Create<T>(TextReader text) where T : ATLFTBReader => Create<T>((MarshalByRefObject)text);
        public static T Create<T>(StringBuilder builder) where T : ATLFTBReader => Create<T>(new StringReader(builder.ToString()), true);

        public static ATLFReader Create(Stream stream) => Create<ATLFStreamReader>(stream);
        public static ATLFReader Create(string filePath) => Create<ATLFStreamReader>(filePath);
        public static ATLFReader Create(TextReader text) => Create<ATLFTextReader>(text);
        public static ATLFReader Create(StringBuilder builder) => Create<ATLFTextReader>(builder);

        private static T Create<T>(MarshalByRefObject refObject, bool closeFlow = false) where T : ATLFReader{
            T reader = Activator.CreateInstance<T>();
            reader.RefObject = refObject;
            reader.CloseFlow = closeFlow;
            return reader;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => (this as IEnumerable<ATLFNode>).GetEnumerator();
    }
}