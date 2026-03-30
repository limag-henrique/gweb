using System;

namespace System.Windows.Ink
{
	/// <summary>Representa uma ponta de caneta retangular.</summary>
	// Token: 0x02000340 RID: 832
	public sealed class RectangleStylusShape : StylusShape
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.RectangleStylusShape" /> com a largura e a altura especificadas.</summary>
		/// <param name="width">A largura da forma de caneta.</param>
		/// <param name="height">A altura da forma de caneta.</param>
		// Token: 0x06001C46 RID: 7238 RVA: 0x0007371C File Offset: 0x00072B1C
		public RectangleStylusShape(double width, double height) : this(width, height, 0.0)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.RectangleStylusShape" /> com a largura, a altura e o ângulo.</summary>
		/// <param name="width">A largura da forma de caneta.</param>
		/// <param name="height">A altura da forma de caneta.</param>
		/// <param name="rotation">O ângulo da forma de caneta.</param>
		// Token: 0x06001C47 RID: 7239 RVA: 0x0007373C File Offset: 0x00072B3C
		public RectangleStylusShape(double width, double height, double rotation) : base(StylusTip.Rectangle, width, height, rotation)
		{
		}
	}
}
