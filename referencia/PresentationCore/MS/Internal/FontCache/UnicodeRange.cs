using System;

namespace MS.Internal.FontCache
{
	// Token: 0x02000776 RID: 1910
	internal struct UnicodeRange
	{
		// Token: 0x06005083 RID: 20611 RVA: 0x0014264C File Offset: 0x00141A4C
		internal UnicodeRange(int first, int last)
		{
			this.firstChar = first;
			this.lastChar = last;
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x00142668 File Offset: 0x00141A68
		internal uint[] GetFullRange()
		{
			int num = Math.Min(this.lastChar, this.firstChar);
			int num2 = Math.Max(this.lastChar, this.firstChar);
			int num3 = num2 - num + 1;
			uint[] array = new uint[num3];
			for (int i = 0; i < num3; i++)
			{
				array[i] = checked((uint)(num + i));
			}
			return array;
		}

		// Token: 0x040024AD RID: 9389
		internal int firstChar;

		// Token: 0x040024AE RID: 9390
		internal int lastChar;
	}
}
