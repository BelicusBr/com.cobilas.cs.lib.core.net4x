using System;
using Cobilas.Collections;

namespace Cobilas.Numeric {
    public static class ParseCalculation {

        private static CalculationsCollection[] collections;

        static ParseCalculation() {
            collections = new CalculationsCollection[1];
            collections[0] = new BasicCalculation();
            collections[0].Initialization();

            foreach (var item in TypeUtilitarian.GetTypes()) {
                if (item.IsSubclassOf(typeof(CalculationsCollection)) &&
                    !item.CompareType<BasicCalculation>() &&
                    !item.CompareType<CalculationsCollection>()) {
                    CalculationsCollection calc = item.Activator<CalculationsCollection>();
                    calc.Initialization();
                    ArrayManipulation.Add(calc, ref collections);
                }
            }
        }

        public static void Parse(string text) {
            Console.WriteLine($"Paser:{text}");
        }
    }
}