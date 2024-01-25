using System;
using System.Text;

namespace Cobilas.IO.Atlf.Text {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public class ATLFVS10Encoding : ATLFEncoding {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        /// <inheritdoc/>
        public override string Version => "std:1.0";

        /// <inheritdoc/>
        /// <param name="args">
        /// <para>args[0] = <seealso cref="ATLFNode"/>[]</para>
        /// </param>
        public override string Writer(params object[] args)
            => Writer((ATLFNode[])args[0]);

        /// <inheritdoc/>
        /// <param name="args">
        /// <para>args[0] = <seealso cref="ATLFNode"/>[]</para>
        /// <para>args[1] = <seealso cref="Encoding"/></para>
        /// </param>
        public override byte[] Writer4Byte(params object[] args) {
            if (!(args[1] is null))
                return (args[1] as Encoding).GetBytes(Writer((ATLFNode[])args[0]));
            return Array.Empty<byte>();
        }

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        protected virtual string Writer(ATLFNode[] nodes) {
            StringBuilder builder = new StringBuilder();
            foreach (var item in nodes) {
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
            }
            return builder.ToString();
        }
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    }
}