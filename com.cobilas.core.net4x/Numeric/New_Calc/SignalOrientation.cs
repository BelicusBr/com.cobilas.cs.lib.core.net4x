namespace Cobilas.Numeric {
    public enum SignalOrientation {
        /// <summary>2&lt;+&gt;2=4 Need two numbers on the sign side.</summary>
        both = 0,
        /// <summary>2&lt;sqr=4 You need the numbers on the left side of the sign.</summary>
        left = 1,
        /// <summary>sqr&gt;2=4 You need the numbers on the right side of the sign.</summary>
        right = 2
    }
}