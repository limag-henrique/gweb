using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003E4 RID: 996
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_PUSH_TRANSFORM
	{
		// Token: 0x060026EB RID: 9963 RVA: 0x0009B5AC File Offset: 0x0009A9AC
		public MILCMD_PUSH_TRANSFORM(uint hTransform)
		{
			this.hTransform = hTransform;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x04001232 RID: 4658
		[FieldOffset(0)]
		public uint hTransform;

		// Token: 0x04001233 RID: 4659
		[FieldOffset(4)]
		private uint QuadWordPad0;
	}
}
