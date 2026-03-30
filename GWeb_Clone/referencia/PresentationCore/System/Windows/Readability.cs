using System;

namespace System.Windows
{
	/// <summary>Especifica o valor da legibilidade de um <see cref="T:System.Windows.LocalizabilityAttribute" /> para uma classe ou membro de classe BAML (XAML binário).</summary>
	// Token: 0x020001D2 RID: 466
	public enum Readability
	{
		/// <summary>Valor de destino não é legível.</summary>
		// Token: 0x0400072D RID: 1837
		Unreadable,
		/// <summary>O valor almejado é um texto legível.</summary>
		// Token: 0x0400072E RID: 1838
		Readable,
		/// <summary>A legibilidade do valor de destino é herdada do respectivo nó pai.</summary>
		// Token: 0x0400072F RID: 1839
		Inherit
	}
}
