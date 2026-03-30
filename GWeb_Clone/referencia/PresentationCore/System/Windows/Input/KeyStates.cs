using System;

namespace System.Windows.Input
{
	/// <summary>Especifica as constantes que definem o estado de uma tecla.</summary>
	// Token: 0x0200026E RID: 622
	[Flags]
	public enum KeyStates : byte
	{
		/// <summary>A tecla não está pressionada.</summary>
		// Token: 0x040009A0 RID: 2464
		None = 0,
		/// <summary>A tecla está pressionada.</summary>
		// Token: 0x040009A1 RID: 2465
		Down = 1,
		/// <summary>A tecla é alternada.</summary>
		// Token: 0x040009A2 RID: 2466
		Toggled = 2
	}
}
