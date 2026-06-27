using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, anima um valor de <see cref="T:System.Decimal" />.</summary>
	// Token: 0x020004BF RID: 1215
	public abstract class DecimalAnimationBase : AnimationTimeline
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.DecimalAnimationBase" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003706 RID: 14086 RVA: 0x000DBD34 File Offset: 0x000DB134
		public new DecimalAnimationBase Clone()
		{
			return (DecimalAnimationBase)base.Clone();
		}

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tiver seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor que essa animação acredita que deve ser o valor atual da propriedade.</returns>
		// Token: 0x06003707 RID: 14087 RVA: 0x000DBD4C File Offset: 0x000DB14C
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
			return this.GetCurrentValue((decimal)defaultOriginValue, (decimal)defaultDestinationValue, animationClock);
		}

		/// <summary>Obtém o tipo de valor que essa animação gera.</summary>
		/// <returns>O tipo do valor produzido por essa animação.</returns>
		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x000DBD90 File Offset: 0x000DB190
		public sealed override Type TargetPropertyType
		{
			get
			{
				base.ReadPreamble();
				return typeof(decimal);
			}
		}

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tiver seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor que essa animação acredita que deve ser o valor atual da propriedade.</returns>
		// Token: 0x06003709 RID: 14089 RVA: 0x000DBDB0 File Offset: 0x000DB1B0
		public decimal GetCurrentValue(decimal defaultOriginValue, decimal defaultDestinationValue, AnimationClock animationClock)
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
		// Token: 0x0600370A RID: 14090
		protected abstract decimal GetCurrentValueCore(decimal defaultOriginValue, decimal defaultDestinationValue, AnimationClock animationClock);
	}
}
