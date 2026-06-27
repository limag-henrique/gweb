using System;

namespace System.Windows.Media
{
	/// <summary>Define uma classe de enumerador que especifica o tipo de substituição de número a executar em números em uma sequência de texto.</summary>
	// Token: 0x02000445 RID: 1093
	public enum NumberSubstitutionMethod
	{
		/// <summary>Padrão. Especifica que o método de substituição deve ser determinado com base no valor da propriedade <see cref="P:System.Globalization.NumberFormatInfo.DigitSubstitution" /> da cultura do número.</summary>
		// Token: 0x0400145D RID: 5213
		AsCulture,
		/// <summary>Se a cultura de número for uma cultura árabe ou persa, especifica que os dígitos dependem do contexto. Dígitos tradicionais ou latinos são usados dependendo do caractere forte precedente mais próximo ou, se não houver nenhum, a direção do texto do parágrafo.</summary>
		// Token: 0x0400145E RID: 5214
		Context,
		/// <summary>Especifica que os pontos de código 0x39–0x30 são sempre renderizados como dígitos europeus, caso em que nenhuma substituição de número é executada.</summary>
		// Token: 0x0400145F RID: 5215
		European,
		/// <summary>Especifica que os números são renderizados usando os dígitos nacionais para a cultura de número, conforme especificado pelo valor da propriedade <see cref="P:System.Globalization.NumberFormatInfo.NativeDigits" /> da cultura.</summary>
		// Token: 0x04001460 RID: 5216
		NativeNational,
		/// <summary>Especifica que os números são renderizados usando os dígitos tradicionais para a cultura de número. Para a maioria das culturas, isso é igual ao valor de enumeração <see cref="F:System.Globalization.DigitShapes.NativeNational" />. No entanto, usar <see cref="F:System.Windows.Media.NumberSubstitutionMethod.NativeNational" /> pode resultar em dígitos latinos para algumas culturas árabes, enquanto usar <see cref="F:System.Windows.Media.NumberSubstitutionMethod.Traditional" /> resulta em dígitos árabes para todas as culturas árabes.</summary>
		// Token: 0x04001461 RID: 5217
		Traditional
	}
}
