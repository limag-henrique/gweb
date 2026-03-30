using System;
using System.Security;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.TextFormatting
{
	// Token: 0x0200074D RID: 1869
	internal struct LsGlyphRunInfo
	{
		// Token: 0x0400237B RID: 9083
		public Plsrun plsrun;

		// Token: 0x0400237C RID: 9084
		[SecurityCritical]
		public unsafe char* pwch;

		// Token: 0x0400237D RID: 9085
		[SecurityCritical]
		public unsafe ushort* rggmap;

		// Token: 0x0400237E RID: 9086
		[SecurityCritical]
		public unsafe ushort* rgchprop;

		// Token: 0x0400237F RID: 9087
		public int cwch;

		// Token: 0x04002380 RID: 9088
		public int duChangeRight;

		// Token: 0x04002381 RID: 9089
		[SecurityCritical]
		public unsafe ushort* rggindex;

		// Token: 0x04002382 RID: 9090
		[SecurityCritical]
		public unsafe uint* rggprop;

		// Token: 0x04002383 RID: 9091
		[SecurityCritical]
		public unsafe int* rgduWidth;

		// Token: 0x04002384 RID: 9092
		[SecurityCritical]
		public unsafe GlyphOffset* rggoffset;

		// Token: 0x04002385 RID: 9093
		public int cgindex;
	}
}
