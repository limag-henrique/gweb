using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa uma classe abstrata para definir os marcadores de texto.</summary>
	// Token: 0x020005AD RID: 1453
	public abstract class TextMarkerProperties
	{
		/// <summary>Obtém a distância do início da linha até o final do símbolo de marcador de texto.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa o deslocamento do símbolo de marcador desde o início da linha.</returns>
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06004284 RID: 17028
		public abstract double Offset { get; }

		/// <summary>Obtém o <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> que representa a fonte do texto é executado para o símbolo de marcador.</summary>
		/// <returns>O <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> que representa o marcador de texto.</returns>
		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06004285 RID: 17029
		public abstract TextSource TextSource { get; }
	}
}
