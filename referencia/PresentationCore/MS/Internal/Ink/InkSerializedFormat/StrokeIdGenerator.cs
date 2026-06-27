using System;
using System.Windows.Ink;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007CB RID: 1995
	internal static class StrokeIdGenerator
	{
		// Token: 0x0600544C RID: 21580 RVA: 0x00159248 File Offset: 0x00158648
		internal static int[] GetStrokeIds(StrokeCollection strokes)
		{
			int[] array = new int[strokes.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i + 1;
			}
			return array;
		}
	}
}
