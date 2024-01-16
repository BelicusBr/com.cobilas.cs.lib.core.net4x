namespace Cobilas;
public readonly struct FramTest {
    #if NET472_OR_GREATER
    public static string FramNet472 => "Net472";
    #endif
    #if NETSTANDARD2_0_OR_GREATER
    public static string FramNetstd20 => "Netstd20";
    #endif
}