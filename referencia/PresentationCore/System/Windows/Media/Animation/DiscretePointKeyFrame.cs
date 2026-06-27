using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Point" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.PointKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004CD RID: 1229
	public class DiscretePointKeyFrame : PointKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscretePointKeyFrame" />.</summary>
		// Token: 0x06003782 RID: 14210 RVA: 0x000DD45C File Offset: 0x000DC85C
		public DiscretePointKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscretePointKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003783 RID: 14211 RVA: 0x000DD470 File Offset: 0x000DC870
		public DiscretePointKeyFrame(Point value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscretePointKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003784 RID: 14212 RVA: 0x000DD484 File Offset: 0x000DC884
		public DiscretePointKeyFrame(Point value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscretePointKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscretePointKeyFrame" />.</returns>
		// Token: 0x06003785 RID: 14213 RVA: 0x000DD49C File Offset: 0x000DC89C
		protected override Freezable CreateInstanceCore()
		{
			return new DiscretePointKeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003786 RID: 14214 RVA: 0x000DD4B0 File Offset: 0x000DC8B0
		protected override Point InterpolateValueCore(Point baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
