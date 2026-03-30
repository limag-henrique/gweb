using System;

namespace System.Windows
{
	/// <summary>Especifica os efeitos de uma operação do tipo "arrastar e soltar".</summary>
	// Token: 0x020001A9 RID: 425
	[Flags]
	public enum DragDropEffects
	{
		/// <summary>A reprodução automática não aceita os dados.</summary>
		// Token: 0x04000594 RID: 1428
		None = 0,
		/// <summary>Os dados são copiados para a reprodução automática.</summary>
		// Token: 0x04000595 RID: 1429
		Copy = 1,
		/// <summary>Os dados da fonte da operação arrastar são movidos para a reprodução automática.</summary>
		// Token: 0x04000596 RID: 1430
		Move = 2,
		/// <summary>Os dados da fonte da operação arrastar são vinculados à reprodução automática.</summary>
		// Token: 0x04000597 RID: 1431
		Link = 4,
		/// <summary>Rolagem está prestes a iniciar ou está ocorrendo atualmente na reprodução automática.</summary>
		// Token: 0x04000598 RID: 1432
		Scroll = -2147483648,
		/// <summary>Os dados são copiados, removidos da origem de arrasto e rolados na reprodução automática.</summary>
		// Token: 0x04000599 RID: 1433
		All = -2147483645
	}
}
