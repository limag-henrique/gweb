using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Representa uma sequência de caracteres que compartilham um único conjunto de propriedades.</summary>
	// Token: 0x020005B2 RID: 1458
	public abstract class TextRun
	{
		/// <summary>Obtém uma referência para a sequência de buffer de caracteres de texto.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.CharacterBufferReference" /> valor que representa os caracteres na sequência de texto.</returns>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060042AA RID: 17066
		public abstract CharacterBufferReference CharacterBufferReference { get; }

		/// <summary>Obtém o número de caracteres na sequência de texto.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o número de caracteres.</returns>
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060042AB RID: 17067
		public abstract int Length { get; }

		/// <summary>Obtém o conjunto de propriedades de texto que são compartilhadas por todos os caracteres no texto, como o pincel de primeiro plano ou de face de tipos.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" /> compartilhado de valor que representa o conjunto de propriedades de texto.</returns>
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060042AC RID: 17068
		public abstract TextRunProperties Properties { get; }
	}
}
