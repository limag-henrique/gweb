using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Byte" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.ByteKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x0200050E RID: 1294
	public class LinearByteKeyFrame : ByteKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearByteKeyFrame" />.</summary>
		// Token: 0x06003AC2 RID: 15042 RVA: 0x000E769C File Offset: 0x000E6A9C
		public LinearByteKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearByteKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003AC3 RID: 15043 RVA: 0x000E76B0 File Offset: 0x000E6AB0
		public LinearByteKeyFrame(byte value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearByteKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003AC4 RID: 15044 RVA: 0x000E76C4 File Offset: 0x000E6AC4
		public LinearByteKeyFrame(byte value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearByteKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearByteKeyFrame" />.</returns>
		// Token: 0x06003AC5 RID: 15045 RVA: 0x000E76DC File Offset: 0x000E6ADC
		protected override Freezable CreateInstanceCore()
		{
			return new LinearByteKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003AC6 RID: 15046 RVA: 0x000E76F0 File Offset: 0x000E6AF0
		protected override byte InterpolateValueCore(byte baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateByte(baseValue, base.Value, keyFrameProgress);
		}
	}
}
