using System;

namespace System.Windows.Input
{
	/// <summary>Especifica os possíveis tipos de entrada que estão sendo relatados.</summary>
	// Token: 0x02000265 RID: 613
	public enum InputType
	{
		/// <summary>A entrada foi fornecida por um teclado.</summary>
		// Token: 0x04000976 RID: 2422
		Keyboard,
		/// <summary>A entrada foi fornecida por um mouse.</summary>
		// Token: 0x04000977 RID: 2423
		Mouse,
		/// <summary>A entrada foi fornecida por uma caneta.</summary>
		// Token: 0x04000978 RID: 2424
		Stylus,
		/// <summary>A entrada foi fornecida um Dispositivo de Interface Humana que não era um teclado, mouse ou uma caneta.</summary>
		// Token: 0x04000979 RID: 2425
		Hid,
		/// <summary>A entrada foi fornecida pelo texto.</summary>
		// Token: 0x0400097A RID: 2426
		Text,
		/// <summary>A entrada foi fornecida por um comando.</summary>
		// Token: 0x0400097B RID: 2427
		Command
	}
}
