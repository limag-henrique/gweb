using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Point3D" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Point3DKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004CE RID: 1230
	public class DiscretePoint3DKeyFrame : Point3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscretePoint3DKeyFrame" />.</summary>
		// Token: 0x06003787 RID: 14215 RVA: 0x000DD4D4 File Offset: 0x000DC8D4
		public DiscretePoint3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscretePoint3DKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		// Token: 0x06003788 RID: 14216 RVA: 0x000DD4E8 File Offset: 0x000DC8E8
		public DiscretePoint3DKeyFrame(Point3D value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscretePoint3DKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		/// <param name="keyTime">O tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003789 RID: 14217 RVA: 0x000DD4FC File Offset: 0x000DC8FC
		public DiscretePoint3DKeyFrame(Point3D value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscretePoint3DKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscretePoint3DKeyFrame" />.</returns>
		// Token: 0x0600378A RID: 14218 RVA: 0x000DD514 File Offset: 0x000DC914
		protected override Freezable CreateInstanceCore()
		{
			return new DiscretePoint3DKeyFrame();
		}

		/// <summary>Interpola, de forma discreta, entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso especificado.</summary>
		/// <param name="baseValue">O valor do qual animar.</param>
		/// <param name="keyFrameProgress">Um valor de 0,0 a 1,0 que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x0600378B RID: 14219 RVA: 0x000DD528 File Offset: 0x000DC928
		protected override Point3D InterpolateValueCore(Point3D baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
