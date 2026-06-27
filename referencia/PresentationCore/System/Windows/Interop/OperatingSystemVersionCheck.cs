using System;

namespace System.Windows.Interop
{
	// Token: 0x02000332 RID: 818
	internal static class OperatingSystemVersionCheck
	{
		// Token: 0x06001BDA RID: 7130 RVA: 0x00070CE8 File Offset: 0x000700E8
		internal static bool IsVersionOrLater(OperatingSystemVersion version)
		{
			PlatformID platformID = PlatformID.Win32NT;
			int num;
			int num2;
			if (version <= OperatingSystemVersion.WindowsVista)
			{
				if (version != OperatingSystemVersion.WindowsXPSP2)
				{
					if (version == OperatingSystemVersion.WindowsVista)
					{
						num = 6;
						num2 = 0;
						goto IL_2D;
					}
				}
			}
			else
			{
				if (version == OperatingSystemVersion.Windows7)
				{
					num = 6;
					num2 = 1;
					goto IL_2D;
				}
				if (version == OperatingSystemVersion.Windows8)
				{
					num = 6;
					num2 = 2;
					goto IL_2D;
				}
			}
			num = 5;
			num2 = 1;
			IL_2D:
			OperatingSystem osversion = Environment.OSVersion;
			return osversion.Platform == platformID && ((osversion.Version.Major == num && osversion.Version.Minor >= num2) || osversion.Version.Major > num);
		}
	}
}
