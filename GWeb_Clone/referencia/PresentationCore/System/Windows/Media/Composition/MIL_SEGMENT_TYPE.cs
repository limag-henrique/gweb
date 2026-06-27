using System;

namespace System.Windows.Media.Composition
{
	// Token: 0x0200061E RID: 1566
	internal enum MIL_SEGMENT_TYPE
	{
		// Token: 0x04001A3D RID: 6717
		MilSegmentNone,
		// Token: 0x04001A3E RID: 6718
		MilSegmentLine,
		// Token: 0x04001A3F RID: 6719
		MilSegmentBezier,
		// Token: 0x04001A40 RID: 6720
		MilSegmentQuadraticBezier,
		// Token: 0x04001A41 RID: 6721
		MilSegmentArc,
		// Token: 0x04001A42 RID: 6722
		MilSegmentPolyLine,
		// Token: 0x04001A43 RID: 6723
		MilSegmentPolyBezier,
		// Token: 0x04001A44 RID: 6724
		MilSegmentPolyQuadraticBezier,
		// Token: 0x04001A45 RID: 6725
		MIL_SEGMENT_TYPE_FORCE_DWORD = -1
	}
}
