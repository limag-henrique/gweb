using System;

namespace System.Windows
{
	/// <summary>Descreve a condição de interrupção ao redor de um objeto embutido.</summary>
	// Token: 0x020001E4 RID: 484
	public enum LineBreakCondition
	{
		/// <summary>Interromper se não for proibido por outro objeto.</summary>
		// Token: 0x0400076B RID: 1899
		BreakDesired,
		/// <summary>Interromper se for permitido por outro objeto.</summary>
		// Token: 0x0400076C RID: 1900
		BreakPossible,
		/// <summary>A interrupção é sempre proibida, a menos que o outro objeto seja definido para <see cref="F:System.Windows.LineBreakCondition.BreakAlways" />.</summary>
		// Token: 0x0400076D RID: 1901
		BreakRestrained,
		/// <summary>A interrupção sempre é permitida.</summary>
		// Token: 0x0400076E RID: 1902
		BreakAlways
	}
}
