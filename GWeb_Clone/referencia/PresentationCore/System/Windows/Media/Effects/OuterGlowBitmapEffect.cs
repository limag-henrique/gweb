using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media.Effects
{
	/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Cria uma aura colorida em torno de objetos ou áreas de cor.</summary>
	// Token: 0x02000612 RID: 1554
	public sealed class OuterGlowBitmapEffect : BitmapEffect
	{
		// Token: 0x06004787 RID: 18311 RVA: 0x0011831C File Offset: 0x0011771C
		[SecuritySafeCritical]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected override SafeHandle CreateUnmanagedEffect()
		{
			return null;
		}

		// Token: 0x06004788 RID: 18312 RVA: 0x0011832C File Offset: 0x0011772C
		[SecurityCritical]
		[SecurityTreatAsSafe]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected override void UpdateUnmanagedPropertyState(SafeHandle unmanagedEffect)
		{
			SecurityHelper.DemandUIWindowPermission();
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Cria um clone modificável desse <see cref="T:System.Windows.Media.Effects.OuterGlowBitmapEffect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06004789 RID: 18313 RVA: 0x00118340 File Offset: 0x00117740
		public new OuterGlowBitmapEffect Clone()
		{
			return (OuterGlowBitmapEffect)base.Clone();
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.OuterGlowBitmapEffect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600478A RID: 18314 RVA: 0x00118358 File Offset: 0x00117758
		public new OuterGlowBitmapEffect CloneCurrentValue()
		{
			return (OuterGlowBitmapEffect)base.CloneCurrentValue();
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x00118370 File Offset: 0x00117770
		private static void GlowColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			OuterGlowBitmapEffect outerGlowBitmapEffect = (OuterGlowBitmapEffect)d;
			outerGlowBitmapEffect.PropertyChanged(OuterGlowBitmapEffect.GlowColorProperty);
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x00118390 File Offset: 0x00117790
		private static void GlowSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			OuterGlowBitmapEffect outerGlowBitmapEffect = (OuterGlowBitmapEffect)d;
			outerGlowBitmapEffect.PropertyChanged(OuterGlowBitmapEffect.GlowSizeProperty);
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x001183B0 File Offset: 0x001177B0
		private static void NoisePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			OuterGlowBitmapEffect outerGlowBitmapEffect = (OuterGlowBitmapEffect)d;
			outerGlowBitmapEffect.PropertyChanged(OuterGlowBitmapEffect.NoiseProperty);
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x001183D0 File Offset: 0x001177D0
		private static void OpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			OuterGlowBitmapEffect outerGlowBitmapEffect = (OuterGlowBitmapEffect)d;
			outerGlowBitmapEffect.PropertyChanged(OuterGlowBitmapEffect.OpacityProperty);
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Obtém ou define a cor do brilho halo.</summary>
		/// <returns>A cor do brilho halo. O padrão é em branco.</returns>
		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x0600478F RID: 18319 RVA: 0x001183F0 File Offset: 0x001177F0
		// (set) Token: 0x06004790 RID: 18320 RVA: 0x00118410 File Offset: 0x00117810
		public Color GlowColor
		{
			get
			{
				return (Color)base.GetValue(OuterGlowBitmapEffect.GlowColorProperty);
			}
			set
			{
				base.SetValueInternal(OuterGlowBitmapEffect.GlowColorProperty, value);
			}
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Obtém ou define a espessura do brilho halo.</summary>
		/// <returns>A espessura do halo brilham, em unidade independente de dispositivo (1/96 polegada). O intervalo de valores válido é de 1 a 199. O padrão é 20.</returns>
		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06004791 RID: 18321 RVA: 0x00118430 File Offset: 0x00117830
		// (set) Token: 0x06004792 RID: 18322 RVA: 0x00118450 File Offset: 0x00117850
		public double GlowSize
		{
			get
			{
				return (double)base.GetValue(OuterGlowBitmapEffect.GlowSizeProperty);
			}
			set
			{
				base.SetValueInternal(OuterGlowBitmapEffect.GlowSizeProperty, value);
			}
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Obtém ou define a granulação do brilho halo.</summary>
		/// <returns>(Nível de ruído) Granulação do brilho halo. O intervalo de valores válidos é de 0,0 a 1,0, 0,0 especificando nenhum ruído e 1.0 especificando ruído máxima. Um valor de 0,5 indica o ruído de 50 por cento, um valor de 0,75 indica o ruído de 75 por cento e assim por diante. O valor padrão é 0,0.</returns>
		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06004793 RID: 18323 RVA: 0x00118470 File Offset: 0x00117870
		// (set) Token: 0x06004794 RID: 18324 RVA: 0x00118490 File Offset: 0x00117890
		public double Noise
		{
			get
			{
				return (double)base.GetValue(OuterGlowBitmapEffect.NoiseProperty);
			}
			set
			{
				base.SetValueInternal(OuterGlowBitmapEffect.NoiseProperty, value);
			}
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Obtém ou define o grau de opacidade do brilho halo.</summary>
		/// <returns>O nível de opacidade do brilho. Um valor de 0 indica que o brilho halo é completamente transparente, enquanto um valor de 1 indica que o brilho é completamente opaco. Um valor de 0,5 indica que o brilho é 50 por cento opaco, que um valor de 0.725 indica que o brilho é 72.5% opaco e assim por diante. Valores menores que 0 são tratados como 0, enquanto valores maiores que 1 são tratados como 1. O padrão é 1.</returns>
		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06004795 RID: 18325 RVA: 0x001184B0 File Offset: 0x001178B0
		// (set) Token: 0x06004796 RID: 18326 RVA: 0x001184D0 File Offset: 0x001178D0
		public double Opacity
		{
			get
			{
				return (double)base.GetValue(OuterGlowBitmapEffect.OpacityProperty);
			}
			set
			{
				base.SetValueInternal(OuterGlowBitmapEffect.OpacityProperty, value);
			}
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x001184F0 File Offset: 0x001178F0
		protected override Freezable CreateInstanceCore()
		{
			return new OuterGlowBitmapEffect();
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x00118504 File Offset: 0x00117904
		static OuterGlowBitmapEffect()
		{
			Type typeFromHandle = typeof(OuterGlowBitmapEffect);
			OuterGlowBitmapEffect.GlowColorProperty = Animatable.RegisterProperty("GlowColor", typeof(Color), typeFromHandle, Colors.Gold, new PropertyChangedCallback(OuterGlowBitmapEffect.GlowColorPropertyChanged), null, true, null);
			OuterGlowBitmapEffect.GlowSizeProperty = Animatable.RegisterProperty("GlowSize", typeof(double), typeFromHandle, 5.0, new PropertyChangedCallback(OuterGlowBitmapEffect.GlowSizePropertyChanged), null, true, null);
			OuterGlowBitmapEffect.NoiseProperty = Animatable.RegisterProperty("Noise", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(OuterGlowBitmapEffect.NoisePropertyChanged), null, true, null);
			OuterGlowBitmapEffect.OpacityProperty = Animatable.RegisterProperty("Opacity", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(OuterGlowBitmapEffect.OpacityPropertyChanged), null, true, null);
		}

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.OuterGlowBitmapEffect.GlowColor" />.</summary>
		// Token: 0x04001A0F RID: 6671
		public static readonly DependencyProperty GlowColorProperty;

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.OuterGlowBitmapEffect.GlowSize" />.</summary>
		// Token: 0x04001A10 RID: 6672
		public static readonly DependencyProperty GlowSizeProperty;

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.OuterGlowBitmapEffect.Noise" />.</summary>
		// Token: 0x04001A11 RID: 6673
		public static readonly DependencyProperty NoiseProperty;

		/// <summary>Observação: esta API agora está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.BlurEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.OuterGlowBitmapEffect.Opacity" />.</summary>
		// Token: 0x04001A12 RID: 6674
		public static readonly DependencyProperty OpacityProperty;

		// Token: 0x04001A13 RID: 6675
		internal static Color s_GlowColor = Colors.Gold;

		// Token: 0x04001A14 RID: 6676
		internal const double c_GlowSize = 5.0;

		// Token: 0x04001A15 RID: 6677
		internal const double c_Noise = 0.0;

		// Token: 0x04001A16 RID: 6678
		internal const double c_Opacity = 1.0;
	}
}
