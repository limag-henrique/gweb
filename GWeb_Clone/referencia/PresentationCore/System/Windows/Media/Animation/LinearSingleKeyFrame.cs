using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Single" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.SingleKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x0200051A RID: 1306
	public class LinearSingleKeyFrame : SingleKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearSingleKeyFrame" />.</summary>
		// Token: 0x06003B01 RID: 15105 RVA: 0x000E7DDC File Offset: 0x000E71DC
		public LinearSingleKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearSingleKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003B02 RID: 15106 RVA: 0x000E7DF0 File Offset: 0x000E71F0
		public LinearSingleKeyFrame(float value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearSingleKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003B03 RID: 15107 RVA: 0x000E7E04 File Offset: 0x000E7204
		public LinearSingleKeyFrame(float value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearSingleKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearSingleKeyFrame" />.</returns>
		// Token: 0x06003B04 RID: 15108 RVA: 0x000E7E1C File Offset: 0x000E721C
		protected override Freezable CreateInstanceCore()
		{
			return new LinearSingleKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003B05 RID: 15109 RVA: 0x000E7E30 File Offset: 0x000E7230
		protected override float InterpolateValueCore(float baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateSingle(baseValue, base.Value, keyFrameProgress);
		}
	}
}
