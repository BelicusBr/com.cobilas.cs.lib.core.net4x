using System;
using System.IO;
using System.Text;
using System.Collections;
using Cobilas.IO.Atlf.Text;
using System.Collections.Generic;

namespace Cobilas.IO.Atlf { 
    /// <summary>Base class for ATLF read classes.</summary>
    public abstract class ATLFReader : ATLFBase, IEnumerable<ATLFNode> {
        /// <summary>Starts the process of reading the ATLF file.</summary>
        public abstract void Reader();
        /// <summary>Gets the ATLF header tags.</summary>
        public abstract ATLFNode[] GetHeader();
        /// <summary>Gets the value of the tag.</summary>
        /// <param name="name">The name of the target tag.</param>
        public abstract string GetTag(string name);
        /// <summary>Gets all comments from the ATLF file.</summary>
        public abstract ATLFNode[] GetAllComments();
        /// <summary>Gets a group of ATLF tags within a path.</summary>
        /// <returns>Returns a list of ATLF tags according to a path. 
        /// Example: there are three nodes with the name <c>com.cob.lib.tag1</c>, <c>com.cob.lib.tag2</c> and <c>com.cob.cli.tag-1</c> 
        /// and passing in the <paramref name="path"/> parameter <c>com.cob.lib</c> the tags <c>tag1</c> and <c>tag2</c> will be obtained.</returns>
        public abstract ATLFNode[] GetTagGroup(string path);
        /// <summary>Gets all nodes within the buffer.</summary>
        public abstract IEnumerator<ATLFNode> GetEnumerator();
        /// <summary>Gets the ATLF encoding.</summary>
        /// <returns>The method returns the encoder corresponding to the version passed in the <paramref name="targetVersion"/> parameter. 
        /// If the previous version does not exist, the default version will be returned.</returns>
        protected abstract ATLFDecoding GetATLFDecoding(string targetVersion);

#pragma warning disable CS1591
        public static T Create<T>(Stream stream) where T : ATLFSBReader => Create<T>((MarshalByRefObject)stream);
        public static T Create<T>(string filePath) where T : ATLFSBReader => Create<T>(File.OpenRead(filePath), true);
        public static T Create<T>(TextReader text) where T : ATLFTBReader => Create<T>((MarshalByRefObject)text);
        public static T Create<T>(StringBuilder builder) where T : ATLFTBReader => Create<T>(new StringReader(builder.ToString()), true);

        public static ATLFReader Create(Stream stream) => Create<ATLFStreamReader>(stream);
        public static ATLFReader Create(string filePath) => Create<ATLFStreamReader>(filePath);
        public static ATLFReader Create(TextReader text) => Create<ATLFTextReader>(text);
        public static ATLFReader Create(StringBuilder builder) => Create<ATLFTextReader>(builder);
#pragma warning restore

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