using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003E0 RID: 992
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_PUSH_CLIP
	{
		// Token: 0x060026E7 RID: 9959 RVA: 0x0009B530 File Offset: 0x0009A930
		public MILCMD_PUSH_CLIP(uint hClipGeometry)
		{
			this.hClipGeometry = hClipGeometry;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x04001229 RID: 4649
		[FieldOffset(0)]
		public uint hClipGeometry;

		// Token: 0x0400122A RID: 4650
		[FieldOffset(4)]
		private uint QuadWordPad0;
	}
}
