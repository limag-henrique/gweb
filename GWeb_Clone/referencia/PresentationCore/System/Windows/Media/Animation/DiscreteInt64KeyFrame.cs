using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Int64" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004CA RID: 1226
	public class DiscreteInt64KeyFrame : Int64KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteInt64KeyFrame" />.</summary>
		// Token: 0x06003773 RID: 14195 RVA: 0x000DD2F4 File Offset: 0x000DC6F4
		public DiscreteInt64KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteInt64KeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003774 RID: 14196 RVA: 0x000DD308 File Offset: 0x000DC708
		public DiscreteInt64KeyFrame(long value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteInt64KeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003775 RID: 14197 RVA: 0x000DD31C File Offset: 0x000DC71C
		public DiscreteInt64KeyFrame(long value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteInt64KeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteInt64KeyFrame" />.</returns>
		// Token: 0x06003776 RID: 14198 RVA: 0x000DD334 File Offset: 0x000DC734
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteInt64KeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003777 RID: 14199 RVA: 0x000DD348 File Offset: 0x000DC748
		protected override long InterpolateValueCore(long baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
