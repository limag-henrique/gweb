using System;
using System.Collections.Generic;
using MS.Win32.Pointer;

namespace System.Windows.Input.StylusPointer
{
	// Token: 0x020002EF RID: 751
	internal class PointerStylusPointPropertyInfoHelper
	{
		// Token: 0x060017C9 RID: 6089 RVA: 0x0005F424 File Offset: 0x0005E824
		internal static StylusPointPropertyInfo CreatePropertyInfo(UnsafeNativeMethods.POINTER_DEVICE_PROPERTY prop)
		{
			StylusPointPropertyInfo result = null;
			Guid knownGuid = StylusPointPropertyIds.GetKnownGuid((StylusPointPropertyIds.HidUsagePage)prop.usagePageId, (StylusPointPropertyIds.HidUsage)prop.usageId);
			if (knownGuid != Guid.Empty)
			{
				StylusPointProperty stylusPointProperty = new StylusPointProperty(knownGuid, StylusPointPropertyIds.IsKnownButton(knownGuid));
				StylusPointPropertyUnit? stylusPointPropertyUnit = StylusPointPropertyUnitHelper.FromPointerUnit(prop.unit);
				if (stylusPointPropertyUnit == null)
				{
					stylusPointPropertyUnit = new StylusPointPropertyUnit?(StylusPointPropertyInfoDefaults.GetStylusPointPropertyInfoDefault(stylusPointProperty).Unit);
				}
				float resolution = StylusPointPropertyInfoDefaults.GetStylusPointPropertyInfoDefault(stylusPointProperty).Resolution;
				short num = 0;
				if (PointerStylusPointPropertyInfoHelper._hidExponentMap.TryGetValue((byte)(prop.unitExponent & 15U), out num))
				{
					float num2 = (float)Math.Pow(10.0, (double)num);
					if (prop.physicalMax - prop.physicalMin > 0)
					{
						resolution = (float)(prop.logicalMax - prop.logicalMin) / ((float)(prop.physicalMax - prop.physicalMin) * num2);
					}
				}
				result = new StylusPointPropertyInfo(stylusPointProperty, prop.logicalMin, prop.logicalMax, stylusPointPropertyUnit.Value, resolution);
			}
			return result;
		}

		// Token: 0x04000D07 RID: 3335
		private const byte HidExponentMask = 15;

		// Token: 0x04000D08 RID: 3336
		private static Dictionary<byte, short> _hidExponentMap = new Dictionary<byte, short>
		{
			{
				5,
				5
			},
			{
				6,
				6
			},
			{
				7,
				7
			},
			{
				8,
				-8
			},
			{
				9,
				-7
			},
			{
				10,
				-6
			},
			{
				11,
				-5
			},
			{
				12,
				-4
			},
			{
				13,
				-3
			},
			{
				14,
				-2
			},
			{
				15,
				-1
			}
		};
	}
}
