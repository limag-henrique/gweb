using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003E3 RID: 995
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_PUSH_OPACITY_ANIMATE
	{
		// Token: 0x060026EA RID: 9962 RVA: 0x0009B588 File Offset: 0x0009A988
		public MILCMD_PUSH_OPACITY_ANIMATE(double opacity, uint hOpacityAnimations)
		{
			this.opacity = opacity;
			this.hOpacityAnimations = hOpacityAnimations;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x0400122F RID: 4655
		[FieldOffset(0)]
		public double opacity;

		// Token: 0x04001230 RID: 4656
		[FieldOffset(8)]
		public uint hOpacityAnimations;

		// Token: 0x04001231 RID: 4657
		[FieldOffset(12)]
		private uint QuadWordPad0;
	}
}
