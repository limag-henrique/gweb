using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, anima um valor de <see cref="T:System.Object" />.</summary>
	// Token: 0x02000522 RID: 1314
	public abstract class ObjectAnimationBase : AnimationTimeline
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.ObjectAnimationBase" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003B5B RID: 15195 RVA: 0x000E91D0 File Offset: 0x000E85D0
		public new ObjectAnimationBase Clone()
		{
			return (ObjectAnimationBase)base.Clone();
		}

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tiver seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor que essa animação acredita que deve ser o valor atual da propriedade.</returns>
		// Token: 0x06003B5C RID: 15196 RVA: 0x000E91E8 File Offset: 0x000E85E8
		public sealed override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
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

		/// <summary>Obtém o tipo de valor que essa animação gera.</summary>
		/// <returns>O tipo do valor produzido por essa animação.</returns>
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x000E9220 File Offset: 0x000E8620
		public sealed override Type TargetPropertyType
		{
			get
			{
				base.ReadPreamble();
				return typeof(object);
			}
		}

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pela animação do host.</summary>
		/// <param name="defaultOriginValue">O valor de origem sugerido, usado se a animação não tiver seu próprio valor inicial definido explicitamente.</param>
		/// <param name="defaultDestinationValue">O valor de destino sugerido, usado se a animação não tiver seu próprio valor final definido explicitamente.</param>
		/// <param name="animationClock">Um <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que gera o <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou o <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> usado pela animação de host.</param>
		/// <returns>O valor que essa animação acredita que deve ser o valor atual da propriedade.</returns>
		// Token: 0x06003B5E RID: 15198
		protected abstract object GetCurrentValueCore(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock);
	}
}
