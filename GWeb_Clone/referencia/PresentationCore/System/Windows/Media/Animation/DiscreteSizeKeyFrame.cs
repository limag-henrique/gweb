using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Size" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.SizeKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004D3 RID: 1235
	public class DiscreteSizeKeyFrame : SizeKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteSizeKeyFrame" />.</summary>
		// Token: 0x060037A0 RID: 14240 RVA: 0x000DD72C File Offset: 0x000DCB2C
		public DiscreteSizeKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteSizeKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x060037A1 RID: 14241 RVA: 0x000DD740 File Offset: 0x000DCB40
		public DiscreteSizeKeyFrame(Size value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteSizeKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x060037A2 RID: 14242 RVA: 0x000DD754 File Offset: 0x000DCB54
		public DiscreteSizeKeyFrame(Size value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteSizeKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteSizeKeyFrame" />.</returns>
		// Token: 0x060037A3 RID: 14243 RVA: 0x000DD76C File Offset: 0x000DCB6C
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteSizeKeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x060037A4 RID: 14244 RVA: 0x000DD780 File Offset: 0x000DCB80
		protected override Size InterpolateValueCore(Size baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
