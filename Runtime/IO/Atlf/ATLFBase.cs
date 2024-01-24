using System;
using System.Text;

namespace Cobilas.IO.Atlf { 
    /// <summary>Base class for all ATLF classes.</summary>
    public abstract class ATLFBase : IDisposable {
        /// <summary>The number of ATLF nodes stored.</summary>
        public abstract long NodeCount { get; }
        /// <summary>Determines whether text should be indented.</summary>
        public abstract bool Indent { get; set; }
        /// <summary>Sets or returns the encoding used.</summary>
        public abstract Encoding Encoding { get; set; }
        /// <summary>Represents the version that the ATLF object is using.</summary>
        public abstract string TargetVersion { get; set; }
        /// <summary>Indicate whether the flow has been closed.</summary>
        public abstract bool Closed { get; protected set; }
        /// <summary>
        /// This property is used to close the workflow automatically.
        /// <para>This property should be used in cases where you directly access a stream. 
        /// Example: when a flow is called using the <seealso cref="System.IO.File"/>.Open(string) method.</para>
        /// </summary>
        protected abstract bool CloseFlow { get; set;}
        /// <summary>Where ATLF nodes are stored.</summary>
        protected abstract ATLFNode[] Nodes { get; set; }
        /// <summary>Sets or returns the current stream.</summary>
        protected abstract MarshalByRefObject RefObject { get; set; }

        internal static string def_version = "std:1.0";

        /// <summary>Allows you to close the current flow.</summary>
        public abstract void Close();
        /// <summary>Allows you to discard the current stream.</summary>
        public abstract void Dispose();
    }
}