using System;
using System.Globalization;
using MS.Internal.FontCache;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Fornece um conjunto de propriedades, como faces de tipos ou pincel de primeiro plano, que podem ser aplicadas a um objeto <see cref="T:System.Windows.Media.TextFormatting.TextRun" />. Esta é uma classe abstrata.</summary>
	// Token: 0x020005B4 RID: 1460
	public abstract class TextRunProperties
	{
		/// <summary>Obtém a face de tipos da sequência de texto.</summary>
		/// <returns>Um valor de <see cref="T:System.Windows.Media.Typeface" />.</returns>
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x060042B4 RID: 17076
		public abstract Typeface Typeface { get; }

		/// <summary>Obtém o tamanho do texto, em pontos, da sequência de texto.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa o tamanho do texto em DIPs (Pixels independentes de dispositivo). O padrão é 12 DIP.</returns>
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x060042B5 RID: 17077
		public abstract double FontRenderingEmSize { get; }

		/// <summary>Obtém o tamanho do texto, em pontos, que é usado para dicas de fonte.</summary>
		/// <returns>Um <see cref="T:System.Double" /> que representa o tamanho do texto em pontos. O padrão é 12 pt.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x060042B6 RID: 17078
		public abstract double FontHintingEmSize { get; }

		/// <summary>Obtém a coleção de objetos <see cref="T:System.Windows.TextDecoration" /> usadas para a sequência de texto.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.TextDecorationCollection" />.</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x060042B7 RID: 17079
		public abstract TextDecorationCollection TextDecorations { get; }

		/// <summary>Obtém o pincel usado para pintar a cor de primeiro plano da sequência de texto.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Brush" /> valor que representa a cor de primeiro plano.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x060042B8 RID: 17080
		public abstract Brush ForegroundBrush { get; }

		/// <summary>Obtém o pincel usado para pintar a cor da tela de fundo da sequência de texto.</summary>
		/// <returns>Um <see cref="T:System.Windows.Media.Brush" /> valor que representa a cor do plano de fundo.</returns>
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x060042B9 RID: 17081
		public abstract Brush BackgroundBrush { get; }

		/// <summary>Obtém as informações de cultura da sequência de texto.</summary>
		/// <returns>Um valor de <see cref="T:System.Globalization.CultureInfo" /> que representa a cultura da sequência de texto.</returns>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060042BA RID: 17082
		public abstract CultureInfo CultureInfo { get; }

		/// <summary>Obtém a coleção de objetos <see cref="T:System.Windows.Media.TextEffect" /> usadas para a sequência de texto.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.Media.TextEffectCollection" />.</returns>
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060042BB RID: 17083
		public abstract TextEffectCollection TextEffects { get; }

		/// <summary>Obtém o estilo de linha de base para o texto posicionado no eixo vertical.</summary>
		/// <returns>Um valor enumerado de <see cref="T:System.Windows.BaselineAlignment" />.</returns>
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x060042BC RID: 17084 RVA: 0x00104034 File Offset: 0x00103434
		public virtual BaselineAlignment BaselineAlignment
		{
			get
			{
				return BaselineAlignment.Baseline;
			}
		}

		/// <summary>Obtém as propriedades de tipografia da sequência de texto.</summary>
		/// <returns>Um valor de <see cref="T:System.Windows.Media.TextFormatting.TextRunTypographyProperties" />.</returns>
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x00104044 File Offset: 0x00103444
		public virtual TextRunTypographyProperties TypographyProperties
		{
			get
			{
				return null;
			}
		}

		/// <summary>Obtém as configurações de substituição de número, que determinam quais números no texto são exibidos em diferentes culturas.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.Media.NumberSubstitution" />.</returns>
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x060042BE RID: 17086 RVA: 0x00104054 File Offset: 0x00103454
		public virtual NumberSubstitution NumberSubstitution
		{
			get
			{
				return null;
			}
		}

		/// <summary>Obtém ou define o PixelsPerDip em que o texto deve ser renderizado.</summary>
		/// <returns>O valor <see cref="P:System.Windows.Media.TextFormatting.TextRunProperties.PixelsPerDip" /> atual.</returns>
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x00104064 File Offset: 0x00103464
		// (set) Token: 0x060042C0 RID: 17088 RVA: 0x00104078 File Offset: 0x00103478
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

		// Token: 0x04001840 RID: 6208
		private double _pixelsPerDip = (double)Util.PixelsPerDip;
	}
}
