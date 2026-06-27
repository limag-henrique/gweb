using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Int64" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Int64KeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x02000514 RID: 1300
	public class LinearInt64KeyFrame : Int64KeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearInt64KeyFrame" />.</summary>
		// Token: 0x06003AE0 RID: 15072 RVA: 0x000E79FC File Offset: 0x000E6DFC
		public LinearInt64KeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearInt64KeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003AE1 RID: 15073 RVA: 0x000E7A10 File Offset: 0x000E6E10
		public LinearInt64KeyFrame(long value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearInt64KeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003AE2 RID: 15074 RVA: 0x000E7A24 File Offset: 0x000E6E24
		public LinearInt64KeyFrame(long value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearInt64KeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearInt64KeyFrame" />.</returns>
		// Token: 0x06003AE3 RID: 15075 RVA: 0x000E7A3C File Offset: 0x000E6E3C
		protected override Freezable CreateInstanceCore()
		{
			return new LinearInt64KeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003AE4 RID: 15076 RVA: 0x000E7A50 File Offset: 0x000E6E50
		protected override long InterpolateValueCore(long baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateInt64(baseValue, base.Value, keyFrameProgress);
		}
	}
}
