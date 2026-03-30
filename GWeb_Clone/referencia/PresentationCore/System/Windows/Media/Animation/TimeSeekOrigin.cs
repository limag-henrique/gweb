using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Indica a origem de uma operação de busca. O deslocamento da operação de busca é em relação a essa origem.</summary>
	// Token: 0x020004AC RID: 1196
	public enum TimeSeekOrigin
	{
		/// <summary>O deslocamento é relativo ao início do período de ativação do <see cref="T:System.Windows.Media.Animation.Timeline" />.</summary>
		// Token: 0x04001644 RID: 5700
		BeginTime,
		/// <summary>O deslocamento é relativo ao <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> do <see cref="T:System.Windows.Media.Animation.Timeline" />, o comprimento de uma única iteração. Esse valor não tem nenhum significado se o <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> do <see cref="T:System.Windows.Media.Animation.Timeline" /> não for resolvido.</summary>
		// Token: 0x04001645 RID: 5701
		Duration
	}
}
