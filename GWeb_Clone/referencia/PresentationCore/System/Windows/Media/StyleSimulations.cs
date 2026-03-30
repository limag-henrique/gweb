using System;

namespace System.Windows.Media
{
	/// <summary>Define uma classe de enumerador que descreve o estilo de simulação de uma fonte.</summary>
	// Token: 0x02000442 RID: 1090
	[Flags]
	public enum StyleSimulations
	{
		/// <summary>Sem simulação de estilo da fonte.</summary>
		// Token: 0x0400144E RID: 5198
		None = 0,
		/// <summary>Simulação de estilo em negrito.</summary>
		// Token: 0x0400144F RID: 5199
		BoldSimulation = 1,
		/// <summary>Simulação de estilo em itálico.</summary>
		// Token: 0x04001450 RID: 5200
		ItalicSimulation = 2,
		/// <summary>Simulação de estilo em negrito e itálico.</summary>
		// Token: 0x04001451 RID: 5201
		BoldItalicSimulation = 3
	}
}
