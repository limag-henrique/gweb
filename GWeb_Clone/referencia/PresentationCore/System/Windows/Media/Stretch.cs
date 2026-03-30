using System;

namespace System.Windows.Media
{
	/// <summary>Descreve como o conteúdo é redimensionado para se ajustar a seu espaço alocado.</summary>
	// Token: 0x020003F1 RID: 1009
	public enum Stretch
	{
		/// <summary>O conteúdo preserva seu tamanho original.</summary>
		// Token: 0x04001265 RID: 4709
		None,
		/// <summary>O conteúdo é redimensionado para se ajustar às dimensões de destino. A taxa de proporção não é preservada.</summary>
		// Token: 0x04001266 RID: 4710
		Fill,
		/// <summary>O conteúdo é redimensionado para se ajustar nas dimensões de destino, enquanto preserva sua taxa de proporção nativa.</summary>
		// Token: 0x04001267 RID: 4711
		Uniform,
		/// <summary>O conteúdo é redimensionado para se ajustar nas dimensões de destino, enquanto preserva sua taxa de proporção nativa. Se a taxa de proporção do retângulo de destino for diferente da origem, o conteúdo de origem será cortado para se ajustar às dimensões de destino.</summary>
		// Token: 0x04001268 RID: 4712
		UniformToFill
	}
}
