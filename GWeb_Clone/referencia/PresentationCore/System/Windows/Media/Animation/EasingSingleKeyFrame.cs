using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.SingleAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004E9 RID: 1257
	public class EasingSingleKeyFrame : SingleKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingSingleKeyFrame" />.</summary>
		// Token: 0x06003888 RID: 14472 RVA: 0x000E0398 File Offset: 0x000DF798
		public EasingSingleKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingSingleKeyFrame" /> com o valor <see cref="T:System.Single" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Single" /> inicial.</param>
		// Token: 0x06003889 RID: 14473 RVA: 0x000E03AC File Offset: 0x000DF7AC
		public EasingSingleKeyFrame(float value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingSingleKeyFrame" /> com o valor <see cref="T:System.Single" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Single" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x0600388A RID: 14474 RVA: 0x000E03C8 File Offset: 0x000DF7C8
		public EasingSingleKeyFrame(float value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingSingleKeyFrame" /> com o valor <see cref="T:System.Single" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Single" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x0600388B RID: 14475 RVA: 0x000E03EC File Offset: 0x000DF7EC
		public EasingSingleKeyFrame(float value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600388C RID: 14476 RVA: 0x000E0414 File Offset: 0x000DF814
		protected override Freezable CreateInstanceCore()
		{
			return new EasingSingleKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600388D RID: 14477 RVA: 0x000E0428 File Offset: 0x000DF828
		protected override float InterpolateValueCore(float baseValue, double keyFrameProgress)
		{
			IEasingFunction easingFunction = this.EasingFunction;
			if (easingFunction != null)
			{
				keyFrameProgress = easingFunction.Ease(keyFrameProgress);
			}
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateSingle(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600388E RID: 14478 RVA: 0x000E0478 File Offset: 0x000DF878
		// (set) Token: 0x0600388F RID: 14479 RVA: 0x000E0498 File Offset: 0x000DF898
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingSingleKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingSingleKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingSingleKeyFrame.EasingFunction" />.</summary>
		// Token: 0x04001695 RID: 5781
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingSingleKeyFrame));
	}
}
