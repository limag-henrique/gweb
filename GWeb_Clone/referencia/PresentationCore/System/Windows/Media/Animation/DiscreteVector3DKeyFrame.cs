using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Vector3D" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Vector3DKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004D6 RID: 1238
	public class DiscreteVector3DKeyFrame : Vector3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteVector3DKeyFrame" />.</summary>
		// Token: 0x060037AF RID: 14255 RVA: 0x000DD894 File Offset: 0x000DCC94
		public DiscreteVector3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteVector3DKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x060037B0 RID: 14256 RVA: 0x000DD8A8 File Offset: 0x000DCCA8
		public DiscreteVector3DKeyFrame(Vector3D value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteVector3DKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Key time para o quadro-chave. O key time determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x060037B1 RID: 14257 RVA: 0x000DD8BC File Offset: 0x000DCCBC
		public DiscreteVector3DKeyFrame(Vector3D value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteVector3DKeyFrame" />.</summary>
		/// <returns>Uma nova instância de <see cref="T:System.Windows.Media.Animation.DiscreteVector3DKeyFrame" />.</returns>
		// Token: 0x060037B2 RID: 14258 RVA: 0x000DD8D4 File Offset: 0x000DCCD4
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteVector3DKeyFrame();
		}

		/// <summary>Usa a interpolação discreta para transicionar entre o valor de quadro-chave anterior e o valor de quadro-chave atual.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor de 0,0 a 1,0 que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x060037B3 RID: 14259 RVA: 0x000DD8E8 File Offset: 0x000DCCE8
		protected override Vector3D InterpolateValueCore(Vector3D baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
