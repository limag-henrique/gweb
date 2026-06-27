using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003E8 RID: 1000
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_PUSH_EFFECT
	{
		// Token: 0x060026EF RID: 9967 RVA: 0x0009B614 File Offset: 0x0009AA14
		public MILCMD_PUSH_EFFECT(uint hEffect, uint hEffectInput)
		{
			this.hEffect = hEffect;
			this.hEffectInput = hEffectInput;
		}

		// Token: 0x04001239 RID: 4665
		[FieldOffset(0)]
		public uint hEffect;

		// Token: 0x0400123A RID: 4666
		[FieldOffset(4)]
		public uint hEffectInput;
	}
}
