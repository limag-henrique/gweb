using System;
using System.IO;
using System.Windows.Input;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007D2 RID: 2002
	internal class MetricBlock
	{
		// Token: 0x06005460 RID: 21600 RVA: 0x0015A748 File Offset: 0x00159B48
		public MetricEntry GetMetricEntryList()
		{
			return this._Entry;
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06005461 RID: 21601 RVA: 0x0015A75C File Offset: 0x00159B5C
		public uint MetricEntryCount
		{
			get
			{
				return this._Count;
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06005462 RID: 21602 RVA: 0x0015A770 File Offset: 0x00159B70
		public uint Size
		{
			get
			{
				return this._size + SerializationHelper.VarSize(this._size);
			}
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x0015A790 File Offset: 0x00159B90
		public void AddMetricEntry(MetricEntry newEntry)
		{
			if (newEntry == null)
			{
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("MetricEntry cannot be null"));
			}
			if (this._Entry == null)
			{
				this._Entry = newEntry;
			}
			else
			{
				this._Entry.Add(newEntry);
			}
			this._Count += 1U;
			this._size += newEntry.Size + SerializationHelper.VarSize(newEntry.Size) + SerializationHelper.VarSize((uint)newEntry.Tag);
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x0015A808 File Offset: 0x00159C08
		public MetricEntryType AddMetricEntry(StylusPointPropertyInfo property, KnownTagCache.KnownTagIndex tag)
		{
			MetricEntry metricEntry = new MetricEntry();
			MetricEntryType result = metricEntry.CreateMetricEntry(property, tag);
			if (metricEntry.Size == 0U)
			{
				return result;
			}
			MetricEntry metricEntry2 = this._Entry;
			if (metricEntry2 == null)
			{
				this._Entry = metricEntry;
			}
			else
			{
				while (metricEntry2.Next != null)
				{
					metricEntry2 = metricEntry2.Next;
				}
				metricEntry2.Next = metricEntry;
			}
			this._Count += 1U;
			this._size += metricEntry.Size + SerializationHelper.VarSize(metricEntry.Size) + SerializationHelper.VarSize((uint)this._Entry.Tag);
			return result;
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x0015A898 File Offset: 0x00159C98
		public uint Pack(Stream strm)
		{
			uint num = SerializationHelper.Encode(strm, this._size);
			for (MetricEntry metricEntry = this._Entry; metricEntry != null; metricEntry = metricEntry.Next)
			{
				num += SerializationHelper.Encode(strm, (uint)metricEntry.Tag);
				num += SerializationHelper.Encode(strm, metricEntry.Size);
				strm.Write(metricEntry.Data, 0, (int)metricEntry.Size);
				num += metricEntry.Size;
			}
			return num;
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x0015A904 File Offset: 0x00159D04
		public bool CompareMetricBlock(MetricBlock metricColl, ref SetType setType)
		{
			if (metricColl == null)
			{
				return false;
			}
			if (this.GetMetricEntryList() == null)
			{
				return metricColl.GetMetricEntryList() == null;
			}
			if (metricColl.GetMetricEntryList() == null)
			{
				return false;
			}
			uint metricEntryCount = this.MetricEntryCount;
			uint metricEntryCount2 = metricColl.MetricEntryCount;
			MetricEntry metricEntry;
			MetricEntry metricEntryList;
			if (metricColl.MetricEntryCount <= this.MetricEntryCount)
			{
				metricEntry = metricColl.GetMetricEntryList();
				metricEntryList = this.GetMetricEntryList();
			}
			else
			{
				metricEntryList = metricColl.GetMetricEntryList();
				metricEntry = this.GetMetricEntryList();
				setType = SetType.SuperSet;
			}
			while (metricEntry != null)
			{
				bool flag = false;
				for (MetricEntry metricEntry2 = metricEntryList; metricEntry2 != null; metricEntry2 = metricEntry2.Next)
				{
					if (metricEntry.Compare(metricEntry2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
				metricEntry = metricEntry.Next;
			}
			return true;
		}

		// Token: 0x0400260F RID: 9743
		private MetricEntry _Entry;

		// Token: 0x04002610 RID: 9744
		private uint _Count;

		// Token: 0x04002611 RID: 9745
		private uint _size;
	}
}
