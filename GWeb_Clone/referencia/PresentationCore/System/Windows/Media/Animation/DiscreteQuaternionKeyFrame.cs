using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Quaternion" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004CF RID: 1231
	public class DiscreteQuaternionKeyFrame : QuaternionKeyFrame
	{
		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteQuaternionKeyFrame" />.</summary>
		// Token: 0x0600378C RID: 14220 RVA: 0x000DD54C File Offset: 0x000DC94C
		public DiscreteQuaternionKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteQuaternionKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x0600378D RID: 14221 RVA: 0x000DD560 File Offset: 0x000DC960
		public DiscreteQuaternionKeyFrame(Quaternion value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteQuaternionKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x0600378E RID: 14222 RVA: 0x000DD574 File Offset: 0x000DC974
		public DiscreteQuaternionKeyFrame(Quaternion value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteQuaternionKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x0600378F RID: 14223 RVA: 0x000DD58C File Offset: 0x000DC98C
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteQuaternionKeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003790 RID: 14224 RVA: 0x000DD5A0 File Offset: 0x000DC9A0
		protected override Quaternion InterpolateValueCore(Quaternion baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
