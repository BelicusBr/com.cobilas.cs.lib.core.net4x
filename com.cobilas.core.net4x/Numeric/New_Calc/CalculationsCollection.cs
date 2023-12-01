using Cobilas.Collections;

namespace Cobilas.Numeric {
    public abstract class CalculationsCollection {
        public abstract CalculationsCollectionItem[] Calculations { get; protected set; }

        public abstract void Initialization();
        public abstract double Clac(double V1, string S, double V2);

        
        public static CalculationsCollection Merge(CalculationsCollection A, CalculationsCollection B) {
            A.Calculations = ArrayManipulation.Add(B.Calculations, A.Calculations);
            return A;
        }
    }
}