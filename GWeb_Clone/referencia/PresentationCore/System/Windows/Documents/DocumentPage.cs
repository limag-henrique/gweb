using System;
using System.Windows.Media;

namespace System.Windows.Documents
{
	/// <summary>Representa uma página de documento produzida por um paginador.</summary>
	// Token: 0x020002FF RID: 767
	public class DocumentPage : IDisposable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Documents.DocumentPage" /> usando o <see cref="T:System.Windows.Media.Visual" /> especificado.</summary>
		/// <param name="visual">A representação visual da página.</param>
		// Token: 0x0600188C RID: 6284 RVA: 0x00062564 File Offset: 0x00061964
		public DocumentPage(Visual visual)
		{
			this._visual = visual;
			this._pageSize = Size.Empty;
			this._bleedBox = Rect.Empty;
			this._contentBox = Rect.Empty;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Documents.DocumentPage" /> usando os tamanhos de caixa e <see cref="T:System.Windows.Media.Visual" /> especificados.</summary>
		/// <param name="visual">A representação visual da página.</param>
		/// <param name="pageSize">O tamanho da página, incluindo as margens, como ele será após qualquer recorte.</param>
		/// <param name="bleedBox">A área de sangria relacionada à produção de impressão, as marcas de registro e as marcas de corte que podem aparecer na folha física fora dos limites de página lógicos.</param>
		/// <param name="contentBox">A área da página dentro das margens.</param>
		// Token: 0x0600188D RID: 6285 RVA: 0x000625A0 File Offset: 0x000619A0
		public DocumentPage(Visual visual, Size pageSize, Rect bleedBox, Rect contentBox)
		{
			this._visual = visual;
			this._pageSize = pageSize;
			this._bleedBox = bleedBox;
			this._contentBox = contentBox;
		}

		/// <summary>Libera todos os recursos usados pelo <see cref="T:System.Windows.Documents.DocumentPage" />.</summary>
		// Token: 0x0600188E RID: 6286 RVA: 0x000625D0 File Offset: 0x000619D0
		public virtual void Dispose()
		{
			this._visual = null;
			this._pageSize = Size.Empty;
			this._bleedBox = Rect.Empty;
			this._contentBox = Rect.Empty;
			this.OnPageDestroyed(EventArgs.Empty);
		}

		/// <summary>Quando substituído em uma classe derivada, obtém a representação visual da página.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Visual" /> ilustrando a página.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00062610 File Offset: 0x00061A10
		public virtual Visual Visual
		{
			get
			{
				return this._visual;
			}
		}

		/// <summary>Quando substituído em uma classe derivada, obtém o tamanho real de uma página como ele será após qualquer corte.</summary>
		/// <returns>Um <see cref="T:System.Windows.Size" /> que representa a altura e largura da página.</returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00062624 File Offset: 0x00061A24
		public virtual Size Size
		{
			get
			{
				if (this._pageSize == Size.Empty && this._visual != null)
				{
					return VisualTreeHelper.GetContentBounds(this._visual).Size;
				}
				return this._pageSize;
			}
		}

		/// <summary>Quando substituído em uma classe derivada, obtém a área de sangria relacionada à produção de impressão, as marcas de registro e as marcas de corte que podem aparecer na folha física fora dos limites de página lógicos.</summary>
		/// <returns>Um <see cref="T:System.Windows.Rect" /> que representa o tamanho e local da área de sangria de caixa.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00062668 File Offset: 0x00061A68
		public virtual Rect BleedBox
		{
			get
			{
				if (this._bleedBox == Rect.Empty)
				{
					return new Rect(this.Size);
				}
				return this._bleedBox;
			}
		}

		/// <summary>Quando substituído em uma classe derivada, obtém a área da página dentro das margens.</summary>
		/// <returns>Um <see cref="T:System.Windows.Rect" /> que representa a área da página, não incluindo as margens.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0006269C File Offset: 0x00061A9C
		public virtual Rect ContentBox
		{
			get
			{
				if (this._contentBox == Rect.Empty)
				{
					return new Rect(this.Size);
				}
				return this._contentBox;
			}
		}

		/// <summary>Ocorre quando o <see cref="P:System.Windows.Documents.DocumentPage.Visual" /> que representa <see cref="T:System.Windows.Documents.DocumentPage" /> é destruído e não pode mais ser usado para exibição.</summary>
		// Token: 0x1400016A RID: 362
		// (add) Token: 0x06001893 RID: 6291 RVA: 0x000626D0 File Offset: 0x00061AD0
		// (remove) Token: 0x06001894 RID: 6292 RVA: 0x00062708 File Offset: 0x00061B08
		public event EventHandler PageDestroyed;

		/// <summary>Aciona o evento <see cref="E:System.Windows.Documents.DocumentPage.PageDestroyed" />.</summary>
		/// <param name="e">Um <see cref="T:System.EventArgs" /> que contém os dados do evento.</param>
		// Token: 0x06001895 RID: 6293 RVA: 0x00062740 File Offset: 0x00061B40
		protected void OnPageDestroyed(EventArgs e)
		{
			if (this.PageDestroyed != null)
			{
				this.PageDestroyed(this, e);
			}
		}

		/// <summary>Define o <see cref="P:System.Windows.Documents.DocumentPage.Visual" /> que representa a página.</summary>
		/// <param name="visual">A representação visual da página.</param>
		// Token: 0x06001896 RID: 6294 RVA: 0x00062764 File Offset: 0x00061B64
		protected void SetVisual(Visual visual)
		{
			this._visual = visual;
		}

		/// <summary>Define o <see cref="P:System.Windows.Documents.DocumentPage.Size" /> da página física como ela será após qualquer corte.</summary>
		/// <param name="size">O tamanho da página.</param>
		// Token: 0x06001897 RID: 6295 RVA: 0x00062778 File Offset: 0x00061B78
		protected void SetSize(Size size)
		{
			this._pageSize = size;
		}

		/// <summary>Define as dimensões e a localização do <see cref="P:System.Windows.Documents.DocumentPage.BleedBox" />.</summary>
		/// <param name="bleedBox">Um objeto que especifica o tamanho e a localização de um retângulo.</param>
		// Token: 0x06001898 RID: 6296 RVA: 0x0006278C File Offset: 0x00061B8C
		protected void SetBleedBox(Rect bleedBox)
		{
			this._bleedBox = bleedBox;
		}

		/// <summary>Define a dimensão e o local do <see cref="P:System.Windows.Documents.DocumentPage.ContentBox" />.</summary>
		/// <param name="contentBox">Um objeto que especifica o tamanho e a localização de um retângulo.</param>
		// Token: 0x06001899 RID: 6297 RVA: 0x000627A0 File Offset: 0x00061BA0
		protected void SetContentBox(Rect contentBox)
		{
			this._contentBox = contentBox;
		}

		/// <summary>Representa uma página ausente. Esta propriedade é somente leitura e estática.</summary>
		// Token: 0x04000D4A RID: 3402
		public static readonly DocumentPage Missing = new DocumentPage.MissingDocumentPage();

		// Token: 0x04000D4C RID: 3404
		private Visual _visual;

		// Token: 0x04000D4D RID: 3405
		private Size _pageSize;

		// Token: 0x04000D4E RID: 3406
		private Rect _bleedBox;

		// Token: 0x04000D4F RID: 3407
		private Rect _contentBox;

		// Token: 0x0200083E RID: 2110
		private sealed class MissingDocumentPage : DocumentPage
		{
			// Token: 0x060056BB RID: 22203 RVA: 0x001638E8 File Offset: 0x00162CE8
			public MissingDocumentPage() : base(null, Size.Empty, Rect.Empty, Rect.Empty)
			{
			}

			// Token: 0x060056BC RID: 22204 RVA: 0x0016390C File Offset: 0x00162D0C
			public override void Dispose()
			{
			}
		}
	}
}
