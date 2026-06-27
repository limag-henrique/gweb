using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Object" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.ObjectKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004CC RID: 1228
	public class DiscreteObjectKeyFrame : ObjectKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteObjectKeyFrame" />.</summary>
		// Token: 0x0600377D RID: 14205 RVA: 0x000DD3E4 File Offset: 0x000DC7E4
		public DiscreteObjectKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteObjectKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x0600377E RID: 14206 RVA: 0x000DD3F8 File Offset: 0x000DC7F8
		public DiscreteObjectKeyFrame(object value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteObjectKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x0600377F RID: 14207 RVA: 0x000DD40C File Offset: 0x000DC80C
		public DiscreteObjectKeyFrame(object value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteObjectKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteObjectKeyFrame" />.</returns>
		// Token: 0x06003780 RID: 14208 RVA: 0x000DD424 File Offset: 0x000DC824
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteObjectKeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003781 RID: 14209 RVA: 0x000DD438 File Offset: 0x000DC838
		protected override object InterpolateValueCore(object baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
