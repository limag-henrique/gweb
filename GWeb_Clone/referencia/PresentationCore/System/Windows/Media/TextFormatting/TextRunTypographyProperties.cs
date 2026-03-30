using System;
using MS.Internal.Text.TextInterface;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Fornece uma classe abstrata para dar suporte às propriedades de tipografia para objetos <see cref="T:System.Windows.Media.TextFormatting.TextRun" />.</summary>
	// Token: 0x020005B5 RID: 1461
	public abstract class TextRunTypographyProperties
	{
		/// <summary>Obtém um valor que indica se as ligaturas padrão estão habilitadas.</summary>
		/// <returns>
		///   <see langword="true" /> Se as ligaturas padrão estão habilitadas; Caso contrário, <see langword="false" />. O padrão é <see langword="true" />.</returns>
		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060042C2 RID: 17090
		public abstract bool StandardLigatures { get; }

		/// <summary>Obtém um valor que indica se as ligaturas contextuais estão habilitadas.</summary>
		/// <returns>
		///   <see langword="true" /> Se as ligaturas contextuais estão habilitadas; Caso contrário, <see langword="false" />. O padrão é <see langword="true" />.</returns>
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060042C3 RID: 17091
		public abstract bool ContextualLigatures { get; }

		/// <summary>Obtém um valor que indica se as ligaturas discricionárias estão habilitadas.</summary>
		/// <returns>
		///   <see langword="true" /> Se as ligaturas discricionárias estão habilitadas; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x060042C4 RID: 17092
		public abstract bool DiscretionaryLigatures { get; }

		/// <summary>Obtém um valor que indica se ligaturas históricas estão habilitadas.</summary>
		/// <returns>
		///   <see langword="true" /> Se as ligaturas históricas estão habilitadas; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060042C5 RID: 17093
		public abstract bool HistoricalLigatures { get; }

		/// <summary>Obtém um valor que indica se os formulários de glifos personalizados podem ser usados com base no contexto do texto que está sendo renderizado.</summary>
		/// <returns>
		///   <see langword="true" /> Se os formulários de glifos personalizados podem ser usados; Caso contrário, <see langword="false" />. O padrão é <see langword="true" />.</returns>
		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x060042C6 RID: 17094
		public abstract bool ContextualAlternates { get; }

		/// <summary>Obtém um valor que indica se as formas históricas estão habilitadas.</summary>
		/// <returns>
		///   <see langword="true" /> Se as formas históricas estão habilitadas; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x060042C7 RID: 17095
		public abstract bool HistoricalForms { get; }

		/// <summary>Obtém um valor que indica se o kerning está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> se o kerning estiver habilitado; caso contrário, <see langword="false" />. O padrão é <see langword="true" />.</returns>
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x060042C8 RID: 17096
		public abstract bool Kerning { get; }

		/// <summary>Obtém um valor que indica se o espaçamento entre os glifos do texto com todas as letras maiúsculas é ajustado globalmente para melhorar a legibilidade.</summary>
		/// <returns>
		///   <see langword="true" /> Se o espaçamento é ajustado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x060042C9 RID: 17097
		public abstract bool CapitalSpacing { get; }

		/// <summary>Obtém um valor que indica se os glifos ajustam sua posição vertical para melhorar o alinhamento com os glifos maiúsculos.</summary>
		/// <returns>
		///   <see langword="true" /> Se a posição vertical é ajustada; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x060042CA RID: 17098
		public abstract bool CaseSensitiveForms { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060042CB RID: 17099
		public abstract bool StylisticSet1 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060042CC RID: 17100
		public abstract bool StylisticSet2 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060042CD RID: 17101
		public abstract bool StylisticSet3 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060042CE RID: 17102
		public abstract bool StylisticSet4 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060042CF RID: 17103
		public abstract bool StylisticSet5 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060042D0 RID: 17104
		public abstract bool StylisticSet6 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060042D1 RID: 17105
		public abstract bool StylisticSet7 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x060042D2 RID: 17106
		public abstract bool StylisticSet8 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x060042D3 RID: 17107
		public abstract bool StylisticSet9 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060042D4 RID: 17108
		public abstract bool StylisticSet10 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x060042D5 RID: 17109
		public abstract bool StylisticSet11 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x060042D6 RID: 17110
		public abstract bool StylisticSet12 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x060042D7 RID: 17111
		public abstract bool StylisticSet13 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060042D8 RID: 17112
		public abstract bool StylisticSet14 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060042D9 RID: 17113
		public abstract bool StylisticSet15 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x060042DA RID: 17114
		public abstract bool StylisticSet16 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x060042DB RID: 17115
		public abstract bool StylisticSet17 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x060042DC RID: 17116
		public abstract bool StylisticSet18 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x060042DD RID: 17117
		public abstract bool StylisticSet19 { get; }

		/// <summary>Obtém um valor que indica se um conjunto de estilos de um formulário de fonte está habilitado.</summary>
		/// <returns>
		///   <see langword="true" /> Se o conjunto de estilos de formulário de fonte estiver habilitado; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x060042DE RID: 17118
		public abstract bool StylisticSet20 { get; }

		/// <summary>Obtém um valor que indica se um formulário de fonte zero nominal deve ser substituído por um de zero cortado.</summary>
		/// <returns>
		///   <see langword="true" /> se formulários com zero cortado estão habilitados; caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x060042DF RID: 17119
		public abstract bool SlashedZero { get; }

		/// <summary>Obtém um valor que indica se os formulários de fonte tipográfica padrão de glifos gregos foram substituídos pelos formulários de fonte correspondentes que geralmente são usados em notação matemática.</summary>
		/// <returns>
		///   <see langword="true" /> Se formulários gregos matemáticos estão habilitados; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x060042E0 RID: 17120
		public abstract bool MathematicalGreek { get; }

		/// <summary>Obtém um valor que indica se os formulários de fonte japonesa padrão foram substituídos pelos formulários tipográficos preferenciais correspondentes.</summary>
		/// <returns>
		///   <see langword="true" /> Se os formulários de fonte japonesa padrão foram substituídos pelos formulários tipográficos preferenciais correspondentes; Caso contrário, <see langword="false" />. O padrão é <see langword="false" />.</returns>
		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x060042E1 RID: 17121
		public abstract bool EastAsianExpertForms { get; }

		/// <summary>Obtém um valor que indica uma variação do formulário tipográfico padrão a ser usado.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.FontVariants" />. O padrão é <see cref="F:System.Windows.FontVariants.Normal" />.</returns>
		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x060042E2 RID: 17122
		public abstract FontVariants Variants { get; }

		/// <summary>Obtém um valor que indica o formato de letras maiúsculas da fonte selecionada.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.FontCapitals" />. O padrão é <see cref="F:System.Windows.FontCapitals.Normal" />.</returns>
		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x060042E3 RID: 17123
		public abstract FontCapitals Capitals { get; }

		/// <summary>Obtém um valor que indica o estilo de fração.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.FontFraction" />. O padrão é <see cref="F:System.Windows.FontFraction.Normal" />.</returns>
		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060042E4 RID: 17124
		public abstract FontFraction Fraction { get; }

		/// <summary>Obtém um valor que indica o conjunto de glifos que são usados para renderizar formulários de fonte numérica alternativos.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.FontNumeralStyle" />. O padrão é <see cref="F:System.Windows.FontNumeralStyle.Normal" />.</returns>
		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060042E5 RID: 17125
		public abstract FontNumeralStyle NumeralStyle { get; }

		/// <summary>Obtém o alinhamento de larguras ao usar numerais.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.FontNumeralAlignment" />. O padrão é <see cref="F:System.Windows.FontNumeralAlignment.Normal" />.</returns>
		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060042E6 RID: 17126
		public abstract FontNumeralAlignment NumeralAlignment { get; }

		/// <summary>Obtém um valor que indica a largura proporcional a ser usada para caracteres latinos em uma fonte da Ásia Oriental.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.FontEastAsianWidths" />. O padrão é <see cref="F:System.Windows.FontEastAsianWidths.Normal" />.</returns>
		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x060042E7 RID: 17127
		public abstract FontEastAsianWidths EastAsianWidths { get; }

		/// <summary>Obtém um valor que indica a versão dos glifos a ser usada para um idioma ou o sistema de escrita específico.</summary>
		/// <returns>Um dos valores de <see cref="T:System.Windows.FontEastAsianLanguage" />. O padrão é <see cref="F:System.Windows.FontEastAsianLanguage.Normal" />.</returns>
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x060042E8 RID: 17128
		public abstract FontEastAsianLanguage EastAsianLanguage { get; }

		/// <summary>Obtém o índice de um formulário de caracteres ornamentados padrão.</summary>
		/// <returns>O índice do formulário de caracteres ornamentados padrão. O padrão é 0 (zero).</returns>
		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x060042E9 RID: 17129
		public abstract int StandardSwashes { get; }

		/// <summary>Obtém um valor que especifica o índice de um formulário de caracteres ornamentados contextual.</summary>
		/// <returns>O índice do formulário de caracteres ornamentados padrão. O padrão é 0 (zero).</returns>
		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x060042EA RID: 17130
		public abstract int ContextualSwashes { get; }

		/// <summary>Obtém o índice de um formulário de alternativas de estilos.</summary>
		/// <returns>O índice do formulário de alternativas estilísticas. O padrão é 0 (zero).</returns>
		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x060042EB RID: 17131
		public abstract int StylisticAlternates { get; }

		/// <summary>Obtém o índice de um formulário de anotação alternativo.</summary>
		/// <returns>O índice do formulário de anotação alternativo. O padrão é 0 (zero).</returns>
		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x060042EC RID: 17132
		public abstract int AnnotationAlternates { get; }

		/// <summary>Corrige o estado interno de uma classe derivada <see cref="T:System.Windows.Media.TextFormatting.TextRunTypographyProperties" /> sempre que alguma propriedade <see cref="T:System.Windows.Media.TextFormatting.TextRunTypographyProperties" /> altera seu valor.</summary>
		// Token: 0x060042ED RID: 17133 RVA: 0x001040AC File Offset: 0x001034AC
		protected void OnPropertiesChanged()
		{
			this._features = null;
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x060042EE RID: 17134 RVA: 0x001040C0 File Offset: 0x001034C0
		// (set) Token: 0x060042EF RID: 17135 RVA: 0x001040D4 File Offset: 0x001034D4
		internal DWriteFontFeature[] CachedFeatureSet
		{
			get
			{
				return this._features;
			}
			set
			{
				this._features = value;
			}
		}

		// Token: 0x04001841 RID: 6209
		private DWriteFontFeature[] _features;
	}
}
