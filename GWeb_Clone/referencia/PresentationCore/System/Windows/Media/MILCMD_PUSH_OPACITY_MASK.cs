using System;
using System.Runtime.InteropServices;
using System.Windows.Media.Composition;

namespace System.Windows.Media
{
	// Token: 0x020003E1 RID: 993
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_PUSH_OPACITY_MASK
	{
		// Token: 0x060026E8 RID: 9960 RVA: 0x0009B54C File Offset: 0x0009A94C
		public MILCMD_PUSH_OPACITY_MASK(uint hOpacityMask)
		{
			this.hOpacityMask = hOpacityMask;
			this.boundingBoxCacheLocalSpace = default(MilRectF);
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x0400122B RID: 4651
		[FieldOffset(0)]
		public MilRectF boundingBoxCacheLocalSpace;

		// Token: 0x0400122C RID: 4652
		[FieldOffset(16)]
		public uint hOpacityMask;

		// Token: 0x0400122D RID: 4653
		[FieldOffset(20)]
		private uint QuadWordPad0;
	}
}
