using System;

namespace System.Windows
{
	/// <summary>Relatar as especificações de uma alteração de valor que envolve um <see cref="T:System.Windows.Size" />. É usado como um parâmetro em substituições <see cref="M:System.Windows.UIElement.OnRenderSizeChanged(System.Windows.SizeChangedInfo)" />.</summary>
	// Token: 0x020001DA RID: 474
	public class SizeChangedInfo
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.SizeChangedInfo" />.</summary>
		/// <param name="element">O elemento em que o tamanho está sendo alterado.</param>
		/// <param name="previousSize">O tamanho anterior, antes da alteração.</param>
		/// <param name="widthChanged">
		///   <see langword="true" /> se o componente de largura do tamanho mudou.</param>
		/// <param name="heightChanged">
		///   <see langword="true" /> se o componente de altura do tamanho mudou.</param>
		// Token: 0x06000CB4 RID: 3252 RVA: 0x000304A4 File Offset: 0x0002F8A4
		public SizeChangedInfo(UIElement element, Size previousSize, bool widthChanged, bool heightChanged)
		{
			this._element = element;
			this._previousSize = previousSize;
			this._widthChanged = widthChanged;
			this._heightChanged = heightChanged;
		}

		/// <summary>Obtém o tamanho anterior do valor relacionado ao tamanho que está sendo relatado como alterado.</summary>
		/// <returns>O tamanho anterior.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x000304D4 File Offset: 0x0002F8D4
		public Size PreviousSize
		{
			get
			{
				return this._previousSize;
			}
		}

		/// <summary>Obtém o novo tamanho que está sendo relatado.</summary>
		/// <returns>O novo tamanho.</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x000304E8 File Offset: 0x0002F8E8
		public Size NewSize
		{
			get
			{
				return this._element.RenderSize;
			}
		}

		/// <summary>Obtém um valor que declara se o componente de largura do tamanho mudou.</summary>
		/// <returns>
		///   <see langword="true" /> Se a largura for alterada; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00030500 File Offset: 0x0002F900
		public bool WidthChanged
		{
			get
			{
				return this._widthChanged;
			}
		}

		/// <summary>Obtém um valor que indica se este <see cref="T:System.Windows.SizeChangedInfo" /> relata uma alteração de tamanho que inclui uma alteração significativa ao componente de altura.</summary>
		/// <returns>
		///   <see langword="true" /> Se não houver uma alteração significativa de componente de altura; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00030514 File Offset: 0x0002F914
		public bool HeightChanged
		{
			get
			{
				return this._heightChanged;
			}
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00030528 File Offset: 0x0002F928
		internal void Update(bool widthChanged, bool heightChanged)
		{
			this._widthChanged = (this._widthChanged || widthChanged);
			this._heightChanged = (this._heightChanged || heightChanged);
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00030554 File Offset: 0x0002F954
		internal UIElement Element
		{
			get
			{
				return this._element;
			}
		}

		// Token: 0x04000749 RID: 1865
		private UIElement _element;

		// Token: 0x0400074A RID: 1866
		private Size _previousSize;

		// Token: 0x0400074B RID: 1867
		private bool _widthChanged;

		// Token: 0x0400074C RID: 1868
		private bool _heightChanged;

		// Token: 0x0400074D RID: 1869
		internal SizeChangedInfo Next;
	}
}
