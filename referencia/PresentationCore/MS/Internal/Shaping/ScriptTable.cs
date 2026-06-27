using System;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006BE RID: 1726
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct ScriptTable
	{
		// Token: 0x06004B40 RID: 19264 RVA: 0x00126464 File Offset: 0x00125864
		public LangSysTable FindLangSys(FontTable Table, uint Tag)
		{
			if (this.IsNull)
			{
				return new LangSysTable(int.MaxValue);
			}
			if (Tag != 1684434036U)
			{
				for (ushort num = 0; num < this.GetLangSysCount(Table); num += 1)
				{
					if (this.GetLangSysTag(Table, num) == Tag)
					{
						return this.GetLangSysTable(Table, num);
					}
				}
				return new LangSysTable(int.MaxValue);
			}
			if (this.IsDefaultLangSysExists(Table))
			{
				return new LangSysTable(this.offset + (int)Table.GetOffset(this.offset));
			}
			return new LangSysTable(int.MaxValue);
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x001264EC File Offset: 0x001258EC
		public bool IsDefaultLangSysExists(FontTable Table)
		{
			return Table.GetOffset(this.offset) > 0;
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x00126508 File Offset: 0x00125908
		public LangSysTable GetDefaultLangSysTable(FontTable Table)
		{
			if (this.IsDefaultLangSysExists(Table))
			{
				return new LangSysTable(this.offset + (int)Table.GetOffset(this.offset));
			}
			return new LangSysTable(int.MaxValue);
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x00126544 File Offset: 0x00125944
		public ushort GetLangSysCount(FontTable Table)
		{
			return Table.GetUShort(this.offset + 2);
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x00126560 File Offset: 0x00125960
		public uint GetLangSysTag(FontTable Table, ushort Index)
		{
			return Table.GetUInt(this.offset + 4 + (int)(Index * 6));
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x00126580 File Offset: 0x00125980
		public LangSysTable GetLangSysTable(FontTable Table, ushort Index)
		{
			return new LangSysTable(this.offset + (int)Table.GetOffset(this.offset + 4 + (int)(Index * 6) + 4));
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x001265B0 File Offset: 0x001259B0
		public ScriptTable(int Offset)
		{
			this.offset = Offset;
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06004B47 RID: 19271 RVA: 0x001265C4 File Offset: 0x001259C4
		public bool IsNull
		{
			get
			{
				return this.offset == int.MaxValue;
			}
		}

		// Token: 0x0400201A RID: 8218
		private const int offsetDefaultLangSys = 0;

		// Token: 0x0400201B RID: 8219
		private const int offsetLangSysCount = 2;

		// Token: 0x0400201C RID: 8220
		private const int offsetLangSysRecordArray = 4;

		// Token: 0x0400201D RID: 8221
		private const int sizeLangSysRecord = 6;

		// Token: 0x0400201E RID: 8222
		private const int offsetLangSysRecordTag = 0;

		// Token: 0x0400201F RID: 8223
		private const int offsetLangSysRecordOffset = 4;

		// Token: 0x04002020 RID: 8224
		private int offset;
	}
}
