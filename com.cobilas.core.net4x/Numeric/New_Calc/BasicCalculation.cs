using System;

namespace Cobilas.Numeric {
    public class BasicCalculation : CalculationsCollection {
        public override CalculationsCollectionItem[] Calculations { get; protected set; }
        public override CalculationsCollectionItem[] OverwriteCalculations { get; protected set; }

        public override void Initialization() {
            Calculations = new CalculationsCollectionItem[] {
                new CalculationsCollectionItem('+', Add, SignalOrientation.both),
                new CalculationsCollectionItem('-', Subtraction, SignalOrientation.both),
                new CalculationsCollectionItem("~-", Subtraction, SignalOrientation.right),
                new CalculationsCollectionItem('*', Multiplication, SignalOrientation.both),
                new CalculationsCollectionItem('/', Division, SignalOrientation.both),
                new CalculationsCollectionItem('%', Module, SignalOrientation.both),
                new CalculationsCollectionItem("++", S_Add, SignalOrientation.left),
                new CalculationsCollectionItem("--", S_Subtraction, SignalOrientation.left),
                new CalculationsCollectionItem("**", S_Multiplication, SignalOrientation.left),
                new CalculationsCollectionItem("//", S_Division, SignalOrientation.left),
                new CalculationsCollectionItem("%%", S_Module, SignalOrientation.left),
                new CalculationsCollectionItem("pow", Pow, SignalOrientation.both),
                new CalculationsCollectionItem("sqr", Sqrt, SignalOrientation.right),
                new CalculationsCollectionItem("cos", Cos, SignalOrientation.right),
                new CalculationsCollectionItem("acos", Acos, SignalOrientation.right),
                new CalculationsCollectionItem("sin", Sin, SignalOrientation.right),
                new CalculationsCollectionItem("asin", Asin, SignalOrientation.right),
                new CalculationsCollectionItem("log", Log, SignalOrientation.right),
                new CalculationsCollectionItem("log2", Log2, SignalOrientation.both),
                new CalculationsCollectionItem("log10", Log10, SignalOrientation.right)
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