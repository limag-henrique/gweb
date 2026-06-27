using System;

namespace System.Windows
{
	/// <summary>Descreve como a linha de base para um elemento baseado em texto é posicionado no eixo vertical, em relação à linha de base estabelecida para texto.</summary>
	// Token: 0x020001E1 RID: 481
	public enum BaselineAlignment
	{
		/// <summary>Uma linha de base que está alinhada à borda superior da caixa delimitadora.</summary>
		// Token: 0x0400075A RID: 1882
		Top,
		/// <summary>Uma linha de base que está alinhada ao centro da caixa delimitadora.</summary>
		// Token: 0x0400075B RID: 1883
		Center,
		/// <summary>Uma linha de base que está alinhada à borda inferior da caixa delimitadora.</summary>
		// Token: 0x0400075C RID: 1884
		Bottom,
		/// <summary>Uma linha de base que está alinhada à linha de base real da caixa delimitadora.</summary>
		// Token: 0x0400075D RID: 1885
		Baseline,
		/// <summary>Uma linha de base que está alinhada à borda superior da linha de base do texto.</summary>
		// Token: 0x0400075E RID: 1886
		TextTop,
		/// <summary>Uma linha de base que está alinhada à borda inferior da linha de base do texto.</summary>
		// Token: 0x0400075F RID: 1887
		TextBottom,
		/// <summary>Uma linha de base que está alinhada à posição de subscrito da caixa delimitadora.</summary>
		// Token: 0x04000760 RID: 1888
		Subscript,
		/// <summary>Uma linha de base que está alinhada à posição de sobrescrito da caixa delimitadora.</summary>
		// Token: 0x04000761 RID: 1889
		Superscript
	}
}
