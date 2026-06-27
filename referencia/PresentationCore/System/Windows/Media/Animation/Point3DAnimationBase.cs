using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Classe abstrata que, quando implementada, anima um valor de <see cref="T:System.Windows.Media.Media3D.Point3D" />.</summary>
	// Token: 0x02000527 RID: 1319
	public abstract class Point3DAnimationBase : AnimationTimeline
	{
		/// <summary>Cria um clone modificável desse <see cref="T:System.Windows.Media.Animation.Point3DAnimationBase" />, fazendo cópias em profundidade dos valores do objeto. Ao copiar as propriedades de dependência, esse método copia associações de dados e referências de recurso (mas eles não podem mais resolver), mas não animações ou seus valores atuais.</summary>
		/// <returns>Um clone modificável do objeto atual. A propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> do objeto clonado será <see langword="false" />, mesmo se a propriedade <see cref="P:System.Windows.Freezable.IsFrozen" /> da origem tiver sido <see langword="true." /></returns>
		// Token: 0x06003BC1 RID: 15297 RVA: 0x000EABE0 File Offset: 0x000E9FE0
		public new Point3DAnimationBase Clone()
		{
			return (Point3DAnimationBase)base.Clone();
		}

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tiver seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003BC2 RID: 15298 RVA: 0x000EABF8 File Offset: 0x000E9FF8
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
			return this.GetCurrentValue((Point3D)defaultOriginValue, (Point3D)defaultDestinationValue, animationClock);
		}

		/// <summary>Obtém o tipo de valor que essa animação gera.</summary>
		/// <returns>O tipo do valor produzido por essa animação.</returns>
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x000EAC3C File Offset: 0x000EA03C
		public sealed override Type TargetPropertyType
		{
			get
			{
				base.ReadPreamble();
				return typeof(Point3D);
			}
		}

		/// <summary>Obtém o valor atual da animação.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tiver seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor atual da animação.</returns>
		// Token: 0x06003BC4 RID: 15300 RVA: 0x000EAC5C File Offset: 0x000EA05C
		public Point3D GetCurrentValue(Point3D defaultOriginValue, Point3D defaultDestinationValue, AnimationClock animationClock)
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

		/// <summary>Calcula um valor que representa o valor atual da propriedade que está sendo animada, conforme determinado pelo <see cref="T:System.Windows.Media.Animation.Point3DAnimation" />.</summary>
		/// <param name="defaultOriginValue">O valor de origem fornecido para a animação se a animação não tiver seu próprio valor inicial.</param>
		/// <param name="defaultDestinationValue">O valor de destino fornecido para a animação se a animação não tiver seu próprio valor de destino.</param>
		/// <param name="animationClock">O <see cref="T:System.Windows.Media.Animation.AnimationClock" /> que pode gerar o valor <see cref="P:System.Windows.Media.Animation.Clock.CurrentTime" /> ou <see cref="P:System.Windows.Media.Animation.Clock.CurrentProgress" /> a ser usado pela animação para gerar o valor de saída.</param>
		/// <returns>O valor calculado da propriedade, conforme determinado pela animação atual.</returns>
		// Token: 0x06003BC5 RID: 15301
		protected abstract Point3D GetCurrentValueCore(Point3D defaultOriginValue, Point3D defaultDestinationValue, AnimationClock animationClock);
	}
}
