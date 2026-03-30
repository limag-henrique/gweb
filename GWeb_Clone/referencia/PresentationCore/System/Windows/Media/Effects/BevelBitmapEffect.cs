using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media.Effects
{
	/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um bisel que eleva a superfície da imagem de acordo com uma curva especificada.</summary>
	// Token: 0x0200060D RID: 1549
	public sealed class BevelBitmapEffect : BitmapEffect
	{
		// Token: 0x06004724 RID: 18212 RVA: 0x001171D8 File Offset: 0x001165D8
		[SecuritySafeCritical]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected override SafeHandle CreateUnmanagedEffect()
		{
			return null;
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x001171E8 File Offset: 0x001165E8
		[SecurityCritical]
		[SecurityTreatAsSafe]
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		protected override void UpdateUnmanagedPropertyState(SafeHandle unmanagedEffect)
		{
			SecurityHelper.DemandUIWindowPermission();
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um clone modificável desse <see cref="T:System.Windows.Media.Effects.BevelBitmapEffect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06004726 RID: 18214 RVA: 0x001171FC File Offset: 0x001165FC
		public new BevelBitmapEffect Clone()
		{
			return (BevelBitmapEffect)base.Clone();
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.BevelBitmapEffect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, associações de dados e animações não são copiados, mas seus valores reais são</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x06004727 RID: 18215 RVA: 0x00117214 File Offset: 0x00116614
		public new BevelBitmapEffect CloneCurrentValue()
		{
			return (BevelBitmapEffect)base.CloneCurrentValue();
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x0011722C File Offset: 0x0011662C
		private static void BevelWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BevelBitmapEffect bevelBitmapEffect = (BevelBitmapEffect)d;
			bevelBitmapEffect.PropertyChanged(BevelBitmapEffect.BevelWidthProperty);
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x0011724C File Offset: 0x0011664C
		private static void ReliefPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BevelBitmapEffect bevelBitmapEffect = (BevelBitmapEffect)d;
			bevelBitmapEffect.PropertyChanged(BevelBitmapEffect.ReliefProperty);
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x0011726C File Offset: 0x0011666C
		private static void LightAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BevelBitmapEffect bevelBitmapEffect = (BevelBitmapEffect)d;
			bevelBitmapEffect.PropertyChanged(BevelBitmapEffect.LightAngleProperty);
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x0011728C File Offset: 0x0011668C
		private static void SmoothnessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			BevelBitmapEffect bevelBitmapEffect = (BevelBitmapEffect)d;
			bevelBitmapEffect.PropertyChanged(BevelBitmapEffect.SmoothnessProperty);
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define a largura do bisel.</summary>
		/// <returns>A largura do bisel. O valor padrão é 5.</returns>
		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x001172AC File Offset: 0x001166AC
		// (set) Token: 0x0600472D RID: 18221 RVA: 0x001172CC File Offset: 0x001166CC
		public double BevelWidth
		{
			get
			{
				return (double)base.GetValue(BevelBitmapEffect.BevelWidthProperty);
			}
			set
			{
				base.SetValueInternal(BevelBitmapEffect.BevelWidthProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define a altura do relevo do bisel.</summary>
		/// <returns>A altura do relevo do bisel. O intervalo válido é entre 0 e 1 com 1 tendo mais alívio (sombras mais escuros). O valor padrão é 0.3.</returns>
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x0600472E RID: 18222 RVA: 0x001172EC File Offset: 0x001166EC
		// (set) Token: 0x0600472F RID: 18223 RVA: 0x0011730C File Offset: 0x0011670C
		public double Relief
		{
			get
			{
				return (double)base.GetValue(BevelBitmapEffect.ReliefProperty);
			}
			set
			{
				base.SetValueInternal(BevelBitmapEffect.ReliefProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define a direção da "luz virtual" proveniente que cria as sombras do bisel.</summary>
		/// <returns>A direção da fonte de luz virtual. O intervalo válido é de 0 a 360 (graus) com 0, especificando o lado direito do objeto e valores sucessivos movendo no sentido anti-horário em torno do objeto. As sombras do bisel estão no lado oposto do qual a luz é convertida. O valor padrão é 135.</returns>
		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06004730 RID: 18224 RVA: 0x0011732C File Offset: 0x0011672C
		// (set) Token: 0x06004731 RID: 18225 RVA: 0x0011734C File Offset: 0x0011674C
		public double LightAngle
		{
			get
			{
				return (double)base.GetValue(BevelBitmapEffect.LightAngleProperty);
			}
			set
			{
				base.SetValueInternal(BevelBitmapEffect.LightAngleProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define de quanto será a suavidade das sombras do bisel.</summary>
		/// <returns>Valor que indica como smooth são das sombras do bisel. O intervalo válido é entre 0 e 1 com 1 sendo a mais uniforme. O valor padrão é 0,2.</returns>
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06004732 RID: 18226 RVA: 0x0011736C File Offset: 0x0011676C
		// (set) Token: 0x06004733 RID: 18227 RVA: 0x0011738C File Offset: 0x0011678C
		public double Smoothness
		{
			get
			{
				return (double)base.GetValue(BevelBitmapEffect.SmoothnessProperty);
			}
			set
			{
				base.SetValueInternal(BevelBitmapEffect.SmoothnessProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define a curva do bisel.</summary>
		/// <returns>A curva do bisel. O valor padrão é <see cref="F:System.Windows.Media.Effects.EdgeProfile.Linear" />.</returns>
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06004734 RID: 18228 RVA: 0x001173AC File Offset: 0x001167AC
		// (set) Token: 0x06004735 RID: 18229 RVA: 0x001173CC File Offset: 0x001167CC
		public EdgeProfile EdgeProfile
		{
			get
			{
				return (EdgeProfile)base.GetValue(BevelBitmapEffect.EdgeProfileProperty);
			}
			set
			{
				base.SetValueInternal(BevelBitmapEffect.EdgeProfileProperty, value);
			}
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x001173EC File Offset: 0x001167EC
		protected override Freezable CreateInstanceCore()
		{
			return new BevelBitmapEffect();
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x00117400 File Offset: 0x00116800
		static BevelBitmapEffect()
		{
			Type typeFromHandle = typeof(BevelBitmapEffect);
			BevelBitmapEffect.BevelWidthProperty = Animatable.RegisterProperty("BevelWidth", typeof(double), typeFromHandle, 5.0, new PropertyChangedCallback(BevelBitmapEffect.BevelWidthPropertyChanged), null, true, null);
			BevelBitmapEffect.ReliefProperty = Animatable.RegisterProperty("Relief", typeof(double), typeFromHandle, 0.3, new PropertyChangedCallback(BevelBitmapEffect.ReliefPropertyChanged), null, true, null);
			BevelBitmapEffect.LightAngleProperty = Animatable.RegisterProperty("LightAngle", typeof(double), typeFromHandle, 135.0, new PropertyChangedCallback(BevelBitmapEffect.LightAnglePropertyChanged), null, true, null);
			BevelBitmapEffect.SmoothnessProperty = Animatable.RegisterProperty("Smoothness", typeof(double), typeFromHandle, 0.2, new PropertyChangedCallback(BevelBitmapEffect.SmoothnessPropertyChanged), null, true, null);
			BevelBitmapEffect.EdgeProfileProperty = Animatable.RegisterProperty("EdgeProfile", typeof(EdgeProfile), typeFromHandle, EdgeProfile.Linear, null, new ValidateValueCallback(ValidateEnums.IsEdgeProfileValid), false, null);
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BevelBitmapEffect.BevelWidth" />.</summary>
		// Token: 0x040019EA RID: 6634
		public static readonly DependencyProperty BevelWidthProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BevelBitmapEffect.Relief" />.</summary>
		// Token: 0x040019EB RID: 6635
		public static readonly DependencyProperty ReliefProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BevelBitmapEffect.LightAngle" />.</summary>
		// Token: 0x040019EC RID: 6636
		public static readonly DependencyProperty LightAngleProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BevelBitmapEffect.Smoothness" />.</summary>
		// Token: 0x040019ED RID: 6637
		public static readonly DependencyProperty SmoothnessProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.BevelBitmapEffect.EdgeProfile" />.</summary>
		// Token: 0x040019EE RID: 6638
		public static readonly DependencyProperty EdgeProfileProperty;

		// Token: 0x040019EF RID: 6639
		internal const double c_BevelWidth = 5.0;

		// Token: 0x040019F0 RID: 6640
		internal const double c_Relief = 0.3;

		// Token: 0x040019F1 RID: 6641
		internal const double c_LightAngle = 135.0;

		// Token: 0x040019F2 RID: 6642
		internal const double c_Smoothness = 0.2;

		// Token: 0x040019F3 RID: 6643
		internal const EdgeProfile c_EdgeProfile = EdgeProfile.Linear;
	}
}
