using System;
using System.IO;
using System.Security;

namespace MS.Internal.Shaping
{
	// Token: 0x020006D2 RID: 1746
	internal class OpenTypeLayoutWorkspace
	{
		// Token: 0x06004B94 RID: 19348 RVA: 0x0012754C File Offset: 0x0012694C
		[SecurityTreatAsSafe]
		[SecurityCritical]
		internal OpenTypeLayoutWorkspace()
		{
			this._bytesPerLookup = 0;
			this._lookupUsageFlags = null;
			this._cachePointers = null;
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x00127574 File Offset: 0x00126974
		internal OpenTypeLayoutResult Init(IOpenTypeFont font, OpenTypeTags tableTag, uint scriptTag, uint langSysTag)
		{
			return OpenTypeLayoutResult.Success;
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x00127584 File Offset: 0x00126984
		public void InitLookupUsageFlags(int lookupCount, int featureCount)
		{
			this._bytesPerLookup = featureCount + 2 + 7 >> 3;
			int num = lookupCount * this._bytesPerLookup;
			if (this._lookupUsageFlags == null || this._lookupUsageFlags.Length < num)
			{
				this._lookupUsageFlags = new byte[num];
			}
			Array.Clear(this._lookupUsageFlags, 0, num);
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x001275D4 File Offset: 0x001269D4
		public bool IsAggregatedFlagSet(int lookupIndex)
		{
			return (this._lookupUsageFlags[lookupIndex * this._bytesPerLookup] & 1) > 0;
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x001275F8 File Offset: 0x001269F8
		public bool IsFeatureFlagSet(int lookupIndex, int featureIndex)
		{
			int num = featureIndex + 2;
			int num2 = lookupIndex * this._bytesPerLookup + (num >> 3);
			byte b = (byte)(1 << num % 8);
			return (this._lookupUsageFlags[num2] & b) > 0;
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x00127630 File Offset: 0x00126A30
		public bool IsRequiredFeatureFlagSet(int lookupIndex)
		{
			return (this._lookupUsageFlags[lookupIndex * this._bytesPerLookup] & 2) > 0;
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x00127654 File Offset: 0x00126A54
		public void SetFeatureFlag(int lookupIndex, int featureIndex)
		{
			int num = lookupIndex * this._bytesPerLookup;
			int num2 = featureIndex + 2;
			int num3 = num + (num2 >> 3);
			byte b = (byte)(1 << num2 % 8);
			if (num3 >= this._lookupUsageFlags.Length)
			{
				throw new FileFormatException();
			}
			byte[] lookupUsageFlags = this._lookupUsageFlags;
			int num4 = num3;
			lookupUsageFlags[num4] |= b;
			byte[] lookupUsageFlags2 = this._lookupUsageFlags;
			int num5 = num;
			lookupUsageFlags2[num5] |= 1;
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x001276B4 File Offset: 0x00126AB4
		public void SetRequiredFeatureFlag(int lookupIndex)
		{
			int num = lookupIndex * this._bytesPerLookup;
			if (num >= this._lookupUsageFlags.Length)
			{
				throw new FileFormatException();
			}
			byte[] lookupUsageFlags = this._lookupUsageFlags;
			int num2 = num;
			lookupUsageFlags[num2] |= 3;
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x001276F0 File Offset: 0x00126AF0
		[SecurityCritical]
		[SecurityTreatAsSafe]
		public void AllocateCachePointers(int glyphRunLength)
		{
			if (this._cachePointers != null && this._cachePointers.Length >= glyphRunLength)
			{
				return;
			}
			this._cachePointers = new ushort[glyphRunLength];
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x00127720 File Offset: 0x00126B20
		[SecurityTreatAsSafe]
		[SecurityCritical]
		public void UpdateCachePointers(int oldLength, int newLength, int firstGlyphChanged, int afterLastGlyphChanged)
		{
			if (oldLength != newLength)
			{
				int num = afterLastGlyphChanged - (newLength - oldLength);
				if (this._cachePointers.Length < newLength)
				{
					ushort[] array = new ushort[newLength];
					Array.Copy(this._cachePointers, array, firstGlyphChanged);
					Array.Copy(this._cachePointers, num, array, afterLastGlyphChanged, oldLength - num);
					this._cachePointers = array;
					return;
				}
				Array.Copy(this._cachePointers, num, this._cachePointers, afterLastGlyphChanged, oldLength - num);
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06004B9E RID: 19358 RVA: 0x00127788 File Offset: 0x00126B88
		public ushort[] CachePointers
		{
			[SecurityCritical]
			get
			{
				return this._cachePointers;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06004B9F RID: 19359 RVA: 0x0012779C File Offset: 0x00126B9C
		// (set) Token: 0x06004BA0 RID: 19360 RVA: 0x001277B0 File Offset: 0x00126BB0
		public byte[] TableCacheData
		{
			[SecurityCritical]
			get
			{
				return this._tableCache;
			}
			[SecurityCritical]
			set
			{
				this._tableCache = value;
			}
		}

		// Token: 0x040020A2 RID: 8354
		private const byte AggregatedFlagMask = 1;

		// Token: 0x040020A3 RID: 8355
		private const byte RequiredFeatureFlagMask = 2;

		// Token: 0x040020A4 RID: 8356
		private const int FeatureFlagsStartBit = 2;

		// Token: 0x040020A5 RID: 8357
		private int _bytesPerLookup;

		// Token: 0x040020A6 RID: 8358
		private byte[] _lookupUsageFlags;

		// Token: 0x040020A7 RID: 8359
		[SecurityCritical]
		private ushort[] _cachePointers;

		// Token: 0x040020A8 RID: 8360
		[SecurityCritical]
		private byte[] _tableCache;
	}
}
