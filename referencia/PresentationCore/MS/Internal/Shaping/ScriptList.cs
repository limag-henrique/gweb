using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006BD RID: 1725
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ScriptList
	{
		// Token: 0x06004B3B RID: 19259 RVA: 0x001263A4 File Offset: 0x001257A4
		public ScriptTable FindScript(FontTable Table, uint Tag)
		{
			for (ushort num = 0; num < this.GetScriptCount(Table); num += 1)
			{
				if (this.GetScriptTag(Table, num) == Tag)
				{
					return this.GetScriptTable(Table, num);
				}
			}
			return new ScriptTable(int.MaxValue);
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x001263E4 File Offset: 0x001257E4
		public ushort GetScriptCount(FontTable Table)
		{
			return Table.GetUShort(this.offset);
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x00126400 File Offset: 0x00125800
		public uint GetScriptTag(FontTable Table, ushort Index)
		{
			return Table.GetUInt(this.offset + 2 + (int)(Index * 6));
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x00126420 File Offset: 0x00125820
		public ScriptTable GetScriptTable(FontTable Table, ushort Index)
		{
			return new ScriptTable(this.offset + (int)Table.GetOffset(this.offset + 2 + (int)(Index * 6) + 4));
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x00126450 File Offset: 0x00125850
		public ScriptList(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x04002014 RID: 8212
		private const int offsetScriptCount = 0;

		// Token: 0x04002015 RID: 8213
		private const int offsetScriptRecordArray = 2;

		// Token: 0x04002016 RID: 8214
		private const int sizeScriptRecord = 6;

		// Token: 0x04002017 RID: 8215
		private const int offsetScriptRecordTag = 0;

		// Token: 0x04002018 RID: 8216
		private const int offsetScriptRecordOffset = 4;

		// Token: 0x04002019 RID: 8217
		private int offset;
	}
}
