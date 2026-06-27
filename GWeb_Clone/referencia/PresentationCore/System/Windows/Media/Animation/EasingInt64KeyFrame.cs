using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.Int64AnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004E3 RID: 1251
	public class EasingInt64KeyFrame : Int64KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt64KeyFrame" />.</summary>
		// Token: 0x06003850 RID: 14416 RVA: 0x000DFB60 File Offset: 0x000DEF60
		public EasingInt64KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt64KeyFrame" /> com o valor <see cref="T:System.Int64" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Int64" /> inicial.</param>
		// Token: 0x06003851 RID: 14417 RVA: 0x000DFB74 File Offset: 0x000DEF74
		public EasingInt64KeyFrame(long value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt64KeyFrame" /> com o valor <see cref="T:System.Int64" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Int64" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x06003852 RID: 14418 RVA: 0x000DFB90 File Offset: 0x000DEF90
		public EasingInt64KeyFrame(long value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt64KeyFrame" /> com o valor <see cref="T:System.Int64" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Int64" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x06003853 RID: 14419 RVA: 0x000DFBB4 File Offset: 0x000DEFB4
		public EasingInt64KeyFrame(long value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003854 RID: 14420 RVA: 0x000DFBDC File Offset: 0x000DEFDC
		protected override Freezable CreateInstanceCore()
		{
			return new EasingInt64KeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003855 RID: 14421 RVA: 0x000DFBF0 File Offset: 0x000DEFF0
		protected override long InterpolateValueCore(long baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateInt64(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x000DFC40 File Offset: 0x000DF040
		// (set) Token: 0x06003857 RID: 14423 RVA: 0x000DFC60 File Offset: 0x000DF060
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingInt64KeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingInt64KeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingInt64KeyFrame.EasingFunction" />.</summary>
		// Token: 0x0400168E RID: 5774
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingInt64KeyFrame));
	}
}
