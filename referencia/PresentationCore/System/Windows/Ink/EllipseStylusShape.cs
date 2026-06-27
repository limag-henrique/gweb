using System;

namespace System.Windows.Ink
{
	/// <summary>Representa uma ponta de caneta com formato de elipse.</summary>
	// Token: 0x0200033F RID: 831
	public sealed class EllipseStylusShape : StylusShape
	{
		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.EllipseStylusShape" /> com a largura e a altura especificadas.</summary>
		/// <param name="width">A largura da forma de caneta.</param>
		/// <param name="height">A altura da forma de caneta.</param>
		// Token: 0x06001C44 RID: 7236 RVA: 0x000736E4 File Offset: 0x00072AE4
		public EllipseStylusShape(double width, double height) : this(width, height, 0.0)
		{
		}

		/// <summary>Inicializa uma nova instância da classe <see cref="T:System.Windows.Ink.EllipseStylusShape" /> com a largura, a altura e o ângulo.</summary>
		/// <param name="width">A largura da forma de caneta.</param>
		/// <param name="height">A altura da forma de caneta.</param>
		/// <param name="rotation">O ângulo da forma de caneta.</param>
		// Token: 0x06001C45 RID: 7237 RVA: 0x00073704 File Offset: 0x00072B04
		public EllipseStylusShape(double width, double height, double rotation) : base(StylusTip.Ellipse, width, height, rotation)
		{
		}
	}
}
