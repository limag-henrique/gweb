using System;

namespace System.Windows
{
	/// <summary>Especifica o valor da modificabilidade de um <see cref="T:System.Windows.LocalizabilityAttribute" /> para uma classe ou membro de classe BAML (XAML binário).</summary>
	// Token: 0x020001CB RID: 459
	public enum Modifiability
	{
		/// <summary>O valor de destino não é modificável pelos localizadores.</summary>
		// Token: 0x0400071C RID: 1820
		Unmodifiable,
		/// <summary>O valor de destino é modificável pelos localizadores.</summary>
		// Token: 0x0400071D RID: 1821
		Modifiable,
		/// <summary>A modificabilidade do valor de destino é herdada do respectivo nó pai.</summary>
		// Token: 0x0400071E RID: 1822
		Inherit
	}
}
