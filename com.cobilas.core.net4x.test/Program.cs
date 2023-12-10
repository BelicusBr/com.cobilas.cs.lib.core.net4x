using System;
using Cobilas.Numeric;
using Cobilas.IO.Atlf;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Cobilas.IO.Atlf.Text;
using System.Xml;

namespace com.cobilas.core.net4x.test {
    class Program {
        static void Main(string[] args) {
            ParseCalculation.Parse("2+2");
            //12+55+(25*7-(14+5/(21*8)))/85+52
            /*
            <root>
                <add a="12" b="55">
                    <add>
                        <para>
                            <mult a="25" b="7">
                                <sub>
                                    <para>
                                        <add a="14" b="5">
                                            <div>
                                                <mult a="21" b="8"/>
                                            </div>
                                        </add> 
                                    </para>
                                </sub>
                            </div>
                        </para>
                        <div b="85">
                            <add b="52"/>
                        </div>
                    </add>
                </add>
            </root>
            */
            try {
                
            }
            catch (System.Exception ex) {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
