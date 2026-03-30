using System;
using System.Collections.Generic;
using System.IO;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007C9 RID: 1993
	internal class GuidList
	{
		// Token: 0x0600541D RID: 21533 RVA: 0x001564B4 File Offset: 0x001558B4
		public bool Add(Guid guid)
		{
			if (this.FindTag(guid, true) == KnownTagCache.KnownTagIndex.Unknown)
			{
				this._CustomGuids.Add(guid);
				return true;
			}
			return false;
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x001564DC File Offset: 0x001558DC
		public static KnownTagCache.KnownTagIndex FindKnownTag(Guid guid)
		{
			byte b = 0;
			while ((int)b < KnownIdCache.OriginalISFIdTable.Length)
			{
				if (guid == KnownIdCache.OriginalISFIdTable[(int)b])
				{
					return KnownIdCache.KnownGuidBaseIndex + (uint)b;
				}
				b += 1;
			}
			return KnownTagCache.KnownTagIndex.Unknown;
		}

		// Token: 0x0600541F RID: 21535 RVA: 0x00156518 File Offset: 0x00155918
		private KnownTagCache.KnownTagIndex FindCustomTag(Guid guid)
		{
			for (int i = 0; i < this._CustomGuids.Count; i++)
			{
				if (guid.Equals(this._CustomGuids[i]))
				{
					return (KnownTagCache.KnownTagIndex)((ulong)KnownIdCache.CustomGuidBaseIndex + (ulong)((long)i));
				}
			}
			return KnownTagCache.KnownTagIndex.Unknown;
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x0015655C File Offset: 0x0015595C
		public KnownTagCache.KnownTagIndex FindTag(Guid guid, bool bFindInKnownListFirst)
		{
			KnownTagCache.KnownTagIndex knownTagIndex;
			if (bFindInKnownListFirst)
			{
				knownTagIndex = GuidList.FindKnownTag(guid);
				if (knownTagIndex == KnownTagCache.KnownTagIndex.Unknown)
				{
					knownTagIndex = this.FindCustomTag(guid);
				}
			}
			else
			{
				knownTagIndex = this.FindCustomTag(guid);
				if (knownTagIndex == KnownTagCache.KnownTagIndex.Unknown)
				{
					knownTagIndex = GuidList.FindKnownTag(guid);
				}
			}
			return knownTagIndex;
		}

		// Token: 0x06005421 RID: 21537 RVA: 0x00156598 File Offset: 0x00155998
		private static Guid FindKnownGuid(KnownTagCache.KnownTagIndex tag)
		{
			if (tag < KnownIdCache.KnownGuidBaseIndex)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Tag is outside of the known guid tag range"));
			}
			uint num = tag - KnownIdCache.KnownGuidBaseIndex;
			if ((long)KnownIdCache.OriginalISFIdTable.Length <= (long)((ulong)num))
			{
				return Guid.Empty;
			}
			return KnownIdCache.OriginalISFIdTable[(int)num];
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x001565E4 File Offset: 0x001559E4
		private Guid FindCustomGuid(KnownTagCache.KnownTagIndex tag)
		{
			if (tag < (KnownTagCache.KnownTagIndex)KnownIdCache.CustomGuidBaseIndex)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("Tag is outside of the known guid tag range"));
			}
			int num = (int)(tag - (KnownTagCache.KnownTagIndex)KnownIdCache.CustomGuidBaseIndex);
			if (0 > num || this._CustomGuids.Count <= num)
			{
				return Guid.Empty;
			}
			return this._CustomGuids[num];
		}

		// Token: 0x06005423 RID: 21539 RVA: 0x00156638 File Offset: 0x00155A38
		public Guid FindGuid(KnownTagCache.KnownTagIndex tag)
		{
			if (tag < (KnownTagCache.KnownTagIndex)KnownIdCache.CustomGuidBaseIndex)
			{
				Guid guid = GuidList.FindKnownGuid(tag);
				if (Guid.Empty != guid)
				{
					return guid;
				}
				return this.FindCustomGuid(tag);
			}
			else
			{
				Guid guid2 = this.FindCustomGuid(tag);
				if (Guid.Empty != guid2)
				{
					return guid2;
				}
				return GuidList.FindKnownGuid(tag);
			}
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x00156688 File Offset: 0x00155A88
		public static uint GetDataSizeIfKnownGuid(Guid guid)
		{
			uint num = 0U;
			while ((ulong)num < (ulong)((long)KnownIdCache.OriginalISFIdTable.Length))
			{
				if (guid == KnownIdCache.OriginalISFIdTable[(int)num])
				{
					return KnownIdCache.OriginalISFIdPersistenceSize[(int)num];
				}
				num += 1U;
			}
			return 0U;
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x001566C8 File Offset: 0x00155AC8
		public uint Save(Stream stream)
		{
			uint num = (uint)((long)this._CustomGuids.Count * (long)((ulong)Native.SizeOfGuid));
			if (num == 0U)
			{
				return 0U;
			}
			if (stream == null)
			{
				return num + SerializationHelper.VarSize(num) + SerializationHelper.VarSize(1U);
			}
			uint num2 = SerializationHelper.Encode(stream, 1U);
			num2 += SerializationHelper.Encode(stream, num);
			for (int i = 0; i < this._CustomGuids.Count; i++)
			{
				stream.Write(this._CustomGuids[i].ToByteArray(), 0, (int)Native.SizeOfGuid);
			}
			return num2 + num;
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x00156750 File Offset: 0x00155B50
		public uint Load(Stream strm, uint size)
		{
			uint num = 0U;
			this._CustomGuids.Clear();
			uint num2 = size / Native.SizeOfGuid;
			byte[] array = new byte[Native.SizeOfGuid];
			for (uint num3 = 0U; num3 < num2; num3 += 1U)
			{
				uint num4 = StrokeCollectionSerializer.ReliableRead(strm, array, Native.SizeOfGuid);
				num += num4;
				if (num4 != Native.SizeOfGuid)
				{
					break;
				}
				this._CustomGuids.Add(new Guid(array));
			}
			return num;
		}

		// Token: 0x040025E1 RID: 9697
		private List<Guid> _CustomGuids = new List<Guid>();
	}
}
