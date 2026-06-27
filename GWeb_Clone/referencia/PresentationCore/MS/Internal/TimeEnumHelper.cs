using System;
using System.Windows.Media.Animation;

namespace MS.Internal
{
	// Token: 0x020006A0 RID: 1696
	internal static class TimeEnumHelper
	{
		// Token: 0x06004A46 RID: 19014 RVA: 0x00120F44 File Offset: 0x00120344
		internal static bool IsValidFillBehavior(FillBehavior value)
		{
			return FillBehavior.HoldEnd <= value && value <= FillBehavior.Stop;
		}

		// Token: 0x06004A47 RID: 19015 RVA: 0x00120F60 File Offset: 0x00120360
		internal static bool IsValidSlipBehavior(SlipBehavior value)
		{
			return SlipBehavior.Grow <= value && value <= SlipBehavior.Slip;
		}

		// Token: 0x06004A48 RID: 19016 RVA: 0x00120F7C File Offset: 0x0012037C
		internal static bool IsValidTimeSeekOrigin(TimeSeekOrigin value)
		{
			return TimeSeekOrigin.BeginTime <= value && value <= TimeSeekOrigin.Duration;
		}

		// Token: 0x06004A49 RID: 19017 RVA: 0x00120F98 File Offset: 0x00120398
		internal static bool IsValidPathAnimationSource(PathAnimationSource value)
		{
			return PathAnimationSource.X <= value && value <= PathAnimationSource.Angle;
		}

		// Token: 0x04001F66 RID: 8038
		private const int c_maxFillBehavior = 1;

		// Token: 0x04001F67 RID: 8039
		private const int c_maxSlipBehavior = 1;

		// Token: 0x04001F68 RID: 8040
		private const int _maxTimeSeekOrigin = 1;

		// Token: 0x04001F69 RID: 8041
		private const byte _maxPathAnimationSource = 2;
	}
}
