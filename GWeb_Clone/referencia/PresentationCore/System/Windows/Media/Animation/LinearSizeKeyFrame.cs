using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Size" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x0200051B RID: 1307
	public class LinearSizeKeyFrame : SizeKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearSizeKeyFrame" />.</summary>
		// Token: 0x06003B06 RID: 15110 RVA: 0x000E7E6C File Offset: 0x000E726C
		public LinearSizeKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearSizeKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003B07 RID: 15111 RVA: 0x000E7E80 File Offset: 0x000E7280
		public LinearSizeKeyFrame(Size value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearSizeKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003B08 RID: 15112 RVA: 0x000E7E94 File Offset: 0x000E7294
		public LinearSizeKeyFrame(Size value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearSizeKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearSizeKeyFrame" />.</returns>
		// Token: 0x06003B09 RID: 15113 RVA: 0x000E7EAC File Offset: 0x000E72AC
		protected override Freezable CreateInstanceCore()
		{
			return new LinearSizeKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003B0A RID: 15114 RVA: 0x000E7EC0 File Offset: 0x000E72C0
		protected override Size InterpolateValueCore(Size baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateSize(baseValue, base.Value, keyFrameProgress);
		}
	}
}
