using System;
using System.Runtime.InteropServices;

namespace System.Windows.Media
{
	// Token: 0x020003D8 RID: 984
	[StructLayout(LayoutKind.Explicit)]
	internal struct MILCMD_DRAW_ELLIPSE_ANIMATE
	{
		// Token: 0x060026DF RID: 9951 RVA: 0x0009B3E8 File Offset: 0x0009A7E8
		public MILCMD_DRAW_ELLIPSE_ANIMATE(uint hBrush, uint hPen, Point center, uint hCenterAnimations, double radiusX, uint hRadiusXAnimations, double radiusY, uint hRadiusYAnimations)
		{
			this.hBrush = hBrush;
			this.hPen = hPen;
			this.center = center;
			this.hCenterAnimations = hCenterAnimations;
			this.radiusX = radiusX;
			this.hRadiusXAnimations = hRadiusXAnimations;
			this.radiusY = radiusY;
			this.hRadiusYAnimations = hRadiusYAnimations;
			this.QuadWordPad0 = 0U;
		}

		// Token: 0x0400120C RID: 4620
		[FieldOffset(0)]
		public Point center;

		// Token: 0x0400120D RID: 4621
		[FieldOffset(16)]
		public double radiusX;

		// Token: 0x0400120E RID: 4622
		[FieldOffset(24)]
		public double radiusY;

		// Token: 0x0400120F RID: 4623
		[FieldOffset(32)]
		public uint hBrush;

		// Token: 0x04001210 RID: 4624
		[FieldOffset(36)]
		public uint hPen;

		// Token: 0x04001211 RID: 4625
		[FieldOffset(40)]
		public uint hCenterAnimations;

		// Token: 0x04001212 RID: 4626
		[FieldOffset(44)]
		public uint hRadiusXAnimations;

		// Token: 0x04001213 RID: 4627
		[FieldOffset(48)]
		public uint hRadiusYAnimations;

		// Token: 0x04001214 RID: 4628
		[FieldOffset(52)]
		private uint QuadWordPad0;
	}
}
