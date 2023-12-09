using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cobilas.IO.Atlf.Text;

namespace Cobilas.IO.Atlf
{
    public class ATLFStreamReader : ATLFReader {
        private bool disposedValue;

        public override long NodeCount => throw new NotImplementedException();

        public override bool Indent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override Encoding Encoding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string TargetVersion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override bool Closed { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
        protected override ATLFNode[] Nodes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        protected override MarshalByRefObject RefObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        ~ATLFStreamReader()
            => Dispose(disposing: false);

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public override ATLFNode[] GetHeader()
        {
            throw new NotImplementedException();
        }

        public override string GetTag(string name)
        {
            throw new NotImplementedException();
        }

        public override ATLFNode[] GetTagGroup(string path)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                }
                disposedValue = true;
            }
        }

        protected override ATLFDecoding GetATLFEncoding(string targetVersion)
        {
            throw new NotImplementedException();
        }
    }
}