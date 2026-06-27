using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, anima um valor de <see cref="T:System.Windows.Media.Media3D.Rotation3D" />.</summary>
	// Token: 0x0200053D RID: 1341
	public abstract class Rotation3DAnimationBase : AnimationTimeline
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Rotation3DAnimationBase" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003D51 RID: 15697 RVA: 0x000F14C8 File Offset: 0x000F08C8
		public new Rotation3DAnimationBase Clone()
		{
			return (Rotation3DAnimationBase)base.Clone();
		}

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial. Se esta animação for a primeira em uma cadeia de composição, ela será o valor base da propriedade que estiver sendo animada; caso contrário, será o valor retornado pela animação anterior na cadeia.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tem seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor atual da animação.</returns>
		// Token: 0x06003D52 RID: 15698 RVA: 0x000F14E0 File Offset: 0x000F08E0
		public sealed override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
		{
			return this.GetCurrentValue((Rotation3D)defaultOriginValue, (Rotation3D)defaultDestinationValue, animationClock);
		}

		/// <summary>Obtém o tipo de valor que essa animação gera.</summary>
		/// <returns>O tipo do valor produzido por essa animação.</returns>
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06003D53 RID: 15699 RVA: 0x000F1500 File Offset: 0x000F0900
		public sealed override Type TargetPropertyType
		{
			get
			{
				base.ReadPreamble();
				return typeof(Rotation3D);
			}
		}

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tem seu próprio valor inicial. Se esta animação for a primeira em uma cadeia de composição, ela será o valor base da propriedade que estiver sendo animada; caso contrário, será o valor retornado pela animação anterior na cadeia.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tem seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor atual da animação.</returns>
		// Token: 0x06003D54 RID: 15700 RVA: 0x000F1520 File Offset: 0x000F0920
		public Rotation3D GetCurrentValue(Rotation3D defaultOriginValue, Rotation3D defaultDestinationValue, AnimationClock animationClock)
		{
			base.ReadPreamble();
			if (animationClock == null)
			{
				throw new ArgumentNullException("animationClock");
			}
			if (animationClock.CurrentState == ClockState.Stopped)
			{
				return defaultDestinationValue;
			}
			return this.GetCurrentValueCore(defaultOriginValue, defaultDestinationValue, animationClock);
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pela animação do host.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação de host.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003D55 RID: 15701
		protected abstract Rotation3D GetCurrentValueCore(Rotation3D defaultOriginValue, Rotation3D defaultDestinationValue, AnimationClock animationClock);
	}
}
