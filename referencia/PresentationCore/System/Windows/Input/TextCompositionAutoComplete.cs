using System;

namespace System.Windows.Input
{
	/// <summary>Define um conjunto de estados para o tratamento da conclusão automática de uma composição de texto.</summary>
	// Token: 0x020002D7 RID: 727
	public enum TextCompositionAutoComplete
	{
		/// <summary>O preenchimento automático está desligado.</summary>
		// Token: 0x04000C00 RID: 3072
		Off,
		/// <summary>O preenchimento automático está ligado. Um evento <see cref="E:System.Windows.Input.TextCompositionManager.TextInput" /> será gerado automaticamente pelo <see cref="T:System.Windows.Input.TextCompositionManager" /> após um evento <see cref="E:System.Windows.Input.TextCompositionManager.TextInputStart" /> ser manipulado.</summary>
		// Token: 0x04000C01 RID: 3073
		On
	}
}
