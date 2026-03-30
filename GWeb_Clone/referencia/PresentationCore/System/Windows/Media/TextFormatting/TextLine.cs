using System;
using System.Collections.Generic;
using MS.Internal.FontCache;
using MS.Internal.PresentationCore;
using MS.Internal.TextFormatting;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Fornece uma classe abstrata para dar suporte a serviços de formatação para uma linha de texto.</summary>
	// Token: 0x020005AA RID: 1450
	public abstract class TextLine : ITextMetrics, IDisposable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextLine" />.</summary>
		/// <param name="pixelsPerDip">
		///   <paramref name="pixelsPerDip" /> deve ser definido como o valor de PixelsPerDip do TextSource.</param>
		// Token: 0x06004257 RID: 16983 RVA: 0x001038E0 File Offset: 0x00102CE0
		protected TextLine(double pixelsPerDip)
		{
			this._pixelsPerDip = pixelsPerDip;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.TextFormatting.TextLine" />.</summary>
		// Token: 0x06004258 RID: 16984 RVA: 0x00103908 File Offset: 0x00102D08
		protected TextLine()
		{
		}

		/// <summary>Libera todos os recursos gerenciados e não gerenciados usados pelo objeto <see cref="T:System.Windows.Media.TextFormatting.TextFormatter" />.</summary>
		// Token: 0x06004259 RID: 16985
		public abstract void Dispose();

		/// <summary>Renderiza o objeto <see cref="T:System.Windows.Media.TextFormatting.TextLine" /> com base no <see cref="T:System.Windows.Media.DrawingContext" /> especificado.</summary>
		/// <param name="drawingContext">O objeto no qual o <see cref="T:System.Windows.Media.TextFormatting.TextLine" /> é renderizado.</param>
		/// <param name="origin">Um valor que representa a origem do desenho.</param>
		/// <param name="inversion">Um valor enumerado <see cref="T:System.Windows.Media.TextFormatting.InvertAxes" /> que indica a inversão dos eixos horizontal e vertical da superfície de desenho.</param>
		// Token: 0x0600425A RID: 16986
		public abstract void Draw(DrawingContext drawingContext, Point origin, InvertAxes inversion);

		/// <summary>Crie uma linha recolhida com base nas propriedades do texto recolhido.</summary>
		/// <param name="collapsingPropertiesList">Uma lista de objetos <see cref="T:System.Windows.Media.TextFormatting.TextCollapsingProperties" /> que representam as propriedades de texto recolhido.</param>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.TextLine" /> que representa uma linha recolhida que pode ser exibida.</returns>
		// Token: 0x0600425B RID: 16987
		public abstract TextLine Collapse(params TextCollapsingProperties[] collapsingPropertiesList);

		/// <summary>Obtém uma coleção de intervalos de texto recolhido depois que uma linha foi recolhida.</summary>
		/// <returns>Uma lista de objetos <see cref="T:System.Windows.Media.TextFormatting.TextCollapsedRange" /> que representam o texto recolhido.</returns>
		// Token: 0x0600425C RID: 16988
		public abstract IList<TextCollapsedRange> GetTextCollapsedRanges();

		/// <summary>Obtém a ocorrência do caractere correspondente à distância especificada do início da linha.</summary>
		/// <param name="distance">Um valor <see cref="T:System.Double" /> que representa a distância do início da linha.</param>
		/// <returns>O objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> no <paramref name="distance" /> especificado do início da linha.</returns>
		// Token: 0x0600425D RID: 16989
		public abstract CharacterHit GetCharacterHitFromDistance(double distance);

		/// <summary>Obtém a distância do início da linha até a ocorrência do caractere especificado.</summary>
		/// <param name="characterHit">O objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> cuja distância você deseja consultar.</param>
		/// <returns>Um <see cref="T:System.Double" /> que representa a distância do início da linha.</returns>
		// Token: 0x0600425E RID: 16990
		public abstract double GetDistanceFromCharacterHit(CharacterHit characterHit);

		/// <summary>Obtém a próxima ocorrência de caractere para a navegação do sinal de interpolação.</summary>
		/// <param name="characterHit">O objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> atual.</param>
		/// <returns>O próximo objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> com base na navegação do sinal de interpolação.</returns>
		// Token: 0x0600425F RID: 16991
		public abstract CharacterHit GetNextCaretCharacterHit(CharacterHit characterHit);

		/// <summary>Obtém a ocorrência de caractere anterior para a navegação do sinal de interpolação.</summary>
		/// <param name="characterHit">O objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> atual.</param>
		/// <returns>O objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> anterior com base na navegação do sinal de interpolação.</returns>
		// Token: 0x06004260 RID: 16992
		public abstract CharacterHit GetPreviousCaretCharacterHit(CharacterHit characterHit);

		/// <summary>Obtém a ocorrência do caractere anterior depois do backspace.</summary>
		/// <param name="characterHit">O objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> atual.</param>
		/// <returns>O objeto <see cref="T:System.Windows.Media.TextFormatting.CharacterHit" /> depois do backspace.</returns>
		// Token: 0x06004261 RID: 16993
		public abstract CharacterHit GetBackspaceCaretCharacterHit(CharacterHit characterHit);

		/// <summary>Obtém ou define o PixelsPerDip em que o texto deve ser renderizado.</summary>
		/// <returns>O valor <see cref="P:System.Windows.Media.TextFormatting.TextLine.PixelsPerDip" /> atual.</returns>
		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x00103928 File Offset: 0x00102D28
		// (set) Token: 0x06004263 RID: 16995 RVA: 0x0010393C File Offset: 0x00102D3C
		public double PixelsPerDip
		{
			get
			{
				return this._pixelsPerDip;
			}
			set
			{
				this._pixelsPerDip = value;
			}
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x00103950 File Offset: 0x00102D50
		[FriendAccessAllowed]
		internal bool IsAtCaretCharacterHit(CharacterHit characterHit, int cpFirst)
		{
			if (characterHit.TrailingLength == 0)
			{
				CharacterHit nextCaretCharacterHit = this.GetNextCaretCharacterHit(characterHit);
				if (nextCaretCharacterHit == characterHit)
				{
					nextCaretCharacterHit = new CharacterHit(cpFirst + this.Length - 1, 1);
				}
				CharacterHit previousCaretCharacterHit = this.GetPreviousCaretCharacterHit(nextCaretCharacterHit);
				return previousCaretCharacterHit == characterHit;
			}
			CharacterHit previousCaretCharacterHit2 = this.GetPreviousCaretCharacterHit(characterHit);
			CharacterHit nextCaretCharacterHit2 = this.GetNextCaretCharacterHit(previousCaretCharacterHit2);
			return nextCaretCharacterHit2 == characterHit;
		}

		/// <summary>Obtém uma matriz de retângulos delimitadores que representam o intervalo de caracteres dentro de uma linha de texto.</summary>
		/// <param name="firstTextSourceCharacterIndex">O índice do primeiro caractere do intervalo especificado.</param>
		/// <param name="textLength">O número de caracteres do intervalo especificado.</param>
		/// <returns>Uma lista de objetos <see cref="T:System.Windows.Media.TextFormatting.TextBounds" /> que representam o retângulo delimitador.</returns>
		// Token: 0x06004265 RID: 16997
		public abstract IList<TextBounds> GetTextBounds(int firstTextSourceCharacterIndex, int textLength);

		/// <summary>Obtém uma coleção de objetos <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> em um intervalo de texto que estão contidos em uma linha.</summary>
		/// <returns>Obtém uma lista de objetos <see cref="T:System.Windows.Media.TextFormatting.TextRun" /> contidos dentro de um intervalo de texto.</returns>
		// Token: 0x06004266 RID: 16998
		public abstract IList<TextSpan<TextRun>> GetTextRunSpans();

		/// <summary>Obtém um enumerador para enumerar objetos <see cref="T:System.Windows.Media.TextFormatting.IndexedGlyphRun" /> no <see cref="T:System.Windows.Media.TextFormatting.TextLine" />.</summary>
		/// <returns>Um enumerador que permite enumerar cada objeto <see cref="T:System.Windows.Media.TextFormatting.IndexedGlyphRun" /> no <see cref="T:System.Windows.Media.TextFormatting.TextLine" />.</returns>
		// Token: 0x06004267 RID: 16999
		public abstract IEnumerable<IndexedGlyphRun> GetIndexedGlyphRuns();

		/// <summary>Obtém um valor que indica se o conteúdo da linha excede a largura do parágrafo especificada.</summary>
		/// <returns>
		///   <see langword="true" />, ele a linha estourar a largura do parágrafo especificada; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06004268 RID: 17000
		public abstract bool HasOverflowed { get; }

		/// <summary>Obtém um valor que indica se a linha está recolhida.</summary>
		/// <returns>
		///   <see langword="true" />, se a linha está recolhida; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06004269 RID: 17001
		public abstract bool HasCollapsed { get; }

		/// <summary>Determina se a linha de texto é truncada no meio de uma palavra.</summary>
		/// <returns>
		///   <see langword="true" /> Se a linha de texto é truncada no meio de uma palavra; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x0600426A RID: 17002 RVA: 0x001039B0 File Offset: 0x00102DB0
		public virtual bool IsTruncated
		{
			get
			{
				return false;
			}
		}

		/// <summary>Obtém o estado da linha quando interrompida pelo processo de quebra de linha.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.TextLineBreak" /> que representa a quebra de linha.</returns>
		// Token: 0x0600426B RID: 17003
		public abstract TextLineBreak GetTextLineBreak();

		/// <summary>Obtém o número total de posições <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> da linha atual.</summary>
		/// <returns>O número total de <see cref="T:System.Windows.Media.TextFormatting.TextSource" /> posições da linha atual.</returns>
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x0600426C RID: 17004
		public abstract int Length { get; }

		/// <summary>Obtém o número de pontos de código de espaço em branco além do último caractere que não é em branco em uma linha.</summary>
		/// <returns>O número de pontos de código de espaço em branco além do último caractere não em branco em uma linha.</returns>
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x0600426D RID: 17005
		public abstract int TrailingWhitespaceLength { get; }

		/// <summary>Obtém o número de caracteres após o último caractere da linha que pode disparar a reformatação da linha atual.</summary>
		/// <returns>Um <see cref="T:System.Int32" /> valor que representa o número de caracteres.</returns>
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x0600426E RID: 17006
		public abstract int DependentLength { get; }

		/// <summary>Obtém o número de caracteres de nova linha no final de uma linha.</summary>
		/// <returns>O número de caracteres de nova linha.</returns>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x0600426F RID: 17007
		public abstract int NewlineLength { get; }

		/// <summary>Obtém a distância do início de um parágrafo até o ponto inicial de uma linha.</summary>
		/// <returns>A distância do início de um parágrafo até o ponto de partida de uma linha.</returns>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06004270 RID: 17008
		public abstract double Start { get; }

		/// <summary>Obtém a largura de uma linha de texto, exceto caracteres de espaço em branco à direita.</summary>
		/// <returns>A texto largura da linha, excluindo os caracteres de espaço em branco à direita.</returns>
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06004271 RID: 17009
		public abstract double Width { get; }

		/// <summary>Obtém a largura de uma linha de texto, incluindo caracteres de espaço em branco à direita.</summary>
		/// <returns>A texto largura da linha, incluindo caracteres de espaço em branco à direita.</returns>
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06004272 RID: 17010
		public abstract double WidthIncludingTrailingWhitespace { get; }

		/// <summary>Obtém a altura de uma linha de texto.</summary>
		/// <returns>A altura da linha de texto.</returns>
		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06004273 RID: 17011
		public abstract double Height { get; }

		/// <summary>Obtém a altura do texto e qualquer outro conteúdo na linha.</summary>
		/// <returns>A altura do texto e qualquer outro conteúdo na linha.</returns>
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06004274 RID: 17012
		public abstract double TextHeight { get; }

		/// <summary>Obtém a distância do pixel preto mais alto até o mais baixo em uma linha.</summary>
		/// <returns>Um valor que representa a distância de extensão.</returns>
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06004275 RID: 17013
		public abstract double Extent { get; }

		/// <summary>Obtém a distância da parte superior até a linha de base do objeto <see cref="T:System.Windows.Media.TextFormatting.TextLine" /> atual.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a distância da linha de base.</returns>
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06004276 RID: 17014
		public abstract double Baseline { get; }

		/// <summary>Obtém a distância da parte superior até a linha de base da linha do texto.</summary>
		/// <returns>A distância da linha de base de texto.</returns>
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06004277 RID: 17015
		public abstract double TextBaseline { get; }

		/// <summary>Obtém a distância da borda do ponto mais alto da linha até o marcador de linha de base da linha.</summary>
		/// <returns>A distância da linha de base do marcador.</returns>
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06004278 RID: 17016
		public abstract double MarkerBaseline { get; }

		/// <summary>Obtém a altura de um marcador para um item de lista.</summary>
		/// <returns>A altura do marcador.</returns>
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06004279 RID: 17017
		public abstract double MarkerHeight { get; }

		/// <summary>Obtém a distância pela qual pixels pretos se estendem antes da borda de alinhamento à esquerda da linha.</summary>
		/// <returns>A distância de folga à esquerda.</returns>
		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x0600427A RID: 17018
		public abstract double OverhangLeading { get; }

		/// <summary>Obtém a distância pela qual pixels pretos se estendem após a borda de alinhamento à direita da linha.</summary>
		/// <returns>A distância de folga à direita.</returns>
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x0600427B RID: 17019
		public abstract double OverhangTrailing { get; }

		/// <summary>Obtém a distância pela qual pixels pretos se estendem além da borda de alinhamento inferior de uma linha.</summary>
		/// <returns>A folga depois de distância.</returns>
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x0600427C RID: 17020
		public abstract double OverhangAfter { get; }

		// Token: 0x0400182F RID: 6191
		private double _pixelsPerDip = (double)Util.PixelsPerDip;
	}
}
