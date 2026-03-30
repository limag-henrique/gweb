using System;
using System.Collections.Generic;

namespace System.Windows.Input
{
	// Token: 0x020002BE RID: 702
	internal static class StylusPointPropertyIds
	{
		// Token: 0x060014FC RID: 5372 RVA: 0x0004D8BC File Offset: 0x0004CCBC
		internal static Guid GetKnownGuid(StylusPointPropertyIds.HidUsagePage page, StylusPointPropertyIds.HidUsage usage)
		{
			Guid empty = Guid.Empty;
			Dictionary<StylusPointPropertyIds.HidUsage, Guid> dictionary = null;
			if (StylusPointPropertyIds._hidToGuidMap.TryGetValue(page, out dictionary))
			{
				dictionary.TryGetValue(usage, out empty);
			}
			return empty;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0004D8EC File Offset: 0x0004CCEC
		internal static bool IsKnownId(Guid guid)
		{
			return guid == StylusPointPropertyIds.X || guid == StylusPointPropertyIds.Y || guid == StylusPointPropertyIds.Z || guid == StylusPointPropertyIds.Width || guid == StylusPointPropertyIds.Height || guid == StylusPointPropertyIds.SystemTouch || guid == StylusPointPropertyIds.PacketStatus || guid == StylusPointPropertyIds.SerialNumber || guid == StylusPointPropertyIds.NormalPressure || guid == StylusPointPropertyIds.TangentPressure || guid == StylusPointPropertyIds.ButtonPressure || guid == StylusPointPropertyIds.XTiltOrientation || guid == StylusPointPropertyIds.YTiltOrientation || guid == StylusPointPropertyIds.AzimuthOrientation || guid == StylusPointPropertyIds.AltitudeOrientation || guid == StylusPointPropertyIds.TwistOrientation || guid == StylusPointPropertyIds.PitchRotation || guid == StylusPointPropertyIds.RollRotation || guid == StylusPointPropertyIds.YawRotation || guid == StylusPointPropertyIds.TipButton || guid == StylusPointPropertyIds.BarrelButton || guid == StylusPointPropertyIds.SecondaryTipButton;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0004DA44 File Offset: 0x0004CE44
		internal static string GetStringRepresentation(Guid guid)
		{
			if (guid == StylusPointPropertyIds.X)
			{
				return "X";
			}
			if (guid == StylusPointPropertyIds.Y)
			{
				return "Y";
			}
			if (guid == StylusPointPropertyIds.Z)
			{
				return "Z";
			}
			if (guid == StylusPointPropertyIds.Width)
			{
				return "Width";
			}
			if (guid == StylusPointPropertyIds.Height)
			{
				return "Height";
			}
			if (guid == StylusPointPropertyIds.SystemTouch)
			{
				return "SystemTouch";
			}
			if (guid == StylusPointPropertyIds.PacketStatus)
			{
				return "PacketStatus";
			}
			if (guid == StylusPointPropertyIds.SerialNumber)
			{
				return "SerialNumber";
			}
			if (guid == StylusPointPropertyIds.NormalPressure)
			{
				return "NormalPressure";
			}
			if (guid == StylusPointPropertyIds.TangentPressure)
			{
				return "TangentPressure";
			}
			if (guid == StylusPointPropertyIds.ButtonPressure)
			{
				return "ButtonPressure";
			}
			if (guid == StylusPointPropertyIds.XTiltOrientation)
			{
				return "XTiltOrientation";
			}
			if (guid == StylusPointPropertyIds.YTiltOrientation)
			{
				return "YTiltOrientation";
			}
			if (guid == StylusPointPropertyIds.AzimuthOrientation)
			{
				return "AzimuthOrientation";
			}
			if (guid == StylusPointPropertyIds.AltitudeOrientation)
			{
				return "AltitudeOrientation";
			}
			if (guid == StylusPointPropertyIds.TwistOrientation)
			{
				return "TwistOrientation";
			}
			if (guid == StylusPointPropertyIds.PitchRotation)
			{
				return "PitchRotation";
			}
			if (guid == StylusPointPropertyIds.RollRotation)
			{
				return "RollRotation";
			}
			if (guid == StylusPointPropertyIds.AltitudeOrientation)
			{
				return "AltitudeOrientation";
			}
			if (guid == StylusPointPropertyIds.YawRotation)
			{
				return "YawRotation";
			}
			if (guid == StylusPointPropertyIds.TipButton)
			{
				return "TipButton";
			}
			if (guid == StylusPointPropertyIds.BarrelButton)
			{
				return "BarrelButton";
			}
			if (guid == StylusPointPropertyIds.SecondaryTipButton)
			{
				return "SecondaryTipButton";
			}
			return "Unknown";
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0004DC0C File Offset: 0x0004D00C
		internal static bool IsKnownButton(Guid guid)
		{
			return guid == StylusPointPropertyIds.TipButton || guid == StylusPointPropertyIds.BarrelButton || guid == StylusPointPropertyIds.SecondaryTipButton;
		}

		// Token: 0x04000B30 RID: 2864
		public static readonly Guid X = new Guid(1502243471, 21184, 19360, 147, 175, 175, 53, 116, 17, 165, 97);

		// Token: 0x04000B31 RID: 2865
		public static readonly Guid Y = new Guid(3040845685U, 1248, 17560, 167, 238, 195, 13, 187, 90, 144, 17);

		// Token: 0x04000B32 RID: 2866
		public static readonly Guid Z = new Guid(1935334192, 3771, 18312, 160, 228, 15, 49, 100, 144, 5, 93);

		// Token: 0x04000B33 RID: 2867
		public static readonly Guid Width = new Guid(3131828557U, 10002, 18677, 190, 157, 143, 139, 94, 160, 113, 26);

		// Token: 0x04000B34 RID: 2868
		public static readonly Guid Height = new Guid(3860355282U, 58439, 16920, 157, 63, 24, 134, 92, 32, 61, 244);

		// Token: 0x04000B35 RID: 2869
		public static readonly Guid SystemTouch = new Guid(3875981316U, 22512, 20224, 138, 12, 133, 61, 87, 120, 155, 233);

		// Token: 0x04000B36 RID: 2870
		public static readonly Guid PacketStatus = new Guid(1846413247U, 45031, 19703, 135, 209, 175, 100, 70, 32, 132, 24);

		// Token: 0x04000B37 RID: 2871
		public static readonly Guid SerialNumber = new Guid(2024282966, 2357, 17555, 186, 174, 0, 84, 26, 138, 22, 196);

		// Token: 0x04000B38 RID: 2872
		public static readonly Guid NormalPressure = new Guid(1929859117U, 63988, 19992, 179, 242, 44, 225, 177, 163, 97, 12);

		// Token: 0x04000B39 RID: 2873
		public static readonly Guid TangentPressure = new Guid(1839483019, 21060, 16876, 144, 91, 50, 216, 154, 184, 8, 9);

		// Token: 0x04000B3A RID: 2874
		public static readonly Guid ButtonPressure = new Guid(2340417476U, 38570, 19454, 172, 38, 138, 95, 11, 224, 123, 245);

		// Token: 0x04000B3B RID: 2875
		public static readonly Guid XTiltOrientation = new Guid(2832235322U, 35824, 16560, 149, 169, 184, 10, 107, 183, 135, 191);

		// Token: 0x04000B3C RID: 2876
		public static readonly Guid YTiltOrientation = new Guid(244523913, 7543, 17327, 172, 0, 91, 149, 13, 109, 75, 45);

		// Token: 0x04000B3D RID: 2877
		public static readonly Guid AzimuthOrientation = new Guid(43066292U, 34856, 16651, 178, 80, 160, 83, 101, 149, 229, 220);

		// Token: 0x04000B3E RID: 2878
		public static readonly Guid AltitudeOrientation = new Guid(2195637703U, 63162, 18694, 137, 79, 102, 214, 141, 252, 69, 108);

		// Token: 0x04000B3F RID: 2879
		public static readonly Guid TwistOrientation = new Guid(221399392, 5042, 16868, 172, 230, 122, 233, 212, 61, 45, 59);

		// Token: 0x04000B40 RID: 2880
		public static readonly Guid PitchRotation = new Guid(2138986423U, 48695, 19425, 163, 86, 122, 132, 22, 14, 24, 147);

		// Token: 0x04000B41 RID: 2881
		public static readonly Guid RollRotation = new Guid(1566400086, 27561, 19547, 159, 176, 133, 28, 145, 113, 78, 86);

		// Token: 0x04000B42 RID: 2882
		public static readonly Guid YawRotation = new Guid(1787074944, 31802, 17847, 170, 130, 144, 162, 98, 149, 14, 137);

		// Token: 0x04000B43 RID: 2883
		public static readonly Guid TipButton = new Guid(59851731, 30923, 17564, 168, 231, 103, 209, 136, 100, 195, 50);

		// Token: 0x04000B44 RID: 2884
		public static readonly Guid BarrelButton = new Guid(4034003752U, 26171, 16783, 133, 166, 149, 49, 174, 62, 205, 250);

		// Token: 0x04000B45 RID: 2885
		public static readonly Guid SecondaryTipButton = new Guid(1735669634, 3813, 16794, 161, 43, 39, 58, 158, 192, 143, 61);

		// Token: 0x04000B46 RID: 2886
		private static Dictionary<StylusPointPropertyIds.HidUsagePage, Dictionary<StylusPointPropertyIds.HidUsage, Guid>> _hidToGuidMap = new Dictionary<StylusPointPropertyIds.HidUsagePage, Dictionary<StylusPointPropertyIds.HidUsage, Guid>>
		{
			{
				StylusPointPropertyIds.HidUsagePage.Generic,
				new Dictionary<StylusPointPropertyIds.HidUsage, Guid>
				{
					{
						StylusPointPropertyIds.HidUsage.TipPressure,
						StylusPointPropertyIds.X
					},
					{
						StylusPointPropertyIds.HidUsage.BarrelPressure,
						StylusPointPropertyIds.Y
					},
					{
						StylusPointPropertyIds.HidUsage.Z,
						StylusPointPropertyIds.Z
					}
				}
			},
			{
				StylusPointPropertyIds.HidUsagePage.Digitizer,
				new Dictionary<StylusPointPropertyIds.HidUsage, Guid>
				{
					{
						StylusPointPropertyIds.HidUsage.Width,
						StylusPointPropertyIds.Width
					},
					{
						StylusPointPropertyIds.HidUsage.Height,
						StylusPointPropertyIds.Height
					},
					{
						StylusPointPropertyIds.HidUsage.TouchConfidence,
						StylusPointPropertyIds.SystemTouch
					},
					{
						StylusPointPropertyIds.HidUsage.TipPressure,
						StylusPointPropertyIds.NormalPressure
					},
					{
						StylusPointPropertyIds.HidUsage.BarrelPressure,
						StylusPointPropertyIds.ButtonPressure
					},
					{
						StylusPointPropertyIds.HidUsage.XTilt,
						StylusPointPropertyIds.XTiltOrientation
					},
					{
						StylusPointPropertyIds.HidUsage.YTilt,
						StylusPointPropertyIds.YTiltOrientation
					},
					{
						StylusPointPropertyIds.HidUsage.Azimuth,
						StylusPointPropertyIds.AzimuthOrientation
					},
					{
						StylusPointPropertyIds.HidUsage.Altitude,
						StylusPointPropertyIds.AltitudeOrientation
					},
					{
						StylusPointPropertyIds.HidUsage.Twist,
						StylusPointPropertyIds.TwistOrientation
					},
					{
						StylusPointPropertyIds.HidUsage.TipSwitch,
						StylusPointPropertyIds.TipButton
					},
					{
						StylusPointPropertyIds.HidUsage.SecondaryTipSwitch,
						StylusPointPropertyIds.SecondaryTipButton
					},
					{
						StylusPointPropertyIds.HidUsage.BarrelSwitch,
						StylusPointPropertyIds.BarrelButton
					},
					{
						StylusPointPropertyIds.HidUsage.TransducerSerialNumber,
						StylusPointPropertyIds.SerialNumber
					}
				}
			}
		};

		// Token: 0x02000810 RID: 2064
		internal enum HidUsagePage
		{
			// Token: 0x0400272F RID: 10031
			Undefined,
			// Token: 0x04002730 RID: 10032
			Generic,
			// Token: 0x04002731 RID: 10033
			Simulation,
			// Token: 0x04002732 RID: 10034
			Vr,
			// Token: 0x04002733 RID: 10035
			Sport,
			// Token: 0x04002734 RID: 10036
			Game,
			// Token: 0x04002735 RID: 10037
			Keyboard = 7,
			// Token: 0x04002736 RID: 10038
			Led,
			// Token: 0x04002737 RID: 10039
			Button,
			// Token: 0x04002738 RID: 10040
			Ordinal,
			// Token: 0x04002739 RID: 10041
			Telephony,
			// Token: 0x0400273A RID: 10042
			Consumer,
			// Token: 0x0400273B RID: 10043
			Digitizer,
			// Token: 0x0400273C RID: 10044
			Unicode = 16,
			// Token: 0x0400273D RID: 10045
			Alphanumeric = 20,
			// Token: 0x0400273E RID: 10046
			BarcodeScanner = 140,
			// Token: 0x0400273F RID: 10047
			WeighingDevice,
			// Token: 0x04002740 RID: 10048
			MagneticStripeReader,
			// Token: 0x04002741 RID: 10049
			CameraControl = 144,
			// Token: 0x04002742 RID: 10050
			MicrosoftBluetoothHandsfree = 65523
		}

		// Token: 0x02000811 RID: 2065
		internal enum HidUsage
		{
			// Token: 0x04002744 RID: 10052
			TipPressure = 48,
			// Token: 0x04002745 RID: 10053
			X = 48,
			// Token: 0x04002746 RID: 10054
			BarrelPressure,
			// Token: 0x04002747 RID: 10055
			Y = 49,
			// Token: 0x04002748 RID: 10056
			Z,
			// Token: 0x04002749 RID: 10057
			XTilt = 61,
			// Token: 0x0400274A RID: 10058
			YTilt,
			// Token: 0x0400274B RID: 10059
			Azimuth,
			// Token: 0x0400274C RID: 10060
			Altitude,
			// Token: 0x0400274D RID: 10061
			Twist,
			// Token: 0x0400274E RID: 10062
			TipSwitch,
			// Token: 0x0400274F RID: 10063
			SecondaryTipSwitch,
			// Token: 0x04002750 RID: 10064
			BarrelSwitch,
			// Token: 0x04002751 RID: 10065
			TouchConfidence = 71,
			// Token: 0x04002752 RID: 10066
			Width,
			// Token: 0x04002753 RID: 10067
			Height,
			// Token: 0x04002754 RID: 10068
			TransducerSerialNumber = 91
		}
	}
}
