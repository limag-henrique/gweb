using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Media.Animation;
using MS.Internal;

namespace System.Windows.Media.Effects
{
	/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um mapeamento de rugosidade do objeto visual para dar a impressão de profundidade e textura de uma fonte de luz artificial.</summary>
	// Token: 0x02000611 RID: 1553
	public sealed class EmbossBitmapEffect : BitmapEffect
	{
		// Token: 0x0600477A RID: 18298 RVA: 0x00118158 File Offset: 0x00117558
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		[SecuritySafeCritical]
		protected override SafeHandle CreateUnmanagedEffect()
		{
			return null;
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x00118168 File Offset: 0x00117568
		[Obsolete("BitmapEffects are deprecated and no longer function.  Consider using Effects where appropriate instead.")]
		[SecurityCritical]
		[SecurityTreatAsSafe]
		protected override void UpdateUnmanagedPropertyState(SafeHandle unmanagedEffect)
		{
			SecurityHelper.DemandUIWindowPermission();
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um clone modificável desse <see cref="T:System.Windows.Media.Effects.EmbossBitmapEffect" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x0600477C RID: 18300 RVA: 0x0011817C File Offset: 0x0011757C
		public new EmbossBitmapEffect Clone()
		{
			return (EmbossBitmapEffect)base.Clone();
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Cria um clone modificável desse objeto <see cref="T:System.Windows.Media.Effects.EmbossBitmapEffect" />, fazendo cópias em profundidade dos valores do objeto atual. Referências a recursos, vinculações de dados e animações não são copiadas, mas seus valores atuais são.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true" />.</returns>
		// Token: 0x0600477D RID: 18301 RVA: 0x00118194 File Offset: 0x00117594
		public new EmbossBitmapEffect CloneCurrentValue()
		{
			return (EmbossBitmapEffect)base.CloneCurrentValue();
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x001181AC File Offset: 0x001175AC
		private static void LightAnglePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			EmbossBitmapEffect embossBitmapEffect = (EmbossBitmapEffect)d;
			embossBitmapEffect.PropertyChanged(EmbossBitmapEffect.LightAngleProperty);
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x001181CC File Offset: 0x001175CC
		private static void ReliefPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			EmbossBitmapEffect embossBitmapEffect = (EmbossBitmapEffect)d;
			embossBitmapEffect.PropertyChanged(EmbossBitmapEffect.ReliefProperty);
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define a direção da que luz artificial que paira sobre o objeto em alto-relevo.</summary>
		/// <returns>A direção da luz artificial é convertida paira sobre o objeto. O intervalo válido é de 0-360 (graus) com 0 especificando o lado direito do objeto e valores sucessivos movendo no sentido anti-horário em torno do objeto. O valor padrão é 45.</returns>
		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06004780 RID: 18304 RVA: 0x001181EC File Offset: 0x001175EC
		// (set) Token: 0x06004781 RID: 18305 RVA: 0x0011820C File Offset: 0x0011760C
		public double LightAngle
		{
			get
			{
				return (double)base.GetValue(EmbossBitmapEffect.LightAngleProperty);
			}
			set
			{
				base.SetValueInternal(EmbossBitmapEffect.LightAngleProperty, value);
			}
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Obtém ou define a quantidade de relevo do alto-relevo.</summary>
		/// <returns>A quantidade de relevo do alto-relevo. O intervalo de valores válido é 0-1 com 0 tendo menos relevo e 1 tendo mais. O valor padrão é 0,44.</returns>
		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06004782 RID: 18306 RVA: 0x0011822C File Offset: 0x0011762C
		// (set) Token: 0x06004783 RID: 18307 RVA: 0x0011824C File Offset: 0x0011764C
		public double Relief
		{
			get
			{
				return (double)base.GetValue(EmbossBitmapEffect.ReliefProperty);
			}
			set
			{
				base.SetValueInternal(EmbossBitmapEffect.ReliefProperty, value);
			}
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x0011826C File Offset: 0x0011766C
		protected override Freezable CreateInstanceCore()
		{
			return new EmbossBitmapEffect();
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x00118280 File Offset: 0x00117680
		static EmbossBitmapEffect()
		{
			Type typeFromHandle = typeof(EmbossBitmapEffect);
			EmbossBitmapEffect.LightAngleProperty = Animatable.RegisterProperty("LightAngle", typeof(double), typeFromHandle, 45.0, new PropertyChangedCallback(EmbossBitmapEffect.LightAnglePropertyChanged), null, true, null);
			EmbossBitmapEffect.ReliefProperty = Animatable.RegisterProperty("Relief", typeof(double), typeFromHandle, 0.44, new PropertyChangedCallback(EmbossBitmapEffect.ReliefPropertyChanged), null, true, null);
		}

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.EmbossBitmapEffect.LightAngle" />.</summary>
		// Token: 0x04001A0B RID: 6667
		public static readonly DependencyProperty LightAngleProperty;

		/// <summary>Observação: Agora esta API está obsoleta. A alternativa não obsoleta é <see cref="T:System.Windows.Media.Effects.Effect" />. Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Effects.EmbossBitmapEffect.Relief" />.</summary>
		// Token: 0x04001A0C RID: 6668
		public static readonly DependencyProperty ReliefProperty;

		// Token: 0x04001A0D RID: 6669
		internal const double c_LightAngle = 45.0;

		// Token: 0x04001A0E RID: 6670
		internal const double c_Relief = 0.44;
	}
}
