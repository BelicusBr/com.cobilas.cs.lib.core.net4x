using System;

namespace Cobilas.Numeric { 
    /// <summary>It represents the mathematical signal and performs its mathematical operation.</summary>
    public readonly struct MathOperator : IEquatable<string>, IEquatable<byte>, IEquatable<SignalOrientation> {

        private readonly string signal;
        private readonly byte executionLevel;
        private readonly SignalOrientation orientation;
        private readonly Func<double, double, double> function;

        /// <summary>The sign of the mathematical operation.</summary>
        public string Signal => signal;
        /// <summary>The function that performs the mathematical operation.</summary>
        public Delegate Function => function;
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
        public bool Equals(string other)
            => other == signal;

        /// <summary>Performs the comparison between <see cref="MathOperator"/> and <see cref="byte"/>.</summary>
        public bool Equals(byte other)
            => other == executionLevel;

        /// <summary>Performs the comparison between <see cref="MathOperator"/> and <see cref="SignalOrientation"/>.</summary>
        public bool Equals(SignalOrientation other)
            => other == orientation;

        /// <summary>Return the hash code for this instance.</summary>
        public override int GetHashCode()
            => base.GetHashCode();

        /// <summary>Performs the comparison between <see cref="MathOperator"/> and <see cref="object"/>.</summary>
        public override bool Equals(object obj) 
            => (obj is string str && Equals(str)) ||
                (obj is byte bt && Equals(bt)) ||
                (obj is SignalOrientation sot && Equals(sot));

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
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
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    }
}