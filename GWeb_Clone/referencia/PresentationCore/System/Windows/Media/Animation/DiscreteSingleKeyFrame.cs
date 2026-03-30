using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Single" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004D2 RID: 1234
	public class DiscreteSingleKeyFrame : SingleKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteSingleKeyFrame" />.</summary>
		// Token: 0x0600379B RID: 14235 RVA: 0x000DD6B4 File Offset: 0x000DCAB4
		public DiscreteSingleKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteSingleKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x0600379C RID: 14236 RVA: 0x000DD6C8 File Offset: 0x000DCAC8
		public DiscreteSingleKeyFrame(float value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteSingleKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x0600379D RID: 14237 RVA: 0x000DD6DC File Offset: 0x000DCADC
		public DiscreteSingleKeyFrame(float value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteSingleKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteSingleKeyFrame" />.</returns>
		// Token: 0x0600379E RID: 14238 RVA: 0x000DD6F4 File Offset: 0x000DCAF4
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteSingleKeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600379F RID: 14239 RVA: 0x000DD708 File Offset: 0x000DCB08
		protected override float InterpolateValueCore(float baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
