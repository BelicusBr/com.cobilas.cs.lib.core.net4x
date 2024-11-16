namespace Cobilas.IO.Atlf.Text;
/// <summary>This class is a null representation of an ATLF encoder.</summary>
public sealed class NullATLFEncoding : ATLFEncoding, INullObject {
    /// <inheritdoc/>
    public override string Version => string.Empty;

    /// <inheritdoc/>
    public override string Writer(params object[] args) => string.Empty;
    /// <inheritdoc/>
    public override byte[] Writer4Byte(params object[] args) => [];
}