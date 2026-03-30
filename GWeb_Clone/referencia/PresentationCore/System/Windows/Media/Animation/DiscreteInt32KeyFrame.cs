using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Int32" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004C9 RID: 1225
	public class DiscreteInt32KeyFrame : Int32KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteInt32KeyFrame" />.</summary>
		// Token: 0x0600376E RID: 14190 RVA: 0x000DD27C File Offset: 0x000DC67C
		public DiscreteInt32KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteInt32KeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x0600376F RID: 14191 RVA: 0x000DD290 File Offset: 0x000DC690
		public DiscreteInt32KeyFrame(int value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteInt32KeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003770 RID: 14192 RVA: 0x000DD2A4 File Offset: 0x000DC6A4
		public DiscreteInt32KeyFrame(int value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteInt32KeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteInt32KeyFrame" />.</returns>
		// Token: 0x06003771 RID: 14193 RVA: 0x000DD2BC File Offset: 0x000DC6BC
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteInt32KeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003772 RID: 14194 RVA: 0x000DD2D0 File Offset: 0x000DC6D0
		protected override int InterpolateValueCore(int baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
