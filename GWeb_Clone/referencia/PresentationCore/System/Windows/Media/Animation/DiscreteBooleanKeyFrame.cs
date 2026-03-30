using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Boolean" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.BooleanKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004C2 RID: 1218
	public class DiscreteBooleanKeyFrame : BooleanKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteBooleanKeyFrame" />.</summary>
		// Token: 0x0600374B RID: 14155 RVA: 0x000DCF34 File Offset: 0x000DC334
		public DiscreteBooleanKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteBooleanKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x0600374C RID: 14156 RVA: 0x000DCF48 File Offset: 0x000DC348
		public DiscreteBooleanKeyFrame(bool value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteBooleanKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x0600374D RID: 14157 RVA: 0x000DCF5C File Offset: 0x000DC35C
		public DiscreteBooleanKeyFrame(bool value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteBooleanKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600374E RID: 14158 RVA: 0x000DCF74 File Offset: 0x000DC374
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteBooleanKeyFrame();
		}

		/// <summary>Interpola entre o valor de quadro-chave anterior e o valor de quadro-chave atual usando a interpolação discreta.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600374F RID: 14159 RVA: 0x000DCF88 File Offset: 0x000DC388
		protected override bool InterpolateValueCore(bool baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
