using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000633 RID: 1587
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct MilMatrix3x2D
	{
		// Token: 0x04001AE8 RID: 6888
		internal double S_11;

		// Token: 0x04001AE9 RID: 6889
		internal double S_12;

		// Token: 0x04001AEA RID: 6890
		internal double S_21;

		// Token: 0x04001AEB RID: 6891
		internal double S_22;

		// Token: 0x04001AEC RID: 6892
		internal double DX;

		// Token: 0x04001AED RID: 6893
		internal double DY;
	}
}
