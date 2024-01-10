namespace Cobilas.IO.Atlf.Text {
    public abstract class ATLFEncoding {
        public readonly static ATLFEncoding Null = new NullATLFEncoding();

        public abstract string Version { get; }

        public abstract string Writer(params object[] args);
        public abstract byte[] Writer4Byte(params object[] args);
    }
}