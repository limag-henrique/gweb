using System;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000748 RID: 1864
	internal struct LsQSubInfo
	{
		// Token: 0x0400234E RID: 9038
		public LsTFlow lstflowSubLine;

		// Token: 0x0400234F RID: 9039
		public int lscpFirstSubLine;

		// Token: 0x04002350 RID: 9040
		public int lsdcpSubLine;

		// Token: 0x04002351 RID: 9041
		public LSPOINT pointUvStartSubLine;

		// Token: 0x04002352 RID: 9042
		public LsHeights lsHeightsPresSubLine;

		// Token: 0x04002353 RID: 9043
		public int dupSubLine;

		// Token: 0x04002354 RID: 9044
		public uint idobj;

		// Token: 0x04002355 RID: 9045
		public IntPtr plsrun;

		// Token: 0x04002356 RID: 9046
		public int lscpFirstRun;

		// Token: 0x04002357 RID: 9047
		public int lsdcpRun;

		// Token: 0x04002358 RID: 9048
		public LSPOINT pointUvStartRun;

		// Token: 0x04002359 RID: 9049
		public LsHeights lsHeightsPresRun;

		// Token: 0x0400235A RID: 9050
		public int dupRun;

		// Token: 0x0400235B RID: 9051
		public int dvpPosRun;

		// Token: 0x0400235C RID: 9052
		public int dupBorderBefore;

		// Token: 0x0400235D RID: 9053
		public int dupBorderAfter;

		// Token: 0x0400235E RID: 9054
		public LSPOINT pointUvStartObj;

		// Token: 0x0400235F RID: 9055
		public LsHeights lsHeightsPresObj;

		// Token: 0x04002360 RID: 9056
		public int dupObj;
	}
}
