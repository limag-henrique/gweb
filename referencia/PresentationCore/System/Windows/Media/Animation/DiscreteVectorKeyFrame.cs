using System;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Vector" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.VectorKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004D5 RID: 1237
	public class DiscreteVectorKeyFrame : VectorKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteVectorKeyFrame" />.</summary>
		// Token: 0x060037AA RID: 14250 RVA: 0x000DD81C File Offset: 0x000DCC1C
		public DiscreteVectorKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteVectorKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x060037AB RID: 14251 RVA: 0x000DD830 File Offset: 0x000DCC30
		public DiscreteVectorKeyFrame(Vector value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteVectorKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x060037AC RID: 14252 RVA: 0x000DD844 File Offset: 0x000DCC44
		public DiscreteVectorKeyFrame(Vector value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteVectorKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x060037AD RID: 14253 RVA: 0x000DD85C File Offset: 0x000DCC5C
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteVectorKeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x060037AE RID: 14254 RVA: 0x000DD870 File Offset: 0x000DCC70
		protected override Vector InterpolateValueCore(Vector baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
