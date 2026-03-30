using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.Int16AnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004E1 RID: 1249
	public class EasingInt16KeyFrame : Int16KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt16KeyFrame" />.</summary>
		// Token: 0x0600383E RID: 14398 RVA: 0x000DF8C8 File Offset: 0x000DECC8
		public EasingInt16KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt16KeyFrame" /> com o valor <see cref="T:System.Int16" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Int16" /> inicial.</param>
		// Token: 0x0600383F RID: 14399 RVA: 0x000DF8DC File Offset: 0x000DECDC
		public EasingInt16KeyFrame(short value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt16KeyFrame" /> com o valor <see cref="T:System.Int16" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Int16" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x06003840 RID: 14400 RVA: 0x000DF8F8 File Offset: 0x000DECF8
		public EasingInt16KeyFrame(short value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt16KeyFrame" /> com o valor <see cref="T:System.Int16" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Int16" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x06003841 RID: 14401 RVA: 0x000DF91C File Offset: 0x000DED1C
		public EasingInt16KeyFrame(short value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003842 RID: 14402 RVA: 0x000DF944 File Offset: 0x000DED44
		protected override Freezable CreateInstanceCore()
		{
			return new EasingInt16KeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003843 RID: 14403 RVA: 0x000DF958 File Offset: 0x000DED58
		protected override short InterpolateValueCore(short baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateInt16(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06003844 RID: 14404 RVA: 0x000DF9A8 File Offset: 0x000DEDA8
		// (set) Token: 0x06003845 RID: 14405 RVA: 0x000DF9C8 File Offset: 0x000DEDC8
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingInt16KeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingInt16KeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingInt16KeyFrame.EasingFunction" />.</summary>
		// Token: 0x0400168C RID: 5772
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingInt16KeyFrame));
	}
}
