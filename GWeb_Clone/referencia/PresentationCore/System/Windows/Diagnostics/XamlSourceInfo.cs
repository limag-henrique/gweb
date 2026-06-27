using System;
using System.Diagnostics;

namespace System.Windows.Diagnostics
{
	/// <summary>Representa informações sobre um documento de origem XAML de um objeto.</summary>
	// Token: 0x0200031C RID: 796
	[DebuggerDisplay("{line={LineNumber}, offset={LinePosition}, uri={SourceUri}}")]
	public class XamlSourceInfo
	{
		/// <summary>Obtém o URI do documento de origem no qual o elemento é declarado.</summary>
		/// <returns>O URI do documento de origem no qual o elemento é declarado.</returns>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00067944 File Offset: 0x00066D44
		// (set) Token: 0x06001A36 RID: 6710 RVA: 0x00067958 File Offset: 0x00066D58
		public Uri SourceUri { get; private set; }

		/// <summary>Obtém o número de linha no documento de origem no qual o elemento é declarado.</summary>
		/// <returns>O número de linha no documento de origem no qual o elemento é declarado.</returns>
		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0006796C File Offset: 0x00066D6C
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x00067980 File Offset: 0x00066D80
		public int LineNumber { get; private set; }

		/// <summary>Obtém a posição na linha no documento de origem em que o elemento é declarado.</summary>
		/// <returns>A posição na linha no documento de origem em que o elemento é declarado.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x00067994 File Offset: 0x00066D94
		// (set) Token: 0x06001A3A RID: 6714 RVA: 0x000679A8 File Offset: 0x00066DA8
		public int LinePosition { get; private set; }

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Diagnostics.XamlSourceInfo" />.</summary>
		/// <param name="sourceUri">O URI do documento de origem no qual o elemento é declarado.</param>
		/// <param name="lineNumber">O número de linha no documento de origem no qual o elemento é declarado.</param>
		/// <param name="linePosition">A posição na linha no documento de origem em que o elemento é declarado.</param>
		// Token: 0x06001A3B RID: 6715 RVA: 0x000679BC File Offset: 0x00066DBC
		public XamlSourceInfo(Uri sourceUri, int lineNumber, int linePosition)
		{
			this.SourceUri = sourceUri;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}
