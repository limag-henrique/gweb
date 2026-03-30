using System;

namespace System.Windows.Media
{
	/// <summary>Determina como as bordas de primitivas não texto são renderizadas.</summary>
	// Token: 0x020003A7 RID: 935
	public enum EdgeMode
	{
		/// <summary>Nenhum modo de borda foi especificado. Não altere o modo de borda atual de primitivas desenhadas não texto. Este é o valor padrão.</summary>
		// Token: 0x04001134 RID: 4404
		Unspecified,
		/// <summary>Renderiza as bordas de primitivas desenhadas não texto como bordas com alias.</summary>
		// Token: 0x04001135 RID: 4405
		Aliased
	}
}
