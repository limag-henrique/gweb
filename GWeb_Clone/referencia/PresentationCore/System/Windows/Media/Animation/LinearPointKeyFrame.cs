using System;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Point" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.PointKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x02000515 RID: 1301
	public class LinearPointKeyFrame : PointKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearPointKeyFrame" />.</summary>
		// Token: 0x06003AE5 RID: 15077 RVA: 0x000E7A8C File Offset: 0x000E6E8C
		public LinearPointKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearPointKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003AE6 RID: 15078 RVA: 0x000E7AA0 File Offset: 0x000E6EA0
		public LinearPointKeyFrame(Point value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearPointKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003AE7 RID: 15079 RVA: 0x000E7AB4 File Offset: 0x000E6EB4
		public LinearPointKeyFrame(Point value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearPointKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003AE8 RID: 15080 RVA: 0x000E7ACC File Offset: 0x000E6ECC
		protected override Freezable CreateInstanceCore()
		{
			return new LinearPointKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003AE9 RID: 15081 RVA: 0x000E7AE0 File Offset: 0x000E6EE0
		protected override Point InterpolateValueCore(Point baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolatePoint(baseValue, base.Value, keyFrameProgress);
		}
	}
}
