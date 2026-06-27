using System;
using System.Collections;

namespace MS.Internal.FontCache
{
	// Token: 0x02000783 RID: 1923
	internal static class TypefaceMetricsCache
	{
		// Token: 0x060050E3 RID: 20707 RVA: 0x00144284 File Offset: 0x00143684
		internal static object ReadonlyLookup(object key)
		{
			return TypefaceMetricsCache._hashTable[key];
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x0014429C File Offset: 0x0014369C
		internal static void Add(object key, object value)
		{
			object @lock = TypefaceMetricsCache._lock;
			lock (@lock)
			{
				if (TypefaceMetricsCache._hashTable.Count >= 64)
				{
					TypefaceMetricsCache._hashTable = new Hashtable(64);
				}
				TypefaceMetricsCache._hashTable[key] = value;
			}
		}

		// Token: 0x040024DE RID: 9438
		private static Hashtable _hashTable = new Hashtable(64);

		// Token: 0x040024DF RID: 9439
		private static object _lock = new object();

		// Token: 0x040024E0 RID: 9440
		private const int MaxCacheCapacity = 64;
	}
}
