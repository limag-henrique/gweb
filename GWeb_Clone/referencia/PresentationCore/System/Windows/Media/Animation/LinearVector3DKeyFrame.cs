using System;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Vector3D" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x0200051D RID: 1309
	public class LinearVector3DKeyFrame : Vector3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearVector3DKeyFrame" />.</summary>
		// Token: 0x06003B10 RID: 15120 RVA: 0x000E7F8C File Offset: 0x000E738C
		public LinearVector3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearVector3DKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003B11 RID: 15121 RVA: 0x000E7FA0 File Offset: 0x000E73A0
		public LinearVector3DKeyFrame(Vector3D value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearVector3DKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003B12 RID: 15122 RVA: 0x000E7FB4 File Offset: 0x000E73B4
		public LinearVector3DKeyFrame(Vector3D value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearVector3DKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003B13 RID: 15123 RVA: 0x000E7FCC File Offset: 0x000E73CC
		protected override Freezable CreateInstanceCore()
		{
			return new LinearVector3DKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003B14 RID: 15124 RVA: 0x000E7FE0 File Offset: 0x000E73E0
		protected override Vector3D InterpolateValueCore(Vector3D baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateVector3D(baseValue, base.Value, keyFrameProgress);
		}
	}
}
