using System;
using System.Windows.Media;

namespace System.Windows.Ink
{
	// Token: 0x0200035B RID: 859
	internal static class MatrixHelper
	{
		// Token: 0x06001D29 RID: 7465 RVA: 0x000771BC File Offset: 0x000765BC
		internal static bool ContainsNaN(Matrix matrix)
		{
			return double.IsNaN(matrix.M11) || double.IsNaN(matrix.M12) || double.IsNaN(matrix.M21) || double.IsNaN(matrix.M22) || double.IsNaN(matrix.OffsetX) || double.IsNaN(matrix.OffsetY);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x00077220 File Offset: 0x00076620
		internal static bool ContainsInfinity(Matrix matrix)
		{
			return double.IsInfinity(matrix.M11) || double.IsInfinity(matrix.M12) || double.IsInfinity(matrix.M21) || double.IsInfinity(matrix.M22) || double.IsInfinity(matrix.OffsetX) || double.IsInfinity(matrix.OffsetY);
		}
	}
}
