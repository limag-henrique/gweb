using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003E6 RID: 998
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_PUSH_GUIDELINE_Y1
	{
		// Token: 0x060026ED RID: 9965 RVA: 0x0009B5E4 File Offset: 0x0009A9E4
		public MILCMD_PUSH_GUIDELINE_Y1(double coordinate)
		{
			this.coordinate = coordinate;
		}

		// Token: 0x04001236 RID: 4662
		[FieldOffset(0)]
		public double coordinate;
	}
}
