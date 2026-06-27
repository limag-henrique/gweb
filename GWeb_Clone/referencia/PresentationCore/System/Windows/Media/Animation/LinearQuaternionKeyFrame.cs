using System;
using System.Windows.Media.Media3D;
using MS.Internal.KnownBoxes;
using MS.Internal.PresentationCore;

namespace System.Windows.Media.Animation
{
	/// <summary>Anima do valor <see cref="T:System.Windows.Media.Media3D.Quaternion" /> do quadro chave anterior para seu próprio <see cref="P:System.Windows.Media.Animation.QuaternionKeyFrame.Value" /> com interpolação linear.</summary>
	// Token: 0x02000517 RID: 1303
	public class LinearQuaternionKeyFrame : QuaternionKeyFrame
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearQuaternionKeyFrame" />.</summary>
		// Token: 0x06003AEF RID: 15087 RVA: 0x000E7BAC File Offset: 0x000E6FAC
		public LinearQuaternionKeyFrame()
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearQuaternionKeyFrame" /> com valor de término especificado.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		// Token: 0x06003AF0 RID: 15088 RVA: 0x000E7BC0 File Offset: 0x000E6FC0
		public LinearQuaternionKeyFrame(Quaternion value) : base(value)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Media.Animation.LinearQuaternionKeyFrame" /> com o tempo-chave e o valor de término especificados.</summary>
		/// <param name="value">Valor de término (também conhecido como "valor de destino") para o quadro-chave.</param>
		/// <param name="keyTime">Tempo-chave para o quadro-chave. O tempo-chave determina quando o valor de destino é atingido, que também é quando o quadro-chave termina.</param>
		// Token: 0x06003AF1 RID: 15089 RVA: 0x000E7BD4 File Offset: 0x000E6FD4
		public LinearQuaternionKeyFrame(Quaternion value, KeyTime keyTime) : base(value, keyTime)
		{
		}

		/// <summary>Cria uma nova instância de <see cref="T:System.Windows.Media.Animation.LinearQuaternionKeyFrame" />.</summary>
		/// <returns>A nova instância.</returns>
		// Token: 0x06003AF2 RID: 15090 RVA: 0x000E7BEC File Offset: 0x000E6FEC
		protected override Freezable CreateInstanceCore()
		{
			return new LinearQuaternionKeyFrame();
		}

		/// <summary>Realiza a interpolação linear entre o valor de quadro-chave anterior e o valor do quadro-chave atual usando o incremento de progresso fornecido.</summary>
		/// <param name="baseValue">O valor a ser usado para animar.</param>
		/// <param name="keyFrameProgress">Um valor entre 0,0 e 1,0, inclusive, que especifica o percentual de tempo decorrido para este quadro-chave.</param>
		/// <returns>O valor de saída desse quadro-chave, considerando o valor base e o andamento especificados.</returns>
		// Token: 0x06003AF3 RID: 15091 RVA: 0x000E7C00 File Offset: 0x000E7000
		protected override Quaternion InterpolateValueCore(Quaternion baseValue, double keyFrameProgress)
		{
			if (keyFrameProgress == 0.0)
			{
				return baseValue;
			}
			if (keyFrameProgress == 1.0)
			{
				return base.Value;
			}
			return AnimatedTypeHelpers.InterpolateQuaternion(baseValue, base.Value, keyFrameProgress, this.UseShortestPath);
		}

		/// <summary>Obtém ou define um valor booliano que indica se a animação usa interpolação linear esférica para calcular o arco mais curto entre posições. É uma propriedade de dependência.</summary>
		/// <returns>Valor booliano que indica se a animação usa interpolação linear esférica para calcular o arco mais curto entre posições.</returns>
		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000E7C44 File Offset: 0x000E7044
		// (set) Token: 0x06003AF5 RID: 15093 RVA: 0x000E7C64 File Offset: 0x000E7064
		public bool UseShortestPath
		{
			get
			{
				return (bool)base.GetValue(LinearQuaternionKeyFrame.UseShortestPathProperty);
			}
			set
			{
				base.SetValue(LinearQuaternionKeyFrame.UseShortestPathProperty, value);
			}
		}

		/// <summary>Identifica a propriedade de dependência <see cref="P:System.Windows.Media.Animation.LinearQuaternionKeyFrame.UseShortestPath" />.</summary>
		// Token: 0x040016E7 RID: 5863
		public static readonly DependencyProperty UseShortestPathProperty = DependencyProperty.Register("UseShortestPath", typeof(bool), typeof(LinearQuaternionKeyFrame), new PropertyMetadata(BooleanBoxes.TrueBox));
	}
}
