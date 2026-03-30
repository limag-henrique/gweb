using System;

namespace System.Windows.Media.Effects
{
	/// <summary>Descreve o kernel usado para criar o efeito.</summary>
	// Token: 0x02000617 RID: 1559
	public enum KernelType
	{
		/// <summary>Uma curva distribuída que cria uma distribuição suave para um desfoque.</summary>
		// Token: 0x04001A29 RID: 6697
		Gaussian,
		/// <summary>Um desfoque simples criado com uma curva de distribuição quadrada.</summary>
		// Token: 0x04001A2A RID: 6698
		Box
	}
}
