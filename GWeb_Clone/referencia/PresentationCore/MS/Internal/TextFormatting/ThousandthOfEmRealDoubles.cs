using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000761 RID: 1889
	internal sealed class ThousandthOfEmRealDoubles : IList<double>, ICollection<double>, IEnumerable<double>, IEnumerable
	{
		// Token: 0x06004F97 RID: 20375 RVA: 0x0013E154 File Offset: 0x0013D554
		internal ThousandthOfEmRealDoubles(double emSize, int capacity)
		{
			this._emSize = emSize;
			this.InitArrays(capacity);
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x0013E178 File Offset: 0x0013D578
		internal ThousandthOfEmRealDoubles(double emSize, IList<double> realValues)
		{
			this._emSize = emSize;
			this.InitArrays(realValues.Count);
			for (int i = 0; i < this.Count; i++)
			{
				this[i] = realValues[i];
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06004F99 RID: 20377 RVA: 0x0013E1C0 File Offset: 0x0013D5C0
		public int Count
		{
			get
			{
				if (this._shortList != null)
				{
					return this._shortList.Length;
				}
				return this._doubleList.Length;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06004F9A RID: 20378 RVA: 0x0013E1E8 File Offset: 0x0013D5E8
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700108D RID: 4237
		public double this[int index]
		{
			get
			{
				if (this._shortList != null)
				{
					return this.ThousandthOfEmToReal(this._shortList[index]);
				}
				return this._doubleList[index];
			}
			set
			{
				if (this._shortList == null)
				{
					this._doubleList[index] = value;
					return;
				}
				short num;
				if (this.RealToThousandthOfEm(value, out num))
				{
					this._shortList[index] = num;
					return;
				}
				this._doubleList = new double[this._shortList.Length];
				for (int i = 0; i < this._shortList.Length; i++)
				{
					this._doubleList[i] = this.ThousandthOfEmToReal(this._shortList[i]);
				}
				this._doubleList[index] = value;
				this._shortList = null;
			}
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x0013E2A4 File Offset: 0x0013D6A4
		public int IndexOf(double item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i] == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x0013E2D0 File Offset: 0x0013D6D0
		public void Clear()
		{
			if (this._shortList != null)
			{
				for (int i = 0; i < this._shortList.Length; i++)
				{
					this._shortList[i] = 0;
				}
				return;
			}
			for (int j = 0; j < this._doubleList.Length; j++)
			{
				this._doubleList[j] = 0.0;
			}
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x0013E328 File Offset: 0x0013D728
		public bool Contains(double item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x0013E344 File Offset: 0x0013D744
		public void CopyTo(double[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_ArrayCannotBeMultidimensional"), "array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (arrayIndex >= array.Length)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_IndexGreaterThanOrEqualToArrayLength", new object[]
				{
					"arrayIndex",
					"array"
				}), "arrayIndex");
			}
			if (array.Length - this.Count - arrayIndex < 0)
			{
				throw new ArgumentException(SR.Get("Collection_CopyTo_NumberOfElementsExceedsArrayLength", new object[]
				{
					"arrayIndex",
					"array"
				}));
			}
			for (int i = 0; i < this.Count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x0013E410 File Offset: 0x0013D810
		public IEnumerator<double> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x0013E42C File Offset: 0x0013D82C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<double>)this).GetEnumerator();
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x0013E440 File Offset: 0x0013D840
		public void Add(double value)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x0013E45C File Offset: 0x0013D85C
		public void Insert(int index, double item)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x0013E478 File Offset: 0x0013D878
		public bool Remove(double item)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x0013E494 File Offset: 0x0013D894
		public void RemoveAt(int index)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x0013E4B0 File Offset: 0x0013D8B0
		private void InitArrays(int capacity)
		{
			if (this._emSize > 48.0)
			{
				this._doubleList = new double[capacity];
				return;
			}
			this._shortList = new short[capacity];
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x0013E4E8 File Offset: 0x0013D8E8
		private bool RealToThousandthOfEm(double value, out short thousandthOfEm)
		{
			double num = value / this._emSize * 1000.0;
			if (num > 32767.0 || num < -32768.0)
			{
				thousandthOfEm = 0;
				return false;
			}
			thousandthOfEm = (short)Math.Round(num);
			return true;
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x0013E530 File Offset: 0x0013D930
		private double ThousandthOfEmToReal(short thousandthOfEm)
		{
			return (double)thousandthOfEm * 0.001 * this._emSize;
		}

		// Token: 0x04002422 RID: 9250
		private short[] _shortList;

		// Token: 0x04002423 RID: 9251
		private double[] _doubleList;

		// Token: 0x04002424 RID: 9252
		private double _emSize;

		// Token: 0x04002425 RID: 9253
		private const double ToThousandthOfEm = 1000.0;

		// Token: 0x04002426 RID: 9254
		private const double ToReal = 0.001;

		// Token: 0x04002427 RID: 9255
		private const double CutOffEmSize = 48.0;
	}
}
