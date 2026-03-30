using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006C1 RID: 1729
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct FeatureTable
	{
		// Token: 0x06004B52 RID: 19282 RVA: 0x00126750 File Offset: 0x00125B50
		public ushort LookupCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 2);
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x0012676C File Offset: 0x00125B6C
		public ushort LookupIndex(FontTable Table, ushort Index)
		{
			return Table.GetUShort(this.offset + 4 + (int)(Index * 2));
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x0012678C File Offset: 0x00125B8C
		public FeatureTable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06004B55 RID: 19285 RVA: 0x001267A0 File Offset: 0x00125BA0
		public bool IsNull
		{
			get
			{
				return this.offset == int.MaxValue;
			}
		}

		// Token: 0x0400202C RID: 8236
		private const int offsetLookupCount = 2;

		// Token: 0x0400202D RID: 8237
		private const int offsetLookupIndexArray = 4;

		// Token: 0x0400202E RID: 8238
		private const int sizeLookupIndex = 2;

		// Token: 0x0400202F RID: 8239
		private int offset;
	}
}
