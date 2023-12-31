using System;

namespace Cobilas.Numeric {
    public readonly struct MathOperator : IEquatable<string>, IEquatable<byte>, IEquatable<SignalOrientation> {

        private readonly string signal;
        private readonly byte executionLevel;
        private readonly SignalOrientation orientation;
        private readonly Func<double, double, double> function;

        public string Signal => signal;
        public Delegate Function => function;
        public byte ExecutionLevel => executionLevel;
        public SignalOrientation Orientation => orientation;

        public MathOperator(
            string signal, Func<double, double, double> function,
             SignalOrientation orientation, byte executionLevel) {
            this.signal = signal;
            this.function = function;
            this.orientation = orientation;
            this.executionLevel = executionLevel;
        }

        public MathOperator(
            string signal, Func<double, double, double> function,
             SignalOrientation orientation) : this(signal, function, orientation, 0) {}

        public MathOperator(
            char signal, Func<double, double, double> function, SignalOrientation orientation,
             byte executionLevel) :
            this(signal.ToString(), function, orientation, executionLevel) {}

        public MathOperator(
            char signal, Func<double, double, double> function,
             SignalOrientation orientation) : this(signal, function, orientation, 0) {}

        public bool Equals(string other)
            => other == signal;

        public bool Equals(byte other)
            => other == executionLevel;

        public bool Equals(SignalOrientation other)
            => other == orientation;

        public override int GetHashCode()
            => base.GetHashCode();

        public override bool Equals(object obj) 
            => (obj is string str && Equals(str)) ||
                (obj is byte bt && Equals(bt)) ||
                (obj is SignalOrientation sot && Equals(sot));

        public static bool operator ==(string A, MathOperator B) => B.Equals(A);
        public static bool operator !=(string A, MathOperator B) => !B.Equals(A);
        public static bool operator ==(byte A, MathOperator B) => B.Equals(A);
        public static bool operator !=(byte A, MathOperator B) => !B.Equals(A);
        public static bool operator ==(SignalOrientation A, MathOperator B) => B.Equals(A);
        public static bool operator !=(SignalOrientation A, MathOperator B) => !B.Equals(A);

        public static bool operator ==(MathOperator A, string B) => A.Equals(B);
        public static bool operator !=(MathOperator A, string B) => !A.Equals(B);
        public static bool operator ==(MathOperator A, byte B) => A.Equals(B);
        public static bool operator !=(MathOperator A, byte B) => !A.Equals(B);
        public static bool operator ==(MathOperator A, SignalOrientation B) => A.Equals(B);
        public static bool operator !=(MathOperator A, SignalOrientation B) => !A.Equals(B);
    }
}