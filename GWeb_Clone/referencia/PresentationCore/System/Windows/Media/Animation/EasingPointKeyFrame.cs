using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.PointAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004E4 RID: 1252
	public class EasingPointKeyFrame : PointKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingPointKeyFrame" />.</summary>
		// Token: 0x06003859 RID: 14425 RVA: 0x000DFCAC File Offset: 0x000DF0AC
		public EasingPointKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingPointKeyFrame" /> com o valor <see cref="T:System.Windows.Point" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Point" /> inicial.</param>
		// Token: 0x0600385A RID: 14426 RVA: 0x000DFCC0 File Offset: 0x000DF0C0
		public EasingPointKeyFrame(Point value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingPointKeyFrame" /> com o valor <see cref="T:System.Windows.Point" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Point" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x0600385B RID: 14427 RVA: 0x000DFCDC File Offset: 0x000DF0DC
		public EasingPointKeyFrame(Point value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingPointKeyFrame" /> com o valor <see cref="T:System.Windows.Point" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Point" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x0600385C RID: 14428 RVA: 0x000DFD00 File Offset: 0x000DF100
		public EasingPointKeyFrame(Point value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600385D RID: 14429 RVA: 0x000DFD28 File Offset: 0x000DF128
		protected override Freezable CreateInstanceCore()
		{
			return new EasingPointKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600385E RID: 14430 RVA: 0x000DFD3C File Offset: 0x000DF13C
		protected override Point InterpolateValueCore(Point baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolatePoint(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000DFD8C File Offset: 0x000DF18C
		// (set) Token: 0x06003860 RID: 14432 RVA: 0x000DFDAC File Offset: 0x000DF1AC
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingPointKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingPointKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingPointKeyFrame.EasingFunction" />.</summary>
		// Token: 0x0400168F RID: 5775
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingPointKeyFrame));
	}
}
