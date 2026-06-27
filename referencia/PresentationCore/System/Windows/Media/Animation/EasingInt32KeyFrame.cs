using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.Int32AnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004E2 RID: 1250
	public class EasingInt32KeyFrame : Int32KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt32KeyFrame" />.</summary>
		// Token: 0x06003847 RID: 14407 RVA: 0x000DFA14 File Offset: 0x000DEE14
		public EasingInt32KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt32KeyFrame" /> com o valor <see cref="T:System.Int32" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Int32" /> inicial.</param>
		// Token: 0x06003848 RID: 14408 RVA: 0x000DFA28 File Offset: 0x000DEE28
		public EasingInt32KeyFrame(int value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt32KeyFrame" /> com o valor <see cref="T:System.Int32" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Int32" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x06003849 RID: 14409 RVA: 0x000DFA44 File Offset: 0x000DEE44
		public EasingInt32KeyFrame(int value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingInt32KeyFrame" /> com o valor <see cref="T:System.Int32" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Int32" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x0600384A RID: 14410 RVA: 0x000DFA68 File Offset: 0x000DEE68
		public EasingInt32KeyFrame(int value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600384B RID: 14411 RVA: 0x000DFA90 File Offset: 0x000DEE90
		protected override Freezable CreateInstanceCore()
		{
			return new EasingInt32KeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600384C RID: 14412 RVA: 0x000DFAA4 File Offset: 0x000DEEA4
		protected override int InterpolateValueCore(int baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateInt32(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000DFAF4 File Offset: 0x000DEEF4
		// (set) Token: 0x0600384E RID: 14414 RVA: 0x000DFB14 File Offset: 0x000DEF14
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingInt32KeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingInt32KeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingInt32KeyFrame.EasingFunction" />.</summary>
		// Token: 0x0400168D RID: 5773
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingInt32KeyFrame));
	}
}
