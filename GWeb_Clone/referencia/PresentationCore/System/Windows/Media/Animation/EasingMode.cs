using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Define os modos em que classes derivadas de <see cref="T:System.Windows.Media.Animation.EasingFunctionBase" /> executam seu easing.</summary>
	// Token: 0x0200058E RID: 1422
	public enum EasingMode
	{
		/// <summary>A interpolação segue a fórmula matemática associada à função de easing.</summary>
		// Token: 0x040017F2 RID: 6130
		EaseIn,
		/// <summary>Interpolação segue 100% de interpolação menos a saída da fórmula associada à função de easing.</summary>
		// Token: 0x040017F3 RID: 6131
		EaseOut,
		/// <summary>Usa interpolação <see cref="F:System.Windows.Media.Animation.EasingMode.EaseIn" /> para a primeira metade da animação e <see cref="F:System.Windows.Media.Animation.EasingMode.EaseOut" /> para a segunda metade.</summary>
		// Token: 0x040017F4 RID: 6132
		EaseInOut
	}
}
