using System;
using System.Windows.Media.Media3D;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Rotation3D" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.Rotation3DKeyFrame.Value" /> com interpolação discreta.</summary>
	// Token: 0x020004D0 RID: 1232
	public class DiscreteRotation3DKeyFrame : Rotation3DKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteRotation3DKeyFrame" />.</summary>
		// Token: 0x06003791 RID: 14225 RVA: 0x000DD5C4 File Offset: 0x000DC9C4
		public DiscreteRotation3DKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteRotation3DKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		// Token: 0x06003792 RID: 14226 RVA: 0x000DD5D8 File Offset: 0x000DC9D8
		public DiscreteRotation3DKeyFrame(Rotation3D value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteRotation3DKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">O valor de término (também conhecido como “valor de destino”) para o quadro-chave.</param>
		/// <param name="keyTime">O tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003793 RID: 14227 RVA: 0x000DD5EC File Offset: 0x000DC9EC
		public DiscreteRotation3DKeyFrame(Rotation3D value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância da classe <see cref="T:System.Windows.Media.Animation.DiscreteRotation3DKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003794 RID: 14228 RVA: 0x000DD604 File Offset: 0x000DCA04
		protected override Freezable CreateInstanceCore()
		{
			return new DiscreteRotation3DKeyFrame();
		}

		/// <summary>Computa uma interpolação discreta entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor de 0,0 a 1,0 que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003795 RID: 14229 RVA: 0x000DD618 File Offset: 0x000DCA18
		protected override Rotation3D InterpolateValueCore(Rotation3D baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress < 1.0)
			{
				return baseValue;
			}
			return base.Value;
		}
	}
}
