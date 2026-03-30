using System;

namespace System.Windows
{
	/// <summary>Fornece métodos de utilitário estático para obter ou definir a posição de um <see cref="T:System.Windows.ContentElement" /> em uma árvore de elementos.</summary>
	// Token: 0x02000191 RID: 401
	public static class ContentOperations
	{
		/// <summary>Obtém o elemento pai do <see cref="T:System.Windows.ContentElement" /> especificado.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.ContentElement" /> do qual obter o pai.</param>
		/// <returns>O elemento pai na árvore atual.</returns>
		// Token: 0x06000581 RID: 1409 RVA: 0x00019D9C File Offset: 0x0001919C
		public static DependencyObject GetParent(ContentElement reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			return reference._parent;
		}

		/// <summary>Define o elemento pai do <see cref="T:System.Windows.ContentElement" /> fornecido.</summary>
		/// <param name="reference">O <see cref="T:System.Windows.ContentElement" /> para redefinir o pai.</param>
		/// <param name="parent">O novo elemento pai.</param>
		// Token: 0x06000582 RID: 1410 RVA: 0x00019DC0 File Offset: 0x000191C0
		public static void SetParent(ContentElement reference, DependencyObject parent)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			DependencyObject parent2 = reference._parent;
			reference._parent = parent;
			reference.OnContentParentChanged(parent2);
		}
	}
}
