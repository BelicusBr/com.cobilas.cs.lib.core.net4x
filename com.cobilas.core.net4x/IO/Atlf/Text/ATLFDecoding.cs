using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cobilas.IO.Atlf.Text {
    public abstract class ATLFDecoding {
        public abstract string Version { get; }

        public abstract ATLFNode[] Reader(params object[] args);
        protected abstract bool ValidCharacter(char c);
    }
}