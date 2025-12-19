namespace Cobilas.Numeric;  
/// <summary>Indicates the direction where the operation numbers will be obtained.</summary>
public enum SignalOrientation : byte {
    /// <summary>None</summary>
    none = 0,
    /// <summary>2&lt;+&gt;2=4 Need two numbers on the sign side.</summary>
    both = 1,
    /// <summary>2&lt;sqr=4 You need the numbers on the left side of the sign.</summary>
    left = 2,
    /// <summary>sqr&gt;2=4 You need the numbers on the right side of the sign.</summary>
    right = 3
}