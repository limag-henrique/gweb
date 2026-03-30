using System;

namespace System.Windows.Media
{
	/// <summary>Especifica qual algoritmo é usado para dimensionar imagens bitmap.</summary>
	// Token: 0x020003A8 RID: 936
	public enum BitmapScalingMode
	{
		/// <summary>Use o modo de escala de bitmap padrão, que é <see cref="F:System.Windows.Media.BitmapScalingMode.Linear" />.</summary>
		// Token: 0x04001137 RID: 4407
		Unspecified,
		/// <summary>Use a escala de bitmap bilinear, que é mais rápida que o modo <see cref="F:System.Windows.Media.BitmapScalingMode.HighQuality" />, mas produz uma saída de qualidade inferior. O modo <see cref="F:System.Windows.Media.BitmapScalingMode.LowQuality" /> é o mesmo que o <see cref="F:System.Windows.Media.BitmapScalingMode.Linear" />.</summary>
		// Token: 0x04001138 RID: 4408
		LowQuality,
		/// <summary>Use a escala de bitmap de alta qualidade, que é mais lenta que o modo <see cref="F:System.Windows.Media.BitmapScalingMode.LowQuality" />, mas produz uma saída de qualidade superior. O modo <see cref="F:System.Windows.Media.BitmapScalingMode.HighQuality" /> é o mesmo que o <see cref="F:System.Windows.Media.BitmapScalingMode.Fant" />.</summary>
		// Token: 0x04001139 RID: 4409
		HighQuality,
		/// <summary>Use a escala de bitmap linear, que é mais rápida que o modo <see cref="F:System.Windows.Media.BitmapScalingMode.HighQuality" />, mas produz uma saída de qualidade inferior.</summary>
		// Token: 0x0400113A RID: 4410
		Linear = 1,
		/// <summary>Use a escala de bitmap Fant de altíssima qualidade, que é a mais lenta entre todos os modos de escala de bitmap, mas produz uma saída de qualidade superior.</summary>
		// Token: 0x0400113B RID: 4411
		Fant,
		/// <summary>Use a escala de bitmap do vizinho mais próximo, que fornece benefícios de desempenho sobre o modo <see cref="F:System.Windows.Media.BitmapScalingMode.LowQuality" /> quando o rasterizador de software é usado. Esse modo geralmente é usado para ampliar um bitmap.</summary>
		// Token: 0x0400113C RID: 4412
		NearestNeighbor
	}
}
