using System;

namespace System.Windows.Input
{
	/// <summary>Especifica o modo de conversão de frase executada por um método de entrada.</summary>
	// Token: 0x02000251 RID: 593
	[Flags]
	public enum ImeSentenceModeValues
	{
		/// <summary>O método de entrada não realiza nenhuma conversão de frase.</summary>
		// Token: 0x04000901 RID: 2305
		None = 0,
		/// <summary>O método de entrada usa uma conversão de frase de cláusula no plural.</summary>
		// Token: 0x04000902 RID: 2306
		PluralClause = 1,
		/// <summary>O método de entrada usa conversão de frase Kanji/Hanja única.</summary>
		// Token: 0x04000903 RID: 2307
		SingleConversion = 2,
		/// <summary>O método de entrada usa o método de conversão de frase automaticamente.</summary>
		// Token: 0x04000904 RID: 2308
		Automatic = 4,
		/// <summary>O método de entrada usa uma conversão de frase de previsão de frase.</summary>
		// Token: 0x04000905 RID: 2309
		PhrasePrediction = 8,
		/// <summary>O método de entrada usa uma conversão de frase estilo conversa.</summary>
		// Token: 0x04000906 RID: 2310
		Conversation = 16,
		/// <summary>O método de entrada não se importa com o método de conversão de frase usado; o modo de conversão de frase real é indeterminado.</summary>
		// Token: 0x04000907 RID: 2311
		DoNotCare = -2147483648
	}
}
