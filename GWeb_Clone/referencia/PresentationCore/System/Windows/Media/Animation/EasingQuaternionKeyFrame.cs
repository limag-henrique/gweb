using System;
using System.Windows.Media.Media3D;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Uma classe que permite associar funções de easing a uma animação de quadro chave <see cref="T:System.Windows.Media.Animation.QuaternionAnimationUsingKeyFrames" />.</summary>
	// Token: 0x020004E6 RID: 1254
	public class EasingQuaternionKeyFrame : QuaternionKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingQuaternionKeyFrame" />.</summary>
		// Token: 0x0600386B RID: 14443 RVA: 0x000DFF44 File Offset: 0x000DF344
		public EasingQuaternionKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingQuaternionKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Quaternion" /> especificado.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Quaternion" /> inicial.</param>
		// Token: 0x0600386C RID: 14444 RVA: 0x000DFF58 File Offset: 0x000DF358
		public EasingQuaternionKeyFrame(Quaternion value) : this()
		{
			base.Value = value;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingQuaternionKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Quaternion" /> e o tempo-chave especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Quaternion" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		// Token: 0x0600386D RID: 14445 RVA: 0x000DFF74 File Offset: 0x000DF374
		public EasingQuaternionKeyFrame(Quaternion value, KeyTime keyTime) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.EasingQuaternionKeyFrame" /> com o valor <see cref="T:System.Windows.Media.Media3D.Quaternion" />, o tempo-chave e a função de easing especificados.</summary>
		/// <param name="value">O valor <see cref="T:System.Windows.Media.Media3D.Quaternion" /> inicial.</param>
		/// <param name="keyTime">O tempo-chave inicial.</param>
		/// <param name="easingFunction">A função de easing.</param>
		// Token: 0x0600386E RID: 14446 RVA: 0x000DFF98 File Offset: 0x000DF398
		public EasingQuaternionKeyFrame(Quaternion value, KeyTime keyTime, IEasingFunction easingFunction) : this()
		{
			base.Value = value;
			base.KeyTime = keyTime;
			this.EasingFunction = easingFunction;
		}

		/// <summary>Cria uma nova instância da classe derivada <see cref="T:System.Windows.Freezable" />. Ao criar uma classe derivada, é necessário substituir esse método.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600386F RID: 14447 RVA: 0x000DFFC0 File Offset: 0x000DF3C0
		protected override Freezable CreateInstanceCore()
		{
			return new EasingQuaternionKeyFrame();
		}

		/// <summary>Interpola, de acordo com a função de easing usada, entre o valor de quadro-chave anterior e o valor do quadro-chave atual, usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003870 RID: 14448 RVA: 0x000DFFD4 File Offset: 0x000DF3D4
		protected override Quaternion InterpolateValueCore(Quaternion baseValue, double keyFrameProgress)
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
			return AnimatedTypeHelpers.InterpolateQuaternion(baseValue, base.Value, keyFrameProgress, this.UseShortestPath);
		}

		/// <summary>Obtém ou define a função de easing aplicada ao quadro-chave.</summary>
		/// <returns>A função de easing aplicada ao quadro-chave.</returns>
		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x000E0028 File Offset: 0x000DF428
		// (set) Token: 0x06003872 RID: 14450 RVA: 0x000E0048 File Offset: 0x000DF448
		public IEasingFunction EasingFunction
		{
			get
			{
				return (IEasingFunction)base.GetValue(EasingQuaternionKeyFrame.EasingFunctionProperty);
			}
			set
			{
				base.SetValueInternal(EasingQuaternionKeyFrame.EasingFunctionProperty, value);
			}
		}

		/// <summary>Obtém ou define um valor que indica se a animação automaticamente inverterá o sinal do quaternion de destino para garantir que o menor caminho seja usado.</summary>
		/// <returns>
		///   <see langword="true" /> Se a animação automaticamente inverterá o sinal do quaternion de destino para garantir que o caminho mais curto será executado; Caso contrário, <see langword="false" />.</returns>
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000E0064 File Offset: 0x000DF464
		// (set) Token: 0x06003874 RID: 14452 RVA: 0x000E0084 File Offset: 0x000DF484
		public bool UseShortestPath
		{
			get
			{
				return (bool)base.GetValue(EasingQuaternionKeyFrame.UseShortestPathProperty);
			}
			set
			{
				base.SetValue(EasingQuaternionKeyFrame.UseShortestPathProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingQuaternionKeyFrame.EasingFunction" />.</summary>
		// Token: 0x04001691 RID: 5777
		public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(EasingQuaternionKeyFrame));

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.EasingQuaternionKeyFrame.UseShortestPath" />.</summary>
		// Token: 0x04001692 RID: 5778
		public static readonly DependencyProperty UseShortestPathProperty = DependencyProperty.Register("UseShortestPath", typeof(bool), typeof(EasingQuaternionKeyFrame), new PropertyMetadata(BooleanBoxes.TrueBox));
	}
}
