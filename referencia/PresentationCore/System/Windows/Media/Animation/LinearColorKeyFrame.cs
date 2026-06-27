using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Color" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.ColorKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x0200050F RID: 1295
	public class LinearColorKeyFrame : ColorKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearColorKeyFrame" />.</summary>
		// Token: 0x06003AC7 RID: 15047 RVA: 0x000E772C File Offset: 0x000E6B2C
		public LinearColorKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearColorKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003AC8 RID: 15048 RVA: 0x000E7740 File Offset: 0x000E6B40
		public LinearColorKeyFrame(Color value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearColorKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003AC9 RID: 15049 RVA: 0x000E7754 File Offset: 0x000E6B54
		public LinearColorKeyFrame(Color value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearColorKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearColorKeyFrame" />.</returns>
		// Token: 0x06003ACA RID: 15050 RVA: 0x000E776C File Offset: 0x000E6B6C
		protected override Freezable CreateInstanceCore()
		{
			return new LinearColorKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003ACB RID: 15051 RVA: 0x000E7780 File Offset: 0x000E6B80
		protected override Color InterpolateValueCore(Color baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateColor(baseValue, base.Value, keyFrameProgress);
		}
	}
}
