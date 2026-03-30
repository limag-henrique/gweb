using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Int16" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Int16KeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x02000512 RID: 1298
	public class LinearInt16KeyFrame : Int16KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearInt16KeyFrame" />.</summary>
		// Token: 0x06003AD6 RID: 15062 RVA: 0x000E78DC File Offset: 0x000E6CDC
		public LinearInt16KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearInt16KeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003AD7 RID: 15063 RVA: 0x000E78F0 File Offset: 0x000E6CF0
		public LinearInt16KeyFrame(short value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearInt16KeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003AD8 RID: 15064 RVA: 0x000E7904 File Offset: 0x000E6D04
		public LinearInt16KeyFrame(short value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearInt16KeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Freezable" />.</returns>
		// Token: 0x06003AD9 RID: 15065 RVA: 0x000E791C File Offset: 0x000E6D1C
		protected override Freezable CreateInstanceCore()
		{
			return new LinearInt16KeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003ADA RID: 15066 RVA: 0x000E7930 File Offset: 0x000E6D30
		protected override short InterpolateValueCore(short baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateInt16(baseValue, base.Value, keyFrameProgress);
		}
	}
}
