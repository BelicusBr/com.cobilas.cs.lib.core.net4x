using System;
using Cobilas.Numeric;
using Cobilas.IO.Atlf;
using System.IO;
using System.Text;

namespace com.cobilas.core.net4x.test
{
    class Program
    {
        static void Main(string[] args)
        {
            //ParseCalculation.Parse("2+2");
            using (MemoryStream memory = new MemoryStream()) {
                using (ATLFWriter writer = ATLFWriter.Create(memory)) {
                    // writer.Indent = true;
                    // writer.IndentChars = "\r\n";
                    writer.WriteHeader();
                    writer.WriteWhitespace("\r\n");
                    writer.WriteComment("comment1");
                    writer.WriteComment("comment1\r\ncomment2");
                    writer.WriteWhitespace("\r\n");
                    writer.WriteNode("Node1", "Olá mundo!!!");
                    writer.WriteNode("Node2", "Olá\r\nmundo!!!");
                }
                Console.WriteLine(Encoding.UTF8.GetString(memory.ToArray()));
                _ = Console.ReadLine();
            }
        }
    }
}
