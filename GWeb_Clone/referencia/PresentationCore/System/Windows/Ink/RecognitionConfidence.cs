using System;

namespace System.Windows.Ink
{
	/// <summary>Especifica o nível de confiança que o <see cref="T:System.Windows.Ink.GestureRecognizer" /> determina para um gesto de tinta específica.</summary>
	// Token: 0x02000359 RID: 857
	public enum RecognitionConfidence
	{
		/// <summary>Indica muita confiança no resultado do reconhecimento.</summary>
		// Token: 0x04000FA6 RID: 4006
		Strong,
		/// <summary>Indica confiança intermediária no resultado do reconhecimento.</summary>
		// Token: 0x04000FA7 RID: 4007
		Intermediate,
		/// <summary>Indica pouca confiança no resultado do reconhecimento.</summary>
		// Token: 0x04000FA8 RID: 4008
		Poor
	}
}
