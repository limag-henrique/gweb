using System;

namespace MS.Win32.Recognizer
{
	// Token: 0x0200065C RID: 1628
	internal struct RECO_LATTICE
	{
		// Token: 0x04001BFC RID: 7164
		public uint ulColumnCount;

		// Token: 0x04001BFD RID: 7165
		public IntPtr pLatticeColumns;

		// Token: 0x04001BFE RID: 7166
		public uint ulPropertyCount;

		// Token: 0x04001BFF RID: 7167
		public IntPtr pGuidProperties;

		// Token: 0x04001C00 RID: 7168
		public uint ulBestResultColumnCount;

		// Token: 0x04001C01 RID: 7169
		public IntPtr pulBestResultColumns;

		// Token: 0x04001C02 RID: 7170
		public IntPtr pulBestResultIndexes;
	}
}
