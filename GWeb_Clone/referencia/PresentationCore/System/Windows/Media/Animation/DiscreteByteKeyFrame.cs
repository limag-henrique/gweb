using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Byte" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004C3 RID: 1219
	public class DiscreteByteKeyFrame : ByteKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteByteKeyFrame" />.</summary>
		// Token: 0x06003750 RID: 14160 RVA: 0x000DCFAC File Offset: 0x000DC3AC
		public DiscreteByteKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteByteKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003751 RID: 14161 RVA: 0x000DCFC0 File Offset: 0x000DC3C0
		public DiscreteByteKeyFrame(byte value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteByteKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003752 RID: 14162 RVA: 0x000DCFD4 File Offset: 0x000DC3D4
		public DiscreteByteKeyFrame(byte value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteByteKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003753 RID: 14163 RVA: 0x000DCFEC File Offset: 0x000DC3EC
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteByteKeyFrame();
		}

		/// <summary>Interpola entre o valor de quadro-chave anterior e o valor de quadro-chave atual usando a interpolação discreta.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003754 RID: 14164 RVA: 0x000DD000 File Offset: 0x000DC400
		protected override byte InterpolateValueCore(byte baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
