using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Double" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.DoubleKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004C7 RID: 1223
	public class DiscreteDoubleKeyFrame : DoubleKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteDoubleKeyFrame" />.</summary>
		// Token: 0x06003764 RID: 14180 RVA: 0x000DD18C File Offset: 0x000DC58C
		public DiscreteDoubleKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteDoubleKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003765 RID: 14181 RVA: 0x000DD1A0 File Offset: 0x000DC5A0
		public DiscreteDoubleKeyFrame(double value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteDoubleKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003766 RID: 14182 RVA: 0x000DD1B4 File Offset: 0x000DC5B4
		public DiscreteDoubleKeyFrame(double value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteDoubleKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteDoubleKeyFrame" />.</returns>
		// Token: 0x06003767 RID: 14183 RVA: 0x000DD1CC File Offset: 0x000DC5CC
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteDoubleKeyFrame();
		}

		/// <summary>Interpola entre o valor de quadro-chave anterior e o valor de quadro-chave atual usando a interpolação discreta.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003768 RID: 14184 RVA: 0x000DD1E0 File Offset: 0x000DC5E0
		protected override double InterpolateValueCore(double baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
