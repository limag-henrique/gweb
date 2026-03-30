using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003E2 RID: 994
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_PUSH_OPACITY
	{
		// Token: 0x060026E9 RID: 9961 RVA: 0x0009B574 File Offset: 0x0009A974
		public MILCMD_PUSH_OPACITY(double opacity)
		{
			this.opacity = opacity;
		}

		// Token: 0x0400122E RID: 4654
		[FieldOffset(0)]
		public double opacity;
	}
}
