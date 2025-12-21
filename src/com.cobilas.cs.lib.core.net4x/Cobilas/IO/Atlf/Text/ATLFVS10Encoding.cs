using System.Text;

namespace Cobilas.IO.Atlf.Text;
/// <summary>This class allows encoding a string in ATLF format in version 1.0</summary>
public class ATLFVS10Encoding : ATLFEncoding {
	/// <inheritdoc/>
	public override string Version => "std:1.0";
	/// <inheritdoc/>
	/// <param name="args">
	/// <para>args[0] = <seealso cref="ATLFNode"/>[]</para>
	/// </param>
	/// <returns>A string containing the ATLF-encoded representation of the nodes.</returns>
	/// <remarks>
	/// The first argument must be an array of <see cref="ATLFNode"/> objects to encode.
	/// </remarks>
	public override string Writer(params object[] args)
		=> Writer((ATLFNode[])args[0]);
	/// <inheritdoc/>
	/// <param name="args">
	/// args[0] = <seealso cref="ATLFNode"/>[]<br/>
	/// args[1] = <seealso cref="Encoding"/>
	/// </param>
	/// <returns>A byte array containing the encoded ATLF data in the specified encoding.</returns>
	/// <remarks>
	/// If the encoding argument is null, an empty byte array is returned.
	/// </remarks>
	public override byte[] Writer4Byte(params object[] args) {
		if (args[1] is Encoding edg && edg is not null)
			return edg.GetBytes(Writer((ATLFNode[])args[0]));
		return [];
	}
	/// <summary>Converts an array of <see cref="ATLFNode"/> objects to an ATLF-formatted string.</summary>
	/// <param name="nodes">The array of nodes to encode.</param>
	/// <returns>An ATLF-formatted string representing the nodes.</returns>
	/// <remarks>
	/// This method processes different node types:<br/>
	/// - <see cref="ATLFNodeType.Tag"/>: Encoded as "#! {name}:/*{value}*/" with "*/" escaped as "\*/"<br/>
	/// - <see cref="ATLFNodeType.Spacing"/>: Outputs the value directly without modification<br/>
	/// - <see cref="ATLFNodeType.Comment"/>: Encoded as "#> {value} &lt;#" with "&lt;#" escaped as "\&lt;#"
	/// </remarks>
	protected virtual string Writer(ATLFNode[] nodes) {
		StringBuilder builder = new();
		foreach (var item in nodes)
			switch (item.NodeType) {
				case ATLFNodeType.Tag:
					builder.AppendFormat("#! {0}:/*{1}*/", item.Name, item.Value.Replace("*/", "\\*/"));
					break;
				case ATLFNodeType.Spacing:
					builder.Append(item.Value);
					break;
				case ATLFNodeType.Comment:
					builder.AppendFormat("#> {0} <#", item.Value.Replace("<#", "\\<#"));
					break;
			}
		return builder.ToString();
	}
}