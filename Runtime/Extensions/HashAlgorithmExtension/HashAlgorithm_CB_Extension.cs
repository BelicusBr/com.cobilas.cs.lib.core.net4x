using System.IO;

namespace System.Security.Cryptography;
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
public static class HashAlgorithm_CB_Extension {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

    /// <summary>
    /// Computes the hash value for the specified byte array.
    /// </summary>
    /// <param name="H">Target object.</param>
    /// <param name="FilePath">The full path of the file.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="PathTooLongException"/>
    /// <exception cref="DirectoryNotFoundException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="UnauthorizedAccessException"/>
    /// <exception cref="FileNotFoundException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="SecurityException"/>
    /// <returns>The computed hash code.</returns>
    public static byte[] ComputeHash(this HashAlgorithm H, string FilePath)
        => H.ComputeHash(File.ReadAllBytes(FilePath));

    /// <summary>
    /// Computes the hash value for the specified byte array.
    /// </summary>
    /// <param name="H">Target object.</param>
    /// <param name="FilePath">The full path of the file.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="PathTooLongException"/>
    /// <exception cref="DirectoryNotFoundException"/>
    /// <exception cref="IOException"/>
    /// <exception cref="UnauthorizedAccessException"/>
    /// <exception cref="FileNotFoundException"/>
    /// <exception cref="NotSupportedException"/>
    /// <exception cref="SecurityException"/>
    /// <returns>The computed hash code for string.</returns>
    public static HashString ComputeHashToString(this HashAlgorithm H, string FilePath) {
        byte[] bytes = ComputeHash(H, FilePath);
        if (bytes is not null)
            return new(bytes);
        return HashString.Empty;
    }

    /// <summary>
    /// Computes the hash value for the specified byte array.
    /// </summary>
    /// <param name="H">Target object.</param>
    /// <param name="buffer">The input to compute the hash code for.</param>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <returns>The computed hash code for string.</returns>
    public static HashString ComputeHashToString(this HashAlgorithm H, byte[] buffer) {
        buffer = H.ComputeHash(buffer);
        if (buffer is not null) 
            return new(buffer);
        return HashString.Empty;
    }
    
    /// <summary>
    /// Computes the hash value for the specified region of the specified byte array.
    /// </summary>
    /// <param name="H">Target object.</param>
    /// <param name="buffer">The input to compute the hash code for.</param>
    /// <param name="offset">The offset into the byte array from which to begin using data.</param>
    /// <param name="count">The number of bytes in the array to use as data.</param>
    /// <exception cref="ArgumentException"/>
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentOutOfRangeException"/>
    /// <exception cref="ObjectDisposedException"/>
    /// <returns>The computed hash code for string.</returns>
    public static HashString ComputeHashToString(this HashAlgorithm H, byte[] buffer, int offset, int count) {
        buffer = H.ComputeHash(buffer, offset, count);
        if (buffer is not null)
            return new(buffer);
        return HashString.Empty;
    }
    
    /// <summary>
    /// Computes the hash value for the specified <see cref="System.IO.Stream"/> object.
    /// </summary>
    /// <param name="H">Target object.</param>
    /// <param name="inputStream">The input to compute the hash code for.</param>
    /// <exception cref="ObjectDisposedException"/>
    /// <returns>The computed hash code for string.</returns>
    public static HashString ComputeHashToString(this HashAlgorithm H, Stream inputStream) {
        byte[] buffer = H.ComputeHash(inputStream);
        if (buffer is not null) 
            return new(buffer);
        return HashString.Empty;
    }
}
