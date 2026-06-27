using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Int16" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004C8 RID: 1224
	public class DiscreteInt16KeyFrame : Int16KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteInt16KeyFrame" />.</summary>
		// Token: 0x06003769 RID: 14185 RVA: 0x000DD204 File Offset: 0x000DC604
		public DiscreteInt16KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteInt16KeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		// Token: 0x0600376A RID: 14186 RVA: 0x000DD218 File Offset: 0x000DC618
		public DiscreteInt16KeyFrame(short value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteInt16KeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		/// <param name="keyTime">O tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x0600376B RID: 14187 RVA: 0x000DD22C File Offset: 0x000DC62C
		public DiscreteInt16KeyFrame(short value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteInt16KeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteInt16KeyFrame" />.</returns>
		// Token: 0x0600376C RID: 14188 RVA: 0x000DD244 File Offset: 0x000DC644
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteInt16KeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor de 0,0 a 1,0 que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600376D RID: 14189 RVA: 0x000DD258 File Offset: 0x000DC658
		protected override short InterpolateValueCore(short baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
