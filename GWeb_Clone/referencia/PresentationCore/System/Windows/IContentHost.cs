using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Windows
{
	/// <summary>Esta interface é implementada por layouts que hospedam <see cref="T:System.Windows.ContentElement" />.</summary>
	// Token: 0x020001C3 RID: 451
	public interface IContentHost
	{
		/// <summary>Executa testes de clique para elementos filho.</summary>
		/// <param name="point">Coordenadas do mouse relativas ao ContentHost.</param>
		/// <returns>Um descendente de <see cref="T:System.Windows.IInputElement" /> ou NULL se esse elemento não existe.</returns>
		// Token: 0x06000B88 RID: 2952
		IInputElement InputHitTest(Point point);

		/// <summary>Retorna uma coleção de retângulos delimitadores para um elemento filho.</summary>
		/// <param name="child">O elemento filho para o qual os retângulos delimitadores são retornados.</param>
		/// <returns>Uma coleção de retângulos delimitadores para um elemento filho.</returns>
		/// <exception cref="T:System.ArgumentNullException">Se child é nulo.</exception>
		/// <exception cref="T:System.ArgumentException">Se o elemento não é um descendente direto (ou seja, o elemento deve ser um filho de <see cref="T:System.Windows.IContentHost" /> ou um <see cref="T:System.Windows.ContentElement" /> que é um descendente direto do <see cref="T:System.Windows.IContentHost" />).</exception>
		// Token: 0x06000B89 RID: 2953
		ReadOnlyCollection<Rect> GetRectangles(ContentElement child);

		/// <summary>Obtém uma enumeração que contém todas as classes derivadas de <see cref="T:System.Windows.ContentElement" /> descendentes, bem como todas as classes derivadas de <see cref="T:System.Windows.UIElement" /> que são descendentes diretos de <see cref="T:System.Windows.IContentHost" /> ou uma de suas classes <see cref="T:System.Windows.ContentElement" /> descendentes.</summary>
		/// <returns>Enumeração que contém todas as classes derivadas de <see cref="T:System.Windows.ContentElement" /> descendentes, bem como todas as classes derivadas de <see cref="T:System.Windows.UIElement" /> que são descendentes diretos de <see cref="T:System.Windows.IContentHost" /> ou uma de suas classes <see cref="T:System.Windows.ContentElement" /> descendentes. Em outras palavras, os elementos para os quais o <see cref="T:System.Windows.IContentHost" /> cria uma representação visual (classes derivadas de <see cref="T:System.Windows.ContentElement" />) ou cujo layout é controlado pelo <see cref="T:System.Windows.IContentHost" /> (as classes derivadas de <see cref="T:System.Windows.UIElement" /> de descendente de primeiro nível).</returns>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000B8A RID: 2954
		IEnumerator<IInputElement> HostedElements { get; }

		/// <summary>Chamado quando há alterações no <see cref="P:System.Windows.UIElement.DesiredSize" /> de uma classe derivada de <see cref="T:System.Windows.UIElement" /> hospedada por um <see cref="T:System.Windows.IContentHost" />.</summary>
		/// <param name="child">Elemento filho cujo <see cref="P:System.Windows.UIElement.DesiredSize" /> foi alterado</param>
		/// <exception cref="T:System.ArgumentNullException">Se child é nulo.</exception>
		/// <exception cref="T:System.ArgumentException">Se child não é um descendente direto (ou seja, child deve ser um filho do <see cref="T:System.Windows.IContentHost" /> ou um <see cref="T:System.Windows.ContentElement" /> que é um descendente direto do <see cref="T:System.Windows.IContentHost" />).</exception>
		// Token: 0x06000B8B RID: 2955
		void OnChildDesiredSizeChanged(UIElement child);
	}
}
