using System;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.Point3DAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004E5 RID: 1253
	public class EasingPoint3DKeyFrame : Point3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingPoint3DKeyFrame" />.</summary>
		// Token: 0x06003862 RID: 14434 RVA: 0x000DFDF8 File Offset: 0x000DF1F8
		public EasingPoint3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingPoint3DKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Point3D" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Point3D" /> inicial.</param>
		// Token: 0x06003863 RID: 14435 RVA: 0x000DFE0C File Offset: 0x000DF20C
		public EasingPoint3DKeyFrame(Point3D value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingPoint3DKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Point3D" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Point3D" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x06003864 RID: 14436 RVA: 0x000DFE28 File Offset: 0x000DF228
		public EasingPoint3DKeyFrame(Point3D value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingPoint3DKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Point3D" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Point3D" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x06003865 RID: 14437 RVA: 0x000DFE4C File Offset: 0x000DF24C
		public EasingPoint3DKeyFrame(Point3D value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003866 RID: 14438 RVA: 0x000DFE74 File Offset: 0x000DF274
		protected override Freezable CreateInstanceCore()
		{
			return new EasingPoint3DKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003867 RID: 14439 RVA: 0x000DFE88 File Offset: 0x000DF288
		protected override Point3D InterpolateValueCore(Point3D baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolatePoint3D(baseValue, base.Value, keyFrameProgress);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x000DFED8 File Offset: 0x000DF2D8
		// (set) Token: 0x06003869 RID: 14441 RVA: 0x000DFEF8 File Offset: 0x000DF2F8
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingPoint3DKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingPoint3DKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingPoint3DKeyFrame.EasingFunction" />.</summary>
		// Token: 0x04001690 RID: 5776
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingPoint3DKeyFrame));
	}
}
