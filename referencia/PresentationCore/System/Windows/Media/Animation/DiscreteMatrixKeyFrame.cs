using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Matrix" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.MatrixKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004CB RID: 1227
	public class DiscreteMatrixKeyFrame : MatrixKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteMatrixKeyFrame" />.</summary>
		// Token: 0x06003778 RID: 14200 RVA: 0x000DD36C File Offset: 0x000DC76C
		public DiscreteMatrixKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteMatrixKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003779 RID: 14201 RVA: 0x000DD380 File Offset: 0x000DC780
		public DiscreteMatrixKeyFrame(Matrix value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteMatrixKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x0600377A RID: 14202 RVA: 0x000DD394 File Offset: 0x000DC794
		public DiscreteMatrixKeyFrame(Matrix value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteMatrixKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600377B RID: 14203 RVA: 0x000DD3AC File Offset: 0x000DC7AC
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteMatrixKeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600377C RID: 14204 RVA: 0x000DD3C0 File Offset: 0x000DC7C0
		protected override Matrix InterpolateValueCore(Matrix baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
