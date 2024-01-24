namespace Cobilas.IO.Atlf.Text { 
    /// <summary>Decoding base class.</summary>
    public abstract class ATLFDecoding {
        /// <summary>Represents a null value of type <see cref="NullATLFDecoding"/>.</summary>
        public readonly static ATLFDecoding Null = new NullATLFDecoding();

        /// <summary>Represents the decoder version.</summary>
        public abstract string Version { get; }

        /// <summary>Read a list of objects.</summary>
        public abstract ATLFNode[] Reader(params object[] args);
        /// <summary>Read a list of objects.</summary>
        public abstract ATLFNode[] Reader4Byte(params object[] args);
        /// <summary>Checks whether the character is valid.</summary>
        protected abstract bool ValidCharacter(char c);
    }
}