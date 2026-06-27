using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, anima um valor de <see cref="T:System.Windows.Media.Media3D.Quaternion" />.</summary>
	// Token: 0x02000532 RID: 1330
	public abstract class QuaternionAnimationBase : AnimationTimeline
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.QuaternionAnimationBase" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003C8A RID: 15498 RVA: 0x000EE0FC File Offset: 0x000ED4FC
		public new QuaternionAnimationBase Clone()
		{
			return (QuaternionAnimationBase)base.Clone();
		}

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tiver seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor atual da animação.</returns>
		// Token: 0x06003C8B RID: 15499 RVA: 0x000EE114 File Offset: 0x000ED514
		public sealed override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
		{
			if (defaultOriginValue == null)
			{
				throw new ArgumentNullException("defaultOriginValue");
			}
			if (defaultDestinationValue == null)
			{
				throw new ArgumentNullException("defaultDestinationValue");
			}
			return this.GetCurrentValue((Quaternion)defaultOriginValue, (Quaternion)defaultDestinationValue, animationClock);
		}

		/// <summary>Obtém o tipo de valor que essa animação gera.</summary>
		/// <returns>O tipo do valor produzido por essa animação.</returns>
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06003C8C RID: 15500 RVA: 0x000EE158 File Offset: 0x000ED558
		public sealed override Type TargetPropertyType
		{
			get
			{
				base.ReadPreamble();
				return typeof(Quaternion);
			}
		}

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tiver seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor atual da animação.</returns>
		// Token: 0x06003C8D RID: 15501 RVA: 0x000EE178 File Offset: 0x000ED578
		public Quaternion GetCurrentValue(Quaternion defaultOriginValue, Quaternion defaultDestinationValue, AnimationClock animationClock)
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

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tiver seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor atual da animação.</returns>
		// Token: 0x06003C8E RID: 15502
		protected abstract Quaternion GetCurrentValueCore(Quaternion defaultOriginValue, Quaternion defaultDestinationValue, AnimationClock animationClock);
	}
}
