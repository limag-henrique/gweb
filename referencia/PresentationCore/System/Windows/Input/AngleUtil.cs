using System;

namespace System.Windows.Input
{
	// Token: 0x0200022C RID: 556
	internal static class AngleUtil
	{
		// Token: 0x06000F70 RID: 3952 RVA: 0x0003AF04 File Offset: 0x0003A304
		public static double DegreesToRadians(double degrees)
		{
			return degrees * 3.1415926535897931 / 180.0;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0003AF28 File Offset: 0x0003A328
		public static double RadiansToDegrees(double radians)
		{
			return radians * 180.0 / 3.1415926535897931;
		}
	}
}
