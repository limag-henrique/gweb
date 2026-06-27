using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Especifica como um <see cref="T:System.Windows.Media.Animation.Timeline" /> se comporta quando está fora de seu período ativo, mas seu pai está no período ativo ou de retenção.</summary>
	// Token: 0x020004AE RID: 1198
	public enum FillBehavior
	{
		/// <summary>Depois de atingir o final do seu período ativo, a linha do tempo mantém seu progresso até o final dos períodos de atividade e retenção do pai.</summary>
		// Token: 0x0400164B RID: 5707
		HoldEnd,
		/// <summary>A linha do tempo será interrompida se ele estiver fora de seu período ativo enquanto seu pai está no período ativo.</summary>
		// Token: 0x0400164C RID: 5708
		Stop
	}
}
