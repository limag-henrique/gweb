using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000745 RID: 1861
	internal struct LsChp
	{
		// Token: 0x04002342 RID: 9026
		public ushort idObj;

		// Token: 0x04002343 RID: 9027
		public ushort dcpMaxContent;

		// Token: 0x04002344 RID: 9028
		public uint effectsFlags;

		// Token: 0x04002345 RID: 9029
		public LsChp.Flags flags;

		// Token: 0x04002346 RID: 9030
		public int dvpPos;

		// Token: 0x020009DD RID: 2525
		[Flags]
		public enum Flags : uint
		{
			// Token: 0x04002E93 RID: 11923
			None = 0U,
			// Token: 0x04002E94 RID: 11924
			fApplyKern = 1U,
			// Token: 0x04002E95 RID: 11925
			fModWidthOnRun = 2U,
			// Token: 0x04002E96 RID: 11926
			fModWidthSpace = 4U,
			// Token: 0x04002E97 RID: 11927
			fModWidthPairs = 8U,
			// Token: 0x04002E98 RID: 11928
			fCompressOnRun = 16U,
			// Token: 0x04002E99 RID: 11929
			fCompressSpace = 32U,
			// Token: 0x04002E9A RID: 11930
			fCompressTable = 64U,
			// Token: 0x04002E9B RID: 11931
			fExpandOnRun = 128U,
			// Token: 0x04002E9C RID: 11932
			fExpandSpace = 256U,
			// Token: 0x04002E9D RID: 11933
			fExpandTable = 512U,
			// Token: 0x04002E9E RID: 11934
			fGlyphBased = 1024U,
			// Token: 0x04002E9F RID: 11935
			fInvisible = 65536U,
			// Token: 0x04002EA0 RID: 11936
			fUnderline = 131072U,
			// Token: 0x04002EA1 RID: 11937
			fStrike = 262144U,
			// Token: 0x04002EA2 RID: 11938
			fShade = 524288U,
			// Token: 0x04002EA3 RID: 11939
			fBorder = 1048576U,
			// Token: 0x04002EA4 RID: 11940
			fSymbol = 2097152U,
			// Token: 0x04002EA5 RID: 11941
			fHyphen = 4194304U,
			// Token: 0x04002EA6 RID: 11942
			fCheckForReplaceChar = 8388608U
		}
	}
}
