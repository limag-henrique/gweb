using System;

namespace MS.Win32.Recognizer
{
	// Token: 0x0200065A RID: 1626
	internal struct RECO_LATTICE_ELEMENT
	{
		// Token: 0x04001BF0 RID: 7152
		public int score;

		// Token: 0x04001BF1 RID: 7153
		public ushort type;

		// Token: 0x04001BF2 RID: 7154
		public IntPtr pData;

		// Token: 0x04001BF3 RID: 7155
		public uint ulNextColumn;

		// Token: 0x04001BF4 RID: 7156
		public uint ulStrokeNumber;

		// Token: 0x04001BF5 RID: 7157
		public RECO_LATTICE_PROPERTIES epProp;
	}
}
