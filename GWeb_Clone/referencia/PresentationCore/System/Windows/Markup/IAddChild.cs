using System;

namespace System.Windows.Markup
{
	/// <summary>Fornece um meio para analisar elementos que permitem misturas de elementos filho ou texto.</summary>
	// Token: 0x020001FE RID: 510
	public interface IAddChild
	{
		/// <summary>Adiciona um objeto filho.</summary>
		/// <param name="value">O objeto filho a ser adicionado.</param>
		// Token: 0x06000D40 RID: 3392
		void AddChild(object value);

		/// <summary>Adiciona o conteúdo do texto de um nó ao objeto.</summary>
		/// <param name="text">O texto a ser adicionado ao objeto.</param>
		// Token: 0x06000D41 RID: 3393
		void AddText(string text);
	}
}
