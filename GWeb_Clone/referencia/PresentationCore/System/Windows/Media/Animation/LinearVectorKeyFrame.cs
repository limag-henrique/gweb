using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Vector" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x0200051C RID: 1308
	public class LinearVectorKeyFrame : VectorKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearVectorKeyFrame" />.</summary>
		// Token: 0x06003B0B RID: 15115 RVA: 0x000E7EFC File Offset: 0x000E72FC
		public LinearVectorKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearVectorKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003B0C RID: 15116 RVA: 0x000E7F10 File Offset: 0x000E7310
		public LinearVectorKeyFrame(Vector value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearVectorKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003B0D RID: 15117 RVA: 0x000E7F24 File Offset: 0x000E7324
		public LinearVectorKeyFrame(Vector value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearVectorKeyFrame" />.</summary>
		/// <returns>Uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearVectorKeyFrame" />.</returns>
		// Token: 0x06003B0E RID: 15118 RVA: 0x000E7F3C File Offset: 0x000E733C
		protected override Freezable CreateInstanceCore()
		{
			return new LinearVectorKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003B0F RID: 15119 RVA: 0x000E7F50 File Offset: 0x000E7350
		protected override Vector InterpolateValueCore(Vector baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateVector(baseValue, base.Value, keyFrameProgress);
		}
	}
}
