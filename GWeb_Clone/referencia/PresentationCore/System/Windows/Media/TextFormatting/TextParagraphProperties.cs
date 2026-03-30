using System;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Fornece um conjunto de propriedades, como a direção de fluxo, alinhamento ou recuo, que pode ser aplicado a um parágrafo. Esta é uma classe abstrata.</summary>
	// Token: 0x020005B1 RID: 1457
	public abstract class TextParagraphProperties
	{
		/// <summary>Obtém um valor que especifica se a direção de avanço do texto primário deve ser da esquerda para a direita ou da direita para a esquerda.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.FlowDirection" />.</returns>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600429A RID: 17050
		public abstract FlowDirection FlowDirection { get; }

		/// <summary>Obtém um valor que descreve como um conteúdo embutido de um bloco é alinhado.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.TextAlignment" />.</returns>
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600429B RID: 17051
		public abstract TextAlignment TextAlignment { get; }

		/// <summary>Obtém a altura de uma linha de texto.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a altura de uma linha de texto.</returns>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x0600429C RID: 17052
		public abstract double LineHeight { get; }

		/// <summary>Obtém um valor que indica se a sequência de texto é a primeira linha do parágrafo.</summary>
		/// <returns>
		///   <see langword="true" />, se a sequência de texto é a primeira linha do parágrafo; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x0600429D RID: 17053
		public abstract bool FirstLineInParagraph { get; }

		/// <summary>Obtém um valor que indica se uma linha formatada sempre pode ser recolhida.</summary>
		/// <returns>
		///   <see langword="true" /> Se a linha formatada sempre pode ser recolhida; Caso contrário, <see langword="false" />, que indica que apenas as linhas formatadas que estouram a largura de parágrafo são recolhidas. O valor padrão é <see langword="false" />.</returns>
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x00103ED4 File Offset: 0x001032D4
		public virtual bool AlwaysCollapsible
		{
			get
			{
				return false;
			}
		}

		/// <summary>Obtém as propriedades de sequência de texto padrão, como faces de tipo ou pincel de primeiro plano.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.TextRunProperties" />.</returns>
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x0600429F RID: 17055
		public abstract TextRunProperties DefaultTextRunProperties { get; }

		/// <summary>Obtém a coleção de objetos <see cref="T:System.Windows.TextDecoration" />.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.TextDecorationCollection" />.</returns>
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x00103EE4 File Offset: 0x001032E4
		public virtual TextDecorationCollection TextDecorations
		{
			get
			{
				return null;
			}
		}

		/// <summary>Obtém um valor que controla se o texto será quebrado quando atingir o limite de fluxo da caixa de bloco que o contém.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.TextWrapping" />.</returns>
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x060042A1 RID: 17057
		public abstract TextWrapping TextWrapping { get; }

		/// <summary>Obtém um valor que especifica as características de marcador da primeira linha do parágrafo.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextFormatting.TextMarkerProperties" />.</returns>
		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x060042A2 RID: 17058
		public abstract TextMarkerProperties TextMarkerProperties { get; }

		/// <summary>Obtém a quantidade de recuo de linha.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a quantidade de recuo de linha.</returns>
		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x060042A3 RID: 17059
		public abstract double Indent { get; }

		/// <summary>Obtém a quantidade de recuo do parágrafo.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa a quantidade de recuo do parágrafo.</returns>
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x00103EF4 File Offset: 0x001032F4
		public virtual double ParagraphIndent
		{
			get
			{
				return 0.0;
			}
		}

		/// <summary>Obtém a distância de tabulação incremental padrão.</summary>
		/// <returns>Um <see cref="T:System.Double" /> valor que representa a distância de tabulação incremental padrão.</returns>
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x060042A5 RID: 17061 RVA: 0x00103F0C File Offset: 0x0010330C
		public virtual double DefaultIncrementalTab
		{
			get
			{
				return 4.0 * this.DefaultTextRunProperties.FontRenderingEmSize;
			}
		}

		/// <summary>Obtém uma coleção de definições de tabulação.</summary>
		/// <returns>Uma lista de objetos <see cref="T:System.Windows.Media.TextFormatting.TextTabProperties" />.</returns>
		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060042A6 RID: 17062 RVA: 0x00103F30 File Offset: 0x00103330
		public virtual IList<TextTabProperties> Tabs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060042A7 RID: 17063 RVA: 0x00103F40 File Offset: 0x00103340
		// (set) Token: 0x060042A8 RID: 17064 RVA: 0x00103F54 File Offset: 0x00103354
		internal virtual TextLexicalService Hyphenator
		{
			[FriendAccessAllowed]
			get
			{
				return this._hyphenator;
			}
			[FriendAccessAllowed]
			set
			{
				this._hyphenator = value;
			}
		}

		// Token: 0x0400183E RID: 6206
		private TextLexicalService _hyphenator;
	}
}
