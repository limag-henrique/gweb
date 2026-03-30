using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003E5 RID: 997
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_PUSH_GUIDELINE_SET
	{
		// Token: 0x060026EC RID: 9964 RVA: 0x0009B5C8 File Offset: 0x0009A9C8
		public MILCMD_PUSH_GUIDELINE_SET(uint hGuidelines)
		{
			this.hGuidelines = hGuidelines;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x04001234 RID: 4660
		[FieldOffset(0)]
		public uint hGuidelines;

		// Token: 0x04001235 RID: 4661
		[FieldOffset(4)]
		private uint QuadWordPad0;
	}
}
