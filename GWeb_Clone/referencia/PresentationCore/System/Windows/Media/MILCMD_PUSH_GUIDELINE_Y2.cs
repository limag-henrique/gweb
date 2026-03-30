using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003E7 RID: 999
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_PUSH_GUIDELINE_Y2
	{
		// Token: 0x060026EE RID: 9966 RVA: 0x0009B5F8 File Offset: 0x0009A9F8
		public MILCMD_PUSH_GUIDELINE_Y2(double leadingCoordinate, double offsetToDrivenCoordinate)
		{
			this.leadingCoordinate = leadingCoordinate;
			this.offsetToDrivenCoordinate = offsetToDrivenCoordinate;
		}

		// Token: 0x04001237 RID: 4663
		[FieldOffset(0)]
		public double leadingCoordinate;

		// Token: 0x04001238 RID: 4664
		[FieldOffset(8)]
		public double offsetToDrivenCoordinate;
	}
}
