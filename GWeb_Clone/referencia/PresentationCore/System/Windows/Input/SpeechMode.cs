using System;

namespace System.Windows.Input
{
	/// <summary>Especifica o modo de interpretação de entrada de fala.</summary>
	// Token: 0x0200024F RID: 591
	public enum SpeechMode
	{
		/// <summary>A entrada de fala é interpretada como ditado.</summary>
		// Token: 0x040008F1 RID: 2289
		Dictation,
		/// <summary>A entrada de fala é interpretada como comandos.</summary>
		// Token: 0x040008F2 RID: 2290
		Command,
		/// <summary>O modo de entrada de fala é indeterminado.</summary>
		// Token: 0x040008F3 RID: 2291
		Indeterminate
	}
}
