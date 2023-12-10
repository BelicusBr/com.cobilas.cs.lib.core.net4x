using System;

namespace Cobilas.Numeric {
    public class BasicCalculation : CalculationsCollection {
        public override MathOperator[] Calculations { get; protected set; }
        public override MathOperator[] OverwriteCalculations { get; protected set; }

        public override void Initialization() {
            Calculations = new MathOperator[] {
                new MathOperator("add", Add, SignalOrientation.both, 2),
                new MathOperator("sub", Subtraction, SignalOrientation.both, 2),
                new MathOperator("ngtv", Negative, SignalOrientation.right),
                new MathOperator("mult", Multiplication, SignalOrientation.both, 1),
                new MathOperator("div", Division, SignalOrientation.both, 1),
                new MathOperator("mod", Module, SignalOrientation.both, 1),
                new MathOperator("incr", S_Add, SignalOrientation.left, 2),
                new MathOperator("decr", S_Subtraction, SignalOrientation.left, 2),
                new MathOperator("mult+", S_Multiplication, SignalOrientation.left, 1),
                new MathOperator("div+", S_Division, SignalOrientation.left, 1),
                new MathOperator("mod+", S_Module, SignalOrientation.left, 1),
                new MathOperator("pow", Pow, SignalOrientation.both),
                new MathOperator("sqr", Sqrt, SignalOrientation.right),
                new MathOperator("cos", Cos, SignalOrientation.right),
                new MathOperator("acos", Acos, SignalOrientation.right),
                new MathOperator("sin", Sin, SignalOrientation.right),
                new MathOperator("asin", Asin, SignalOrientation.right),
                new MathOperator("log", Log, SignalOrientation.right),
                new MathOperator("log2", Log2, SignalOrientation.both),
                new MathOperator("log10", Log10, SignalOrientation.right)
            };
        }

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
        private double Log2(double a, double b) => Math.Log(a, b);
        private double Log10(double a, double b) => Math.Log10(b);
    }
}