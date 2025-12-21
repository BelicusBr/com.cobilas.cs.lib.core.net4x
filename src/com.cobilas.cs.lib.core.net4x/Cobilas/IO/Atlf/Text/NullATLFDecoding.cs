namespace Cobilas.IO.Atlf.Text;
/// <summary>This class is a null representation of an ATLF decoder.</summary>
public sealed class NullATLFDecoding : ATLFDecoding, INullObject {
    /// <inheritdoc/>
    public override string Version => string.Empty;

    /// <inheritdoc/>
    public override ATLFNode[] Reader(params object[] args) => [];
    /// <inheritdoc/>
    public override ATLFNode[] Reader4Byte(params object[] args) => [];
    /// <inheritdoc/>
    protected override bool ValidCharacter(char c) => false;
}