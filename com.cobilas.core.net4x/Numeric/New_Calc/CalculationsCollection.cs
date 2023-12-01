using Cobilas.Collections;

namespace Cobilas.Numeric {
    public abstract class CalculationsCollection {
        public abstract CalculationsCollectionItem[] Calculations { get; protected set; }
        public abstract CalculationsCollectionItem[] OverwriteCalculations { get; protected set; }

        public abstract void Initialization();
        public abstract double Clac(double V1, string S, double V2);
        
        internal static void OverrideFunction(CalculationsCollection[] collections) {
            CalculationsCollectionItem[] overwriteCalculations = null;
            foreach (var item in collections)
                ArrayManipulation.Add(item.OverwriteCalculations, ref overwriteCalculations);
        }

        public static CalculationsCollection Merge(CalculationsCollection A, CalculationsCollection B) {
            A.Calculations = ArrayManipulation.Add(B.Calculations, A.Calculations);
            return A;
        }
    }
}