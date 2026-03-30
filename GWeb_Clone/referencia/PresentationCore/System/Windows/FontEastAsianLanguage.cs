using System;

namespace System.Windows
{
	/// <summary>Fornece um mecanismo para que o usuário selecione versões de glifos específicas a uma fonte para um idioma ou sistema de escrita do Leste Asiático especificado.</summary>
	// Token: 0x020001EE RID: 494
	public enum FontEastAsianLanguage
	{
		/// <summary>Nenhuma versão de glifo específica a uma fonte é aplicada.</summary>
		// Token: 0x040007A5 RID: 1957
		Normal,
		/// <summary>Substitui os glifos de japonês pelas formas correspondentes da especificação JIS78.</summary>
		// Token: 0x040007A6 RID: 1958
		Jis78,
		/// <summary>Substitui os glifos de japonês pelas formas correspondentes da especificação JIS83.</summary>
		// Token: 0x040007A7 RID: 1959
		Jis83,
		/// <summary>Substitui os glifos de japonês pelas formas correspondentes da especificação JIS90.</summary>
		// Token: 0x040007A8 RID: 1960
		Jis90,
		/// <summary>Substitui os glifos de japonês pelas formas correspondentes da especificação JIS04.</summary>
		// Token: 0x040007A9 RID: 1961
		Jis04,
		/// <summary>Substitui os glifos padrão pelas formas correspondentes da especificação Hojo Kanji.</summary>
		// Token: 0x040007AA RID: 1962
		HojoKanji,
		/// <summary>Substitui os glifos padrão pelas formas correspondentes da especificação NLC Kanji.</summary>
		// Token: 0x040007AB RID: 1963
		NlcKanji,
		/// <summary>Substitui os formatos tradicionais em chinês ou japonês por suas formas simplificadas correspondentes.</summary>
		// Token: 0x040007AC RID: 1964
		Simplified,
		/// <summary>Substitui as formas simplificadas em chinês ou japonês por suas formas tradicionais correspondentes.</summary>
		// Token: 0x040007AD RID: 1965
		Traditional,
		/// <summary>Substitui as formas simplificadas em Kanji por suas formas tradicionais correspondentes. Esse conjunto de glifos é explicitamente limitado às formas tradicionais consideradas adequadas para uso em nomes de pessoas.</summary>
		// Token: 0x040007AE RID: 1966
		TraditionalNames
	}
}
