using System;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x02000518 RID: 1304
	public class LinearRotation3DKeyFrame : Rotation3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearRotation3DKeyFrame" />.</summary>
		// Token: 0x06003AF7 RID: 15095 RVA: 0x000E7CBC File Offset: 0x000E70BC
		public LinearRotation3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearRotation3DKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		// Token: 0x06003AF8 RID: 15096 RVA: 0x000E7CD0 File Offset: 0x000E70D0
		public LinearRotation3DKeyFrame(Rotation3D value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearRotation3DKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		/// <param name="keyTime">O tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003AF9 RID: 15097 RVA: 0x000E7CE4 File Offset: 0x000E70E4
		public LinearRotation3DKeyFrame(Rotation3D value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearRotation3DKeyFrame" />.</summary>
		/// <returns>Uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearRotation3DKeyFrame" />.</returns>
		// Token: 0x06003AFA RID: 15098 RVA: 0x000E7CFC File Offset: 0x000E70FC
		protected override Freezable CreateInstanceCore()
		{
			return new LinearRotation3DKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor de 0,0 a 1,0 que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003AFB RID: 15099 RVA: 0x000E7D10 File Offset: 0x000E7110
		protected override Rotation3D InterpolateValueCore(Rotation3D baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateRotation3D(baseValue, base.Value, keyFrameProgress);
		}
	}
}
