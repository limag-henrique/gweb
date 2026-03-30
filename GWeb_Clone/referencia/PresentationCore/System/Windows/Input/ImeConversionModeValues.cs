using System;

namespace System.Windows.Input
{
	/// <summary>Descreve um modo de conversão de entrada a ser executado por um método de entrada.</summary>
	// Token: 0x02000250 RID: 592
	[Flags]
	public enum ImeConversionModeValues
	{
		/// <summary>O método de entrada usa um modo de conversão de caracteres nativos (Hiragana, Hangul, chinês).</summary>
		// Token: 0x040008F5 RID: 2293
		Native = 1,
		/// <summary>O método de entrada usa o modo de conversão de caracteres Katakana.</summary>
		// Token: 0x040008F6 RID: 2294
		Katakana = 2,
		/// <summary>O método de entrada usa o modo de conversão de forma completa.</summary>
		// Token: 0x040008F7 RID: 2295
		FullShape = 4,
		/// <summary>O método de entrada usa o modo de conversão de caracteres romanos.</summary>
		// Token: 0x040008F8 RID: 2296
		Roman = 8,
		/// <summary>O método de entrada usa o modo de conversão do código de caractere.</summary>
		// Token: 0x040008F9 RID: 2297
		CharCode = 16,
		/// <summary>O método de entrada não executará nenhuma conversão de entrada.</summary>
		// Token: 0x040008FA RID: 2298
		NoConversion = 32,
		/// <summary>O método de entrada usa o modo de conversão EUDC (caractere definido pelo usuário final).</summary>
		// Token: 0x040008FB RID: 2299
		Eudc = 64,
		/// <summary>O método de entrada usa o modo de conversão de símbolo.</summary>
		// Token: 0x040008FC RID: 2300
		Symbol = 128,
		/// <summary>O método de entrada usa o modo de conversão fixo.</summary>
		// Token: 0x040008FD RID: 2301
		Fixed = 256,
		/// <summary>O método de entrada usa o modo de conversão alfanumérico.</summary>
		// Token: 0x040008FE RID: 2302
		Alphanumeric = 512,
		/// <summary>O método de entrada não se importa com qual método de conversão de entrada é usado, o método de conversão propriamente dito é indeterminado.</summary>
		// Token: 0x040008FF RID: 2303
		DoNotCare = -2147483648
	}
}
