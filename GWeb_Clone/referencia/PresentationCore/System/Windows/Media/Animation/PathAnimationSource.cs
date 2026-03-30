using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Especifica o valor da propriedade de saída do caminho usado para orientar a animação.</summary>
	// Token: 0x02000575 RID: 1397
	public enum PathAnimationSource : byte
	{
		/// <summary>Especifica o deslocamento da coordenada X durante a progressão junto com um caminho de sequência de animação.</summary>
		// Token: 0x040017B0 RID: 6064
		X,
		/// <summary>Especifica o deslocamento da coordenada Y durante a progressão junto com um caminho de sequência de animação.</summary>
		// Token: 0x040017B1 RID: 6065
		Y,
		/// <summary>Especifica o ângulo de rotação da tangente durante a progressão junto com um caminho de sequência de animação.</summary>
		// Token: 0x040017B2 RID: 6066
		Angle
	}
}
