using System;
using System.IO;
using System.Text;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFBase : IDisposable {
        public abstract long NodeCount { get; }
        public abstract bool Indent { get; set; }
        public abstract Encoding Encoding { get; set; }
        public abstract string TargetVersion { get; set; }
        public abstract bool Closed { get; protected set; }
        protected abstract bool CloseFlow { get; set;}
        protected abstract ATLFNode[] Nodes { get; set; }
        protected abstract MarshalByRefObject RefObject { get; set; }

        internal static string def_version = "std:1.0";

        public abstract void Close();
        public abstract void Dispose();
    }
}