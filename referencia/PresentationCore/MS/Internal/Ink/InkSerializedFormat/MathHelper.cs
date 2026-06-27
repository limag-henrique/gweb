using System;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007D8 RID: 2008
	internal static class MathHelper
	{
		// Token: 0x0600548F RID: 21647 RVA: 0x0015BEAC File Offset: 0x0015B2AC
		internal static int AbsNoThrow(int data)
		{
			if (data >= 0)
			{
				return data;
			}
			return -data;
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x0015BEC4 File Offset: 0x0015B2C4
		internal static long AbsNoThrow(long data)
		{
			if (data >= 0L)
			{
				return data;
			}
			return -data;
		}
	}
}
