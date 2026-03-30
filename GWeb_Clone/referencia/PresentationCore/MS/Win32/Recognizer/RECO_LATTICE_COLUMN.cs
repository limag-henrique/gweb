using System;

namespace MS.Win32.Recognizer
{
	// Token: 0x0200065B RID: 1627
	internal struct RECO_LATTICE_COLUMN
	{
		// Token: 0x04001BF6 RID: 7158
		public uint key;

		// Token: 0x04001BF7 RID: 7159
		public RECO_LATTICE_PROPERTIES cpProp;

		// Token: 0x04001BF8 RID: 7160
		public uint cStrokes;

		// Token: 0x04001BF9 RID: 7161
		public IntPtr pStrokes;

		// Token: 0x04001BFA RID: 7162
		public uint cLatticeElements;

		// Token: 0x04001BFB RID: 7163
		public IntPtr pLatticeElements;
	}
}
