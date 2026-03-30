using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.VectorAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004EB RID: 1259
	public class EasingVectorKeyFrame : VectorKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingVectorKeyFrame" />.</summary>
		// Token: 0x0600389A RID: 14490 RVA: 0x000E0630 File Offset: 0x000DFA30
		public EasingVectorKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingVectorKeyFrame" /> com o valor <see cref="T:System.Windows.Vector" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Vector" /> inicial.</param>
		// Token: 0x0600389B RID: 14491 RVA: 0x000E0644 File Offset: 0x000DFA44
		public EasingVectorKeyFrame(Vector value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingVectorKeyFrame" /> com o valor <see cref="T:System.Windows.Vector" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Vector" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x0600389C RID: 14492 RVA: 0x000E0660 File Offset: 0x000DFA60
		public EasingVectorKeyFrame(Vector value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingVectorKeyFrame" /> com o valor <see cref="T:System.Windows.Vector" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Vector" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x0600389D RID: 14493 RVA: 0x000E0684 File Offset: 0x000DFA84
		public EasingVectorKeyFrame(Vector value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600389E RID: 14494 RVA: 0x000E06AC File Offset: 0x000DFAAC
		protected override Freezable CreateInstanceCore()
		{
			return new EasingVectorKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600389F RID: 14495 RVA: 0x000E06C0 File Offset: 0x000DFAC0
		protected override Vector InterpolateValueCore(Vector baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateVector(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060038A0 RID: 14496 RVA: 0x000E0710 File Offset: 0x000DFB10
		// (set) Token: 0x060038A1 RID: 14497 RVA: 0x000E0730 File Offset: 0x000DFB30
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingVectorKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingVectorKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingVectorKeyFrame.EasingFunction" />.</summary>
		// Token: 0x04001697 RID: 5783
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingVectorKeyFrame));
	}
}
