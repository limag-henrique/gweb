using System;
using System.Collections.Generic;

namespace System.Windows.Ink
{
	// Token: 0x0200035C RID: 860
	internal static class IEnumerablePointHelper
	{
		// Token: 0x06001D2B RID: 7467 RVA: 0x00077284 File Offset: 0x00076684
		internal static int GetCount(IEnumerable<Point> ienum)
		{
			ICollection<Point> collection = ienum as ICollection<Point>;
			if (collection != null)
			{
				return collection.Count;
			}
			int num = 0;
			foreach (Point point in ienum)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000772EC File Offset: 0x000766EC
		internal static Point[] GetPointArray(IEnumerable<Point> ienum)
		{
			Point[] array = ienum as Point[];
			if (array != null)
			{
				return array;
			}
			array = new Point[IEnumerablePointHelper.GetCount(ienum)];
			int num = 0;
			foreach (Point point in ienum)
			{
				array[num++] = point;
			}
			return array;
		}
	}
}
