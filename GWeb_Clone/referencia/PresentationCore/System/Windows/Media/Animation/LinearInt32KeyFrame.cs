using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Int32" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Int32KeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x02000513 RID: 1299
	public class LinearInt32KeyFrame : Int32KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearInt32KeyFrame" />.</summary>
		// Token: 0x06003ADB RID: 15067 RVA: 0x000E796C File Offset: 0x000E6D6C
		public LinearInt32KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearInt32KeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003ADC RID: 15068 RVA: 0x000E7980 File Offset: 0x000E6D80
		public LinearInt32KeyFrame(int value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearInt32KeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003ADD RID: 15069 RVA: 0x000E7994 File Offset: 0x000E6D94
		public LinearInt32KeyFrame(int value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearInt32KeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003ADE RID: 15070 RVA: 0x000E79AC File Offset: 0x000E6DAC
		protected override Freezable CreateInstanceCore()
		{
			return new LinearInt32KeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003ADF RID: 15071 RVA: 0x000E79C0 File Offset: 0x000E6DC0
		protected override int InterpolateValueCore(int baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateInt32(baseValue, base.Value, keyFrameProgress);
		}
	}
}
