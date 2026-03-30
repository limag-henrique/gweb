using System;
using System.Collections;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma implementação de interface de <see cref="T:System.Windows.Media.Animation.IKeyFrameAnimation" /> fornece acesso não tipado a membros da coleção de quadros-chave.</summary>
	// Token: 0x0200056E RID: 1390
	public interface IKeyFrameAnimation
	{
		/// <summary>Obtém ou define um <see cref="P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames" /> de coleção ordenada associado a esta sequência de animação.</summary>
		/// <returns>Um <see cref="T:System.Collections.IList" /> de <see cref="P:System.Windows.Media.Animation.IKeyFrameAnimation.KeyFrames" />.</returns>
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004070 RID: 16496
		// (set) Token: 0x06004071 RID: 16497
		IList KeyFrames { get; set; }
	}
}
