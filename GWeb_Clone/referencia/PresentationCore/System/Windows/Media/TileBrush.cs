using System;
using System.Security;
using System.Windows.Media.Animation;
using System.Windows.Media.Composition;
using MS.Win32.PresentationCore;

namespace System.Windows.Media
{
	/// <summary>Descreve uma maneira de pintar uma região usando um ou mais blocos.</summary>
	// Token: 0x020003F5 RID: 1013
	public abstract class TileBrush : Brush
	{
		/// <summary>Cria uma cópia modificável deste <see cref="T:System.Windows.Media.TileBrush" /> fazendo cópias em profundidade de seus valores.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado retorna <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true." /></returns>
		// Token: 0x06002801 RID: 10241 RVA: 0x000A0CEC File Offset: 0x000A00EC
		public new TileBrush Clone()
		{
			return (TileBrush)base.Clone();
		}

		/// <summary>Cria uma cópia modificável deste objeto <see cref="T:System.Windows.Media.TileBrush" /> fazendo cópias em profundidade de seus valores. Esse método não copia referências de recurso, associações de dados ou animações, embora ele copie os valores atuais.</summary>
		/// <returns>Uma cópia em profundidade modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado é <see langword="false" /> mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem é <see langword="true" />.</returns>
		// Token: 0x06002802 RID: 10242 RVA: 0x000A0D04 File Offset: 0x000A0104
		public new TileBrush CloneCurrentValue()
		{
			return (TileBrush)base.CloneCurrentValue();
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x000A0D1C File Offset: 0x000A011C
		private static void ViewportUnitsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(TileBrush.ViewportUnitsProperty);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000A0D3C File Offset: 0x000A013C
		private static void ViewboxUnitsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(TileBrush.ViewboxUnitsProperty);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000A0D5C File Offset: 0x000A015C
		private static void ViewportPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(TileBrush.ViewportProperty);
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000A0D7C File Offset: 0x000A017C
		private static void ViewboxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(TileBrush.ViewboxProperty);
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000A0D9C File Offset: 0x000A019C
		private static void StretchPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(TileBrush.StretchProperty);
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000A0DBC File Offset: 0x000A01BC
		private static void TileModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(TileBrush.TileModeProperty);
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x000A0DDC File Offset: 0x000A01DC
		private static void AlignmentXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(TileBrush.AlignmentXProperty);
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x000A0DFC File Offset: 0x000A01FC
		private static void AlignmentYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(TileBrush.AlignmentYProperty);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x000A0E1C File Offset: 0x000A021C
		private static void CachingHintPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(RenderOptions.CachingHintProperty);
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x000A0E3C File Offset: 0x000A023C
		private static void CacheInvalidationThresholdMinimumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(RenderOptions.CacheInvalidationThresholdMinimumProperty);
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x000A0E5C File Offset: 0x000A025C
		private static void CacheInvalidationThresholdMaximumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			TileBrush tileBrush = (TileBrush)d;
			tileBrush.PropertyChanged(RenderOptions.CacheInvalidationThresholdMaximumProperty);
		}

		/// <summary>Obtém ou define uma enumeração <see cref="T:System.Windows.Media.BrushMappingMode" /> que especifica se o valor da <see cref="P:System.Windows.Media.TileBrush.Viewport" />, que indica o tamanho e posição do bloco base <see cref="T:System.Windows.Media.TileBrush" />, é relativo ao tamanho da área de saída.</summary>
		/// <returns>Indica se o valor da <see cref="P:System.Windows.Media.TileBrush.Viewport" />, que descreve o tamanho e posição dos blocos <see cref="T:System.Windows.Media.TileBrush" />, é relativo ao tamanho de toda a área de saída. O valor padrão é <see cref="F:System.Windows.Media.BrushMappingMode.RelativeToBoundingBox" />.</returns>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x000A0E7C File Offset: 0x000A027C
		// (set) Token: 0x0600280F RID: 10255 RVA: 0x000A0E9C File Offset: 0x000A029C
		public BrushMappingMode ViewportUnits
		{
			get
			{
				return (BrushMappingMode)base.GetValue(TileBrush.ViewportUnitsProperty);
			}
			set
			{
				base.SetValueInternal(TileBrush.ViewportUnitsProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica se o valor <see cref="P:System.Windows.Media.TileBrush.Viewbox" /> se relaciona com a caixa delimitadora dos conteúdos <see cref="T:System.Windows.Media.TileBrush" /> ou se o valor é absoluto.</summary>
		/// <returns>Um valor que indica se o <see cref="P:System.Windows.Media.TileBrush.Viewbox" /> valor é a caixa delimitadora do <see cref="T:System.Windows.Media.TileBrush" /> conteúdo ou se é um valor absoluto. O valor padrão é <see cref="F:System.Windows.Media.BrushMappingMode.RelativeToBoundingBox" />.</returns>
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x000A0EBC File Offset: 0x000A02BC
		// (set) Token: 0x06002811 RID: 10257 RVA: 0x000A0EDC File Offset: 0x000A02DC
		public BrushMappingMode ViewboxUnits
		{
			get
			{
				return (BrushMappingMode)base.GetValue(TileBrush.ViewboxUnitsProperty);
			}
			set
			{
				base.SetValueInternal(TileBrush.ViewboxUnitsProperty, value);
			}
		}

		/// <summary>Obtém ou define a posição e as dimensões do bloco base de um <see cref="T:System.Windows.Media.TileBrush" />.</summary>
		/// <returns>A posição e as dimensões do bloco base de um <see cref="T:System.Windows.Media.TileBrush" />. O valor padrão é um retângulo (<see cref="T:System.Windows.Rect" />) com uma <see cref="P:System.Windows.Rect.TopLeft" /> de (0,0) e uma <see cref="P:System.Windows.Rect.Width" /> e uma <see cref="P:System.Windows.Rect.Height" /> de 1.</returns>
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x000A0EFC File Offset: 0x000A02FC
		// (set) Token: 0x06002813 RID: 10259 RVA: 0x000A0F1C File Offset: 0x000A031C
		public Rect Viewport
		{
			get
			{
				return (Rect)base.GetValue(TileBrush.ViewportProperty);
			}
			set
			{
				base.SetValueInternal(TileBrush.ViewportProperty, value);
			}
		}

		/// <summary>Obtém ou define a posição e as dimensões do conteúdo em um bloco <see cref="T:System.Windows.Media.TileBrush" />.</summary>
		/// <returns>A posição e as dimensões do conteúdo <see cref="T:System.Windows.Media.TileBrush" />. O valor padrão é um retângulo (<see cref="T:System.Windows.Rect" />) com um <see cref="P:System.Windows.Rect.TopLeft" /> de (0,0) e um <see cref="P:System.Windows.Rect.Width" /> e <see cref="P:System.Windows.Rect.Height" /> de 1.</returns>
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x000A0F3C File Offset: 0x000A033C
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x000A0F5C File Offset: 0x000A035C
		public Rect Viewbox
		{
			get
			{
				return (Rect)base.GetValue(TileBrush.ViewboxProperty);
			}
			set
			{
				base.SetValueInternal(TileBrush.ViewboxProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica como o conteúdo deste <see cref="T:System.Windows.Media.TileBrush" /> é ampliado para se ajustar a seus blocos.</summary>
		/// <returns>Um valor que especifica como este conteúdo <see cref="T:System.Windows.Media.TileBrush" /> é projetado em seu bloco base. O valor padrão é <see cref="F:System.Windows.Media.Stretch.Fill" />.</returns>
		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x000A0F7C File Offset: 0x000A037C
		// (set) Token: 0x06002817 RID: 10263 RVA: 0x000A0F9C File Offset: 0x000A039C
		public Stretch Stretch
		{
			get
			{
				return (Stretch)base.GetValue(TileBrush.StretchProperty);
			}
			set
			{
				base.SetValueInternal(TileBrush.StretchProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que especifica como um <see cref="T:System.Windows.Media.TileBrush" /> preenche a área que você estiver pintando se o bloco base for menor que a área de saída.</summary>
		/// <returns>Um valor que especifica como os blocos <see cref="T:System.Windows.Media.TileBrush" /> preenchem a área de saída quando o bloco base, que é especificado pela propriedade <see cref="P:System.Windows.Media.TileBrush.Viewport" />, é menor do que a área de saída. O valor padrão é <see cref="F:System.Windows.Media.TileMode.None" />.</returns>
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x000A0FBC File Offset: 0x000A03BC
		// (set) Token: 0x06002819 RID: 10265 RVA: 0x000A0FDC File Offset: 0x000A03DC
		public TileMode TileMode
		{
			get
			{
				return (TileMode)base.GetValue(TileBrush.TileModeProperty);
			}
			set
			{
				base.SetValueInternal(TileBrush.TileModeProperty, value);
			}
		}

		/// <summary>Obtém ou define o alinhamento horizontal do conteúdo no bloco base <see cref="T:System.Windows.Media.TileBrush" />.</summary>
		/// <returns>Um valor que especifica a posição horizontal do conteúdo <see cref="T:System.Windows.Media.TileBrush" /> em seu bloco base. O valor padrão é <see cref="F:System.Windows.HorizontalAlignment.Center" />.</returns>
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x000A0FFC File Offset: 0x000A03FC
		// (set) Token: 0x0600281B RID: 10267 RVA: 0x000A101C File Offset: 0x000A041C
		public AlignmentX AlignmentX
		{
			get
			{
				return (AlignmentX)base.GetValue(TileBrush.AlignmentXProperty);
			}
			set
			{
				base.SetValueInternal(TileBrush.AlignmentXProperty, value);
			}
		}

		/// <summary>Obtém ou define o alinhamento vertical do conteúdo no bloco base <see cref="T:System.Windows.Media.TileBrush" />.</summary>
		/// <returns>Um valor que especifica a posição vertical de <see cref="T:System.Windows.Media.TileBrush" /> conteúdo em seu bloco base. O valor padrão é <see cref="F:System.Windows.Media.AlignmentY.Center" />.</returns>
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600281C RID: 10268 RVA: 0x000A103C File Offset: 0x000A043C
		// (set) Token: 0x0600281D RID: 10269 RVA: 0x000A105C File Offset: 0x000A045C
		public AlignmentY AlignmentY
		{
			get
			{
				return (AlignmentY)base.GetValue(TileBrush.AlignmentYProperty);
			}
			set
			{
				base.SetValueInternal(TileBrush.AlignmentYProperty, value);
			}
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000A107C File Offset: 0x000A047C
		static TileBrush()
		{
			RenderOptions.CachingHintProperty.OverrideMetadata(typeof(TileBrush), new UIPropertyMetadata(CachingHint.Unspecified, new PropertyChangedCallback(TileBrush.CachingHintPropertyChanged)));
			RenderOptions.CacheInvalidationThresholdMinimumProperty.OverrideMetadata(typeof(TileBrush), new UIPropertyMetadata(0.707, new PropertyChangedCallback(TileBrush.CacheInvalidationThresholdMinimumPropertyChanged)));
			RenderOptions.CacheInvalidationThresholdMaximumProperty.OverrideMetadata(typeof(TileBrush), new UIPropertyMetadata(1.414, new PropertyChangedCallback(TileBrush.CacheInvalidationThresholdMaximumPropertyChanged)));
			Type typeFromHandle = typeof(TileBrush);
			TileBrush.ViewportUnitsProperty = Animatable.RegisterProperty("ViewportUnits", typeof(BrushMappingMode), typeFromHandle, BrushMappingMode.RelativeToBoundingBox, new PropertyChangedCallback(TileBrush.ViewportUnitsPropertyChanged), new ValidateValueCallback(ValidateEnums.IsBrushMappingModeValid), false, null);
			TileBrush.ViewboxUnitsProperty = Animatable.RegisterProperty("ViewboxUnits", typeof(BrushMappingMode), typeFromHandle, BrushMappingMode.RelativeToBoundingBox, new PropertyChangedCallback(TileBrush.ViewboxUnitsPropertyChanged), new ValidateValueCallback(ValidateEnums.IsBrushMappingModeValid), false, null);
			TileBrush.ViewportProperty = Animatable.RegisterProperty("Viewport", typeof(Rect), typeFromHandle, new Rect(0.0, 0.0, 1.0, 1.0), new PropertyChangedCallback(TileBrush.ViewportPropertyChanged), null, true, null);
			TileBrush.ViewboxProperty = Animatable.RegisterProperty("Viewbox", typeof(Rect), typeFromHandle, new Rect(0.0, 0.0, 1.0, 1.0), new PropertyChangedCallback(TileBrush.ViewboxPropertyChanged), null, true, null);
			TileBrush.StretchProperty = Animatable.RegisterProperty("Stretch", typeof(Stretch), typeFromHandle, Stretch.Fill, new PropertyChangedCallback(TileBrush.StretchPropertyChanged), new ValidateValueCallback(ValidateEnums.IsStretchValid), false, null);
			TileBrush.TileModeProperty = Animatable.RegisterProperty("TileMode", typeof(TileMode), typeFromHandle, TileMode.None, new PropertyChangedCallback(TileBrush.TileModePropertyChanged), new ValidateValueCallback(ValidateEnums.IsTileModeValid), false, null);
			TileBrush.AlignmentXProperty = Animatable.RegisterProperty("AlignmentX", typeof(AlignmentX), typeFromHandle, AlignmentX.Center, new PropertyChangedCallback(TileBrush.AlignmentXPropertyChanged), new ValidateValueCallback(ValidateEnums.IsAlignmentXValid), false, null);
			TileBrush.AlignmentYProperty = Animatable.RegisterProperty("AlignmentY", typeof(AlignmentY), typeFromHandle, AlignmentY.Center, new PropertyChangedCallback(TileBrush.AlignmentYPropertyChanged), new ValidateValueCallback(ValidateEnums.IsAlignmentYValid), false, null);
		}

		/// <summary>Obtém os limites atuais do conteúdo <see cref="T:System.Windows.Media.TileBrush" /></summary>
		/// <param name="contentBounds">Os limites de saída do conteúdo <see cref="T:System.Windows.Media.TileBrush" />.</param>
		// Token: 0x06002820 RID: 10272
		protected abstract void GetContentBounds(out Rect contentBounds);

		// Token: 0x06002821 RID: 10273 RVA: 0x000A13A0 File Offset: 0x000A07A0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal unsafe void GetTileBrushMapping(Rect shapeFillBounds, out Matrix tileBrushMapping)
		{
			Rect empty = Rect.Empty;
			BrushMappingMode viewboxUnits = this.ViewboxUnits;
			bool flag = false;
			tileBrushMapping = Matrix.Identity;
			if (viewboxUnits == BrushMappingMode.RelativeToBoundingBox)
			{
				this.GetContentBounds(out empty);
				if (empty == Rect.Empty)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				Rect viewport = this.Viewport;
				Rect viewbox = this.Viewbox;
				Matrix matrix;
				Transform.GetTransformValue(base.Transform, out matrix);
				Matrix matrix2;
				Transform.GetTransformValue(base.RelativeTransform, out matrix2);
				D3DMATRIX d3DMATRIX;
				MILUtilities.ConvertToD3DMATRIX(&matrix, &d3DMATRIX);
				D3DMATRIX d3DMATRIX2;
				MILUtilities.ConvertToD3DMATRIX(&matrix2, &d3DMATRIX2);
				D3DMATRIX d3DMATRIX3;
				int num;
				UnsafeNativeMethods.MilCoreApi.MilUtility_GetTileBrushMapping(&d3DMATRIX, &d3DMATRIX2, this.Stretch, this.AlignmentX, this.AlignmentY, this.ViewportUnits, viewboxUnits, &shapeFillBounds, &empty, ref viewport, ref viewbox, out d3DMATRIX3, out num);
				if (num == 0)
				{
					Matrix matrix3;
					MILUtilities.ConvertFromD3DMATRIX(&d3DMATRIX3, &matrix3);
					tileBrushMapping = matrix3;
				}
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TileBrush.ViewportUnits" />.</summary>
		// Token: 0x04001276 RID: 4726
		public static readonly DependencyProperty ViewportUnitsProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TileBrush.ViewboxUnits" />.</summary>
		// Token: 0x04001277 RID: 4727
		public static readonly DependencyProperty ViewboxUnitsProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TileBrush.Viewport" />.</summary>
		// Token: 0x04001278 RID: 4728
		public static readonly DependencyProperty ViewportProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TileBrush.Viewbox" />.</summary>
		// Token: 0x04001279 RID: 4729
		public static readonly DependencyProperty ViewboxProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TileBrush.Stretch" />.</summary>
		// Token: 0x0400127A RID: 4730
		public static readonly DependencyProperty StretchProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TileBrush.TileMode" />.</summary>
		// Token: 0x0400127B RID: 4731
		public static readonly DependencyProperty TileModeProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TileBrush.AlignmentX" />.</summary>
		// Token: 0x0400127C RID: 4732
		public static readonly DependencyProperty AlignmentXProperty;

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.TileBrush.AlignmentY" />.</summary>
		// Token: 0x0400127D RID: 4733
		public static readonly DependencyProperty AlignmentYProperty;

		// Token: 0x0400127E RID: 4734
		internal const BrushMappingMode c_ViewportUnits = BrushMappingMode.RelativeToBoundingBox;

		// Token: 0x0400127F RID: 4735
		internal const BrushMappingMode c_ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;

		// Token: 0x04001280 RID: 4736
		internal static Rect s_Viewport = new Rect(0.0, 0.0, 1.0, 1.0);

		// Token: 0x04001281 RID: 4737
		internal static Rect s_Viewbox = new Rect(0.0, 0.0, 1.0, 1.0);

		// Token: 0x04001282 RID: 4738
		internal const Stretch c_Stretch = Stretch.Fill;

		// Token: 0x04001283 RID: 4739
		internal const TileMode c_TileMode = TileMode.None;

		// Token: 0x04001284 RID: 4740
		internal const AlignmentX c_AlignmentX = AlignmentX.Center;

		// Token: 0x04001285 RID: 4741
		internal const AlignmentY c_AlignmentY = AlignmentY.Center;

		// Token: 0x04001286 RID: 4742
		internal const CachingHint c_CachingHint = CachingHint.Unspecified;

		// Token: 0x04001287 RID: 4743
		internal const double c_CacheInvalidationThresholdMinimum = 0.707;

		// Token: 0x04001288 RID: 4744
		internal const double c_CacheInvalidationThresholdMaximum = 1.414;
	}
}
