using System;
using Cobilas.Numeric;
using Cobilas.IO.Atlf;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Cobilas.IO.Atlf.Text;
using System.Xml;
using Cobilas.IO.Alf.Components;
using Cobilas.Collections;

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
            string txt1 = "12+55+(25*7-(14+5/(21*8)))/85+52";
            string txt2 = "12+55+(25*7-(14+5/(21*8))/85+52";
            StringBuilder builder = new();
            try {
                /*
                Fazer com que os sinais façam a verificação.
                Sinais 'both' devem verifacar a presença de numéros nos lados.
                Sinais 'left' devem verifacar a presença de numéros a esquedar e a direita um sinal.
                Sinais 'right' devem verifacar a presença de numéros a direita e a esquerda um sinal.
                */
                ParseCalculation.DebugLogCalc("12+55+(25*7-(14+5/(21*8)))/85+52");
                ParseCalculation.DebugLogCalc("12+55+:sqr:52+(25*7-7:pow:2+(14+5/(21*8)))/85+52%2");
                ParseCalculation.DebugLogCalc("3++");
                ParseCalculation.DebugLogCalc("3--");
                ParseCalculation.DebugLogCalc("3**");
                ParseCalculation.DebugLogCalc("3//");
                ParseCalculation.DebugLogCalc("3%%");
                ParseCalculation.DebugLogCalc("~-3");
                ParseCalculation.DebugLogCalc("12:pow:3");
                ParseCalculation.DebugLogCalc(":sqr:3");
                ParseCalculation.DebugLogCalc(":cos:3");
                ParseCalculation.DebugLogCalc(":acos:3");
                ParseCalculation.DebugLogCalc(":sin:3");
                ParseCalculation.DebugLogCalc(":asin:3");
                ParseCalculation.DebugLogCalc(":log:3");
                ParseCalculation.DebugLogCalc("12:log-nb:3");
                ParseCalculation.DebugLogCalc(":log10:3");
                //_ = GetMathBlock(new(txt2), GetSignals(), false);
            }
            catch (System.Exception ex) {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
