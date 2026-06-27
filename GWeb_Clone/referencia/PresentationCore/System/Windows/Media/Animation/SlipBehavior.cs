using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Indica como um <see cref="T:System.Windows.Media.Animation.ParallelTimeline" /> se comportará quando um ou mais dos seus <see cref="T:System.Windows.Media.Animation.Timeline" /> filhos sair.</summary>
	// Token: 0x0200057A RID: 1402
	public enum SlipBehavior
	{
		/// <summary>Indica que um <see cref="T:System.Windows.Media.Animation.ParallelTimeline" /> não sairá com o <see cref="T:System.Windows.Media.Animation.Timeline" /> filho, mas se expandirá para ajustar todos os filhos de <see cref="T:System.Windows.Media.Animation.Timeline" /> em processo de saída. OBSERVAÇÃO: Isso só é eficaz quando o <see cref="P:System.Windows.Media.Animation.Timeline.Duration" /> do <see cref="T:System.Windows.Media.Animation.ParallelTimeline" /> não é especificado explicitamente.</summary>
		// Token: 0x040017BD RID: 6077
		Grow,
		/// <summary>Indica que um <see cref="T:System.Windows.Media.Animation.ParallelTimeline" /> sairá junto com seu primeiro <see cref="T:System.Windows.Media.Animation.Timeline" /> filho que pode sair sempre que o filho está atrasado ou acelerado.</summary>
		// Token: 0x040017BE RID: 6078
		Slip
	}
}
