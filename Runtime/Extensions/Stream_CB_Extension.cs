using System.Text;

namespace System.IO {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public static class Stream_CB_Extension {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    
        /// <summary>
        /// When overridden in a derived class, reads a sequence of bytes from the current
        /// stream and advances the position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="F">Target stream.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="ObjectDisposedException"/>
        public static byte[] Read(this Stream F) {
            byte[] Res = new byte[F.Length];
            int numDeBytesPraLer = Res.Length;
            int numDeBytesLidos = 0;
            while (numDeBytesPraLer > 0) {
                int n = F.Read(Res, numDeBytesLidos, numDeBytesPraLer);
                if (n == 0) break;
                numDeBytesLidos += n;
                numDeBytesPraLer -= n;
            }
            return Res;
        }

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current
        /// stream and advances the current position within this stream by the number of
        /// bytes written.
        /// </summary>
        /// <param name="F">Target stream.</param>
        /// <param name="text">The text that will be written in the current stream.</param>
        /// <param name="encoding">The Encoding used to write to the current stream.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="ObjectDisposedException"/>
        public static void Write(this Stream F, string text, Encoding encoding)
            => Write(F, text.ToCharArray(), encoding);

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current
        /// stream and advances the current position within this stream by the number of
        /// bytes written.
        /// <para><see cref="Encoding"/>.UTF8 is used by default in the method.</para>
        /// </summary>
        /// <param name="F">Target stream.</param>
        /// <param name="text">The text that will be written in the current stream.</param>
        public static void Write(this Stream F, string text)
            => Write(F, text, Encoding.UTF8);

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current
        /// stream and advances the current position within this stream by the number of
        /// bytes written.
        /// </summary>
        /// <param name="F">Target stream.</param>
        /// <param name="chars">The list of characters that will be written to the current stream.</param>
        /// <param name="encoding">The Encoding used to write to the current stream.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="ObjectDisposedException"/>
        public static void Write(this Stream F, char[] chars, Encoding encoding)
            => Write(F, encoding.GetBytes(chars));

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current
        /// stream and advances the current position within this stream by the number of
        /// bytes written.
        /// <para><see cref="Encoding"/>.UTF8 is used by default in the method.</para>
        /// </summary>
        /// <param name="F">Target stream.</param>
        /// <param name="chars">The list of characters that will be written to the current stream.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="ObjectDisposedException"/>
        public static void Write(this Stream F, char[] chars)
            => Write(F, chars, Encoding.UTF8);

        /// <summary>
        /// When overridden in a derived class, writes a sequence of bytes to the current
        /// stream and advances the current position within this stream by the number of
        /// bytes written.
        /// </summary>
        /// <param name="F">Target stream.</param>
        /// <param name="bytes">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="IOException"/>
        /// <exception cref="NotSupportedException"/>
        /// <exception cref="ObjectDisposedException"/>
        public static void Write(this Stream F, byte[] bytes)
            => F.Write(bytes, 0, bytes.Length);

        /// <summary>
        /// Gets an array of characters from the current stream.
        /// </summary>
        /// <param name="F">Target stream.</param>
        /// <param name="encoding">The Encoding used to write to the current stream.</param>
        public static char[] GetChars(this Stream F, Encoding encoding)
            => encoding.GetChars(Read(F));

        /// <summary>
        /// Gets an array of characters from the current stream.
        /// <para><see cref="Encoding"/>.UTF8 is used by default in the method.</para>
        /// </summary>
        /// <param name="F">Target stream.</param>
        public static char[] GetChars(this Stream F)
            => GetChars(F, Encoding.UTF8);

        /// <summary>
        /// Gets a string from the current stream.
        /// </summary>
        /// <param name="F">Target stream.</param>
        /// <param name="encoding">The Encoding used to write to the current stream.</param>
        public static string GetString(this Stream F, Encoding encoding)
            => new string(GetChars(F, encoding));

        /// <summary>
        /// Gets a string from the current stream.
        /// <para><see cref="Encoding"/>.UTF8 is used by default in the method.</para>
        /// </summary>
        /// <param name="F">Target stream.</param>
        public static string GetString(this Stream F)
            => GetString(F, Encoding.UTF8);

        /// <summary>
        /// Generates a <see cref="Guid"/> object from the current flow.
        /// </summary>
        /// <param name="F">Target stream.</param>
        /// <returns>The method returns a Guid value by reading a copy of the current stream.</returns>
        public static Guid GenerateGuid(this Stream F) {
            MemoryStream memory = new MemoryStream();
            F.CopyTo(memory);
            byte[] guid = new byte[16];
            byte[] content = Read(memory);
            for (int I = 0, g = 0; I < content.Length; I++, g++)
                guid[(g >= 16 ? g = 0 : g)] ^= content[I];
            return new Guid(guid);
        }
    }
}
