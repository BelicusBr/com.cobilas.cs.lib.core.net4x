using System;
using System.Runtime.Serialization;

namespace Cobilas.IO.Atlf {
    [Serializable]
    public class ATLFException : Exception {
        public ATLFException() { }
        public ATLFException(string message) : base(message) { }
        public ATLFException(string message, Exception inner) : base(message, inner) { }
        protected ATLFException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public static ATLFException GetATLFException(string format, params object[] args)
            => new ATLFException(string.Format(format, args));

        /// <summary>msg:The ATLFWriter object has already been closed.</summary>
        public static InvalidOperationException ATLFClosed()
            => new InvalidOperationException("The ATLF object has already been closed.");

        public static InvalidOperationException ATLFReaderAfterClosing()
            => new InvalidOperationException("The read operation cannot be done after closing the ATLF object!");

        public static InvalidOperationException ATLFReaderTagAfterClosing()
            => new InvalidOperationException("The tag reading operation cannot be done after closing the ATLF object!");

        /// <summary>msg:It is not possible to release resources to the flow after closing the ATLF object.</summary>
        public static InvalidOperationException ATLFFlowAfterClosing()
            => new InvalidOperationException("It is not possible to release resources to the flow after closing the ATLF object.");

        public static InvalidOperationException ATLFTagsAfterClosing()
            => new InvalidOperationException("You cannot add new tags to the stream after closing the ATLF object.");
    }
}