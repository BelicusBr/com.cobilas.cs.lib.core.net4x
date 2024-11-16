using System;

namespace Cobilas.Numeric;  
/// <summary>It represents the mathematical signal and performs its mathematical operation.</summary>
public readonly struct MathOperator : IEquatable<string>, IEquatable<byte>, IEquatable<SignalOrientation> {

    private readonly string signal;
    private readonly byte executionLevel;
    private readonly SignalOrientation orientation;
    private readonly Func<double, double, double> function;

    /// <summary>The sign of the mathematical operation.</summary>
    public string Signal => signal;
    /// <summary>The function that performs the mathematical operation.</summary>
    public Delegate? Function => function;
    /// <summary>The order of execution of the mathematical operation.</summary>
    public byte ExecutionLevel => executionLevel;
    /// <summary>Indicates the direction where the operation numbers will be obtained.</summary>
    public SignalOrientation Orientation => orientation;

    /// <param name="signal">The sign of the mathematical operation.</param>
    /// <param name="function">The function that performs the mathematical operation.</param>
    /// <param name="orientation">Indicates the direction where the operation numbers will be obtained.</param>
    /// <param name="executionLevel">The order of execution of the mathematical operation.</param>
    public MathOperator(
        string signal, Func<double, double, double> function,
         SignalOrientation orientation, byte executionLevel) {
        this.signal = signal;
        this.function = function;
        this.orientation = orientation;
        this.executionLevel = executionLevel;
    }

    /// <param name="signal">The sign of the mathematical operation.</param>
    /// <param name="function">The function that performs the mathematical operation.</param>
    /// <param name="orientation">Indicates the direction where the operation numbers will be obtained.</param>
    public MathOperator(
        string signal, Func<double, double, double> function,
         SignalOrientation orientation) : this(signal, function, orientation, 0) {}

    /// <param name="signal">The sign of the mathematical operation.</param>
    /// <param name="function">The function that performs the mathematical operation.</param>
    /// <param name="orientation">Indicates the direction where the operation numbers will be obtained.</param>
    /// <param name="executionLevel">The order of execution of the mathematical operation.</param>
    public MathOperator(
        char signal, Func<double, double, double> function, SignalOrientation orientation,
         byte executionLevel) :
        this(signal.ToString(), function, orientation, executionLevel) {}

    /// <param name="signal">The sign of the mathematical operation.</param>
    /// <param name="function">The function that performs the mathematical operation.</param>
    /// <param name="orientation">Indicates the direction where the operation numbers will be obtained.</param>
    public MathOperator(
        char signal, Func<double, double, double> function,
         SignalOrientation orientation) : this(signal, function, orientation, 0) {}

    /// <summary>Performs the comparison between <see cref="MathOperator"/> and <see cref="string"/>.</summary>
    public bool Equals(string? other)
        => other == signal;

    /// <summary>Performs the comparison between <see cref="MathOperator"/> and <see cref="byte"/>.</summary>
    public bool Equals(byte other)
        => other == executionLevel;

    /// <summary>Performs the comparison between <see cref="MathOperator"/> and <see cref="SignalOrientation"/>.</summary>
    public bool Equals(SignalOrientation other)
        => other == orientation;

    /// <inheritdoc/>
    public override int GetHashCode()
        => base.GetHashCode();

    /// <inheritdoc/>
    public override bool Equals(object? obj) 
        => (obj is string str && Equals(str)) ||
            (obj is byte bt && Equals(bt)) ||
            (obj is SignalOrientation sot && Equals(sot));
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(string A, MathOperator B) => B.Equals(A);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(string A, MathOperator B) => !B.Equals(A);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(byte A, MathOperator B) => B.Equals(A);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(byte A, MathOperator B) => !B.Equals(A);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(SignalOrientation A, MathOperator B) => B.Equals(A);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(SignalOrientation A, MathOperator B) => !B.Equals(A);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(MathOperator A, string B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(MathOperator A, string B) => !A.Equals(B);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(MathOperator A, byte B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(MathOperator A, byte B) => !A.Equals(B);
    /// <summary>Indicates whether this instance is equal to another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator ==(MathOperator A, SignalOrientation B) => A.Equals(B);
    /// <summary>Indicates whether this instance is different from another instance of the same type.</summary>
    /// <param name="A">Object to be compared.</param>
    /// <param name="B">Object of comparison.</param>
    /// <returns>Returns the result of the comparison.</returns>
    public static bool operator !=(MathOperator A, SignalOrientation B) => !A.Equals(B);
}