using System;
using System.Windows.Media.Media3D;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Point3D" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x02000516 RID: 1302
	public class LinearPoint3DKeyFrame : Point3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearPoint3DKeyFrame" />.</summary>
		// Token: 0x06003AEA RID: 15082 RVA: 0x000E7B1C File Offset: 0x000E6F1C
		public LinearPoint3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearPoint3DKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003AEB RID: 15083 RVA: 0x000E7B30 File Offset: 0x000E6F30
		public LinearPoint3DKeyFrame(Point3D value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearPoint3DKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003AEC RID: 15084 RVA: 0x000E7B44 File Offset: 0x000E6F44
		public LinearPoint3DKeyFrame(Point3D value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearPoint3DKeyFrame" />.</summary>
		/// <returns>Uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearPoint3DKeyFrame" />.</returns>
		// Token: 0x06003AED RID: 15085 RVA: 0x000E7B5C File Offset: 0x000E6F5C
		protected override Freezable CreateInstanceCore()
		{
			return new LinearPoint3DKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003AEE RID: 15086 RVA: 0x000E7B70 File Offset: 0x000E6F70
		protected override Point3D InterpolateValueCore(Point3D baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolatePoint3D(baseValue, base.Value, keyFrameProgress);
		}
	}
}
