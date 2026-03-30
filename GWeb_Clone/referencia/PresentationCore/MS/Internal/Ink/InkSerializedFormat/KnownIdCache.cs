using System;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007CC RID: 1996
	internal static class KnownIdCache
	{
		// Token: 0x040025F1 RID: 9713
		public static Guid[] OriginalISFIdTable = new Guid[]
		{
			new Guid(1502243471, 21184, 19360, 147, 175, 175, 53, 116, 17, 165, 97),
			new Guid(3040845685U, 1248, 17560, 167, 238, 195, 13, 187, 90, 144, 17),
			new Guid(1935334192, 3771, 18312, 160, 228, 15, 49, 100, 144, 5, 93),
			new Guid(1846413247U, 45031, 19703, 135, 209, 175, 100, 70, 32, 132, 24),
			new Guid(1130696901U, 65235, 17873, 139, 118, 113, 211, 234, 122, 130, 157),
			new Guid(2024282966, 2357, 17555, 186, 174, 0, 84, 26, 138, 22, 196),
			new Guid(1929859117U, 63988, 19992, 179, 242, 44, 225, 177, 163, 97, 12),
			new Guid(1839483019, 21060, 16876, 144, 91, 50, 216, 154, 184, 8, 9),
			new Guid(2340417476U, 38570, 19454, 172, 38, 138, 95, 11, 224, 123, 245),
			new Guid(2832235322U, 35824, 16560, 149, 169, 184, 10, 107, 183, 135, 191),
			new Guid(244523913, 7543, 17327, 172, 0, 91, 149, 13, 109, 75, 45),
			new Guid(43066292U, 34856, 16651, 178, 80, 160, 83, 101, 149, 229, 220),
			new Guid(2195637703U, 63162, 18694, 137, 79, 102, 214, 141, 252, 69, 108),
			new Guid(221399392, 5042, 16868, 172, 230, 122, 233, 212, 61, 45, 59),
			new Guid(2138986423U, 48695, 19425, 163, 86, 122, 132, 22, 14, 24, 147),
			new Guid(1566400086, 27561, 19547, 159, 176, 133, 28, 145, 113, 78, 86),
			new Guid(1787074944, 31802, 17847, 170, 130, 144, 162, 98, 149, 14, 137),
			new Guid(868343683U, 60635, 17648, 185, 35, 219, 209, 165, 178, 19, 110),
			new Guid(1395248549U, 64091, 20178, 187, 50, 131, 70, 1, 114, 68, 40),
			new Guid(3013039U, 56716, 18761, 186, 70, 214, 94, 16, 125, 26, 138),
			new Guid(2637346762U, 4627, 20308, 183, 228, 201, 5, 14, 225, 122, 56),
			new Guid(3877415609U, 32857, 19469, 162, 219, 124, 121, 84, 71, 141, 130),
			new Guid(1544254218U, 62356, 18785, 169, 51, 55, 196, 52, 244, 183, 235),
			new Guid(672276751U, 34590, 19857, 134, 7, 73, 50, 125, 223, 10, 159),
			new Guid(2203689210U, 12100, 19942, 146, 129, 206, 90, 137, 156, 245, 143),
			new Guid(1279673053, 18334, 19558, 180, 64, 31, 205, 131, 149, 143, 0),
			new Guid(3459095178U, 58766, 16570, 147, 250, 24, 155, 179, 144, 0, 174),
			new Guid(3284617231U, 22585, 18159, 165, 102, 216, 72, 28, 122, 254, 193),
			new Guid(3928127663U, 50589, 20212, 152, 91, 212, 190, 18, 223, 34, 52),
			new Guid(3093499337U, 52316, 19507, 141, 173, 180, 127, 98, 43, 140, 121),
			new Guid(367196390, 25473, 20107, 169, 101, 1, 31, 125, 127, 202, 56),
			new Guid(1885797348, 18238, 18037, 156, 37, 0, 38, 130, 155, 64, 31),
			new Guid(3150470042U, 44518, 16531, 179, 187, 100, 31, 161, 211, 122, 26),
			new Guid(59851731, 30923, 17564, 168, 231, 103, 209, 136, 100, 195, 50),
			new Guid(1735669634, 3813, 16794, 161, 43, 39, 58, 158, 192, 143, 61),
			new Guid(4034003752U, 26171, 16783, 133, 166, 149, 49, 174, 62, 205, 250),
			new Guid(2708573405U, 3500, 16533, 161, 129, 123, 89, 203, 16, 107, 251),
			new Guid(2164946130U, 28386, 20025, 130, 94, 109, 239, 130, 106, byte.MaxValue, 197)
		};

		// Token: 0x040025F2 RID: 9714
		public static uint[] OriginalISFIdPersistenceSize = new uint[]
		{
			Native.SizeOfInt,
			Native.SizeOfInt,
			Native.SizeOfInt,
			Native.SizeOfInt,
			2U * Native.SizeOfUInt,
			Native.SizeOfUInt,
			Native.SizeOfUShort,
			Native.SizeOfUShort,
			Native.SizeOfUShort,
			Native.SizeOfFloat,
			Native.SizeOfFloat,
			Native.SizeOfFloat,
			Native.SizeOfInt,
			Native.SizeOfInt,
			Native.SizeOfUShort,
			Native.SizeOfUShort,
			Native.SizeOfUShort,
			Native.SizeOfUShort,
			Native.SizeOfUInt,
			Native.SizeOfUInt,
			Native.SizeOfUInt,
			Native.SizeOfByte,
			Native.SizeOfUInt,
			Native.SizeOfUInt,
			0U,
			0U,
			5U * Native.SizeOfUInt,
			3U * Native.SizeOfUInt,
			8U * Native.SizeOfUShort,
			Native.SizeOfUShort,
			Native.SizeOfByte,
			Native.SizeOfUInt,
			0U,
			Native.SizeOfInt,
			Native.SizeOfInt,
			Native.SizeOfInt,
			Native.SizeOfInt,
			Native.SizeOfInt
		};

		// Token: 0x040025F3 RID: 9715
		public static Guid[] TabletInternalIdTable = new Guid[]
		{
			new Guid(2606917560U, 14696, 16456, 171, 116, 244, 144, 64, 106, 45, 250),
			new Guid(2143489681U, 54925, 20231, 139, 98, 6, 246, 210, 115, 27, 237),
			new Guid(3761223105U, 38547, 17170, 164, 52, 0, 222, 127, 58, 212, 147),
			new Guid(86326097, 18886, 18948, 137, 147, 100, 221, 154, 189, 132, 42),
			new Guid(82470596U, 62266, 17947, 184, 254, 104, 7, 13, 156, 117, 117),
			new Guid(84634568, 15229, 18454, 140, 97, 188, 126, 144, 91, 33, 50),
			new Guid(2189892741U, 57927, 19852, 141, 113, 34, 229, 214, 242, 87, 118),
			new Guid(869120947, 22671, 20116, 177, 254, 93, 121, byte.MaxValue, 231, 110, 118)
		};

		// Token: 0x040025F4 RID: 9716
		internal static KnownTagCache.KnownTagIndex KnownGuidBaseIndex = (KnownTagCache.KnownTagIndex)KnownTagCache.MaximumPossibleKnownTags;

		// Token: 0x040025F5 RID: 9717
		internal static uint MaximumPossibleKnownGuidIndex = 100U;

		// Token: 0x040025F6 RID: 9718
		internal static uint CustomGuidBaseIndex = KnownIdCache.MaximumPossibleKnownGuidIndex;

		// Token: 0x040025F7 RID: 9719
		public static Guid[] ExtendedISFIdTable = new Guid[]
		{
			new Guid(2606917560U, 14696, 16456, 171, 116, 244, 144, 64, 106, 45, 250),
			new Guid(2143489681U, 54925, 20231, 139, 98, 6, 246, 210, 115, 27, 237),
			new Guid(3761223105U, 38547, 17170, 164, 52, 0, 222, 127, 58, 212, 147),
			new Guid(86326097, 18886, 18948, 137, 147, 100, 221, 154, 189, 132, 42),
			new Guid(82470596U, 62266, 17947, 184, 254, 104, 7, 13, 156, 117, 117),
			new Guid(84634568, 15229, 18454, 140, 97, 188, 126, 144, 91, 33, 50),
			new Guid(2189892741U, 57927, 19852, 141, 113, 34, 229, 214, 242, 87, 118),
			new Guid(869120947, 22671, 20116, 177, 254, 93, 121, byte.MaxValue, 231, 110, 118)
		};
	}
}
