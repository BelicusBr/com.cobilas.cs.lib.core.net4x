using System;

namespace Cobilas.Numeric { 
    /// <summary>Basic functions of mathematical operations.</summary>
    public class BasicCalculation : CalculationsCollection {
        /// <inheritdoc/>
        public override MathOperator[] Calculations { get; protected set; } = Array.Empty<MathOperator>();
        /// <inheritdoc/>
        public override MathOperator[] OverwriteCalculations { get; protected set; } = Array.Empty<MathOperator>();

        /// <inheritdoc/>
        public override void Initialization() {
            Calculations = new MathOperator[] {
                new("~-", Negative, SignalOrientation.right),
                new("++", S_Add, SignalOrientation.left, 2),
                new("--", S_Subtraction, SignalOrientation.left, 2),
                new("**", S_Multiplication, SignalOrientation.left, 1),
                new("//", S_Division, SignalOrientation.left, 1),
                new("%%", S_Module, SignalOrientation.left, 1),
                new('+', Add, SignalOrientation.both, 2),
                new('-', Subtraction, SignalOrientation.both, 2),
                new('*', Multiplication, SignalOrientation.both, 1),
                new('/', Division, SignalOrientation.both, 1),
                new('%', Module, SignalOrientation.both, 1),
                new(":pow:", Pow, SignalOrientation.both),
                new(":sqr:", Sqrt, SignalOrientation.right),
                new(":cos:", Cos, SignalOrientation.right),
                new(":acos:", Acos, SignalOrientation.right),
                new(":sin:", Sin, SignalOrientation.right),
                new(":asin:", Asin, SignalOrientation.right),
                new(":log:", Log, SignalOrientation.right),
                new(":log-nb:", Log_nb, SignalOrientation.both),
                new(":log10:", Log10, SignalOrientation.right)
            };
        }

        /// <inheritdoc/>
        public override double Clac(double V1, string S, double V2) {
            foreach (var item in Calculations)
                if (item.Signal == S)
                    return (double)item.Function.DynamicInvoke(V1, V2);
            return 0d;
        }

        private double Add(double a, double b) => a + b;
        private double Subtraction(double a, double b) => a - b;
        private double Multiplication(double a, double b) => a * b;
        private double Division(double a, double b) => a / b;
        private double Module(double a, double b) => a % b;

        private double Negative(double a, double b) => -b;

        private double S_Add(double a, double b) => a + 1;
        private double S_Subtraction(double a, double b) => a - 1;
        private double S_Multiplication(double a, double b) => a * 2;
        private double S_Division(double a, double b) => a / 2;
        private double S_Module(double a, double b) => a % 2;

        private double Pow(double a, double b) => Math.Pow(a, b);
        private double Sqrt(double a, double b) => Math.Sqrt(b);
        private double Cos(double a, double b) => Math.Cos(b);
        private double Acos(double a, double b) => Math.Acos(b);
        private double Sin(double a, double b) => Math.Sin(b);
        private double Asin(double a, double b) => Math.Asin(b);
        private double Log(double a, double b) => Math.Log(b);
        private double Log_nb(double a, double b) => Math.Log(a, b);
        private double Log10(double a, double b) => Math.Log10(b);
    }
}