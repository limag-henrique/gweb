using System;

namespace MS.Internal.Shaping
{
	// Token: 0x020006E3 RID: 1763
	internal class ShaperFeaturesList
	{
		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06004C1F RID: 19487 RVA: 0x0012A05C File Offset: 0x0012945C
		public int FeaturesCount
		{
			get
			{
				return (int)this._featuresCount;
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06004C20 RID: 19488 RVA: 0x0012A070 File Offset: 0x00129470
		public Feature[] Features
		{
			get
			{
				return this._features;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06004C21 RID: 19489 RVA: 0x0012A084 File Offset: 0x00129484
		public int NextIx
		{
			get
			{
				return (int)this._featuresCount;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06004C22 RID: 19490 RVA: 0x0012A098 File Offset: 0x00129498
		public uint CurrentTag
		{
			get
			{
				if (this._featuresCount != 0)
				{
					return this._features[(int)(this._featuresCount - 1)].Tag;
				}
				return 0U;
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06004C23 RID: 19491 RVA: 0x0012A0C4 File Offset: 0x001294C4
		public int Length
		{
			get
			{
				return (int)this._featuresCount;
			}
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x0012A0D8 File Offset: 0x001294D8
		public void SetFeatureParameter(ushort featureIx, uint paramValue)
		{
			Invariant.Assert(this._featuresCount > featureIx);
			this._features[(int)featureIx].Parameter = paramValue;
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0012A104 File Offset: 0x00129504
		internal bool Initialize(ushort newSize)
		{
			if (this._features == null || (int)newSize > this._features.Length || newSize == 0)
			{
				Feature[] array = new Feature[(int)newSize];
				if (array != null)
				{
					this._features = array;
				}
			}
			this._featuresCount = 0;
			this._minimumAddCount = 3;
			return this._features != null;
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x0012A150 File Offset: 0x00129550
		internal bool Resize(ushort newSize, ushort keepCount)
		{
			this._featuresCount = keepCount;
			if (this._features != null && this._features.Length != 0 && keepCount > 0 && this._features.Length >= (int)keepCount)
			{
				ushort num = (ushort)this._features.Length;
				if (newSize < keepCount)
				{
					newSize = keepCount;
				}
				if (newSize > num)
				{
					if (newSize < num + this._minimumAddCount)
					{
						newSize = num + this._minimumAddCount;
					}
					Feature[] array = new Feature[(int)newSize];
					if (array == null)
					{
						return false;
					}
					for (int i = 0; i < (int)keepCount; i++)
					{
						array[i] = this._features[i];
					}
					this._features = array;
				}
				return true;
			}
			return this.Initialize(newSize);
		}

		// Token: 0x06004C27 RID: 19495 RVA: 0x0012A1E4 File Offset: 0x001295E4
		internal void AddFeature(Feature feature)
		{
			if ((int)this._featuresCount == this._features.Length && !this.Resize(this._featuresCount + 1, this._featuresCount))
			{
				return;
			}
			this._features[(int)this._featuresCount] = feature;
			this._featuresCount += 1;
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0012A238 File Offset: 0x00129638
		internal void AddFeature(ushort startIndex, ushort length, uint featureTag, uint parameter)
		{
			if ((int)this._featuresCount == this._features.Length && !this.Resize(this._featuresCount + 1, this._featuresCount))
			{
				return;
			}
			if (this._features[(int)this._featuresCount] != null)
			{
				this._features[(int)this._featuresCount].Tag = featureTag;
				this._features[(int)this._featuresCount].StartIndex = startIndex;
				this._features[(int)this._featuresCount].Length = length;
				this._features[(int)this._featuresCount].Parameter = parameter;
			}
			else
			{
				this._features[(int)this._featuresCount] = new Feature(startIndex, length, featureTag, parameter);
			}
			this._featuresCount += 1;
		}

		// Token: 0x06004C29 RID: 19497 RVA: 0x0012A2F4 File Offset: 0x001296F4
		internal void AddFeature(ushort charIx, uint featureTag)
		{
			if (featureTag == 1U)
			{
				return;
			}
			if (this._featuresCount <= 0)
			{
				if (featureTag != 0U)
				{
					this.AddFeature(charIx, 1, featureTag, 1U);
				}
				return;
			}
			ushort num = this._featuresCount - 1;
			if ((featureTag == 0U || featureTag == this._features[(int)num].Tag) && this._features[(int)num].StartIndex + this._features[(int)num].Length == charIx)
			{
				Feature feature = this._features[(int)num];
				feature.Length += 1;
				return;
			}
			this.AddFeature(charIx, 1, (featureTag == 0U) ? this._features[(int)num].Tag : featureTag, 1U);
		}

		// Token: 0x06004C2A RID: 19498 RVA: 0x0012A38C File Offset: 0x0012978C
		internal void UpdatePreviousShapedChar(uint featureTag)
		{
			if (featureTag <= 1U)
			{
				return;
			}
			if (this._featuresCount > 0)
			{
				ushort num = this._featuresCount - 1;
				if (this._features[(int)num].Tag != featureTag)
				{
					this._features[(int)num].Tag = featureTag;
				}
			}
		}

		// Token: 0x04002108 RID: 8456
		private ushort _minimumAddCount;

		// Token: 0x04002109 RID: 8457
		private ushort _featuresCount;

		// Token: 0x0400210A RID: 8458
		private Feature[] _features;
	}
}
