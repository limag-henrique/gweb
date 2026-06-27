using System;
using System.Globalization;
using System.Reflection;
using MS.Internal.Ink.InkSerializedFormat;

namespace System.Windows.Ink
{
	// Token: 0x02000336 RID: 822
	internal static class KnownIds
	{
		// Token: 0x06001BEE RID: 7150 RVA: 0x00071680 File Offset: 0x00070A80
		internal static string ConvertToString(Guid id)
		{
			if (KnownIds.PublicMemberInfo == null)
			{
				KnownIds.PublicMemberInfo = typeof(KnownIds).FindMembers(MemberTypes.Field, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField, null, null);
			}
			foreach (MemberInfo memberInfo in KnownIds.PublicMemberInfo)
			{
				if (id == (Guid)typeof(KnownIds).InvokeMember(memberInfo.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField, null, null, new object[0], CultureInfo.InvariantCulture))
				{
					return memberInfo.Name;
				}
			}
			return id.ToString();
		}

		// Token: 0x04000EF1 RID: 3825
		internal static readonly Guid X = KnownIdCache.OriginalISFIdTable[0];

		// Token: 0x04000EF2 RID: 3826
		internal static readonly Guid Y = KnownIdCache.OriginalISFIdTable[1];

		// Token: 0x04000EF3 RID: 3827
		internal static readonly Guid Z = KnownIdCache.OriginalISFIdTable[2];

		// Token: 0x04000EF4 RID: 3828
		internal static readonly Guid PacketStatus = KnownIdCache.OriginalISFIdTable[3];

		// Token: 0x04000EF5 RID: 3829
		internal static readonly Guid TimerTick = KnownIdCache.OriginalISFIdTable[4];

		// Token: 0x04000EF6 RID: 3830
		internal static readonly Guid SerialNumber = KnownIdCache.OriginalISFIdTable[5];

		// Token: 0x04000EF7 RID: 3831
		internal static readonly Guid NormalPressure = KnownIdCache.OriginalISFIdTable[6];

		// Token: 0x04000EF8 RID: 3832
		internal static readonly Guid TangentPressure = KnownIdCache.OriginalISFIdTable[7];

		// Token: 0x04000EF9 RID: 3833
		internal static readonly Guid ButtonPressure = KnownIdCache.OriginalISFIdTable[8];

		// Token: 0x04000EFA RID: 3834
		internal static readonly Guid XTiltOrientation = KnownIdCache.OriginalISFIdTable[9];

		// Token: 0x04000EFB RID: 3835
		internal static readonly Guid YTiltOrientation = KnownIdCache.OriginalISFIdTable[10];

		// Token: 0x04000EFC RID: 3836
		internal static readonly Guid AzimuthOrientation = KnownIdCache.OriginalISFIdTable[11];

		// Token: 0x04000EFD RID: 3837
		internal static readonly Guid AltitudeOrientation = KnownIdCache.OriginalISFIdTable[12];

		// Token: 0x04000EFE RID: 3838
		internal static readonly Guid TwistOrientation = KnownIdCache.OriginalISFIdTable[13];

		// Token: 0x04000EFF RID: 3839
		internal static readonly Guid PitchRotation = KnownIdCache.OriginalISFIdTable[14];

		// Token: 0x04000F00 RID: 3840
		internal static readonly Guid RollRotation = KnownIdCache.OriginalISFIdTable[15];

		// Token: 0x04000F01 RID: 3841
		internal static readonly Guid YawRotation = KnownIdCache.OriginalISFIdTable[16];

		// Token: 0x04000F02 RID: 3842
		internal static readonly Guid Color = KnownIdCache.OriginalISFIdTable[18];

		// Token: 0x04000F03 RID: 3843
		internal static readonly Guid DrawingFlags = KnownIdCache.OriginalISFIdTable[22];

		// Token: 0x04000F04 RID: 3844
		internal static readonly Guid CursorId = KnownIdCache.OriginalISFIdTable[23];

		// Token: 0x04000F05 RID: 3845
		internal static readonly Guid WordAlternates = KnownIdCache.OriginalISFIdTable[24];

		// Token: 0x04000F06 RID: 3846
		internal static readonly Guid CharacterAlternates = KnownIdCache.OriginalISFIdTable[25];

		// Token: 0x04000F07 RID: 3847
		internal static readonly Guid InkMetrics = KnownIdCache.OriginalISFIdTable[26];

		// Token: 0x04000F08 RID: 3848
		internal static readonly Guid GuideStructure = KnownIdCache.OriginalISFIdTable[27];

		// Token: 0x04000F09 RID: 3849
		internal static readonly Guid Timestamp = KnownIdCache.OriginalISFIdTable[28];

		// Token: 0x04000F0A RID: 3850
		internal static readonly Guid Language = KnownIdCache.OriginalISFIdTable[29];

		// Token: 0x04000F0B RID: 3851
		internal static readonly Guid Transparency = KnownIdCache.OriginalISFIdTable[30];

		// Token: 0x04000F0C RID: 3852
		internal static readonly Guid CurveFittingError = KnownIdCache.OriginalISFIdTable[31];

		// Token: 0x04000F0D RID: 3853
		internal static readonly Guid RecognizedLattice = KnownIdCache.OriginalISFIdTable[32];

		// Token: 0x04000F0E RID: 3854
		internal static readonly Guid CursorDown = KnownIdCache.OriginalISFIdTable[33];

		// Token: 0x04000F0F RID: 3855
		internal static readonly Guid SecondaryTipSwitch = KnownIdCache.OriginalISFIdTable[34];

		// Token: 0x04000F10 RID: 3856
		internal static readonly Guid TabletPick = KnownIdCache.OriginalISFIdTable[36];

		// Token: 0x04000F11 RID: 3857
		internal static readonly Guid BarrelDown = KnownIdCache.OriginalISFIdTable[35];

		// Token: 0x04000F12 RID: 3858
		internal static readonly Guid RasterOperation = KnownIdCache.OriginalISFIdTable[37];

		// Token: 0x04000F13 RID: 3859
		internal static readonly Guid StylusHeight = KnownIdCache.OriginalISFIdTable[20];

		// Token: 0x04000F14 RID: 3860
		internal static readonly Guid StylusWidth = KnownIdCache.OriginalISFIdTable[19];

		// Token: 0x04000F15 RID: 3861
		internal static readonly Guid Highlighter = KnownIdCache.TabletInternalIdTable[0];

		// Token: 0x04000F16 RID: 3862
		internal static readonly Guid InkProperties = KnownIdCache.TabletInternalIdTable[1];

		// Token: 0x04000F17 RID: 3863
		internal static readonly Guid InkStyleBold = KnownIdCache.TabletInternalIdTable[2];

		// Token: 0x04000F18 RID: 3864
		internal static readonly Guid InkStyleItalics = KnownIdCache.TabletInternalIdTable[3];

		// Token: 0x04000F19 RID: 3865
		internal static readonly Guid StrokeTimestamp = KnownIdCache.TabletInternalIdTable[4];

		// Token: 0x04000F1A RID: 3866
		internal static readonly Guid StrokeTimeId = KnownIdCache.TabletInternalIdTable[5];

		// Token: 0x04000F1B RID: 3867
		internal static readonly Guid StylusTip = new Guid(891733809U, 61049, 18824, 185, 62, 112, 217, 47, 137, 7, 237);

		// Token: 0x04000F1C RID: 3868
		internal static readonly Guid StylusTipTransform = new Guid(1264827414, 31684, 20434, 149, 218, 172, byte.MaxValue, 71, 117, 115, 45);

		// Token: 0x04000F1D RID: 3869
		internal static readonly Guid IsHighlighter = new Guid(3459276314U, 3592, 17891, 140, 220, 228, 11, 180, 80, 111, 33);

		// Token: 0x04000F1E RID: 3870
		internal static readonly Guid PenStyle = KnownIdCache.OriginalISFIdTable[17];

		// Token: 0x04000F1F RID: 3871
		internal static readonly Guid PenTip = KnownIdCache.OriginalISFIdTable[21];

		// Token: 0x04000F20 RID: 3872
		internal static readonly Guid InkCustomStrokes = KnownIdCache.TabletInternalIdTable[7];

		// Token: 0x04000F21 RID: 3873
		internal static readonly Guid InkStrokeLattice = KnownIdCache.TabletInternalIdTable[6];

		// Token: 0x04000F22 RID: 3874
		private static MemberInfo[] PublicMemberInfo = null;
	}
}
