using System.IO;
using System.Text;

namespace System.Security.Cryptography {
    public static class HashAlgorithm_CB_Extension {

        public static byte[] ComputeHash(this HashAlgorithm H, string FilePath)
            => H.ComputeHash(File.ReadAllBytes(FilePath));

        public static string ComputeHashToString(this HashAlgorithm H, string FilePath) {
            byte[] bytes = ComputeHash(H, FilePath);
            if (bytes is not null) {
                StringBuilder builder = new();
                foreach (var item in bytes)
                    builder.Append(item);
                return builder.ToString();
            }
            return string.Empty;
        }

        public static string ComputeHashToString(this HashAlgorithm H, byte[] buffer) {
            buffer = H.ComputeHash(buffer);
            if (buffer is not null) {
                StringBuilder builder = new();
                foreach (var item in buffer)
                    builder.Append(item);
                return builder.ToString();
            }
            return string.Empty;
        }
        
        public static string ComputeHashToString(this HashAlgorithm H, byte[] buffer, int offset, int count) {
            buffer = H.ComputeHash(buffer, offset, count);
            if (buffer is not null) {
                StringBuilder builder = new();
                foreach (var item in buffer)
                    builder.Append(item);
                return builder.ToString();
            }
            return string.Empty;
        }
        
        public static string ComputeHashToString(this HashAlgorithm H, Stream inputStream) {
            byte[] buffer = H.ComputeHash(inputStream);
            if (buffer is not null) {
                StringBuilder builder = new();
                foreach (var item in buffer)
                    builder.Append(item);
                return builder.ToString();
            }
            return string.Empty;
        }
    }
}
