using System;
using System.IO;
using System.Windows.Input;

namespace MS.Internal.Ink.InkSerializedFormat
{
	// Token: 0x020007D1 RID: 2001
	internal class MetricEntry
	{
		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06005450 RID: 21584 RVA: 0x0015A008 File Offset: 0x00159408
		public static MetricEntryList[] MetricEntry_Optional
		{
			get
			{
				if (MetricEntry._metricEntryOptional == null)
				{
					MetricEntry._metricEntryOptional = new MetricEntryList[]
					{
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 0U, StylusPointPropertyInfoDefaults.X),
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 1U, StylusPointPropertyInfoDefaults.Y),
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 2U, StylusPointPropertyInfoDefaults.Z),
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 6U, StylusPointPropertyInfoDefaults.NormalPressure),
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 7U, StylusPointPropertyInfoDefaults.TangentPressure),
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 8U, StylusPointPropertyInfoDefaults.ButtonPressure),
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 9U, StylusPointPropertyInfoDefaults.XTiltOrientation),
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 10U, StylusPointPropertyInfoDefaults.YTiltOrientation),
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 11U, StylusPointPropertyInfoDefaults.AzimuthOrientation),
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 12U, StylusPointPropertyInfoDefaults.AltitudeOrientation),
						new MetricEntryList(KnownIdCache.KnownGuidBaseIndex + 13U, StylusPointPropertyInfoDefaults.TwistOrientation)
					};
				}
				return MetricEntry._metricEntryOptional;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06005451 RID: 21585 RVA: 0x0015A140 File Offset: 0x00159540
		// (set) Token: 0x06005452 RID: 21586 RVA: 0x0015A154 File Offset: 0x00159554
		public KnownTagCache.KnownTagIndex Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06005453 RID: 21587 RVA: 0x0015A168 File Offset: 0x00159568
		public uint Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06005454 RID: 21588 RVA: 0x0015A17C File Offset: 0x0015957C
		// (set) Token: 0x06005455 RID: 21589 RVA: 0x0015A190 File Offset: 0x00159590
		public byte[] Data
		{
			get
			{
				return this._data;
			}
			set
			{
				if (value.Length > MetricEntry.MAX_METRIC_DATA_BUFF)
				{
					this._size = (uint)MetricEntry.MAX_METRIC_DATA_BUFF;
				}
				else
				{
					this._size = (uint)value.Length;
				}
				for (int i = 0; i < (int)this._size; i++)
				{
					this._data[i] = value[i];
				}
			}
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x0015A1DC File Offset: 0x001595DC
		public bool Compare(MetricEntry metricEntry)
		{
			if (this.Tag != metricEntry.Tag)
			{
				return false;
			}
			if (this.Size != metricEntry.Size)
			{
				return false;
			}
			int num = 0;
			while ((long)num < (long)((ulong)this.Size))
			{
				if (this.Data[num] != metricEntry.Data[num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06005457 RID: 21591 RVA: 0x0015A234 File Offset: 0x00159634
		// (set) Token: 0x06005458 RID: 21592 RVA: 0x0015A248 File Offset: 0x00159648
		public MetricEntry Next
		{
			get
			{
				return this._next;
			}
			set
			{
				this._next = value;
			}
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x0015A280 File Offset: 0x00159680
		public void Add(MetricEntry next)
		{
			if (this._next == null)
			{
				this._next = next;
				return;
			}
			MetricEntry next2 = this._next;
			while (next2.Next != null)
			{
				next2 = next2.Next;
			}
			next2.Next = next;
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x0015A2BC File Offset: 0x001596BC
		public void Initialize(StylusPointPropertyInfo originalInfo, StylusPointPropertyInfo defaultInfo)
		{
			this._size = 0U;
			using (MemoryStream memoryStream = new MemoryStream(this._data))
			{
				if (!DoubleUtil.AreClose((double)originalInfo.Resolution, (double)defaultInfo.Resolution))
				{
					this._size += SerializationHelper.SignEncode(memoryStream, originalInfo.Minimum);
					this._size += SerializationHelper.SignEncode(memoryStream, originalInfo.Maximum);
					this._size += SerializationHelper.Encode(memoryStream, (uint)originalInfo.Unit);
					using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
					{
						binaryWriter.Write(originalInfo.Resolution);
						this._size += 4U;
						return;
					}
				}
				if (originalInfo.Unit != defaultInfo.Unit)
				{
					this._size += SerializationHelper.SignEncode(memoryStream, originalInfo.Minimum);
					this._size += SerializationHelper.SignEncode(memoryStream, originalInfo.Maximum);
					this._size += SerializationHelper.Encode(memoryStream, (uint)originalInfo.Unit);
				}
				else if (originalInfo.Maximum != defaultInfo.Maximum)
				{
					this._size += SerializationHelper.SignEncode(memoryStream, originalInfo.Minimum);
					this._size += SerializationHelper.SignEncode(memoryStream, originalInfo.Maximum);
				}
				else if (originalInfo.Minimum != defaultInfo.Minimum)
				{
					this._size += SerializationHelper.SignEncode(memoryStream, originalInfo.Minimum);
				}
			}
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0015A478 File Offset: 0x00159878
		public MetricEntryType CreateMetricEntry(StylusPointPropertyInfo propertyInfo, KnownTagCache.KnownTagIndex tag)
		{
			uint num = 0U;
			this.Tag = tag;
			MetricEntryType result;
			if (MetricEntry.IsValidMetricEntry(propertyInfo, this.Tag, out result, out num))
			{
				switch (result)
				{
				case MetricEntryType.Optional:
					this.Initialize(propertyInfo, MetricEntry.MetricEntry_Optional[(int)num].PropertyMetrics);
					return result;
				case MetricEntryType.Must:
				case MetricEntryType.Custom:
					this.Initialize(propertyInfo, MetricEntry.DefaultPropertyMetrics);
					return result;
				}
				throw new ArgumentException(StrokeCollectionSerializer.ISFDebugMessage("MetricEntryType was persisted with Never flag which should never happen"));
			}
			return result;
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0015A4F0 File Offset: 0x001598F0
		private static bool IsValidMetricEntry(StylusPointPropertyInfo propertyInfo, KnownTagCache.KnownTagIndex tag, out MetricEntryType metricEntryType, out uint index)
		{
			index = 0U;
			if (tag >= (KnownTagCache.KnownTagIndex)KnownIdCache.CustomGuidBaseIndex)
			{
				metricEntryType = MetricEntryType.Custom;
				return int.MinValue != propertyInfo.Minimum || int.MaxValue != propertyInfo.Maximum || propertyInfo.Unit != StylusPointPropertyUnit.None || !DoubleUtil.AreClose(1.0, (double)propertyInfo.Resolution);
			}
			int i;
			for (i = 0; i < MetricEntry.MetricEntry_Never.Length; i++)
			{
				if (MetricEntry.MetricEntry_Never[i] == tag)
				{
					metricEntryType = MetricEntryType.Never;
					return false;
				}
			}
			for (i = 0; i < MetricEntry.MetricEntry_Must.Length; i++)
			{
				if (MetricEntry.MetricEntry_Must[i] == tag)
				{
					metricEntryType = MetricEntryType.Must;
					return propertyInfo.Minimum != MetricEntry.DefaultPropertyMetrics.Minimum || propertyInfo.Maximum != MetricEntry.DefaultPropertyMetrics.Maximum || propertyInfo.Unit != MetricEntry.DefaultPropertyMetrics.Unit || !DoubleUtil.AreClose((double)propertyInfo.Resolution, (double)MetricEntry.DefaultPropertyMetrics.Resolution);
				}
			}
			i = 0;
			while (i < MetricEntry.MetricEntry_Optional.Length)
			{
				if (MetricEntry.MetricEntry_Optional[i].Tag == tag)
				{
					metricEntryType = MetricEntryType.Optional;
					if (propertyInfo.Minimum == MetricEntry.MetricEntry_Optional[i].PropertyMetrics.Minimum && propertyInfo.Maximum == MetricEntry.MetricEntry_Optional[i].PropertyMetrics.Maximum && propertyInfo.Unit == MetricEntry.MetricEntry_Optional[i].PropertyMetrics.Unit && DoubleUtil.AreClose((double)propertyInfo.Resolution, (double)MetricEntry.MetricEntry_Optional[i].PropertyMetrics.Resolution))
					{
						return false;
					}
					index = (uint)i;
					return true;
				}
				else
				{
					i++;
				}
			}
			metricEntryType = MetricEntryType.Must;
			return true;
		}

		// Token: 0x04002604 RID: 9732
		private static int MAX_METRIC_DATA_BUFF = 24;

		// Token: 0x04002605 RID: 9733
		private KnownTagCache.KnownTagIndex _tag;

		// Token: 0x04002606 RID: 9734
		private uint _size;

		// Token: 0x04002607 RID: 9735
		private MetricEntry _next;

		// Token: 0x04002608 RID: 9736
		private byte[] _data = new byte[MetricEntry.MAX_METRIC_DATA_BUFF];

		// Token: 0x04002609 RID: 9737
		private static MetricEntryList[] _metricEntryOptional;

		// Token: 0x0400260A RID: 9738
		public static StylusPointPropertyInfo DefaultXMetric = MetricEntry.MetricEntry_Optional[0].PropertyMetrics;

		// Token: 0x0400260B RID: 9739
		public static StylusPointPropertyInfo DefaultYMetric = MetricEntry.MetricEntry_Optional[1].PropertyMetrics;

		// Token: 0x0400260C RID: 9740
		private static KnownTagCache.KnownTagIndex[] MetricEntry_Must = new KnownTagCache.KnownTagIndex[]
		{
			KnownIdCache.KnownGuidBaseIndex + 14U,
			KnownIdCache.KnownGuidBaseIndex + 15U,
			KnownIdCache.KnownGuidBaseIndex + 16U
		};

		// Token: 0x0400260D RID: 9741
		private static KnownTagCache.KnownTagIndex[] MetricEntry_Never = new KnownTagCache.KnownTagIndex[]
		{
			KnownIdCache.KnownGuidBaseIndex + 3U,
			KnownIdCache.KnownGuidBaseIndex + 4U,
			KnownIdCache.KnownGuidBaseIndex + 5U
		};

		// Token: 0x0400260E RID: 9742
		private static StylusPointPropertyInfo DefaultPropertyMetrics = StylusPointPropertyInfoDefaults.DefaultValue;
	}
}
