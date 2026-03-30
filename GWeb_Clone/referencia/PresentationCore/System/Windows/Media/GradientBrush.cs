using System;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media
{
	/// <summary>Uma classe abstrata que descreve um gradiente, composta por marcas de gradiente. Classes que herdam de <see cref="T:System.Windows.Media.GradientBrush" /> descrevem as diferentes maneiras de interpretar paradas de gradiente.</summary>
	// Token: 0x020003B2 RID: 946
	[ContentProperty("GradientStops")]
	public abstract class GradientBrush : Brush
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.GradientBrush" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true." /></returns>
		// Token: 0x06002407 RID: 9223 RVA: 0x00091298 File Offset: 0x00090698
		public new GradientBrush Clone()
		{
			return (GradientBrush)base.Clone();
		}

		/// <summary>Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.GradientBrush" />, fazendo cópias em profundidade dos valores do objeto atual.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem for <see langword="true" />.</returns>
		// Token: 0x06002408 RID: 9224 RVA: 0x000912B0 File Offset: 0x000906B0
		public new GradientBrush CloneCurrentValue()
		{
			return (GradientBrush)base.CloneCurrentValue();
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000912C8 File Offset: 0x000906C8
		private static void ColorInterpolationModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GradientBrush gradientBrush = (GradientBrush)d;
			gradientBrush.PropertyChanged(GradientBrush.ColorInterpolationModeProperty);
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000912E8 File Offset: 0x000906E8
		private static void MappingModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GradientBrush gradientBrush = (GradientBrush)d;
			gradientBrush.PropertyChanged(GradientBrush.MappingModeProperty);
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x00091308 File Offset: 0x00090708
		private static void SpreadMethodPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GradientBrush gradientBrush = (GradientBrush)d;
			gradientBrush.PropertyChanged(GradientBrush.SpreadMethodProperty);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x00091328 File Offset: 0x00090728
		private static void GradientStopsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			GradientBrush gradientBrush = (GradientBrush)d;
			gradientBrush.PropertyChanged(GradientBrush.GradientStopsProperty);
		}

		/// <summary>Obtém ou define uma enumeração <see cref="T:System.Windows.Media.ColorInterpolationMode" /> que especifica como as cores de gradiente são interpoladas.</summary>
		/// <returns>Especifica como as cores em um gradiente são interpoladas. O padrão é <see cref="F:System.Windows.Media.ColorInterpolationMode.SRgbLinearInterpolation" />.</returns>
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x00091348 File Offset: 0x00090748
		// (set) Token: 0x0600240E RID: 9230 RVA: 0x00091368 File Offset: 0x00090768
		public ColorInterpolationMode ColorInterpolationMode
		{
			get
			{
				return (ColorInterpolationMode)base.GetValue(GradientBrush.ColorInterpolationModeProperty);
			}
			set
			{
				base.SetValueInternal(GradientBrush.ColorInterpolationModeProperty, value);
			}
		}

		/// <summary>Obtém ou define uma enumeração <see cref="T:System.Windows.Media.BrushMappingMode" /> que especifica se as coordenadas de posicionamento de pincel de gradiente são absolutas ou relativas à área de saída.</summary>
		/// <returns>Um valor <see cref="T:System.Windows.Media.BrushMappingMode" /> que especifica como as coordenadas de posicionamento do pincel de gradiente são interpretadas. O padrão é <see cref="F:System.Windows.Media.BrushMappingMode.RelativeToBoundingBox" />.</returns>
		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x0600240F RID: 9231 RVA: 0x00091388 File Offset: 0x00090788
		// (set) Token: 0x06002410 RID: 9232 RVA: 0x000913A8 File Offset: 0x000907A8
		public BrushMappingMode MappingMode
		{
			get
			{
				return (BrushMappingMode)base.GetValue(GradientBrush.MappingModeProperty);
			}
			set
			{
				base.SetValueInternal(GradientBrush.MappingModeProperty, value);
			}
		}

		/// <summary>Obtém ou define o tipo de método de disseminação que especifica como desenhar um gradiente que inicia ou termina dentro dos limites do objeto a ser pintado.</summary>
		/// <returns>O tipo de método de disseminação usado para pintar o gradiente. O padrão é <see cref="F:System.Windows.Media.GradientSpreadMethod.Pad" />.</returns>
		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x000913C8 File Offset: 0x000907C8
		// (set) Token: 0x06002412 RID: 9234 RVA: 0x000913E8 File Offset: 0x000907E8
		public GradientSpreadMethod SpreadMethod
		{
			get
			{
				return (GradientSpreadMethod)base.GetValue(GradientBrush.SpreadMethodProperty);
			}
			set
			{
				base.SetValueInternal(GradientBrush.SpreadMethodProperty, value);
			}
		}

		/// <summary>Obtém ou define as marcas de gradiente do pincel.</summary>
		/// <returns>Uma coleção dos objetos <see cref="T:System.Windows.Media.GradientStop" /> associados ao pincel, cada um dos quais especifica uma cor e um deslocamento ao longo do eixo de gradiente do pincel. O padrão é um <see cref="T:System.Windows.Media.GradientStopCollection" /> vazio.</returns>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x00091408 File Offset: 0x00090808
		// (set) Token: 0x06002414 RID: 9236 RVA: 0x00091428 File Offset: 0x00090828
		public GradientStopCollection GradientStops
		{
			get
			{
				return (GradientStopCollection)base.GetValue(GradientBrush.GradientStopsProperty);
			}
			set
			{
				base.SetValueInternal(GradientBrush.GradientStopsProperty, value);
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002415 RID: 9237 RVA: 0x00091444 File Offset: 0x00090844
		internal override int EffectiveValuesInitialSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x00091454 File Offset: 0x00090854
		static GradientBrush()
		{
			Type typeFromHandle = typeof(GradientBrush);
			GradientBrush.ColorInterpolationModeProperty = Animatable.RegisterProperty("ColorInterpolationMode", typeof(ColorInterpolationMode), typeFromHandle, ColorInterpolationMode.SRgbLinearInterpolation, new PropertyChangedCallback(GradientBrush.ColorInterpolationModePropertyChanged), new ValidateValueCallback(ValidateEnums.IsColorInterpolationModeValid), false, null);
			GradientBrush.MappingModeProperty = Animatable.RegisterProperty("MappingMode", typeof(BrushMappingMode), typeFromHandle, BrushMappingMode.RelativeToBoundingBox, new PropertyChangedCallback(GradientBrush.MappingModePropertyChanged), new ValidateValueCallback(ValidateEnums.IsBrushMappingModeValid), false, null);
			GradientBrush.SpreadMethodProperty = Animatable.RegisterProperty("SpreadMethod", typeof(GradientSpreadMethod), typeFromHandle, GradientSpreadMethod.Pad, new PropertyChangedCallback(GradientBrush.SpreadMethodPropertyChanged), new ValidateValueCallback(ValidateEnums.IsGradientSpreadMethodValid), false, null);
			GradientBrush.GradientStopsProperty = Animatable.RegisterProperty("GradientStops", typeof(GradientStopCollection), typeFromHandle, new FreezableDefaultValueFactory(GradientStopCollection.Empty), new PropertyChangedCallback(GradientBrush.GradientStopsPropertyChanged), null, false, null);
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GradientBrush" />.</summary>
		// Token: 0x06002417 RID: 9239 RVA: 0x00091558 File Offset: 0x00090958
		protected GradientBrush()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.GradientBrush" /> com o <see cref="T:System.Windows.Media.GradientStopCollection" /> especificado.</summary>
		/// <param name="gradientStopCollection">O <see cref="T:System.Windows.Media.GradientStopCollection" /> usado para especificar o local e a cor dos pontos de transição em um gradiente.</param>
		// Token: 0x06002418 RID: 9240 RVA: 0x0009156C File Offset: 0x0009096C
		protected GradientBrush(GradientStopCollection gradientStopCollection)
		{
			this.GradientStops = gradientStopCollection;
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GradientBrush.ColorInterpolationMode" />.</summary>
		// Token: 0x0400115C RID: 4444
		public static readonly DependencyProperty ColorInterpolationModeProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GradientBrush.MappingMode" />.</summary>
		// Token: 0x0400115D RID: 4445
		public static readonly DependencyProperty MappingModeProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GradientBrush.SpreadMethod" />.</summary>
		// Token: 0x0400115E RID: 4446
		public static readonly DependencyProperty SpreadMethodProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.GradientBrush.GradientStops" />.</summary>
		// Token: 0x0400115F RID: 4447
		public static readonly DependencyProperty GradientStopsProperty;

		// Token: 0x04001160 RID: 4448
		internal const ColorInterpolationMode c_ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation;

		// Token: 0x04001161 RID: 4449
		internal const BrushMappingMode c_MappingMode = BrushMappingMode.RelativeToBoundingBox;

		// Token: 0x04001162 RID: 4450
		internal const GradientSpreadMethod c_SpreadMethod = GradientSpreadMethod.Pad;

		// Token: 0x04001163 RID: 4451
		internal static GradientStopCollection s_GradientStops = GradientStopCollection.Empty;
	}
}
