using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Representa os tipos diferentes que podem representar uma instância de <see cref="T:System.Windows.Media.Animation.KeyTime" />.</summary>
	// Token: 0x02000573 RID: 1395
	public enum KeyTimeType : byte
	{
		/// <summary>Especifica que o tempo total alocado para uma sequência de animação é dividido igualmente entre cada um dos quadros chave.</summary>
		// Token: 0x040017A4 RID: 6052
		Uniform,
		/// <summary>Especifica que cada valor <see cref="T:System.Windows.Media.Animation.KeyTime" /> é expresso como um percentual do tempo total alocado para uma sequência de animação determinada.</summary>
		// Token: 0x040017A5 RID: 6053
		Percent,
		/// <summary>Especifica que cada <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.KeyTime" /> é expresso como um valor <see cref="P:System.Windows.Media.Animation.KeyTime.TimeSpan" /> relativo ao <see cref="P:System.Windows.Media.Animation.Timeline.BeginTime" /> de uma sequência de animação.</summary>
		// Token: 0x040017A6 RID: 6054
		TimeSpan,
		/// <summary>Especifica que os KeyFrames adjacentes recebem, cada um, uma fatia de tempo proporcional à sua extensão, respectivamente.  A meta geral é produzir um valor de comprimento que acompanhe o ritmo da constante de sequência de animação.</summary>
		// Token: 0x040017A7 RID: 6055
		Paced
	}
}
