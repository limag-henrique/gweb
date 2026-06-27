using System;

namespace System.Windows.Input
{
	/// <summary>Especifica como os eventos de manipulação são interpretados.</summary>
	// Token: 0x02000277 RID: 631
	[Flags]
	public enum ManipulationModes
	{
		/// <summary>Os eventos de manipulação não ocorrem.</summary>
		// Token: 0x040009F0 RID: 2544
		None = 0,
		/// <summary>Uma manipulação pode fazer a translação de um objeto horizontalmente.</summary>
		// Token: 0x040009F1 RID: 2545
		TranslateX = 1,
		/// <summary>Uma manipulação pode fazer a translação de um objeto verticalmente.</summary>
		// Token: 0x040009F2 RID: 2546
		TranslateY = 2,
		/// <summary>Uma manipulação pode fazer a translação de um objeto.</summary>
		// Token: 0x040009F3 RID: 2547
		Translate = 3,
		/// <summary>Uma manipulação pode girar um objeto.</summary>
		// Token: 0x040009F4 RID: 2548
		Rotate = 4,
		/// <summary>Uma manipulação pode colocar um objeto em escala.</summary>
		// Token: 0x040009F5 RID: 2549
		Scale = 8,
		/// <summary>Uma manipulação pode colocar em escala, fazer a translação ou fazer a rotação de um objeto e pode ocorrer com um ponto de entrada.</summary>
		// Token: 0x040009F6 RID: 2550
		All = 15
	}
}
