using System;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace System.Windows
{
	/// <summary>Representa uma decoração de texto, que é um ornamento visual adicionado ao texto (como um sublinhado).</summary>
	// Token: 0x020001F1 RID: 497
	[Localizability(LocalizationCategory.None)]
	public sealed class TextDecoration : Animatable
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.TextDecoration" />.</summary>
		// Token: 0x06000CD7 RID: 3287 RVA: 0x0003091C File Offset: 0x0002FD1C
		public TextDecoration()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.TextDecoration" /> com os valores <see cref="P:System.Windows.TextDecoration.Location" />, <see cref="P:System.Windows.TextDecoration.Pen" />, <see cref="P:System.Windows.TextDecoration.PenOffset" />, <see cref="P:System.Windows.TextDecoration.PenOffsetUnit" /> e <see cref="P:System.Windows.TextDecoration.PenThicknessUnit" />.</summary>
		/// <param name="location">O local da decoração de texto.</param>
		/// <param name="pen">A <see cref="T:System.Windows.Media.Pen" /> usada para desenhar a decoração de texto. Se esse valor for <see langword="null" />, a cor da decoração de texto corresponderá à cor do texto à qual ela é aplicada e a espessura da decoração de texto será definida com a espessura recomendada da fonte.</param>
		/// <param name="penOffset">O deslocamento vertical do local da decoração de texto. Um valor negativo move a decoração para a parte inferior, enquanto um valor positivo move a decoração para a parte superior.</param>
		/// <param name="penOffsetUnit">As unidades usadas para interpretar o valor do <paramref name="penOffset" />.</param>
		/// <param name="penThicknessUnit">As unidades usadas para interpretar o valor da <see cref="P:System.Windows.Media.Pen.Thickness" /> para a <paramref name="pen" />.</param>
		// Token: 0x06000CD8 RID: 3288 RVA: 0x00030930 File Offset: 0x0002FD30
		public TextDecoration(TextDecorationLocation location, Pen pen, double penOffset, TextDecorationUnit penOffsetUnit, TextDecorationUnit penThicknessUnit)
		{
			this.Location = location;
			this.Pen = pen;
			this.PenOffset = penOffset;
			this.PenOffsetUnit = penOffsetUnit;
			this.PenThicknessUnit = penThicknessUnit;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00030968 File Offset: 0x0002FD68
		internal bool ValueEquals(TextDecoration textDecoration)
		{
			if (textDecoration == null)
			{
				return false;
			}
			if (this == textDecoration)
			{
				return true;
			}
			if (this.Location != textDecoration.Location || this.PenOffset != textDecoration.PenOffset || this.PenOffsetUnit != textDecoration.PenOffsetUnit || this.PenThicknessUnit != textDecoration.PenThicknessUnit)
			{
				return false;
			}
			if (this.Pen != null)
			{
				return this.Pen.Equals(textDecoration.Pen);
			}
			return textDecoration.Pen == null;
		}

		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.TextDecoration" />, fazendo cópias em profundidade dos valores do objeto.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06000CDA RID: 3290 RVA: 0x000309E0 File Offset: 0x0002FDE0
		public new TextDecoration Clone()
		{
			return (TextDecoration)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.TextDecoration" />, fazendo cópias em profundidade dos valores do objeto atual.</summary>
		/// <returns>Um clone modificável do objeto atual. O valor da propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado é <see langword="false" />, mesmo se o valor da propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true" />.</returns>
		// Token: 0x06000CDB RID: 3291 RVA: 0x000309F8 File Offset: 0x0002FDF8
		public new TextDecoration CloneCurrentValue()
		{
			return (TextDecoration)base.CloneCurrentValue();
		}

		/// <summary>Obtém ou define a <see cref="T:System.Windows.Media.Pen" /> usada para desenhar a decoração de texto.</summary>
		/// <returns>A <see cref="T:System.Windows.Media.Pen" /> usada para desenhar a decoração de texto. Se esse valor for nulo, a cor da decoração corresponde ao texto ao qual ela é aplicada e a espessura da decoração é definida como a fonte recomendada espessura.</returns>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x00030A10 File Offset: 0x0002FE10
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x00030A30 File Offset: 0x0002FE30
		public Pen Pen
		{
			get
			{
				return (Pen)base.GetValue(TextDecoration.PenProperty);
			}
			set
			{
				base.SetValueInternal(TextDecoration.PenProperty, value);
			}
		}

		/// <summary>Obtém ou define o deslocamento da decoração de texto de seu <see cref="P:System.Windows.TextDecoration.Location" />.</summary>
		/// <returns>A decoração de texto de deslocamento a partir de seu <see cref="P:System.Windows.TextDecoration.Location" />. O padrão é 0.</returns>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x00030A4C File Offset: 0x0002FE4C
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x00030A6C File Offset: 0x0002FE6C
		public double PenOffset
		{
			get
			{
				return (double)base.GetValue(TextDecoration.PenOffsetProperty);
			}
			set
			{
				base.SetValueInternal(TextDecoration.PenOffsetProperty, value);
			}
		}

		/// <summary>Obtém as unidades nas quais o valor <see cref="P:System.Windows.TextDecoration.PenOffset" /> é expresso.</summary>
		/// <returns>A unidade na qual o <see cref="P:System.Windows.TextDecoration.PenOffset" /> valor é expresso. O padrão é <see cref="F:System.Windows.TextDecorationUnit.FontRecommended" />.</returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00030A8C File Offset: 0x0002FE8C
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x00030AAC File Offset: 0x0002FEAC
		public TextDecorationUnit PenOffsetUnit
		{
			get
			{
				return (TextDecorationUnit)base.GetValue(TextDecoration.PenOffsetUnitProperty);
			}
			set
			{
				base.SetValueInternal(TextDecoration.PenOffsetUnitProperty, value);
			}
		}

		/// <summary>Obtém as unidades nas quais a <see cref="P:System.Windows.Media.Pen.Thickness" /> da <see cref="P:System.Windows.TextDecoration.Pen" /> da decoração de texto é expressa.</summary>
		/// <returns>A unidade na qual o <see cref="P:System.Windows.Media.Pen.Thickness" /> da decoração do texto <see cref="P:System.Windows.TextDecoration.Pen" /> é expresso. O padrão é <see cref="F:System.Windows.TextDecorationUnit.FontRecommended" />.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00030ACC File Offset: 0x0002FECC
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x00030AEC File Offset: 0x0002FEEC
		public TextDecorationUnit PenThicknessUnit
		{
			get
			{
				return (TextDecorationUnit)base.GetValue(TextDecoration.PenThicknessUnitProperty);
			}
			set
			{
				base.SetValueInternal(TextDecoration.PenThicknessUnitProperty, value);
			}
		}

		/// <summary>Obtém ou define a localização vertical na qual a decoração de texto é desenhada.</summary>
		/// <returns>A localização vertical na qual a decoração de texto é desenhada.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00030B0C File Offset: 0x0002FF0C
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x00030B2C File Offset: 0x0002FF2C
		public TextDecorationLocation Location
		{
			get
			{
				return (TextDecorationLocation)base.GetValue(TextDecoration.LocationProperty);
			}
			set
			{
				base.SetValueInternal(TextDecoration.LocationProperty, value);
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00030B4C File Offset: 0x0002FF4C
		protected override Freezable CreateInstanceCore()
		{
			return new TextDecoration();
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00030B60 File Offset: 0x0002FF60
		static TextDecoration()
		{
			Type typeFromHandle = typeof(TextDecoration);
			TextDecoration.PenProperty = Animatable.RegisterProperty("Pen", typeof(Pen), typeFromHandle, null, null, null, false, null);
			TextDecoration.PenOffsetProperty = Animatable.RegisterProperty("PenOffset", typeof(double), typeFromHandle, 0.0, null, null, false, null);
			TextDecoration.PenOffsetUnitProperty = Animatable.RegisterProperty("PenOffsetUnit", typeof(TextDecorationUnit), typeFromHandle, TextDecorationUnit.FontRecommended, null, new ValidateValueCallback(ValidateEnums.IsTextDecorationUnitValid), false, null);
			TextDecoration.PenThicknessUnitProperty = Animatable.RegisterProperty("PenThicknessUnit", typeof(TextDecorationUnit), typeFromHandle, TextDecorationUnit.FontRecommended, null, new ValidateValueCallback(ValidateEnums.IsTextDecorationUnitValid), false, null);
			TextDecoration.LocationProperty = Animatable.RegisterProperty("Location", typeof(TextDecorationLocation), typeFromHandle, TextDecorationLocation.Underline, null, new ValidateValueCallback(ValidateEnums.IsTextDecorationLocationValid), false, null);
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.TextDecoration.Pen" />.</summary>
		// Token: 0x040007BA RID: 1978
		public static readonly DependencyProperty PenProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.TextDecoration.PenOffset" />.</summary>
		// Token: 0x040007BB RID: 1979
		public static readonly DependencyProperty PenOffsetProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.TextDecoration.PenOffsetUnit" />.</summary>
		// Token: 0x040007BC RID: 1980
		public static readonly DependencyProperty PenOffsetUnitProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.TextDecoration.PenThicknessUnit" />.</summary>
		// Token: 0x040007BD RID: 1981
		public static readonly DependencyProperty PenThicknessUnitProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.TextDecoration.Location" />.</summary>
		// Token: 0x040007BE RID: 1982
		public static readonly DependencyProperty LocationProperty;

		// Token: 0x040007BF RID: 1983
		internal const double c_PenOffset = 0.0;

		// Token: 0x040007C0 RID: 1984
		internal const TextDecorationUnit c_PenOffsetUnit = TextDecorationUnit.FontRecommended;

		// Token: 0x040007C1 RID: 1985
		internal const TextDecorationUnit c_PenThicknessUnit = TextDecorationUnit.FontRecommended;

		// Token: 0x040007C2 RID: 1986
		internal const TextDecorationLocation c_Location = TextDecorationLocation.Underline;
	}
}
