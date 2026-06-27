using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Decimal" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.DecimalKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004C6 RID: 1222
	public class DiscreteDecimalKeyFrame : DecimalKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteDecimalKeyFrame" />.</summary>
		// Token: 0x0600375F RID: 14175 RVA: 0x000DD114 File Offset: 0x000DC514
		public DiscreteDecimalKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteDecimalKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003760 RID: 14176 RVA: 0x000DD128 File Offset: 0x000DC528
		public DiscreteDecimalKeyFrame(decimal value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteDecimalKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003761 RID: 14177 RVA: 0x000DD13C File Offset: 0x000DC53C
		public DiscreteDecimalKeyFrame(decimal value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteDecimalKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003762 RID: 14178 RVA: 0x000DD154 File Offset: 0x000DC554
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteDecimalKeyFrame();
		}

		/// <summary>Interpola entre o valor de quadro-chave anterior e o valor de quadro-chave atual usando a interpolação discreta.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003763 RID: 14179 RVA: 0x000DD168 File Offset: 0x000DC568
		protected override decimal InterpolateValueCore(decimal baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
