using System;

namespace Cobilas.Numeric {
    public readonly struct CalculationsCollectionItem {

        private readonly string signal;
        private readonly SignalOrientation orientation;
        private readonly Func<double, double, double> function;

        public string Signal => signal;
        public Delegate Function => function;
        public SignalOrientation Orientation => orientation;

        public CalculationsCollectionItem(
            string signal, Func<double, double, double> function, SignalOrientation orientation) {
            this.signal = signal;
            this.function = function;
            this.orientation = orientation;
        }

        public CalculationsCollectionItem(
            char signal, Func<double, double, double> function, SignalOrientation orientation) :
            this(signal.ToString(), function, orientation) {}
    }
}