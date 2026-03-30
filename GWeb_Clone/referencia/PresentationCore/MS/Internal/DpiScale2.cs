using System;
using System.Windows;

namespace MS.Internal
{
	// Token: 0x02000688 RID: 1672
	internal class DpiScale2 : IEquatable<DpiScale2>, IEquatable<DpiScale>
	{
		// Token: 0x0600499B RID: 18843 RVA: 0x0011EEB8 File Offset: 0x0011E2B8
		internal DpiScale2(DpiScale dpiScale)
		{
			this.dpiScale = dpiScale;
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x0011EED4 File Offset: 0x0011E2D4
		internal DpiScale2(double dpiScaleX, double dpiScaleY) : this(new DpiScale(dpiScaleX, dpiScaleY))
		{
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x0600499D RID: 18845 RVA: 0x0011EEF0 File Offset: 0x0011E2F0
		internal double DpiScaleX
		{
			get
			{
				return this.dpiScale.DpiScaleX;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x0600499E RID: 18846 RVA: 0x0011EF08 File Offset: 0x0011E308
		internal double DpiScaleY
		{
			get
			{
				return this.dpiScale.DpiScaleY;
			}
		}

		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x0011EF20 File Offset: 0x0011E320
		internal double PixelsPerDip
		{
			get
			{
				return this.dpiScale.PixelsPerDip;
			}
		}

		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x0011EF38 File Offset: 0x0011E338
		internal double PixelsPerInchX
		{
			get
			{
				return this.dpiScale.PixelsPerInchX;
			}
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x0011EF50 File Offset: 0x0011E350
		internal double PixelsPerInchY
		{
			get
			{
				return this.dpiScale.PixelsPerInchY;
			}
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x0011EF68 File Offset: 0x0011E368
		public static implicit operator DpiScale(DpiScale2 dpiScale2)
		{
			return dpiScale2.dpiScale;
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x0011EF7C File Offset: 0x0011E37C
		public static bool operator !=(DpiScale2 dpiScaleA, DpiScale2 dpiScaleB)
		{
			return (dpiScaleA == null && dpiScaleB != null) || (dpiScaleA != null && dpiScaleB == null) || !dpiScaleA.Equals(dpiScaleB);
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x0011EFA4 File Offset: 0x0011E3A4
		public static bool operator ==(DpiScale2 dpiScaleA, DpiScale2 dpiScaleB)
		{
			return (dpiScaleA == null && dpiScaleB == null) || dpiScaleA.Equals(dpiScaleB);
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x0011EFC0 File Offset: 0x0011E3C0
		public bool Equals(DpiScale dpiScale)
		{
			return DoubleUtil.AreClose(this.DpiScaleX, dpiScale.DpiScaleX) && DoubleUtil.AreClose(this.DpiScaleY, dpiScale.DpiScaleY);
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x0011EFF8 File Offset: 0x0011E3F8
		public bool Equals(DpiScale2 dpiScale2)
		{
			return dpiScale2 != null && this.Equals(dpiScale2.dpiScale);
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x0011F018 File Offset: 0x0011E418
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is DpiScale)
			{
				result = this.Equals((DpiScale)obj);
			}
			else if (obj is DpiScale2)
			{
				result = this.Equals((DpiScale2)obj);
			}
			else
			{
				result = base.Equals(obj);
			}
			return result;
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x0011F060 File Offset: 0x0011E460
		public override int GetHashCode()
		{
			return ((int)this.PixelsPerInchX).GetHashCode();
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x0011F07C File Offset: 0x0011E47C
		internal static DpiScale2 FromPixelsPerInch(double ppiX, double ppiY)
		{
			if (DoubleUtil.LessThanOrClose(ppiX, 0.0) || DoubleUtil.LessThanOrClose(ppiY, 0.0))
			{
				return null;
			}
			return new DpiScale2(ppiX / 96.0, ppiY / 96.0);
		}

		// Token: 0x04001D0C RID: 7436
		private DpiScale dpiScale;
	}
}
