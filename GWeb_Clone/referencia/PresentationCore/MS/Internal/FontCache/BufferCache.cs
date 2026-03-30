using System;
using System.Threading;
using MS.Internal.Text.TextInterface;

namespace MS.Internal.FontCache
{
	// Token: 0x0200076F RID: 1903
	internal static class BufferCache
	{
		// Token: 0x06005039 RID: 20537 RVA: 0x00140F5C File Offset: 0x0014035C
		internal static void Reset()
		{
			if (Interlocked.Increment(ref BufferCache._mutex) == 1L)
			{
				BufferCache._buffers = null;
			}
			Interlocked.Decrement(ref BufferCache._mutex);
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x00140F88 File Offset: 0x00140388
		internal static GlyphMetrics[] GetGlyphMetrics(int length)
		{
			GlyphMetrics[] array = (GlyphMetrics[])BufferCache.GetBuffer(length, 0);
			if (array == null)
			{
				array = new GlyphMetrics[length];
			}
			return array;
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x00140FB0 File Offset: 0x001403B0
		internal static void ReleaseGlyphMetrics(GlyphMetrics[] glyphMetrics)
		{
			BufferCache.ReleaseBuffer(glyphMetrics, 0);
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x00140FC4 File Offset: 0x001403C4
		internal static ushort[] GetUShorts(int length)
		{
			ushort[] array = (ushort[])BufferCache.GetBuffer(length, 2);
			if (array == null)
			{
				array = new ushort[length];
			}
			return array;
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x00140FEC File Offset: 0x001403EC
		internal static void ReleaseUShorts(ushort[] ushorts)
		{
			BufferCache.ReleaseBuffer(ushorts, 2);
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x00141000 File Offset: 0x00140400
		internal static uint[] GetUInts(int length)
		{
			uint[] array = (uint[])BufferCache.GetBuffer(length, 1);
			if (array == null)
			{
				array = new uint[length];
			}
			return array;
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x00141028 File Offset: 0x00140428
		internal static void ReleaseUInts(uint[] uints)
		{
			BufferCache.ReleaseBuffer(uints, 1);
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x0014103C File Offset: 0x0014043C
		private static Array GetBuffer(int length, int index)
		{
			Array result = null;
			if (Interlocked.Increment(ref BufferCache._mutex) == 1L && BufferCache._buffers != null && BufferCache._buffers[index] != null && length <= BufferCache._buffers[index].Length)
			{
				result = BufferCache._buffers[index];
				BufferCache._buffers[index] = null;
			}
			Interlocked.Decrement(ref BufferCache._mutex);
			return result;
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x00141094 File Offset: 0x00140494
		private static void ReleaseBuffer(Array buffer, int index)
		{
			if (buffer != null)
			{
				if (Interlocked.Increment(ref BufferCache._mutex) == 1L)
				{
					if (BufferCache._buffers == null)
					{
						BufferCache._buffers = new Array[3];
					}
					if (BufferCache._buffers[index] == null || (BufferCache._buffers[index].Length < buffer.Length && buffer.Length <= 1024))
					{
						BufferCache._buffers[index] = buffer;
					}
				}
				Interlocked.Decrement(ref BufferCache._mutex);
			}
		}

		// Token: 0x0400247B RID: 9339
		private const int MaxBufferLength = 1024;

		// Token: 0x0400247C RID: 9340
		private const int GlyphMetricsIndex = 0;

		// Token: 0x0400247D RID: 9341
		private const int UIntsIndex = 1;

		// Token: 0x0400247E RID: 9342
		private const int UShortsIndex = 2;

		// Token: 0x0400247F RID: 9343
		private const int BuffersLength = 3;

		// Token: 0x04002480 RID: 9344
		private static long _mutex;

		// Token: 0x04002481 RID: 9345
		private static Array[] _buffers;
	}
}
