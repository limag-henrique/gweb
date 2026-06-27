using System;
using System.Collections.Generic;

namespace MS.Internal
{
	// Token: 0x0200067E RID: 1662
	internal class LegacyPriorityQueue<T>
	{
		// Token: 0x06004951 RID: 18769 RVA: 0x0011E07C File Offset: 0x0011D47C
		internal LegacyPriorityQueue(int capacity, IComparer<T> comparer)
		{
			this._heap = new T[(capacity > 0) ? capacity : 6];
			this._count = 0;
			this._comparer = comparer;
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x0011E0B0 File Offset: 0x0011D4B0
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06004953 RID: 18771 RVA: 0x0011E0C4 File Offset: 0x0011D4C4
		internal T Top
		{
			get
			{
				return this._heap[0];
			}
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x0011E0E0 File Offset: 0x0011D4E0
		internal void Push(T value)
		{
			if (this._count == this._heap.Length)
			{
				T[] array = new T[this._count * 2];
				for (int i = 0; i < this._count; i++)
				{
					array[i] = this._heap[i];
				}
				this._heap = array;
			}
			int j;
			int num;
			for (j = this._count; j > 0; j = num)
			{
				num = LegacyPriorityQueue<T>.HeapParent(j);
				if (this._comparer.Compare(value, this._heap[num]) >= 0)
				{
					break;
				}
				this._heap[j] = this._heap[num];
			}
			this._heap[j] = value;
			this._count++;
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x0011E19C File Offset: 0x0011D59C
		internal void Pop()
		{
			if (this._count > 1)
			{
				int num = 0;
				for (int i = LegacyPriorityQueue<T>.HeapLeftChild(num); i < this._count; i = LegacyPriorityQueue<T>.HeapLeftChild(num))
				{
					int num2 = LegacyPriorityQueue<T>.HeapRightFromLeft(i);
					int num3 = (num2 < this._count && this._comparer.Compare(this._heap[num2], this._heap[i]) < 0) ? num2 : i;
					this._heap[num] = this._heap[num3];
					num = num3;
				}
				this._heap[num] = this._heap[this._count - 1];
			}
			this._count--;
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x0011E254 File Offset: 0x0011D654
		private static int HeapParent(int i)
		{
			return (i - 1) / 2;
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x0011E268 File Offset: 0x0011D668
		private static int HeapLeftChild(int i)
		{
			return i * 2 + 1;
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x0011E27C File Offset: 0x0011D67C
		private static int HeapRightFromLeft(int i)
		{
			return i + 1;
		}

		// Token: 0x04001CD3 RID: 7379
		private T[] _heap;

		// Token: 0x04001CD4 RID: 7380
		private int _count;

		// Token: 0x04001CD5 RID: 7381
		private IComparer<T> _comparer;

		// Token: 0x04001CD6 RID: 7382
		private const int DefaultCapacity = 6;
	}
}
