using System;

namespace System.Windows.Media
{
	/// <summary>Especifica como a cultura para números em uma execução de texto é determinada.</summary>
	// Token: 0x02000444 RID: 1092
	public enum NumberCultureSource
	{
		/// <summary>Padrão. A cultura de número é derivada do valor da propriedade <see cref="P:System.Windows.Media.TextFormatting.TextRunProperties.CultureInfo" />, que é a cultura da execução de texto. Na marcação, isso é representado pelo atributo xml:lang.</summary>
		// Token: 0x04001459 RID: 5209
		Text,
		/// <summary>A cultura de número é derivada do valor de cultura do thread atual, que, por padrão, é a cultura padrão do usuário.</summary>
		// Token: 0x0400145A RID: 5210
		User,
		/// <summary>A cultura de número é derivada da propriedade <see cref="P:System.Windows.Media.NumberSubstitution.CultureOverride" />.</summary>
		// Token: 0x0400145B RID: 5211
		Override
	}
}
