using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000749 RID: 1865
	internal struct LsTextCell
	{
		// Token: 0x04002361 RID: 9057
		public int lscpStartCell;

		// Token: 0x04002362 RID: 9058
		public int lscpEndCell;

		// Token: 0x04002363 RID: 9059
		public LSPOINT pointUvStartCell;

		// Token: 0x04002364 RID: 9060
		public int dupCell;

		// Token: 0x04002365 RID: 9061
		public int cCharsInCell;

		// Token: 0x04002366 RID: 9062
		public int cGlyphsInCell;

		// Token: 0x04002367 RID: 9063
		public IntPtr plsCellDetails;
	}
}
