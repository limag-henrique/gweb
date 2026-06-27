using System;

namespace System.Windows.Media
{
	/// <summary>Especifica como desenhar um gradiente fora do espaço ou vetor de gradiente de um pincel de gradiente.</summary>
	// Token: 0x020003B3 RID: 947
	public enum GradientSpreadMethod
	{
		/// <summary>Valor padrão. Os valores de cor nas extremidades do vetor de gradiente preenchem o espaço restante.</summary>
		// Token: 0x04001165 RID: 4453
		Pad,
		/// <summary>O gradiente é repetido na direção inversa até o espaço ser preenchido.</summary>
		// Token: 0x04001166 RID: 4454
		Reflect,
		/// <summary>O gradiente é repetido na direção original até o espaço ser preenchido.</summary>
		// Token: 0x04001167 RID: 4455
		Repeat
	}
}
