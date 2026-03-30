using System;

namespace System.Windows
{
	/// <summary>Descreve a aparência de estilo de marcador de um item de lista.</summary>
	// Token: 0x020001E5 RID: 485
	public enum TextMarkerStyle
	{
		/// <summary>Sem marcador.</summary>
		// Token: 0x04000770 RID: 1904
		None,
		/// <summary>Um círculo de disco sólido.</summary>
		// Token: 0x04000771 RID: 1905
		Disc,
		/// <summary>Um círculo de disco vazado.</summary>
		// Token: 0x04000772 RID: 1906
		Circle,
		/// <summary>Uma forma quadrada vazada.</summary>
		// Token: 0x04000773 RID: 1907
		Square,
		/// <summary>Uma caixa quadrada sólida.</summary>
		// Token: 0x04000774 RID: 1908
		Box,
		/// <summary>Um numeral romano em minúsculas que começa com o numeral i. Por exemplo, i, ii, iii e iv. O numeral é incrementado automaticamente para cada item adicionado à lista.</summary>
		// Token: 0x04000775 RID: 1909
		LowerRoman,
		/// <summary>Um numeral romano em maiúsculas que começa com o numeral I. Por exemplo, I, II, III e IV. O valor numérico é incrementado automaticamente para cada item adicionado à lista.</summary>
		// Token: 0x04000776 RID: 1910
		UpperRoman,
		/// <summary>Um caractere ASCII minúsculo que começa com a letra a. Por exemplo, a, b e c. O valor do caractere é incrementado automaticamente para cada item adicionado à lista.</summary>
		// Token: 0x04000777 RID: 1911
		LowerLatin,
		/// <summary>Um caractere ASCII em maiúsculas que começa com a letra A. Por exemplo, A, B e C. O valor do caractere é incrementado automaticamente para cada item adicionado à lista.</summary>
		// Token: 0x04000778 RID: 1912
		UpperLatin,
		/// <summary>Um decimal iniciado com o número um. Por exemplo, 1, 2 e 3. O valor decimal é incrementado automaticamente para cada item adicionado à lista.</summary>
		// Token: 0x04000779 RID: 1913
		Decimal
	}
}
