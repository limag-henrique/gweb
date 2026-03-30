using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006C6 RID: 1734
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ExtensionLookupTable
	{
		// Token: 0x06004B76 RID: 19318 RVA: 0x00126D80 File Offset: 0x00126180
		internal ushort LookupType(FontTable Table)
		{
			return Table.GetUShort(this.offset + 2);
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00126D9C File Offset: 0x0012619C
		internal int LookupSubtableOffset(FontTable Table)
		{
			return this.offset + (int)Table.GetUInt(this.offset + 4);
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00126DC0 File Offset: 0x001261C0
		public ExtensionLookupTable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04002054 RID: 8276
		private const int offsetFormat = 0;

		// Token: 0x04002055 RID: 8277
		private const int offsetLookupType = 2;

		// Token: 0x04002056 RID: 8278
		private const int offsetExtensionOffset = 4;

		// Token: 0x04002057 RID: 8279
		private int offset;
	}
}
