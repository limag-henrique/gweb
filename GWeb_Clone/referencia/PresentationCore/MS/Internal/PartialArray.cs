using System;
using System.Collections;
using System.Collections.Generic;
using MS.Internal.PresentationCore;

namespace MS.Internal
{
	// Token: 0x02000691 RID: 1681
	internal struct PartialArray<T> : IList<T>, ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060049DC RID: 18908 RVA: 0x0011F74C File Offset: 0x0011EB4C
		public PartialArray(T[] array, int initialIndex, int count)
		{
			this._array = array;
			this._initialIndex = initialIndex;
			this._count = count;
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x0011F770 File Offset: 0x0011EB70
		public PartialArray(T[] array)
		{
			this = new PartialArray<T>(array, 0, array.Length);
		}

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x0011F788 File Offset: 0x0011EB88
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x0011F798 File Offset: 0x0011EB98
		public bool Contains(T item)
		{
			return this.IndexOf(item) >= 0;
		}

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x0011F7B4 File Offset: 0x0011EBB4
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x0011F7C4 File Offset: 0x0011EBC4
		public bool Remove(T item)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x060049E2 RID: 18914 RVA: 0x0011F7E0 File Offset: 0x0011EBE0
		public void RemoveAt(int index)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x060049E3 RID: 18915 RVA: 0x0011F7FC File Offset: 0x0011EBFC
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060049E4 RID: 18916 RVA: 0x0011F810 File Offset: 0x0011EC10
		public void Add(T item)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x060049E5 RID: 18917 RVA: 0x0011F82C File Offset: 0x0011EC2C
		public void Insert(int index, T item)
		{
			throw new NotSupportedException(SR.Get("CollectionIsFixedSize"));
		}

		// Token: 0x17000F4F RID: 3919
		public T this[int index]
		{
			get
			{
				return this._array[index + this._initialIndex];
			}
			set
			{
				this._array[index + this._initialIndex] = value;
			}
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x0011F88C File Offset: 0x0011EC8C
		public int IndexOf(T item)
		{
			int num = Array.IndexOf<T>(this._array, item, this._initialIndex, this._count);
			if (num >= 0)
			{
				return num - this._initialIndex;
			}
			return -1;
		}

		// Token: 0x17000F50 RID: 3920
		// (get) Token: 0x060049E9 RID: 18921 RVA: 0x0011F8C0 File Offset: 0x0011ECC0
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x0011F8D4 File Offset: 0x0011ECD4
		public void CopyTo(T[] array, int arrayIndex)
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

		// Token: 0x060049EB RID: 18923 RVA: 0x0011F9A4 File Offset: 0x0011EDA4
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x0011F9C4 File Offset: 0x0011EDC4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x04001EE2 RID: 7906
		private T[] _array;

		// Token: 0x04001EE3 RID: 7907
		private int _initialIndex;

		// Token: 0x04001EE4 RID: 7908
		private int _count;
	}
}
