using System;
using System.Collections;

namespace System.Windows.Media.Imaging
{
	// Token: 0x020005F0 RID: 1520
	internal static class ImagingCache
	{
		// Token: 0x060045B0 RID: 17840 RVA: 0x0010FB50 File Offset: 0x0010EF50
		internal static void AddToImageCache(Uri uri, object obj)
		{
			ImagingCache.AddToCache(uri, obj, ImagingCache._imageCache);
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x0010FB6C File Offset: 0x0010EF6C
		internal static void RemoveFromImageCache(Uri uri)
		{
			ImagingCache.RemoveFromCache(uri, ImagingCache._imageCache);
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x0010FB84 File Offset: 0x0010EF84
		internal static object CheckImageCache(Uri uri)
		{
			return ImagingCache.CheckCache(uri, ImagingCache._imageCache);
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x0010FB9C File Offset: 0x0010EF9C
		internal static void AddToDecoderCache(Uri uri, object obj)
		{
			ImagingCache.AddToCache(uri, obj, ImagingCache._decoderCache);
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x0010FBB8 File Offset: 0x0010EFB8
		internal static void RemoveFromDecoderCache(Uri uri)
		{
			ImagingCache.RemoveFromCache(uri, ImagingCache._decoderCache);
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x0010FBD0 File Offset: 0x0010EFD0
		internal static object CheckDecoderCache(Uri uri)
		{
			return ImagingCache.CheckCache(uri, ImagingCache._decoderCache);
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x0010FBE8 File Offset: 0x0010EFE8
		private static void AddToCache(Uri uri, object obj, Hashtable table)
		{
			lock (table)
			{
				if (!table.Contains(uri))
				{
					if (table.Count == ImagingCache.MAX_CACHE_SIZE)
					{
						ArrayList arrayList = new ArrayList();
						foreach (object obj2 in table)
						{
							DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
							WeakReference weakReference = dictionaryEntry.Value as WeakReference;
							if (weakReference != null && weakReference.Target == null)
							{
								arrayList.Add(dictionaryEntry.Key);
							}
						}
						foreach (object key in arrayList)
						{
							table.Remove(key);
						}
					}
					if (table.Count != ImagingCache.MAX_CACHE_SIZE)
					{
						table[uri] = obj;
					}
				}
			}
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x0010FD28 File Offset: 0x0010F128
		private static void RemoveFromCache(Uri uri, Hashtable table)
		{
			lock (table)
			{
				if (table.Contains(uri))
				{
					table.Remove(uri);
				}
			}
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x0010FD7C File Offset: 0x0010F17C
		private static object CheckCache(Uri uri, Hashtable table)
		{
			object result;
			lock (table)
			{
				result = table[uri];
			}
			return result;
		}

		// Token: 0x04001948 RID: 6472
		private static Hashtable _imageCache = new Hashtable();

		// Token: 0x04001949 RID: 6473
		private static Hashtable _decoderCache = new Hashtable();

		// Token: 0x0400194A RID: 6474
		private static int MAX_CACHE_SIZE = 300;
	}
}
