using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Rect" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.RectKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004D1 RID: 1233
	public class DiscreteRectKeyFrame : RectKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteRectKeyFrame" />.</summary>
		// Token: 0x06003796 RID: 14230 RVA: 0x000DD63C File Offset: 0x000DCA3C
		public DiscreteRectKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteRectKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003797 RID: 14231 RVA: 0x000DD650 File Offset: 0x000DCA50
		public DiscreteRectKeyFrame(Rect value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteRectKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003798 RID: 14232 RVA: 0x000DD664 File Offset: 0x000DCA64
		public DiscreteRectKeyFrame(Rect value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteRectKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteRectKeyFrame" />.</returns>
		// Token: 0x06003799 RID: 14233 RVA: 0x000DD67C File Offset: 0x000DCA7C
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteRectKeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600379A RID: 14234 RVA: 0x000DD690 File Offset: 0x000DCA90
		protected override Rect InterpolateValueCore(Rect baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
