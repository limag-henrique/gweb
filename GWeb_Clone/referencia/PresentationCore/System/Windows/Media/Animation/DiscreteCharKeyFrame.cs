using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Char" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.CharKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004C4 RID: 1220
	public class DiscreteCharKeyFrame : CharKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteCharKeyFrame" />.</summary>
		// Token: 0x06003755 RID: 14165 RVA: 0x000DD024 File Offset: 0x000DC424
		public DiscreteCharKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteCharKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003756 RID: 14166 RVA: 0x000DD038 File Offset: 0x000DC438
		public DiscreteCharKeyFrame(char value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteCharKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003757 RID: 14167 RVA: 0x000DD04C File Offset: 0x000DC44C
		public DiscreteCharKeyFrame(char value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteCharKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteCharKeyFrame" />.</returns>
		// Token: 0x06003758 RID: 14168 RVA: 0x000DD064 File Offset: 0x000DC464
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteCharKeyFrame();
		}

		/// <summary>Interpola entre o valor de quadro-chave anterior e o valor de quadro-chave atual usando a interpolação discreta.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003759 RID: 14169 RVA: 0x000DD078 File Offset: 0x000DC478
		protected override char InterpolateValueCore(char baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
