using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe abstrata que permite associar funções de easing a uma animação de quadro-chave <see cref="T:System.Windows.Media.Animation.DecimalAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004DF RID: 1247
	public class EasingDecimalKeyFrame : DecimalKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingDecimalKeyFrame" />.</summary>
		// Token: 0x0600382C RID: 14380 RVA: 0x000DF630 File Offset: 0x000DEA30
		public EasingDecimalKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingDecimalKeyFrame" /> com o valor especificado.</summary>
		/// <param name="value">O valor inicial.</param>
		// Token: 0x0600382D RID: 14381 RVA: 0x000DF644 File Offset: 0x000DEA44
		public EasingDecimalKeyFrame(decimal value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingDecimalKeyFrame" /> com o tempo-chave e o valor especificados.</summary>
		/// <param name="value">O valor inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x0600382E RID: 14382 RVA: 0x000DF660 File Offset: 0x000DEA60
		public EasingDecimalKeyFrame(decimal value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingDecimalKeyFrame" /> com o valor, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x0600382F RID: 14383 RVA: 0x000DF684 File Offset: 0x000DEA84
		public EasingDecimalKeyFrame(decimal value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003830 RID: 14384 RVA: 0x000DF6AC File Offset: 0x000DEAAC
		protected override Freezable CreateInstanceCore()
		{
			return new EasingDecimalKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003831 RID: 14385 RVA: 0x000DF6C0 File Offset: 0x000DEAC0
		protected override decimal InterpolateValueCore(decimal baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateDecimal(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06003832 RID: 14386 RVA: 0x000DF710 File Offset: 0x000DEB10
		// (set) Token: 0x06003833 RID: 14387 RVA: 0x000DF730 File Offset: 0x000DEB30
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingDecimalKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingDecimalKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingDecimalKeyFrame.EasingFunction" />.</summary>
		// Token: 0x0400168A RID: 5770
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingDecimalKeyFrame));
	}
}
