using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Double" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x02000511 RID: 1297
	public class LinearDoubleKeyFrame : DoubleKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearDoubleKeyFrame" />.</summary>
		// Token: 0x06003AD1 RID: 15057 RVA: 0x000E784C File Offset: 0x000E6C4C
		public LinearDoubleKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearDoubleKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003AD2 RID: 15058 RVA: 0x000E7860 File Offset: 0x000E6C60
		public LinearDoubleKeyFrame(double value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearDoubleKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003AD3 RID: 15059 RVA: 0x000E7874 File Offset: 0x000E6C74
		public LinearDoubleKeyFrame(double value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearDoubleKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearDoubleKeyFrame" />.</returns>
		// Token: 0x06003AD4 RID: 15060 RVA: 0x000E788C File Offset: 0x000E6C8C
		protected override Freezable CreateInstanceCore()
		{
			return new LinearDoubleKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003AD5 RID: 15061 RVA: 0x000E78A0 File Offset: 0x000E6CA0
		protected override double InterpolateValueCore(double baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateDouble(baseValue, base.Value, keyFrameProgress);
		}
	}
}
