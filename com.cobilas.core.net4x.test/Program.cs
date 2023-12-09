using System;
using Cobilas.Numeric;
using Cobilas.IO.Atlf;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Cobilas.IO.Atlf.Text;

namespace com.cobilas.core.net4x.test {
    class Program {
        static void Main(string[] args) {
            //ParseCalculation.Parse("2+2");
            ATLFVS10Decoding aTLFVS10 = new();
            aTLFVS10.Reader(
                "#!version:/*1.0*/   " +
                "#!encoding:/*utf-8*/   " +
                "#>Comment1 <#" +
                "#!Tag1:/*Text 1*/   " +
                "#!Tag2:/*Text 1\r\nText 2*/" +
                "#!Tag1.1:/*Text 1*/" +
                "#!Tag1.2:/*Text 1\r\nText 2*/" +
                "#! :/**/" +
                "#! GF:/*" +
                "#! GF"
            );
        }
    }
}
