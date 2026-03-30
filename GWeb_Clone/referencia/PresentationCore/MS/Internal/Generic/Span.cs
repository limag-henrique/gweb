using System;

namespace MS.Internal.Generic
{
	// Token: 0x020006A7 RID: 1703
	internal struct Span<T>
	{
		// Token: 0x06004A95 RID: 19093 RVA: 0x00122CC4 File Offset: 0x001220C4
		internal Span(T value, int length)
		{
			this.Value = value;
			this.Length = length;
		}

		// Token: 0x04001F92 RID: 8082
		internal T Value;

		// Token: 0x04001F93 RID: 8083
		internal int Length;
	}
}
