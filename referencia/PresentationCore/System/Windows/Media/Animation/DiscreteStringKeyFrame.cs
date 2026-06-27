using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.String" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.StringKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004D4 RID: 1236
	public class DiscreteStringKeyFrame : StringKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteStringKeyFrame" />.</summary>
		// Token: 0x060037A5 RID: 14245 RVA: 0x000DD7A4 File Offset: 0x000DCBA4
		public DiscreteStringKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteStringKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x060037A6 RID: 14246 RVA: 0x000DD7B8 File Offset: 0x000DCBB8
		public DiscreteStringKeyFrame(string value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteStringKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x060037A7 RID: 14247 RVA: 0x000DD7CC File Offset: 0x000DCBCC
		public DiscreteStringKeyFrame(string value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteStringKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x060037A8 RID: 14248 RVA: 0x000DD7E4 File Offset: 0x000DCBE4
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteStringKeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x060037A9 RID: 14249 RVA: 0x000DD7F8 File Offset: 0x000DCBF8
		protected override string InterpolateValueCore(string baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
