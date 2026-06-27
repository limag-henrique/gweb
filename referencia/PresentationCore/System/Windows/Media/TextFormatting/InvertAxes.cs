using System;

namespace System.Windows.Media.TextFormatting
{
	/// <summary>Indica a inversão dos eixos horizontal e vertical da superfície de desenho.</summary>
	// Token: 0x020005AB RID: 1451
	[Flags]
	public enum InvertAxes
	{
		/// <summary>A superfície de desenho não é invertida em nenhum eixo.</summary>
		// Token: 0x04001831 RID: 6193
		None = 0,
		/// <summary>A superfície de desenho é invertida no eixo horizontal.</summary>
		// Token: 0x04001832 RID: 6194
		Horizontal = 1,
		/// <summary>A superfície de desenho é invertida no eixo vertical.</summary>
		// Token: 0x04001833 RID: 6195
		Vertical = 2,
		/// <summary>A superfície de desenho é invertida em ambos os eixos.</summary>
		// Token: 0x04001834 RID: 6196
		Both = 3
	}
}
