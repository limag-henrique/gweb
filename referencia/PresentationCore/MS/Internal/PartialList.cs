using System;
using System.Collections;
using System.Collections.Generic;

namespace MS.Internal
{
	// Token: 0x0200067D RID: 1661
	internal class PartialList<T> : IList<T>, ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06004941 RID: 18753 RVA: 0x0011DE98 File Offset: 0x0011D298
		public PartialList(IList<T> list)
		{
			this._list = list;
			this._initialIndex = 0;
			this._count = list.Count;
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x0011DEC8 File Offset: 0x0011D2C8
		public PartialList(IList<T> list, int initialIndex, int count)
		{
			this._list = list;
			this._initialIndex = initialIndex;
			this._count = count;
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x0011DEF0 File Offset: 0x0011D2F0
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004944 RID: 18756 RVA: 0x0011DF04 File Offset: 0x0011D304
		public void Insert(int index, T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000F26 RID: 3878
		public T this[int index]
		{
			get
			{
				return this._list[index + this._initialIndex];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x0011DF4C File Offset: 0x0011D34C
		public int IndexOf(T item)
		{
			int num = this._list.IndexOf(item);
			if (num == -1 || num < this._initialIndex || num - this._initialIndex >= this._count)
			{
				return -1;
			}
			return num - this._initialIndex;
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06004948 RID: 18760 RVA: 0x0011DF90 File Offset: 0x0011D390
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004949 RID: 18761 RVA: 0x0011DFA0 File Offset: 0x0011D3A0
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600494A RID: 18762 RVA: 0x0011DFB4 File Offset: 0x0011D3B4
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x0011DFC8 File Offset: 0x0011D3C8
		public bool Contains(T item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x0011DFE4 File Offset: 0x0011D3E4
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x0600494D RID: 18765 RVA: 0x0011DFF8 File Offset: 0x0011D3F8
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x0011E00C File Offset: 0x0011D40C
		public void CopyTo(T[] array, int arrayIndex)
		{
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			for (int i = 0; i < this._count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x0011E04C File Offset: 0x0011D44C
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			int num;
			for (int i = this._initialIndex; i < this._initialIndex + this._count; i = num)
			{
				yield return this._list[i];
				num = i + 1;
			}
			yield break;
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x0011E068 File Offset: 0x0011D468
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x04001CD0 RID: 7376
		private IList<T> _list;

		// Token: 0x04001CD1 RID: 7377
		private int _initialIndex;

		// Token: 0x04001CD2 RID: 7378
		private int _count;
	}
}
