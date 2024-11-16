using Cobilas.Collections;

namespace Cobilas.Numeric;
/// <summary>Base class used to store calculation and calculation overwriting functions.</summary>
public abstract class CalculationsCollection {
    /// <summary>The list of calculation functions.</summary>
    public abstract MathOperator[] Calculations { get; protected set; }
    /// <summary>The list of calculation functions that will replace other calculation functions.</summary>
    public abstract MathOperator[] OverwriteCalculations { get; protected set; }

    /// <summary>Function to initialize the calculation functions.</summary>
    public abstract void Initialization();

    /// <summary>
    /// Function responsible for calling a calculation function specified the sign of the mathematical operation.
    /// </summary>
    /// <param name="V1"></param>
    /// <param name="S">mathematical operator.</param>
    /// <param name="V2"></param>
    public abstract double Clac(double V1, string S, double V2);

    internal static void OverrideFunction(CalculationsCollection[] collections) {
        MathOperator[] overwriteCalculations = [];
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

    /// <summary>Merge two collections of calculations.</summary>
    public static CalculationsCollection Merge(CalculationsCollection A, CalculationsCollection B) {
        A.Calculations = ArrayManipulation.Add(B.Calculations, A.Calculations);
        return A;
    }
}