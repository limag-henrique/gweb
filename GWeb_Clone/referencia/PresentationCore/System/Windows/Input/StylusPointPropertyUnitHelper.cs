using System;
using System.Collections.Generic;

namespace System.Windows.Input
{
	// Token: 0x020002C2 RID: 706
	internal static class StylusPointPropertyUnitHelper
	{
		// Token: 0x0600150B RID: 5387 RVA: 0x0004E860 File Offset: 0x0004DC60
		internal static StylusPointPropertyUnit? FromPointerUnit(uint pointerUnit)
		{
			StylusPointPropertyUnit stylusPointPropertyUnit = StylusPointPropertyUnit.None;
			StylusPointPropertyUnitHelper._pointerUnitMap.TryGetValue(pointerUnit & 15U, out stylusPointPropertyUnit);
			if (!StylusPointPropertyUnitHelper.IsDefined(stylusPointPropertyUnit))
			{
				return null;
			}
			return new StylusPointPropertyUnit?(stylusPointPropertyUnit);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0004E898 File Offset: 0x0004DC98
		internal static bool IsDefined(StylusPointPropertyUnit unit)
		{
			return unit >= StylusPointPropertyUnit.None && unit <= StylusPointPropertyUnit.Grams;
		}

		// Token: 0x04000B6C RID: 2924
		private const uint UNIT_MASK = 15U;

		// Token: 0x04000B6D RID: 2925
		private static Dictionary<uint, StylusPointPropertyUnit> _pointerUnitMap = new Dictionary<uint, StylusPointPropertyUnit>
		{
			{
				1U,
				StylusPointPropertyUnit.Centimeters
			},
			{
				2U,
				StylusPointPropertyUnit.Radians
			},
			{
				3U,
				StylusPointPropertyUnit.Inches
			},
			{
				4U,
				StylusPointPropertyUnit.Degrees
			}
		};
	}
}
