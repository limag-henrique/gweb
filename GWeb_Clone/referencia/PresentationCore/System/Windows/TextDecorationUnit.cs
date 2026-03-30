using System;

namespace System.Windows
{
	/// <summary>Especifica o tipo de unidade de um <see cref="T:System.Windows.TextDecoration" /><see cref="P:System.Windows.TextDecoration.PenOffset" /> ou um valor de espessura da <see cref="P:System.Windows.TextDecoration.Pen" />.</summary>
	// Token: 0x020001F9 RID: 505
	public enum TextDecorationUnit
	{
		/// <summary>Um valor de unidade relativo à fonte usada para a <see cref="T:System.Windows.TextDecoration" />. Se a decoração abranger várias fontes, uma média de valor recomendado será calculada. Este é o valor padrão.</summary>
		// Token: 0x040007F7 RID: 2039
		FontRecommended,
		/// <summary>Um valor de unidade relativo ao tamanho em da fonte. O valor do deslocamento ou da espessura é igual ao valor de deslocamento ou espessura multiplicado pelo tamanho em da fonte.</summary>
		// Token: 0x040007F8 RID: 2040
		FontRenderingEmSize,
		/// <summary>Um valor de unidade expresso em pixels.</summary>
		// Token: 0x040007F9 RID: 2041
		Pixel
	}
}
