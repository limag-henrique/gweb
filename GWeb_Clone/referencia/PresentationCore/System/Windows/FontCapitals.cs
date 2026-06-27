using System;

namespace System.Windows
{
	/// <summary>Descreve o estilo de letra maiúscula para um objeto <see cref="T:System.Windows.Documents.Typography" />.</summary>
	// Token: 0x020001E9 RID: 489
	public enum FontCapitals
	{
		/// <summary>As letras maiúsculas são renderizadas normalmente.</summary>
		// Token: 0x0400078A RID: 1930
		Normal,
		/// <summary>As letras maiúsculas e minúsculas são substituídas por uma forma de glifo de uma letra maiúscula com a mesma altura aproximada.</summary>
		// Token: 0x0400078B RID: 1931
		AllSmallCaps,
		/// <summary>As letras minúsculas são substituídas por uma forma de glifo de uma letra maiúscula com a mesma altura aproximada.</summary>
		// Token: 0x0400078C RID: 1932
		SmallCaps,
		/// <summary>As letras maiúsculas e minúsculas são substituídas por uma forma de glifo de uma letra maiúscula com a mesma altura aproximada. Minimaiúsculas são menores que maiúsculas pequenas.</summary>
		// Token: 0x0400078D RID: 1933
		AllPetiteCaps,
		/// <summary>As letras minúsculas são substituídas por uma forma de glifo de uma letra maiúscula com a mesma altura aproximada. Minimaiúsculas são menores que maiúsculas pequenas.</summary>
		// Token: 0x0400078E RID: 1934
		PetiteCaps,
		/// <summary>As letras maiúsculas são exibidas em unicase. As fontes unicase renderizam letras minúsculas e maiúsculas em uma mistura de glifos maiúsculos e minúsculos determinados pelo designer de tipo.</summary>
		// Token: 0x0400078F RID: 1935
		Unicase,
		/// <summary>Os formulários de glifo são substituídos por uma forma tipográfica projetada especificamente para títulos.</summary>
		// Token: 0x04000790 RID: 1936
		Titling
	}
}
