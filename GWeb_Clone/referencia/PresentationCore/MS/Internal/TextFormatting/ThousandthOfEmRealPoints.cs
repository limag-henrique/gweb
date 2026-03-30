using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using MS.Internal.PresentationCore;

namespace MS.Internal.TextFormatting
{
	// Token: 0x02000762 RID: 1890
	internal sealed class ThousandthOfEmRealPoints : IList<Point>, ICollection<Point>, IEnumerable<Point>, IEnumerable
	{
		// Token: 0x06004FAA RID: 20394 RVA: 0x0013E550 File Offset: 0x0013D950
		internal ThousandthOfEmRealPoints(double emSize, int capacity)
		{
			this.InitArrays(emSize, capacity);
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x0013E56C File Offset: 0x0013D96C
		internal ThousandthOfEmRealPoints(double emSize, IList<Point> pointValues)
		{
			this.InitArrays(emSize, pointValues.Count);
			for (int i = 0; i < this.Count; i++)
			{
				this._xArray[i] = pointValues[i].X;
				this._yArray[i] = pointValues[i].Y;
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06004FAC RID: 20396 RVA: 0x0013E5D4 File Offset: 0x0013D9D4
		public int Count
		{
			get
			{
				return this._xArray.Count;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06004FAD RID: 20397 RVA: 0x0013E5EC File Offset: 0x0013D9EC
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001090 RID: 4240
		public Point this[int index]
		{
			get
			{
				return new Point(this._xArray[index], this._yArray[index]);
			}
			set
			{
				this._xArray[index] = value.X;
				this._yArray[index] = value.Y;
			}
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x0013E65C File Offset: 0x0013DA5C
		public int IndexOf(Point item)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this._xArray[i] == item.X && this._yArray[i] == item.Y)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x0013E6A8 File Offset: 0x0013DAA8
		public void Clear()
		{
			this._xArray.Clear();
			this._yArray.Clear();
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x0013E6CC File Offset: 0x0013DACC
		public bool Contains(Point item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x0013E6E8 File Offset: 0x0013DAE8
		public void CopyTo(Point[] array, int arrayIndex)
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

		// Token: 0x06004FB4 RID: 20404 RVA: 0x0013E7B8 File Offset: 0x0013DBB8
		public IEnumerator<Point> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x0013E7D4 File Offset: 0x0013DBD4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<Point>)this).GetEnumerator();
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x0013E7E8 File Offset: 0x0013DBE8
		public void Add(Point value)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x0013E804 File Offset: 0x0013DC04
		public void Insert(int index, Point item)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x0013E820 File Offset: 0x0013DC20
		public bool Remove(Point item)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x0013E83C File Offset: 0x0013DC3C
		public void RemoveAt(int index)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x0013E858 File Offset: 0x0013DC58
		private void InitArrays(double emSize, int capacity)
		{
			this._xArray = new ThousandthOfEmRealDoubles(emSize, capacity);
			this._yArray = new ThousandthOfEmRealDoubles(emSize, capacity);
		}

		// Token: 0x04002428 RID: 9256
		private ThousandthOfEmRealDoubles _xArray;

		// Token: 0x04002429 RID: 9257
		private ThousandthOfEmRealDoubles _yArray;
	}
}
