using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Rect" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.RectKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x02000519 RID: 1305
	public class LinearRectKeyFrame : RectKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearRectKeyFrame" />.</summary>
		// Token: 0x06003AFC RID: 15100 RVA: 0x000E7D4C File Offset: 0x000E714C
		public LinearRectKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearRectKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003AFD RID: 15101 RVA: 0x000E7D60 File Offset: 0x000E7160
		public LinearRectKeyFrame(Rect value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearRectKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003AFE RID: 15102 RVA: 0x000E7D74 File Offset: 0x000E7174
		public LinearRectKeyFrame(Rect value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearRectKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003AFF RID: 15103 RVA: 0x000E7D8C File Offset: 0x000E718C
		protected override Freezable CreateInstanceCore()
		{
			return new LinearRectKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003B00 RID: 15104 RVA: 0x000E7DA0 File Offset: 0x000E71A0
		protected override Rect InterpolateValueCore(Rect baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateRect(baseValue, base.Value, keyFrameProgress);
		}
	}
}
