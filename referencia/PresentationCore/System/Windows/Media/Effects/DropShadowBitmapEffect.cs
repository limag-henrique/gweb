using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media.Effects
{
	/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Aplica uma sombra atrás de um objeto visual em um deslocamento pequeno. O deslocamento é determinado pela imitação de um sombreamento de uma fonte de luz imaginária.</summary>
	// Token: 0x0200060F RID: 1551
	public sealed class DropShadowBitmapEffect : BitmapEffect
	{
		// Token: 0x06004747 RID: 18247 RVA: 0x0011779C File Offset: 0x00116B9C
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		[SecuritySafeCritical]
		protected override SafeHandle CreateUnmanagedEffect()
		{
			return null;
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x001177AC File Offset: 0x00116BAC
		[SecurityCritical]
		[SecurityTreatAsSafe]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected override void UpdateUnmanagedPropertyState(SafeHandle unmanagedEffect)
		{
			SecurityHelper.DemandUIWindowPermission();
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x001177C0 File Offset: 0x00116BC0
		internal override bool CanBeEmulatedUsingEffectPipeline()
		{
			return this.Noise == 0.0;
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x001177E0 File Offset: 0x00116BE0
		internal override Effect GetEmulatingEffect()
		{
			if (this._imageEffectEmulation != null && this._imageEffectEmulation.IsFrozen)
			{
				return this._imageEffectEmulation;
			}
			if (this._imageEffectEmulation == null)
			{
				this._imageEffectEmulation = new DropShadowEffect();
			}
			Color color = this.Color;
			if (this._imageEffectEmulation.Color != color)
			{
				this._imageEffectEmulation.Color = color;
			}
			double shadowDepth = this.ShadowDepth;
			if (this._imageEffectEmulation.ShadowDepth != shadowDepth)
			{
				if (shadowDepth >= 50.0)
				{
					this._imageEffectEmulation.ShadowDepth = 50.0;
				}
				else if (shadowDepth < 0.0)
				{
					this._imageEffectEmulation.ShadowDepth = 0.0;
				}
				else
				{
					this._imageEffectEmulation.ShadowDepth = shadowDepth;
				}
			}
			double direction = this.Direction;
			if (this._imageEffectEmulation.Direction != direction)
			{
				this._imageEffectEmulation.Direction = direction;
			}
			double opacity = this.Opacity;
			if (this._imageEffectEmulation.Opacity != opacity)
			{
				if (opacity >= 1.0)
				{
					this._imageEffectEmulation.Opacity = 1.0;
				}
				else if (opacity <= 0.0)
				{
					this._imageEffectEmulation.Opacity = 0.0;
				}
				else
				{
					this._imageEffectEmulation.Opacity = opacity;
				}
			}
			double softness = this.Softness;
			if (this._imageEffectEmulation.BlurRadius / 25.0 != softness)
			{
				if (softness >= 1.0)
				{
					this._imageEffectEmulation.BlurRadius = 25.0;
				}
				else if (softness <= 0.0)
				{
					this._imageEffectEmulation.BlurRadius = 0.0;
				}
				else
				{
					this._imageEffectEmulation.BlurRadius = 25.0 * softness;
				}
			}
			this._imageEffectEmulation.RenderingBias = RenderingBias.Performance;
			if (base.IsFrozen)
			{
				this._imageEffectEmulation.Freeze();
			}
			return this._imageEffectEmulation;
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Cria um clone modificável desse <see cref="T:System.Windows.Media.Effects.DropShadowBitmapEffect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600474B RID: 18251 RVA: 0x001179D0 File Offset: 0x00116DD0
		public new DropShadowBitmapEffect Clone()
		{
			return (DropShadowBitmapEffect)base.Clone();
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.DropShadowBitmapEffect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600474C RID: 18252 RVA: 0x001179E8 File Offset: 0x00116DE8
		public new DropShadowBitmapEffect CloneCurrentValue()
		{
			return (DropShadowBitmapEffect)base.CloneCurrentValue();
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x00117A00 File Offset: 0x00116E00
		private static void ShadowDepthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowBitmapEffect dropShadowBitmapEffect = (DropShadowBitmapEffect)d;
			dropShadowBitmapEffect.PropertyChanged(DropShadowBitmapEffect.ShadowDepthProperty);
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x00117A20 File Offset: 0x00116E20
		private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowBitmapEffect dropShadowBitmapEffect = (DropShadowBitmapEffect)d;
			dropShadowBitmapEffect.PropertyChanged(DropShadowBitmapEffect.ColorProperty);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x00117A40 File Offset: 0x00116E40
		private static void DirectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowBitmapEffect dropShadowBitmapEffect = (DropShadowBitmapEffect)d;
			dropShadowBitmapEffect.PropertyChanged(DropShadowBitmapEffect.DirectionProperty);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x00117A60 File Offset: 0x00116E60
		private static void NoisePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowBitmapEffect dropShadowBitmapEffect = (DropShadowBitmapEffect)d;
			dropShadowBitmapEffect.PropertyChanged(DropShadowBitmapEffect.NoiseProperty);
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x00117A80 File Offset: 0x00116E80
		private static void OpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowBitmapEffect dropShadowBitmapEffect = (DropShadowBitmapEffect)d;
			dropShadowBitmapEffect.PropertyChanged(DropShadowBitmapEffect.OpacityProperty);
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x00117AA0 File Offset: 0x00116EA0
		private static void SoftnessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			DropShadowBitmapEffect dropShadowBitmapEffect = (DropShadowBitmapEffect)d;
			dropShadowBitmapEffect.PropertyChanged(DropShadowBitmapEffect.SoftnessProperty);
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Obtém ou define a distância entre o objeto e a sombra que ele projeta.</summary>
		/// <returns>A distância entre o plano do objeto Projetando uma sombra e o plano de sombra é medido em unidades independentes de dispositivo (1/96 polegada por unidade). O intervalo de valores válido é de 0 a 300. O padrão é 5.</returns>
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06004753 RID: 18259 RVA: 0x00117AC0 File Offset: 0x00116EC0
		// (set) Token: 0x06004754 RID: 18260 RVA: 0x00117AE0 File Offset: 0x00116EE0
		public double ShadowDepth
		{
			get
			{
				return (double)base.GetValue(DropShadowBitmapEffect.ShadowDepthProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowBitmapEffect.ShadowDepthProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Obtém ou define a cor da sombra.</summary>
		/// <returns>A cor da sombra. O valor padrão é FF000000 (preto).</returns>
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06004755 RID: 18261 RVA: 0x00117B00 File Offset: 0x00116F00
		// (set) Token: 0x06004756 RID: 18262 RVA: 0x00117B20 File Offset: 0x00116F20
		public Color Color
		{
			get
			{
				return (Color)base.GetValue(DropShadowBitmapEffect.ColorProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowBitmapEffect.ColorProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Obtém ou define o ângulo no qual a sombra é projetada.</summary>
		/// <returns>O ângulo no qual a sombra é projetada. O intervalo de valores válido é de 0 a 360. O valor 0 coloca a direção imediatamente à direita do objeto. Valores subsequentes movem a direção em torno do objeto no sentido anti-horário. Por exemplo, um valor de 90 indica que a sombra é convertida diretamente para cima de objeto; um valor de 180 é convertido diretamente para a esquerda do objeto e assim por diante. O valor padrão é 315.</returns>
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06004757 RID: 18263 RVA: 0x00117B40 File Offset: 0x00116F40
		// (set) Token: 0x06004758 RID: 18264 RVA: 0x00117B60 File Offset: 0x00116F60
		public double Direction
		{
			get
			{
				return (double)base.GetValue(DropShadowBitmapEffect.DirectionProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowBitmapEffect.DirectionProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Obtém ou define a granulação ou "nível de ruído", da sombra.</summary>
		/// <returns>O nível de ruído da sombra. O intervalo de valores válido é de 0 a 1. Um valor de 0 indica nenhum ruído e 1 indica o ruído máxima. Um valor de 0,5 indica o ruído de 50 por cento, um valor de 0,75 indica o ruído de 75 por cento e assim por diante. O valor padrão é 0.</returns>
		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06004759 RID: 18265 RVA: 0x00117B80 File Offset: 0x00116F80
		// (set) Token: 0x0600475A RID: 18266 RVA: 0x00117BA0 File Offset: 0x00116FA0
		public double Noise
		{
			get
			{
				return (double)base.GetValue(DropShadowBitmapEffect.NoiseProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowBitmapEffect.NoiseProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Obtém ou define o grau de opacidade da sombra.</summary>
		/// <returns>O grau de opacidade. O intervalo de valores válido é de 0 a 1. Um valor de 0 indica que a sombra é completamente transparente, e um valor de 1 indica que a sombra é completamente opaca. Um valor de 0,5 indica que a sombra é 50 por cento opaco, que um valor de 0.725 indica que a sombra é 72.5% opaco e assim por diante. Valores menores que 0 são tratados como 0, enquanto valores maiores que 1 são tratados como 1. O padrão é 1.</returns>
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x0600475B RID: 18267 RVA: 0x00117BC0 File Offset: 0x00116FC0
		// (set) Token: 0x0600475C RID: 18268 RVA: 0x00117BE0 File Offset: 0x00116FE0
		public double Opacity
		{
			get
			{
				return (double)base.GetValue(DropShadowBitmapEffect.OpacityProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowBitmapEffect.OpacityProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Obtém ou define a suavidade da sombra.</summary>
		/// <returns>Suavidade da sombra. O intervalo de valores válido é de 0 a 1. Um valor igual a 0,0 não indica que nenhuma suavidade (uma sombra nitidamente definida) e 1,0 indica suavidade máximo (uma sombra muito difusa). Um valor de 0,5 indica suavidade 50 por cento, um valor de 0,75 indica suavidade 75 por cento e assim por diante. O padrão é 0,5.</returns>
		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x0600475D RID: 18269 RVA: 0x00117C00 File Offset: 0x00117000
		// (set) Token: 0x0600475E RID: 18270 RVA: 0x00117C20 File Offset: 0x00117020
		public double Softness
		{
			get
			{
				return (double)base.GetValue(DropShadowBitmapEffect.SoftnessProperty);
			}
			set
			{
				base.SetValueInternal(DropShadowBitmapEffect.SoftnessProperty, value);
			}
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x00117C40 File Offset: 0x00117040
		protected override Freezable CreateInstanceCore()
		{
			return new DropShadowBitmapEffect();
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x00117C54 File Offset: 0x00117054
		static DropShadowBitmapEffect()
		{
			Type typeFromHandle = typeof(DropShadowBitmapEffect);
			DropShadowBitmapEffect.ShadowDepthProperty = Animatable.RegisterProperty("ShadowDepth", typeof(double), typeFromHandle, 5.0, new PropertyChangedCallback(DropShadowBitmapEffect.ShadowDepthPropertyChanged), null, true, null);
			DropShadowBitmapEffect.ColorProperty = Animatable.RegisterProperty("Color", typeof(Color), typeFromHandle, Colors.Black, new PropertyChangedCallback(DropShadowBitmapEffect.ColorPropertyChanged), null, true, null);
			DropShadowBitmapEffect.DirectionProperty = Animatable.RegisterProperty("Direction", typeof(double), typeFromHandle, 315.0, new PropertyChangedCallback(DropShadowBitmapEffect.DirectionPropertyChanged), null, true, null);
			DropShadowBitmapEffect.NoiseProperty = Animatable.RegisterProperty("Noise", typeof(double), typeFromHandle, 0.0, new PropertyChangedCallback(DropShadowBitmapEffect.NoisePropertyChanged), null, true, null);
			DropShadowBitmapEffect.OpacityProperty = Animatable.RegisterProperty("Opacity", typeof(double), typeFromHandle, 1.0, new PropertyChangedCallback(DropShadowBitmapEffect.OpacityPropertyChanged), null, true, null);
			DropShadowBitmapEffect.SoftnessProperty = Animatable.RegisterProperty("Softness", typeof(double), typeFromHandle, 0.5, new PropertyChangedCallback(DropShadowBitmapEffect.SoftnessPropertyChanged), null, true, null);
		}

		// Token: 0x040019F9 RID: 6649
		private DropShadowEffect _imageEffectEmulation;

		// Token: 0x040019FA RID: 6650
		private const double _MAX_EMULATED_BLUR_RADIUS = 25.0;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowBitmapEffect.ShadowDepth" />.</summary>
		// Token: 0x040019FB RID: 6651
		public static readonly DependencyProperty ShadowDepthProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowBitmapEffect.Color" />.</summary>
		// Token: 0x040019FC RID: 6652
		public static readonly DependencyProperty ColorProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowBitmapEffect.Direction" />.</summary>
		// Token: 0x040019FD RID: 6653
		public static readonly DependencyProperty DirectionProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowBitmapEffect.Noise" />.</summary>
		// Token: 0x040019FE RID: 6654
		public static readonly DependencyProperty NoiseProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowBitmapEffect.Opacity" />.</summary>
		// Token: 0x040019FF RID: 6655
		public static readonly DependencyProperty OpacityProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.DropShadowEffect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.DropShadowBitmapEffect.Softness" />.</summary>
		// Token: 0x04001A00 RID: 6656
		public static readonly DependencyProperty SoftnessProperty;

		// Token: 0x04001A01 RID: 6657
		internal const double c_ShadowDepth = 5.0;

		// Token: 0x04001A02 RID: 6658
		internal static Color s_Color = Colors.Black;

		// Token: 0x04001A03 RID: 6659
		internal const double c_Direction = 315.0;

		// Token: 0x04001A04 RID: 6660
		internal const double c_Noise = 0.0;

		// Token: 0x04001A05 RID: 6661
		internal const double c_Opacity = 1.0;

		// Token: 0x04001A06 RID: 6662
		internal const double c_Softness = 0.5;
	}
}
