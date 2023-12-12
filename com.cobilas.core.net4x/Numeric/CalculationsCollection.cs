using Cobilas.Collections;

namespace Cobilas.Numeric {
    public abstract class CalculationsCollection {
        public abstract MathOperator[] Calculations { get; protected set; }
        public abstract MathOperator[] OverwriteCalculations { get; protected set; }

        public abstract void Initialization();
        public abstract double Clac(double V1, string S, double V2);
        
        internal static void OverrideFunction(CalculationsCollection[] collections) {
            MathOperator[] overwriteCalculations = null;
            foreach (var item in collections)
                ArrayManipulation.Add(item.OverwriteCalculations, ref overwriteCalculations);

            bool _break = false;
            foreach (var item1 in overwriteCalculations)
                foreach (var item2 in collections) {
                    for (int I = 0; I < ArrayManipulation.ArrayLength(item2.Calculations); I++) {
                        if (item2.Calculations[I].Signal == item1.Signal) {
                            item2.Calculations[I] = item1;
                            _break = true;
                        }
                        if (_break) break;
                    }
                    if (_break) break;
                }
        }

        public static CalculationsCollection Merge(CalculationsCollection A, CalculationsCollection B) {
            A.Calculations = ArrayManipulation.Add(B.Calculations, A.Calculations);
            return A;
        }
    }
}