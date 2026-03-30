using System;
using System.Runtime.InteropServices;

namespace MS.Internal.Ink
{
	// Token: 0x020007B6 RID: 1974
	internal static class Native
	{
		// Token: 0x04002592 RID: 9618
		internal static readonly uint SizeOfInt = (uint)Marshal.SizeOf(typeof(int));

		// Token: 0x04002593 RID: 9619
		internal static readonly uint SizeOfUInt = (uint)Marshal.SizeOf(typeof(uint));

		// Token: 0x04002594 RID: 9620
		internal static readonly uint SizeOfUShort = (uint)Marshal.SizeOf(typeof(ushort));

		// Token: 0x04002595 RID: 9621
		internal static readonly uint SizeOfByte = (uint)Marshal.SizeOf(typeof(byte));

		// Token: 0x04002596 RID: 9622
		internal static readonly uint SizeOfFloat = (uint)Marshal.SizeOf(typeof(float));

		// Token: 0x04002597 RID: 9623
		internal static readonly uint SizeOfDouble = (uint)Marshal.SizeOf(typeof(double));

		// Token: 0x04002598 RID: 9624
		internal static readonly uint SizeOfGuid = (uint)Marshal.SizeOf(typeof(Guid));

		// Token: 0x04002599 RID: 9625
		internal static readonly uint SizeOfDecimal = (uint)Marshal.SizeOf(typeof(decimal));

		// Token: 0x0400259A RID: 9626
		internal const int BitsPerByte = 8;

		// Token: 0x0400259B RID: 9627
		internal const int BitsPerShort = 16;

		// Token: 0x0400259C RID: 9628
		internal const int BitsPerInt = 32;

		// Token: 0x0400259D RID: 9629
		internal const int BitsPerLong = 64;

		// Token: 0x0400259E RID: 9630
		internal const int MaxFloatToIntValue = 2147483583;
	}
}
