using System;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007DC RID: 2012
	internal struct GorillaAlgoByte
	{
		// Token: 0x060054A4 RID: 21668 RVA: 0x0015C8E0 File Offset: 0x0015BCE0
		public GorillaAlgoByte(uint bitCount, uint padCount)
		{
			this.BitCount = bitCount;
			this.PadCount = padCount;
		}

		// Token: 0x04002627 RID: 9767
		public uint BitCount;

		// Token: 0x04002628 RID: 9768
		public uint PadCount;
	}
}
