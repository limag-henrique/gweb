using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Media.Composition
{
	// Token: 0x02000641 RID: 1601
	internal static class MilCoreApi
	{
		// Token: 0x06004808 RID: 18440
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll")]
		internal static extern int MilComposition_SyncFlush(IntPtr pChannel);

		// Token: 0x06004809 RID: 18441
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll")]
		internal unsafe static extern int MilUtility_GetPointAtLengthFraction(MilMatrix3x2D* pMatrix, FillRule fillRule, byte* pPathData, uint nSize, double rFraction, out Point pt, out Point vecTangent);

		// Token: 0x0600480A RID: 18442
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("wpfgfx_v0400.dll")]
		internal unsafe static extern int MilUtility_PolygonBounds(MilMatrix3x2D* pWorldMatrix, MIL_PEN_DATA* pPenData, double* pDashArray, Point* pPoints, byte* pTypes, uint pointCount, uint segmentCount, MilMatrix3x2D* pGeometryMatrix, double rTolerance, bool fRelative, bool fSkipHollows, Rect* pBounds);

		// Token: 0x0600480B RID: 18443
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll")]
		internal unsafe static extern int MilUtility_PolygonHitTest(MilMatrix3x2D* pGeometryMatrix, MIL_PEN_DATA* pPenData, double* pDashArray, Point* pPoints, byte* pTypes, uint cPoints, uint cSegments, double rTolerance, bool fRelative, Point* pHitPoint, out bool pDoesContain);

		// Token: 0x0600480C RID: 18444
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("wpfgfx_v0400.dll")]
		internal unsafe static extern int MilUtility_PathGeometryHitTest(MilMatrix3x2D* pMatrix, MIL_PEN_DATA* pPenData, double* pDashArray, FillRule fillRule, byte* pPathData, uint nSize, double rTolerance, bool fRelative, Point* pHitPoint, out bool pDoesContain);

		// Token: 0x0600480D RID: 18445
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("wpfgfx_v0400.dll")]
		internal unsafe static extern int MilUtility_PathGeometryHitTestPathGeometry(MilMatrix3x2D* pMatrix1, FillRule fillRule1, byte* pPathData1, uint nSize1, MilMatrix3x2D* pMatrix2, FillRule fillRule2, byte* pPathData2, uint nSize2, double rTolerance, bool fRelative, IntersectionDetail* pDetail);

		// Token: 0x0600480E RID: 18446
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll")]
		internal unsafe static extern int MilUtility_GeometryGetArea(FillRule fillRule, byte* pPathData, uint nSize, MilMatrix3x2D* pMatrix, double rTolerance, bool fRelative, double* pArea);

		// Token: 0x0600480F RID: 18447
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[DllImport("wpfgfx_v0400.dll")]
		internal unsafe static extern void MilUtility_ArcToBezier(Point ptStart, Size rRadii, double rRotation, bool fLargeArc, SweepDirection fSweepUp, Point ptEnd, MilMatrix3x2D* pMatrix, Point* pPt, out int cPieces);
	}
}
