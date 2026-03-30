using System;

namespace System.Windows
{
	/// <summary>Fornece um mecanismo para que o usuário selecione glifos com estilos de larguras diferentes.</summary>
	// Token: 0x020001ED RID: 493
	public enum FontEastAsianWidths
	{
		/// <summary>Estilo de largura padrão.</summary>
		// Token: 0x0400079E RID: 1950
		Normal,
		/// <summary>Substitui glifos de largura uniforme por glifos proporcionalmente espaçados.</summary>
		// Token: 0x0400079F RID: 1951
		Proportional,
		/// <summary>Substitui glifos de largura uniforme por glifos de largura inteira (normalmente em).</summary>
		// Token: 0x040007A0 RID: 1952
		Full,
		/// <summary>Substitui glifos de largura uniforme por glifos de meia largura (meio em).</summary>
		// Token: 0x040007A1 RID: 1953
		Half,
		/// <summary>Substitui glifos de largura uniforme por glifos com um terço da largura (um terço de em).</summary>
		// Token: 0x040007A2 RID: 1954
		Third,
		/// <summary>Substitui glifos de largura uniforme por glifos com um quarto da largura (um quarto de em).</summary>
		// Token: 0x040007A3 RID: 1955
		Quarter
	}
}
