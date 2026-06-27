using System;
using System.Runtime.InteropServices;

namespace MS.Internal.Text.TextInterface
{
	// Token: 0x02000007 RID: 7
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal sealed class FontMetrics
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000AF94 File Offset: 0x0000A394
		public double Baseline
		{
			get
			{
				return ((double)this.LineGap * 0.5 + this.Ascent) / this.DesignUnitsPerEm;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000AFC4 File Offset: 0x0000A3C4
		public double LineSpacing
		{
			get
			{
				return (double)(this.LineGap + (short)this.Descent + (short)this.Ascent) / this.DesignUnitsPerEm;
			}
		}

		// Token: 0x040002F9 RID: 761
		[FieldOffset(0)]
		public ushort DesignUnitsPerEm;

		// Token: 0x040002FA RID: 762
		[FieldOffset(2)]
		public ushort Ascent;

		// Token: 0x040002FB RID: 763
		[FieldOffset(4)]
		public ushort Descent;

		// Token: 0x040002FC RID: 764
		[FieldOffset(8)]
		public short LineGap;

		// Token: 0x040002FD RID: 765
		[FieldOffset(10)]
		public ushort CapHeight;

		// Token: 0x040002FE RID: 766
		[FieldOffset(12)]
		public ushort XHeight;

		// Token: 0x040002FF RID: 767
		[FieldOffset(14)]
		public short UnderlinePosition;

		// Token: 0x04000300 RID: 768
		[FieldOffset(16)]
		public ushort UnderlineThickness;

		// Token: 0x04000301 RID: 769
		[FieldOffset(18)]
		public short StrikethroughPosition;

		// Token: 0x04000302 RID: 770
		[FieldOffset(20)]
		public ushort StrikethroughThickness;
	}
}
